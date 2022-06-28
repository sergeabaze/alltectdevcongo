using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AllTech.FrameWork.Utils
{
   public  class UserInterfaceUtilities
    {
       public static bool ValidateVisualTree(DependencyObject parent)
       {
           bool isValid = true;

           LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();

           // Check all of the localValues bindings for validation rules.
           while (localValues.MoveNext())
           {
               LocalValueEntry entry = localValues.Current;

               // If validation has already been run and found to have failed, then
               // the failing DP will be UnsetValue, but Validation.HasErrors will
               // be set to true.
               if ((Validation.HasErrorProperty == entry.Property) &&
                   (true == (bool)parent.GetValue(Validation.HasErrorProperty)))
               {
                   isValid = false;
                   continue;
               }

               // Ignore this entry if there aren't any bindings.
               if (false == BindingOperations.IsDataBound(parent, entry.Property))
               {
                   continue;
               }

               // Get the validation rules from the binding.
               Binding binding = BindingOperations.GetBinding(parent, entry.Property);
               if (null != binding)
               {
                   foreach (ValidationRule rule in binding.ValidationRules)
                   {
                       // Call each rule.
                       ValidationResult result = rule.Validate(
                           parent.GetValue(entry.Property), null);
                       if (false == result.IsValid)
                       {
                           BindingExpression expression =
                               BindingOperations.GetBindingExpression(parent, entry.Property);
                           Validation.MarkInvalid(expression,
                               new ValidationError(rule, expression, result.ErrorContent, null));
                           isValid = false;
                       }
                   }
               }
           }

           // Validate all of the bindings on the children of the supplied DependencyObject.
           for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
           {
               if (false == ValidateVisualTree(VisualTreeHelper.GetChild(parent, i)))
               {
                   isValid = false;
               }
           }

           return isValid;
       }        
    }
}
