using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TheOne.Data;
using TheOne.Diagnostics;
using TheOne.Diagnostics.Loggers;

namespace CommonLib
{
    //
    // 성능 정보를 DB에 기록하는 로거 구현
    //
    // 이 로거는 FoxLoggerBase에서 파생되어 직접 DB 액세스를 수행하는 로거 이다.
    // 따라서 FoxDbLogger 처럼 TPL을 사용하여 유연한 비동기 기록 메커니즘을 제공하지 않고
    // 단순한 비동기 메커니즘만을 제공한다.
    //
    // 성능 정보는 성능 활동 1 건과 성능 문맥 여러 건을 저장해야 하므로 FoxDbLogger를 통해 사용하는 것이
    // 효율적이지 못하다. FoxDbLogger는 외부에서 DB 기록을 위한 명령(SQL 문장, SP 이름, Fox Query)을
    // 외부에서 설정하기 때문에 master-detail 구조를 갖는 여러 건의 데이터 기록에 적합하지 않기 때문이다.
    // (테이블 타입의 매개변수를 사용하는 경우 FoxDbLogger를 이용하는 것이 가능할 수는 있다)
    public class PerformanceInfoDbLogger : FoxLoggerBase
    {
        // 디폴트 QueryId 들
        public const string DefaultActivityQueryId = "PerformanceLog.InsertActivtyInfo";
        public const string DefaultContextQueryId = "PerformanceLog.InsertContextInfo";

        #region 생성자들

        public PerformanceInfoDbLogger(string name)
            : base(name)
        {
            this.ConnectionStringName = null;
            this.ActivityQueryId = DefaultActivityQueryId;
            this.ContextQueryId = DefaultContextQueryId;
        }

        public PerformanceInfoDbLogger(string name, string connectionStringName = null, string activityQueryId = DefaultActivityQueryId, string contextQueryId = DefaultContextQueryId)
            : base(name)
        {
            this.ConnectionStringName = connectionStringName;
            this.ActivityQueryId = activityQueryId;
            this.ContextQueryId = contextQueryId;
        }

        #endregion

        #region public 속성

        // configuration의 연결 문자열 이름
        public string ConnectionStringName { get; set; }

        // Activity 성능 정보를 기록하는 Fox Query Id
        public string ActivityQueryId { get; set; }

        // Context 성능 정보를 기록하는 Fox Query Id
        public string ContextQueryId { get; set; }
        // 예외 발생 여부 플래그
        public bool Debug { get; set; } = true;

        #endregion

        #region FoxLoggerBase 로거 구현

        // FoxLoggerBase 핵심 메서드 구현
        protected override void WriteLog(string source, FoxLogLevel level, object data)
        {
            var activityInfo = data as FoxPerformanceActivityInfo;
            if (activityInfo != null)
            {
                WriteActivityInfo(activityInfo);
            }
        }

        // FoxLoggerBase 핵심 메서드 구현
        // 이 메서드는 결코 호출되지 않는다.
        protected override void WriteLogMessage(string source, FoxLogLevel level, string message)
        {
            throw new InvalidOperationException("This method cannot be invoked. Verify the call stack.");
        }

        #endregion

        // 성능 활동 정보를 DB에 기록한다.
        private void WriteActivityInfo(FoxPerformanceActivityInfo activityInfo)
        {
            // 디버깅 모드인 경우 동기적으로 작업을 수행하고
            // 그렇지 않은 경우 비동기 모드로 작업을 수행한다.
            if (this.Debug == true)
            {
                WriteActivityInfoCore(activityInfo);
            }
            else
            {
                Task.Run(() => WriteActivityInfoCore(activityInfo));
            }
        }

        // 성능 활동 정보를 기록하는 핵심 메서드
        private void WriteActivityInfoCore(FoxPerformanceActivityInfo activityInfo)
        {
            // 외부 트랜잭션과 연관되지 않도록 TransactionScopeOption.Suppress 옵션으로
            // TransactionScope를 구성한다.
            using (var txScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dbAccess = GetDbAccess();
                dbAccess.Open();
                // 성능 활동/문맥 정보가 하나로 묶여 저장되도록 로컬 트랜잭션을 사용한다.
                dbAccess.BeginTrans();
                try
                {
                    // 성능 활동 정보(1건) 저장
                    InsertActivityInfo(dbAccess, activityInfo);
                    // 성능 문맥 정보(1건 이상) 저장
                    InsertContextInfo(dbAccess, activityInfo);
                    dbAccess.CommitTrans();
                }
                catch (Exception ex)
                {
                    // 오류가 발생한 경우, 트랜잭션 롤백
                    dbAccess.RollbackTrans();
                    // 디버깅인 경우에만 예외를 유발하고 그렇지 않은 경우 SafeLogger에 기록한다.
                    if (this.Debug == true)
                    {
                        throw;
                    }
                    else
                    {
                        FoxLogManager.SafeLogger.Write(FoxLogLevel.Error, ex);
                    }
                }
                finally
                {
                    dbAccess.Close();
                }
            }
        }

