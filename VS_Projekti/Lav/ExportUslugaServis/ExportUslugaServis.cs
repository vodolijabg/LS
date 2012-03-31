using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ExportUslugaServis
{
    public partial class ExportUslugaServis : ServiceBase
    {
        Server server = new Server();

        public ExportUslugaServis()
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
