using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickForensics.Quartz.Manager
{
    class JobType
    {
        public string FullName { get; set; }
        public string AssemblyName { get; set; }
        public override string ToString()
        {
            return FullName;
        }
    }
}
