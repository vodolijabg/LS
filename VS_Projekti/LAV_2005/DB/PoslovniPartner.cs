using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DB
{
    public class PoslovniPartner
    {
        public void DajSvePoslovnePartnere(DataTable poslovniPartner)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSvePoslovnePartnereSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSvePoslovnePartnereSqlCommand = new SqlCommand("SELECT PoslovniPartner_ID, [Stara šifra], Naziv, PIB, Mesto_ID,  Mesto, Adresa, Telefon, [E-mail],[Kontakt osoba], Napomena FROM vwPoslovniPartner", _konekcijaSqlConnection);

                _dajSvePoslovnePartnereSqlDataAdapter.SelectCommand = _dajSvePoslovnePartnereSqlCommand;

                poslovniPartner.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSvePoslovnePartnereSqlDataAdapter.Fill(poslovniPartner);
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

        public Int16 UnesiPoslovnogPartnera(string staraSifra, string naziv, string PIB, Byte mesto_ID, string adresa, string telefon, string email, string kontaktOsoba, string napomena)
        {
            Int16 _poslovniPartner_ID = -1;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiPoslovnogPartneraSqlCommand = new SqlCommand("uspUnesiPoslovnogPartnera", _konekcijaSqlConnection);

                _unesiPoslovnogPartneraSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@PIB", SqlDbType.VarChar, 15).Value = PIB;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 50).Value = adresa;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Telefon", SqlDbType.VarChar, 50).Value = telefon;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@KontaktOsoba", SqlDbType.NVarChar, 50).Value = kontaktOsoba;
                _unesiPoslovnogPartneraSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj brojac
                    InkrementalniKljuc.ResetujInkrementalniKljuc("PoslovniPartner_ID", "PoslovniPartner", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiPoslovnogPartneraSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _poslovniPartner_ID = (Int16)_unesiPoslovnogPartneraSqlCommand.Parameters["@PoslovniPartner_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _poslovniPartner_ID;

            }
        }

        public void IzmeniPoslovnogPartnera(Int16 poslovniPartner, string staraSifra, string naziv, string PIB, Byte mesto_ID, string adresa, string telefon, string email, string kontaktOsoba, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {

                SqlCommand _izmeniPoslovnogPartneraSqlCommand = new SqlCommand("uspIzmeniPoslovnogPartnera", _konekcijaSqlConnection);

                _izmeniPoslovnogPartneraSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.SmallInt).Value = poslovniPartner;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@PIB", SqlDbType.VarChar, 15).Value = PIB;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Mesto_ID", SqlDbType.TinyInt).Value = mesto_ID;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 50).Value = adresa;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Telefon", SqlDbType.VarChar, 50).Value = telefon;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = email;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@KontaktOsoba", SqlDbType.NVarChar, 50).Value = kontaktOsoba;
                _izmeniPoslovnogPartneraSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;
                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniPoslovnogPartneraSqlCommand.ExecuteNonQuery();
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

        public void ObrisiPoslovnogPartnera(Int16 poslovniPartner)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiPoslovnogPartneraSQLCommand = new SqlCommand("uspObrisiPoslovnogPartnera", _konekcijaSqlConnection);

                _obrisiPoslovnogPartneraSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiPoslovnogPartneraSQLCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.SmallInt).Value = poslovniPartner;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiPoslovnogPartneraSQLCommand.ExecuteNonQuery();

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

        public void DajPadajucuListuPoslovniPartner(DataTable poslovniPartner)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuPoslovniPartnerSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuPoslovniPartnerSqlCommand = new SqlCommand("SELECT PoslovniPartner_ID, Naziv FROM vwPoslovniPartner order by Naziv", _konekcijaSqlConnection);

                _dajPadajucuListuPoslovniPartnerSqlDataAdapter.SelectCommand = _dajPadajucuListuPoslovniPartnerSqlCommand;

                poslovniPartner.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajPadajucuListuPoslovniPartnerSqlDataAdapter.Fill(poslovniPartner);

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

        public int IzveziPoslovnePartnere(string imeFajla)
        {
            int _brojRedova = 0;

            StreamWriter _exportStreamWriter = new StreamWriter(imeFajla);

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _dajPoslovnePartnereSqlCommand = new SqlCommand("SELECT [Stara šifra], Naziv, PIB, Mesto, Adresa, Telefon, [E-mail], [Kontakt osoba], Napomena FROM vwPoslovniPartner", _konekcijaSqlConnection);

                _dajPoslovnePartnereSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    SqlDataReader _exportSqlDataReader = _dajPoslovnePartnereSqlCommand.ExecuteReader();

                    while (_exportSqlDataReader.Read())
                    {
                        _brojRedova++;

                        string _c = _exportSqlDataReader[0] + "\t" + _exportSqlDataReader[1] + "\t" + _exportSqlDataReader[2] + "\t" + _exportSqlDataReader[3] + "\t" + _exportSqlDataReader[4] + "\t" + _exportSqlDataReader[5] + "\t" + _exportSqlDataReader[6] + "\t" + _exportSqlDataReader[7] + "\t" + _exportSqlDataReader[8];
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

        public void UveziPoslovnogPartnera(string staraSifra, string naziv, string PIB, string mesto, string adresa, string telefon, string email, string kontaktOsoba, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _uveziPoslovnogPartneraSqlCommand = new SqlCommand("uspUveziPoslovnogPartnera", _konekcijaSqlConnection);

                _uveziPoslovnogPartneraSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@StaraSifra", SqlDbType.VarChar, 15).Value = staraSifra;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Naziv", SqlDbType.NVarChar, 60).Value = naziv;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@PIB", SqlDbType.VarChar, 15).Value = PIB;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Mesto", SqlDbType.NVarChar, 30).Value = mesto;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 50).Value = adresa;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Telefon", SqlDbType.VarChar, 50).Value = telefon;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                _uveziPoslovnogPartneraSqlCommand.Parameters.Add("@KontaktOsoba", SqlDbType.NVarChar, 50).Value = kontaktOsoba;
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

        public void DajArtikleDobavljace(DataTable artikliDobavljaca, Int16 poslovniPartner_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajArtikleDobavljacaSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajArtikleDobavljacaSqlCommand = new SqlCommand("uspDajArtikleDobavljaca", _konekcijaSqlConnection);

                _dajArtikleDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajArtikleDobavljacaSqlCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.SmallInt).Value = poslovniPartner_ID;

                #endregion


                _dajArtikleDobavljacaSqlDataAdapter.SelectCommand = _dajArtikleDobavljacaSqlCommand;

                artikliDobavljaca.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajArtikleDobavljacaSqlDataAdapter.Fill(artikliDobavljaca);

                    _konekcijaSqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void NadjiPoslovnogPartnera(DataTable poslovniPartner, string sQLNaredba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiPoslovnogPartneraSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiPoslovnogPartneraSQLCommand = new SqlCommand();

                _nadjiPoslovnogPartneraSQLCommand.CommandText = sQLNaredba;

                _nadjiPoslovnogPartneraSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiPoslovnogPartneraSQLDataAdapter.SelectCommand = _nadjiPoslovnogPartneraSQLCommand;

                //isprazni tabelu
                poslovniPartner.Clear(); 

                //napuni tabelu
                try
                {
                    _nadjiPoslovnogPartneraSQLDataAdapter.Fill(poslovniPartner);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DajPoslovnogPartnere(DataTable poslovniPartner, Int16 poslovniPartner_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajSvePoslovnePartnereSqlDataAdapter = new SqlDataAdapter();
                SqlCommand _dajSvePoslovnePartnereSqlCommand = new SqlCommand("SELECT PoslovniPartner_ID, [Stara šifra], Naziv, PIB, Mesto_ID,  Mesto, Adresa, Telefon, [E-mail],[Kontakt osoba], Napomena FROM vwPoslovniPartner where PoslovniPartner_ID = " + poslovniPartner_ID, _konekcijaSqlConnection);

                _dajSvePoslovnePartnereSqlDataAdapter.SelectCommand = _dajSvePoslovnePartnereSqlCommand;

                poslovniPartner.Clear();

                try
                {
                    _konekcijaSqlConnection.Open();

                    _dajSvePoslovnePartnereSqlDataAdapter.Fill(poslovniPartner);
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
