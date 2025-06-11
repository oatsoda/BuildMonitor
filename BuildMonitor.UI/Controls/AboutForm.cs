using BuildMonitor.UI.Helpers;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Icon = Properties.Resources._0031_Tools;
            Text = $"About {VersionHelper.AppName}";
            lblVersion.Text = VersionHelper.VersionString;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtErrors.Text = string.Join("\r\n-------------------\r\n", s_Exceptions.ToArray().Select(ex => ex.ToString()));
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private static ConcurrentQueue<Exception> s_Exceptions = new();

        public static void AddException(Exception ex)
        {
            s_Exceptions.Enqueue(ex);
            while (s_Exceptions.Count > 5)
            {
                if (!s_Exceptions.TryDequeue(out _))
                    return;
            }
        }
    }
}
