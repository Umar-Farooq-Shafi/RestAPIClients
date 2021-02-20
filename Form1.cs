using System;
using System.Windows.Forms;

namespace RestAPIClients
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region UI Event Handler
        private void cmdGO_Click(object sender, EventArgs e)
        {
            RestClient restClient = new RestClient
            {
                EndPoint = txtRestURI.Text,
                AuthType = AuthenticationType.Basic,
                UserName = txtUserName.Text,
                Password = txtPassword.Text
                // AuthTechnique = AuthenticationTechnique.NetworkCredentials
            };

            DebugOutput("Request Client Created");

            DebugOutput(restClient.MakeRequest());
        }
        #endregion

        private void DebugOutput(string strDebugOutput)
        {
            try
            {
                System.Diagnostics.Debug.Write(strDebugOutput + Environment.NewLine);
                txtResponse.Text += strDebugOutput + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.TextLength;
                txtResponse.ScrollToCaret();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message, ToString() + Environment.NewLine);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
