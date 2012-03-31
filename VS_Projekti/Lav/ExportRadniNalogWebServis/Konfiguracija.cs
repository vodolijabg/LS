using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace ExportRadniNalogWebServis
{
    public class Konfiguracija
    {
        //Configuration webConfig = null;
        private string exportFolder;

        public string ExportFolder
        {
            get
            {
                return this.exportFolder;
            }
        }


        public Konfiguracija()
        {
            exportFolder = DajExportFolder();
        }

 
        private string DajExportFolder()
        {
            try
            {
                return ConfigurationManager.AppSettings["ExportFolder"];
            }
            catch (Exception)
            {
                return @"C:";
            }
        }

    }
}