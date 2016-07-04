using Guiet.kQuatre.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Guiet.kQuatre.UI
{
    public partial class TestForm : Form
    {
        private Firework _firework;
        private bool _testInProgress = false;
        private Thread _testThread = null;
        private int _nbMessageSent = 0;
        private int _nbOfFailedMessageSent = 0;
        private int _nbOfSuccessMessageSent = 0;
        private int? _rssi = null;
        private double? _distance = null;
        private int? _lastCorrectRssiReceived = null;
        private bool _isFormLoading = false;
        //Pour la mise à jour de l'interface graphique depuis un autre thread
        private SynchronizationContext _syncContext = null;


        public TestForm(Firework firework)
        {
            _isFormLoading = true;

            InitializeComponent();

            _firework = firework;

            InitializeGridView();

            //Chargement de la combo des boitiers
            RefreshCombobox();

            //Récupération du contexte courant
            _syncContext = WindowsFormsSynchronizationContext.Current;

            //Charge la grille
            LoadDataGrid();

            _isFormLoading = false;
        }

        private void RefreshCombobox()
        {
            this.cbxReceptor.DataSource = null;
            this.cbxReceptor.DataSource = _firework.ReceptorList;
            this.cbxReceptor.DisplayMember = "ReceptorText";
        }        

        private void ConnexionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_testInProgress)
                _testThread.Abort();
        }

        private void btnLaunchTest_Click(object sender, EventArgs e)
        {
            if (!_testInProgress)
            {
                Receptor r = (Receptor)cbxReceptor.SelectedItem;

                _testThread = new Thread(() => LaunchTest(r));
                _testThread.Start();
                _testThread.IsBackground = true;

                //Réinitialisation
                _nbMessageSent = 0;
                _nbOfFailedMessageSent = 0;
                _nbOfSuccessMessageSent = 0;
                _rssi = null;
                _lastCorrectRssiReceived = null;
                _distance = null;


                btnLaunchTest.Text = "Arrêter le test";
                _testInProgress = true;
            }
            else
            {
                _testThread.Abort();
                _testInProgress = false;
                btnLaunchTest.Text = "Lancer le test";
            }
        }

        private void LaunchTest(Receptor r)
        {
            while (true)
            {
                bool testStatut = _firework.CheckCommunication(r);

                if (_firework.RssiReadRequested)
                {
                    _rssi = _firework.ReadRssi();

                    if (_rssi.HasValue)
                    {
                        _distance = Math.Pow(10.0, ((Emetteur.RSSI_AT_ONE_M + _rssi.Value) / (10.0 * Emetteur.PATH_LOSS)));                    
                    }
                    else
                    {
                        _distance = null;
                    }
                }

                _nbMessageSent++;

                if (testStatut)
                    _nbOfSuccessMessageSent++;
                else
                    _nbOfFailedMessageSent++;                

                if (_rssi.HasValue)
                    _lastCorrectRssiReceived = _rssi.Value;

                _syncContext.Post(new SendOrPostCallback((o) => { RefreshGUI(); }), null);

                //On attend une seconde le temps de recevoir une reponse de l'emetteur
                Thread.Sleep(1000);
            }
        }

        private void RefreshGUI()
        {
            string puissance = "NA";
            string lastCorrectPuissace = "NA";
            string distance = "NA";

            if (_rssi.HasValue)
            {
                puissance = _rssi.ToString();

                //Pourcentage
                int percent = (Convert.ToInt32(Emetteur.RSSI_AT_ONE_M)*-1 / _rssi.Value) * 100;

                puissance = string.Format("{0} ({1}%)", puissance, percent.ToString());
            }

            if (_distance.HasValue)
            {
                distance = string.Format("{0:0.00} m", _distance.Value);
            }

            if (_lastCorrectRssiReceived.HasValue)
            {
                lastCorrectPuissace = _lastCorrectRssiReceived.ToString();
            }

            lblDistance.Text = string.Format("Distance approx. : {0}", distance);

            lblPuissance.Text = string.Format("Puissance du signal : {0}", puissance);

            lblResultat.Text = string.Format("Messages envoyés : {0}, reçus : {1}, perdus : {2}", _nbMessageSent.ToString(), _nbOfSuccessMessageSent.ToString(), _nbOfFailedMessageSent.ToString());

            lblLastKnownPower.Text = string.Format("Dernière puissance connue du signal : {0}", lastCorrectPuissace);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void InitializeGridView()
        {

            this.gvResistance.MasterTemplate.ShowRowHeaderColumn = false;
            this.gvResistance.ReadOnly = true;
            this.gvResistance.EnableGrouping = false;
            this.gvResistance.AllowAddNewRow = false;
            this.gvResistance.AllowEditRow = false;
            this.gvResistance.SelectionMode = GridViewSelectionMode.FullRowSelect;
            this.gvResistance.EnableSorting = false;

            //Master template
            this.gvResistance.TableElement.BeginUpdate();
            this.gvResistance.MasterTemplate.ShowColumnHeaders = true;
            this.gvResistance.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            this.gvResistance.MasterTemplate.Columns.Clear();
            this.gvResistance.MasterTemplate.AutoGenerateColumns = false; //Pour les tests

            GridViewTextBoxColumn lineNumberColumn = new GridViewTextBoxColumn();
            lineNumberColumn.Name = "NumeroLigne";
            lineNumberColumn.HeaderText = "N° ligne";
            lineNumberColumn.FieldName = "LineNumberText";
            lineNumberColumn.Width = 50;
            //lineNumberColumn.ReadOnly = true;
            this.gvResistance.MasterTemplate.Columns.Add(lineNumberColumn);

            GridViewImageColumn gvImageColumn = new GridViewImageColumn();
            gvImageColumn.Name = "EtatResistanceImage";
            gvImageColumn.HeaderText = "Etat";
            gvImageColumn.FieldName = "ResistanceStateImage";
            gvImageColumn.Width = 50;
            this.gvResistance.MasterTemplate.Columns.Add(gvImageColumn);            

            GridViewTextBoxColumn designationColumn = new GridViewTextBoxColumn();
            designationColumn.Name = "Designation";
            designationColumn.HeaderText = "Désignation";
            designationColumn.FieldName = "Designation";
            designationColumn.Width = 250;
            // designationColumn.ReadOnly = true;
            this.gvResistance.MasterTemplate.Columns.Add(designationColumn);
         
            GridViewTextBoxColumn adresseRecepteurColumn = new GridViewTextBoxColumn();
            adresseRecepteurColumn.Name = "AdresseRecepteur";
            adresseRecepteurColumn.HeaderText = "Liaison";
            adresseRecepteurColumn.FieldName = "ReceptorAddress.ReceptorAddressText";
            adresseRecepteurColumn.Width = 150;
            // adresseRecepteurColumn.ReadOnly = true;
            this.gvResistance.MasterTemplate.Columns.Add(adresseRecepteurColumn);

            GridViewTextBoxColumn resistanceColumn = new GridViewTextBoxColumn();
            resistanceColumn.Name = "Resistance";
            resistanceColumn.HeaderText = "Résistance  (Ohm)";
            resistanceColumn.FieldName = "Resistance";
            resistanceColumn.Width = 150;
            this.gvResistance.MasterTemplate.Columns.Add(resistanceColumn);
            
            GridViewCommandColumn commandColumn1 = new GridViewCommandColumn();
            commandColumn1.Name = "TesterResistance";
            commandColumn1.UseDefaultText = true;
            commandColumn1.DefaultText = "Tester";
            commandColumn1.TextAlignment = ContentAlignment.MiddleCenter;
            this.gvResistance.MasterTemplate.Columns.Add(commandColumn1);            

            this.gvResistance.TableElement.EndUpdate(false);
        }

        private void cbxReceptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFormLoading) return;

            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            Receptor r = (Receptor)cbxReceptor.SelectedItem;

            this.gvResistance.DataSource = null;
            this.gvResistance.DataSource = _firework.GetLigneArtificeByReceptorList(r);
            this.gvResistance.MasterTemplate.ExpandAll();
        }

        private void gvResistance_CommandCellClick(object sender, EventArgs e)
        {
            switch (((GridCommandCellElement)sender).ColumnInfo.Name)
            {
                case "TesterResistance":
                    LigneArtifice ligneArtifice = (LigneArtifice)(((GridCommandCellElement)sender).RowInfo.DataBoundItem);
                    //Receptor r = (Receptor)cbxReceptor.SelectedItem;
                    _firework.TestResistance(ligneArtifice);
                    break;
            }
        }

    }
}
