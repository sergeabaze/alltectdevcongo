using System;
using System.Windows.Controls;
using System.Globalization;

namespace AllTech.FrameWork.ValidationRules
{
    public class NotNullOrEmptyValidationRule : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (true == string.IsNullOrWhiteSpace(value as string))
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            return ValidationResult.ValidResult;
        }
    }
}
