using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace DB
{
    public class KorisnikPrograma
    {
        public void DajKorisnikaPrograma(DataTable korisnikPrograma)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajKorisnikaProgramaSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajKorisnikaProgramaSqlCommand = new SqlCommand("SELECT KorisnikPrograma_ID, Naziv, Mesto_ID, Mesto, [Pozivni broj], [Poštanski broj], Adresa, PIB, [Žiro račun], Telefon FROM vwKorisnikPrograma", _konekcijaSqlConnection);

                _dajKorisnikaProgramaSqlDataAdapter.SelectCommand = _dajKorisnikaProgramaSqlCommand;

                korisnikPrograma.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajKorisnikaProgramaSqlDataAdapter.Fill(korisnikPrograma);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void UnesiKorisnikaPrograma(Byte korisnikPrograma_ID, string naziv, Byte mesto_ID, string adresa, string PIB, string ziroRacun, string telefon)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiKorisnikaProgramaSqlCommand = new SqlCommand("uspUnesiKorisnikaPrograma", _konekcijaSqlConnection);

                _unesiKorisnikaProgramaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@KorisnikPrograma_ID", SqlDbType.TinyInt).Value = korisnikPrograma_ID;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 50).Value = adresa;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@PIB", SqlDbType.VarChar, 15).Value = PIB;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@ZiroRacun", SqlDbType.VarChar, 50).Value = ziroRacun;
                _unesiKorisnikaProgramaSqlCommand.Parameters.Add("@Telefon", SqlDbType.VarChar, 50).Value = telefon;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                     //pa zatim upisi novi red
                    _unesiKorisnikaProgramaSqlCommand.ExecuteScalar();

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

        public void IzmeniKorisnikaPrograma(Byte korisnikPrograma_ID, string naziv, Byte mesto_ID, string adresa, string PIB, string ziroRacun, string telefon)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniKorisnikaProgramaSqlCommand = new SqlCommand("uspIzmeniKorisnikaPrograma", _konekcijaSqlConnection);

                _izmeniKorisnikaProgramaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@KorisnikPrograma_ID", SqlDbType.TinyInt).Value = korisnikPrograma_ID;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 50).Value = adresa;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@PIB", SqlDbType.VarChar, 15).Value = PIB;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@ZiroRacun", SqlDbType.VarChar, 50).Value = ziroRacun;
                _izmeniKorisnikaProgramaSqlCommand.Parameters.Add("@Telefon", SqlDbType.VarChar, 50).Value = telefon;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniKorisnikaProgramaSqlCommand.ExecuteNonQuery();
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

        public void ObrisiKorisnikaPrograma(Byte korisnikPrograma_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiKorisnikaProgramaSQLCommand = new SqlCommand("uspObrisiKorisnikaPrograma", _konekcijaSqlConnection);

                _obrisiKorisnikaProgramaSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiKorisnikaProgramaSQLCommand.Parameters.Add("@KorisnikPrograma_ID", SqlDbType.TinyInt).Value = korisnikPrograma_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiKorisnikaProgramaSQLCommand.ExecuteNonQuery();

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
