using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickForensics.Quartz.Manager
{
    public class ConnectionInfo
    {
        public ConnectionInfo()
        {

        }
        public static ConnectionInfo Parse(string connectionString)
        {
            if (connectionString == null)
            {
                return null;
            }
            string[] parameters = connectionString.Split(new string[] { "|" }, StringSplitOptions.None);
            if (parameters.Length != 3)
            {
                return null;
            }
            return new ConnectionInfo { ServerName = parameters[0], Port = int.Parse(parameters[1]), SchedulerName = parameters[2] };
        }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string SchedulerName { get; set; }
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}", ServerName, Port, SchedulerName);
        }
    }
}
