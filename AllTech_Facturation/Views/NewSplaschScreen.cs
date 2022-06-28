using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllTech_Facturation.Views
{
    public partial class NewSplaschScreen : Form
    {
        public NewSplaschScreen()
        {
            InitializeComponent();
        }

        private void NewSplaschScreen_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Opacity = 1;
        }
    }
}
