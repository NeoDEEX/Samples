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
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Data;
using TheOne.Windows.Forms;

namespace DataServiceClient
{
    public partial class AccessBizServiceForm : FormBase
    {
        public AccessBizServiceForm()
        {
            InitializeComponent();

            this.GetProgressDialog();
            btnQuery.Enabled = false;
        }

        private async void AccessBizServiceForm_Load(object sender, EventArgs e)
        {
            using(var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateBizClient(BizServiceUri))
                {
                    var response = await client.ExecuteAsync("DataServiceWeb.Biz.NorthwindBiz", "GetBaseData");
                    lookupSuppliers.Properties.DataSource = response.DataSet.Tables[0];
                    lookupCategories.Properties.DataSource = response.DataSet.Tables[0];
                    btnQuery.Enabled = true;
                }
            }
        }

        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            var request = FoxBizRequest.Create("DataServiceWeb.Biz.NorthwindBiz", "GetProducts");
            request["supplierId"] = lookupSuppliers.EditValue as int?;
            request["categoryId"] = lookupCategories.EditValue as int?;

            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateBizClient(BizServiceUri))
                {
                    var response = await client.ExecuteAsync(request);
                    grdProducts.DataSource = response.DataSet.Tables[0];
                }
            }
        }

        private async void BtnGenerateError_Click(object sender, EventArgs e)
        {
            var result = FoxMessageBox.Show(this, 
                "오류를 발생하는 비즈 로직을 수행하여 DB Profile에 오류 정보가 쌓이도록 테스트 합니다.\r\n",
                "알림",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result != DialogResult.OK)
            {
                return;
            }

            var request = FoxBizRequest.Create("DataServiceWeb.Biz.NorthwindBiz", "GenerateError");
            request.ThrowException = false;
            using (var scope = new FoxProgressScope(this))
            {
                using (var client = FoxClientFactory.CreateBizClient(BizServiceUri))
                {
                    // NOTE: 오류이기 때문에
                    var response = await client.ExecuteAsync(request);
                    FoxMessageBox.Show(this,
                        "비즈 서비스 수행 도중 오류가 발생하였습니다.\r\n\r\n"
                        + $"오류 메시지: { response.ErrorInfo.Message }\r\n"
                        + $"오류 타입: { response.ErrorInfo.ExceptionType}",
                        "비즈 서비스 오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
