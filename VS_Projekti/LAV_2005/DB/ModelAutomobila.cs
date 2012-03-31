using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DB
{
    public class ModelAutomobila
    {
        public void DajPadajucuListuModelAutomobila(DataTable modelAutomobila, Int16 proizvodjacAutomobila_ID)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuModelAutomobilaSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuModelAutomobilaSQLCommand = new SqlCommand("dbo.uspPadajucaListaModelAutomobila", _konekcijaSqlConnection);
                _dajPadajucuListuModelAutomobilaSQLCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _dajPadajucuListuModelAutomobilaSQLCommand.Parameters.Add("@ProizvodjacAutomobila_ID", SqlDbType.SmallInt).Value = proizvodjacAutomobila_ID;

                #endregion

                _dajPadajucuListuModelAutomobilaSQLDataAdapter.SelectCommand = _dajPadajucuListuModelAutomobilaSQLCommand;

                //isprazni tabelu
                modelAutomobila.Clear();

                //napuni tabelu
                try
                {
                    _dajPadajucuListuModelAutomobilaSQLDataAdapter.Fill(modelAutomobila);

                    DataRow _dr = modelAutomobila.NewRow();

                    _dr[0] = "-1";
                    _dr[1] = "";
                    modelAutomobila.Rows.InsertAt(_dr, 0);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
    }
}
