using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class Stavka
    {
        public void DajStavkeZaPonudu(DataTable stavka, Int32 ponuda_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajStavkeZaPonuduSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajStavkeZaPonuduSqlCommand = new SqlCommand("uspDajStavkeZaPonudu", _konekcijaSqlConnection);

                _dajStavkeZaPonuduSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajStavkeZaPonuduSqlCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Value = ponuda_ID;

                #endregion


                _dajStavkeZaPonuduSQLDataAdapter.SelectCommand = _dajStavkeZaPonuduSqlCommand;

                //isprazni tabelu
                stavka.Clear();

                try
                {
                    //napuni tabelu 
                    _dajStavkeZaPonuduSQLDataAdapter.Fill(stavka);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void ObrisiStavku(Int32 stavka_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _obrisiStavkuSQLCommand = new SqlCommand("uspObrisiStavku", _konekcijaSqlConnection);

                _obrisiStavkuSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _obrisiStavkuSQLCommand.Parameters.Add("@Stavka_ID", SqlDbType.Int).Value = stavka_ID;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    _obrisiStavkuSQLCommand.ExecuteNonQuery();

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

        public Int32 UnesiStavku(Int32 artikal_ID, Int32 roba_ID, Int32 usluga_ID, decimal cena, decimal cenaUgradnje, decimal normaSati, Int16 kolicina, Int32 ponuda_ID)
        {
            Int32 _stavka_ID = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiStavkuSqlCommand = new SqlCommand("uspUnesiStavku", _konekcijaSqlConnection);

                _unesiStavkuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiStavkuSqlCommand.Parameters.Add("@Stavka_ID", SqlDbType.Int).Direction = ParameterDirection.Output; 
                if (artikal_ID != -1)
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID; 
                }
                else
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                if (roba_ID != -1)
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = roba_ID;
                }
                else
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                if (usluga_ID != -1)
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = usluga_ID;
                }
                else
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                _unesiStavkuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                if (cenaUgradnje != -1)
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                }
                else
                {
                    _unesiStavkuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = Convert.DBNull;
                }
                _unesiStavkuSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _unesiStavkuSqlCommand.Parameters.Add("@Kolicina", SqlDbType.SmallInt).Value = kolicina;
                _unesiStavkuSqlCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Value = ponuda_ID;
                

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    //prvo resetuj inkrementalni kljuc
                    InkrementalniKljuc.ResetujInkrementalniKljuc("Stavka_ID", "Stavka", _konekcijaSqlConnection);

                    //pa zatim upisi novi red
                    _unesiStavkuSqlCommand.ExecuteScalar();

                    //Daj ID upravo unetog reda. Vrednost se cita iz output parametra i vraca kao output parametar metode
                    _stavka_ID = (Int32)_unesiStavkuSqlCommand.Parameters["@Stavka_ID"].Value;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _stavka_ID;
            }
        }

        public void IzmeniStavku(Int32 stavka_ID ,Int32 artikal_ID, Int32 roba_ID, Int32 usluga_ID, decimal cena, decimal cenaUgradnje, decimal normaSati, Int16 kolicina, Int32 ponuda_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _izmeniStavkuSqlCommand = new SqlCommand("uspIzmeniStavku", _konekcijaSqlConnection);

                _izmeniStavkuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _izmeniStavkuSqlCommand.Parameters.Add("@Stavka_ID", SqlDbType.Int).Value = stavka_ID;
                if (artikal_ID != -1)
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = artikal_ID;
                }
                else
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Artikal_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                if (roba_ID != -1)
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = roba_ID;
                }
                else
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Roba_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                if (usluga_ID != -1)
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = usluga_ID;
                }
                else
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@Usluga_ID", SqlDbType.Int).Value = Convert.DBNull;
                }
                _izmeniStavkuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                if (cenaUgradnje != -1)
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                }
                else
                {
                    _izmeniStavkuSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = Convert.DBNull;
                }
                _izmeniStavkuSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _izmeniStavkuSqlCommand.Parameters.Add("@Kolicina", SqlDbType.SmallInt).Value = kolicina;
                _izmeniStavkuSqlCommand.Parameters.Add("@Ponuda_ID", SqlDbType.Int).Value = ponuda_ID;


                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();

                    
                    _izmeniStavkuSqlCommand.ExecuteNonQuery();

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
