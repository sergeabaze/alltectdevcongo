using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Model;
using System.Windows.Controls.Primitives;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for Facturation_Sortie.xaml
    /// </summary>
    public partial class Facturation_Sortie : UserControl
    {
        FactureSortieViewModel localViewModel;
        public Facturation_Sortie()
        {
            InitializeComponent();
            FactureSortieViewModel viewModel = new FactureSortieViewModel();
            localViewModel = viewModel;
            this.DataContext = viewModel;
            toolbarMain.Width = SystemParameters.WorkArea.Width;
            if (GlobalDatas.mainHeight > 390)
                GridFacture.Height = (GlobalDatas.mainHeight - 390);
            else GridFacture.Height = 100;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

           
            var facture = (GridFacture.ActiveItem  as FactureModel);
            var checkBox = sender as CheckBox;
            if (facture != null && checkBox != null)
            {
                if (checkBox.IsChecked.Value)
                {
                   // if (facture.ClienOk)
                   // {
                        //facture.IsCheck = facture.IsCheck == true;
                        localViewModel.FacturesListe.First(f => f.IdFacture == facture.IdFacture).IsCheck = true;
                   // }
                   // else
                   // {
                      //  MessageBox.Show(" Ce client est incomplet");
                       // facture.IsCheck = false;
                        //checkBox.IsChecked = false;
                   // }
                }
                else
                {
                    if (localViewModel.FacturesListe != null && localViewModel.FacturesListe.Count > 0)
                    {
                        var facure=localViewModel.FacturesListe.FirstOrDefault (f => f.IdFacture == facture.IdFacture);
                          if (facure !=null )
                              localViewModel.FacturesListe.First(f => f.IdFacture == facture.IdFacture).IsCheck = false;
                    }
                    
                }
                
            }
        }

        void VeridDataContext()
        {
            dateDebut.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            DateFin.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            //if (_viewModel.FatureCurrent.DateCreation != null)
            //{
            //if (dateDebut < DateFin.SelectedDate)
            //        _viewModel.FatureCurrent.DateCreation = null;
            //    this.datevalidate.BlackoutDates.Add(new CalendarDateRange(new DateTime(), ((DateTime)_viewModel.FatureCurrent.DateCreation).AddDays(-1)));
            //}
            //else
            //{
            this.dateDebut.BlackoutDates.Clear();
            dateDebut.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            //}
        }

        private void Show_PopupToolTip(object sender, MouseEventArgs e)
        {
            var facture = (GridFacture.ActiveItem  as FactureModel);
           // MyToolTip.DataContext= (sender as DataGridRow).DataContext as FactureModel;

            DataGridCell listViewItem = e.Source as DataGridCell;
            //FactureModel items = listViewItem.Content as FactureModel;
            //MyFirstPopupTextBlock.Text = fact.NumeroFacture ;
            //MyToolTip.PlacementTarget = GridFacture ;
            //MyToolTip.Placement = PlacementMode.MousePoint;
          // MyToolTip.IsOpen = true;
        }

        private void Hide_PopupToolTip(object sender, MouseEventArgs e)
        {
            //MyToolTip.IsOpen = false;
        }



    }
}
