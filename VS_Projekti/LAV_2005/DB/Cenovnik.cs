using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.IO;

namespace DB
{
    public class Cenovnik
    {
        public int ImportCenovnika(string poslovniPartner, string proizvodjac, string brojProizvodjaca, decimal cena, decimal cenaUgradnje, decimal normaSati, string napomena)
        {
            int _redovaUneto = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiCenovnikDobavljacaSqlCommand = new SqlCommand("uspUveziCenovnik", _konekcijaSqlConnection);

                _unesiCenovnikDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@PoslovniPartner", SqlDbType.NVarChar, 60).Value = poslovniPartner;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar, 60).Value = proizvodjac;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.VarChar, 66).Value = brojProizvodjaca;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@CenaUgradnje", SqlDbType.Decimal, 18).Value = cenaUgradnje;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@NormaSati", SqlDbType.Decimal, 5).Value = normaSati;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();


                    //pa zatim upisi novi red
                    _redovaUneto = _unesiCenovnikDobavljacaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _redovaUneto;
            }
        }

        public int ImportCenovnika(string poslovniPartner, string proizvodjac, string brojProizvodjaca, decimal cena, string napomena)
        {
            int _redovaUneto = 0;

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _unesiCenovnikDobavljacaSqlCommand = new SqlCommand("uspUveziCenovnik", _konekcijaSqlConnection);

                _unesiCenovnikDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@PoslovniPartner", SqlDbType.NVarChar, 60).Value = poslovniPartner;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar, 60).Value = proizvodjac;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.VarChar, 66).Value = brojProizvodjaca;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal, 18).Value = cena;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = napomena;

                #endregion

                try
                {
                    _konekcijaSqlConnection.Open();


                    //pa zatim upisi novi red
                    _redovaUneto = _unesiCenovnikDobavljacaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _redovaUneto;
            }
        }
        
        public int ExportCenovnika(string imeFajla)
        {
            int _brojRedova = 0;

            StreamWriter _cenovnikStreamWriter = new StreamWriter(imeFajla);

            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlCommand _dajCenovnikSqlCommand = new SqlCommand("select * from vwCenovnik", _konekcijaSqlConnection);

                _dajCenovnikSqlCommand.CommandType = CommandType.Text;

                try
                {
                    _konekcijaSqlConnection.Open();

                    SqlDataReader _exportSqlDataReader = _dajCenovnikSqlCommand.ExecuteReader();

                    while (_exportSqlDataReader.Read())
                    {
                        _brojRedova++;

                        string _c = _exportSqlDataReader[0] + "\t" + _exportSqlDataReader[1] + "\t" + _exportSqlDataReader[2] + "\t" + _exportSqlDataReader[3] + "\t" + _exportSqlDataReader[4] + "\t" + _exportSqlDataReader[5] + "\t" + _exportSqlDataReader[6];
                        _cenovnikStreamWriter.WriteLine(_c.ToCharArray());

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
                    _cenovnikStreamWriter.Close();
                }

            }
            return _brojRedova;
        }
    }
}
