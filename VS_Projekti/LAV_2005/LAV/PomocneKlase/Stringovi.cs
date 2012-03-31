using System;
using System.Collections.Generic;
using System.Text;

namespace LAV.PomocneKlase
{
    public class Stringovi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">string koji predstavlja datum u formi YYYYMM ili YYYYM</param>
        /// <returns>vraca string koji predstavlja datum u formi MM.YYYY ili parametar ako nema 5 ili 6 karaktera </returns>
        public static string StringUDatum(string s)
        {
            string _s = "";

            if ((s.Length == 5)||(s.Length == 6))
            {
                StringBuilder _godina = new StringBuilder(s, 0, 4, 6);
                
                if (s.Length==5)
                {
                    StringBuilder _mesec = new StringBuilder(s, 4, 1, 5);
                    _s = _mesec + "." + _godina;
                }
                else if (s.Length == 6)
                {
                    StringBuilder _mesec = new StringBuilder(s, 4, 2, 6);
                    _s = _mesec + "." + _godina;
                }

               

                return _s;
            }
            else
            {
                //ako nije dobar format vrati parametar
                return s;
            }

        }
    }
}
