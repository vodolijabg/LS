using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Servis
{
    public class Konfiguracija
    {
        public static string KonekcioniString
        {
            get
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

                    _csSection.ConnectionStrings.Add(new ConnectionStringSettings("DB", "Data Source=localhost;Initial Catalog=Lav;Integrated Security=True"));

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("connectionStrings");

                    return "Data Source=localhost;Initial Catalog=Lav;Integrated Security=True";
                }
            }
            set
            {
                if (ConfigurationManager.ConnectionStrings["DB"] == null)
                {
                    ConfigurationManager.RefreshSection("connectionStrings");
                    ConfigurationManager.ConnectionStrings["DB"].ConnectionString = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    
                    ConnectionStringsSection _csSection =  (ConnectionStringsSection)_config.GetSection("connectionStrings");

                    // Modify the connection string.
                    _csSection.ConnectionStrings["DB"].ConnectionString = value;

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);

                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("connectionStrings");


                }
            }
        }

        public static string KorisnickoIme
        {
            get
            {
                if (ConfigurationManager.AppSettings["KorisnickoIme"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["KorisnickoIme"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("KorisnickoIme", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["KorisnickoIme"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["KorisnickoIme"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["KorisnickoIme"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string Lozinka
        {
            get
            {
                if (ConfigurationManager.AppSettings["Lozinka"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["Lozinka"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("Lozinka", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["Lozinka"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["Lozinka"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["Lozinka"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string VrstaCeneUCenovniku
        {
            get
            {
                if (ConfigurationManager.AppSettings["VrstaCeneUCenovniku"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["VrstaCeneUCenovniku"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("VrstaCeneUCenovniku", "SaPDV");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "SaPDV";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["VrstaCeneUCenovniku"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["VrstaCeneUCenovniku"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["VrstaCeneUCenovniku"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string RadniNalogStatusIDOtvoren
        {
            get
            {
                if (ConfigurationManager.AppSettings["RadniNalogStatusIDOtvoren"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["RadniNalogStatusIDOtvoren"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("RadniNalogStatusIDOtvoren", "0");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "0";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["RadniNalogStatusIDOtvoren"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["RadniNalogStatusIDOtvoren"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["RadniNalogStatusIDOtvoren"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string RadniNalogStatusIDZavrsen
        {
            get
            {
                if (ConfigurationManager.AppSettings["RadniNalogStatusIDZavrsen"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["RadniNalogStatusIDZavrsen"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("RadniNalogStatusIDZavrsen", "0");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "0";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["RadniNalogStatusIDZavrsen"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["RadniNalogStatusIDZavrsen"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["RadniNalogStatusIDZavrsen"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string ExportRadniNalogPath
        {
            get
            {
                if (ConfigurationManager.AppSettings["ExportRadniNalogPath"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["ExportRadniNalogPath"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("ExportRadniNalogPath", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["ExportRadniNalogPath"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["ExportRadniNalogPath"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["ExportRadniNalogPath"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string EMailAdresa
        {
            get
            {
                if (ConfigurationManager.AppSettings["EMailAdresa"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["EMailAdresa"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("EMailAdresa", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["EMailAdresa"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["EMailAdresa"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["EMailAdresa"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string EMailIme
        {
            get
            {
                if (ConfigurationManager.AppSettings["EMailIme"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["EMailIme"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("EMailIme", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["EMailIme"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["EMailIme"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["EMailIme"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        public static string EMailLozinka
        {
            get
            {
                if (ConfigurationManager.AppSettings["EMailLozinka"] != null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    return ConfigurationManager.AppSettings["EMailLozinka"].ToString();
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                    // Add an Application Setting.
                    _config.AppSettings.Settings.Add("EMailLozinka", "");

                    // Save the configuration file.
                    _config.Save(ConfigurationSaveMode.Modified);


                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    return "";
                }
            }
            set
            {
                if (ConfigurationManager.AppSettings["EMailLozinka"] == null)
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.AppSettings["EMailLozinka"] = value;
                }
                else
                {
                    // Open App.Config
                    System.Configuration.Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    KeyValueConfigurationElement _setting = (KeyValueConfigurationElement)_config.AppSettings.Settings["EMailLozinka"];

                    if ((_setting != null))
                    {
                        _setting.Value = value;
                        _config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }
    }
}
