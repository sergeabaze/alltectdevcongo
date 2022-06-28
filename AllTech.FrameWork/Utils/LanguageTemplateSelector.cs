using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace AllTech.FrameWork.Utils
{
    public class LanguageTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ContentPresenter presenter = (ContentPresenter)container;

            if (presenter.TemplatedParent is ComboBox)
            {
                return (DataTemplate)presenter.FindResource("langueComboCollapsed");
            }
            else // Templated parent is ComboBoxItem
            {
                return (DataTemplate)presenter.FindResource("langueComboExpand");
            }
        }
    }
}
