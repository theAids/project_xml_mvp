using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_XML.Presenters.ExportPanel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.ExportPanel.Tests
{
    [TestClass()]
    public class ExportPanelPresenterTests
    {
        [TestMethod()]
        public void InitViewTest()
        {
            int year = DateTime.UtcNow.Year;

            Assert.AreEqual(2017, year, "Not equal");
        }

        [TestMethod]
        public void GenerateXML()
        {
            DateTime date = DateTime.ParseExact("1982-07-13", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Assert.AreEqual("1982-07-13", date.ToString("yyyy-MM-dd"), "Not equal");
        }
    }
}