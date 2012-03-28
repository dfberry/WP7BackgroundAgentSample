using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;

namespace SampleShared
{
    public static class AgentMgr
    {
        private const String AgentName = "SampleApp1";
        private const String AgentDescription = "Sample of using data from Mutex Iso Storage File";

        public static void AddAgent()
        {
            PeriodicTask task = new PeriodicTask(AgentName);
            task.Description = AgentDescription;
            task.ExpirationTime = DateTime.Now.AddDays(1);

            ScheduledActionService.Add(task);
        }
        public static void RemoveAgent()
        {
            if (ScheduledActionService.Find(AgentName) != null)
                ScheduledActionService.Remove(AgentName);
        }
        public static void RunDebugAgent()
        {
            Debug.WriteLine("SampleShared.AgentMgr.RunDebugAgent");

            //1 Minute
            ScheduledActionService.LaunchForTest(AgentName, TimeSpan.FromMinutes(1));
            
            //10 Seconds
            ScheduledActionService.LaunchForTest(AgentName, TimeSpan.FromSeconds(10));

        }
        public static ScheduledAction FindAgent()
        {
            return ScheduledActionService.Find(AgentName);
        }

    }
}
