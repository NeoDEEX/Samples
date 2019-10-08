using CommonLib;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;
using TheOne.Windows.Forms;

namespace DataServiceClient
{
    public partial class PerformanceLogViewerForm : FormBase
    {
        public PerformanceLogViewerForm()
        {
            InitializeComponent();
        }

        #region 헬퍼 메서드들

        // 조회 조건 기간에 대한 유효성 확인
        private bool ValidateCondiation()
        {
            var fromDate = dateFrom.DateTime;
            var toDate = dateTo.DateTime;

            if (fromDate > toDate)
            {
                FoxMessageBox.Show(this, $"Invalid From/To date time", "Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // 조회 기간을 최근 1시간으로 설정한다.
        private void SetRecentOneHour()
        {
            var from = DateTime.Now - TimeSpan.FromHours(1);
            var to = DateTime.Now;
            dateFrom.DateTime = new DateTime(from.Year, from.Month, from.Day, from.Hour, from.Minute, 0);
            dateTo.DateTime = new DateTime(to.Year, to.Month, to.Day, to.Hour, to.Minute, 59);
        }

        // 주어진 좌표(마우스 좌표)가 성능 문맥 이름 노드인가를 확인하여 반환한다.
        private TreeListNode GetContextNameNodeFromPoint(Point location)
        {
            var info = tlstContextInfo.CalcHitInfo(location);
            if (info.InRow && info.InRowCell && info.Column.FieldName == "ContextName")
            {
                return info.Node;
            }
            return null;
        }

        // 주어진 성능 문맥이 유효한 DB Profile 로그 정보를 포함하는지 반환한다.
        private bool IsNodeDbProfile(TreeListNode node)
        {
            var row = tlstContextInfo.GetDataRow(node.Id);
            if (row != null)
            {
                return row.Field<bool>("IsDbProfile");
            }
            return false;
        }

        #endregion

        #region 데이터 액세스 메서드들

        // 데이터 바인딩을 모두 클리어 한다.
        private void ClearDataSource()
        {
            grdActivityInfo.DataSource = null;
            tlstContextInfo.DataSource = null;
        }

        // 성능 정보(성능 활동 및 성능 문맥) 조회
        private async Task QueryPerformanceInfo()
        {
            var request = FoxDataRequest.Create("PerformanceLog.GetPerformanceInfo");
            request["DateFrom"] = dateFrom.DateTime;
            request["DateTo"] = dateTo.DateTime;
            request.Parameters.SetNullToDBNull();
            // 이 조회 자체가 성능 로그 및 DB Profile을 기록하지 않도록 설정한다.
            request.Diagnostics = FoxDataRequestDiagnostics.SupressPerfLogWrite | FoxDataRequestDiagnostics.SupressDbProfileWrite;

            using (var scope = new FoxProgressScope(this))
            {
                ClearDataSource();
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    var response = await client.ExecuteDataSetAsync(request);
                    // DataTable 이름 구성
                    var ds = response.DataSet;
                    var activityTable = ds.Tables[0];
                    var contextTable = ds.Tables[1];
                    activityTable.TableName = "ActivityInfo";
                    contextTable.TableName = "ContextInfo";
                    // 각 성능 문맥이 DB Profile 정보를 포함하는지 여부 설정
                    SetDBProfileFlag(contextTable);
                    // 마스터-디테일 DataReleation 구성
                    var relation = new DataRelation("PerformanceInfoRelation", activityTable.Columns["ActivityId"], contextTable.Columns["ActivityId"]);
                    ds.Relations.Add(relation);
                    // 데이터 소스 구성
                    bsActivityInfo.DataSource = ds;
                    bsActivityInfo.DataMember = activityTable.TableName;
                    bsContextInfo.DataSource = bsActivityInfo;
                    bsContextInfo.DataMember = "PerformanceInfoRelation";
                    // 바인딩 수행
                    grdActivityInfo.DataSource = bsActivityInfo;
                    tlstContextInfo.DataSource = bsContextInfo;
                }
                // 초기 표시 데이터를 위한 ExpandAll 메서드 호출
                // 이후에는 ActivityInfo 그리드의 FocusedRowChanged 이벤트에서 ExpandAll 메서드 호출
                tlstContextInfo.ExpandAll();
            }
        }

        // 주어진 데이터 테이블을 검사하여 성능 문맥의 이름이 유효한 DB Profile ID를 가지는지 확인하여 설정한다.
        private static void SetDBProfileFlag(DataTable contextTable)
        {
            // 성능 문맥이 유효한 DB Profile ID 인가를 확인하는 정규식
            // 유혀한 DB Profile ID: DB-{GUID}
            var regex = new Regex(@"(?im)^DB-[{]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[}]?$");
            // 매번 정규식을 통해 ID를 확인하지 않기 위해 테이블에 컬럼을 추가하여 기록해 둔다.
            contextTable.Columns.Add("IsDbProfile", typeof(bool));
            foreach (DataRow row in contextTable.Rows)
            {
                var contextName = row.Field<string>("ContextName");
                row.SetField("IsDbProfile", regex.IsMatch(contextName));
            }
        }

        // 주어진 Id를 가진 DB Profile 로그 정보를 조회한다.
        private async Task<DataRow> QueryDbProfileInfo(string logId)
        {
            var request = FoxDataRequest.Create("DbProfile.GetDbProfileEntity");
            request["LogId"] = logId;
            // 이 조회 자체가 성능 로그 및 DB Profile을 기록하지 않도록 설정한다.
            request.Diagnostics = FoxDataRequestDiagnostics.SupressPerfLogWrite | FoxDataRequestDiagnostics.SupressDbProfileWrite;
            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    var response = await client.ExecuteDataSetAsync(request);
                    var dt = response.DataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
                return null;
            }
        }

        // 성능 로그를 모두 제거 한다.
        private async Task DeleteAllPerformanceLog()
        {
            var request = FoxDataRequest.Create("PerformanceLog.DeletePerformanceInfo");
            // 이 조회 자체가 성능 로그 및 DB Profile을 기록하지 않도록 설정한다.
            request.Diagnostics = FoxDataRequestDiagnostics.SupressPerfLogWrite | FoxDataRequestDiagnostics.SupressDbProfileWrite;
            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    await client.ExecuteNonQueryAsync(request);
                    await Task.Delay(2000);
                }
                ClearDataSource();
            }
        }

