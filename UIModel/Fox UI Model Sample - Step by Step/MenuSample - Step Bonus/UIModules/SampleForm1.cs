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

namespace UIModules
{
    public partial class SampleForm1 : FoxForm
    {
        public SampleForm1()
        {
            InitializeComponent();
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            var args = "argument string #1";
            this.ViewModel.Navigate("1020", args);
        }

        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            var args = "argument string #2";
            this.ViewModel.MoveTo("1020", args);
        }

        private void btnReplaceTo_Click(object sender, EventArgs e)
        {
            var args = "argument string #3";
            this.ViewModel.ReplaceTo("1020", args);
        }
    }
}
