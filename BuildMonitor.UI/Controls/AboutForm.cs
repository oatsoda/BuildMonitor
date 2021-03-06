﻿using System.Reflection;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            lblVersion.Text = Assembly.GetAssembly(GetType()).GetName().Version.ToString();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
