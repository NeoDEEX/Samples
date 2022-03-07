using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheOne.Windows.Forms;

namespace ExecuteMultipleMigration
{
    public partial class MainForm : XtraForm, IFoxSupportProgress
    {
        public MainForm()
        {
            InitializeComponent();
            AddProgressDialog();
        }

        #region IFoxSupportProgress 인터페이스 구현

        FoxProgressDialog _progressDialog;

        private void AddProgressDialog()
        {
            _progressDialog = new FoxProgressDialog();
            this.Controls.Add(_progressDialog);
        }

        IFoxProgressDialog IFoxSupportProgress.GetProgressDialog()
        {
            if (_progressDialog == null)
            {
                AddProgressDialog();
            }
            return _progressDialog;
        }

        #endregion

        private DataSet dsData1;
        private DataSet dsData2;

        private async Task ExecuteMultipleAsync()
        {
            FoxAsyncProxy proxy = new FoxAsyncProxy(this);
            await proxy.ExecuteAsync(() =>
            {
                using(var client = new MyBizClient("비즈클래스"))
                {
                    var collection = new ExecutionCollection();
                    // 기존 호출이 다음과 같은 경우를 AddExeccute 호출로 마이그레이션 합니다.
                    // this.dsData1 = oService.GetData("param1", "param2", 3);
                    // 호출 결과를 Action<DataSet> 으로 명시하였음에 주목하십시요.
                    collection.AddExecute("GetData", ds => dsData1 = ds, "param1", "param2", 3);
                    // 추가적인 호출에 대해 AddExecute를 호출합니다.
                    collection.AddExecute("GetData", ds => dsData2 = ds, 1, 2, "param3");
                    client.ExecuteParallel(collection);
                }
            });
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            await ExecuteMultipleAsync();
            gridControl1.DataSource = dsData1.Tables[0];
            gridControl2.DataSource = dsData2.Tables[0];
        }
    }
}
