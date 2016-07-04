
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using com.digi.xbee.api;
using com.digi.xbee.api.exceptions;
using com.digi.xbee.api.listeners;
using com.digi.xbee.api.models;
using com.digi.xbee.api.utils;

namespace Guiet.kQuatre.Business
{
    public class MessageReceivedEventArgs : EventArgs
    {
        private Guiet.kQuatre.Business.Emetteur.MessageType _messageType;
        private int? _relayNumber;
        private string _emitterMacAddress;
        private string _dataValue;
        //private int? _rssi; 

        public MessageReceivedEventArgs(Guiet.kQuatre.Business.Emetteur.MessageType messageType,
                                        int? relayNumber, string emitterMacAddress, string dataValue)
        {
            _messageType = messageType;
            _relayNumber = relayNumber;
            _emitterMacAddress = emitterMacAddress;
            _dataValue = dataValue;
        }

        public int? RelayNumber
        {
            get
            {
                return _relayNumber;
            }
        }

        public string EmitterMacAddress
        {
            get
            {
                return _emitterMacAddress;
            }
        }
     

        public string DataValue
        {
            get
            {
                return _dataValue;
            }
        }

        public Guiet.kQuatre.Business.Emetteur.MessageType MessageType
        {
            get
            {
                return _messageType;
            }
        }
    }

    public class MyDataReceiveListener : IDataReceiveListener {

        public event MessageReceivedEventHandler MessageReceived;
        public delegate void MessageReceivedEventHandler(MessageReceivedEventArgs messageReceivedArgs); 

        public void dataReceived(com.digi.xbee.api.models.XBeeMessage xbm)
        {
            string senderMacAddress = xbm.getDevice().get64BitAddress().toString();
            string result = xbm.getDataString();
           // Console.Out.WriteLine(dataString);

         //   string result = System.Text.Encoding.ASCII.GetString(data);

         //   string senderMacAddress = ByteUtils.ToBase16(sender.Address);

            //On a recu un message du xbee recepteur avec la force du signal
            if (result == "RSSI")
            {
                if (MessageReceived != null)
                {
                    MessageReceived(new MessageReceivedEventArgs(Guiet.kQuatre.Business.Emetteur.MessageType.RSSI, null, senderMacAddress, result));
                }
            }

            if (result.StartsWith("OHM"))
            {
                if (MessageReceived != null)
                {
                    int relayNumber = Convert.ToInt32(result.Split(';')[1]);
                    string dataValue = result.Split(';')[2];
                    MessageReceived(new MessageReceivedEventArgs(Guiet.kQuatre.Business.Emetteur.MessageType.OHM, relayNumber, senderMacAddress, dataValue));
                }
            }   
        }
    }

    public class Emetteur
    {
        public enum MessageType
        {
            RSSI,
            OHM
        }
    
        private const int BAUD = 9600;
        private string _portCOM;
        //private NETMF.OpenSource.XBee.XBeeApi _xbee = null;
        private XBeeDevice _xbee = null;
        private MyDataReceiveListener _dataListener = null;
        public const double RSSI_AT_ONE_M = -66; // Reference RSSI value at 1 meter
        public const double PATH_LOSS = 2.2;  //http://evilrobotfactory.blogspot.fr/2014/05/estimating-distance-from-rssi-values.html     

        public event MessageReceivedEventHandler MessageReceived;
        
        public delegate void MessageReceivedEventHandler(MessageReceivedEventArgs messageReceivedArgs); 

        public Emetteur(string COMPort)
        {
            _portCOM = COMPort;
        }        

        public void OuvreEmetteur()
        {
            if (_xbee == null)
            {
                _xbee = new XBeeDevice(_portCOM, BAUD);
                _dataListener = new MyDataReceiveListener();
                _dataListener.MessageReceived += _dataListener_MessageReceived;
            }

            _xbee.open();
            //_xbee.DataReceived += Xbee_DataReceived;
            _xbee.addDataListener(_dataListener); 
       
        }

        void _dataListener_MessageReceived(MessageReceivedEventArgs messageReceivedArgs)
        {
            if (MessageReceived != null)
            {
                MessageReceived(messageReceivedArgs);
            }
        }

        public int? Rssi
        {
            get
            {
                return this.GetRssi();                
            }
        }        


