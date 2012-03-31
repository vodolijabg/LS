using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servis
{
    class Helper
    {
        public static string DajStringSaVelikimPocetnimSlovom(string s)
        {
            string _s = s.Trim();

            if(s.Count().Equals(0))
            {
                return s.Trim();
            }else
            {
                string _ret = s.Substring(0, 1).ToUpper();
                
                if(s.Count() > 1)
                {
                    _ret += _s.Substring(1, s.Count() - 1).ToLower();
                }

                return _ret;
            }

        }

        public static string ZameniSrpskeEngleskimKarakterima(string tekst)
        {
            try
            {
                //ŠšŽžČčĆć
                string _tekst = tekst;
                _tekst = _tekst.Replace("Š", "S");
                _tekst = _tekst.Replace("š", "s");
                _tekst = _tekst.Replace("Ž", "Z");
                _tekst = _tekst.Replace("ž", "z");
                _tekst = _tekst.Replace("Č", "C");
                _tekst = _tekst.Replace("č", "c");
                _tekst = _tekst.Replace("Ć", "C");
                _tekst = _tekst.Replace("ć", "c");

                return _tekst;
            }
            catch (Exception)
            {
                //ako dodje do greske vrati orginalan tekst
                return tekst;
            }
        }
    }
}
