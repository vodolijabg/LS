using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DB
{
    public class Roba
    {
        public void DajSvuRobu(DataTable roba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSvuRobuSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSvuRobuSqlCommand = new SqlCommand("SELECT  Roba_ID, [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba", _konekcijaSqlConnection);

                _dajSvuRobuSqlDataAdapter.SelectCommand = _dajSvuRobuSqlCommand;

                roba.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSvuRobuSqlDataAdapter.Fill(roba);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public Int32 UnesiRobu(string staraSifra, string internaSifra, string naziv, decimal cena, decimal cenaUgradnje, decimal normaSati, string napomena)
        {
            Int32 _roba_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiRobuSqlCommand = new SqlCommand("uspUnesiRobu", _konekcijaSqlConnection);

                _unesiRobuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiRobuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                _unesiRobuSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _unesiRobuSqlCommand.Parameters.Add("@InternaSifra", SqlDbType.VarChar, 50).Value = internaSifra;
                _unesiRobuSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _unesiRobuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _unesiRobuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                _unesiRobuSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _unesiRobuSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Roba_ID", "Roba", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiRobuSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _roba_ID = (Int32)_unesiRobuSqlCommand.Parameters["@Roba_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _roba_ID;
            }
        }

        public void IzmeniRobu(Int32 roba_ID, string staraSifra, string internaSifra, string naziv, decimal cena, decimal cenaUgradnje, decimal normaSati, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniRobuSqlCommand = new SqlCommand("uspIzmeniRobu", _konekcijaSqlConnection);

                _izmeniRobuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniRobuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = roba_ID;
                _izmeniRobuSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _izmeniRobuSqlCommand.Parameters.Add("@InternaSifra", SqlDbType.VarChar, 50).Value = internaSifra;
                _izmeniRobuSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _izmeniRobuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _izmeniRobuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                _izmeniRobuSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _izmeniRobuSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniRobuSqlCommand.ExecuteNonQuery();
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

        public void ObrisiRobu(Int32 roba_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiRobuSQLCommand = new SqlCommand("uspObrisiRobu", _konekcijaSqlConnection);

                _obrisiRobuSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiRobuSQLCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = roba_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiRobuSQLCommand.ExecuteNonQuery();

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

        public int IzveziRobu(string imeFajla)
        {
            int _brojRedova = 0;

            StreamWriter _exportStreamWriter = new StreamWriter(imeFajla);

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _dajRobuSqlCommand = new SqlCommand("SELECT [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba", _konekcijaSqlConnection);

                _dajRobuSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    SqlDataReader _exportSqlDataReader = _dajRobuSqlCommand.ExecuteReader();

                    while (_exportSqlDataReader.Read())
                    {
                        _brojRedova++;

                        string _c = _exportSqlDataReader[0] + "\t" + _exportSqlDataReader[1] + "\t" + _exportSqlDataReader[2] + "\t" + _exportSqlDataReader[3] + "\t" + _exportSqlDataReader[4] + "\t" + _exportSqlDataReader[5] + "\t" + _exportSqlDataReader[6];
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

        public void UveziRobu(string staraSifra, string internaSifra, string naziv, decimal cenaDela, decimal cenaUgradnje, decimal normaSati, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _uveziPoslovnogPartneraSqlCommand = new SqlCommand("uspUveziRobu", _konekcijaSqlConnection);

                _uveziPoslovnogPartneraSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@InternaSifra", SqlDbType.VarChar, 50).Value = internaSifra;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cenaDela;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
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

        public void NadjiRobu(DataTable roba, string sQLNaredba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiRobuSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiRobuSQLCommand = new SqlCommand();

                _nadjiRobuSQLCommand.CommandText = sQLNaredba;

                _nadjiRobuSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiRobuSQLDataAdapter.SelectCommand = _nadjiRobuSQLCommand;

                //isprazni tabelu
                roba.Clear();

                //napuni tabelu
                try
                {
                    _nadjiRobuSQLDataAdapter.Fill(roba);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void NadjiRobu(DataTable roba, Int32  roba_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiRobuSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiRobuSQLCommand = new SqlCommand();

                _nadjiRobuSQLCommand.CommandText = " SELECT  Roba_ID, [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba where Roba_ID = " + roba_ID;

                _nadjiRobuSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiRobuSQLDataAdapter.SelectCommand = _nadjiRobuSQLCommand;

                //isprazni tabelu
                roba.Clear();

                //napuni tabelu
                try
                {
                    _nadjiRobuSQLDataAdapter.Fill(roba);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
