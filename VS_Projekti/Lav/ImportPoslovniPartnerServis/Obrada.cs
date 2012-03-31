using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace ImportPoslovniPartnerServis
{
    public class Obrada
    {
        private static ILog loger = LogManager.GetLogger(typeof(Obrada));

        public void ImportPoslovniPartner(string putanjaFajla)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            int ukupanBrojRedova = 0;
            int brojUnetih = 0;
            int brojIzmenjenih = 0;
            int brojNepoznatih = 0;
            int brojGresaka = 0;


            DateTime pocetak = DateTime.Now;
            TimeSpan vremeTrajanja;

            if (loger.IsInfoEnabled)
            {
                loger.Info("[" + guid + "] " + "Zapoceo import fajla " + putanjaFajla);
            }

            StreamReader reader = new StreamReader(putanjaFajla);
            string red;

            try
            {
                Konfiguracija konfiguracija = new Konfiguracija();
                DB.DBProksi dBProksi = new DB.DBProksi(konfiguracija.KonekcioniString);

                bool resetujBrojac = true;
                while ((red = reader.ReadLine()) != null)
                {
                    ukupanBrojRedova++;

                    string[] _kolone = red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(10))
                    {
                        try
                        {
                             int _i = dBProksi.UnesiPoslovniPartnerRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), _kolone[3].Trim(), _kolone[4].Trim(), _kolone[5].Trim(), _kolone[6].Trim(), _kolone[7].Trim(), _kolone[8].Trim(), _kolone[9].Trim(), resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (resetujBrojac)
                            {
                                resetujBrojac = false;
                            }

                            if (_i == 1)
                            {
                                brojUnetih++;
                            }
                            else if (_i == 2)
                            {
                                brojIzmenjenih++;
                            }
                            else
                            {
                                brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            resetujBrojac = true;

                            brojGresaka++;
                            //if (loger.IsErrorEnabled)
                            //{
                            //    loger.Error("[" + guid + "] " + red + "->" + ex.Message);
                            //}
                        }
                    }
                    else
                    {
                        brojGresaka++;

                        if (loger.IsErrorEnabled)
                        {
                            loger.Error("[" + guid + "] " + red + "->" + "Red nema 10 kolona");
                        }
                    }
                }

                vremeTrajanja = (DateTime.Now - pocetak);

                if (loger.IsInfoEnabled)
                {
                    loger.Info("[" + guid + "] " + "Zavrsen import fajla " + putanjaFajla);
                    loger.Info("[" + guid + "] " + "Vreme trajanja importa :" + vremeTrajanja);
                    loger.Info("[" + guid + "] " + "Ukupan broj redova :" + ukupanBrojRedova);
                    loger.Info("[" + guid + "] " + "Broj unetih :" + brojUnetih);
                    loger.Info("[" + guid + "] " + "Broj izmenjenih :" + brojIzmenjenih);
                    loger.Info("[" + guid + "] " + "Broj nepoznatih :" + brojNepoznatih);
                    loger.Info("[" + guid + "] " + "Broj gresaka :" + brojGresaka);
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
                reader.Close();
            }
        }
    }
}
