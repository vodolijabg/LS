using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AutoServis
{
    public class ObavezanPodatakRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((string)value).Trim().Equals(""))
            {
                return new ValidationResult(false, "Obavezan podatak.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }

    public class ObavezanPodatakTagRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value==null)
            {
                return new ValidationResult(false, "Obavezan podatak.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }

    public class ObavezanPodatakListaRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Obavezan podatak.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }

    public class IsIntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    Int32 _vrednost;

                    if (!((string)value).Length.Equals(0))
                    {
                        _vrednost = Int32.Parse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    }

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            catch
            {
                return new ValidationResult(false, "Vrednost mora biti broj.");
            }
        }
    }

    public class IsBigintRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    Int64 _vrednost;

                    if (!((string)value).Length.Equals(0))
                    {
                        _vrednost = Int64.Parse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    }

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            catch
            {
                return new ValidationResult(false, "Vrednost mora biti broj.");
            }
        }
    }

    public class IsDateTimeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    DateTime _vrednost;

                    if (!((string)value).Length.Equals(0))
                    {
                        _vrednost = DateTime.Parse((string)value, CultureInfo.CurrentCulture);
                    }

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            catch
            {
                return new ValidationResult(false, "Vrednost mora biti datum.");
            }
        }
    }

    public class IsDecimalRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    Decimal _vrednost;

                    if (!((string)value).Length.Equals(0))
                    {
                        _vrednost = Decimal.Parse((string)value, CultureInfo.CurrentCulture);
                    }

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            catch
            {
                return new ValidationResult(false, "Vrednost mora biti decimalan broj, 16 brojeva plus 2 decimale.");
            }
        }
    }

    public class IsVeciOdNuleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null)
                {
                    Int32 _vrednost;

                    if (!((string)value).Length.Equals(0))
                    {
                        _vrednost = Int32.Parse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture);

                        if (_vrednost > 0)
                        {
                            return ValidationResult.ValidResult;
                        }
                        else
                        {
                            return new ValidationResult(false, "Vrednost mora biti veca od nule.");
                        }

                    }
                    else
                    {
                        return ValidationResult.ValidResult;
                    }

                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            catch
            {
                return new ValidationResult(false, "Vrednost mora biti broj.");
            }
        }
    }

}
