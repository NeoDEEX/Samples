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
    public partial class SampleForm2 : FoxForm
    {
        public SampleForm2()
        {
            InitializeComponent();
        }

        protected override void OnViewInitialize(object args)
        {
            label2.Text = $"Argument: {args}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ViewModel.CanGoBack == true)
            {
                this.ViewModel.GoBack();
            }
        }
    }
}