        #endregion

        #region 이벤트 핸들러

        // 폼 로드 이벤트 핸들러
        private void PerformanceLogViewerForm_Load(object sender, EventArgs e)
        {
            SetRecentOneHour();
        }

        // 조회 버튼 클릭 이벤트 핸들러
        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            if (ValidateCondiation() == true)
            {
                await QueryPerformanceInfo();
            }
        }

        // 성능 로그 전체 삭제 버튼 클릭 이벤트 핸들러
        private async void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            var result = FoxMessageBox.Show(this,
                "성능 로그 데이터 전체를 삭제합니다.\r\n\r\n삭제 하시겠습니까?",
                "데이터 전체 삭제",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                await DeleteAllPerformanceLog();
            }
        }

        // 최근 1시간 설정 버튼 클릭 이벤트 핸들러
        private void BtnRecentHour_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetRecentOneHour();
        }

        // 오늘 설정 버튼 클릭 이벤트 핸들러
        private void BtnToday_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var now = DateTime.Now;
            dateFrom.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            dateTo.DateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 59);
        }

        // 성능 활동 그리드 행 변경 이벤트 핸들러
        private void GrvActivityInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            tlstContextInfo.ExpandAll();
        }

        // TreeList의 셀 스타일 설정 이벤트 핸들러
        private void TlstContextInfo_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column == colContextName)
            {
                // DbProfile을 나타내는 성능 문맥인 경우 폰트를 바꾸어 준다.
                if (IsNodeDbProfile(e.Node) == true)
                {
                    e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.FontStyleDelta = FontStyle.Underline;
                }
            }
        }

        // 트리 리스트 더블 클릭 이벤트 핸들러
        private async void TlstContextInfo_DoubleClick(object sender, EventArgs e)
        {
            var mouseArgs = e as DXMouseEventArgs;
            // ContextName 컬럼에서 유효한 DB Profile 이름이 존재한 경우에만 
            // 더블 클릭할 때에만 DB Profile 조회를 시도한다.
            var node = GetContextNameNodeFromPoint(mouseArgs.Location);
            if (node != null)
            {
                var row = tlstContextInfo.GetDataRow(node.Id);
                var contextName = row.Field<string>("ContextName");
                var dbProfileRow = await QueryDbProfileInfo(contextName);
                if (dbProfileRow != null)
                {
                    var popup = new DbProfileViewPopup();
                    popup.DataRow = dbProfileRow;
                    popup.ShowDialog(this);
                }
                else
                {
                    FoxMessageBox.Show(this, $"다음 ID를 가진 DB Profile 정보를 찾을 수 없습니다.\r\n\r\n{contextName}",
                        "DB Profile 상세 조회 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // TreeList에서 마우스가 DB Profile을 포함하는 성능 문맥인 경우 마우스 커서 모양을 변경한다.
        private void TlstContextInfo_MouseMove(object sender, MouseEventArgs e)
        {
            var node = GetContextNameNodeFromPoint(e.Location);
            if (node != null && IsNodeDbProfile(node) == true)
            {
                tlstContextInfo.Cursor = Cursors.Hand;
            }
            else
            {
                tlstContextInfo.Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}
