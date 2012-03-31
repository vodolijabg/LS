using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Collections.ObjectModel;
using System.IO;

namespace ExportUslugaServis
{
    public class Obrada
    {
        private static ILog loger = LogManager.GetLogger(typeof(Obrada));
        Konfiguracija konfiguracija;
        DB.DBProksi dBProksi;

        public void ExportUsluga()
        {
            StreamWriter streamWriter = null;

            string guid = Guid.NewGuid().ToString().Replace("-", "");
            int ukupanBrojRedova = 0;

            DateTime pocetak = DateTime.Now;
            TimeSpan vremeTrajanja;            

            try
            {
                konfiguracija = new Konfiguracija();
                dBProksi = new DB.DBProksi(konfiguracija.KonekcioniString);

                string imeFajla = DajImeFajlaZaExport(); 

                ObservableCollection<DB.Usluga> uslugeZaExport = dBProksi.DajUslugeZaExport(true);

                if (uslugeZaExport.Count() > 0)
                {
                    streamWriter = new StreamWriter(konfiguracija.ExportFolder + @"\" + imeFajla);

                    if (loger.IsInfoEnabled)
                    {
                        loger.Info("[" + guid + "] " + "Zapoceo export fajla " + konfiguracija.ExportFolder + @"\" + imeFajla);
                    }

                    foreach (DB.Usluga item in uslugeZaExport)
                    {
                        ukupanBrojRedova++;

                        string sifra = item.Sifra;
                        string naziv = item.VrstaUsluge.Naziv + " " + item.NosilacGrupe.Naziv + " " + item.Nivo.Naziv + " " + item.Pozicija.Naziv;
                        if (naziv.Length > 44)
                        {
                            naziv = naziv.Substring(0, 44);
                        }

                        naziv = ZameniSrpskeEngleskimKarakterima(naziv);

                        string cenaBezPDV = (item.Bod.Vrednost * item.BrojBodova).ToString("0.00", new System.Globalization.CultureInfo("en-US"));
                        string poreskaStopa = item.PoreskaStopaID.ToString();
                        string normaSati = (item.NormaMinuta / 60).ToString() + "." + (item.NormaMinuta % 60).ToString("00");

                        if (item.ZaExport)
                        {
                            dBProksi.MarkirajUsluguExportovanom(item.UslugaID);
                        }

                        streamWriter.WriteLine(sifra.PadRight(6, ' ') + "\t" + naziv.PadRight(44, ' ') + "\t" + cenaBezPDV.PadRight(14, ' ') + "\t" + poreskaStopa + "\t" + normaSati.PadRight(5, ' '));
                    }

                    vremeTrajanja = (DateTime.Now - pocetak);

                    if (loger.IsInfoEnabled)
                    {
                        loger.Info("[" + guid + "] " + "Zavrsen export fajla " + konfiguracija.ExportFolder + @"\" + imeFajla);
                        loger.Info("[" + guid + "] " + "Vreme trajanja emporta :" + vremeTrajanja);
                        loger.Info("[" + guid + "] " + "Ukupan broj redova :" + ukupanBrojRedova);
                    }

                }            

                


            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error("[" + guid + "] " + ex.Message);

                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close(); 
                }
            }
        }

        private string ZameniSrpskeEngleskimKarakterima(string tekst)
        {
            try
            {
                //ŠšŽžČčĆć
                string _tekst = tekst;
                _tekst = _tekst.Replace("Š", "S");
                _tekst = _tekst.Replace("š", "s");
                _tekst = _tekst.Replace("Ž", "Z");
                _tekst = _tekst.Replace("ž", "z");
                _tekst = _tekst.Replace("Č", "C");
                _tekst = _tekst.Replace("č", "c");
                _tekst = _tekst.Replace("Ć", "C");
                _tekst = _tekst.Replace("ć", "c");

                return _tekst;
            }
            catch (Exception)
            {
                //ako dodje do greske vrati orginalan tekst
                return tekst;
            }
        }

        private List<string> DajListuFajlova()
        {
            try
            {
                List<string> lista = new List<string>();
                foreach (String item in Directory.GetFiles(konfiguracija.ExportFolder, "US2*.txt"))
                {
                    lista.Add(item);
                }
                lista.Sort();

                return lista;
            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error(ex.Message);
                }
                throw new Exception(ex.Message);
            }
        }

        private string DajImeFajlaZaExport()
        {
            try
            {
                List<string> fajlovi = DajListuFajlova();
                List<int> lista = new List<int>();

                if (fajlovi.Count() > 0)
                {
                    foreach (String item in fajlovi)
                    {
                        //US20001.txt
                        int startIndex = item.Length - 8;
                        string rb = item.Substring(startIndex, 4);
                        //loger.Info("rb->" + rb);

                        lista.Add(Convert.ToInt32(rb));
                    }

                    //lista.Sort();

                    if (lista.Max() < 9)
                    {
                        return "US2000" + (lista.Max() + 1).ToString() + ".txt";
                    }
                    else if (lista.Max() == 9)
                    {
                        return "US20010.txt";
                    }
                    else
                    {
                        throw new Exception("Poslednji exportovan fajl je sa rednim brojem " + lista.Max().ToString());
                    } 
                }
                else
                {
                    return "US20001.txt";
                }
            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error(ex.Message);
                }
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

    }
}
