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

namespace WcfClientApp
{
    public partial class ClientForm : XtraForm
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (var svc = new ServiceReference1.OldWcfServiceClient())
            {
                var ds = svc.GetAllProducts();
                grdProducts.DataSource = ds.Tables[0];
            }
        }
    }
}
