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
using Guiet.kQuatre.Business;
using Guiet.kQuatre.UI.UserControl;

namespace Guiet.kQuatre.UI
{
    public partial class ManuelLaunchForm : Form
    {
        private Firework _firework;
        private bool _boardActivated = false;
        //Pour la mise à jour de l'interface graphique depuis un autre thread
        private SynchronizationContext _syncContext = null;

        public ManuelLaunchForm(Firework firework)
        {
            InitializeComponent();

            //Récupération du contexte courant
            _syncContext = WindowsFormsSynchronizationContext.Current;

            _firework = firework;            

            LoadFiringBoard();

            ChangeOnOffButtonState();            
        }

        public void LoadFiringBoard() 
        {
            int x = 10;
            int y = 10;
            int nbLargeur = 5;
            int cptLargeur = 0;

            //On regroupe les artifices qui doivent partir en même temps
            var results = from la in _firework.GetLigneArtificeList()
                                          group la by la.MiseAFeu.TotalSeconds into g
                                          select new { MiseAFeu = g.Key };

            foreach (var firework in results)
            {
                List<LigneArtifice> laList = _firework.GetLigneByMiseAFeu(firework.MiseAFeu);                

                //Création d'un nouveau bouton de lancement
                FireworkUserControl userControl = new FireworkUserControl(laList);
                userControl.FireworkClicked += UserControl_FireworkClicked;
                userControl.Location = new Point(x, y);
                pnlBoard.Controls.Add(userControl);

                if (cptLargeur == nbLargeur - 1)
                {
                    cptLargeur = 0;
                    y += userControl.Height + 10;
                    x = 10;
                }
                else
                {
                    x += userControl.Width + 10;
                    cptLargeur++;
                }

            }            
        }

        private void UserControl_FireworkClicked(FireworkClickedEventArgs fireworkClicked)
        {
            if (_firework.FireworkStatus != FireworkStatus.Running)
            {
                _firework.StartFireWork(null);
                _firework.PropertyChanged += Firework_PropertyChanged;
            }

            _firework.FireManually(fireworkClicked.LigneArtificeList);
        }

        public void Firework_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Firework && e.PropertyName == "DureeFirework")
            {
                _syncContext.Post(new SendOrPostCallback((o) => { UpdateDureeFirework(); }), null);
            }
        }

        private void UpdateDureeFirework()
        {
            lblDureeFeu.Text = _firework.DureeFirework;
            lblDureeFeu.Update();
        }

        private void btnOnOff_Click(object sender, EventArgs e)
        {
            if (_boardActivated)
            {
                if (_firework.FireworkStatus == FireworkStatus.Running)
                {
                    StopFireWork();
                }

                _boardActivated = false;
            }
            else
            {
                _boardActivated = true;
            }

            ChangeOnOffButtonState();
        }

        private void StopFireWork()
        {
            _firework.StopFireWork(true);
            _firework.PropertyChanged -= Firework_PropertyChanged;
        }

        private void ChangeOnOffButtonState()
        {
            if (_boardActivated)
            {
                btnOnOff.Text = "ON";
                btnOnOff.BackColor = Color.Green;

                foreach (FireworkUserControl fuc in pnlBoard.Controls)
                {
                    fuc.SetState(true);
                }
            }
            else
            {
                btnOnOff.Text = "OFF";
                btnOnOff.BackColor = Color.Red;

                foreach (FireworkUserControl fuc in pnlBoard.Controls)
                {
                    fuc.SetState(false);
                }
            }
        }

        private void ManuelLaunchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO : Attention un feu est en cours...voulez vous contiuer...
        }

    }
}
