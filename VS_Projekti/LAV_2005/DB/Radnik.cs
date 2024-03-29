using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DB
{
    public class Radnik
    {
        public void DajSveRadnike(DataTable radnik)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSveRadnikeSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSveRadnikeSqlCommand = new SqlCommand("SELECT Radnik_ID, [Stara Šifra], Ime, Prezime  FROM vwRadnik", _konekcijaSqlConnection);

                _dajSveRadnikeSqlDataAdapter.SelectCommand = _dajSveRadnikeSqlCommand;

                radnik.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSveRadnikeSqlDataAdapter.Fill(radnik);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public Int16 UnesiRadnika(string staraSifra, string ime, string prezime)
        {
            Int16 _radnik_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiRadnikaSqlCommand = new SqlCommand("uspUnesiRadnika", _konekcijaSqlConnection);

                _unesiRadnikaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiRadnikaSqlCommand.Parameters.Add("@Radnik_ID", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                _unesiRadnikaSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.Char, 10).Value = staraSifra;
                _unesiRadnikaSqlCommand.Parameters.Add("@Ime", SqlDbType.NVarChar, 20).Value = ime;
                _unesiRadnikaSqlCommand.Parameters.Add("@Prezime", SqlDbType.NVarChar, 20).Value = prezime;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Radnik_ID", "Radnik", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiRadnikaSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _radnik_ID = (Int16)_unesiRadnikaSqlCommand.Parameters["@Radnik_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _radnik_ID;
            }
        }

        public void IzmeniRadnika(Int16 radnik_ID, string staraSifra, string ime, string prezime)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniRadnikaSqlCommand = new SqlCommand("uspIzmeniRadnika", _konekcijaSqlConnection);

                _izmeniRadnikaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniRadnikaSqlCommand.Parameters.Add("@Radnik_ID", SqlDbType.SmallInt).Value = radnik_ID;
                _izmeniRadnikaSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.Char, 10).Value = staraSifra;
                _izmeniRadnikaSqlCommand.Parameters.Add("@Ime", SqlDbType.NVarChar, 20).Value = ime;
                _izmeniRadnikaSqlCommand.Parameters.Add("@Prezime", SqlDbType.NVarChar, 20).Value = prezime;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniRadnikaSqlCommand.ExecuteNonQuery();
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

        public void ObrisiRadnika(Int16 radnik_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiRadnikaSQLCommand = new SqlCommand("uspObrisiRadnika", _konekcijaSqlConnection);

                _obrisiRadnikaSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiRadnikaSQLCommand.Parameters.Add("@Radnik_ID", SqlDbType.SmallInt).Value = radnik_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiRadnikaSQLCommand.ExecuteNonQuery();

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

        public int ExportRadnika(string imeFajla)
        {
            int _brojRedova = 0;

            StreamWriter _exportStreamWriter = new StreamWriter(imeFajla);

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _dajPoslovnePartnereSqlCommand = new SqlCommand("SELECT [Stara šifra], Ime, Prezime FROM vwRadnik", _konekcijaSqlConnection);

                _dajPoslovnePartnereSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    SqlDataReader _exportSqlDataReader = _dajPoslovnePartnereSqlCommand.ExecuteReader();

                    while (_exportSqlDataReader.Read())
                    {
                        _brojRedova++;

                        string _c = _exportSqlDataReader[0] + "\t" + _exportSqlDataReader[1] + "\t" + _exportSqlDataReader[2];
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

        public void ImportRadnika(string staraSifra, string ime, string prezime)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _uveziRadnikaSqlCommand = new SqlCommand("uspUveziRadnika", _konekcijaSqlConnection);

                _uveziRadnikaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _uveziRadnikaSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.Char, 10).Value = staraSifra;
                _uveziRadnikaSqlCommand.Parameters.Add("@Ime", SqlDbType.NVarChar, 20).Value = ime;
                _uveziRadnikaSqlCommand.Parameters.Add("@Prezime", SqlDbType.NVarChar, 20).Value = prezime;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _uveziRadnikaSqlCommand.ExecuteNonQuery();
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

        public void DajPadajucuListuRadnik(DataTable radnik)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuRadnikSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuRadnikSqlCommand = new SqlCommand("SELECT Radnik_ID, Ime + N' ' + Prezime AS ImePrezime FROM vwRadnik", _konekcijaSqlConnection);

                _dajPadajucuListuRadnikSqlDataAdapter.SelectCommand = _dajPadajucuListuRadnikSqlCommand;

                radnik.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajPadajucuListuRadnikSqlDataAdapter.Fill(radnik);

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
