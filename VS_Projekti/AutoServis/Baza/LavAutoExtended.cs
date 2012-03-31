using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;

namespace Baza
{
    public partial class LavAutoDataContext : System.Data.Linq.DataContext
    {
        /// <summary>
        /// Vraca sledeci identity za tabelu
        /// </summary>
        /// <param name="imeTabele">Ime tabele za koju zelis identity</param>
        /// <returns>Vraca sledeci identity za tabelu</returns>
        public int DajSledeciIdentity(string imeTabele)
        {
            try
            {
                IEnumerable<decimal> _dajSledeciIdentity = this.ExecuteQuery<decimal>(@"SELECT IDENT_CURRENT ({0})", imeTabele);
                
                return Convert.ToInt32(_dajSledeciIdentity.ElementAt(0)+1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resetuje identyty na sledeci 
        /// </summary>
        /// <param name="imeTabele">Ime tabele za koju zelis da resetujes identity</param>
        public void ResetujBrojac(string imeTabele, string imeIdentityKolone)
        {
            try
            {
                IEnumerable<int> _dajBrojSlogova = this.ExecuteQuery<int>("SELECT count(*) from " + imeTabele + "");

                if (_dajBrojSlogova.ElementAt(0).Equals(0))
                {
                    this.ExecuteCommand("DBCC CHECKIDENT ({0}, RESEED, 0)", imeTabele);
                }
                else
                {
                    IEnumerable<int> _dajTrenutniIdentity = this.ExecuteQuery<int>("SELECT Max(" + imeIdentityKolone + ") from " + imeTabele + "");

                    this.ExecuteCommand("DBCC CHECKIDENT (" + imeTabele + ", RESEED, " + _dajTrenutniIdentity.ElementAt(0) + " )");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string DajKorisnickiNalog()
        {
            try
            {
                IEnumerable<string> _dajKorisnickiNalog = this.ExecuteQuery<string>(@"SELECT SYSTEM_USER AS 'KorisnickiNalog'");

                return _dajKorisnickiNalog.ElementAt(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Radnik DajRadnikaZaKorisnickiNalog()
        {
            try
            {
                string _korisnickiNalog = DajKorisnickiNalog();
                IEnumerable<Radnik> _dajRadnikaZaKorisnickiNalog = (from r in Radniks
                                                                   join vrkn in VezaRadnikKorisnickiNalogs
                                                                   on r.Radnik_ID equals vrkn.Radnik_ID
                                                                    where vrkn.KorisnickiNalog == _korisnickiNalog
                                                                   select r).Take(1);

                return _dajRadnikaZaKorisnickiNalog.First();
            }
            catch (Exception)
            {
                throw new Exception("Radnik nije registrovan.");
            }
        }

        public KorisnikPrograma DajKorisnikPrograma()
        {
            try
            {
                IEnumerable<KorisnikPrograma> _dajKorisnikPrograma = (from k in KorisnikProgramas
                                                                      select k).Take(1);

                return _dajKorisnikPrograma.First();
            }
            catch (Exception)
            {
                throw new Exception("Korisnik programa nije definisan.");
            }
        }
        
    }


}



