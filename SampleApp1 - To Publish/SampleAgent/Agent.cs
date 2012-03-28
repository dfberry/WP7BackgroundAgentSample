using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Info;
using Microsoft.Phone.Scheduler;
using SampleShared;


namespace SampleAgent
{
  /// <summary>
  /// Simple agent to show a toast and update a tile based on twitter info
  /// </summary>
    public class SampleAgent : ScheduledTaskAgent
    {

        public String BackgroundPNG = "background.png";
        public String MainPageURLForToast = "/MainPage.xaml";
        public String ToastTitle = "Mutex:";
        public String AppName = "SampleApp1";
        
        /// <summary>
        /// Called by the system when there is work to be done
        /// </summary>
        /// <param name="Task">The task representing the work to be done</param>
        protected override void OnInvoke(ScheduledTask Task)
        {
            Debug.WriteLine("SampleAgent.SampleAgent.OnInvoke");

            //Here is where you put the meat of the work to be done by the agent
            IsoStorageData MutexedData = BackgroundAgentRESTCall.Call();

            //Toast Popups (processname + datetime in minutes)
            ToastNotifications.ShowToast(ToastTitle, MutexedData.LastProcessToTouchFile + "-" + MutexedData.LastTimeFileTouched.ToShortTimeString().ToString(), MainPageURLForToast);

            //Tile Pinned to Start screen (processname + datetime in minutes)
            // "2" signifies sampleagent
            TileNotifications.UpdateTile(MutexedData.LastProcessToTouchFile + "-" + MutexedData.LastTimeFileTouched.ToShortTimeString().ToString(), BackgroundPNG, 2);

            // this makes the agent run in a shorter cycle than 30 minutes
            // if the iso data setting is turned off (from the app)
            if (MutexedData.CycleAgentEveryMinute)
            {
                AgentMgr.RunDebugAgent();
            }

            //Required Background Agent Finisher
            NotifyComplete();
      }

    }
}
