using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickForensics.Quartz.Manager
{
    public class ConnectionInfo
    {
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string SchedulerName { get; set; }
    }
}
