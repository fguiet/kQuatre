using Guiet.kQuatre.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guiet.kQuatre.UI
{
    public partial class FireworkForm : Form
    {
        #region Membres privés

        private bool _cancelButtonPressed = false;
        private bool _okButtonPressed = false;
        private LigneArtifice _ligneArtificeModel = null;
        private Firework _firework = null;
        private Mode _mode;

        public enum Mode
        {
            Add,
            Modify
        }

        #endregion

        public LigneArtifice LigneArtifice
        {
            get
            {
                return _ligneArtificeModel;
            }
        }

        public FireworkForm(Firework firework, LigneArtifice ligneArtifice, Mode mode)
        {
            InitializeComponent();

            _ligneArtificeModel = ligneArtifice;
            _firework = firework;
            _mode = mode;

            UpdateViewModel(false);
        }

        private void UpdateViewModel(bool viewToModel) 
        {
            if (viewToModel)
            {
                //Mise à jour du modèle
                _ligneArtificeModel.Designation = tbxDesignation.Text;
                _ligneArtificeModel.MiseAFeu = new TimeSpan(dtpMiseAFeu.Value.Hour, dtpMiseAFeu.Value.Minute, dtpMiseAFeu.Value.Second);
                _ligneArtificeModel.DureeArtifice = new TimeSpan(dtpDuree.Value.Hour, dtpDuree.Value.Minute, dtpDuree.Value.Second);

                //A t-on changer l'adresse ?
                if (_ligneArtificeModel.ReceptorAddress != (ReceptorAddress)cbxFreeReceptorAdresses.SelectedItem)
                    _ligneArtificeModel.ReceptorAddress = (ReceptorAddress)cbxFreeReceptorAdresses.SelectedItem;                                
            }
            else
            {

                lblLineNumber.Text = _ligneArtificeModel.LineNumberText;
                //Mis à jour de la vue
                tbxDesignation.Text = _ligneArtificeModel.Designation;
                
                LigneArtifice lastAdded = _firework.GetLastLigneArtifice();
                DateTime foo = new DateTime(1900, 1, 1, 0, 0, 0);
                dtpMiseAFeu.Value = foo.Add(_ligneArtificeModel.MiseAFeu);                    

                if (lastAdded != null)
                {                 
                    //Est ce qu'on ajoute un artifice sur la derniere ligne ?
                    if (lastAdded.LineNumberText == _ligneArtificeModel.LineNumberText && _mode == Mode.Add)
                    {
                        //Initialisation du temps avec la ligne précédente
                        foo = new DateTime(1900, 1, 1, 0, 0, 0);                        
                        TimeSpan computeNextMiseAFeu = lastAdded.MiseAFeu + lastAdded.DureeArtifice;
                        dtpMiseAFeu.Value = foo.Add(computeNextMiseAFeu);                 
                    }
                }                

                DateTime foo1 = new DateTime(1900, 1, 1, 0, 0, 0);
                dtpDuree.Value = foo1.Add(_ligneArtificeModel.DureeArtifice);

                foreach (ReceptorAddress ra in _firework.FreeReceptorAddresses)
                {
                    cbxFreeReceptorAdresses.Items.Add(ra);
                }                
                cbxFreeReceptorAdresses.DisplayMember = "ReceptorAddressText";

                //Ajout de la ligne 
                if (_ligneArtificeModel.ReceptorAddress != null)
                {
                    cbxFreeReceptorAdresses.Items.Add(_ligneArtificeModel.ReceptorAddress);
                    cbxFreeReceptorAdresses.SelectedItem = _ligneArtificeModel.ReceptorAddress;
                }

            }
        }

        public bool CancelButtonPressed
        {
            get
            {
                return _cancelButtonPressed;
            }
        }

        public bool OkButtonPressed
        {
            get
            {
                return _okButtonPressed;
            }
        }     

        private bool IsFormValid()
        {
            bool isOk = true;
            errorProvider.Clear();

            if (string.IsNullOrEmpty(tbxDesignation.Text))
            {
                isOk = false;
                errorProvider.SetError(tbxDesignation, "La désignation est obligatoire");
            }

            if (cbxFreeReceptorAdresses.SelectedItem == null)
            {
                isOk = false;
                errorProvider.SetError(cbxFreeReceptorAdresses, "Le choix d'un boitier et relaie est obligatoire");
            }

            if (dtpDuree.Value.Hour == 0 && dtpDuree.Value.Minute == 0 && dtpDuree.Value.Second == 0)
            {
                isOk = false;
                errorProvider.SetError(dtpDuree, "La durée de l'artifice doit être renseignée");
            }

            return isOk;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                _okButtonPressed = true;
                UpdateViewModel(true);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelButtonPressed = true;
            this.Close();
        }
    }
}
