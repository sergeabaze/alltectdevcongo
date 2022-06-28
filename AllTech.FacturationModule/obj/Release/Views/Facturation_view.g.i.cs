﻿#pragma checksum "..\..\..\Views\Facturation_view.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3280ED3BAA76804E3B20B141EDEEB549"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllTech.FrameWork.Converter;
using AllTech.FrameWork.UIControl;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.ValidationRules;
using Microsoft.Practices.Prism.Commands;
using NumericUpDownControl;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPFAutoCompleteBox;


namespace AllTech.FacturationModule.Views {
    
    
    /// <summary>
    /// Facturation_view
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Facturation_view : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 22 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmdnewfacture;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbClient;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbObjet;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbexploit;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePrestation;
        
        #line default
        #line hidden
        
        
        #line 292 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkValidateDate;
        
        #line default
        #line hidden
        
        
        #line 403 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbChoice;
        
        #line default
        #line hidden
        
        
        #line 408 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AllTech.FrameWork.Utils.AutoCompleteTextBox txtOther;
        
        #line default
        #line hidden
        
        
        #line 412 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericUpDownControl.NumericBox txtQty;
        
        #line default
        #line hidden
        
        
        #line 439 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 444 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddgrid;
        
        #line default
        #line hidden
        
        
        #line 490 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Items;
        
        #line default
        #line hidden
        
        
        #line 584 "..\..\..\Views\Facturation_view.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtbloclCumul;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/facturation_view.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Facturation_view.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 15 "..\..\..\Views\Facturation_view.xaml"
            ((AllTech.FacturationModule.Views.Facturation_view)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.cmdnewfacture = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Views\Facturation_view.xaml"
            this.cmdnewfacture.Click += new System.Windows.RoutedEventHandler(this.cmdnewfacture_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbClient = ((System.Windows.Controls.ComboBox)(target));
            
            #line 178 "..\..\..\Views\Facturation_view.xaml"
            this.cmbClient.KeyDown += new System.Windows.Input.KeyEventHandler(this.cmbClient_KeyDown);
            
            #line default
            #line hidden
            
            #line 178 "..\..\..\Views\Facturation_view.xaml"
            this.cmbClient.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbClient_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cmbObjet = ((System.Windows.Controls.ComboBox)(target));
            
            #line 186 "..\..\..\Views\Facturation_view.xaml"
            this.cmbObjet.KeyDown += new System.Windows.Input.KeyEventHandler(this.cmbObjet_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cmbexploit = ((System.Windows.Controls.ComboBox)(target));
            
            #line 202 "..\..\..\Views\Facturation_view.xaml"
            this.cmbexploit.KeyDown += new System.Windows.Input.KeyEventHandler(this.cmbexploit_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.datePrestation = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.chkValidateDate = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.cmbChoice = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.txtOther = ((AllTech.FrameWork.Utils.AutoCompleteTextBox)(target));
            return;
            case 11:
            this.txtQty = ((NumericUpDownControl.NumericBox)(target));
            
            #line 414 "..\..\..\Views\Facturation_view.xaml"
            this.txtQty.LostFocus += new System.Windows.RoutedEventHandler(this.txtQty_LostFocus);
            
            #line default
            #line hidden
            return;
            case 12:
            this.txtUnitPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.btnAddgrid = ((System.Windows.Controls.Button)(target));
            
            #line 444 "..\..\..\Views\Facturation_view.xaml"
            this.btnAddgrid.Click += new System.Windows.RoutedEventHandler(this.btnAddgrid_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Items = ((System.Windows.Controls.DataGrid)(target));
            
            #line 497 "..\..\..\Views\Facturation_view.xaml"
            this.Items.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Items_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 16:
            this.txtbloclCumul = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 15:
            
            #line 571 "..\..\..\Views\Facturation_view.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.txtMontant_MouseLeave);
            
            #line default
            #line hidden
            
            #line 571 "..\..\..\Views\Facturation_view.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.txtMontant_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

