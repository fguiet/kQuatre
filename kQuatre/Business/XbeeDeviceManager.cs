using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Guiet.kQuatre.Business
{

    /// <summary>
    ///     Provides automated detection and initiation of xbee devices. This class cannot be inherited.
    /// </summary>
    public sealed class XBeeDeviceManager : IDisposable
    {
        /// <summary>
        ///     A System Watcher to hook events from the WMI tree.
        /// </summary>
        private ManagementEventWatcher _deviceWatcher = null;

        private string _xbeeComPort;
        private FTD2XX_NET.FTDI _ftdi = new FTD2XX_NET.FTDI();

        /// <summary>
        /// Retourne le port COM auquel est associé le XBee
        /// </summary>
        public string COMPort
        {
            get
            {
                return _xbeeComPort;
            }
        }

        /// <summary>
        ///     Initialises a new instance of the <see cref="ArduinoDeviceManager"/> class.
        /// </summary>
        public XBeeDeviceManager()
        {            
            WqlEventQuery query = new WqlEventQuery();
            query.EventClassName = "__InstanceOperationEvent";
            query.WithinInterval = new TimeSpan(0, 0, 1);
            query.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";

            _deviceWatcher = new ManagementEventWatcher(new ManagementScope("root\\CIMV2"), query);

            // Attach an event listener to the device watcher.
            _deviceWatcher.EventArrived += _deviceWatcher_EventArrived;

            // Start monitoring the WMI tree for changes in SerialPort devices.
            _deviceWatcher.Start();

            // Initially populate the devices list.
            //DiscoverXBeeDevices();
        }

        public event EventHandler XBeeConnected;
        public event EventHandler XBeeDisconnected;

        private void OnXBeeConnectedEvent()
        {
            if (XBeeConnected != null)
            {
                XBeeConnected(this, new EventArgs());
            }
        }
        private void OnXBeeDisconnectedEvent()
        {
            if (XBeeDisconnected != null)
            {
                XBeeDisconnected(this, new EventArgs());
            }
        }

        /// <summary>
        ///     Gets a list of all dynamically found SerialPorts.
        /// </summary>
        /// <value>A list of all dynamically found SerialPorts.</value>
        public string XbeeComPort
        {
            get { return _xbeeComPort; }
        }

        public bool IsXbeeConnected
        {
            get
            {
                return !string.IsNullOrEmpty(_xbeeComPort);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Stop the WMI monitors when this instance is disposed.
            _deviceWatcher.EventArrived -= _deviceWatcher_EventArrived;

            if (_ftdi.IsOpen)
                _ftdi.Close();
            
            _ftdi = null;

            _deviceWatcher.Stop();
            _deviceWatcher.Dispose();            
        }

        /// <summary>
        ///     Handles the EventArrived event of the _deviceWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArrivedEventArgs"/> instance containing the event data.</param>
        private void _deviceWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {          
            //On laisse le temps à l'usb de se connecter...            
            Thread.Sleep(2000);

            //On ressait...
            DiscoverXBeeDevices();
        }

        /// <summary>
        ///     Dynamically populates the SerialPorts property with relevant devices discovered from the WMI Win32_SerialPorts class.
        /// </summary>
        public void DiscoverXBeeDevices()
        {            
            _xbeeComPort = null;

            bool isConnected = false;

            try
            {
                UInt32 countDevice = 0;

                FTD2XX_NET.FTDI.FT_STATUS status = _ftdi.GetNumberOfDevices(ref countDevice);

                if (status == FTD2XX_NET.FTDI.FT_STATUS.FT_OK)
                {
                    //Un seul XBee doit être connecté à la fois!
                    if (countDevice == 1)
                    {
                        FTD2XX_NET.FTDI.FT_DEVICE_INFO_NODE[] listDevice = new FTD2XX_NET.FTDI.FT_DEVICE_INFO_NODE[countDevice];
                        status = _ftdi.GetDeviceList(listDevice);

                        if (status == FTD2XX_NET.FTDI.FT_STATUS.FT_OK)
                        {
                            foreach (FTD2XX_NET.FTDI.FT_DEVICE_INFO_NODE node in listDevice)
                            {
                                status = _ftdi.OpenByLocation(node.LocId);
                                if (status == FTD2XX_NET.FTDI.FT_STATUS.FT_OK)
                                {
                                    string comPort;
                                    _ftdi.GetCOMPort(out comPort);

                                    if (!string.IsNullOrEmpty(comPort))
                                    {
                                        _xbeeComPort = comPort;
                                        isConnected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                if (_ftdi.IsOpen)
                    _ftdi.Close();

                if (isConnected)
                    OnXBeeConnectedEvent();
                else
                    OnXBeeDisconnectedEvent();
            }
        }
    }
}
