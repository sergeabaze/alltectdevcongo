using System;
using System.Windows.Controls;
using System.Globalization;
using AllTech.FrameWork.Global;


namespace AllTech.FrameWork.ValidationRules
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime result;
            if ((true == DateTime.TryParse(value.ToString(), out result)))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "This value must be DateTime.");
        }
    }

    public class DateValidationRulefact : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime result;
            if ((true == DateTime.TryParse(value.ToString(), out result)))
            {
                if (GlobalDatas.dataBasparameter.JourLimiteFacturation > 0)
                {
                    if (result.Month >= DateTime.Now.Month && result.Year == DateTime.Now.Year)
                    {
                        return ValidationResult.ValidResult;
                    }
                    else
                    {
                        int jourMois = DateTime.Now.Day;
                        if (jourMois < GlobalDatas.dataBasparameter.JourLimiteFacturation)
                            return ValidationResult.ValidResult;
                    }
                }else
                    return ValidationResult.ValidResult;
               
               
            }
            return new ValidationResult(false, "Unable to create this pecifica invoice.");

             //DateTime ndated = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
             //   int jourMois = ndated.Day;
             //   if (jourMois < GlobalDatas.dataBasparameter.JourLimiteFacturation)
             //   {
             //       return ValidationResult.ValidResult;
             //   }
             //   return new ValidationResult(false, "Unable to create this pecifica invoice.");
            //if ((true == DateTime.TryParse(value.ToString(), out result)))
            //{
            //    return ValidationResult.ValidResult;
            //}
            //return new ValidationResult(false, "This value must be DateTime.");
        }
    }
}
