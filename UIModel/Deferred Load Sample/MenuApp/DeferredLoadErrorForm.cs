using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuApp
{
    public partial class DeferredLoadErrorForm : Form
    {
        public DeferredLoadErrorForm()
        {
            InitializeComponent();
        }

        public string ErrorTitle { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorDetail { get; set; }

        private void DeferredLoadErrorForm_Load(object sender, EventArgs e)
        {
            labelControl1.Text = this.ErrorTitle;
            memoEdit1.Text = this.ErrorMessage;
            memoEdit2.Text = this.ErrorDetail;
        }
    }
}
