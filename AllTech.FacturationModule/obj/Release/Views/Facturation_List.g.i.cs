﻿#pragma checksum "..\..\..\Views\Facturation_List.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "64E974DC929F69C2280765CAB62CDB91"
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
using AllTech.FrameWork.Utils;
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
using Wpf.Controls;


namespace AllTech.FacturationModule.Views {
    
    
    /// <summary>
    /// Facturation_List
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Facturation_List : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 36 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ToolBar toolbarMain;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Controls.SplitButton mnFacture;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Controls.SplitButton mnClient;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Controls.SplitButton mnDate;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datedebut;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datefin;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Editors.XamComboEditor cmbClieListe;
        
        #line default
        #line hidden
        
        
        #line 398 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Controls.SplitButton spImprime;
        
        #line default
        #line hidden
        
        
        #line 479 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnvalidation;
        
        #line default
        #line hidden
        
        
        #line 509 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton cmbsortie;
        
        #line default
        #line hidden
        
        
        #line 543 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnSuspension;
        
        #line default
        #line hidden
        
        
        #line 570 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnNonValable;
        
        #line default
        #line hidden
        
        
        #line 633 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AllTech.FrameWork.Utils.SortableListView historicGrid;
        
        #line default
        #line hidden
        
        
        #line 825 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popUpFacture;
        
        #line default
        #line hidden
        
        
        #line 829 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectAll;
        
        #line default
        #line hidden
        
        
        #line 837 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUnselectAll;
        
        #line default
        #line hidden
        
        
        #line 860 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popUpClient;
        
        #line default
        #line hidden
        
        
        #line 864 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectAllclient;
        
        #line default
        #line hidden
        
        
        #line 884 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listboxClient;
        
        #line default
        #line hidden
        
        
        #line 896 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popUpDate;
        
        #line default
        #line hidden
        
        
        #line 913 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateRecherche;
        
        #line default
        #line hidden
        
        
        #line 922 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup MyToolTip;
        
        #line default
        #line hidden
        
        
        #line 936 "..\..\..\Views\Facturation_List.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MyFirstPopupTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/facturation_list.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Facturation_List.xaml"
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
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.toolbarMain = ((System.Windows.Controls.ToolBar)(target));
            return;
            case 3:
            this.mnFacture = ((Wpf.Controls.SplitButton)(target));
            
            #line 114 "..\..\..\Views\Facturation_List.xaml"
            this.mnFacture.Click += new System.Windows.RoutedEventHandler(this.mnFacture_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mnClient = ((Wpf.Controls.SplitButton)(target));
            
            #line 134 "..\..\..\Views\Facturation_List.xaml"
            this.mnClient.Click += new System.Windows.RoutedEventHandler(this.mnClient_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.mnDate = ((Wpf.Controls.SplitButton)(target));
            
            #line 153 "..\..\..\Views\Facturation_List.xaml"
            this.mnDate.Click += new System.Windows.RoutedEventHandler(this.mnDate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.datedebut = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.datefin = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.cmbClieListe = ((Infragistics.Controls.Editors.XamComboEditor)(target));
            
            #line 255 "..\..\..\Views\Facturation_List.xaml"
            this.cmbClieListe.SelectionChanged += new System.EventHandler(this.cmbClieListe_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.spImprime = ((Wpf.Controls.SplitButton)(target));
            return;
            case 10:
            this.btnvalidation = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 11:
            this.cmbsortie = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 12:
            this.btnSuspension = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 13:
            this.btnNonValable = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 14:
            this.historicGrid = ((AllTech.FrameWork.Utils.SortableListView)(target));
            
            #line 636 "..\..\..\Views\Facturation_List.xaml"
            this.historicGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.historicGrid_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 16:
            this.popUpFacture = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 17:
            this.btnSelectAll = ((System.Windows.Controls.Button)(target));
            
            #line 829 "..\..\..\Views\Facturation_List.xaml"
            this.btnSelectAll.Click += new System.Windows.RoutedEventHandler(this.btnSelectAll_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.btnUnselectAll = ((System.Windows.Controls.Button)(target));
            
            #line 837 "..\..\..\Views\Facturation_List.xaml"
            this.btnUnselectAll.Click += new System.Windows.RoutedEventHandler(this.btnUnselectAll_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.popUpClient = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 20:
            this.btnSelectAllclient = ((System.Windows.Controls.Button)(target));
            
            #line 864 "..\..\..\Views\Facturation_List.xaml"
            this.btnSelectAllclient.Click += new System.Windows.RoutedEventHandler(this.btnSelectAllClient_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.listboxClient = ((System.Windows.Controls.ListBox)(target));
            return;
            case 23:
            this.popUpDate = ((System.Windows.Controls.Primitives.Popup)(target));
            
            #line 896 "..\..\..\Views\Facturation_List.xaml"
            this.popUpDate.Opened += new System.EventHandler(this.popUpDate_Opened);
            
            #line default
            #line hidden
            return;
            case 24:
            this.DateRecherche = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 25:
            this.MyToolTip = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 26:
            this.MyFirstPopupTextBlock = ((System.Windows.Controls.TextBlock)(target));
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
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 15:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 640 "..\..\..\Views\Facturation_List.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.historicGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 22:
            
            #line 887 "..\..\..\Views\Facturation_List.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.ApplyclientFilters);
            
            #line default
            #line hidden
            
            #line 887 "..\..\..\Views\Facturation_List.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.UnApplyclientFilters);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
