using System;
using System.Collections.Generic;
using System.Data;
using TheOne.Data;
using TheOne.Data.Diagnostics;
using TheOne.Diagnostics;
using TheOne.Diagnostics.Loggers;

namespace CommonLib
{
    //
    // FoxDbLoggerBase를 기반으로 DB Profile을 DB에 기록하는 로거 예제
    //
    // FoxDbLogger를 사용하면 SQL Text, SP, Fox Query 중 하나를 사용하여 DB 액세스를 사용할 수 있다.
    // 이 예제는 SP를 사용하여 DB Profile을 기록한다.
    //
    public class DbProfileDbLogger : FoxDbLoggerBase
    {
        public DbProfileDbLogger(string name)
            : base(name)
        {
        }

        protected override void WriteLog(string source, FoxLogLevel level, object data)
        {
            // DB Profile 로그만 기록한다.
            if (data is FoxDbProfileInfo)
            {
                base.WriteLog(source, level, data);
            }
        }

        // Command Text 혹은 저장 프로시저 사용 시 GetParamterCollection 메서드를 override 하면 된다.
        protected override FoxDbParameterCollection GetParameterCollection(FoxDbAccess db, FoxLogEntry logEntry)
        {
            var info = logEntry.Data as FoxDbProfileInfo;
            // DB에 무관한 DbType을 사용했지만 특정 DB에 기록한다면 SqlDbParamCollection, OracleDbParamCollection 등의
            // 타입을 사용하고 DbSqlType, DbOracleType 등을 사용하여 매개변수 타입을 스키마에 정확하게 맞게 구성할 수
            // 있다.
            var parameters = db.CreateParamCollection();
            parameters.AddWithValue("LogId", DbType.String, info.InfoId.ToString());
            parameters.AddWithValue("LogTimestamp", DbType.DateTime, DateTime.Now);
            parameters.AddWithValue("UserId", DbType.String, info.UserId);
            parameters.AddWithValue("FoxQueryId", DbType.String, info.QueryId);
            parameters.AddWithValue("ExecutionType", DbType.String, info.ExecutionType.ToString());
            parameters.AddWithValue("ExecutionTime", DbType.Decimal, info.ExecutionTime);
            parameters.AddWithValue("QueryParameters", DbType.String, info.ParametersString);
            parameters.AddWithValue("QueryText", DbType.String, info.CommandText);
            parameters.AddWithValue("ResultString", DbType.String, info.ResultString);
            parameters.AddWithValue("InlineQuery", DbType.String, info.QueryString);
            parameters.AddWithValue("CallerName", DbType.String, info.CallerName);
            parameters.AddWithValue("ExceptionType", DbType.String, info.DbExceptionInfo?.ExceptionType);
            parameters.AddWithValue("ErrorCode", DbType.Decimal, info.DbExceptionInfo?.ErrorCode);
            parameters.AddWithValue("ErrorMessage", DbType.String, info.DbExceptionInfo?.Message);
            return parameters;
        }

        // Fox Query 사용 시 GetParameterObject를 override 해야 한다.
        protected override object GetParameterObject(FoxLogEntry logEntry)
        {
            var info = logEntry.Data as FoxDbProfileInfo;
            var parameters = new Dictionary<string, object>();
            parameters.Add("LogId", info.InfoId.ToString());
            parameters.Add("LogTimestamp", logEntry.Timestamp);
            parameters.Add("UserId", info.UserId);
            parameters.Add("FoxQueryId", info.QueryId);
            parameters.Add("ExecutionType", info.ExecutionType.ToString());
            parameters.Add("ExecutionTime", info.ExecutionTime);
            parameters.Add("QueryParameters", info.ParametersString);
            parameters.Add("QueryText", info.CommandText);
            parameters.Add("ResultString", info.ResultString);
            parameters.Add("InlineQuery", info.QueryString);
            parameters.Add("CallerName", info.CallerName);
            parameters.Add("ExceptionType", info.DbExceptionInfo?.ExceptionType);
            parameters.Add("ErrorCode", info.DbExceptionInfo?.ErrorCode);
            parameters.Add("ErrorMessage", info.DbExceptionInfo?.Message);
            parameters.SetNullToDBNull();
            return parameters;
        }
    }
}
