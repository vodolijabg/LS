using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class TipAutomobila
    {
        public void DajPadajucuListuTipAutomobila(DataTable tipAutomobila, Int32 modelAutomobila_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuTipAutomobilaSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuTipAutomobilaSQLCommand = new SqlCommand("dbo.uspPadajucaListaTipAutomobila", _konekcijaSqlConnection);
                _dajPadajucuListuTipAutomobilaSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajPadajucuListuTipAutomobilaSQLCommand.Parameters.Add("@ModelAutomobila_ID", SqlDbType.Int).Value = modelAutomobila_ID;

                #endregion

                _dajPadajucuListuTipAutomobilaSQLDataAdapter.SelectCommand = _dajPadajucuListuTipAutomobilaSQLCommand;

                //isprazni tabelu
                tipAutomobila.Clear();

                //napuni tabelu
                try
                {
                    _dajPadajucuListuTipAutomobilaSQLDataAdapter.Fill(tipAutomobila);

                    //DataRow _dr = tipAutomobila.NewRow();

                    //_dr[0] = "-1";
                    //_dr[1] = "";
                    //tipAutomobila.Rows.InsertAt(_dr, 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DajTipoveAutomobilaZaModel(DataTable tipAutomobila, Int32 modelAutomobila_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajTipoveAutomobilaZaModelSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajTipoveAutomobilaZaModelSQLCommand = new SqlCommand("dbo.uspDajTipoveAutomobilaZaModel", _konekcijaSqlConnection);
                _dajTipoveAutomobilaZaModelSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajTipoveAutomobilaZaModelSQLCommand.Parameters.Add("@ModelAutomobila_ID", SqlDbType.Int).Value = modelAutomobila_ID;

                #endregion

                _dajTipoveAutomobilaZaModelSQLDataAdapter.SelectCommand = _dajTipoveAutomobilaZaModelSQLCommand;

                //isprazni tabelu
                tipAutomobila.Clear();

                //napuni tabelu
                try
                {
                    _dajTipoveAutomobilaZaModelSQLDataAdapter.Fill(tipAutomobila);

                    //DataRow _dr = tipAutomobila.NewRow();

                    //_dr[0] = "-1";
                    //_dr[1] = "";
                    //tipAutomobila.Rows.InsertAt(_dr, 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DajTipAutomobilaDetaljno(DataTable tipAutomobila, Int32 tipAutomobila_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajTipAutomobilaDetaljnoSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajTipAutomobilaDetaljnoSQLCommand = new SqlCommand("dbo.uspDajTipAutomobilaDetaljno", _konekcijaSqlConnection);
                _dajTipAutomobilaDetaljnoSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajTipAutomobilaDetaljnoSQLCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = tipAutomobila_ID;

                #endregion

                _dajTipAutomobilaDetaljnoSQLDataAdapter.SelectCommand = _dajTipAutomobilaDetaljnoSQLCommand;

                //isprazni tabelu
                tipAutomobila.Clear();

                //napuni tabelu
                try
                {
                    _dajTipAutomobilaDetaljnoSQLDataAdapter.Fill(tipAutomobila);

                    //DataRow _dr = tipAutomobila.NewRow();

                    //_dr[0] = "-1";
                    //_dr[1] = "";
                    //tipAutomobila.Rows.InsertAt(_dr, 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public void DajMotorZaTipAutomobila(DataTable motor, Int32 tipAutomobila_ID)
        //{
        //    SqlConnection _konekcijaSqlConnection = new SqlConnection();
        //    using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
        //    {
        //        SqlDataAdapter _dajMotorZaTipAutomobilaSQLDataAdapter = new SqlDataAdapter();
        //        SqlCommand _dajMotorZaTipAutomobilaSQLCommand = new SqlCommand("dbo.uspDajMotorZaTipAutomobila", _konekcijaSqlConnection);
        //        _dajMotorZaTipAutomobilaSQLCommand.CommandType = CommandType.StoredProcedure;

        //        #region Definisi parametre

        //        _dajMotorZaTipAutomobilaSQLCommand.Parameters.Add("@TipAutomobila_ID", SqlDbType.Int).Value = tipAutomobila_ID;

        //        #endregion

        //        _dajMotorZaTipAutomobilaSQLDataAdapter.SelectCommand = _dajMotorZaTipAutomobilaSQLCommand;

        //        //isprazni tabelu
        //        motor.Clear();

        //        //napuni tabelu
        //        try
        //        {
        //            _dajMotorZaTipAutomobilaSQLDataAdapter.Fill(motor);

        //            //DataRow _dr = tipAutomobila.NewRow();

        //            //_dr[0] = "-1";
        //            //_dr[1] = "";
        //            //tipAutomobila.Rows.InsertAt(_dr, 0);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public void DajTipAutomobilaOsnovniPodaci(DataTable proizvodjacIModelDataTable, Int32 tipAutomobila_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajTipAutomobilaOsnovniPodaciSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajTipAutomobilaOsnovniPodaciSQLCommand = new SqlCommand(" SELECT Proizvodjac_ID, Proizvodjac, ModelAutomobila_ID, Model, TipAutomobila_ID, Tip FROM vwTipAutomobilaOsnovniPodaci WHERE TipAutomobila_ID = " + tipAutomobila_ID, _konekcijaSqlConnection);

                _dajTipAutomobilaOsnovniPodaciSQLDataAdapter.SelectCommand = _dajTipAutomobilaOsnovniPodaciSQLCommand;

                //isprazni tabelu
                proizvodjacIModelDataTable.Clear();

                //napuni tabelu
                try
                {
                    _dajTipAutomobilaOsnovniPodaciSQLDataAdapter.Fill(proizvodjacIModelDataTable);

                    //DataRow _dr = tipAutomobila.NewRow();

                    //_dr[0] = "-1";
                    //_dr[1] = "";
                    //tipAutomobila.Rows.InsertAt(_dr, 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
