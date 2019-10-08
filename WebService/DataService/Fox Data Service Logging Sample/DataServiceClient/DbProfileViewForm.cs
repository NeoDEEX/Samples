using CommonLib;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;
using TheOne.Windows.Forms;

namespace DataServiceClient
{
    public partial class DbProfileViewForm : FormBase
    {
        public DbProfileViewForm()
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

        #endregion

        #region 데이터 액세스 메서드들

        // DB Profile 정보 조회
        private async Task QueryContextInfo()
        {
            var request = FoxDataRequest.Create("DbProfile.GetDbProfile");
            request["DateFrom"] = dateFrom.DateTime; ;
            request["DateTo"] = dateTo.DateTime;
            request.Parameters.SetNullToDBNull();
            // 이 조회 자체가 성능 로그 및 DB Profile을 기록하지 않도록 설정한다.
            request.Diagnostics = FoxDataRequestDiagnostics.SupressPerfLogWrite | FoxDataRequestDiagnostics.SupressDbProfileWrite;

            using (var scope = new FoxProgressScope(this))
            {
                grdDbProfile.DataSource = null;
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    var response = await client.ExecuteDataSetAsync(request);
                    grdDbProfile.DataSource = response.DataSet.Tables[0];
                }
            }
        }

        // DB Profile 로그를 모두 제거 한다.
        private async Task DeleteAllPerformanceLog()
        {
            var request = FoxDataRequest.Create("DbProfile.DeleteDbProfile");
            // 이 조회 자체가 성능 로그 및 DB Profile을 기록하지 않도록 설정한다.
            request.Diagnostics = FoxDataRequestDiagnostics.SupressPerfLogWrite | FoxDataRequestDiagnostics.SupressDbProfileWrite;
            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    await client.ExecuteNonQueryAsync(request);
                }
                grdDbProfile.DataSource = null;
            }
        }

        #endregion

        #region 이벤트 핸들러

        // 폼 로드 이벤트
        private void DbProfileViewForm_Load(object sender, EventArgs e)
        {
            SetRecentOneHour();
        }

        // 조회 버튼 클릭 이벤트 핸들러
        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            if (ValidateCondiation() == true)
            {
                await QueryContextInfo();
            }
        }

        // DB Profile 로그 전체 삭제 버튼 클릭 이벤트 핸들러
        private async void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            var result = FoxMessageBox.Show(this,
                "DB Profile 로그 데이터 전체를 삭제합니다.\r\n\r\n삭제 하시겠습니까?",
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

        // 그리드 컨트롤 더블 클릭 이벤트 핸들러
        private void GrvDbProfile_DoubleClick(object sender, EventArgs e)
        {
            var mouseArgs = e as DXMouseEventArgs;
            var info = grvDbProfile.CalcHitInfo(mouseArgs.Location);
            // 더블 클릭시 해당 Row의 상세 정보를 팝업으로 표시한다.
            if (info.InRow && info.InRowCell)
            {
                var handle = info.RowHandle;
                var dbProfileRow = grvDbProfile.GetDataRow(handle);
                if (dbProfileRow != null)
                {
                    var popup = new DbProfileViewPopup();
                    popup.DataRow = dbProfileRow;
                    popup.ShowDialog(this);
                }
            }
        }

        #endregion
    }
}
