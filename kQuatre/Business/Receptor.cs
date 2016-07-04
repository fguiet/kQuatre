using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public class Receptor
    {
        /// <summary>
        /// Nom du récepteur parlant por l'utilisateur
        /// </summary>
        private string _name;

        /// <summary>
        /// Mac adresse du récepteur
        /// </summary>
        private string _macAddress;

        /// <summary>
        /// Nombre de relay
        /// </summary>
        private int _nbOfRelay;

       // private int? _rssi = null;

      //  private double? _distance = null;

        /// <summary>
        /// Liste des adresses possibles du récepteur
        /// </summary>
        private List<ReceptorAddress> _addresses = new List<ReceptorAddress>();

        /// <summary>
        /// Liste des adresses disponibles de ce récepteur
        /// </summary>
        public List<ReceptorAddress> FreeReceptorAddresses
        {
            get
            {
                return (from a in _addresses
                         where a.IsReserved == false
                         select a).ToList();
            }
        }      

        public string MacAddress
        {
            get { return _macAddress; }
            set { _macAddress = value; }
        }

        public string Name
        {
            get { return _name; }            
        }

        public string NbOfRelay
        {
            get { return _nbOfRelay.ToString(); }
        }

        public string ReceptorText
        {
            get
            {
                int freeAddresses = (from ra in _addresses
                                     where ra.IsReserved == false
                                     select ra).Count();

                return string.Format("{0} (Adresses libres : {1})", _name, freeAddresses.ToString());
            }
        }

        public Receptor(string name, string macAddress, int nbOfRelay) 
        {
            _name = name;
            _macAddress = macAddress;
            _nbOfRelay = nbOfRelay;

            ReceptorAddress ra = null;
            for (int i = 1; i <= nbOfRelay; i++)
            {
                ra = new ReceptorAddress(name, _macAddress, i);
                _addresses.Add(ra);
            }
        }

        public ReceptorAddress GetReceptorAddressFromRelayNumber(int relayNumber) 
        {
            ReceptorAddress ra = (from a in _addresses
                                  where a.MacAddress == _macAddress && a.RelayNumber == relayNumber
                                  select a).FirstOrDefault();

            if (ra == null)
            {
                throw new Exception("Impossible de trouver l'adresse de ce relay");
            }
            
            return ra;
        }       
    }
}
