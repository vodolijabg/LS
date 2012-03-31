using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class Mesto
    {
        public void DajPadajucuListuMesto(DataTable mesto, bool vratiNultiPrazan)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuMestoSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuMestoSqlCommand = new SqlCommand("SELECT Mesto_ID, Naziv FROM vwMesto order by Naziv", _konekcijaSqlConnection);

                _dajPadajucuListuMestoSqlDataAdapter.SelectCommand = _dajPadajucuListuMestoSqlCommand;

                mesto.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajPadajucuListuMestoSqlDataAdapter.Fill(mesto);

                    if (vratiNultiPrazan)
                    {
                        DataRow _dr = mesto.NewRow();
                        _dr[0] = "0";
                        _dr[1] = "";
                        mesto.Rows.InsertAt(_dr, 0); 
                    }


                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void DajSvaMesta(DataTable mesto)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSvaMestaSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSvaMestaSqlCommand = new SqlCommand("SELECT Mesto_ID, Naziv, [Pozivni broj], [Poštanski broj] FROM vwMesto", _konekcijaSqlConnection);

                _dajSvaMestaSqlDataAdapter.SelectCommand = _dajSvaMestaSqlCommand;

                mesto.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSvaMestaSqlDataAdapter.Fill(mesto);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public Byte UnesiMesto(string nazivMesta, string pozivniBroj, string postanskiBroj)
        {
            Byte _mesto_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiMestoSqlCommand = new SqlCommand("uspUnesiMesto", _konekcijaSqlConnection);

                _unesiMestoSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiMestoSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                _unesiMestoSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 30).Value = nazivMesta;
                _unesiMestoSqlCommand.Parameters.Add("@PozivniBroj", SqlDbType.VarChar, 5).Value = pozivniBroj;
                _unesiMestoSqlCommand.Parameters.Add("@PostanskiBroj", SqlDbType.VarChar, 5).Value = postanskiBroj;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Mesto_ID", "Mesto", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiMestoSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _mesto_ID = (Byte)_unesiMestoSqlCommand.Parameters["@Mesto_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _mesto_ID;
            }
        }

        public void IzmeniMesto(Byte mesto_ID, string nazivMesta, string pozivniBroj, string postanskiBroj)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniMestoSqlCommand = new SqlCommand("uspIzmeniMesto", _konekcijaSqlConnection);

                _izmeniMestoSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniMestoSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;
                _izmeniMestoSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 30).Value = nazivMesta;
                _izmeniMestoSqlCommand.Parameters.Add("@PozivniBroj", SqlDbType.VarChar, 5).Value = pozivniBroj;
                _izmeniMestoSqlCommand.Parameters.Add("@PostanskiBroj", SqlDbType.VarChar, 5).Value = postanskiBroj;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniMestoSqlCommand.ExecuteNonQuery();
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


        public void ObrisiMesto(Byte mesto_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiMestoSQLCommand = new SqlCommand("uspObrisiMesto", _konekcijaSqlConnection);

                _obrisiMestoSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiMestoSQLCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiMestoSQLCommand.ExecuteNonQuery();

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
