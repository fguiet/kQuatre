using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guiet.kQuatre.Business;
using System.Threading;

namespace Guiet.kQuatre.UI.UserControl
{   
    public partial class FireworkUserControl : System.Windows.Forms.UserControl
    {
        public event FireworkClickedEventHandler FireworkClicked;

        public delegate void FireworkClickedEventHandler(FireworkClickedEventArgs FireworkClicked); 
        
        private List<LigneArtifice> _ligneArtificeList = null;
        //Pour la mise à jour de l'interface graphique depuis un autre thread
        private SynchronizationContext _syncContext = null;

        private bool _isActivated = false;

        public FireworkUserControl(List<LigneArtifice> ligneArtificeList)
        {
            InitializeComponent();

            //Récupération du contexte courant
            _syncContext = WindowsFormsSynchronizationContext.Current;

            _ligneArtificeList = ligneArtificeList;            
            
            //TODO : Gestion du temps écoulé depuis la mise à feu
            _ligneArtificeList[0].PropertyChanged += FireworkUserControl_PropertyChanged;

            InitializeControl();

            this.Click += UserControl_Click;

            AddClickEvent(this);
        }

        private void UpdateFireworkState(LigneArtifice la)
        {
            pbStatut.Image = la.FireworkStateImage;
            lblStatut.Text = la.FireworkState;            
        }

        private void UpdateDureeDepuisMiseAFeu(LigneArtifice la)
        {
            lblDureeDepuisMiseAFeu.Text = la.DureeDepuisMiseAFeuText;
        }

        public void FireworkUserControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is LigneArtifice && (e.PropertyName == "FireworkStateImage" || e.PropertyName == "FireworkState"))
            {
                _syncContext.Post(new SendOrPostCallback((o) => { UpdateFireworkState((LigneArtifice)sender); }), null);
            }

            if (sender is LigneArtifice && e.PropertyName == "DureeDepuisMiseAFeuText")
            {
                _syncContext.Post(new SendOrPostCallback((o) => { UpdateDureeDepuisMiseAFeu((LigneArtifice)sender); }), null);
            }
        }

        public void SetState(bool activated)
        {
            _isActivated = activated;
        }

        private void InitializeControl() 
        {
            lblDesignation.Text = string.Empty;
            lblMiseAFeu.Text = string.Empty;
            lblLigneNumber.Text = string.Empty;            

            foreach (LigneArtifice la in _ligneArtificeList)
            {                
                if (string.IsNullOrEmpty(lblDesignation.Text))
                    lblDesignation.Text = la.Designation;
                else
                    lblDesignation.Text = lblDesignation.Text + Environment.NewLine + la.Designation;

                if (string.IsNullOrEmpty(lblLigneNumber.Text))
                    lblLigneNumber.Text = la.LineNumberText;
                else
                    lblLigneNumber.Text = lblLigneNumber.Text + ", " + la.LineNumberText;                
            }

            lblMiseAFeu.Text = _ligneArtificeList[0].MiseAFeuText;
            lblDuree.Text = _ligneArtificeList[0].DureeArtificeText;           
        }        

        private void AddClickEvent(Control parentCtrl)
        {
            foreach (Control c in parentCtrl.Controls)
            {
                c.Click += UserControl_Click;
                AddClickEvent(c);
            }
        }

        private void UserControl_Click(object sender, EventArgs e)
        {
            if (!_isActivated)
            {
                MessageBox.Show("La console n'est pas allumée. Impossible de tirer ce feu d'artifice", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FireworkClicked != null)
            {
                FireworkClicked(new FireworkClickedEventArgs(_ligneArtificeList));
            }
        }
    }

    public class FireworkClickedEventArgs : EventArgs
    {
        List<LigneArtifice> _ligneArtificeList = null;

        public FireworkClickedEventArgs(List<LigneArtifice> ligneArtificeList)
        {
            _ligneArtificeList = ligneArtificeList;
        }

        public List<LigneArtifice> LigneArtificeList
        {
            get
            {
                return _ligneArtificeList;
            }
        }
    }

}
