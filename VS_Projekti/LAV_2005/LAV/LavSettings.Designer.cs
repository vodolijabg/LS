﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4016
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAV {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class LavSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LavSettings defaultInstance = ((LavSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LavSettings())));
        
        public static LavSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("serverp\\sqlexpress")]
        public string _Server {
            get {
                return ((string)(this["_Server"]));
            }
            set {
                this["_Server"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LAV")]
        public string _Baza {
            get {
                return ((string)(this["_Baza"]));
            }
            set {
                this["_Baza"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SQL")]
        public string _Autentifikacija {
            get {
                return ((string)(this["_Autentifikacija"]));
            }
            set {
                this["_Autentifikacija"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("sa")]
        public string _Korisnik {
            get {
                return ((string)(this["_Korisnik"]));
            }
            set {
                this["_Korisnik"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("auto1!Lav")]
        public string _Lozinka {
            get {
                return ((string)(this["_Lozinka"]));
            }
            set {
                this["_Lozinka"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>1</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection _UspesnePretrage {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["_UspesnePretrage"]));
            }
            set {
                this["_UspesnePretrage"] = value;
            }
        }
    }
}
