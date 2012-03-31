using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

using System.Data.Linq;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Servis
{
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
                //return _datum.ToString("d", App.cultureInfo);
                return _datum.ToString(_parametar, App.cultureInfo);

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

                return DateTime.Parse(_datum, App.cultureInfo, DateTimeStyles.AdjustToUniversal);
            }
            catch
            {
                return DBNull.Value;
            }

        }

        [ValueConversion(typeof(Int64), typeof(string))]
        public class BigIntToStringConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    return Int64.Parse((string)value, NumberStyles.Any, App.cultureInfo);
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

    }

    [ValueConversion(typeof(Int64), typeof(string))]
    public class BigIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Int64.Parse((string)value, NumberStyles.Any, App.cultureInfo);
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

    [ValueConversion(typeof(Int32), typeof(string))]
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Int32.Parse((string)value, NumberStyles.Any, App.cultureInfo);
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

    [ValueConversion(typeof(Decimal), typeof(string))]
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Decimal _decimal = (Decimal)value;
                return _decimal.ToString("0.00", App.cultureInfo);
                //return _decimal.ToString(App.cultureInfo);
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

                return Decimal.Parse(_decimal, App.cultureInfo);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(DB.Mesto), typeof(DB.Mesto))]
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
                DB.Mesto _mesto = (DB.Mesto)value;

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

    [ValueConversion(typeof(DB.Radnik), typeof(DB.Radnik))]
    public class RadnikToRadnikConverter : IValueConverter
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
                DB.Radnik _radnik = (DB.Radnik)value;

                if (!_radnik.Nadimak.Equals(""))
                {
                    return _radnik;
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

                return DateTime.Parse(_datum, App.cultureInfo, DateTimeStyles.AdjustToUniversal);
            }
            catch
            {
                return DBNull.Value;
            }

        }

    }

    [ValueConversion(typeof(DB.RadniNalogStatus), typeof(DB.RadniNalogStatus))]
    public class RadniNalogStatusToRadniNalogStatusConverter : IValueConverter
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
                DB.RadniNalogStatus _radniNalogStatus = (DB.RadniNalogStatus)value;

                if (!_radniNalogStatus.Naziv.Equals(""))
                {
                    return _radniNalogStatus;
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

    [ValueConversion(typeof(DB.NacinZahtevaZaPonudu), typeof(DB.NacinZahtevaZaPonudu))]
    public class NacinZahtevaZaPonuduToNacinZahtevaZaPonuduConverter : IValueConverter
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
                DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = (DB.NacinZahtevaZaPonudu)value;

                if (!_nacinZahtevaZaPonudu.Naziv.Equals(""))
                {
                    return _nacinZahtevaZaPonudu;
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

    [ValueConversion(typeof(string), typeof(bool))]
    public class DaLiPostojiVrednostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string _s = value.ToString();

                if (_s == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                string e = ex.Message;
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    [ValueConversion(typeof(EntitySet<DB.VezaArtikalDobavljac>), typeof(decimal))]
    public class DajNajpovoljnijuCenuDobavljacaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DB.VezaArtikalDobavljac _vezaArtikalDobavljac = null;

                EntitySet<DB.VezaArtikalDobavljac> _vezaArtikalDobavljacLista = (EntitySet<DB.VezaArtikalDobavljac>)value;


                if (!_vezaArtikalDobavljacLista.Count().Equals(0))
                {
                    _vezaArtikalDobavljac = _vezaArtikalDobavljacLista.OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.Cena).First();

                    return _vezaArtikalDobavljac.Cena;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    [ValueConversion(typeof(EntitySet<DB.VezaArtikalDobavljac>), typeof(string))]
    public class DajKolicinuNajpovoljnijegDobavljacaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DB.VezaArtikalDobavljac _vezaArtikalDobavljac = null;

                EntitySet<DB.VezaArtikalDobavljac> _vezaArtikalDobavljacLista = (EntitySet<DB.VezaArtikalDobavljac>)value;


                if (!_vezaArtikalDobavljacLista.Count().Equals(0))
                {
                    _vezaArtikalDobavljac = _vezaArtikalDobavljacLista.OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.Cena).First();

                    return ((decimal)_vezaArtikalDobavljac.KolicinaNaStanju).ToString("0.00", App.cultureInfo);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    //[ValueConversion(typeof(DB.VezaArtikalDobavljac), typeof(bool))]
    public class DaLiJeDobavljacKorisnikProgramaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value != null && value!= DependencyProperty.UnsetValue)
                {
                                    
                    DB.VezaArtikalDobavljac _vezaArtikalDobavljac = (DB.VezaArtikalDobavljac)value;

                    if (_vezaArtikalDobavljac.KorisnikProgramaID != null)
                    {
                        return Visibility.Hidden;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }


    [ValueConversion(typeof(EntitySet<DB.VezaArtikalDobavljac>), typeof(string))]
    public class DajNajpovoljnijiNazivDobavljacaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DB.VezaArtikalDobavljac _vezaArtikalDobavljac = null;


                EntitySet<DB.VezaArtikalDobavljac> _vezaArtikalDobavljacLista = (EntitySet<DB.VezaArtikalDobavljac>)value;


                if (!_vezaArtikalDobavljacLista.Count().Equals(0))
                {
                    _vezaArtikalDobavljac = _vezaArtikalDobavljacLista.OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.Cena).First();

                    if (_vezaArtikalDobavljac.PoslovniPartner != null)
                    {
                        return _vezaArtikalDobavljac.PoslovniPartner.SkracenNaziv;
                    }
                    else //if (_vezaArtikalDobavljac.KorisnikPrograma != null)
                    {
                        return _vezaArtikalDobavljac.KorisnikPrograma.Naziv;
                    } 
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    public sealed class BackgroundConverterArtikal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            System.Windows.Controls.ListViewItem _item = (System.Windows.Controls.ListViewItem)value;

            DB.Artikal _artikal = (DB.Artikal)_item.DataContext;

            DB.VezaArtikalDobavljac _vezaArtikalDobavljac = null;
            EntitySet<DB.VezaArtikalDobavljac> _vezaArtikalDobavljacLista = (EntitySet<DB.VezaArtikalDobavljac>)_artikal.VezaArtikalDobavljacs;

            if (!_vezaArtikalDobavljacLista.Count().Equals(0))
            {
                _vezaArtikalDobavljac = _vezaArtikalDobavljacLista.OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.Cena).First();

                if (_vezaArtikalDobavljac.PoslovniPartner != null)
                {
                    return System.Windows.Media.Brushes.Transparent; 
                }
                else //if (_vezaArtikalDobavljac.KorisnikPrograma != null)
                {
                    
                    LinearGradientBrush _pozadina = new LinearGradientBrush();

                    //Color _c1 = (Color)new ColorConverter().ConvertFrom("#cd0001");
                    //ColorConverter _cc1 = new ColorConverter().ConvertFrom("#cd0001");

                    //_c1 = (Color)_cc1.ConvertFrom("#cd0001"); 

                    _pozadina.StartPoint = new Point(0, 0);
                    _pozadina.EndPoint = new Point(0, 1);
                    _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0));
                    _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0.40625));
                    _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0.40625));
                    _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 1));

                    return _pozadina; 
                    //return System.Windows.Media.Brushes.GreenYellow; 
                }
            }
            else
            {
                return System.Windows.Media.Brushes.Transparent; 
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public sealed class BackgroundConverterVezaArtikalBrojZaPretragu : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            System.Windows.Controls.ListViewItem _item = (System.Windows.Controls.ListViewItem)value;

            DB.VezaArtikalBrojZaPretragu _vezaArtikalBrojZaPretragu = (DB.VezaArtikalBrojZaPretragu)_item.DataContext;

            if (_vezaArtikalBrojZaPretragu.BrojZaPretragu == "5PK775")
            {
                LinearGradientBrush _pozadina = new LinearGradientBrush();

                _pozadina.StartPoint = new Point(0, 0);
                _pozadina.EndPoint = new Point(0, 1);
                _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0));
                _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0.40625));
                _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 0.40625));
                _pozadina.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FFBD69"), 1));

                return _pozadina;

            }
            else
            {
                return System.Windows.Media.Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    
    public class PomnoziDveVrednostiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
                {
                    values[0] = "0";
                }
                if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
                {
                    values[1] = "0";
                }

                return (System.Convert.ToDecimal(values[0], App.cultureInfo) * System.Convert.ToDecimal(values[1], App.cultureInfo)).ToString("0.00").ToString(App.cultureInfo);

            }
            catch
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

    public class DajPDVZaVrednostConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
                {
                    values[0] = "0";
                }
                if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
                {
                    values[1] = "0";
                }
                if (values[2] == null || values[2] == DependencyProperty.UnsetValue)
                {
                    values[2] = "0";
                }

                return (((System.Convert.ToDecimal(values[0], App.cultureInfo) * System.Convert.ToDecimal(values[1], App.cultureInfo)) / 100) * System.Convert.ToDecimal(values[2], App.cultureInfo)).ToString("0.00").ToString(App.cultureInfo);

            }
            catch
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

    public class DajUkupnoSaPDVConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
                {
                    values[0] = "0";
                }
                if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
                {
                    values[1] = "0";
                }
                if (values[2] == null || values[2] == DependencyProperty.UnsetValue)
                {
                    values[2] = "0";
                }

                decimal _vrednostBezPDV = (System.Convert.ToDecimal(values[0], App.cultureInfo) * System.Convert.ToDecimal(values[1], App.cultureInfo));

                decimal _pDV = ((System.Convert.ToDecimal(values[0], App.cultureInfo) * System.Convert.ToDecimal(values[1], App.cultureInfo)) / 100) * System.Convert.ToDecimal(values[2], App.cultureInfo);

                return (_vrednostBezPDV + _pDV).ToString("0.00").ToString(App.cultureInfo);

            }
            catch
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

    public class DajCenuSaPDVConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //vrednost
                if (values[0] == null || values[0] == DependencyProperty.UnsetValue)
                {
                    values[0] = "0";
                }
                //poreska stopa
                if (values[1] == null || values[1] == DependencyProperty.UnsetValue)
                {
                    values[1] = "0";
                }

                decimal _vrednostBezPDV = (System.Convert.ToDecimal(values[0], App.cultureInfo) * System.Convert.ToDecimal(values[1], App.cultureInfo));

                decimal _pDV = (System.Convert.ToDecimal(values[0], App.cultureInfo) / 100) * System.Convert.ToDecimal(values[1], App.cultureInfo);

                return (System.Convert.ToDecimal(values[0], App.cultureInfo) + _pDV).ToString("0.00").ToString(App.cultureInfo);

            }
            catch
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

            if ((string)parameter == "[]")
            {
                _rezultat = values[0].ToString() + " [" + values[1].ToString() + "]";
            }
            else
            {
                _rezultat = values[0].ToString() + (string)parameter + values[1].ToString();
 
            }
            return _rezultat;
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
            string _rezultat = "";

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

            if (values[0].ToString() != "" && values[1].ToString() != "" && values[2].ToString() != "")
            {
                if ((string)parameter == "ConcatenateArtikal")
                {
                    _rezultat = values[0].ToString() + " [" + values[1].ToString() + "] - " + values[2].ToString();
                }
                else if ((string)parameter == "ConcatenateArtikalTag")
                {
                    _rezultat = values[1].ToString() + "$" + values[0].ToString();
                }
                else if ((string)parameter == "[]")
                {
                    _rezultat = values[0].ToString() + " " + values[1].ToString() + " [" + values[2].ToString() + "]";
                }
                else
                {
                    _rezultat = values[0].ToString() + (string)parameter + values[1].ToString() + (string)parameter + values[2].ToString();
                }
            }

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

    public class ConcatenateFourStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat = "";

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
            if (values[3] == null || values[3] == DependencyProperty.UnsetValue)
            {
                values[3] = "";
            }

            if ((string)parameter == null)
            {
                parameter = "  ";
            }

            if (values[0].ToString() != "" && values[1].ToString() != "" && values[2].ToString() != "")
            {
                _rezultat = values[0].ToString() + (string)parameter + values[1].ToString() + (string)parameter + values[2].ToString() + (string)parameter + values[3].ToString();
            }

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

    public class DajVrstuPartneraConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat = "";

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                _rezultat = "Poslovni partner";
            }
            else if (values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                _rezultat = "Fizičko lice";
            }

            return _rezultat;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajNazivPartneraConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat = "";

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                _rezultat = values[0].ToString();
            }
            else if (values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                if (values.Count().Equals(3))
                {
                    //samo za fizicko lice u gridu ServisnaKnjizica
                    if (values[2] != null && values[2] != DependencyProperty.UnsetValue)
                    {
                        _rezultat = values[1].ToString() + " " + values[2].ToString();
                    }
                    else
                    {
                        _rezultat = values[1].ToString();
                    }
                }
                else
                {
                    _rezultat = values[1].ToString();
                }

            }

            return _rezultat;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajIDPartneraConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _rezultat = "";

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                if ((string)parameter == "ZaTag")
                {
                    _rezultat = values[0].ToString() + "$" + "-1";
                }
                else
                {
                    _rezultat = values[0].ToString();
                }
            }
            else if (values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                if ((string)parameter == "ZaTag")
                {
                    _rezultat = "-1" + "$" + values[1].ToString();
                }
                else
                {
                    _rezultat = values[1].ToString();
                }
            }

            return _rezultat;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajIkonuZaStatusPonudeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool _preuzimaLicno = false;
            DateTime _preuzeoLicnoU;
            bool _preuzeoLicnoUImaVrednost = false;

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                _preuzimaLicno = (bool)values[0];
            }
            if (values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                _preuzeoLicnoU = (DateTime)values[1];
                _preuzeoLicnoUImaVrednost = true;
            }

            bool _obavestiTelefonom = false;
            DateTime _obavestenTelefonomU;
            bool _obavestenTelefonomUImaVrednost = false;

            if (values[2] != null && values[2] != DependencyProperty.UnsetValue)
            {
                _obavestiTelefonom = (bool)values[2];
            }
            if (values[3] != null && values[3] != DependencyProperty.UnsetValue)
            {
                _obavestenTelefonomU = (DateTime)values[3];
                _obavestenTelefonomUImaVrednost = true;
            }

            bool _posaljiSmsObavestenje = false;
            DateTime _poslatoSmsObavestenjeU;
            bool _poslatoSmsObavestenjeUImaVrednost = false;

            if (values[4] != null && values[4] != DependencyProperty.UnsetValue)
            {
                _posaljiSmsObavestenje = (bool)values[4];
            }
            if (values[5] != null && values[5] != DependencyProperty.UnsetValue)
            {
                _poslatoSmsObavestenjeU = (DateTime)values[5];
                _poslatoSmsObavestenjeUImaVrednost = true;
            }

            bool _posaljiEMail = false;
            DateTime _poslatEMailU;
            bool _poslatEMailUImaVrednost = false;

            if (values[6] != null && values[6] != DependencyProperty.UnsetValue)
            {
                _posaljiEMail = (bool)values[6];
            }
            if (values[7] != null && values[7] != DependencyProperty.UnsetValue)
            {
                _poslatEMailU = (DateTime)values[7];
                _poslatEMailUImaVrednost = true;
            }

            string _ikona = "";

            EntitySet<DB.StavkaUsluga> _stavkaUsluga;
            bool _imaStavke = false;
            if (values[8] != null && values[8] != DependencyProperty.UnsetValue)
            {
                _stavkaUsluga = (EntitySet<DB.StavkaUsluga>)values[8];
                if (!_stavkaUsluga.Where(f => f.Status != 'D').Count().Equals(0))
                {
                    _imaStavke = true;
                }
            }


            if (!_imaStavke)
            {
                _ikona = @"images/Cancel.ico";
            }
            else if ( 
                (_preuzimaLicno && !_preuzeoLicnoUImaVrednost) ||
                (_obavestiTelefonom && !_obavestenTelefonomUImaVrednost) ||
                (_obavestiTelefonom && !_obavestenTelefonomUImaVrednost) ||
                (_posaljiEMail && !_poslatEMailUImaVrednost)
                )
            {
                _ikona = @"images/Cancel.ico";
            }
            else
            {
                _ikona = @"images/Check.ico";
            }
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(_ikona, UriKind.Relative);
            logo.EndInit(); 
            
            return  logo; 

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajIkonuZaStatusRadniNalogConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _radniNalogStatusIDZavrsen = Konfiguracija.RadniNalogStatusIDZavrsen;

            bool _zavrsen = true;
            bool _zakljucan = (bool)values[1];

            EntitySet<DB.StavkaUsluga> _stavkaUslugaLista = (EntitySet<DB.StavkaUsluga>)values[0];
            //DB.RadniNalog _radniNalog = (DB.RadniNalog)values[1];

            if (_stavkaUslugaLista.Count > 0)
            {

                foreach (DB.StavkaUsluga item in _stavkaUslugaLista)
                {
                    DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = item.RadniNalogStavkaUsluga;

                    if (_radniNalogStavkaUsluga != null)
                    {
                        if (_radniNalogStavkaUsluga.Status.ToString() != "D" && _radniNalogStavkaUsluga.RadniNalogStatusID.ToString() != _radniNalogStatusIDZavrsen)
                        {
                            _zavrsen = false;
                            break;
                        }
                    }
                    else
                    {
                        _zavrsen = false;
                        break;
                    }
                }
            }

            string _ikona = "";

            if (_zakljucan)
            {
                _ikona = @"images/Log-Out.ico";
            }
            else if (!_zavrsen)            
            {
                _ikona = @"images/Cancel.ico";
            }
            else
            {
                _ikona = @"images/Check.ico";
            }
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(_ikona, UriKind.Relative);
            logo.EndInit();

            return logo;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajPredvidjenoVremeMinutaConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int _predvidjenoVremeMinuta = 0;

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                EntitySet<DB.StavkaUsluga> _stavkaUslugaLista = (EntitySet<DB.StavkaUsluga>)values[0];

                if (_stavkaUslugaLista.Count > 0)
                {
                    foreach (DB.StavkaUsluga item in _stavkaUslugaLista)
                    {
                        DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = item.RadniNalogStavkaUsluga;

                        if (_radniNalogStavkaUsluga != null)
                        {
                            if (_radniNalogStavkaUsluga.Status.ToString() != "D")
                            {
                                _predvidjenoVremeMinuta += _radniNalogStavkaUsluga.PredvidjenoVremeMinuta;
                            }
                        }
                    }
                } 
            }

            return _predvidjenoVremeMinuta > 0 ? _predvidjenoVremeMinuta.ToString() : "0";

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajMotorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _motorLista = "";
            bool _prviMotor = true;

            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                EntitySet<DB.VezaTipAutomobilaMotor> _vezaTipAutomobilaMotorLista = (EntitySet<DB.VezaTipAutomobilaMotor>)values[0];

                

                if (_vezaTipAutomobilaMotorLista.Count > 0)
                {
                    foreach (DB.VezaTipAutomobilaMotor item in _vezaTipAutomobilaMotorLista)
                    {
                        if (_prviMotor)
                        {
                            _motorLista += item.Motor.Oznaka;
                            _prviMotor = false;
                        }
                        else
                        {
                            _motorLista += "\n" + item.Motor.Oznaka;
                        }
                    }
                }
            }

            return _motorLista;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajIzvorZaStavkaUslugaConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                return ((EntitySet<DB.StavkaUsluga>)values[0]).Where(f => f.Status != 'D');
            }
            else
            {
                return new EntitySet<DB.StavkaUsluga>();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    public class DajIzvorZaStavkaArtikalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                return ((EntitySet<DB.StavkaArtikal>)values[0]).Where(f => f.Status != 'D');
            }
            else
            {
                return new EntitySet<DB.StavkaArtikal>();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] _rezultat = new string[] { (string)value };
            return _rezultat;
        }

    }

    [ValueConversion(typeof(string), typeof(string))]
    public class TelefonMaskKonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                if (parameter != null)
                {
                    char[] _maska = parameter.ToString().ToCharArray();
                    char[] _telefon = value.ToString().ToCharArray();
                    //

                    for (int i = 0; i < _maska.Length; i++)
                    {
                        if (!_maska[i].Equals('_'))
                        {
                            _maska[i] = ' ';
                            break;
                        }
                    }


                    foreach (char t in _telefon)
                    {
                        for (int i = 0; i < _maska.Length; i++)
                        {
                            if (_maska[i].Equals('_'))
                            {
                                _maska[i] = t;
                                break;
                            }
                        }
                    }

                    return new string(_maska);

                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
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

    [ValueConversion(typeof(string), typeof(string))]
    public class TelefonMaskKonverterReadOnly : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != DependencyProperty.UnsetValue)
            {
                if (parameter != null)
                {
                    char[] _maska = parameter.ToString().ToCharArray();
                    char[] _telefon = value.ToString().ToCharArray();
                    //

                    foreach (char t in _telefon)
                    {
                        for (int i = 0; i < _maska.Length; i++)
                        {
                            if (_maska[i].Equals('_'))
                            {
                                _maska[i] = t;
                                break;
                            }
                        }
                    }

                    for (int i = _maska.Length - 1; i >= 0; i--)
                    {
                        if (!Char.IsNumber(_maska[i]) && !Char.IsLetter(_maska[i]))
                        {
                            _maska[i] = '_';

                        }
                        else
                        {
                            break;
                        }
                    }

                    return new string(_maska).Replace("_", "");

                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
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

}
