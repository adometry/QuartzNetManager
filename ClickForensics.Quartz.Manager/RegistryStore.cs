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

            RegistryKey managerKey = Registry.CurrentUser.CreateSubKey("QuartzNetManager");
            RegistryKey key = null;
            if (managerKey == null)
            {
                return lastConnections;
            }

            key = managerKey.CreateSubKey("MRUList");

            if (key == null)
            {
                return lastConnections;
            }

            for (int i = 0; i < 5; i++)
            {
                ConnectionInfo info = ConnectionInfo.Parse((key.GetValue(string.Format("connection{0}", i), null) as string));
                if (info != null)
                {
                    lastConnections.Add(info);
                }
            }

            key.Close();
            managerKey.Close();

            return lastConnections;
        }
        public static void AddConnection(ConnectionInfo info)
        {
            RegistryKey managerKey = Registry.CurrentUser.CreateSubKey("QuartzNetManager");
            RegistryKey key = null;
            if (managerKey == null)
            {
                return;
            }

            key = managerKey.CreateSubKey("MRUList");

            if (key == null)
            {
                return;
            }
            //TODO: check that the key doesn't exist before trying to add. if it exists, move it to the top, but don't add it

            for (int i = 4; i > 0; i--)
            {
                var previous = key.GetValue(string.Format("connection{0}", i - 1), null);
                if (previous != null)
                {
                    key.SetValue(string.Format("connection{0}", i), previous);

                }
            }
            key.SetValue("connection0", info, RegistryValueKind.String);

        }
        private static object lockObject = new object();
    }
}
