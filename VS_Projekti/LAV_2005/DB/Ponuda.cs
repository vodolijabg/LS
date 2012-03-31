using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class Ponuda
    {
        public void DajPoslednjuPonudu(DataTable ponuda)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPoslednjuPonuduSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPoslednjuPonuduSqlCommand = new SqlCommand("SELECT top 1 * FROM  vwPonudaZaglavlje ORDER BY Ponuda_ID DESC", _konekcijaSqlConnection);

                _dajPoslednjuPonuduSQLDataAdapter.SelectCommand = _dajPoslednjuPonuduSqlCommand;

                //isprazni tabelu
                ponuda.Clear();

                try
                {
                    //napuni tabelu 
                    _dajPoslednjuPonuduSQLDataAdapter.Fill(ponuda);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void DajPonudu(DataTable ponuda, Int32 ponuda_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPonuduSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPoslednjuPonuduSqlCommand = new SqlCommand("SELECT  * FROM  vwPonudaZaglavlje where Ponuda_ID = " + ponuda_ID, _konekcijaSqlConnection);

                _dajPonuduSQLDataAdapter.SelectCommand = _dajPoslednjuPonuduSqlCommand;

                //isprazni tabelu
                ponuda.Clear();

                try
                {
                    //napuni tabelu 
                    _dajPonuduSQLDataAdapter.Fill(ponuda);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void ObrisiPonudu(Int32 ponuda_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiRobuSQLCommand = new SqlCommand("uspObrisiPonudu", _konekcijaSqlConnection);

                _obrisiRobuSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiRobuSQLCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Value = ponuda_ID;

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

        public Int32 UnesiPonudu(Int16 poslovniPartner_ID, Int32 tipAutomobila_ID, Int16 radnik_ID, DateTime datum, bool zakljucena, string brojServisneKnjizice, string brojMotora, string brojSasije, string registarskiBroj, bool servo, bool klima, bool abs, string napomena)
        {
            Int32 _ponuda_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiPonuduSqlCommand = new SqlCommand("uspUnesiPonudu", _konekcijaSqlConnection);

                _unesiPonuduSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiPonuduSqlCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                _unesiPonuduSqlCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.Int).Value = poslovniPartner_ID;
                if (tipAutomobila_ID != -1)
                {
                    _unesiPonuduSqlCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = tipAutomobila_ID;
                }
                else
                {
                    _unesiPonuduSqlCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = Convert.DBNull;

                }
                _unesiPonuduSqlCommand.Parameters.Add("@Radnik_ID", SqlDbType.SmallInt).Value = radnik_ID;
                _unesiPonuduSqlCommand.Parameters.Add("@Datum", SqlDbType.DateTime).Value = datum;
                _unesiPonuduSqlCommand.Parameters.Add("@Zakljucena", SqlDbType.Bit).Value = zakljucena;
                _unesiPonuduSqlCommand.Parameters.Add("@BrojServisneKnjizice", SqlDbType.NVarChar, 20).Value = brojServisneKnjizice;
                _unesiPonuduSqlCommand.Parameters.Add("@BrojMotora", SqlDbType.NVarChar, 20).Value = brojMotora;
                _unesiPonuduSqlCommand.Parameters.Add("@BrojSasije", SqlDbType.NVarChar, 20).Value = brojSasije;
                _unesiPonuduSqlCommand.Parameters.Add("@RegistarskiBroj", SqlDbType.NVarChar, 20).Value = registarskiBroj;
                _unesiPonuduSqlCommand.Parameters.Add("@Servo", SqlDbType.Bit).Value = servo;
                _unesiPonuduSqlCommand.Parameters.Add("@Klima", SqlDbType.Bit).Value = klima;
                _unesiPonuduSqlCommand.Parameters.Add("@ABS", SqlDbType.Bit).Value = abs;
                _unesiPonuduSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Ponuda_ID", "Ponuda", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiPonuduSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _ponuda_ID = (Int32)_unesiPonuduSqlCommand.Parameters["@Ponuda_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _ponuda_ID;
            }
        }

        public void IzmeniPonudu(Int32 ponuda_ID,Int16 poslovniPartner_ID, Int32 tipAutomobila_ID, Int16 radnik_ID, DateTime datum, bool zakljucena, string brojServisneKnjizice, string brojMotora, string brojSasije, string registarskiBroj, bool servo, bool klima, bool abs, string napomena)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _izmeniPonuduSqlCommand = new SqlCommand("uspIzmeniPonudu", _konekcijaSqlConnection);

                _izmeniPonuduSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _izmeniPonuduSqlCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Value = ponuda_ID;
                _izmeniPonuduSqlCommand.Parameters.Add("@PoslovniPartner_ID", SqlDbType.Int).Value = poslovniPartner_ID;
                if (tipAutomobila_ID != -1)
                {
                    _izmeniPonuduSqlCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = tipAutomobila_ID;
                }
                else
                {
                    _izmeniPonuduSqlCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = Convert.DBNull;

                }
                _izmeniPonuduSqlCommand.Parameters.Add("@Radnik_ID", SqlDbType.SmallInt).Value = radnik_ID;
                _izmeniPonuduSqlCommand.Parameters.Add("@Datum", SqlDbType.DateTime).Value = datum;
                _izmeniPonuduSqlCommand.Parameters.Add("@Zakljucena", SqlDbType.Bit).Value = zakljucena;
                _izmeniPonuduSqlCommand.Parameters.Add("@BrojServisneKnjizice", SqlDbType.NVarChar, 20).Value = brojServisneKnjizice;
                _izmeniPonuduSqlCommand.Parameters.Add("@BrojMotora", SqlDbType.NVarChar, 20).Value = brojMotora;
                _izmeniPonuduSqlCommand.Parameters.Add("@BrojSasije", SqlDbType.NVarChar, 20).Value = brojSasije;
                _izmeniPonuduSqlCommand.Parameters.Add("@RegistarskiBroj", SqlDbType.NVarChar, 20).Value = registarskiBroj;
                _izmeniPonuduSqlCommand.Parameters.Add("@Servo", SqlDbType.Bit).Value = servo;
                _izmeniPonuduSqlCommand.Parameters.Add("@Klima", SqlDbType.Bit).Value = klima;
                _izmeniPonuduSqlCommand.Parameters.Add("@ABS", SqlDbType.Bit).Value = abs;
                _izmeniPonuduSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _izmeniPonuduSqlCommand.ExecuteNonQuery();

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

        public Boolean PostojiLiPonuda(Int32 ponuda_ID)
        {
            Boolean _b = false;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _postojiLiRacunSqlCommand = new SqlCommand("SELECT dbo.udfPostojiLiPonuda (" + ponuda_ID + ")", _konekcijaSqlConnection);

                _postojiLiRacunSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    _b = (Boolean)_postojiLiRacunSqlCommand.ExecuteScalar();
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

            return _b;
        }

        public void NadjiPonudu(DataTable ponuda, string sQLNaredba)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _nadjiPonuduSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _nadjiPonuduSQLCommand = new SqlCommand();

                _nadjiPonuduSQLCommand.CommandText = sQLNaredba;

                _nadjiPonuduSQLCommand.Connection = _konekcijaSqlConnection;

                _nadjiPonuduSQLDataAdapter.SelectCommand = _nadjiPonuduSQLCommand;

                //isprazni tabelu
                ponuda.Clear();

                //napuni tabelu
                try
                {
                    _nadjiPonuduSQLDataAdapter.Fill(ponuda);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
