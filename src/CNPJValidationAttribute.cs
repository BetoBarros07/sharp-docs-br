using System.ComponentModel.DataAnnotations;

namespace O7.SharpDocsBR
{
    /// <summary>
    /// Attribute to validate CNPJ.
    /// </summary>
    public class CNPJValidationAttribute : ValidationAttribute
    {
        private readonly bool _excludeNonNumericCharacters;

        /// <summary>
        /// Used when you receive a CNPJ like "33545831000106".
        /// </summary>
        public CNPJValidationAttribute()
        {
            _excludeNonNumericCharacters = false;
        }

        /// <summary>
        /// Used when you receive a CPF like "33.545.831/0001-06".
        /// </summary>
        /// <param name="ExcludeNonNumericCharacters">If you want to remove all the special characters, set to true.</param>
        public CNPJValidationAttribute(bool ExcludeNonNumericCharacters)
        {
            _excludeNonNumericCharacters = ExcludeNonNumericCharacters;
        }

        public override bool IsValid(object value)
        {
            if (value is string strValue)
            {
                if (_excludeNonNumericCharacters)
                    strValue = strValue.Replace("/", "").Replace(".", "").Replace("-", "");

                if (strValue.Length == 14)
                {
                    if (long.TryParse(strValue, out var longValue))
                    {
                        var countDigit1 = 0;
                        var multiplier = 2;
                        var index = 11;
                        while (index > -1)
                        {
                            countDigit1 += byte.Parse(strValue[index].ToString()) * multiplier++;
                            if (multiplier > 9)
                            {
                                multiplier = 2;
                            }
                            index--;
                        }
                        var verificationDigit1 = countDigit1 % 11;
                        if (verificationDigit1 == 1 || verificationDigit1 == 0)
                            verificationDigit1 = 0;
                        else
                            verificationDigit1 = 11 - verificationDigit1;
                        if (verificationDigit1 == int.Parse(strValue[12].ToString()))
                        {
                            var countDigit2 = 0;
                            multiplier = 2;
                            index = 12;
                            while (index > -1)
                            {
                                countDigit2 += byte.Parse(strValue[index].ToString()) * multiplier++;
                                if (multiplier > 9)
                                {
                                    multiplier = 2;
                                }
                                index--;
                            }
                            var verificationDigit2 = countDigit2 % 11;
                            if (verificationDigit2 == 1 || verificationDigit2 == 0)
                                verificationDigit2 = 0;
                            else
                                verificationDigit2 = 11 - verificationDigit2;
                            return verificationDigit2 == byte.Parse(strValue[13].ToString());
                        }
                    }
                }
                FormatErrorMessage(ErrorMessage);
                return false;
            }
            return true;
        }
    }
}