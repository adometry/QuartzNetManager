using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace ClickForensics.Quartz.Manager
{
    public class RegistryStore
    {

        public static List<ConnectionInfo> GetLastConnections()
        {
            List<ConnectionInfo> lastConnections = new List<ConnectionInfo>();

            RegistryKey key = Registry.CurrentUser.CreateSubKey("QuartzNetManager").CreateSubKey("MRUList");
            if (key != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    ConnectionInfo info = (ConnectionInfo)key.GetValue(string.Format("connection{0}", i), null);
                    if (info != null)
                    {
                        lastConnections.Add(info);
                    }
                }
            }
            key.Close();
            return lastConnections;
        }
        public static void AddConnection(ConnectionInfo info)
        {

        }
        private static object lockObject = new object();
    }
}
