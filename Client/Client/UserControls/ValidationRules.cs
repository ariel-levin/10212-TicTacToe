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

    class IntValidationRule : ValidationRule
    {
        private bool nullable, isBit;

        public IntValidationRule(bool isBit, bool nullable)
        {
            this.isBit = isBit;
            this.nullable = nullable;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null && !nullable)
                return new ValidationResult(false, "The value can not be empty");
            else if (value != null)
            {
                if (value.ToString().Length == 0 && nullable)
                    return new ValidationResult(true, null);
                int tmp;
                if (!int.TryParse(value.ToString(), out tmp))
                    return new ValidationResult(false, "'" + value.ToString() + "' is not an integer number");
                else if (isBit && tmp != 0 && tmp != 1)
                    return new ValidationResult(false, "'" + value.ToString() + "' is not 0 or 1");
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
            Regex reg = new Regex("^([0-9]+-)*[0-9]*$");
            String str = value.ToString();

            if (str.Length == 0 && !nullable)
                return new ValidationResult(false, "The value can not be empty");
            else if (!reg.IsMatch(str) && str.Length != 0)
                return new ValidationResult(false, "'" + value.ToString() + "' does not match a phone number pattern");
            else
                return new ValidationResult(true, null);
        }
    }

}
