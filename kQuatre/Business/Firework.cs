using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Guiet.kQuatre.Business
{
    public class Firework : INotifyPropertyChanged
    {
        #region Propriétés privées

        private ThreadedBindingList<Ligne> _ligneList = new ThreadedBindingList<Ligne>();
        private List<Receptor> _receptorList = new List<Receptor>();
        private List<Thread> _threadList = new List<Thread>();
        private List<LigneArtifice> _nextLigneArtiToLaunch;

        public List<Receptor> ReceptorList
        {
            get { return _receptorList; }
            set { _receptorList = value; }
        }

        /// <summary>
        /// Renvoie toutes les adresses disponibles de tous les boitiers
        /// </summary>
        public List<ReceptorAddress> FreeReceptorAddresses
        {
            get
            {
                List<ReceptorAddress> raList = new List<ReceptorAddress>();
                foreach (Receptor r in _receptorList)
                {
                    raList.AddRange(r.FreeReceptorAddresses);
                }

                return raList;
            }
        }

        private string _nomFeu = string.Empty;
        //private DateTime _dateFeu;
        private int _messageRetry = 0; //Nb de fois ou l'on essait de renvoyer le message

        //Chronomètre du feux
        private Stopwatch _chronoFirework = null;
        //Durée totale
        private Stopwatch _chronoTotal = null;
        private System.Timers.Timer _timer = null;
        private BackgroundWorker _worker = null;
        private bool _isRunning = false;
        private bool _isPaused = false;
        private Emetteur _emetteur = null;
        private bool _emitterConnected = false;
        private bool _fireworkStoppedByUser = false;
        private bool _rssiReadRequested = false;

        #endregion

        #region Constructeur

        public Firework()
        {
            _messageRetry = Convert.ToInt32(ConfigurationManager.AppSettings["firework.message.retry"]);
        }

        #endregion

        #region Propriétés

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Indique si le feu a été tiré
        /// </summary>
        public FireworkStatus FireworkStatus
        {
            //Running, //En cours de fonctionnement
            //StoppedAndNeverLaunched, //Arrêté mais jamais lancé
            //StoppedByUserBeforeEnd, //Arrêté et l'utilisateur a lancé le feu
            //Paused, //En pause
            //StoppedAndFinished //Arrêté et fini complètement

            get
            {
                if (_isPaused)
                    return Business.FireworkStatus.Paused;

                if (_isRunning)
                    return Business.FireworkStatus.Running;
                else
                {
                    //ici le feu est stoppé
                    //Feu arrêté par l'utilisateur?
                    if (_fireworkStoppedByUser)
                        return Business.FireworkStatus.StoppedByUserBeforeEnd;
                    //Feu fini?
                    if (IsFireworkFinished() && IsLoaded)
                    {
                        return Business.FireworkStatus.StoppedAndFinished;
                    }

                    //Le feu n'a pas démarré
                    return Business.FireworkStatus.StoppedAndNeverLaunched;
                }


                throw new Exception("Impossible de déterminer le status du feu");
            }
        }

        public bool RssiReadRequested
        {
            get
            {
                return _rssiReadRequested;
            }
        }

        public bool IsLoaded
        {
            get
            {
                return (_ligneList.Count > 0 || _receptorList.Count > 0);
            }
        }

        public ThreadedBindingList<Ligne> LigneList
        {
            get
            {
                return _ligneList;
            }
        }

        public bool EmitterConnected
        {
            get
            {
                return _emitterConnected;
            }
        }

        public TimeSpan DureeFireworkTimeSpan
        {
            get
            {
                return _chronoFirework.Elapsed;
            }
        }

        public string DureeFirework
        {
            get
            {
                if (_chronoFirework == null)
                    return "00:00";
                else
                    return string.Format("{0}:{1}", _chronoFirework.Elapsed.Minutes.ToString("00"), _chronoFirework.Elapsed.Seconds.ToString("00"));
            }
        }

        public string DureeTotale
        {
            get
            {
                if (_chronoTotal == null)
                    return "00:00";
                else
                    return string.Format("{0}:{1}", _chronoTotal.Elapsed.Minutes.ToString("00"), _chronoTotal.Elapsed.Seconds.ToString("00"));
            }
        }

        #endregion

        #region Membres privées

        /// <summary>
        /// Création d'une nouvelle ligne avec attribution automatique du numéro
        /// </summary>
        /// <returns></returns>
        private Ligne CreateNewLigne()
        {
            if (_ligneList.Count == 0) return new Ligne(1);

            int currentLigneNumber = (from ll in _ligneList
                                      select ll.Numero).Max();

            return new Ligne(currentLigneNumber + 1);

        }
        private Ligne CreateNewLigne(int number)
        {
            return new Ligne(number);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Rapporte le status du feux tous les secondes...
            ReportStatus();
        }

        private void ReportStatus()
        {
            LigneArtifice nextLigne = null;
            //TODO : Attention il peut y avoir plusieurs artifices ici => concatenation et utilisation de liste plutot
            if (_nextLigneArtiToLaunch != null && _nextLigneArtiToLaunch.Count >= 1)
                nextLigne = _nextLigneArtiToLaunch[0];

            if (_worker != null)
            {
                _worker.ReportProgress(0, nextLigne);

            }

            OnPropertyChanged("DureeFirework");
            OnPropertyChanged("DureeTotale");
        }

        /// <summary>
        /// Tir de façon manuel le feu
        /// </summary>
        /// <param name="la"></param>
        public void FireManually(List<LigneArtifice> laList)
        {
            FireFirework(laList);
        }

        /// <summary>
        /// Gestion du feu automatique!!
        /// </summary>
        public void AutomaticFireworkLaunch()
        {
            List<Thread> artificeThreadList = new List<Thread>();
            bool userStoppedFirework = false;

            while (!IsFireworkFinished())
            {

                //Prochain tir! (plurieurs artifice peuvent être retournés si ils sont tirés en même temps
                _nextLigneArtiToLaunch = GetAndPrepareNextArtificeToLaunch();

                if (_nextLigneArtiToLaunch.Count == 0)
                {
                    //Plus de ligne à tirer!!
                    //On attend que la derniere ligne soit fini...
                    continue;
                }

                while (_isPaused) ; //Si le feu est en pause on attend ici

                //On attend la prochaine mise à feu
                while (_chronoFirework.Elapsed.TotalSeconds <= _nextLigneArtiToLaunch[0].MiseAFeu.TotalSeconds)
                {
                    if (_worker.CancellationPending)
                    {
                        userStoppedFirework = true;
                        //L'utilisateur a demander l'arret du feu
                        break;
                    }
                };

                if (userStoppedFirework) break;

                FireFirework(_nextLigneArtiToLaunch);
            }

            //On laisse le temps à la GUI de se raffrachir...
            Thread.Sleep(1000);

            StopFireWork(userStoppedFirework);
        }

        /// <summary>
        /// Tire les artifices qui sont prets
        /// </summary>
        /// <param name="laList"></param>
        private void FireFirework(List<LigneArtifice> laList)
        {
            bool allLinesLaunched = false;
            while (!allLinesLaunched)
            {
                foreach (Receptor r in _receptorList)
                {
                    //Regroupement des artifices à tirer qui sont sur le même boitiers
                    List<LigneArtifice> sameReceptorLines = (from srl in laList
                                                             where 
                                                             //srl.EtatArtifice == EtatArtifice.ImminentLaunch
                                                                srl.ReceptorAddress.MacAddress == r.MacAddress
                                                             select srl).ToList();
                    //Preparation du message 
                    string receptorMessage = string.Format("FIRE;{0}", sameReceptorLines.Count.ToString());
                    foreach (LigneArtifice la in sameReceptorLines)
                    {
                        receptorMessage = string.Format("{0};{1}", receptorMessage, la.ReceptorAddress.RelayNumber.ToString());
                    }

                    //Debug.WriteLine(receptorMessage);

                    int cpt = 0;
                    //Par défaut, le message n'a pas été envoyé correctement
                    bool messageSentSuccess = false;
                    while (cpt <= _messageRetry)
                    {
                        //Envoi du message au receiver
                        bool responseStatut = _emetteur.SendFireMessage(r.MacAddress, receptorMessage);

                        //Attente de la réponse...
                        //while (_emetteur.IsLastResponseStatusSuccess == null) ;

                        //Vérification de la reponse
                        if (responseStatut)
                        {
                            messageSentSuccess = true;
                            break; //On sort de la boucle
                        }

                        cpt++;
                    }

                    //Debug.WriteLine("Response reçue");

                    foreach (LigneArtifice la in sameReceptorLines)
                    {
                        if (messageSentSuccess)
                        {

                            Thread t = new Thread(() => la.Fire());
                            t.Start();
                            t.IsBackground = true;
                            _threadList.Add(t);
                        }
                        else
                        {
                            //Le tir a échoué...message non reçu...
                            la.SetLaunchedFailed();
                        }
                    }
                }

                //Tous les artifices sont tirés!
                allLinesLaunched = true;
            }
        }

        /// <summary>
        /// Récupère un récepteur via son adresse MAC        
        /// </summary>
        /// <param name="macAddress"></param>
        private Receptor GetReceptor(string macAddress)
        {
            Receptor receptor = (from r in _receptorList
                                 where r.MacAddress == macAddress
                                 select r).FirstOrDefault();

            if (receptor == null)
            {
                throw new Exception("Impossible de trouver le récepteur");
            }

            return receptor;
        }


        #endregion

        #region Membres publics

        /// <summary>
        /// Récupère la derniere ajouté au feu
        /// </summary>
        public LigneArtifice GetLastLigneArtifice()
        {
            return GetLigneArtificeList().LastOrDefault();
        }

        public LigneArtifice GetLigneArtificeById(string macAddress, int relayNumber)
        {
            var a = (from la in GetLigneArtificeList()
                     where la.ReceptorAddress.RelayNumber == relayNumber
                        && la.ReceptorAddress.MacAddress == macAddress
                     select la).Single();

            return a;
        }

        public List<LigneArtifice> GetLigneByMiseAFeu(double miseAFeuInSeconds)
        {
            var a = (from la in GetLigneArtificeList()
                     where la.MiseAFeu.TotalSeconds == miseAFeuInSeconds
                     select la).ToList();

            return a;
        }

        public ThreadedBindingList<LigneArtifice> GetLigneArtificeList()
        {
            ThreadedBindingList<LigneArtifice> laList = new ThreadedBindingList<LigneArtifice>();
            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    laList.Add(la);
                }
            }

            return laList;
        }

        public ThreadedBindingList<LigneArtifice> GetLigneArtificeByReceptorList(Receptor r)
        {
            ThreadedBindingList<LigneArtifice> laList = new ThreadedBindingList<LigneArtifice>();
            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    if (la.ReceptorAddress.MacAddress == r.MacAddress)
                        laList.Add(la);
                }
            }

            return laList;
        }

        public void DetroyTransmitter()
        {
            _emetteur.FermeEmetteur();
            _emetteur.MessageReceived -= Emetteur_MessageReceived;
            _emitterConnected = false;
            _emetteur = null;
        }

        public void InitializeTransmitter(string COMPort)
        {
            if (_emetteur == null)
            {
                _emetteur = new Emetteur(COMPort);
                _emetteur.OuvreEmetteur();
                _emetteur.MessageReceived += Emetteur_MessageReceived;
                _emitterConnected = true;
            }
        }

        public int? ReadRssi()
        {
            _rssiReadRequested = false;

            return _emetteur.Rssi;
        }

        private void Emetteur_MessageReceived(MessageReceivedEventArgs messageReceivedArgs)
        {
            switch (messageReceivedArgs.MessageType)
            {
                case Emetteur.MessageType.RSSI:
                    //Lecture du dernier RSSI ici
                    _rssiReadRequested = true;
                    break;

                case Emetteur.MessageType.OHM:

                    Receptor r = GetReceptor(messageReceivedArgs.EmitterMacAddress);
                    //Lecture de la résistance
                    //On affecte la résistance à la ligne
                    LigneArtifice la = GetLigneArtificeById(r.MacAddress, messageReceivedArgs.RelayNumber.Value);
                    la.Resistance = messageReceivedArgs.DataValue;
                    break;
            }
        }

        public void AddNewLigne()
        {
            _ligneList.Add(CreateNewLigne());
        }

        public void AddNewSousLigne(Ligne ligne, LigneArtifice ligneArtifice)
        {
            ligne.AddLigneArtifice(ligneArtifice);
        }

        /// <summary>
        /// Sauve un feu d'artifice dans un fichier XML
        /// </summary>
        /// <param name="fullFilename"></param>
        public void SaveFirework(string fullFilename)
        {
            XDocument doc = new XDocument();

            //Definition du feu
            XElement fd = new XElement("FireworkDefinition", new XAttribute("fireworkName", "Nom du feu"), new XAttribute("fireworkDate", "14/07/2015"));

            //Récepteurs
            XElement r = new XElement("Receptors",
                        _receptorList.Select(x => new XElement("Receptor", new XAttribute("name", x.Name), new XAttribute("macAddress", x.MacAddress), new XAttribute("nbOfRelays", x.NbOfRelay)))
                     );

            //Lines
            XElement lines = new XElement("Lines");

            foreach (Ligne l in _ligneList)
            {
                XElement line = new XElement("Line", new XAttribute("number", l.Numero.ToString()));

                //Feu d'artifice
                XElement fireworks = new XElement("Fireworks");

                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    XElement firework = new XElement("Firework", new XAttribute("designation", la.Designation)
                            , new XElement("ReceptorAddress", new XAttribute("macAddress", la.ReceptorAddress.MacAddress), new XAttribute("relayNumber", la.ReceptorAddress.RelayNumber.ToString()))
                            , new XElement("FireworkIgnition", la.MiseAFeuText)
                            , new XElement("FireworkDuration", la.DureeArtificeText)
                            );


                    fireworks.Add(firework);
                }

                line.Add(fireworks);

                lines.Add(line);
            }

            //Ajout des récepteurs
            fd.Add(r);

            //Ajout des lignes
            fd.Add(lines);

            //Finalisation du doc
            doc.Add(fd);

            //XDocument doc =
            //  new XDocument(
            //    new XElement("FireworkDefinition", new XAttribute("fireworkName", "Nom du feu"), new XAttribute("fireworkDate", "14/07/2015"),
            //        new XElement("Receptors",
            //            _receptorList.Select(x => new XElement("Receptor", new XAttribute("name", x.Name), new XAttribute("macAddress", x.MacAddress), new XAttribute("nbOfRelays", x.NbOfRelay)))
            //         )
            //    )
            //    );

            doc.Save(fullFilename);
        }

        /// <summary>
        /// Charge un feu d'artifice à partir d'un fichier
        /// </summary>
        /// <param name="fullFilename"></param>
        public void LoadFirework(string fullFilename)
        {
            //Initialisation d'un nouveau feu
            InitNewFireWork();

            //Lecture du fichier
            XDocument firework = XDocument.Load(fullFilename);

            //Parcours et création des récepteurs
            List<XElement> receptors = (from r in firework.Descendants("Receptor")
                                        select r).ToList();

            foreach (XElement r in receptors)
            {
                Receptor recep = new Receptor(r.Attribute("name").Value.ToString(), r.Attribute("macAddress").Value.ToString(), Convert.ToInt32(r.Attribute("nbOfRelays").Value.ToString()));

                //if (_receptorList == null) //TODO : A supprimer lorsque l'ajout d'un boitier fonctionnera
                _receptorList.Add(recep);
            }

            //Parcours des lignes et création des artifices
            List<XElement> lines = (from l in firework.Descendants("Line")
                                    select l).ToList();

            foreach (XElement l in lines)
            {
                int lineNumber = Convert.ToInt32(l.Attribute("number").Value.ToString());
                Ligne line = CreateNewLigne(lineNumber);

                List<XElement> fireworks = (from f in l.Descendants("Firework")
                                            select f).ToList();

                foreach (XElement f in fireworks)
                {
                    string designation = f.Attribute("designation").Value.ToString();

                    string macAddress = f.Element("ReceptorAddress").Attribute("macAddress").Value.ToString();
                    string relayNumber = f.Element("ReceptorAddress").Attribute("relayNumber").Value.ToString();
                    Receptor r = GetReceptor(macAddress);
                    TimeSpan fireworkIgnition = TimeSpan.Parse(f.Element("FireworkIgnition").Value.ToString());


                    TimeSpan fireworkDuration = TimeSpan.Parse(f.Element("FireworkDuration").Value.ToString());

                    LigneArtifice la = new LigneArtifice(lineNumber, designation, fireworkIgnition, fireworkDuration, r.GetReceptorAddressFromRelayNumber(Convert.ToInt32(relayNumber)));

                    line.AddLigneArtifice(la);

                }

                _ligneList.Add(line);
            }
        }

        private bool IsFireworkFinished()
        {
            bool isFinished = true;

            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    //La premiere ligne qu'on trouve non finie ou tir échoué, alors le feu 
                    //n'est pas terminé
                    if (la.EtatArtifice != EtatArtifice.Finished && la.EtatArtifice != EtatArtifice.LaunchFailed)
                    {
                        isFinished = false;
                        break;
                    }
                }

                if (!isFinished) break;
            }

            return isFinished;
        }

        /// <summary>
        /// Récupérère le prochain tir à faire et le positionne en ImminentLaunch
        /// </summary>
        /// <returns></returns>
        private List<LigneArtifice> GetAndPrepareNextArtificeToLaunch()
        {
            List<LigneArtifice> laList = new List<LigneArtifice>();
            TimeSpan lastMiseAFeu = new TimeSpan(0, 0, 0);

            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    if (la.EtatArtifice == EtatArtifice.StandBy)
                    {
                        if (laList.Count == 0)
                        {
                            la.EtatArtifice = EtatArtifice.ImminentLaunch;
                            lastMiseAFeu = la.MiseAFeu;
                            laList.Add(la);
                        }
                        else
                        {
                            //Une autre ligne avec la meme seconde de mise à feu?
                            if (lastMiseAFeu.TotalSeconds == la.MiseAFeu.TotalSeconds)
                            {
                                la.EtatArtifice = EtatArtifice.ImminentLaunch;
                                lastMiseAFeu = la.MiseAFeu;
                                laList.Add(la);
                            }
                            else
                            {
                                //Si la suivante n'a pas la meme mise à feu alors c'est fini ce ne peut plus etre les autres ensuite
                                return laList;
                            }
                        }
                    }
                }
            }

            return laList;
        }

        /// <summary>
        /// Feu opérationnel si les mises à jour définit se fond dans l'ordre croissant dans le temps
        /// </summary>
        /// <returns></returns>
        public bool IsFireworkOperational()
        {
            //Si aucune ligne ni artifice lié à la ligne alors non opérationnel
            if (_ligneList == null
                || _ligneList.Count == 0
                || _ligneList[0].LigneArtificeList == null
                || _ligneList[0].LigneArtificeList.Count == 0)
                return false;

            TimeSpan lastLine = new TimeSpan(0, 0, 0);
            bool isOk = true;
            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    if (la.MiseAFeu.TotalSeconds < lastLine.TotalSeconds)
                    {
                        isOk = false;
                        break;
                    }

                    lastLine = la.MiseAFeu;
                }

                if (!isOk) break;
            }

            return isOk;
        }

        public void StartFireWork(BackgroundWorker worker)
        {
            //TODO : A remettre juste pour les tests ici
            //if (_emetteur == null)
            //    throw new NoTransmitterFoundException();

            _worker = worker;
            _chronoFirework = new Stopwatch();
            _chronoTotal = new Stopwatch();
            _timer = new System.Timers.Timer(1000); //Toutes les secondes
            _timer.Elapsed += _timer_Elapsed;
            _chronoFirework.Start();
            _chronoTotal.Start();
            _timer.Start();
            _isRunning = true;

            //Reinitialisation des récepteurs
            foreach (Receptor r in _receptorList)
            {
                //TODO : A remettre
                //_emetteur.SendInitMessage(r.MacAddress);
            }

            //ManageFirework();
        }

        public void TestResistance(LigneArtifice la)
        {
            if (_emetteur == null)
                throw new NoTransmitterFoundException();

            _emetteur.SendOhmMessage(la.ReceptorAddress.MacAddress, la.ReceptorAddress.RelayNumber);
        }

        /// <summary>
        /// Lance les tests sur un recepteur
        /// </summary>
        /// <param name="r"></param>
        public bool CheckCommunication(Receptor r)
        {
            if (_emetteur == null)
                throw new NoTransmitterFoundException();

            bool responseStatut = _emetteur.SendRssiMessage(r.MacAddress);

            if (responseStatut)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteFirework(LigneArtifice ligneArtifice)
        {
            ligneArtifice.FreeReceptorAddress();

            //Pas optimum mais doit marcher...
            foreach (Ligne l in _ligneList)
            {
                l.LigneArtificeList.Remove(ligneArtifice);
            }
        }

        public void StopFireWork(bool askedByUser)
        {
            if (_chronoFirework != null) _chronoFirework.Reset();
            if (_chronoTotal != null) _chronoTotal.Reset();
            if (_timer != null) _timer.Stop();

            //On tue les threads en cours, c'est plus prudent
            foreach (Thread t in _threadList)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }

            _fireworkStoppedByUser = askedByUser;
            _isRunning = false;
        }

        /// <summary>
        /// Initialise un nouveau feu
        /// </summary>
        public void InitNewFireWork()
        {
            StopFireWork(false);
            OnPropertyChanged("DureeFirework");
            OnPropertyChanged("DureeTotale");

            _receptorList = new List<Receptor>();
            _ligneList = new ThreadedBindingList<Ligne>();
            _receptorList = new List<Receptor>();

            //TODO : A supprimer lorsque l'ajout d'un nouveau boitier sera disponible
            //Receptor r = new Receptor("Boitier 1", "0013A20040A76E58", 16);
            //_receptorList.Add(r);
        }

        /// <summary>
        /// Remet un zéro un feu chargé
        /// </summary>
        public void ResetFireWork()
        {
            StopFireWork(false);
            OnPropertyChanged("DureeFirework");
            OnPropertyChanged("DureeTotale");

            foreach (Ligne l in _ligneList)
            {
                foreach (LigneArtifice la in l.LigneArtificeList)
                {
                    la.Reset();
                }
            }
        }

        public void PauseFireWork()
        {
            _chronoFirework.Stop();
            _isPaused = true;
        }

        public void ContinueFireWork()
        {
            _chronoFirework.Start();
            _isPaused = false;
        }

        /// <summary>
        /// Provoque le lancement du prochain artifice sans attendre 
        /// le déroulement définit à la base
        /// </summary>
        public void FireNextFireworkNow()
        {
            //Récupération des lignes en état Tir imminent
            List<LigneArtifice> laImminentLaunch = (from la in GetLigneArtificeList()
                                                    where la.State == EtatArtifice.ImminentLaunch
                                                    select la).ToList();

            if (laImminentLaunch.Count == 0) return;

            TimeSpan backToFutureTime = laImminentLaunch[0].MiseAFeu - _chronoFirework.Elapsed;

            //Récupération des lignes en état En attente
            List<LigneArtifice> laStandBy = (from la in GetLigneArtificeList()
                                             where la.State == EtatArtifice.StandBy
                                             select la).ToList();

            //Mise à jour des temps de lancement!!!
            //Les artifices en tir doivent être lancé de suite!!!
            foreach (LigneArtifice la in laImminentLaunch)
            {
                la.MiseAFeu = _chronoFirework.Elapsed;
            }
            //La mise à feu des artifices en attente doit être revu pour rester cohérent
            foreach (LigneArtifice la in laStandBy)
            {
                la.MiseAFeu = la.MiseAFeu - backToFutureTime;
            }
        }

        

        #endregion

        #region Evenement

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
