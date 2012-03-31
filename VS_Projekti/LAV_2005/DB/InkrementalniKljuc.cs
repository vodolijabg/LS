using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace DB
{
    public class InkrementalniKljuc
    {
        public static void ResetujInkrementalniKljuc(string nazivInkrementalneKolone, string nazivTabele, SqlConnection konekcija)
        {
            //MANA - prvi unos ce biti -1 od identity seed
            SqlCommand _komandaSqlCommand = new SqlCommand(
                "DECLARE @COUNT INT " +
                    " if ((select count(*) from " + nazivTabele + ") = 0) " +
                    " set @COUNT = (select IDENT_SEED ( '" + nazivTabele + "' )-1) " +
                    " else " +
                //" set @COUNT = (select top 1 " + nazivInkrementalneKolone + " from " + nazivTabele + " ORDER BY " + nazivInkrementalneKolone + " desc) " +
                    " set @COUNT = (select max(" + nazivInkrementalneKolone + ") from " + nazivTabele + ")" +
                    " DBCC CHECKIDENT (" + nazivTabele + ",RESEED,@COUNT) ", konekcija);

            _komandaSqlCommand.CommandType = CommandType.Text;

            try
            {
                _komandaSqlCommand.ExecuteNonQuery();
            }
            //ako se desi greska "nikom nista"
            catch (Exception)
            {
                //throw ex;
            }
        }

    }
}
