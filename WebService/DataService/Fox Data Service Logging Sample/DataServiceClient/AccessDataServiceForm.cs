using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Data;
using TheOne.Windows.Forms;

namespace DataServiceClient
{
    public partial class AccessDataServiceForm : FormBase
    {
        public AccessDataServiceForm()
        {
            InitializeComponent();

            this.GetProgressDialog();
            btnQuery.Enabled = false;
        }

        private async void AccessDataServiceForm_Load(object sender, EventArgs e)
        {
            using(var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    var requests = new FoxDataRequest[]
                    {
                        FoxDataRequest.Create("Northwind.GetSuppliers"),
                        FoxDataRequest.Create("Northwind.GetCategories"),
                    };
                    requests[0].Operation = FoxDataOperations.ExecuteDataSet;
                    requests[1].Operation = FoxDataOperations.ExecuteDataSet;

                    var tasks = client.ExecuteParallelAsync(requests);
                    var responses = await Task.WhenAll(tasks);
                    lookupSuppliers.Properties.DataSource = responses[0].DataSet.Tables[0];
                    lookupCategories.Properties.DataSource = responses[1].DataSet.Tables[0];
                    btnQuery.Enabled = true;
                }
            }
        }

        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            var request = FoxDataRequest.Create("Northwind.GetProducts");
            request["SupplierID"] = lookupSuppliers.EditValue as int?;
            request["CategoryID"] = lookupCategories.EditValue as int?;
            request.Parameters.SetNullToDBNull();

            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    var response = await client.ExecuteDataSetAsync(request);
                    grdProducts.DataSource = response.DataSet.Tables[0];
                }
            }
        }

        private async void BtnGenerateError_Click(object sender, EventArgs e)
        {
            var result = FoxMessageBox.Show(this, 
                "오류를 발생하는 쿼리를 수행하여 DB Profile에 오류 정보가 쌓이도록 테스트 합니다.\r\n",
                "알림",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result != DialogResult.OK)
            {
                return;
            }

            var request = FoxDataRequest.Create("Northwind.ErrorQuery");
            request.ThrowException = false;
            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateDataClient(DataServiceUri))
                {
                    // NOTE: 오류이기 때문에
                    var response = await client.ExecuteDataSetAsync(request);
                    FoxMessageBox.Show(this,
                        "데이터 서비스 수행 도중 오류가 발생하였습니다.\r\n\r\n"
                        + $"오류 메시지: { response.ErrorInfo.Message }\r\n"
                        + $"오류 타입: { response.ErrorInfo.ExceptionType}",
                        "데이터 서비스 오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
