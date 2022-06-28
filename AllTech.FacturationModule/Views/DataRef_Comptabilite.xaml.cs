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
using System.Data;
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataRef_Comptabilite.xaml
    /// </summary>
    public partial class DataRef_Comptabilite : UserControl
    {
        DatarefComptabiliteViewModel localViewModel;
        DataRowView rowSelected = null;
        bool isloading = false;

        public DataRef_Comptabilite( Window window)
        {
            InitializeComponent();
            DatarefComptabiliteViewModel viewModel = new DatarefComptabiliteViewModel(window);
            this.DataContext = viewModel;
            localViewModel = viewModel;
            gridComptaParam.Height = 1000; //(GlobalDatas.mainHeight*0.5);
            DetailViewAnal.Height = GlobalDatas.mainHeight-550;
            gridCompeGeneral.Height = GlobalDatas.mainHeight - 550;
        }


        private void gridCompeGeneral_ActiveCellChanged(object sender, EventArgs e)
        {
            if (this.gridCompeGeneral.ActiveCell != null)
            {
                var cell = this.gridCompeGeneral.ActiveCell.Value;
                //this.ActiveCellValue.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:d}", this.dataGrid.ActiveCell.Value);
            }
        }

        private void gridCompeGeneral_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CompteGenralModel rowSelected = this.gridCompeGeneral.ActiveItem as CompteGenralModel;
            if (rowSelected != null)
            {
                localViewModel.CompteGeneSelected = rowSelected;
            }
        }

        private void DetailViewAnal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
           this.localViewModel.CompteCASelected = DetailViewAnal.SelectedItem as CompteAnalytiqueModel;
        }

        private void cmbComptaListechamps_SelectionChanged(object sender, EventArgs e)
        {
            this.localViewModel.RowComptaListeSelcted = cmbComptaListechamps.SelectedItem as ComptaList;
        }

        private void gridComptaParam_ActiveCellChanged(object sender, EventArgs e)
        {
            int i=0;
            //if (this.gridComptaParam.ActiveCell != null && this.gridComptaParam.ActiveCell.Value !=null)
            //{
            //    if (this.gridComptaParam.ActiveCell.IsEditable)
            //    {
            //        rowSelected = this.gridComptaParam.ActiveItem as DataRowView;
            //        if (rowSelected != null)
            //        {
            //            var w = this.gridComptaParam.ActiveCell.Column;
            //            string valeur = w.Key;
            //            if (valeur.Equals("Position") || valeur.Equals("Taille"))
            //            {
            //                foreach (DataRow row in localViewModel.TableComptaChamparam.Rows)
            //                {
            //                    if (int.Parse(row["IdChamps"].ToString()) == int.Parse(rowSelected.Row[1].ToString()))
            //                    {

            //                        if (valeur.Equals("Taille"))
            //                            localViewModel.TableComptaChamparam.Rows[i]["taille"] = this.gridComptaParam.ActiveCell.Value;
            //                        else localViewModel.TableComptaChamparam.Rows[i]["Positions"] = this.gridComptaParam.ActiveCell.Value;
            //                        localViewModel.TableComptaChamparam.Rows[i]["etat"] = 0;
            //                    }

            //                    i++;
            //                }
            //            }
            //        }
            //    }
                //if (this.gridComptaParam.ActiveCell.Value !=null )
                //MessageBox.Show(this.gridComptaParam.ActiveCell.Value.ToString ());
                //else MessageBox.Show("0");
           // }

        }

        private void gridComptaParam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
         

             rowSelected = this.gridComptaParam.ActiveItem as DataRowView;

           // MessageBox.Show(" fin -->" + rowSelected.Row[0]);
        }

        private void detail_click(object sender, RoutedEventArgs e)
        {
            DataRowView row = ((Button)sender).CommandParameter as DataRowView;
            try
            {
                if (row != null)
                {
                    ComptabiliteModel.GetComptaGene_Param_Delete(Convert.ToInt32 ( row.Row["ID"]));

                    localViewModel.LoadComptabiliteListe();
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
            //DetailProduitClient vf = new DetailProduitClient(client);
        }

        private void gridComptaParam_ActiveCellChanged_1(object sender, EventArgs e)
        {

        }

        private void gridComptaParam_CellDoubleClicked(object sender, Infragistics.Controls.Grids.CellClickedEventArgs e)
        {
            DataRowView rowSelected = this.gridComptaParam.ActiveItem as DataRowView;
            if (rowSelected!=null )
            localViewModel.RowSelect = rowSelected;
        }

        private void gridComptaParam_CellExitedEditMode(object sender, Infragistics.Controls.Grids.CellExitedEditingEventArgs e)
        {
            int i = 0;
            rowSelected = this.gridComptaParam.ActiveItem as DataRowView;
             var w = this.gridComptaParam.ActiveCell.Column;
             string valeur = w.Key;
            if (valeur.Equals("Positions") || valeur.Equals("taille"))
            {
                foreach (DataRow row in localViewModel.TableComptaChamparam.Rows)
                {
                    if (int.Parse(row["IdChamps"].ToString()) == int.Parse(rowSelected.Row[1].ToString()))
                    {

                        if (valeur.Equals("taille"))
                            localViewModel.TableComptaChamparam.Rows[i]["taille"] = this.gridComptaParam.ActiveCell.Value;
                        else
                        {
                            //if (!checkMultiplePosition(this.gridComptaParam.ActiveCell.Value.ToString()))
                                localViewModel.TableComptaChamparam.Rows[i]["Positions"] = this.gridComptaParam.ActiveCell.Value;
                            //else {
                            //    MessageBox.Show("Cette position existedéja [ " + this.gridComptaParam.ActiveCell.Value + " ] dans la colletion");
                            //    localViewModel.TableComptaChamparam.Rows[i]["Positions"] =0;
                               
                            //}
                        }
                        localViewModel.TableComptaChamparam.Rows[i]["etat"] = 0;
                        localViewModel.isOperation = true;
                    }

                    i++;
                }
            }

        }

        bool checkMultiplePosition(string position)
        {
            bool value = false;
            if (localViewModel.TableComptaChamparam.Rows.Count==0 || localViewModel.TableComptaChamparam == null)
              value=  false;

            foreach (DataRow row in localViewModel.TableComptaChamparam.Rows)
            {
                object actualRow = row["Positions"];

                if (Convert.ToInt32(row["Positions"]) == Int32.Parse(position))
                {
                    value = true;
                    break;
                }
            }

            return value;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
            {
                gridComptaParam.Height = (GlobalDatas.mainHeight * 0.3);
            }
            isloading = false;
        }

        private void cmbListCompteOhada_SelectionChanged(object sender, EventArgs e)
        {
            if (cmbListCompteOhada.SelectedItem !=null )
            localViewModel.ComptaOhadafreeSelected = cmbListCompteOhada.SelectedItem as CompteOhadaModel;
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            object  row = ((Button)sender).CommandParameter as object;
            int testc =int .Parse ( row.ToString());
            try
            {
                if (row != null)
                {
                    CompteGenralModel dale = new CompteGenralModel();
                    dale.ModelCompteGeneral_Delete(Convert.ToInt32(row.ToString()));
                    localViewModel.LoadCallBack();
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

       

      
    }
}
