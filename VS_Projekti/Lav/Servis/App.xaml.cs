using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static CultureInfo cultureInfo = null;

        public enum Stanje { Unos, Izmena, Brisanje, Detaljno, Osnovno, IzgasiSve }

        private static int servisnaKnjizicaPartnerID = -1;
        private static string servisnaKnjizicaVrstaPartnera = "";
        public static int ServisnaKnjizicaPartnerID
        {
            get { return servisnaKnjizicaPartnerID; }
            set { servisnaKnjizicaPartnerID = value; }
        }
        public static string ServisnaKnjizicaVrstaPartnera
        {
            get { return servisnaKnjizicaVrstaPartnera; }
            set { servisnaKnjizicaVrstaPartnera = value; }
        }

        private static DB.Radnik  radnik = null;
        public static DB.Radnik Radnik
        {
            get { return radnik ; }
            set { radnik = value; }
        }

        private static int ponudaPartnerID = -1;
        private static string ponudaVrstaPartnera = "";
        public static int PonudaPartnerID
        {
            get { return ponudaPartnerID; }
            set { ponudaPartnerID = value; }
        }
        public static string PonudaVrstaPartnera
        {
            get { return ponudaVrstaPartnera; }
            set { ponudaVrstaPartnera = value; }
        }

        private static int ponudaServisnaKnjizicaID = -1;
        public static int PonudaServisnaKnjizicaID
        {
            get { return ponudaServisnaKnjizicaID; }
            set { ponudaServisnaKnjizicaID = value; }
        }


        private static int radniNalogPartnerID = -1;
        private static string radniNalogVrstaPartnera = "";
        public static int RadniNalogPartnerID
        {
            get { return radniNalogPartnerID; }
            set { radniNalogPartnerID = value; }
        }
        public static string RadniNalogVrstaPartnera
        {
            get { return radniNalogVrstaPartnera; }
            set { radniNalogVrstaPartnera = value; }
        }

        private static int radniNalogServisnaKnjizicaID = -1;
        public static int RadniNalogServisnaKnjizicaID
        {
            get { return radniNalogServisnaKnjizicaID; }
            set { radniNalogServisnaKnjizicaID = value; }
        }

        public static DB.FizickoLice fizickoLicePonudaWizard = null;
        public static DB.NacinZahtevaZaPonudu nacinZahtevaZaPonuduWizard = null;
    }
}
