using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using SampleShared;


namespace SampleApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public String ProcessName = "App";
        public String BackgroundPNG = "background.png";
        public bool CycleAgentEveryMinute { get; set; }

        // mutexed iso data
        private IsoStorageData _mutexedData;

        public MainViewModel()
        {
            _mutexedData = new IsoStorageData();
            CycleAgentEveryMinute = false;

            //read mutexed iso data
            ReadData();
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public IsoStorageData MutexedData
        {
            get
            {
                return _mutexedData;
            }
            set
            {
                if (value != _mutexedData)
                {
                    _mutexedData = value;
                    NotifyPropertyChanged("MutexedData");
                }
            }
        }
        public void WriteData()
        {
            //construct data
            MutexedData.LastTimeFileTouched = DateTime.Now;
            MutexedData.LastProcessToTouchFile = ProcessName;
            MutexedData.CycleAgentEveryMinute = CycleAgentEveryMinute;

            //write mutexed iso data
            MutexedIsoStorageFile.Write(MutexedData);
        }
        public void ReadData()
        {
            // read mutexed iso data
            MutexedData = MutexedIsoStorageFile.Read();
        }
        public void UpdateTile()
        {
            TileNotifications.UpdateTile(MutexedData.LastProcessToTouchFile + "-" + MutexedData.LastTimeFileTouched.ToShortTimeString().ToString(), App.ViewModel.BackgroundPNG, 1);
        }
        public void AddAgent()
        {
            AgentMgr.AddAgent();
        }
        public void RunDebugAgent()
        {
            AgentMgr.RunDebugAgent();
        }
        public void RemoveAgent()
        {
            AgentMgr.RemoveAgent();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}