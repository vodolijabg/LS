using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace Servis
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
                        _vrednost = Int64.Parse((string)value, NumberStyles.Any, App.cultureInfo);
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
}
