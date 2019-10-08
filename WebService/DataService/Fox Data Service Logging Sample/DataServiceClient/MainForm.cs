using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataServiceClient
{
    public partial class MainForm : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowChild<FormType>(int imageIndex) where FormType : Form, new()
        {
            foreach(var form in this.MdiChildren)
            {
                if (form is FormType)
                {
                    this.ActivateMdiChild(form);
                    return;
                }
            }

            var childForm = new FormType();
            childForm.MdiParent = this;
            childForm.Show();

            var doc = docmgrMain.GetDocument(childForm);
            doc.ImageOptions.ImageIndex = imageIndex;
        }

        private void BtnAccessDataService_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowChild<AccessDataServiceForm>(0);
        }

        private void BtnAccessBizService_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowChild<AccessBizServiceForm>(1);
        }

        private void BtnViewPerformanceLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowChild<PerformanceLogViewerForm>(2);
        }

        private void BtnViewDbProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowChild<DbProfileViewForm>(3);
        }
    }
}