        // 성능 활동 정보를 DB에 기록한다.
        private void InsertActivityInfo(FoxDbAccess dbAccess, FoxPerformanceActivityInfo activityInfo)
        {
            // Fox Query를 사용하여 1건의 데이터를 기록한다.
            var parameters = new Dictionary<string, object>();
            parameters.Add("ActivityId", activityInfo.Id.ToString());
            parameters.Add("ActivityName", activityInfo.Name);
            parameters.Add("LogTimestamp", activityInfo.LocalTimestamp);
            parameters.Add("ElapsedTime", activityInfo.ElapsedMilliseconds);
            parameters.Add("Category", activityInfo.Category);
            parameters.Add("MachineName", activityInfo.MachineName);
            parameters.Add("ProcessId", activityInfo.ProcessId);
            dbAccess.ExecuteQueryNonQuery(this.ActivityQueryId, parameters);
        }

        // 성능 문맥 정보를 DB에 기록한다.
        // 기록 성능 및 효율 향상을 위해 배치 업데이트(DbDataAdapter.Update)를 사용한다.
        private void InsertContextInfo(FoxDbAccess dbAccess, FoxPerformanceActivityInfo activityInfo)
        {
            // 배치 업데이트를 위해 임시 테이블을 생성한다.
            var dt = new DataTable();
            dt.Columns.Add("ActivityId", typeof(string));
            dt.Columns.Add("ContextId", typeof(int));
            dt.Columns.Add("ContextName", typeof(string));
            dt.Columns.Add("LogTimestamp", typeof(DateTime));
            dt.Columns.Add("InclusiveTime", typeof(decimal));
            dt.Columns.Add("ExclusiveTime", typeof(decimal));
            dt.Columns.Add("ParentContextId", typeof(int));

            // 성능 문맥 정보를 임시 테이블에 추가 한다.
            foreach(var contextInfo in activityInfo.PerformanceContextInfos)
            {
                var row = dt.NewRow();
                row.SetField("ActivityId", activityInfo.Id.ToString());
                row.SetField("ContextId", contextInfo.Id);
                row.SetField("ContextName", contextInfo.Name);
                row.SetField("LogTimestamp", contextInfo.LocalTimestamp);
                row.SetField("InclusiveTime", (decimal)contextInfo.InclusiveTime);
                row.SetField("ExclusiveTime", (decimal)contextInfo.ExclusiveTime);
                row.SetField("ParentContextId", contextInfo.ParentId);
                dt.Rows.Add(row);
            }

            // INSERT를 위한 Fox Query로부터 Command 객체를 생성한다.
            // 주) 배치 업데이트를 위해서는 매개변수를 null로 하여 Command 객체를 생성해야 한다.
            var foxQuery = dbAccess.GetQuery(ContextQueryId);
            var cmd = dbAccess.CreateCommand(foxQuery, null);
            // Fox Query의 매개변수를 반영하여 배치 업데이트에서 사용할 수 있도록 매개변수를 구성한다.
            dbAccess.SetParameterForAdapterUpdate(cmd, foxQuery);
            // 트랜잭션이 반영되도록 Command 객체에 트랜잭션 객체를 설정한다.
            cmd.Transaction = dbAccess.DbTransaction;
            var adapter = dbAccess.CreateDataAdapter();
            adapter.InsertCommand = cmd as DbCommand;
            adapter.Update(dt);
        }

        // DatabaseFactory를 통해 FoxDbAccess 객체를 반환한다.
        private FoxDbAccess GetDbAccess()
        {
            FoxDbAccess db;
            if (this.ConnectionStringName == null)
            {
                db = FoxDatabaseFactory.CreateDatabase();
            }
            else
            {
                db = FoxDatabaseFactory.CreateDatabase(this.ConnectionStringName);
            }
            db.DbProfile.Enabled = false;
            return db;
        }
    }
}
