using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ImportPoslovniPartnerServis
{
    public partial class ImportPoslovniPartnerServis : ServiceBase
    {
        Server server = new Server();

        public ImportPoslovniPartnerServis()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            server.Start();
        }

        protected override void OnStop()
        {
            server.Stop();
        }
    }
}
