﻿using System;
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

        private void SampleForm2_Load(object sender, EventArgs e)
        {
            Task.Delay(1000).Wait();
        }
    }
}
