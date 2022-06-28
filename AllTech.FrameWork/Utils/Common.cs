using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AllTech.FrameWork.Utils
{
   public  class Common
    {
        #region FORMAT DOUBLE
        
      

       

        public static double GetDoule(string input)
        {
            // unify string (no spaces, only . )
            string output;
            output = input.Trim().Replace(" ", "").Replace(",", ".");

            // split it on points
            string[] split = output.Split('.');

            if (split.Count() > 1)
            {
                // take all parts except last
                output = string.Join("", split.Take(split.Count() - 1).ToArray());

                // combine token parts with last part
                output = string.Format("{0}.{1}", output, split.Last());
            }

            // parse double invariant
            double d = double.Parse(output, CultureInfo.InvariantCulture);
            return d;
        }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;
            string output;

            // check if last seperator==groupSeperator
            string groupSep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            if (value.LastIndexOf(groupSep) + 4 == value.Count())
            {
                bool tryParse = double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out result);
                result = tryParse ? result : defaultValue;
            }
            else
            {
                // unify string (no spaces, only . )     
                output = value.Trim().Replace(" ", string.Empty).Replace(",", ".");

                // split it on points     
                string[] split = output.Split('.');

                if (split.Count() > 1)
                {
                    // take all parts except last         
                    output = string.Join(string.Empty, split.Take(split.Count() - 1).ToArray());

                    // combine token parts with last part         
                    output = string.Format("{0}.{1}", output, split.Last());
                }
                // parse double invariant     
                result = double.Parse(output, System.Globalization.CultureInfo.InvariantCulture);
            }
            return result;
        }


        private string FormatResult(string vResult)
        {
            string output;
            string input = vResult;

            // unify string (no spaces, only . ) 
            output = input.Trim().Replace(" ", "").Replace(",", ".");

            // split it on points 
            string[] split = output.Split('.');

            if (split.Count() > 1)
            {
                // take all parts except last 
                output = string.Join("", split.Take(split.Count() - 1).ToArray());

                // combine token parts with last part 
                output = string.Format("{0}.{1}", output, split.Last());
            }
            string sfirst = output.Substring(0, 1);

            try
            {
                if (sfirst == "<" || sfirst == ">")
                {
                    output = output.Replace(sfirst, "");
                    double res = Double.Parse(output);
                    return String.Format("{1}{0:0.####}", res, sfirst);
                }
                else
                {
                    double res = Double.Parse(output);
                    return String.Format("{0:0.####}", res);
                }
            }
            catch
            {
                return output;
            }
        }
        #endregion

    }


   #region FORMAT NUMBER TO ENGLISH
   public class NumberToEnglish
   {
       public static String changeNumericToWords(double numb)
       {
           String num = numb.ToString();
           return changeToWords(num, false);
       }
       public static String changeCurrencyToWords(String numb)
       {
           return changeToWords(numb, true);
       }
       public static String changeNumericToWords(String numb)
       {
           string returNumber=changeToWords(numb, false);
           returNumber = returNumber.ToLower();
           char[] avaleurNbre = returNumber.TrimStart().ToCharArray();
           avaleurNbre[0] = char.ToUpper(avaleurNbre[0]);

           return new string(avaleurNbre);
           
       }
       public static String changeCurrencyToWords(double numb)
       {
           return changeToWords(numb.ToString(), true);
       }
       private static String changeToWords(String numb, bool isCurrency)
       {
           String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
           String endStr = (isCurrency) ? ("Only") : ("");
           try
           {
               int decimalPlace = numb.IndexOf(".");
               if (decimalPlace > 0)
               {
                   wholeNo = numb.Substring(0, decimalPlace);
                   points = numb.Substring(decimalPlace + 1);
                   if (Convert.ToInt32(points) > 0)
                   {
                       andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents
                       endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                       pointStr = translateCents(points);
                   }
               }
               val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
           }
           catch { ;}
           return val;
       }
       private static String translateWholeNumber(String number)
       {
           string word = "";
           try
           {
               bool beginsZero = false;//tests for 0XX
               bool isDone = false;//test if already translated
               double dblAmt = (Convert.ToDouble(number));
               //if ((dblAmt > 0) && number.StartsWith("0"))
               if (dblAmt > 0)
               {//test for zero or digit zero in a nuemric
                   beginsZero = number.StartsWith("0");

                   int numDigits = number.Length;
                   int pos = 0;//store digit grouping
                   String place = "";//digit grouping name:hundres,thousand,etc...
                   switch (numDigits)
                   {
                       case 1://ones' range
                           word = ones(number);
                           isDone = true;
                           break;
                       case 2://tens' range
                           word = tens(number);
                           isDone = true;
                           break;
                       case 3://hundreds' range

                           pos = (numDigits % 3) + 1;
                           if (pos == 1 && number.StartsWith("0"))
                               place = "";
                           else
                               place = " Hundred ";
                           break;
                       case 4:
                       case 5:
                       case 6:
                           pos = (numDigits % 4) + 1;
                           place = " Thousand ";
                           break;
                       case 7:
                       case 8:
                       case 9:
                           pos = (numDigits % 7) + 1;
                           place = " Million ";
                           break;
                       case 10://Billions's range
                           pos = (numDigits % 10) + 1;
                           place = " Billion ";
                           break;
                       //add extra case options for anything above Billion...
                       default:
                           isDone = true;
                           break;
                   }
                   if (!isDone)
                   {//if transalation is not done, continue...(Recursion comes in now!!)
                       word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                       //check for trailing zeros
                       if (beginsZero) if (!word.StartsWith("and")) word = " and " + word.Trim();
                   }
                   //ignore digit grouping names
                   if (word.Trim().Equals(place.Trim())) word = "";
               }
           }
           catch { ;}
           return word.Trim();
       }
       private static String tens(String digit)
       {
           int digt = Convert.ToInt32(digit);
           String name = null;
           switch (digt)
           {
               case 10:
                   name = "Ten";
                   break;
               case 11:
                   name = "Eleven";
                   break;
               case 12:
                   name = "Twelve";
                   break;
               case 13:
                   name = "Thirteen";
                   break;
               case 14:
                   name = "Fourteen";
                   break;
               case 15:
                   name = "Fifteen";
                   break;
               case 16:
                   name = "Sixteen";
                   break;
               case 17:
                   name = "Seventeen";
                   break;
               case 18:
                   name = "Eighteen";
                   break;
               case 19:
                   name = "Nineteen";
                   break;
               case 20:
                   name = "Twenty";
                   break;
               case 30:
                   name = "Thirty";
                   break;
               case 40:
                   name = "Forty";
                   break;
               case 50:
                   name = "Fifty";
                   break;
               case 60:
                   name = "Sixty";
                   break;
               case 70:
                   name = "Seventy";
                   break;
               case 80:
                   name = "Eighty";
                   break;
               case 90:
                   name = "Ninety";
                   break;
               default:
                   if (digt > 0)
                   {
                       name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                   }
                   break;
           }
           return name;
       }
       private static String ones(String digit)
       {
           int digt = Convert.ToInt32(digit);
           String name = "";
           switch (digt)
           {
               case 1:
                   name = "One";
                   break;
               case 2:
                   name = "Two";
                   break;
               case 3:
                   name = "Three";
                   break;
               case 4:
                   name = "Four";
                   break;
               case 5:
                   name = "Five";
                   break;
               case 6:
                   name = "Six";
                   break;
               case 7:
                   name = "Seven";
                   break;
               case 8:
                   name = "Eight";
                   break;
               case 9:
                   name = "Nine";
                   break;
           }
           return name;
       }
       private static String translateCents(String cents)
       {
           String cts = "", digit = "", engOne = "";
           for (int i = 0; i < cents.Length; i++)
           {
               digit = cents[i].ToString();
               if (digit.Equals("0"))
               {
                   engOne = "Zero";
               }
               else
               {
                   engOne = ones(digit);
               }
               cts += " " + engOne;
           }
           return cts;
       }
   }
   #endregion

   #region FORMAT NUMBER TO FRENCH

   public class NombreEnLettre
   {


       public static String changerNombrelettre(String numb)
       {


           string valeurNbre = changeToWords(numb, false);

           if (valeurNbre.Trim().ToLower().Trim().EndsWith("un"))
               valeurNbre = valeurNbre.Substring(0, valeurNbre.ToLower().LastIndexOf("un")).Replace("un", "") + "Un";
           else
               valeurNbre = valeurNbre.ToLower ().Replace("un", "");

           if (valeurNbre.ToLower().Trim().StartsWith("million") || valeurNbre.ToLower().Trim().StartsWith("milliard"))
               valeurNbre = "Un " + valeurNbre;





           if (valeurNbre.ToLower().IndexOf("mille") > 1)
           {
               int value = 0;
               int taille = numb.Length;
               string sub = numb.Substring(0, 2);

               value = int.Parse(numb[1].ToString());

               if (taille == 5)
               {
                   value = int.Parse(numb[1].ToString());
                   if (value == 1 && int.Parse(numb.Substring(0, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
               }
               if (taille == 6)
               {
                   value = int.Parse(numb[2].ToString());

                   if (value == 1 && int.Parse(numb.Substring(1, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
               }
               if (taille == 7)
               {
                   value = int.Parse(numb[3].ToString());

                   if (value == 1 && int.Parse(numb.Substring(2, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
               }
               if (taille == 8)
               {
                   int firstvalue = int.Parse(numb[1].ToString());
                   value = int.Parse(numb[4].ToString());

                   if (firstvalue == 1 && int.Parse(numb.Substring(0, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("million") - 1), "Un");


                   if (value == 1 && int.Parse(numb.Substring(3, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
               }
               if (taille == 9)
               {
                   int firstvalue = int.Parse(numb[2].ToString());
                   value = int.Parse(numb[5].ToString());

                   if (firstvalue == 1 && int.Parse(numb.Substring(1, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("million") - 1), "Un");
                   if (value == 1 && int.Parse(numb.Substring(4, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");

               }
               if (taille == 10)
               {
                   int firstvalue = int.Parse(numb[3].ToString());
                   value = int.Parse(numb[6].ToString());

                   if (firstvalue == 1 && int.Parse(numb.Substring(2, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("million") - 1), "Un");
                   if (value == 1 && int.Parse(numb.Substring(5, 2)) != 11)
                       valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
               }
               if (taille == 11)
               {
                   int firstvalue = int.Parse(numb[2].ToString());
                   int secodevalue = int.Parse(numb[5].ToString());
                   value = int.Parse(numb[8].ToString());
               }
               if (taille == 12)
               {
                   int firstvalue = int.Parse(numb[1].ToString());
                   int secodevalue = int.Parse(numb[4].ToString());
                   value = int.Parse(numb[7].ToString());
               }

               //if (value==1)
               //valeurNbre = valeurNbre.Insert((valeurNbre.ToLower().LastIndexOf("mille") - 1), "Un");
           }


           if ((valeurNbre.Contains("million") || valeurNbre.Contains("Million")) && !valeurNbre.StartsWith("Un"))
               valeurNbre = valeurNbre.Replace("million", "millions").Replace("Million","millions") ;


           valeurNbre = valeurNbre.ToLower();
           char[] avaleurNbre = valeurNbre.TrimStart().ToCharArray();
           avaleurNbre[0] = char.ToUpper(avaleurNbre[0]);

           return new string(avaleurNbre);
       }

       private static String changeToWords(String numb, bool isCurrency)
       {
           String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
           String endStr = (isCurrency) ? ("Only") : ("");

           try
           {
               int decimalPlace = numb.IndexOf(",");
               if (decimalPlace > 0)
               {
                   // si de décimal
                   wholeNo = numb.Substring(0, decimalPlace);
                   points = numb.Substring(decimalPlace + 1);
                   if (Convert.ToInt32(points) > 0)
                   {
                       andStr = (isCurrency) ? ("virgule") : ("virgule");// just to separate whole numbers from points/cents
                       endStr = (isCurrency) ? ("Cent " + endStr) : ("");
                       pointStr = translateCents(points);
                   }
               }
               val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo), andStr, pointStr, endStr); ;
           }
           catch
           {
               ;

           }

           return val;
       }

       private static String translateWholeNumber(String number)
       {
           string word = "";
           try
           {
               bool beginsZero = false;//tests for 0XX
               bool isDone = false;//test if already translated
               double dblAmt = (Convert.ToDouble(number));
               if (dblAmt > 0)
               {//test for zero or digit zero in a nuemric
                   beginsZero = number.StartsWith("0");
                   int numDigits = number.Length;
                   int pos = 0;//store digit grouping
                   String place = "";//digit grouping name:hundres,thousand,etc...
                   switch (numDigits)
                   {
                       case 1://ones' range
                           word = ones(number);
                           isDone = true;
                           break;
                       case 2://tens' range
                           word = tens(number);
                           isDone = true;
                           break;
                       case 3://hundreds' range
                           pos = (numDigits % 3) + 1;
                           if (pos == 1 && number.StartsWith("0"))
                               place = "";
                           else
                               place = " Cent ";
                           break;
                       case 4://thousands' range
                       case 5:
                       case 6:
                           pos = (numDigits % 4) + 1;
                           place = " Mille ";
                           break;
                       case 7://millions' range
                       case 8:
                       case 9:
                           pos = (numDigits % 7) + 1;
                           if (pos == 1 && number.StartsWith("1"))
                               place = " Million ";
                           else
                               place = " Million ";
                           break;
                       case 10://Billions's range
                           pos = (numDigits % 10) + 1;
                           place = " Milliard ";
                           break;
                       //add extra case options for anything above Billion...
                       default:
                           isDone = true;
                           break;
                   }
                   if (!isDone)
                   {//if transalation is not done, continue...(Recursion comes in now!!)
                       word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                       //check for trailing zeros
                       if (beginsZero) word = "" + word.Trim();
                   }
                   //ignore digit grouping names
                   if (word.Trim().Equals(place.Trim())) word = "";
               }
           }
           catch { ;}

           return word.Trim();
       }

       private static String tens(String digit)
       {
           int digt = Convert.ToInt32(digit);
           String name = null;
           switch (digt)
           {
               case 10:
                   name = "Dix";
                   break;
               case 11:
                   name = "Onze";
                   break;
               case 12:
                   name = "Douze";
                   break;
               case 13:
                   name = "Treize";
                   break;
               case 14:
                   name = "Quatorze";
                   break;
               case 15:
                   name = "Quinze";
                   break;
               case 16:
                   name = "Seize";
                   break;
               case 17:
                   name = "Dix-sept";
                   break;
               case 18:
                   name = "Dix-huit";
                   break;
               case 19:
                   name = "Dix-neuf";
                   break;
               case 20:
                   name = "Vingt";
                   break;
               case 30:
                   name = "Trente";
                   break;
               case 40:
                   name = "Quarante";
                   break;
               case 50:
                   name = "Cinquante";
                   break;
               case 60:
                   name = "Soixante";
                   break;
               case 70:
                   name = "Soixante-dix";
                   break;
               case 80:
                   name = "Quatre-vingt";
                   break;
               case 90:
                   name = "Quatre-vingt-dix";
                   break;
               default:
                   if (digt > 0)
                   {
                       //name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));

                       if (digit.Substring(0, 1).Contains("7") || digit.Substring(0, 1).Contains("9"))
                       {
                           name = tens((int.Parse(digit.Substring(0, 1)) - 1).ToString() + "0") + " " + tens((int.Parse(digit.Substring(1)) + 10).ToString());
                       }
                       // else name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                       else name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit);
                   }
                   break;
           }
           return name;
       }

       private static String translateCents(String cents)
       {
           String cts = "", digit = "", engOne = "";
           for (int i = 0; i < cents.Length; i++)
           {
               digit = cents[i].ToString();
               if (digit.Equals("0"))
               {
                   engOne = "";
               }
               else
               {
                   engOne = ones(digit);
               }
               cts += " " + engOne;
           }
           return cts;
       }

       private static String ones(String digit)
       {
           int digt = 0;

           if (digit.Length > 1)
               digt = Convert.ToInt32(digit.Substring(1));
           else digt = Convert.ToInt32(digit);


           String name = "";

           switch (digt)
           {
               case 1:

                   name = "Un";
                   break;
               case 2:
                   name = "Deux";
                   break;
               case 3:
                   name = "Trois";
                   break;
               case 4:
                   name = "Quatre";
                   break;
               case 5:
                   name = "Cinq";
                   break;
               case 6:
                   name = "Six";
                   break;
               case 7:
                   name = "Sept";
                   break;
               case 8:
                   name = "Huit";
                   break;
               case 9:
                   name = "Neuf";
                   break;


           }
           return name;
       }


       public static string testeConvertion(float chiffre)
       {

           int centaine, dizaine, unite, reste, y;
           bool dix = false;
           string lettre = "";
           //strcpy(lettre, "");

           reste = (int)(chiffre / 1);

           for (int i = 1000000000; i >= 1; i /= 1000)
           {
               y = reste / i;
               if (y != 0)
               {
                   centaine = y / 100;
                   dizaine = (y - centaine * 100) / 10;
                   unite = y - (centaine * 100) - (dizaine * 10);
                   switch (centaine)
                   {
                       case 0:
                           break;
                       case 1:
                           {
                               lettre += "cent ";
                               break;
                           }
                       case 2:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "deux-cents ";
                               else lettre += "deux-cents ";
                               break;
                           }
                       case 3:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "trois-cents ";
                               else lettre += "trois-cents ";
                               break;
                           }
                       case 4:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "quatre-cents ";
                               else lettre += "quatre-cents ";
                               break;
                           }
                       case 5:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "cinq-cent ";
                               else lettre += "cinq-cents ";
                               break;
                           }
                       case 6:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "six-cents ";
                               else lettre += "six-cents ";
                               break;
                           }
                       case 7:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "sept-cents ";
                               else lettre += "sept-cents ";
                               break;
                           }
                       case 8:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "huit-cents ";
                               else lettre += "huit-cents ";
                               break;
                           }


                       case 9:
                           {
                               if ((dizaine == 0) && (unite == 0)) lettre += "neuf-cents ";
                               else lettre += "neuf-cents ";
                               break;
                           }
                   }// endSwitch(centaine)

                   switch (dizaine)
                   {
                       case 0:
                           break;
                       case 1:
                           dix = true;
                           break;
                       case 2:
                           lettre += "vingt ";
                           break;
                       case 3:
                           lettre += "trente ";
                           break;
                       case 4:
                           lettre += "quarante ";
                           break;
                       case 5:
                           lettre += "cinquante ";
                           break;
                       case 6:
                           lettre += "soixante ";
                           break;
                       case 7:
                           dix = true;
                           lettre += "soixante ";
                           break;
                       case 8:
                           lettre += "quatre-vingt ";
                           break;
                       case 9:
                           dix = true;
                           lettre += "quatre-vingt-dix ";
                           break;

                   } // endSwitch(dizaine)

                   switch (unite)
                   {
                       case 0:
                           if (dix) lettre += "dix ";
                           break;
                       case 1:
                           if (dix) lettre += "onze ";
                           else lettre += "un ";
                           break;
                       case 2:
                           if (dix) lettre += "douze ";
                           else lettre += "deux ";
                           break;
                       case 3:
                           if (dix) lettre += "treize ";
                           else lettre += "trois ";
                           break;
                       case 4:
                           if (dix) lettre += "quatorze ";
                           else lettre += "quatre ";
                           break;
                       case 5:
                           if (dix) lettre += "quinze ";
                           else lettre += "cinq ";
                           break;
                       case 6:
                           if (dix) lettre += "seize ";
                           else lettre += "six ";
                           break;
                       case 7:
                           if (dix) lettre += "dix-sept ";
                           else lettre += "sept ";
                           break;
                       case 8:
                           if (dix) lettre += "dix-huit ";
                           else lettre += "huit ";
                           break;
                       case 9:
                           if (dix) lettre += "dix-neuf ";
                           else lettre += "neuf ";
                           break;
                   } // endSwitch(unite)

                   switch (i)
                   {
                       case 1000000000:
                           if (y > 1) lettre += "milliards ";
                           else lettre += "milliard ";
                           break;
                       case 1000000:
                           if (y > 1) lettre += "millions ";
                           else lettre += "million ";
                           break;
                       //case 1000:
                       //    lettre+="mille ";
                   }
               } // end if(y!=0)
               reste -= y * i;
               dix = false;
           } // end for
           if (lettre.Length == 0) lettre += "zero";

           return lettre;
       }
   }
   #endregion

  

  
}
