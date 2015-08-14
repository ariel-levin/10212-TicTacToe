using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Client
{
    class CharValidationRule : ValidationRule
    {
        private bool nullable;

        public CharValidationRule(bool nullable = false)
        {
            this.nullable = nullable;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg = new Regex("^[A-Z][a-z]*([ ][A-Z][a-z]*)*$");
            String str = value.ToString();

            if (str.Length == 0 && !nullable)
                return new ValidationResult(false, "The value can not be empty");
            else if (!reg.IsMatch(str) && str.Length != 0)
                return new ValidationResult(false, "'" + value.ToString() + "' does not contain only chars");
            else
                return new ValidationResult(true, null);
        }
    }

    //class StringCellDataInfoValidationRule : ValidationRule
    //{
    //    private bool nullable;

    //    public StringCellDataInfoValidationRule(bool nullable = false)
    //    {
    //        this.nullable = nullable;
    //    }

    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        Regex reg = new Regex("^[A-Z][a-z]*([ ][A-Z][a-z]*)*$");

    //        String str = value.ToString();

    //        if (str.Length == 0 && !nullable)
    //        {
    //            return new ValidationResult(false, "The value can not be empty");
    //        }
    //        else if (!reg.IsMatch(str) && str.Length != 0)
    //        {
    //            return new ValidationResult(false, "'" + value.ToString() + "' is not a whole chars.");
    //        }
    //        else
    //            return new ValidationResult(true, null);
    //    }
    //}

    class IntValidationRule : ValidationRule
    {
        private bool nullable;

        public IntValidationRule(bool nullable = false)
        {
            this.nullable = nullable;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null && !nullable)
                return new ValidationResult(false, "The value can not be empty");
            else if (value != null)
            {
                int tmp;
                if (!int.TryParse(value.ToString(), out tmp))
                    return new ValidationResult(false, "'" + value.ToString() + "' is not an integer number");
            }
            return new ValidationResult(true, null);
        }
    }

    class PhoneValidationRule : ValidationRule
    {
        private bool nullable;

        public PhoneValidationRule(bool nullable = false)
        {
            this.nullable = nullable;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg = new Regex("^([0-9]+-)*[0-9]$");
            String str = value.ToString();

            if (str.Length == 0 && !nullable)
                return new ValidationResult(false, "The value can not be empty");
            else if (!reg.IsMatch(str) && str.Length != 0)
                return new ValidationResult(false, "'" + value.ToString() + "' does not match a phone number pattern");
            else
                return new ValidationResult(true, null);
        }
    }

    //class DateValidationRule : ValidationRule
    //{
    //    private bool nullable;

    //    public DateValidationRule(bool nullable = false)
    //    {
    //        this.nullable = nullable;
    //    }

    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        Regex reg = new Regex("^[0-3][0-9]/[01][0-9]/[0-2][0-9]{3}");
    //        String str = value.ToString();

    //        if (str.Length == 0 && !nullable)
    //            return new ValidationResult(false, "The value can not be empty");
    //        else if (!reg.IsMatch(str) && str.Length != 0)
    //            return new ValidationResult(false, "The value must be in the format: dd/MM/yyyy hh:mm:ss");
    //        else
    //            return new ValidationResult(true, null);
    //    }
    //}

}
