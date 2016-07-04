using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guiet.kQuatre.Business
{
    public class ReceptorAddress
    {
        /// <summary>
        /// Mac Adress du boitier
        /// </summary>
        private string _macAddress;
        /// <summary>
        /// Adresse réservée?
        /// </summary>
        private bool _isReserved = false;

        //Nom du boitier plus parlant
        private string _receptorName;

        public bool IsReserved
        {
            get { return _isReserved; }            
        }

        public string MacAddress
        {
            get { return _macAddress; }            
        }

        public string ReceptorAddressText
        {
            get
            {
                return string.Format("{0} - Relaie : {1}", _receptorName, _relayNumber.ToString());
            }
        }

        private int _relayNumber;

        public int RelayNumber
        {
            get { return _relayNumber; }            
        }        

        public ReceptorAddress(string receptorName, string macAddress, int relayNumber)
        {
            _relayNumber = relayNumber;
            _macAddress = macAddress;
            _receptorName = receptorName;
        }

        /// <summary>
        /// Rend l'adresse disponible
        /// </summary>
        public void SetFree() 
        {
            _isReserved = false;
        }
        
        /// <summary>
        /// Rend l'adresse réservée
        /// </summary>
        public void SetReserved() 
        {
            _isReserved = true;
        }
        
    }
}
