using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace WebMapClientInteractionSystemTray
{
    public partial class Form1 : Form
    {

        public class CustomHeaderHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                return base.SendAsync(request, cancellationToken)
                    .ContinueWith((task) =>
                    {
                        HttpResponseMessage response = task.Result;
                        response.Headers.Add("Access-Control-Allow-Origin", "*");
                        return response;
                    });
            }
        }



        public Form1()
        {
            InitializeComponent();
            this.Text = System.Configuration.ConfigurationManager.AppSettings["ManagerTitle"];
            this.label1.Text = System.Configuration.ConfigurationManager.AppSettings["ManagerContent"];
            this.Resize += Form1_Resize;

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                mynotifyicon.Visible = true;
                mynotifyicon.BalloonTipText = "WebMap Client Interation minimized";
                mynotifyicon.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                //mynotifyicon.Visible = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
           
        }
    }
}
