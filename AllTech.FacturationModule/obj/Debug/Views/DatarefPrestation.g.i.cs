﻿#pragma checksum "..\..\..\Views\DatarefPrestation.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "84E6733F7FD8B6D4E00A35952ECE6131"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1026
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllTech.FrameWork.Converter;
using Infragistics;
using Infragistics.Collections;
using Infragistics.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Controls.Maps;
using Infragistics.Controls.Menus;
using Infragistics.Controls.Menus.Primitives;
using Infragistics.Controls.Primitives;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Windows.OutlookBar;
using Microsoft.Practices.Prism.Commands;
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


namespace AllTech.FacturationModule.Views {
    
    
    /// <summary>
    /// DatarefPrestation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class DatarefPrestation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 29 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ToolBar toolbarMain;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Editors.XamComboEditor cmbLangueAff;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cmbLangue;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.OutlookBar.XamOutlookBar optionrechechName;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTRecherche;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button txtRecherche;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Grids.XamGrid productGrid;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\Views\DatarefPrestation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Grids.RowSelectorSettings rowSelectorSettings;
        
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
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/datarefprestation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\DatarefPrestation.xaml"
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
            
            #line 15 "..\..\..\Views\DatarefPrestation.xaml"
            ((AllTech.FacturationModule.Views.DatarefPrestation)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.UserControl_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.toolbarMain = ((System.Windows.Controls.ToolBar)(target));
            return;
            case 4:
            this.cmbLangueAff = ((Infragistics.Controls.Editors.XamComboEditor)(target));
            
            #line 101 "..\..\..\Views\DatarefPrestation.xaml"
            this.cmbLangueAff.SelectionChanged += new System.EventHandler(this.cmbLangueAff_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cmbLangue = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\Views\DatarefPrestation.xaml"
            this.cmbLangue.Click += new System.Windows.RoutedEventHandler(this.cmbLangue_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.optionrechechName = ((Infragistics.Windows.OutlookBar.XamOutlookBar)(target));
            return;
            case 7:
            this.txtTRecherche = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtRecherche = ((System.Windows.Controls.Button)(target));
            
            #line 193 "..\..\..\Views\DatarefPrestation.xaml"
            this.txtRecherche.Click += new System.Windows.RoutedEventHandler(this.txtRecherche_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.productGrid = ((Infragistics.Controls.Grids.XamGrid)(target));
            
            #line 291 "..\..\..\Views\DatarefPrestation.xaml"
            this.productGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.preductGrid_SelectChange);
            
            #line default
            #line hidden
            return;
            case 10:
            this.rowSelectorSettings = ((Infragistics.Controls.Grids.RowSelectorSettings)(target));
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
            case 11:
            
            #line 347 "..\..\..\Views\DatarefPrestation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.detail_click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
