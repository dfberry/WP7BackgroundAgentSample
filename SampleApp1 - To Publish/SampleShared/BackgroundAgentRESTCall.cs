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

namespace SampleShared
{
    public static class BackgroundAgentRESTCall
    {
        public const String ProcessName = "Agent";

        public static IsoStorageData Call()
        {
            //This is where you put the call to your webservice. This
            //call can/should determine what to display on the tile and/or
            //toast. However, bring back as little as possible. Leave 
            //the bigger capture of data to your app.
 
            //For purposes of this sample, we just write/read iso storage 
            //and return results

            //Read the mutexed iso storage to get mutexedData.CycleAgentEveryMinute 
            IsoStorageData mutexedData = MutexedIsoStorageFile.Read();

            //Current datetime
            DateTime DateTimeNow = DateTime.Now;

            //Put current process and datetime into mutexed iso storage 
            //carry over previous cycle value
            MutexedIsoStorageFile.Write(new IsoStorageData()
            {
                LastProcessToTouchFile = ProcessName,
                LastTimeFileTouched = DateTimeNow,
                CycleAgentEveryMinute = mutexedData.CycleAgentEveryMinute 
            });

            //Read the mutexed iso storage to verify it was written
            mutexedData = MutexedIsoStorageFile.Read();

            return mutexedData;
        }
    }
}
