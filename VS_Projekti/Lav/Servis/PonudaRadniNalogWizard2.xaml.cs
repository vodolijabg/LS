using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for CarobnjakPonudaRadniNalogStrana2.xaml
    /// </summary>
    public partial class PonudaRadniNalogWizard2 : PageFunction<String>
    {
        DB.DBProksi dBProksi;
        DB.RadniNalog radniNalog;
        DB.Ponuda ponuda;
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        public PonudaRadniNalogWizard2(DB.RadniNalog radniNalog, DB.Ponuda ponuda)
        {
            InitializeComponent();

            DB.Ponuda _ponuda = new DB.Ponuda
            {
                PonudaID = ponuda.PonudaID,
                KorisnikPrograma = ponuda.KorisnikPrograma,
                KorisnikProgramaID = ponuda.KorisnikProgramaID,
                ServisnaKnjizica = ponuda.ServisnaKnjizica,
                ServisnaKnjizicaID = ponuda.ServisnaKnjizicaID,
                Radnik = ponuda.Radnik,
                RadnikID = ponuda.RadnikID,
                Vreme = ponuda.Vreme,
                NacinZahtevaZaPonudu = ponuda.NacinZahtevaZaPonudu,
                NacinZahtevaZaPonuduID = ponuda.NacinZahtevaZaPonuduID,
                PreuzimaLicno = ponuda.PreuzimaLicno,
                PreuzeoLicnoU = ponuda.PreuzeoLicnoU,
                ObavestiTelefonom = ponuda.ObavestiTelefonom,
                ObavestenTelefonomU = ponuda.ObavestenTelefonomU,
                PosaljiSMSObavestenje = ponuda.PosaljiSMSObavestenje,
                PoslatoSMSObavestenjeU = ponuda.PoslatoSMSObavestenjeU,
                PosaljiEMail = ponuda.PosaljiEMail,
                PoslatEMailU = ponuda.PoslatEMailU,
                Napomena = ponuda.Napomena,
                Status = ponuda.Status,
                VremePromene = ponuda.VremePromene,
                KorisnickiNalog = ponuda.KorisnickiNalog
            };
           

            foreach (DB.StavkaUsluga itemU in ponuda.StavkaUslugas.Where(s => s.Status != 'D').OrderBy(o => o.UslugaID))
            {
                DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                {
                    StavkaUslugaID = itemU.StavkaUslugaID,
                    PonudaID = itemU.PonudaID,
                    //RadniNalog = itemU.RadniNalog,
                    RadniNalogID = itemU.StavkaUslugaID,
                    Usluga = itemU.Usluga,
                    UslugaID = itemU.UslugaID,
                    UslugaKolicina = itemU.UslugaKolicina,
                    UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                    PoreskaStopa = itemU.PoreskaStopa,
                    UslugaPoreskaStopa_ID = itemU.UslugaPoreskaStopa_ID,
                    Status = itemU.Status,
                    VremePromene = itemU.VremePromene,
                    KorisnickiNalog = itemU.KorisnickiNalog
                };

                foreach (DB.StavkaArtikal itemA in itemU.StavkaArtikals.Where(s => s.Status != 'D').OrderBy(o => o.NosilacGrupe.Naziv))
                {
                    DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                    {
                        StavkaArtikalID = itemA.StavkaArtikalID,
                        StavkaUslugaID = itemA.StavkaUslugaID,
                        //PoslovniPartner = itemA.PoslovniPartner,
                        PoslovniPartnerID = itemA.PoslovniPartnerID,
                        //KorisnikPrograma = itemA.KorisnikPrograma,
                        KorisnikProgramaID = itemA.KorisnikProgramaID,
                        ArtikalKolicina = itemA.ArtikalKolicina,
                        ArtikalCenaBezPoreza = itemA.ArtikalCenaBezPoreza,
                        PoreskaStopa = itemA.PoreskaStopa,
                        ArtikalPoreskaStopaID = itemA.ArtikalPoreskaStopaID,
                        ArtikalNaziv = itemA.ArtikalNaziv,
                        ArtikalBrojProizvodjaca = itemA.ArtikalBrojProizvodjaca,
                        ArtikalProizvodjacID = itemA.ArtikalProizvodjacID,
                        ArtikalProizvodjacNaziv = itemA.ArtikalProizvodjacNaziv,
                        NosilacGrupe = itemA.NosilacGrupe,
                        NosilacGrupeID = itemA.NosilacGrupeID,
                        Status = itemA.Status,
                        VremePromene = itemA.VremePromene,
                        KorisnickiNalog = itemA.KorisnickiNalog
                    };

                    if (itemA.PoslovniPartner != null)
                    {
                        _stavkaArtikal.PoslovniPartner = itemA.PoslovniPartner;
                    }
                    if (itemA.KorisnikPrograma != null)
                    {
                        _stavkaArtikal.KorisnikPrograma = itemA.KorisnikPrograma;
                    }


                    _stavkaUsluga.StavkaArtikals.Add(_stavkaArtikal);
                }

                _ponuda.StavkaUslugas.Add(_stavkaUsluga);
            }            

            

            this.radniNalog = radniNalog;
            this.ponuda = _ponuda;

            gridRadniNalog.DataContext = this.ponuda;

        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                prvoOtvaranjeStrane = false;

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            }
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<String>());
        }

        private void buttonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        List<DB.StavkaUsluga> stavkaUslugaLista = new List<DB.StavkaUsluga>();
        List<DB.StavkaArtikal> stavkaArtikalLista = new List<DB.StavkaArtikal>();
       
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox _checkBox = (CheckBox)sender;
            object o = _checkBox.Tag;

            if (o.ToString() == "DB.StavkaUsluga")
            {
                DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)o;

                if ((bool)_checkBox.IsChecked)
                {
                    if (!stavkaUslugaLista.Contains(_stavkaUsluga))
                    {
                        stavkaUslugaLista.Add(_stavkaUsluga);
                    }
                }
                else
                {
                    if (stavkaUslugaLista.Contains(_stavkaUsluga))
                    {
                        stavkaUslugaLista.Remove(_stavkaUsluga);
                    }
                }

                //MessageBox.Show(_stavkaUsluga.ToString());
            }
            else if (o.ToString() == "DB.StavkaArtikal")
            {
                DB.StavkaArtikal _stavkaArtikal = (DB.StavkaArtikal)o;

                if ((bool)_checkBox.IsChecked)
                {
                    if (!stavkaArtikalLista.Contains(_stavkaArtikal))
                    {
                        stavkaArtikalLista.Add(_stavkaArtikal);
                    }
                }
                else
                {
                    if (stavkaArtikalLista.Contains(_stavkaArtikal))
                    {
                        stavkaArtikalLista.Remove(_stavkaArtikal);
                    }
                }

                //MessageBox.Show(_stavkaArtikal.ToString());
            }

        }

        private void buttonZavrsi_Click(object sender, RoutedEventArgs e)
        {
            DateTime _vremeUnosa = DateTime.Now;
            try
            {
                DB.RadniNalog _radniNalog = new DB.RadniNalog
                {
                    KorisnikProgramaID = ponuda.KorisnikProgramaID,
                    ServisnaKnjizicaID = ponuda.ServisnaKnjizicaID,
                    RadnikID = App.Radnik.RadnikID,
                    Vreme = _vremeUnosa,
                    PredvidjenoVremeMinuta = 0,
                    AutomatskiDodeliPredvidjenoVreme = false,
                    RezervisaniDelovi = false,
                    Zakljucan = false,
                    Status = 'I',
                    VremePromene = _vremeUnosa,
                    KorisnickiNalog = App.Radnik.Nadimak
                };

                if (radniNalog.Kilometraza != null)
                {
                    _radniNalog.Kilometraza = radniNalog.Kilometraza;
                }
                if (radniNalog.RegistarskiBroj != null)
                {
                    _radniNalog.RegistarskiBroj = radniNalog.RegistarskiBroj;
                }
                if (radniNalog.DatumRegistracije != null)
                {
                    _radniNalog.DatumRegistracije = radniNalog.DatumRegistracije;
                }
                if (radniNalog.Napomena != null)
                {
                    _radniNalog.Napomena = radniNalog.Napomena;
                }

                foreach (DB.StavkaUsluga itemU in stavkaUslugaLista.OrderBy(o => o.StavkaUslugaID))
                {
                    bool b = false;

                    foreach (DB.StavkaArtikal itemA in stavkaArtikalLista.OrderBy(o => o.StavkaUslugaID))
                    {
                        //dodajem samo one koje nisu u listi stavkaArtikalLista
                        if (itemU.StavkaUslugaID == itemA.StavkaUslugaID)
                        {
                            b = true;
                            break;
                        }
                    }

                    if (!b)
                    {
                        DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = new DB.RadniNalogStavkaUsluga
                        {
                            PredvidjenoVremeMinuta = itemU.Usluga.NormaMinuta,
                            RadniNalogStatusID = Convert.ToInt32(Konfiguracija.RadniNalogStatusIDOtvoren),
                            Status = 'I',
                            VremePromene = _vremeUnosa,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                        {
                            UslugaID = itemU.UslugaID,
                            UslugaKolicina = itemU.UslugaKolicina,
                            UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                            UslugaPoreskaStopa_ID = itemU.UslugaPoreskaStopa_ID,
                            Status = 'I',
                            VremePromene = _vremeUnosa,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        _stavkaUsluga.RadniNalogStavkaUsluga = _radniNalogStavkaUsluga;

                        _radniNalog.StavkaUslugas.Add(_stavkaUsluga);
                    }
                }

                foreach (DB.StavkaArtikal itemA in stavkaArtikalLista.OrderBy(o => o.StavkaUslugaID))
                {
                    DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                    {
                        ArtikalKolicina = itemA.ArtikalKolicina,
                        ArtikalCenaBezPoreza = itemA.ArtikalCenaBezPoreza,
                        ArtikalPoreskaStopaID = itemA.ArtikalPoreskaStopaID,
                        ArtikalNaziv = itemA.ArtikalNaziv,
                        ArtikalBrojProizvodjaca = itemA.ArtikalBrojProizvodjaca,
                        ArtikalProizvodjacNaziv = itemA.ArtikalProizvodjacNaziv,
                        ArtikalProizvodjacID = itemA.ArtikalProizvodjacID,
                        StavkaUslugaID = itemA.StavkaUslugaID,
                        NosilacGrupeID = itemA.NosilacGrupeID,
                        Status = 'I',
                        VremePromene = _vremeUnosa,
                        KorisnickiNalog = App.Radnik.Nadimak
                    };

                    if (itemA.PoslovniPartnerID != null)
                    {
                        _stavkaArtikal.PoslovniPartnerID = itemA.PoslovniPartnerID;
                    }
                    if (itemA.KorisnikProgramaID != null)
                    {
                        _stavkaArtikal.KorisnikProgramaID = itemA.KorisnikProgramaID;
                    }

                    bool d = false;

                    foreach (DB.StavkaUsluga itemU in _radniNalog.StavkaUslugas)
                    {
                        //dodajem samo one koje nisu vec dodate
                        if (itemU.UslugaID == itemA.StavkaUsluga.UslugaID)
                        {
                            d = true;
                            break;
                        }
                    }

                    if (!d)
                    {
                        DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = new DB.RadniNalogStavkaUsluga
                        {
                            PredvidjenoVremeMinuta = itemA.StavkaUsluga.Usluga.NormaMinuta,
                            RadniNalogStatusID = Convert.ToInt32(Konfiguracija.RadniNalogStatusIDOtvoren),
                            Status = 'I',
                            VremePromene = _vremeUnosa,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                        {
                            UslugaID = itemA.StavkaUsluga.UslugaID,
                            UslugaKolicina = itemA.StavkaUsluga.UslugaKolicina,
                            UslugaCenaBezPoreza = itemA.StavkaUsluga.UslugaCenaBezPoreza,
                            UslugaPoreskaStopa_ID = itemA.StavkaUsluga.UslugaPoreskaStopa_ID,
                            Status = 'I',
                            VremePromene = _vremeUnosa,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        _stavkaUsluga.RadniNalogStavkaUsluga = _radniNalogStavkaUsluga;

                        _radniNalog.StavkaUslugas.Add(_stavkaUsluga);
                    }

                    foreach (DB.StavkaUsluga item in _radniNalog.StavkaUslugas)
                    {
                        if (item.UslugaID == itemA.StavkaUsluga.UslugaID)
                        {
                            item.StavkaArtikals.Add(_stavkaArtikal);
                            break;
                        }
                    }
                }



                dBProksi.UnesiRadniNalog(_radniNalog);

                Window.GetWindow(this).Close();

                MessageBox.Show("Radni nalog sa ID = " + _radniNalog.RadniNalogID.ToString() + " je uspešno kreiran." , "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
