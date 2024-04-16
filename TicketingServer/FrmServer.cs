using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketingCommon.Communication;

namespace TicketingServer
{
    public partial class FrmServer : Form
    {
        Server server;
        public FrmServer()
        {
            InitializeComponent();
            server = new Server();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(server.Start));
            btnStopServer.Enabled = true;
            btnStartServer.Enabled = false;
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            server.Stop();
            foreach (ClientHandler item in ClientHandler.CurrentClients)
                item.Stop();
            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
            ClientHandler.CurrentClients.Clear();
        }
    }
}
