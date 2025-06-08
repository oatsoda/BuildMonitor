using BuildMonitor.UI.Helpers;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Icon = Properties.Resources._0031_Tools;
            lblVersion.Text = VersionHelper.VersionString;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
