using System;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;


namespace AllTech.FrameWork.ValidationRules
{
    public class NumericValidationRule : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
          
            Regex pattern = new Regex("[0-9]");

           

            if (value == null || !pattern.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, "This value must be numeric");
            }
            else
            {
                return new ValidationResult(true, null);
            }
           
            //ValidationResult result = new ValidationResult(true, null);

            //if ((false  == Int32.TryParse(value.ToString(), out intResult)) ||
            //    (false  == Double.TryParse(value.ToString(), out doubleResult)))
            //{
               
            //    //return ValidationResult.ValidResult;
            //   // return new ValidationResult(false, "This value must be numeric.");
            //    result = new ValidationResult(false, this.ErrorMessage);
            //}

            //if (string.IsNullOrEmpty(value.ToString()))
            //{
            //    //return new ValidationResult(false, " Please enter a value");
            //   // result = "This value must be numeric.";
            //    result = new ValidationResult(false, this.ErrorMessage);
            //}

            //if (intResult == 0 || doubleResult == 0)
            //    result = new ValidationResult(false, "This value must be numeric.");

            //return result;
        }
    }

    public class StringSizeValidationRule : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 18)
            {
                return new ValidationResult(false, "The Size of field alowed less 18");
            }

            return new ValidationResult(true, null);
        }
    }
}
