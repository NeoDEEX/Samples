using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using TheOne.Security;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;

namespace SampleClientApp
{
    public partial class MainForm : XtraForm
    {
        private static readonly string SelectQueryId = "Common.GetProducts";

        #region 생성자

        public MainForm()
        {
            InitializeComponent();

            // Fox Data Service REST API를 사용하도록 클라이언트 팩터리를 교체한다.
            FoxClientFactory.DataClientFactory = new DataClientFactory();
            // 기본 사용자를 선택한다.
            ChangeUser();
        }

        #endregion

        #region 조회 관련 루틴

        // 조회 버튼 클릭 이벤트 핸들러
        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            using (var client = FoxClientFactory.CreateDataClient("api/dataservice"))
            {
                var response = await client.ExecuteDataSetAsync(SelectQueryId);
                grdProducts.DataSource = response.DataSet.Tables[0];
            }
        }

        #endregion

        #region 데이터 변경 관련 루틴

        // 수정된 데이터만을 포함하는 데이터 테이블을 반환한다.
        private DataTable GetChanages()
        {
            var dt = grdProducts.DataSource as DataTable;
            var changedDataTable = dt.GetChanges(DataRowState.Modified);
            if (changedDataTable == null || changedDataTable.Rows.Count == 0)
            {
                return null;
            }
            return changedDataTable;
        }

        // 실제 데이터 업데이트를 수행한다.
        private async Task DoUpdate(string updateQueryId, DataTable dt)
        {
            using (var client = FoxClientFactory.CreateDataClient("api/dataservice"))
            {
                var response = await client.SaveDataTableAsync(null, updateQueryId, null, SelectQueryId, null, dt);
                grdProducts.DataSource = response.DataSet.Tables[0];
            }
        }

        // Ambient Valued Parameter를 사용하지 않는 레거시 구현 방식
        private async Task LegacyUpdateImpl()
        {
            DataTable dt = this.GetChanages();
            if (dt == null)
            {
                return;
            }
            // 조회 시 ModifiedBy 컬럼을 포함시키지 않았지만 저장 시
            // 사용자 아이디를 포함 시켜야 하므로 컬럼을 추가 한다.
            dt.Columns.Add("ModifiedBy", typeof(string));
            // ModifiedBy 컬럼 값 설정
            foreach (DataRow row in dt.Rows)
            {
                row["ModifiedBy"] = FoxUserInfoContext.Current?.UserId;
            }
            await DoUpdate("Legacy.UpdateProduct", dt);
        }

        // Ambient Valued Parameter를 사용하는 경우 구현 방식
        // foxml 내에서 Ambient Valued  Parameter를 사용하여 사용자 아이디를 설정하므로
        // 별도로 ModifiedBy 컬럼의 값을 변경할 필요가 없다.
        private async Task UpdateUsingAmbientValuedParameter()
        {
            DataTable dt = this.GetChanages();
            if (dt == null)
            {
                return;
            }
            await DoUpdate("Ambient.UpdateProduct", dt);
        }

        // 업데이트 버튼 클릭 이벤트 핸들러
        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            //await LegacyUpdateImpl();
            await UpdateUsingAmbientValuedParameter();
        }

        #endregion

        #region Grid 관련 루틴

        // Grid의 RowIndicator 커스텀 그리기 이벤트 핸들러
        private void GrvProducts_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Info.IsRowIndicator == false || view == null)
            {
                return;
            }
            // 변경된(Modified) 행에 대해서만 커스텀 표시기를 표시한다.
            DataRow dataRow = view.GetDataRow(e.RowHandle);
            if (dataRow.RowState != DataRowState.Modified)
            {
                return;
            }
            // 디폴트 그리기 수행 
            // (Handled 속성이 false인 경우에만 기본 그리기를 수행하고 Handled 속성을 true로 변경한다)
            e.DefaultDraw();
            // 변경된 행을 표시하는 이미지를 표시한다.
            Image indicator = imgcMain.Images[0];
            Rectangle bounds = e.Bounds;
            bounds.Inflate(-1, -1);
            int x = bounds.X + (bounds.Width - indicator.Width) / 2;
            int y = bounds.Y + (bounds.Height - indicator.Height) / 2;
            e.Cache.DrawImage(indicator, new Rectangle(x, y, indicator.Width, indicator.Height));
        }

        #endregion

        #region 기타 기능

        private void ChangeUser()
        {
            var userId = (string)cbeUsers.EditValue;
            var ctx = new FoxUserInfoContext(userId);
            ctx.SetCallContext();
        }

        private void CbeUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 사용자 전환을 수행한다.
            ChangeUser();
        }

        // 테이블 데이터 전체를 보여주는 폼을 표시한다.
        private void LnkShowRawData_Click(object sender, EventArgs e)
        {
            var dialog = new RawDataForm();
            dialog.ShowDialog(this);
        }

        // 커스텀 환경 값 소스 테스트를 위한 코드
        private async void LnkWriteServerLog_Click(object sender, EventArgs e)
        {
            using (var client = FoxClientFactory.CreateDataClient("api/dataservice"))
            {
                var request = FoxDataRequest.Create("Ambient.WriteServerLog");
                request["Message"] = "Test Server Log Message #" + Environment.TickCount;
                await client.ExecuteNonQueryAsync(request);
            }
        }
        #endregion
    }
}
