using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servis.Klase
{
    class Telefon
    {
        public static string Odmaskiraj(string telefon)
        {
            char[] _maska = telefon.ToCharArray();
            StringBuilder _telefon = new StringBuilder();

            for (int i = 0; i < _maska.Length; i++)
            {

                if (Char.IsNumber(_maska[i]))
                {
                    _telefon.Append(_maska[i]);
                }
            }

            if (_telefon.Length == 0)
            {
                return "";
            }
            else
            {
                return _telefon.ToString();
            }
        }
    }
}
