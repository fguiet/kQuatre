using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public class LigneArtifice : INotifyPropertyChanged
    {
      
        //private Artifice _artifice = null;
        private string _designation;
        // private int _quantite;
        private TimeSpan _miseAFeu;
        private TimeSpan _dureeArtifice;
        private EtatArtifice _etatArtifice = EtatArtifice.StandBy;
        private Image _fireworkStateImage = Guiet.kQuatre.Properties.Resources.standby;
        private Image _resistanceStateImage = Guiet.kQuatre.Properties.Resources.standby;
        private Stopwatch _dureeDepuisMiseAFeu = new Stopwatch();
        private string _dureeDepuisMiseAFeuText = "00:00";
        private ReceptorAddress _receptorAddress = null;
        private int _lineNumber;
        private string _resistance;

        public event PropertyChangedEventHandler PropertyChanged;

        public LigneArtifice(int lineNumber, string designation, TimeSpan miseAFeu, TimeSpan duree, ReceptorAddress ra)
        {
            _lineNumber = lineNumber;
            _designation = designation;
            _miseAFeu = miseAFeu;
            _dureeArtifice = duree;

            ReceptorAddress = ra;
        }
        public LigneArtifice(int lineNumber)
        {
            _lineNumber = lineNumber;
        }

        public Image FireworkStateImage
        {
            get
            {
                return _fireworkStateImage;
            }
        }

        public Image ResistanceStateImage
        {
            get
            {
                return _resistanceStateImage;
            }
        }

        public string Resistance
        {
            set
            {
                _resistance = value;
                ChangeResistanceState();
                OnPropertyChanged("Resistance");
            }

            get
            {
                return _resistance;
            }
        }

        public void SetLaunchedFailed()
        {
            ChangeFireworkState(Business.EtatArtifice.LaunchFailed);
        }

        /// <summary>
        /// Indique la ligne a été tirée
        /// </summary>
        public void Fire()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000); //Toutes les secondes
            timer.Elapsed += timer_Elapsed;

            ChangeFireworkState(Business.EtatArtifice.InProgress);

            //_dureeDepuisMiseAFeu = new Stopwatch();
            _dureeDepuisMiseAFeu.Start();
            timer.Start();

            while (_dureeDepuisMiseAFeu.Elapsed.TotalSeconds <= _dureeArtifice.Seconds) ;

            //Pour indiquer la bonne durée...sinon il manque une seconde...
            DureeDepuisMiseAFeuText = string.Format("{0}:{1}", _dureeDepuisMiseAFeu.Elapsed.Minutes.ToString("00"), _dureeDepuisMiseAFeu.Elapsed.Seconds.ToString("00"));

            timer.Stop();
            _dureeDepuisMiseAFeu.Stop();

            ChangeFireworkState(Business.EtatArtifice.Finished);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateDureeDepuisMiseAFeu();
            //DureeDepuisMiseAFeuText = string.Format("{0}:{1}", _dureeDepuisMiseAFeu.Elapsed.Minutes.ToString("00"), _dureeDepuisMiseAFeu.Elapsed.Seconds.ToString("00"));
        }

        public string DureeDepuisMiseAFeuText
        {
            private set
            {
                _dureeDepuisMiseAFeuText = value;
                OnPropertyChanged("DureeDepuisMiseAFeuText");
            }
            get
            {
                return _dureeDepuisMiseAFeuText;
            }
        }

        private void ChangeResistanceState()
        {            
            float res;
            if (float.TryParse(_resistance,NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"),  out res)) 
            {
                if (res > 0)
                {
                    _resistanceStateImage = Guiet.kQuatre.Properties.Resources.done;
                }
                else
                {
                    _resistanceStateImage = Guiet.kQuatre.Properties.Resources.cross;
                }
            }
            else
            
            {
                _resistanceStateImage = Guiet.kQuatre.Properties.Resources.cross;
            }

            OnPropertyChanged("ResistanceStateImage");
        }

        /// <summary>
        /// Change l'état de la ligne du feu d'artifice
        /// </summary>
        /// <param name="newState"></param>
        private void ChangeFireworkState(Business.EtatArtifice newState)
        {
            _etatArtifice = newState;

            switch (_etatArtifice)
            {
                case Business.EtatArtifice.Finished:
                    _fireworkStateImage = Guiet.kQuatre.Properties.Resources.done;
                    break;

                case Business.EtatArtifice.ImminentLaunch:
                    _fireworkStateImage = Guiet.kQuatre.Properties.Resources.warning;
                    break;

                case Business.EtatArtifice.InProgress:
                    _fireworkStateImage = Guiet.kQuatre.Properties.Resources.inprogress;
                    break;

                case Business.EtatArtifice.LaunchFailed:
                    _fireworkStateImage = Guiet.kQuatre.Properties.Resources.cross;
                    break;

                case Business.EtatArtifice.StandBy:
                    _fireworkStateImage = Guiet.kQuatre.Properties.Resources.standby;
                    break;

            }


            OnPropertyChanged("FireworkState");
            OnPropertyChanged("FireworkStateImage");
        }

        /// <summary>
        /// Indique que la mise à feu à changer  (pour mettre à jour la gridview)
        /// </summary>
        /// <param name="newState"></param>
        private void FireworkIgnitionChanged()
        {
            OnPropertyChanged("MiseAFeuText");
        }


        private void UpdateDureeDepuisMiseAFeu()
        {
            DureeDepuisMiseAFeuText = string.Format("{0}:{1}", _dureeDepuisMiseAFeu.Elapsed.Minutes.ToString("00"), _dureeDepuisMiseAFeu.Elapsed.Seconds.ToString("00"));
        }

        public EtatArtifice State
        {
            get
            {
                return _etatArtifice;
            }
        }

        public string FireworkState
        {
            get
            {
                if (_etatArtifice == Business.EtatArtifice.Finished)
                    return "Tiré et terminé";

                if (_etatArtifice == Business.EtatArtifice.ImminentLaunch)
                    return "Tir imminent...";

                if (_etatArtifice == Business.EtatArtifice.InProgress)
                    return "En cours...";

                if (_etatArtifice == Business.EtatArtifice.LaunchFailed)
                    return "!!! Tir échoué !!!";

                if (_etatArtifice == Business.EtatArtifice.StandBy)
                    return "En attente...";

                return "Etat inconnu";
            }
        }

        public string LineNumberText
        {
            get
            {
                return _lineNumber.ToString();
            }
        }

        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
        }

        /// <summary>
        /// Libère l'adresse du recepteur pour un autre feu d'artifice
        /// </summary>
        public void FreeReceptorAddress() 
        {
            _receptorAddress.SetFree();
        }

        public ReceptorAddress ReceptorAddress
        {
            set
            {
                //Est ce que l'adresse n'est pas déjà utilisé?
                if (value.IsReserved)
                {
                    throw new Exception("Impossible d'utiliser cette adresse. Elle est déjà réservée");
                }

                //Est ce qu'on change l'affectation ?
                if (_receptorAddress != null && _receptorAddress != value)
                {
                    //On libère l'ancienne ligne
                    FreeReceptorAddress();
                }                

                _receptorAddress = value;
                //On réserve la ligne
                _receptorAddress.SetReserved();
            }

            get
            {
                return _receptorAddress;
            }
        }

        public EtatArtifice EtatArtifice
        {
            get
            {
                return _etatArtifice;
            }
            set
            {
                ChangeFireworkState(value);
            }
        }

        public TimeSpan DureeArtifice
        {
            set
            {
                _dureeArtifice = value;
            }

            get
            {
                return _dureeArtifice;
            }
        }

        public string Designation
        {
            set
            {
                _designation = value;
            }

            get
            {
                return _designation;
            }
        }


        public string DureeArtificeText
        {
            get
            {
                return string.Format("{0}:{1}:{2}", _dureeArtifice.Hours.ToString("00"), _dureeArtifice.Minutes.ToString("00"), _dureeArtifice.Seconds.ToString("00"));
            }
        }

        public TimeSpan MiseAFeu
        {
            set
            {
                _miseAFeu = value;
                FireworkIgnitionChanged();
            }

            get
            {
                return _miseAFeu;
            }
        }

        public string MiseAFeuText
        {
            get
            {
                return string.Format("{0}:{1}:{2}", _miseAFeu.Hours.ToString("00"), _miseAFeu.Minutes.ToString("00"), _miseAFeu.Seconds.ToString("00"));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Reset()
        {
            _dureeDepuisMiseAFeu.Reset();
            UpdateDureeDepuisMiseAFeu();
            ChangeFireworkState(Business.EtatArtifice.StandBy);
        }
    }
}
