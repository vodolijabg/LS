using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DB
{
    public partial class LavDataClassesDataContext : System.Data.Linq.DataContext
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

                return Convert.ToInt32(_dajSledeciIdentity.ElementAt(0) + 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resetuje identity na sledeci 
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

        public ObservableCollection<StampaBrojIzdatihPonuda> DajBrojIzdatihPonudaPoRadnicima(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                DateTime _datumOd = new DateTime(datumOd.Year, datumOd.Month, datumOd.Day, 0, 0, 0);
                DateTime _datumDo = new DateTime(datumDo.Year, datumDo.Month, datumDo.Day, 23, 59, 59);
                string _s = "with A as " +
                            "( " +
                            "SELECT r.RadnikID, COUNT(*) AS BrojPonuda " +
                            "FROM  Radnik r  JOIN " +
                            "               Ponuda p ON r.RadnikID = p.RadnikID " +
                            "where not (p.Status = 'D') and  (p.Vreme between '" + _datumOd.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + _datumDo.ToString("yyyy-MM-dd HH:mm:ss") + "' )" +
                            "GROUP BY r.RadnikID " +
                            ") " +
                            "select r.RadnikID, r.Sifra, r.Nadimak, isnull(a.BrojPonuda, 0) as BrojPonuda " +
                            "from Radnik r " +
                            "left outer join a " +
                            "on r.RadnikID = a.RadnikID " + 
                            "order by r.Sifra, r.Nadimak ";

                return new ObservableCollection<StampaBrojIzdatihPonuda>(this.ExecuteQuery<StampaBrojIzdatihPonuda>(_s).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Porudzbenica> DajPorudzbenicu()
        {
            try
            {
                string _s = "with A as " +
                            "( " +
                            "SELECT " +
                            "a.Artikal_ID, a.BrojProizvodjaca, p.Naziv AS Proizvodjac, sa.ArtikalNaziv AS Naziv, su.RadniNalogID, " +
                            "sa.ArtikalKolicina AS Kolicina, sa.ArtikalCenaBezPoreza AS PonudjenaCenaBezPDV " +
                            "FROM  Artikal AS a " +
                            "INNER JOIN StavkaArtikal AS sa ON a.BrojProizvodjaca = sa.ArtikalBrojProizvodjaca AND a.Proizvodjac_ID = sa.ArtikalProizvodjacID " +
                            "INNER JOIN StavkaUsluga AS su ON sa.StavkaUslugaID = su.StavkaUslugaID " +
                            "INNER JOIN Proizvodjac AS p ON a.Proizvodjac_ID = p.Proizvodjac_ID " +
                            "WHERE (a.Sifra IS NULL) " +
                            "GROUP BY a.Artikal_ID, a.BrojProizvodjaca, p.Naziv, sa.ArtikalNaziv, su.RadniNalogID, sa.ArtikalKolicina, sa.ArtikalCenaBezPoreza " +
                            "HAVING (NOT (su.RadniNalogID IS NULL)) " +
                            ") " +
                            "select a.*, pp.SkracenNaziv as Dobavljac, vad.Cena as DobavljacCena, vad.DatumAzuriranja " +
                            "from a " +
                            "INNER JOIN VezaArtikalDobavljac as vad on a.Artikal_ID = vad.ArtikalID " +
                            "INNER JOIN PoslovniPartner as pp on vad.PoslovniPartnerID = pp.PoslovniPartnerID " +
                            "order by a.Artikal_ID, a.RadniNalogID, vad.Cena ";

                return new ObservableCollection<Porudzbenica>(this.ExecuteQuery<Porudzbenica>(_s).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
