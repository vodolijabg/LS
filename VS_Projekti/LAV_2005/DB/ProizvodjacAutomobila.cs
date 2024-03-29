using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace DB
{
    public class ProizvodjacAutomobila
    {
        public void DajPadajucuListuProizvodjacAutomobila(DataTable proizvodjacAutomobila)
        {
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = Konekcija.DajKonekciju())
            {
                SqlDataAdapter _dajPadajucuListuProizvodjacAutomobilaSQLDataAdapter = new SqlDataAdapter();
                SqlCommand _dajPadajucuListuProizvodjacAutomobilaSQLCommand = new SqlCommand();

                _dajPadajucuListuProizvodjacAutomobilaSQLCommand.CommandText =
                " SELECT     Proizvodjac.Proizvodjac_ID AS ProizvodjacAutomobila_ID, Proizvodjac.Naziv " +
                        " FROM         ModelAutomobila INNER JOIN " +
                              " Proizvodjac ON ModelAutomobila.Proizvodjac_ID = Proizvodjac.Proizvodjac_ID " +
                        " GROUP BY Proizvodjac.Proizvodjac_ID, Proizvodjac.Naziv " +
                        " ORDER BY Proizvodjac.Naziv ";

                _dajPadajucuListuProizvodjacAutomobilaSQLCommand.Connection = _konekcijaSqlConnection;

                _dajPadajucuListuProizvodjacAutomobilaSQLDataAdapter.SelectCommand = _dajPadajucuListuProizvodjacAutomobilaSQLCommand;

                //isprazni tabelu
                proizvodjacAutomobila.Clear();

                //napuni tabelu
                try
                {
                    _dajPadajucuListuProizvodjacAutomobilaSQLDataAdapter.Fill(proizvodjacAutomobila);

                    //dodaj prazan red na indexu 0
                    DataRow _dr;
                    _dr = proizvodjacAutomobila.NewRow();

                    _dr[0] = "-1";
                    _dr[1] = "";
                    proizvodjacAutomobila.Rows.InsertAt(_dr, 0);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
