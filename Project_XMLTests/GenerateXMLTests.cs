using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;
using Project_XML.Presenters.ExportPanel;
using Project_XML.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Project_XMLTests
{
    [TestClass()]
    public class GenerateXMLTests
    {

        //[TestMethod()]
        public void InitViewTest()
        {
            //Validate_XML();
        }

        [TestMethod()]
        public void NewReport()
        {
            CrsReport crs = new CrsReport();
            //single data for testing:
            string entries = "10572008:2001";
            //int acctHolderId = 2001;
            //string resCountryCode = "UK";

            var ent = entries.Split(',');

            DbExportManager db = new DbExportManager();
            AEOI_Report report = new AEOI_Report();

            //MessageSpec_Type
            report.MessageSpec = crs.MessageSpec("2017", "AZ00099", "CRS701", "", "");

            List<CorrectableAccountReport_Type> correctableAccounts = new List<CorrectableAccountReport_Type>();
            int i = 0;

            foreach (string s in ent)
            {
                string acctNum = s.Split(':')[0];
                int acctHolderId = Convert.ToInt32(s.Split(':')[1]);

                if (db.isEntity(acctHolderId))
                {
                    string[] resCountries = db.GetEntityResCountry(acctHolderId);

                    foreach (string country in resCountries)
                    {
                        //Account Reports
                        CorrectableAccountReport_Type account = new CorrectableAccountReport_Type();
                        //DocSpec
                        account.DocSpec = crs.DocSpec("OECD1", i);

                        AccountDetailsModel acctDetails = db.GetAccountDetials(acctNum);
                        //FIAccountNumber
                        account.AccountNumber = crs.FIAccountNumber(acctDetails);
                        //MonAmnt
                        account.AccountBalance = crs.MonAmntBalance(acctDetails);
                        //Payment
                        List<Payment_Type> payments = crs.Payment(acctNum);
                        if (payments != null)
                            account.Payment = payments.ToArray();

                        //AccountHolder
                        AccountHolder_Type holder = new AccountHolder_Type();

                        EntityDetailsModel model = db.GetEntityDetails(acctHolderId, country);
                        List<object> acctHolderItems = new List<object>();

                        OrganisationParty_Type org = crs.OrganisationType(model, country);
                        acctHolderItems.Add(org);

                        //ControllingPerson
                        Debug.WriteLine("Input: {0} {1} {2}", acctHolderId, country, acctNum);
                        List<ControllingPersonModel> ctrlList = db.GetEntityCtrlPerson(acctHolderId, country, acctNum);
                        if (ctrlList != null && ctrlList.Count != 0)
                        {
                            ControllingPerson_Type[] ctrlPersons = crs.ControllingPerson(ctrlList);
                            account.ControllingPerson = ctrlPersons;

                            CrsAcctHolderType_EnumType acctType;
                            if (Enum.TryParse<CrsAcctHolderType_EnumType>(model.AcctHolderType, out acctType) == true)
                                acctHolderItems.Add(acctType);

                        }
                        else
                        {
                            acctHolderItems.Add(CrsAcctHolderType_EnumType.CRS102); // if entity has no controlling person in the same jurisdiction country
                        }

                        holder.Items = acctHolderItems.ToArray();
                        account.AccountHolder = holder;

                        correctableAccounts.Add(account);
                        i++; // for DocRefId
                    }
                }
                else // account holder is an Individual/Person
                {
                    Debug.WriteLine("Individual Account.");
                    string[] resCountries = db.GetPersonReportableResCountry(acctHolderId);

                    foreach (string country in resCountries)
                    {

                        //Account Reports
                        CorrectableAccountReport_Type account = new CorrectableAccountReport_Type();
                        //DocSpec
                        account.DocSpec = crs.DocSpec("OECD1", i);

                        AccountDetailsModel acctDetails = db.GetAccountDetials(acctNum);
                        //FIAccountNumber
                        account.AccountNumber = crs.FIAccountNumber(acctDetails);
                        //MonAmnt
                        account.AccountBalance = crs.MonAmntBalance(acctDetails);
                        //Payment
                        List<Payment_Type> payments = crs.Payment(acctNum);
                        if (payments != null)
                            account.Payment = payments.ToArray();

                        //AccountHolder
                        PersonDetailsModel model = db.GetPersonDetails(acctHolderId);

                        //Get AcctHolder details
                        PersonParty_Type person = crs.PersonParty(model, false);

                        CountryCode_Type[] countries = { (CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), country) };
                        person.ResCountryCode = countries;

                        List<object> acctHolderItems = new List<object>();
                        acctHolderItems.Add(person);

                        AccountHolder_Type holder = new AccountHolder_Type();
                        holder.Items = acctHolderItems.ToArray();
                        account.AccountHolder = holder;

                        /*
                        //ControllingPerson
                        Debug.WriteLine("Input: {0} {1} {2}", acctHolderId, country, acctNum);
                        List<ControllingPersonModel> ctrlList = db.GetIndividualCtrlPerson(acctHolderId, country, acctNum);
                        if (ctrlList != null && ctrlList.Count != 0)
                        {
                            ControllingPerson_Type[] ctrlPersons = ControllingPerson(ctrlList);
                            account.ControllingPerson = ctrlPersons;
                        }
                        */

                        correctableAccounts.Add(account);
                        i++;
                    }

                }
            }

            //ReportingGroup
            CrsBody_Type body = new CrsBody_Type();
            body.ReportingGroup = correctableAccounts.ToArray();
            report.CrsBody = body;


            //Serialize class to XML document.
            XmlDocument doc = new XmlDocument();

            XmlSerializer serializer = new XmlSerializer(typeof(AEOI_Report));
            TextWriter writer = new StreamWriter("C:/Users/adrian.m.perez/Desktop/sample1.xml");
            serializer.Serialize(writer, report);
            writer.Close();
            crs.Validate_XML();

            //  Validate_XML(doc);
            Assert.IsTrue(true);
            //Assert.AreEqual("EY Hong Kong", report.MessageSpec.FIName);
        }
    }
}
