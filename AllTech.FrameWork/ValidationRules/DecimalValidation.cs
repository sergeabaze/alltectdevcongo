using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AllTech.FrameWork.ValidationRules
{
    public class DecimalValidation : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
          
            double  decimalresult;

            ValidationResult result = new ValidationResult(true ,null );
            string inputresult =(value ??string .Empty ).ToString ();

            if (false ==double.TryParse(inputresult, out decimalresult))
            {
               // return ValidationResult.ValidResult;
                result = new ValidationResult(false ,this.ErrorMessage );
            }

            return result;
           
        }
    }
}
