using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ClickForensics.Quartz.Manager.Tests
{
    [TestFixture]
    public class QuartzScehdulerTests
    {
        [Test]
        public void BackupToFileTests()
        {
            QuartzScheduler scheduler = new QuartzScheduler("app01", 555, "QuartzScheduler");
            scheduler.BackupToFile(new System.IO.FileInfo(@"C:\Users\jvilalta\Documents\toto.xml"));

        }
    }
}
