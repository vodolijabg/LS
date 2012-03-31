using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;
using System.Globalization;

namespace ImportZaliheServis
{
    class Obrada
    {
        private static ILog loger = LogManager.GetLogger(typeof(Obrada));

        public void ImportZalihe(string putanjaFajla)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            int ukupanBrojRedova = 0;
            int brojUnetih = 0;
            int brojGresaka = 0;
            int brojNeNadjenih = 0;

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
                NumberFormatInfo decimalFormatProvider = new NumberFormatInfo();
                decimalFormatProvider.NumberDecimalSeparator = ".";

                bool resetujBrojac = true;
                while ((red = reader.ReadLine()) != null)
                {
                    ukupanBrojRedova++;

                    string[] _kolone = red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(4))
                    {
                        try
                        {

                            int _i = dBProksi.UnesiZalihe(_kolone[1].Trim(), Convert.ToDecimal(_kolone[2].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[3].Trim(), decimalFormatProvider), resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (resetujBrojac)
                            {
                                resetujBrojac = false;
                            }

                            if (_i == -1)
                            {
                                //if (loger.IsWarnEnabled)
                                //{
                                //    loger.Info("[" + guid + "] " + "Artikal nije nadjen: " + red);
                                //}
                                brojNeNadjenih++;
                            }
                            else
                            {
                                brojUnetih++;
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
                            loger.Error("[" + guid + "] " + red + "->" + "Red nema 4 kolone");
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
                    loger.Info("[" + guid + "] " + "Broj ne nadjenih :" + brojNeNadjenih);
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
