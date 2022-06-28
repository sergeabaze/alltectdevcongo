using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace AllTech.FrameWork.Behavior
{
  public   class DataGridCustomCommandBehavior:CommandBehaviorBase<DataGrid>
    {
        public DataGridCustomCommandBehavior(DataGrid datagrid)
            : base(datagrid)
        {
            datagrid.MouseDoubleClick += new MouseButtonEventHandler(datagrid_MouseDoubleClick);
        }
        void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ExecuteCommand();
        }
    }

  public class CurrentCellChangesClass
  {
      public static object GetCommandParameter(DependencyObject obj)
      {
          return (object)obj.GetValue(CommandParameterProperty);
      }
      public static void SetCommandParameter(DependencyObject obj, object value)
      {
          obj.SetValue(CommandParameterProperty, value);
      }

      public static DependencyProperty CommandParameterProperty =
          DependencyProperty.RegisterAttached("CommandParameter",
          typeof(object), typeof(CurrentCellChangesClass),
          new PropertyMetadata(OnsetCommandParameterCallBack));

      public static ICommand GetCommand(DependencyObject obj)
      {
          return (ICommand)obj.GetValue(CommandProperty);
      }


      public static void SetCommand(DependencyObject obj, ICommand value)
      {
          obj.SetValue(CommandProperty, value);
      }

      public static readonly DependencyProperty CommandProperty =
      DependencyProperty.RegisterAttached("Command", typeof(ICommand),
      typeof(CurrentCellChangesClass),
      new PropertyMetadata(onsetCommandCallback));

      public static DataGridCustomCommandBehavior GetClickCommandBehavior(DependencyObject obj)
      {
          return (DataGridCustomCommandBehavior)
              obj.GetValue(ClickCommandBehaviorProperty);
      }

      public static void SetClickCommandBehavior(DependencyObject obj,
                                               DataGridCustomCommandBehavior value)
      {
          obj.SetValue(ClickCommandBehaviorProperty, value);
      }

      public static readonly DependencyProperty ClickCommandBehaviorProperty =
          DependencyProperty.RegisterAttached("ClickCommandBehavior",
                   typeof(DataGridCustomCommandBehavior),
                   typeof(CurrentCellChangesClass), null);

      private static DataGridCustomCommandBehavior GetOrCreateDataGridBehavior(DataGrid datagrid)
      {
          var dgbehavior = datagrid.GetValue(ClickCommandBehaviorProperty) as DataGridCustomCommandBehavior;
          if (dgbehavior == null)
          {
              dgbehavior = new DataGridCustomCommandBehavior(datagrid);
              datagrid.SetValue(ClickCommandBehaviorProperty, dgbehavior);
          }
          return dgbehavior;

      }
      public static void onsetCommandCallback(DependencyObject dep, DependencyPropertyChangedEventArgs e)
      {
          var datagrid = dep as DataGrid;
          if (datagrid != null)
          {
              DataGridCustomCommandBehavior behavior =
                  GetOrCreateDataGridBehavior(datagrid);
              behavior.Command = e.NewValue as ICommand;
          }
      }

      public static void OnsetCommandParameterCallBack(DependencyObject dep,
          DependencyPropertyChangedEventArgs e)
      {
          var datagrid = dep as DataGrid;
          if (datagrid != null)
          {
              DataGridCustomCommandBehavior behavior =
                GetOrCreateDataGridBehavior(datagrid);
              behavior.CommandParameter = e.NewValue;
          }
      }
    }
}
