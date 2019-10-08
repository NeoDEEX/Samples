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

namespace DataServiceClient
{
    public partial class FormBase : XtraForm, IFoxSupportProgress
    {
        // 서비스 주소 상수
        protected const string DataServiceUri = "api/dataservice";
        protected const string BizServiceUri = "api/bizservice";

        // 프로그래스 대화상자
        private FoxProgressDialog _progressDialog;

        public FormBase()
        {
            InitializeComponent();
        }

        protected virtual IFoxProgressDialog GetProgressDialog()
        {
            if (_progressDialog == null)
            {
                FoxProgressDialog dialog = new FoxProgressDialog();
                _progressDialog = dialog;
                this.Controls.Add(dialog);
            }

            return _progressDialog;
        }

        IFoxProgressDialog IFoxSupportProgress.GetProgressDialog()
        {
            return this.GetProgressDialog();
        }
    }
}
