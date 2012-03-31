using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DB
{
    public class Usluga
    {
        public void DajSveUsluge(DataTable usluga)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSveUslugeSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSveUslugeSqlCommand = new SqlCommand("SELECT Usluga_ID, [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM vwUsluga", _konekcijaSqlConnection);

                _dajSveUslugeSqlDataAdapter.SelectCommand = _dajSveUslugeSqlCommand;

                usluga.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSveUslugeSqlDataAdapter.Fill(usluga);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public Int32 UnesiUslugu(string staraSifra, string naziv, decimal cena, decimal normaSati, string napomena)
        {
            Int32 _usluga_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiUsluguSqlCommand = new SqlCommand("uspUnesiUslugu", _konekcijaSqlConnection);

                _unesiUsluguSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiUsluguSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                _unesiUsluguSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _unesiUsluguSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _unesiUsluguSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _unesiUsluguSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _unesiUsluguSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Usluga_ID", "Usluga", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiUsluguSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _usluga_ID = (Int32)_unesiUsluguSqlCommand.Parameters["@Usluga_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _usluga_ID;
            }
        }

        public void IzmeniUslugu(Int32 usluga_ID, string staraSifra, string naziv, decimal cena, decimal normaSati, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniUsluguSqlCommand = new SqlCommand("uspIzmeniUslugu", _konekcijaSqlConnection);

                _izmeniUsluguSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniUsluguSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = usluga_ID;
                _izmeniUsluguSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _izmeniUsluguSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _izmeniUsluguSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _izmeniUsluguSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _izmeniUsluguSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniUsluguSqlCommand.ExecuteNonQuery();
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

        public void ObrisiUslugu(Int32 usluga_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiUsluguSQLCommand = new SqlCommand("uspObrisiUslugu", _konekcijaSqlConnection);

                _obrisiUsluguSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiUsluguSQLCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = usluga_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiUsluguSQLCommand.ExecuteNonQuery();

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

        public int IzveziUsluge(string imeFajla)
        {
            int _brojRedova = 0;

            StreamWriter _exportStreamWriter = new StreamWriter(imeFajla);

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _dajRobuSqlCommand = new SqlCommand("SELECT [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM  vwUsluga", _konekcijaSqlConnection);

                _dajRobuSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    SqlDataReader _exportSqlDataReader = _dajRobuSqlCommand.ExecuteReader();

                    while (_exportSqlDataReader.Read())
                    {
                        _brojRedova++;

                        string _c = _exportSqlDataReader[0] + "\t" + _exportSqlDataReader[1] + "\t" + _exportSqlDataReader[2] + "\t" + _exportSqlDataReader[3] + "\t" + _exportSqlDataReader[4];
                        _exportStreamWriter.WriteLine(_c.ToCharArray());
                    }

                    _exportSqlDataReader.Close();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();

                    _exportStreamWriter.Close();
                }

            }


            return _brojRedova;
        }

        public void UveziUslugu(string staraSifra,string naziv, decimal cenaUsluge, decimal normaSati, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _uveziPoslovnogPartneraSqlCommand = new SqlCommand("uspUveziUslugu", _konekcijaSqlConnection);

                _uveziPoslovnogPartneraSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cenaUsluge;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _uveziPoslovnogPartneraSqlCommand.ExecuteNonQuery();
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

        public void NadjiUslugu(DataTable usluga, string sQLNaredba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiUsluguSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiUsluguSQLCommand = new SqlCommand();

                _nadjiUsluguSQLCommand.CommandText = sQLNaredba;

                _nadjiUsluguSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiUsluguSQLDataAdapter.SelectCommand = _nadjiUsluguSQLCommand;

                //isprazni tabelu
                usluga.Clear();

                //napuni tabelu
                try
                {
                    _nadjiUsluguSQLDataAdapter.Fill(usluga);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void NadjiUslugu(DataTable usluga, Int32 usluga_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiUsluguSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiUsluguSQLCommand = new SqlCommand();

                _nadjiUsluguSQLCommand.CommandText = " SELECT Usluga_ID, [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM vwUsluga where Usluga_ID = " + usluga_ID;

                _nadjiUsluguSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiUsluguSQLDataAdapter.SelectCommand = _nadjiUsluguSQLCommand;

                //isprazni tabelu
                usluga.Clear();

                //napuni tabelu
                try
                {
                    _nadjiUsluguSQLDataAdapter.Fill(usluga);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
