using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;


namespace LAV.PomocneKlase
{
    class Index
    {
        /// <summary>
        /// Vraca index reda ako nadje vrednost ili -1
        /// </summary>
        /// <param name="grid">Kontrola u kojoj se nalaze podaci</param>
        /// <param name="kolonaZaPretragu">kolona u kojoj se nalazi vrednost za koju zelimo index reda u kojoj se nalazi. Ako vrednost nije jedinstvena vratice index prve na koju naidje</param>
        /// <param name="vrednostZaPretragu">vrednost za pretragu</param>
        /// <returns>Index reda ili -1</returns>
        public static int DajIndexReda(DataGridView grid, string kolonaZaPretragu, string vrednostZaPretragu)
        {
            int _indexReda = -1;

            for (int i = 0; i < grid.RowCount; i++)
            {
                string _s = grid.Rows[i].Cells[kolonaZaPretragu].Value.ToString();

                if (_s == vrednostZaPretragu)
                {
                    _indexReda = i;

                    //zavrsi petlju 
                    break;
                }
            }
            return _indexReda;
        }
    }
}
