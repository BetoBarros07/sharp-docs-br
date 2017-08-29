using System.ComponentModel.DataAnnotations;

namespace O7.SharpDocsBR
{
    /// <summary>
    /// Attribute to validate CPF.
    /// </summary>
    public class CPFValidationAttribute : ValidationAttribute
    {
        private readonly bool _excludeNonNumericCharacters;

        /// <summary>
        /// Used when you receive a CPF like "1234567809".
        /// </summary>
        public CPFValidationAttribute()
        {
            _excludeNonNumericCharacters = false;
        }

        /// <summary>
        /// Used when you receive a CPF like "123.456.789-09".
        /// </summary>
        /// <param name="ExcludeNonNumericCharacters">If you want to remove all the special characters, set to true.</param>
        public CPFValidationAttribute(bool ExcludeNonNumericCharacters)
        {
            _excludeNonNumericCharacters = ExcludeNonNumericCharacters;
        }

        public override bool IsValid(object value)
        {
            if (value is string strValue)
            {
                if (_excludeNonNumericCharacters)
                    strValue = strValue.Replace(".", "").Replace("-", "");

                if (strValue.Length == 11)
                {
                    if (long.TryParse(strValue, out var longValue))
                    {
                        var countDigit1 = 0;
                        var strIndex = 0;
                        for (byte i = 10; i > 1; i--)
                            countDigit1 += byte.Parse(strValue[strIndex++].ToString()) * i;
                        var verificationDigit1 = (countDigit1 * 10) % 11;
                        if (verificationDigit1 == 10)
                            verificationDigit1 = 0;
                        if (int.Parse(strValue[9].ToString()) == verificationDigit1)
                        {
                            var countDigit2 = 0;
                            strIndex = 0;
                            for (byte i = 11; i > 1; i--)
                                countDigit2 += byte.Parse(strValue[strIndex++].ToString()) * i;
                            var verificationDigit2 = (countDigit2 * 10) % 11;
                            if (verificationDigit2 == 10)
                                verificationDigit2 = 0;
                            return int.Parse(strValue[10].ToString()) == verificationDigit2;
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