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
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DatarefInvoice.xaml
    /// </summary>
    public partial class DatarefInvoice : UserControl
    {
        DatarefInvoiceViewModel localviewModel;
        bool isloading;
        public DatarefInvoice(Window window)
        {
            InitializeComponent();
            DatarefInvoiceViewModel viewModel = new DatarefInvoiceViewModel(window);
            localviewModel = viewModel;
            this.DataContext = viewModel;
            toolbarMain.Width = GlobalDatas.mainWidth;
            lstObjet.Width = GlobalDatas.mainWidth * 0.95;

            lstObjet.Height = GlobalDatas.mainHeight -550;
            optionEntete.Width = GlobalDatas.mainWidth * 0.95; 

            optionLangue.Width = GlobalDatas.mainWidth * 0.95;
            optionLangue.Height = GlobalDatas.mainHeight - 390;

            optionDepartement.Width = GlobalDatas.mainWidth * 0.95;
            optionDepartement.Height = GlobalDatas.mainHeight - 400;
            DetailViewDep.Height = optionDepartement.Height * 0.70;

            optionterme.Width = GlobalDatas.mainWidth * 0.95;
           // optionterme.Height = GlobalDatas.mainHeight - 400;

           // gridtaxes.Height = GlobalDatas.mainHeight - 600;
           // deviseHeader.Height = GlobalDatas.mainHeight - 600;
            
               
             isloading = false;
        }


      

        private void DetailView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.localviewModel.StatutSelected = ((ListViewItem)sender).Content as StatutModel;
            e.Handled = true;
            if (this.localviewModel.StatutSelected != null)
            {
                if (localviewModel.LanguageList != null)
                {
                    int i = 0;
                    foreach (var obj in localviewModel.LanguageList)
                    {
                        if (obj.Id == this.localviewModel.StatutSelected.IdLangue)
                        {
                          // cmblanguestat.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            }
        }

     

        private void lstObjet_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.Objetselected = lstObjet.SelectedItem  as ObjetGenericModel ;
            if (this.localviewModel.Objetselected != null)
            {

                if (localviewModel.LanguagedisplayList != null)
                {
                    int i = 0;
                    foreach (var obj in localviewModel.LanguagedisplayList)
                    {
                        if (obj.Id == this.localviewModel.Objetselected.IdLangue)
                        {
                            cmblangueSelect.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            }
          
        }

        private void LanguageView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.LanguageSelected = LanguageView.SelectedItem  as LangueModel;
        }


        private void DetailView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.StatutSelected  = DetailView.SelectedItem  as StatutModel ;

            if (this.localviewModel.StatutSelected != null)
            {
                if (localviewModel.LanguageList != null)
                {
                    int i = 0;
                    foreach (var obj in localviewModel.LanguageList)
                    {
                        if (obj.Id  == this.localviewModel.StatutSelected.IdLangue)
                        {
                          //  cmblanguestat.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            }
        }

        private void DetailViewDep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.DepSelected = DetailViewDep.SelectedItem  as DepartementModel;
        }

        private void DetailViewTerme_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.TermeSelected = DetailViewTerme.SelectedItem as LibelleTermeModel ;
        }

        private void cmblangueSelect_SelectionChanged(object sender, EventArgs e)
        {
            LangueModel langue = cmblangueSelect.SelectedItem as LangueModel;
            if (langue != null)
                localviewModel.LanguageDisplaySelected = langue;
        }

        //private void cmblangue_SelectionChanged(object sender, EventArgs e)
        //{
        //    LangueModel langue = cmblangue.SelectedItem as LangueModel;
        //    if (langue != null)
        //        localviewModel.LanguageSelected = langue;
        //}

        private void cmblanguestat_SelectionChanged(object sender, EventArgs e)
        {
            //LangueModel langue = cmblanguestat.SelectedItem as LangueModel;
            //if (langue != null)
            //    localviewModel.LanguageStatSelected = langue;
        }

     

        private void cmbtermeLangue_SelectionChanged(object sender, EventArgs e)
        {

            LangueModel langue = cmbtermeLangue.SelectedItem as LangueModel;
            if (langue != null)
                localviewModel.LangueTermeSelected = langue;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isloading)
            {
               // lstObjet.Width = GlobalDatas.mainWidth * 0.55;//(GlobalDatas.mainWidth * 0.70);
               // lstObjet.Height = GlobalDatas.mainHeight * 0.30;
                toolbarMain.Width = GlobalDatas.mainWidth;
                lstObjet.Width = GlobalDatas.mainWidth * 0.95;

                lstObjet.Height = GlobalDatas.mainHeight - 550;
                optionEntete.Width = GlobalDatas.mainWidth * 0.95;

                optionLangue.Width = GlobalDatas.mainWidth * 0.95;
                optionLangue.Height = GlobalDatas.mainHeight - 390;

                optionDepartement.Width = GlobalDatas.mainWidth * 0.95;
                optionDepartement.Height = GlobalDatas.mainHeight - 400;
                DetailViewDep.Height = optionDepartement.Height * 0.70;
            }
            isloading = true;
           
        }

        private void objFacturedepartment_GotFocus(object sender, RoutedEventArgs e)
        {
            localviewModel.LoadDepartmentDatas();
        }

      

        private void deviseHeader_GotFocus(object sender, RoutedEventArgs e)
        {
            localviewModel.LoadDevises();
        }

        private void CheckBoxColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var de = e.PropertyName;
        }

       

       
    }
}