       /* public void Xbee_DataReceived(NETMF.OpenSource.XBee.XBeeApi receiver, byte[] data, XBeeAddress sender, int? rssi)
        {
            string result = System.Text.Encoding.ASCII.GetString(data);           

            string senderMacAddress = ByteUtils.ToBase16(sender.Address);

            //On a recu un message du xbee recepteur avec la force du signal
            if (result == "RSSI")
            {
                if (MessageReceived != null)
                {
                    MessageReceived(new MessageReceivedEventArgs(MessageType.RSSI, null, senderMacAddress, result));
                }
            }

            if (result.StartsWith("OHM"))
            {
                if (MessageReceived != null)
                {
                    int relayNumber = Convert.ToInt32(result.Split(';')[1]);
                    string dataValue = result.Split(';')[2];
                    MessageReceived(new MessageReceivedEventArgs(MessageType.OHM, relayNumber, senderMacAddress, dataValue));
                }
            }            
        }*/


        public void FermeEmetteur()
        {
            if (_xbee != null)
            {
                _xbee.removeDataListener(_dataListener);
                _xbee.close();
                _xbee = null;
            }            
        }

        public bool SendFireMessage(string macAddress, string message)
        {
            byte[] payload = System.Text.Encoding.ASCII.GetBytes(message);

            return SendMessage(macAddress, payload);
        }

        public bool SendOhmMessage(string macAddress, int relayNumber)
        {
            string message = string.Format("OHM;{0}", relayNumber.ToString());

            byte[] payload = System.Text.Encoding.ASCII.GetBytes(message);

            return SendMessage(macAddress, payload);
        }

        public bool SendRssiMessage(string macAddress)
        {          
            byte[] payload = System.Text.Encoding.ASCII.GetBytes("RSSI");

            return SendMessage(macAddress, payload);
        }

        public bool SendInitMessage(string macAddress)
        {
            byte[] payload = System.Text.Encoding.ASCII.GetBytes("INIT");

            return SendMessage(macAddress, payload);
        }

        private bool SendMessage(string macAddress, byte[] payload)
        {

            try
            {
                RemoteXBeeDevice rxd = new RemoteXBeeDevice(_xbee, new XBee64BitAddress(macAddress));
                _xbee.sendData(rxd, payload);
              
                return true;
            }
            catch (TransmitException te)
            {
                string message = te.getTransmitStatusMessage();
                XBeeTransmitStatus xts = te.getTransmitStatus();
                
                return false;
            }
            catch (XBeeException xe)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

           // XBeeAddress64 xbeeAddress64 = new XBeeAddress64(macAddress);
           // NETMF.OpenSource.XBee.Api.Zigbee.TxRequest request = new NETMF.OpenSource.XBee.Api.Zigbee.TxRequest(xbeeAddress64, payload);

           //// request.Option = TxRequest.Options.DisableAck;

           // try
           // {
           //     //_xbee.
           //     var r = _xbee.Send(request);
           //    // Thread.Sleep(200);
           //     return CheckResponseStatus(r.GetResponse());
           // }
           // catch (XBeeTimeoutException x)
           // {

           //     return false;
           // }
        }

        /*private bool CheckResponseStatus(XBeeResponse response)
        {
            if (response.ApiId == ApiId.ZnetTxStatusResponse)
            {
                NETMF.OpenSource.XBee.Api.Zigbee.TxStatusResponse txResponse = response as NETMF.OpenSource.XBee.Api.Zigbee.TxStatusResponse;
                //if (txResponse.IsSuccess) 
                if (txResponse.DeliveryStatus == NETMF.OpenSource.XBee.Api.Zigbee.TxStatusResponse.DeliveryResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
            //throw new Exception("Invalid Response Status Received");
        }*/

        /// <summary>
        /// Envoie d'un commande AT pour lecture du RSSI (force du signal)
        /// </summary>
        private int? GetRssi()
        {
            try
            {
                //https://docs.digi.com/pages/viewpage.action?pageId=2626044     
                //http://evilrobotfactory.blogspot.fr/2014/05/estimating-distance-from-rssi-values.html
                byte[] payload = _xbee.getParameter("DB");

                int decValue = ByteUtils.byteArrayToInt(payload);

                //string result = ByteArrayToString(payload);
               // int decValue = int.Parse(decValueHex, System.Globalization.NumberStyles.HexNumber);
                /*var request = _xbee.Send(AtCmd.ReceivedSignalStrength);
                AtResponse response = (AtResponse)request.GetResponse();

                if (response.IsOk)
                    return Convert.ToInt32(response.Value[0]);
                else
                    return null;*/
                return decValue;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
