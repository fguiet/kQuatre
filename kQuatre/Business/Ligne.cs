using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public class Ligne : INotifyPropertyChanged
    {
        private int _numero;
        private ThreadedBindingList<LigneArtifice> _ligneArtificeList = new ThreadedBindingList<LigneArtifice>();
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Ligne(int numero)
        {
            _numero = numero;
        }

        public int Numero
        {
            get
            {
                return _numero;
            }
        }

        public string TextNumero
        {
            get
            {
                return string.Format("Ligne numéro : {0}", _numero.ToString());
            }
        }        

        public ThreadedBindingList<LigneArtifice> LigneArtificeList
        {
            get
            {
                return _ligneArtificeList;
            }
        }

        public void AddLigneArtifice(LigneArtifice ligneArtifice)
        {                        
            _ligneArtificeList.Add(ligneArtifice);            
        }
        

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }        
    }
}
