using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ImportZaliheServis
{
    public class Konfiguracija
    {
        private string konekcioniString;
        private string importFolder;

        public string KonekcioniString
        {
            get
            {
                return this.konekcioniString;
            }
        }
        public string ImportFolder
        {
            get
            {
                return this.importFolder;
            }
        }


        public Konfiguracija()
        {
            konekcioniString = DajKonekcioniString();
            importFolder = DajImportFolder();
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

        private string DajImportFolder()
        {
            if (ConfigurationManager.AppSettings["ImportFolder"] != null)
            {
                ConfigurationManager.RefreshSection("appSettings");
                return ConfigurationManager.AppSettings["ImportFolder"].ToString();
            }
            else
            {
                // Open App.Config
                System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                // Add an Application Setting.
                _config.AppSettings.Settings.Add("ImportFolder", "");

                // Save the configuration file.
                _config.Save(ConfigurationSaveMode.Modified);


                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

                return "";
            }
        }

    }
}
