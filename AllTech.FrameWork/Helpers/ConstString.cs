using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace AllTech.FrameWork.Helpers
{
    public class ConstString : MarkupExtension
    {
        const string LabelMark = "Label";
        static Func<string, string> getLocalizedString;
        public static void SetConstStrings(Func<string, string> getLocalizedString1)
        {
            getLocalizedString = getLocalizedString1;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            bool label = false;
            string id = ID;
            if (id.EndsWith(LabelMark))
            {
                label = true;
                id = id.Remove(id.Length - LabelMark.Length);
            }
            string s;
            try
            {
                s = getLocalizedString(id);
            }
            catch
            {
                s = id;// DevExpress.Data.Helpers.MasterDetailHelper.SplitPascalCaseString(id);
            }
            return label ? s + ":" : s;
        }
        public ConstString() { }
        public string ID { get; set; }
    }
}
