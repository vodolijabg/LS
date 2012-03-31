using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ExportUslugaServis
{
    public class Konfiguracija
    {
        private string konekcioniString;
        private string exportFolder;

        public string KonekcioniString
        {
            get
            {
                return this.konekcioniString;
            }
        }
        public string ExportFolder
        {
            get
            {
                return this.exportFolder;
            }
        }


        public Konfiguracija()
        {
            konekcioniString = DajKonekcioniString();
            exportFolder = DajExportFolder();
        }

        private string DajKonekcioniString()
        {
            if (ConfigurationManager.ConnectionStrings["DB"] != null)
            {
                ConfigurationManager.RefreshSection("connectionStrings");
                return ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            }
            else
            {
                // Open App.Config
                System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                // Add the connection string.
                ConnectionStringsSection _csSection = _config.ConnectionStrings;

                _csSection.ConnectionStrings.Add(new ConnectionStringSettings("DB", "Data Source=serverp;Initial Catalog=Lav;Integrated Security=False;User ID=sa;Password=auto1!Lav"));

                // Save the configuration file.
                _config.Save(ConfigurationSaveMode.Modified);


                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("connectionStrings");

                return "Data Source=serverp;Initial Catalog=Lav;Integrated Security=False;User ID=sa;Password=auto1!Lav";
            }
        }

        private string DajExportFolder()
        {
            if (ConfigurationManager.AppSettings["ExportFolder"] != null)
            {
                ConfigurationManager.RefreshSection("appSettings");
                return ConfigurationManager.AppSettings["ExportFolder"].ToString();
            }
            else
            {
                // Open App.Config
                System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                // Add an Application Setting.
                _config.AppSettings.Settings.Add("ExportFolder", "");

                // Save the configuration file.
                _config.Save(ConfigurationSaveMode.Modified);


                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

                return "";
            }
        }

    }
}
