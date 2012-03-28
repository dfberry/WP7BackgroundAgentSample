using System;
using System.Net;
using System.Threading; //added via the mscorlib.Extensions.dll 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics; // added

namespace SampleShared
{
    public class IsoStorageData
    {
        public DateTime LastTimeFileTouched {get;set;}

        public String LastProcessToTouchFile {get;set;}

        public bool CycleAgentEveryMinute { get; set; }
    }

    public static class MutexedIsoStorageFile
    {
        private static Mutex Mutex = new Mutex(false, "BackgroundAgentDemo1");

        private const String IsoStorageDateFile = "BackgroundAgentDemo1data.txt";

        public static IsoStorageData Read()
        {
            IsoStorageData IsoStorageData = new IsoStorageData();

            Mutex.WaitOne();

            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                using (var stream = new IsolatedStorageFileStream(IsoStorageDateFile, FileMode.OpenOrCreate, FileAccess.Read, store))
                using (var reader = new StreamReader(stream))
                {
                    if (!reader.EndOfStream)
                    {
                        var serializer = new XmlSerializer(typeof(IsoStorageData));
                        IsoStorageData = (IsoStorageData)serializer.Deserialize(reader);
                    }
                }
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
            Debug.WriteLine("RRR-data.LastProcessToTouchFile=" + IsoStorageData.LastProcessToTouchFile);
            Debug.WriteLine("RRR-data.LastTimeFileTouched=" + IsoStorageData.LastTimeFileTouched.ToString());
            Debug.WriteLine("RRR-data.CycleAgentEveryMinute=" + IsoStorageData.CycleAgentEveryMinute.ToString()); 
           
            return IsoStorageData;
        }
        public static void Write(IsoStorageData data)
        {
            Debug.WriteLine("WWW-data.LastProcessToTouchFile=" + data.LastProcessToTouchFile);
            Debug.WriteLine("WWW-data.LastTimeFileTouched=" + data.LastTimeFileTouched.ToString());
            Debug.WriteLine("WWW-data.CycleAgentEveryMinute=" + data.CycleAgentEveryMinute.ToString());

            // persist the data using isolated storage
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            using (var stream = new IsolatedStorageFileStream(IsoStorageDateFile,
                                                              FileMode.Create,
                                                              FileAccess.Write,
                                                              store))
            {
                var serializer = new XmlSerializer(typeof(IsoStorageData));
                serializer.Serialize(stream, data);
            }
        }

    }

}
