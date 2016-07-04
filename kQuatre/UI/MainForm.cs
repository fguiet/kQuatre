using Guiet.kQuatre.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Guiet.kQuatre.UI
{
    public partial class MainForm : Form
    {
        #region Membres privés

        //Classe qui gère le feu
        private Firework _firework = null;
        //Pour la gestion d'un XBee via port USB
        private XBeeDeviceManager _deviceManager = null;
        //Gestion du Thread qui gère le feu d'artifice
        private BackgroundWorker _fireWorkThread = null;
        //Pour la mise à jour de l'interface graphique depuis un autre thread
        private SynchronizationContext _syncContext = null;

        private bool _transmitterInitialisationStarted = false;

        #endregion

        #region Constructeur

        public MainForm()
        {
            InitializeComponent();

            //Récupération du contexte courant
            _syncContext = WindowsFormsSynchronizationContext.Current;

            //Initialisation des tableaux
            InitializeGridviews();

            //Initialisation d'un firework vide
            _firework = new Firework();
            //TODO : A Supprimer quand l'ajout d'un boitier sera dispo
            _firework.InitNewFireWork();
            RefreshCombobox();
            //fin todo

            //Initialisation d'un backgroundworker
            _fireWorkThread = new BackgroundWorker();
            _fireWorkThread.DoWork += _fireWorkThread_DoWork;
            _fireWorkThread.ProgressChanged += _fireWorkThread_ProgressChanged;
            _fireWorkThread.RunWorkerCompleted += _fireWorkThread_RunWorkerCompleted;
            _fireWorkThread.WorkerReportsProgress = true;
            _fireWorkThread.WorkerSupportsCancellation = true;

            //Initialisation d'un device manager
            _deviceManager = new XBeeDeviceManager();
            _deviceManager.XBeeConnected += _deviceManager_XBeeConnected;
            _deviceManager.XBeeDisconnected += _deviceManager_XBeeDisconnected;

            //Binding sur le temps du feu
            //_firework.PropertyChanged += Firework_PropertyChanged;

            //Lance la recherche d'un XBee qui pourrait déjà être connecté à l'ordi
            _deviceManager.DiscoverXBeeDevices();

            //Initialise les boutons de la barre de commandes            
            RefreshCommandBarButtonState();
        }


        /// <summary>
        /// On est obligé de passer par ce mécanisme pour mettre à jour la GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Firework_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Firework && e.PropertyName == "DureeFirework")
            {
                _syncContext.Post(new SendOrPostCallback((o) => { UpdateDureeFirework(); }), null);
            }

            if (sender is Firework && e.PropertyName == "DureeTotale")
            {
                _syncContext.Post(new SendOrPostCallback((o) => { UpdateDureeTotale(); }), null);
            }

        }

        #endregion

        #region Membres privés

        private void UpdateDureeFirework()
        {
            lblDureeFeu.Text = _firework.DureeFirework;
            lblDureeFeu.Update();
        }

        private void UpdateDureeTotale()
        {
            lblDureeTotale.Text = _firework.DureeTotale;
            lblDureeTotale.Update();
        }

        /// <summary>
        /// Initialise l'émetteur XBee du feu d'artifice.
        /// Cette méthode est appelée lorsque l'utilisateur branche un XBee à l'ordinateur
        /// </summary>
        private void InitializeFireworkTransmitter()
        {
            if (_transmitterInitialisationStarted) return;

            try
            {
                if (_deviceManager.IsXbeeConnected && !_firework.EmitterConnected)
                {
                    _transmitterInitialisationStarted = true;
                    _firework.InitializeTransmitter(_deviceManager.XbeeComPort);
                    _transmitterInitialisationStarted = false;
                }
            }
            catch (Exception e)
            {
                _transmitterInitialisationStarted = false;
                MessageBox.Show(string.Format("Erreur lors de l'initialisation du XBee transmetteur\n\n{0}", e.Message), "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Détruit l'émetteur lié au feu d'artifice (cas où l'utilisateur déconnecte le XBee 
        /// de l'ordinateur par exemple
        /// </summary>
        private void DestroyFireworkTransmitter()
        {
            try
            {
                if (_firework.EmitterConnected)
                    _firework.DetroyTransmitter();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Erreur lors de la suppression du XBee transmetteur\n\n{0}", e.Message), "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Méthode qui met à jour l'état de la barre de commande
        /// en fonction de plusieurs paramètres :
        /// XBee connecté ou pas, feu d'artifice lancé ou pas, etc
        /// </summary>
        private void RefreshCommandBarButtonState()
        {
            // if (_deviceManager == null) return;

            //1. On regarde le statut de connection du transmetteur
            if (!_deviceManager.IsXbeeConnected)
            {
                lblTransmitterNotConnected.Visible = true;
            }
            else
            {
                lblTransmitterNotConnected.Visible = false;
            }

            if (!_deviceManager.IsXbeeConnected)
            {
                mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                btnFire.Enabled = false;
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnFireNextFirework.Enabled = false;
                btnTestConnexion.Enabled = false;
                btnReinit.Enabled = false;
                btnPause.Text = "Pause";
                return;
            }

            //On vérifie qu'un feu est chargé
            if (!_firework.IsLoaded)
            {
                mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = true;
                mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                btnFire.Enabled = false;
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnTestConnexion.Enabled = false;
                btnFireNextFirework.Enabled = false;
                btnReinit.Enabled = false;
                btnPause.Text = "Pause";
                return;
            }

            //On regarder le statut du feu
            switch (_firework.FireworkStatus)
            {
                case FireworkStatus.Paused:
                    mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = false;
                    btnFire.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = false;
                    btnFireNextFirework.Enabled = false;
                    btnTestConnexion.Enabled = false;
                    btnReinit.Enabled = false;
                    btnPause.Text = "Reprendre";
                    break;

                case FireworkStatus.Running:
                    mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = false;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = false;
                    btnFire.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;
                    btnTestConnexion.Enabled = false;
                    btnFireNextFirework.Enabled = true;
                    btnReinit.Enabled = false;
                    btnPause.Text = "Pause";
                    break;

                case FireworkStatus.StoppedAndFinished:
                    mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    btnFire.Enabled = false;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnTestConnexion.Enabled = true;
                    btnFireNextFirework.Enabled = false;
                    btnReinit.Enabled = true;
                    btnPause.Text = "Pause";
                    break;

                case FireworkStatus.StoppedAndNeverLaunched:
                    mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    btnFire.Enabled = true;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnTestConnexion.Enabled = true;
                    btnFireNextFirework.Enabled = false;
                    btnReinit.Enabled = false;
                    btnPause.Text = "Pause";
                    break;

                case FireworkStatus.StoppedByUserBeforeEnd:
                    mainMenu.Items.Find("nouveauToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("chargerToolStripMenuItem", true)[0].Enabled = true;
                    mainMenu.Items.Find("quitterToolStripMenuItem", true)[0].Enabled = true;
                    btnFire.Enabled = false;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnTestConnexion.Enabled = true;
                    btnFireNextFirework.Enabled = false;
                    btnReinit.Enabled = true;
                    btnPause.Text = "Pause";
                    break;

            }
        }

        /// <summary>
        /// Charge un feu d'artifice dans l'interface graphique depuis un fichier de sauvegarde
        /// </summary>
        /// <param name="fullFilename"></param>
        private void LoadFirework(string fullFilename)
        {
            _firework.LoadFirework(fullFilename);

            RefreshCombobox();
            RefreshGridDataSource(false);
            RefreshCommandBarButtonState();
        }

        /// <summary>
        /// Initialisation des gridviews
        /// </summary>
        private void InitializeGridviews()
        {
            //*******************
            //Gridview Conception
            //*******************
            this.gvFireworkConception.MasterTemplate.ShowRowHeaderColumn = false;
            this.gvFireworkConception.ReadOnly = true;
            this.gvFireworkConception.EnableGrouping = false;
            this.gvFireworkConception.AllowAddNewRow = false;
            this.gvFireworkConception.AllowEditRow = false;
            this.gvFireworkConception.SelectionMode = GridViewSelectionMode.FullRowSelect;
            this.gvFireworkConception.EnableSorting = false;

            //Master template
            this.gvFireworkConception.TableElement.BeginUpdate();
            this.gvFireworkConception.MasterTemplate.ShowColumnHeaders = false;
            this.gvFireworkConception.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.gvFireworkConception.MasterTemplate.Columns.Clear();
            this.gvFireworkConception.MasterTemplate.AutoGenerateColumns = false;

            GridViewTextBoxColumn textBoxColumn = new GridViewTextBoxColumn();
            textBoxColumn.Name = "NumeroLigne";
            textBoxColumn.HeaderText = "Numéro de ligne";
            textBoxColumn.FieldName = "TextNumero";

            this.gvFireworkConception.MasterTemplate.Columns.Add(textBoxColumn);

            GridViewCommandColumn commandColumn = new GridViewCommandColumn();
            commandColumn.Name = "AjouterArtifice";
            commandColumn.UseDefaultText = true;

            commandColumn.DefaultText = "Ajouter un artifice";
            commandColumn.TextAlignment = ContentAlignment.MiddleCenter;
            commandColumn.Width = 8;
            this.gvFireworkConception.MasterTemplate.Columns.Add(commandColumn);

            this.gvFireworkConception.TableElement.EndUpdate(false);

            //Création de la childtable                       
            GridViewTemplate childTemplate = new GridViewTemplate();

            childTemplate.AllowAddNewRow = false;
            childTemplate.ShowRowHeaderColumn = false;
            childTemplate.AutoGenerateColumns = false;
            childTemplate.EnableSorting = false;
            //childTemplate.AllowEditRow = false;
            //childTemplate.se = GridViewSelectionMode.FullRowSelect;

            childTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.gvFireworkConception.Templates.Add(childTemplate);

            textBoxColumn = new GridViewTextBoxColumn();
            textBoxColumn.Name = "Designation";
            textBoxColumn.HeaderText = "Désignation";
            textBoxColumn.FieldName = "Designation";
            //textBoxColumn.ReadOnly = true;

            childTemplate.Columns.Add(textBoxColumn);

            /*GridViewDecimalColumn decimalColumn = new GridViewDecimalColumn();
            decimalColumn.Name = "Quantite";
            decimalColumn.HeaderText = "Quantité";
            decimalColumn.FieldName = "Quantite";
            decimalColumn.ReadOnly = true;

            childTemplate.Columns.Add(decimalColumn);*/

            GridViewTextBoxColumn miseAFeuColumn = new GridViewTextBoxColumn();
            miseAFeuColumn.Name = "MiseAFeu";
            miseAFeuColumn.FieldName = "MiseAFeu";
            miseAFeuColumn.HeaderText = "Mise à feu";
            //miseAFeuColumn.ReadOnly = true;
            childTemplate.Columns.Add(miseAFeuColumn);

            GridViewTextBoxColumn dureeColumn = new GridViewTextBoxColumn();
            dureeColumn.FieldName = "DureeArtifice";
            dureeColumn.Name = "Duree";
            dureeColumn.HeaderText = "Durée";
            childTemplate.Columns.Add(dureeColumn);

            GridViewTextBoxColumn adresseRecepteurColumn1 = new GridViewTextBoxColumn();
            adresseRecepteurColumn1.Name = "AdresseRecepteur";
            adresseRecepteurColumn1.HeaderText = "Liaison";
            adresseRecepteurColumn1.FieldName = "ReceptorAddress.ReceptorAddressText";
            //adresseRecepteurColumn1.ReadOnly = true;
            childTemplate.Columns.Add(adresseRecepteurColumn1);

            GridViewCommandColumn commandColumn1 = new GridViewCommandColumn();
            commandColumn1.Name = "ModifierArtifice";
            commandColumn1.UseDefaultText = true;

            commandColumn1.DefaultText = "Modifier";
            commandColumn1.TextAlignment = ContentAlignment.MiddleCenter;
            //commandColumn1.Width = 8;
            childTemplate.Columns.Add(commandColumn1);

            GridViewCommandColumn commandColumn2 = new GridViewCommandColumn();
            commandColumn2.Name = "SupprimerArtifice";
            commandColumn2.UseDefaultText = true;

            commandColumn2.DefaultText = "Supprimer";
            commandColumn2.TextAlignment = ContentAlignment.MiddleCenter;
            //commandColumn1.Width = 8;
            childTemplate.Columns.Add(commandColumn2);

            GridViewRelation relation = new GridViewRelation(this.gvFireworkConception.MasterTemplate, childTemplate);
            relation.ChildColumnNames.Add("LigneArtificeList");

            this.gvFireworkConception.Relations.Add(relation);


            //***************
            //* GridView tir
            //***************
            this.gvFireworkLaunch.MasterTemplate.ShowRowHeaderColumn = false;
            this.gvFireworkLaunch.ReadOnly = true;
            this.gvFireworkLaunch.EnableGrouping = false;
            this.gvFireworkLaunch.AllowAddNewRow = false;
            this.gvFireworkLaunch.AllowEditRow = false;
            this.gvFireworkLaunch.SelectionMode = GridViewSelectionMode.FullRowSelect;
            this.gvFireworkLaunch.EnableSorting = false;

            //Master template
            this.gvFireworkLaunch.TableElement.BeginUpdate();
            this.gvFireworkLaunch.MasterTemplate.ShowColumnHeaders = true;
            this.gvFireworkLaunch.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            this.gvFireworkLaunch.MasterTemplate.Columns.Clear();
            this.gvFireworkLaunch.MasterTemplate.AutoGenerateColumns = false; //Pour les tests

            GridViewTextBoxColumn lineNumberColumn = new GridViewTextBoxColumn();
            lineNumberColumn.Name = "NumeroLigne";
            lineNumberColumn.HeaderText = "N° ligne";
            lineNumberColumn.FieldName = "LineNumberText";
            lineNumberColumn.Width = 50;
            //lineNumberColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(lineNumberColumn);

            GridViewImageColumn gvImageColumn = new GridViewImageColumn();
            gvImageColumn.Name = "EtatArtificeImage";
            gvImageColumn.HeaderText = "Etat";
            gvImageColumn.FieldName = "FireworkStateImage";
            gvImageColumn.Width = 50;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(gvImageColumn);

            GridViewTextBoxColumn etatArtificeColumn = new GridViewTextBoxColumn();
            etatArtificeColumn.Name = "EtatArtifice";
            etatArtificeColumn.HeaderText = "Etat";
            etatArtificeColumn.Width = 100;
            etatArtificeColumn.FieldName = "FireworkState";
            //etatArtificeColumn.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;

            //Permet de gérer l'état du tir
            //TODO : Changer le texte en dur pour que cela soit plus générique à l'avenir
            /*ConditionalFormattingObject c1 = new ConditionalFormattingObject("Tiré et terminé", ConditionTypes.Equal, "Tiré et terminé", "", true);
            c1.RowBackColor = Color.Green;
            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Tir imminent...", ConditionTypes.Equal, "Tir imminent...", "", true);
            c2.RowBackColor = Color.Orange;
            ConditionalFormattingObject c3 = new ConditionalFormattingObject("En cours...", ConditionTypes.Equal, "En cours...", "", true);
            c3.RowBackColor = Color.Red;
            ConditionalFormattingObject c4 = new ConditionalFormattingObject("!!! Tir échoué !!!", ConditionTypes.Equal, "!!! Tir échoué !!!", "", true);
            c4.RowBackColor = Color.Gray;

            etatArtificeColumn.ConditionalFormattingObjectList.Add(c1);
            etatArtificeColumn.ConditionalFormattingObjectList.Add(c2);
            etatArtificeColumn.ConditionalFormattingObjectList.Add(c3);
            etatArtificeColumn.ConditionalFormattingObjectList.Add(c4);*/

            //etatArtificeColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(etatArtificeColumn);

            GridViewTextBoxColumn designationColumn = new GridViewTextBoxColumn();
            designationColumn.Name = "Designation";
            designationColumn.HeaderText = "Désignation";
            designationColumn.FieldName = "Designation";
            designationColumn.Width = 350;
            // designationColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(designationColumn);

            GridViewTextBoxColumn miseAFeuArtificeColumn = new GridViewTextBoxColumn();
            miseAFeuArtificeColumn.Name = "MiseAFeuArtifice";
            miseAFeuArtificeColumn.HeaderText = "Mise à feu";
            miseAFeuArtificeColumn.FieldName = "MiseAFeuText";
            miseAFeuArtificeColumn.Width = 120;
            miseAFeuArtificeColumn.TextAlignment = ContentAlignment.MiddleCenter;
            //  miseAFeuArtificeColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(miseAFeuArtificeColumn);

            GridViewTextBoxColumn dureeArtificeColumn = new GridViewTextBoxColumn();
            dureeArtificeColumn.Name = "DureeAFeuArtifice";
            dureeArtificeColumn.HeaderText = "Durée\nde l'artifice";
            dureeArtificeColumn.FieldName = "DureeArtificeText";
            dureeArtificeColumn.TextAlignment = ContentAlignment.MiddleCenter;
            dureeArtificeColumn.Width = 120;
            // dureeArtificeColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(dureeArtificeColumn);

            GridViewTextBoxColumn dureeDepuisMiseAFeuColumn = new GridViewTextBoxColumn();
            dureeDepuisMiseAFeuColumn.Name = "DureeDepuisMiseAFeu";
            dureeDepuisMiseAFeuColumn.HeaderText = "Durée écoulée\ndepuis la mise à feu";
            dureeDepuisMiseAFeuColumn.FieldName = "DureeDepuisMiseAFeuText";
            dureeDepuisMiseAFeuColumn.TextAlignment = ContentAlignment.MiddleCenter;
            dureeDepuisMiseAFeuColumn.Width = 120;
            //  dureeDepuisMiseAFeuColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(dureeDepuisMiseAFeuColumn);

            GridViewTextBoxColumn adresseRecepteurColumn = new GridViewTextBoxColumn();
            adresseRecepteurColumn.Name = "AdresseRecepteur";
            adresseRecepteurColumn.HeaderText = "Liaison";
            adresseRecepteurColumn.FieldName = "ReceptorAddress.ReceptorAddressText";
            adresseRecepteurColumn.Width = 150;
            // adresseRecepteurColumn.ReadOnly = true;
            this.gvFireworkLaunch.MasterTemplate.Columns.Add(adresseRecepteurColumn);

            this.gvFireworkLaunch.TableElement.EndUpdate(false);
        }

        /// Raffraichit la datasource de la gridview Conception
        /// </summary>
        private void RefreshGridDataSource(bool rowAdded)
        {
            this.gvFireworkConception.DataSource = null;
            this.gvFireworkConception.DataSource = _firework.LigneList;
            this.gvFireworkConception.MasterTemplate.ExpandAll();

            if (rowAdded)
                this.gvFireworkConception.TableElement.ScrollToRow(gvFireworkConception.Rows.Last());

        }

        private void RefreshCombobox()
        {
            this.cbxReceptor.DataSource = null;
            this.cbxReceptor.DataSource = _firework.ReceptorList;
            this.cbxReceptor.DisplayMember = "ReceptorText";
        }

        #endregion

        #region Evénements

        /// <summary>
        /// Evénement qui se déclence lorsque l'XBee est connecté physiquement à l'ordinateur
        /// par l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _deviceManager_XBeeConnected(object sender, EventArgs e)
        {
            _syncContext.Post(new SendOrPostCallback((o) => { RefreshCommandBarButtonState(); }), null);
            _syncContext.Post(new SendOrPostCallback((o) => { InitializeFireworkTransmitter(); }), null);
        }

        /// <summary>
        /// Evénement qui se déclence lorsque l'XBee est déconnecté physiquement de l'ordinateur
        /// par l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _deviceManager_XBeeDisconnected(object sender, EventArgs e)
        {
            //Marche poa...
            //if (_firework.FireworkStatus == FireworkStatus.Paused ||
            //    _firework.FireworkStatus == FireworkStatus.Running)
            //{
            //    _firework.StopFireWork(false);
            //}

            _syncContext.Post(new SendOrPostCallback((o) => { RefreshCommandBarButtonState(); }), null);
            _syncContext.Post(new SendOrPostCallback((o) => { DestroyFireworkTransmitter(); }), null);
        }

        /// <summary>
        /// Se déclence lorsque le feu est terminé...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fireWorkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _firework.PropertyChanged -= Firework_PropertyChanged;
            RefreshCommandBarButtonState();
            lblProchainTir.Text = "";
        }

        /// <summary>
        /// Se déclenche pendant que le feu s'exécute...toutes les secondes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fireWorkThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {           
            LigneArtifice nextLigneArtifice = (LigneArtifice)e.UserState;

            string libelleProchainTir = string.Empty;

            if (nextLigneArtifice != null)
            {
                TimeSpan tempsAvantMiseAFeu = nextLigneArtifice.MiseAFeu.Subtract(_firework.DureeFireworkTimeSpan);
                string temps = tempsAvantMiseAFeu.Hours.ToString("00") + ":" + tempsAvantMiseAFeu.Minutes.ToString("00") + ":" + tempsAvantMiseAFeu.Seconds.ToString("00");
                libelleProchainTir = string.Format("PROCHAIN ARTIFICE : {0}, DEPART : {1}",
                                                    nextLigneArtifice.Designation, temps);
            }

            lblProchainTir.Text = libelleProchainTir;
            lblProchainTir.Update();

            RefreshCommandBarButtonState();
        }

        /// <summary>
        /// Thread qui gère le démarrage du feu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fireWorkThread_DoWork(object sender, DoWorkEventArgs e)
        {
            //Binding sur le temps du feu
            _firework.PropertyChanged += Firework_PropertyChanged;
            _firework.StartFireWork(_fireWorkThread);
            _firework.AutomaticFireworkLaunch();
        }

        /// <summary>
        /// Ajout d'une nouvelle ligne dans le tableau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewLigne_Click(object sender, EventArgs e)
        {
            _firework.AddNewLigne();
            RefreshGridDataSource(true);
        }

        private void gvFireworkConception_CommandCellClick(object sender, EventArgs e)
        {

            FireworkForm form = null;
            LigneArtifice ligneArtifice = null;

            switch (((GridCommandCellElement)sender).ColumnInfo.Name)
            {
                case "AjouterArtifice":
                    //Récupération de la ligne
                    Ligne ligne = (Ligne)(((GridCommandCellElement)sender).RowInfo.DataBoundItem);

                    form = new FireworkForm(_firework, new LigneArtifice(ligne.Numero), FireworkForm.Mode.Add);
                    form.ShowDialog(this);

                    if (form.OkButtonPressed)
                    {
                        //Ajout de la sous-ligne dans la grille
                        _firework.AddNewSousLigne(ligne, form.LigneArtifice);

                        this.gvFireworkConception.Templates[0].Refresh();
                        this.gvFireworkConception.MasterTemplate.ExpandAll();

                        //On raffrachit la liste des boitiers
                        RefreshCombobox();
                    }
                    break;

                case "ModifierArtifice":
                    ligneArtifice = (LigneArtifice)(((GridCommandCellElement)sender).RowInfo.DataBoundItem);

                    form = new FireworkForm(_firework, ligneArtifice, FireworkForm.Mode.Modify);
                    form.ShowDialog(this);

                    if (form.OkButtonPressed)
                    {
                        this.gvFireworkConception.Templates[0].Refresh();
                        this.gvFireworkConception.MasterTemplate.ExpandAll();

                        //On raffrachit la liste des boitiers
                        RefreshCombobox();
                    }
                    break;

                case "SupprimerArtifice":
                    ligneArtifice = (LigneArtifice)(((GridCommandCellElement)sender).RowInfo.DataBoundItem);

                    if (MessageBox.Show("Voulez-vous vraiment supprimer cet artifice ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        _firework.DeleteFirework(ligneArtifice);

                        this.gvFireworkConception.Templates[0].Refresh();
                        this.gvFireworkConception.MasterTemplate.ExpandAll();

                        //On raffrachit la liste des boitiers
                        RefreshCombobox();
                    }

                    break;
            }

            if (!_firework.IsFireworkOperational())
            {
                MessageBox.Show("La définition du feu d'artifice est incorrect\nRevoir les mises à feu\nCe feu ne pourra pas être tiré", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Gestion du bouton de lancement du feu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFire_Click(object sender, EventArgs e)
        {
            if (!_fireWorkThread.IsBusy)
            {
                if (_firework.IsFireworkOperational())
                {
                    _fireWorkThread.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("La définition du feu d'artifice est incorrect", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Gestion du bouton d'arrêt du feu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Arrêt du feu demandé ! Cette action est irréversible\n\nVoulez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (_fireWorkThread.WorkerSupportsCancellation)
                {
                    _fireWorkThread.CancelAsync();
                }
            }
        }

        /// <summary>
        /// Gestion du bouton Pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_firework.FireworkStatus == FireworkStatus.Running)
            {
                _firework.PauseFireWork();
                return;
            }

            if (_firework.FireworkStatus == FireworkStatus.Paused)
            {
                _firework.ContinueFireWork();
                return;
            }
        }

        /// <summary>
        /// Gestion du bouton Test de la connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConnexion_Click(object sender, EventArgs e)
        {
            TestForm form = new TestForm(_firework);
            form.Show(this);
        }


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "tabLaunch")
            {
                this.gvFireworkLaunch.DataSource = null;
                this.gvFireworkLaunch.DataSource = _firework.GetLigneArtificeList();
                this.gvFireworkLaunch.MasterTemplate.ExpandAll();
            }
        }

        /// <summary>
        /// Chargement d'un feu d'artifice à partir d'un fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_firework.IsLoaded)
            {
                if (MessageBox.Show("Un feu est déjà en cours d'édition\n\nVoulez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    InitializeNewFirework();
                }
                else
                {
                    return;
                }
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Fichier kQuatre (*.k4)|*.k4";

            DialogResult dr = ofd.ShowDialog(this);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    LoadFirework(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Erreur lors de la lecture du fichier de définition du feu d'artifice", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        /// <summary>
        /// Initialise un nouveau feu d'artifice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_firework.IsLoaded)
            {
                if (MessageBox.Show("Un feu est déjà en cours d'édition\n\nVoulez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    InitializeNewFirework();
                }
            }
        }

        private void InitializeNewFirework()
        {
            //On reset le feu
            _firework.InitNewFireWork();
            //On raffrachit la datasource
            RefreshGridDataSource(false);
            //On définit le panneau par defaut
            tabControl.SelectTab("tabDesign");
            //On raffrachit le panneau de controle
            RefreshCommandBarButtonState();
            //On raffrachit les boitiers
            RefreshCombobox();
        }

        private void btnReinit_Click(object sender, EventArgs e)
        {
            //Reset du feu
            _firework.ResetFireWork();
            //On raffrachit le panneau de controle
            RefreshCommandBarButtonState();
        }

        /// <summary>
        /// Sauvegarde du feu dans un fichier XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Fichier kQuatre (*.k4)|*.k4";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _firework.SaveFirework(sfd.FileName);
            }
        }

        private void btnFireNextFirework_Click(object sender, EventArgs e)
        {
            _firework.FireNextFireworkNow();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evenement déclenché lors de la femerture de la main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Voulez-vous vraiment quitter cet outil merveilleux ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            //Libère la mémoire
            FreeMemory();
        }

        private void FreeMemory()
        {
            _firework.PropertyChanged -= Firework_PropertyChanged;
            _firework = null;

            _fireWorkThread.DoWork -= _fireWorkThread_DoWork;
            _fireWorkThread.ProgressChanged -= _fireWorkThread_ProgressChanged;
            _fireWorkThread.RunWorkerCompleted -= _fireWorkThread_RunWorkerCompleted;
            _fireWorkThread.Dispose();
            _fireWorkThread = null;

            //Evite que le process kQuatre reste dans la liste des process
            _deviceManager.XBeeConnected -= _deviceManager_XBeeConnected;
            _deviceManager.XBeeDisconnected -= _deviceManager_XBeeDisconnected;
            _deviceManager.Dispose();
            _deviceManager = null;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //TODO : En attendant de comprendre pourquoi l'appli reste dans les process à cause de FTDI mal fermé
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManuelLaunchForm form = new ManuelLaunchForm(_firework);
            form.ShowDialog(this);
        }
    }
}
