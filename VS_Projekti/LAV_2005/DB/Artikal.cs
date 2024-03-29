using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class Artikal
    {
        public void NadjiArtikal(DataTable artikal, string sQLNaredba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiArtikalSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiArtikalSQLCommand = new SqlCommand();

                _nadjiArtikalSQLCommand.CommandText = sQLNaredba;

                _nadjiArtikalSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiArtikalSQLDataAdapter.SelectCommand = _nadjiArtikalSQLCommand;

                //isprazni tabelu
                artikal.Clear();

                //napuni tabelu
                try
                {
                    _nadjiArtikalSQLDataAdapter.Fill(artikal);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void NadjiArtikal(DataTable artikal, Int32 artikal_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiArtikalSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiArtikalSQLCommand = new SqlCommand();

                _nadjiArtikalSQLCommand.CommandText = " SELECT vwArtikal.Artikal_ID, vwArtikal.Proizvođač, vwArtikal.[Broj proizvoda], vwArtikal.[Naziv proizvoda], vwArtikal.[Cena ugradnje], vwArtikal.[Norma sati], vwArtikal.Cena, vwArtikal.[Najpovoljniji dobavljač], vwArtikal.[Korisnik programa], vwArtikal.Napomena from vwArtikal where Artikal_ID = " + artikal_ID;

                _nadjiArtikalSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiArtikalSQLDataAdapter.SelectCommand = _nadjiArtikalSQLCommand;

                //isprazni tabelu
                artikal.Clear();

                //napuni tabelu
                try
                {
                    _nadjiArtikalSQLDataAdapter.Fill(artikal);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ObradiNapomenu(Int32 artikal_ID, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obradiNapomenuSqlCommand = new SqlCommand("dbo.uspNapomena", _konekcijaSqlConnection);
                _obradiNapomenuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obradiNapomenuSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;
                _obradiNapomenuSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obradiNapomenuSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public void DajDobavljaceZaArtikal(DataTable vezaArtikalDobavljac, Int32 artikal_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajDobavljaceZaArtikalSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajDobavljaceZaArtikalSQLCommand = new SqlCommand();

                _dajDobavljaceZaArtikalSQLCommand.CommandText = " SELECT Artikal_ID, PoslovniPartner_ID, Dobavljač, Cena, [Datum ažuriranja], Telefon, [Kontakt osoba], Napomena FROM vwVezaArtikalDobavljac WHERE  (Artikal_ID = " + artikal_ID + ") order by  Cena, Dobavljač ";

                _dajDobavljaceZaArtikalSQLCommand.Connection = _konekcijaSqlConnection;

                _dajDobavljaceZaArtikalSQLDataAdapter.SelectCommand = _dajDobavljaceZaArtikalSQLCommand;

                //isprazni tabelu
                vezaArtikalDobavljac.Clear();

                //napuni tabelu
                try
                {
                    _dajDobavljaceZaArtikalSQLDataAdapter.Fill(vezaArtikalDobavljac);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void PovecajBrojac(Int32 artikal_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _povecajBrojacSqlCommand = new SqlCommand("dbo.uspPovecajBrojac", _konekcijaSqlConnection);
                _povecajBrojacSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _povecajBrojacSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _povecajBrojacSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public void DajBrojac(DataTable brojac)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajBrojacSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajBrojacSQLCommand = new SqlCommand("uspDajBrojac");
                _dajBrojacSQLCommand.CommandType = CommandType.StoredProcedure;

                _dajBrojacSQLCommand.Connection = _konekcijaSqlConnection;

                _dajBrojacSQLDataAdapter.SelectCommand = _dajBrojacSQLCommand;

                //isprazni tabelu
                brojac.Clear();

                //napuni tabelu
                try
                {
                    _dajBrojacSQLDataAdapter.Fill(brojac);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ObradiDodatnePodatke(Int32 artikal_ID, decimal cenaUgradnje, decimal normaSati, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obradiDodatnePodatkeSqlCommand = new SqlCommand("dbo.uspIzmeniDodatnePodatke", _konekcijaSqlConnection);
                _obradiDodatnePodatkeSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;

                if (cenaUgradnje >= 0)
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                }
                if (normaSati >= 0)
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                }
                if (napomena != "")
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;
                }

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obradiDodatnePodatkeSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public void ObradiDodatnePodatke(Int32 artikal_ID, decimal cenaUgradnje, decimal normaSati)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obradiDodatnePodatkeSqlCommand = new SqlCommand("dbo.uspIzmeniDodatnePodatke", _konekcijaSqlConnection);
                _obradiDodatnePodatkeSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;

                if (cenaUgradnje >= 0)
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                }
                if (normaSati >= 0)
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                }
                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obradiDodatnePodatkeSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public void ObradiDodatnePodatke(Int32 artikal_ID,string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obradiDodatnePodatkeSqlCommand = new SqlCommand("dbo.uspIzmeniDodatnePodatke", _konekcijaSqlConnection);
                _obradiDodatnePodatkeSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;
               
                if (napomena != "")
                {
                    _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;
                }

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obradiDodatnePodatkeSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public void ObradiDodatnePodatke(Int32 artikal_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obradiDodatnePodatkeSqlCommand = new SqlCommand("dbo.uspIzmeniDodatnePodatke", _konekcijaSqlConnection);
                _obradiDodatnePodatkeSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obradiDodatnePodatkeSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obradiDodatnePodatkeSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }


    }
}
