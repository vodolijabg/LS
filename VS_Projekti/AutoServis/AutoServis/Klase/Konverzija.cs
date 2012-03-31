using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Data.Linq;


namespace AutoServis
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _parametar = "d";

            if ((string)parameter == "g")
            {
                _parametar = "g";
            }
            try
            {
                DateTime _datum = (DateTime)value;
                //return _datum.ToString("d", CultureInfo.CurrentCulture);
                return _datum.ToString(_parametar, CultureInfo.CurrentCulture);

            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _datum = value.ToString();

                return DateTime.Parse(_datum, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(DateTime), typeof(bool))]
    public class DateTimeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime _datum = (DateTime)value;

                return true;

            }
            catch
            {
                return false; ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _datum = value.ToString();

                return DateTime.Parse(_datum, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(Int32), typeof(string))]
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Int32.Parse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture);
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _vrednost = value.ToString();

                return Int32.Parse(_vrednost);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(Int64), typeof(string))]
    public class BigIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Int64.Parse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture);
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _vrednost = value.ToString();

                return Int64.Parse(_vrednost);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(Baza.Mesto), typeof(Baza.Mesto))]
    public class MestoToMestoConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.Mesto _mesto = (Baza.Mesto)value;

                if (!_mesto.Naziv.Equals(""))
                {
                    return _mesto;
                }
                else
                {
                    return null;
                }
                    
            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(Baza.Artikal), typeof(Baza.Artikal))]
    public class ArtikalToArtikalConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.Artikal _artikal = (Baza.Artikal)value;

                if (!_artikal.BrojProizvodjaca.Equals(""))
                {
                    return _artikal;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(Baza.FizickoLice), typeof(Baza.FizickoLice))]
    public class FizickoLiceToFizickoLiceConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.FizickoLice _fizickoLice = (Baza.FizickoLice)value;

                if (!_fizickoLice.Ime.Equals(""))
                {
                    return _fizickoLice;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(Baza.PoslovniPartner), typeof(Baza.PoslovniPartner))]
    public class PoslovniPartnerToPoslovniPartnerConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.PoslovniPartner _poslovniPartner = (Baza.PoslovniPartner)value;

                if (!_poslovniPartner.SkracenNaziv.Equals(""))
                {
                    return _poslovniPartner;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(Baza.NosilacGrupe), typeof(Baza.NosilacGrupe))]
    public class NosilacGrupeToNosilacGrupeConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.NosilacGrupe _nosilacGrupe = (Baza.NosilacGrupe)value;

                if (!_nosilacGrupe.Naziv.Equals(""))
                {
                    return _nosilacGrupe;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(Baza.PoreskaStopa), typeof(Baza.PoreskaStopa))]
    public class PoreskaStopaToPoreskaStopaConverter : IValueConverter
    {
        //koristi se zbog prvog praznog reda u padajucoj listi. Taj red ima Mesto_ID= -1 a sve ostalo prazno
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Baza.PoreskaStopa _poreskaStopa = (Baza.PoreskaStopa)value;

                if (!_poreskaStopa.VrednostProcenata.Equals(""))
                {
                    return _poreskaStopa;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }

    [ValueConversion(typeof(string), typeof(string))]
    public class PrazanStringToNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (((string)value).Trim().Equals(""))
                {
                    return DBNull.Value;
                }
                else
                {
                    return value;
                }
            }
            catch 
            {

                return DBNull.Value;
            }
            

        }

    }

    [ValueConversion(typeof(Decimal), typeof(string))]
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Decimal _decimal = (Decimal)value;
                return _decimal.ToString(".00", CultureInfo.CurrentCulture);
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _decimal = value.ToString();

                return Decimal.Parse(_decimal, CultureInfo.CurrentCulture);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(Int32), typeof(string))]
    public class IntToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string _value = value.ToString();

                if (_value.Length.Equals(6))
                {
                    char[] _godina = _value.ToCharArray(0, 4);
                    char[] _mesec = _value.ToCharArray(4, 2);

                    StringBuilder _rezultat = new StringBuilder(7);

                    foreach (var mesec in _mesec)
                    {
                        _rezultat.Append(mesec);
                    }

                    _rezultat.Append(".");

                    foreach (var godina in _godina)
                    {
                        _rezultat.Append(godina);
                    }


                    return _rezultat;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _datum = value.ToString();

            Int32 _rezultat;

            if (_datum.Length.Equals(7))
            {
                char[] _godina = _datum.ToCharArray(0, 4);
                char[] _mesec = _datum.ToCharArray(5, 2);



                if (Int32.TryParse(_mesec.ToString() + _godina.ToString(), NumberStyles.Any, culture, out _rezultat))
                {
                    return _rezultat;
                }
            }

            return value;
        }
    }

    public class ConcatenateDateToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name;

            if (values[0] == null)
            {
                values[0] = "";
            }
            if (values[1] == null)
            {
                values[1] = "";
            }

            name = values[0].ToString() + (string)parameter + values[1].ToString();
            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split('-');
            return splitValues;
        }

    }

    public class ConcatenateStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat;

            if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
            {
                values[0] = "";
            }
            if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
            {
                values[1] = "";
            }

            _rezultat = values[0].ToString() + (string)parameter + values[1].ToString();
            return _rezultat;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class ConcatenateIntToDateToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _godina1 = "";
            string _godina2 = "";

            if (values[0] != null)
            {
                string _value1 = values[0].ToString();

                if (_value1.Length.Equals(6))
                {
                    char[] _godina = _value1.ToCharArray(0, 4);
                    char[] _mesec = _value1.ToCharArray(4, 2);

                    StringBuilder _rezultat1 = new StringBuilder(7);

                    foreach (var mesec in _mesec)
                    {
                        _rezultat1.Append(mesec);
                    }

                    _rezultat1.Append(".");

                    foreach (var godina in _godina)
                    {
                        _rezultat1.Append(godina);
                    }


                    _godina1 = _rezultat1.ToString();
                }
            }

            if (values[1] != null)
            {
                string _value2 = values[1].ToString();

                if (_value2.Length.Equals(6))
                {
                    char[] _godina = _value2.ToCharArray(0, 4);
                    char[] _mesec = _value2.ToCharArray(4, 2);

                    StringBuilder _rezultat2 = new StringBuilder(7);

                    foreach (var mesec in _mesec)
                    {
                        _rezultat2.Append(mesec);
                    }

                    _rezultat2.Append(".");

                    foreach (var godina in _godina)
                    {
                        _rezultat2.Append(godina);
                    }


                    _godina2 = _rezultat2.ToString();
                }
            }

            if (_godina1 == "" && _godina2 == "")
            {
                return "";
            }
            else
            {
                return _godina1 + (string)parameter + _godina2;
            }
            
        
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }
    }

    public class ConcatenateThreeStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat;

            if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
            {
                values[0] = "";
            }
            if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
            {
                values[1] = "";
            }
            if (values[2] == null || values[2] == DependencyProperty.UnsetValue)
            {
                values[2] = "";
            }

            if ((string)parameter == null)
            {
                parameter = "  ";
            }

            _rezultat = values[0].ToString() + (string)parameter + values[1].ToString() + (string)parameter + values[2].ToString();

            if (!_rezultat.Trim().Length.Equals(0))
            {
                return _rezultat;
            }
            else
            {
                return values;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajCenuUslugeSaPorezomConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                decimal _vrednost = 0;

                string _bodVrednost = values[0].ToString();
                string _poreskaStopaVrednost = values[1].ToString();
                string _brojBodova = values[2].ToString();

                _vrednost = (Decimal.Parse(_bodVrednost) * ((Decimal.Parse(_poreskaStopaVrednost) / 100) + 1)) * Decimal.Parse(_brojBodova);

                return _vrednost.ToString(CultureInfo.CurrentCulture);

            }
            catch (Exception)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] _values = new object[1];
            _values[0] = value;
            return _values;
        }

    }

    public class DajCenuUslugeBezPorezaConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                decimal _vrednost = 0;

                string _bodVrednost = values[0].ToString();
                string _brojBodova = values[1].ToString();

                _vrednost = Decimal.Parse(_bodVrednost) * Decimal.Parse(_brojBodova);

                return _vrednost.ToString("##.00").ToString(CultureInfo.CurrentCulture);

            }
            catch (Exception)
            {
                return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] _values = new object[1];
            _values[0] = value;
            return _values;
        }

    }

    public class PravnoLiceAndFizickoLiceToPartnerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat = "";

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                _rezultat = values[0].ToString();
            }
            else  if (values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                _rezultat = values[1].ToString();
            }

            return _rezultat;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class ConcatenateTwoStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat;

            if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
            {
                values[0] = "";
            }
            if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
            {
                values[1] = "";
            }
            
            if ((string)parameter == null)
            {
                parameter = "   ";
            }

            _rezultat = values[0].ToString() + (string)parameter +  values[1].ToString() ;

            if (!_rezultat.Trim().Length.Equals(0))
            {
                return _rezultat;
            }
            else
            {
                return values;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class ConcatenateRadnikAndRadniNalogConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat;

            _rezultat = values[0].ToString() + " (" + values[1].ToString() + "/" + values[2].ToString() + "/" + values[3].ToString() + ")";

            return _rezultat;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }


    public class FilterStavkaUslugaRadniRasporedPoRadnoMestoMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {

                //EntitySet<Baza.StavkaUslugaRadniRaspored> _value = null;

                //_value = (EntitySet<Baza.StavkaUslugaRadniRaspored>)values[0];

                //int _radnoMesto_ID = System.Convert.ToInt32(values[1]);

                ////EntitySet<Baza.StavkaUslugaRadniRaspored> _novi = new EntitySet<Baza.StavkaUslugaRadniRaspored>();


                ////foreach (Baza.StavkaUslugaRadniRaspored item in _value)
                ////{
                ////    if (item.RadnoMesto_ID == _radnoMesto_ID)
                ////        _novi.Add(item);
                ////}

                ////return _novi;

                //return _value.Where(f => f.RadnoMesto_ID == _radnoMesto_ID);

                ObservableCollection<Baza.StavkaUslugaRadniRaspored> _value = null;

                _value = (ObservableCollection<Baza.StavkaUslugaRadniRaspored>)values[0];

                int _radnoMesto_ID = System.Convert.ToInt32(values[1]);
                int _radnoVreme_ID = System.Convert.ToInt32(values[2]);

                ObservableCollection<Baza.StavkaUslugaRadniRaspored> _novi = new ObservableCollection<Baza.StavkaUslugaRadniRaspored>();

                foreach (Baza.StavkaUslugaRadniRaspored item in _value)
                {
                    if (item.RadnoMesto_ID == _radnoMesto_ID && item.RadnoVreme_ID == _radnoVreme_ID)
                        _novi.Add(item);
                }

                return _novi;
                //_value.Where(f => f.RadnoMesto_ID == _radnoMesto_ID && f.RadnoVreme_ID == _radnoVreme_ID);


            }
            catch (Exception)
            {
                return values;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] _rezultat = new object[] { (EntitySet<Baza.StavkaUslugaRadniRaspored>)value };
            return _rezultat;
        }

    }

    public class DajFontHeighConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values[0].ToString() == values[1].ToString())
                {
                    return FontWeights.Bold;

                }
                else
                {
                    return FontWeights.Normal;
                }
            }
            catch (Exception ex)
            {
                return FontWeights.Normal;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
