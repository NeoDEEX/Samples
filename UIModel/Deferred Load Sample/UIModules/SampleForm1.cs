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

        private void SampleForm1_Load(object sender, EventArgs e)
        {
            Task.Delay(1000).Wait();
        }
    }
}
