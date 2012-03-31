using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class Konekcija
    {
        private static string sQLBaza;
        private static string sQLServer;
        private static string autentifikacija;
        private static string korisnickoIme;
        private static string lozinka;

        #region SQLServer
        public static string SQLServer
        {
            get
            {
                return sQLServer;
            }
            set
            {
                sQLServer = value;
            }
        }
        #endregion

        #region SQLBaza
        public static string SQLBaza
        {
            get
            {
                return sQLBaza;
            }
            set
            {
                sQLBaza = value;
            }
        }
        #endregion

        #region Autentifikacija
        public static string Autentifikacija
        {
            get
            {
                return autentifikacija;
            }
            set
            {
                autentifikacija = value;
            }
        }
        #endregion

        #region KorisnickoIme
        public static string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }
            set
            {
                korisnickoIme = value;
            }
        }
        #endregion

        #region Lozinka
        public static string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
            }
        }
        #endregion

        public static SqlConnection DajKonekciju()
        {

            //string _konekcioniString = konekcioniString;
            //               //@"Data Source=localhost;" +
            //               //"Initial Catalog =LAV;" +
            //               //"Integrated Security = True;" +
            //               //"Persist Security Info = False";

            ////string _konekcioniString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\BAZA\LAV_TD.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

            ////Properties.Settings setings = new DB.Properties.Settings();

            ////string _konekcioniString =
            ////            @"Data Source= server1\sql1;" +
            ////            "Initial Catalog = KATALOG_17;" +
            ////            "User ID= oliver; " +
            ////            "Password= oliver; " +
            ////            "Persist Security Info = False";

            //SqlConnection _konekcija = new SqlConnection(_konekcioniString);
            ////SqlConnection _konekcija = new SqlConnection(setings.Konekcija);
            //return _konekcija;

            if (Autentifikacija == "Windows")
            {
                string _konekcioniString =
                            "Data Source=" + sQLServer + ";" +
                            "Initial Catalog =" + sQLBaza + ";" +
                            "Integrated Security = True;" +
                            "Persist Security Info = False";

                SqlConnection _konekcija =
                new SqlConnection(_konekcioniString);
                return _konekcija;
            }
            else
            {
                string _konekcioniString =
                            "Data Source=" + sQLServer + ";" +
                            "Initial Catalog =" + sQLBaza + ";" +
                            "Integrated Security = False;" +
                            "User ID=" + korisnickoIme + ";" +
                            "Password=" + lozinka + ";" +
                            "Persist Security Info = False";

                SqlConnection _konekcija =
                new SqlConnection(_konekcioniString);
                return _konekcija;
            }
        }
    }
}
