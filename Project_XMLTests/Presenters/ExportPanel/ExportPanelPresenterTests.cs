using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_XML.Presenters.ExportPanel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Project_XML.Schema;
using System.Diagnostics;
using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;

namespace Project_XML.Presenters.ExportPanel.Tests
{
    [TestClass()]
    public class ExportPanelPresenterTests
    {
        //[TestMethod()]
        public void InitViewTest()
        {
            string s = "";

            string[] str = s.Split(',');
            Console.WriteLine(str);
            Assert.IsTrue(true);

        }

        [TestMethod]
        public void NewReport()
        {
            //single data for testing:
            string entries = "123-444-567890,2001";
            //int acctHolderId = 2001;
            //string resCountryCode = "UK";

            var ent = entries.Split(';');


            XmlSerializer serializer = new XmlSerializer(typeof(AEOI_Report));
            DbExportManager db = new DbExportManager();
            AEOI_Report report = new AEOI_Report();

            //MessageSpec_Type
            report.MessageSpec = MessageSpec("2017", "AZ00099", "CRS701");

            List<CorrectableAccountReport_Type> correctableAccounts = new List<CorrectableAccountReport_Type>();

            foreach (string s in ent)
            {
                string acctNum = s.Split(',')[0];
                int acctHolderId = Convert.ToInt32(s.Split(',')[1]);

                if (db.isEntity(acctHolderId))
                {
                    string[] resCountries = db.GetEntityResCountry(acctHolderId);
                    
                    foreach(string country in resCountries)
                    {
                        //Account Reports
                        CorrectableAccountReport_Type account = new CorrectableAccountReport_Type();
                        //DocSpec
                        account.DocSpec = DocSpec("OECD1", 000);

                        AccountDetailsModel acctDetails = db.GetAccountDetials(acctNum);
                        //FIAccountNumber
                        account.AccountNumber = FIAccountNumber(acctDetails);
                        //MonAmnt
                        account.AccountBalance = MonAmntBalance(acctDetails);
                        //Payment
                        List<Payment_Type> payments = Payment(acctNum);
                        if (payments != null)
                            account.Payment = payments.ToArray();

                        //AccountHolder
                        AccountHolder_Type holder = new AccountHolder_Type();

                        EntityDetailsModel model = db.GetEntityDetails(acctHolderId, country);
                        List<object> acctHolderItems = new List<object>();

                        OrganisationParty_Type org = OrganisationType(model, country);
                        acctHolderItems.Add(org);

                        CrsAcctHolderType_EnumType acctType;
                        if (Enum.TryParse<CrsAcctHolderType_EnumType>(model.AcctHolderType, out acctType) == true)
                            acctHolderItems.Add(acctType);

                        holder.Items = acctHolderItems.ToArray();

                        account.AccountHolder = holder;

                        //ControllingPerson
                        List<int> ctrlIds = db.GetEntityCtrlPersonId(acctHolderId, country, acctNum);

                        
                    }
                }
                else
                {

                }


                //ReportingGroup
                CrsBody_Type body = new CrsBody_Type();
                body.ReportingGroup = correctableAccounts.ToArray(); ;


                Assert.IsTrue(true);
                //Assert.AreEqual("EY Hong Kong", report.MessageSpec.FIName);
            }
        }

        public ControllingPerson_Type[] ControllingPerson(List<int> pId, string acctNumber, string country, bool isEntity)
        {
            DbExportManager db = new DbExportManager();
            List<PersonParty_Type> ctrlList = new List<object[]>();
            if (isEntity)
            {
                foreach(int id in pId)
                {
                    object[] obj = db.GetEntityCtrlPerson(id, acctNumber);

                    if(obj != null && obj.Length != 0)
                    {
                        PersonParty_Type person = PersonParty((PersonDetailsModel)obj[0], country); //first element is perdetailsmdel
                    }

                }
                
            }
            else
            {

            }
        }

        public PersonParty_Type PersonParty(PersonDetailsModel person, string country)
        {
            PersonParty_Type personType = new PersonParty_Type();
            //NamePersonType
            personType.Name = NamePerson(person);

            //ResCountryCode
            CountryCode_Type[] countries = {(CountryCode_Type) Enum.Parse(typeof(CountryCode_Type), country)};
            personType.ResCountryCode = countries;

            //Address
            personType.Address = Address(person)

            //TIN

            //BirthInfo
        }

        public NamePerson_Type[] NamePerson(PersonDetailsModel model)
        {
            NamePerson_Type name = new NamePerson_Type();

            name.PrecedingTitle = model.PreceedingTitle;
            name.Title = model.Title != null ? model.Title.Split(',') : null;
            name.FirstName = model.Firstname;
            name.MiddleName = model.MiddleName;
            name.NamePrefix = model.NamePrefix;
            name.LastName = model.LastName;
            name.GenerationIdentifier = model.GenerationIdentifier != null ? model.GenerationIdentifier.Split(',') : null;
            name.Suffix = model.Suffix != null ? model.Suffix.Split(',') : null;
            name.GeneralSuffix = model.GeneralSuffix;

            if (model.NameType != null && !model.NameType.Equals(""))
            {
                name.nameType = (OECDNameType_EnumType)Enum.Parse(typeof(OECDNameType_EnumType), model.NameType);
                name.nameTypeSpecified = true;
            }
            else
                name.nameTypeSpecified = false;

            NamePerson_Type[] nameList = { name};
            return nameList;
        }


        public OrganisationParty_Type OrganisationType(EntityDetailsModel entity, string resCountryCode)
        {
            OrganisationParty_Type org = new OrganisationParty_Type();

            //Res Country Code
            CountryCode_Type countryCode = (CountryCode_Type) Enum.Parse(typeof(CountryCode_Type), resCountryCode);
            CountryCode_Type[] countries = { countryCode };
            org.ResCountryCode = countries;

            //INType
            List<OrganisationIN_Type> inList = new List<OrganisationIN_Type>();

            var inStr = entity.INVal.Split(';');
            foreach (string s in inStr)
            {
                OrganisationIN_Type inType = new OrganisationIN_Type();

                string[] inCsv = s.Split(',');

                if (inCsv.Length == 3)
                {
                    inType.Value = inCsv[0];
                    if (inCsv[1] != null && !inCsv[1].Equals(""))
                    {
                        inType.issuedBy = (CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), inCsv[1]);
                        inType.issuedBySpecified = true;
                    }
                    else
                        inType.issuedBySpecified = false;
                }
                inType.INType = inCsv[2];

                inList.Add(inType);
            }

            org.IN = inList.ToArray();

            //Name
            NameOrganisation_Type nameOrg = new NameOrganisation_Type();
            nameOrg.Value = entity.Name;
            if (!entity.NameType.Equals("") && entity.NameType != null)
            {
                nameOrg.nameType = (OECDNameType_EnumType)Enum.Parse(typeof(OECDNameType_EnumType), entity.NameType);
                nameOrg.nameTypeSpecified = true;
            }
            else
                nameOrg.nameTypeSpecified = false;

            NameOrganisation_Type[] nameOrgList = { nameOrg };
            org.Name = nameOrgList;

            //Address
            org.Address = Address(entity.EntityId);

            return org;

        }

        public Address_Type[] Address(int acctId)
        {
            DbExportManager db = new DbExportManager();

            List<AddressModel> addrList = new List<AddressModel>();
            List<Address_Type> addrTypeList = new List<Address_Type>();

            if (db.isEntity(acctId))
                addrList = db.GetEntityAddress(acctId);

            else
                addrList = db.GetPersonAddress(acctId);

            foreach (AddressModel model in addrList)
            {
                Address_Type addrType = new Address_Type();

                addrType.Items = AddressType(model);
                addrType.CountryCode = (CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), model.CountryCode);

                if (model.AddressType != null && !model.AddressType.Equals(""))
                {
                    addrType.legalAddressType = (OECDLegalAddressType_EnumType)Enum.Parse(typeof(OECDLegalAddressType_EnumType), model.AddressType);
                    addrType.legalAddressTypeSpecified = true;
                }
                else
                    addrType.legalAddressTypeSpecified = false;

                addrTypeList.Add(addrType);
            }

            return addrTypeList.ToArray();

        }

        public object[] AddressType(AddressModel addr)
        {
            List<object> addrArr = new List<object>();

            if (addr.isFixed)
            {
                AddressFix_Type fix = new AddressFix_Type();
                fix.Street = addr.Street;
                fix.BuildingIdentifier = addr.BuildingIdentifier;
                fix.SuiteIdentifier = addr.SuiteIdentifier;
                fix.FloorIdentifier = addr.FloorIdentifier;
                fix.DistrictName = addr.DistrictName;
                fix.POB = addr.POB;
                fix.PostCode = addr.PostCode;
                fix.City = addr.City;
                fix.CountrySubentity = addr.CountrySubentity;

                addrArr.Add(fix);

                //if FreeLine has content
                if (addr.FreeLine != null && !addr.FreeLine.Equals(""))
                    addrArr.Add(AddressFree(addr.FreeLine));

                return addrArr.ToArray();
            }
            else
            {
                addrArr.Add(AddressFree(addr.FreeLine));
                return addrArr.ToArray();
            }

        }

        public AddressFree_Type AddressFree(string addr)
        {
            AddressFree_Type free = new AddressFree_Type();
            List<string> strList = new List<string>();
            int max = 150;

            for (int i = 0; i < addr.Length; i += max)
                strList.Add(addr.Substring(i, Math.Min(max, addr.Length - i)));

            free.Line = strList.ToArray();
            return free;
        }

        public NameOrganisation_Type NameOrganisation(EntityDetailsModel model)
        {
            NameOrganisation_Type name = new NameOrganisation_Type();

            if (model.NameType != null && !model.NameType.Equals(""))
            {
                try
                {
                    name.nameType = (OECDNameType_EnumType)Enum.Parse(typeof(OECDNameType_EnumType), model.NameType);
                    name.nameTypeSpecified = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Entity Name Type Error:" + e.Message);
                }
            }
            else
                name.nameTypeSpecified = false;

            name.Value = model.Name;

            return name;
        }

        public List<Payment_Type> Payment(string acctNumber)
        {

            DbExportManager db = new DbExportManager();

            List<PaymentModel> paymentList = db.GetPayments(acctNumber);

            if (paymentList == null)
                return null;
            else
            {
                List<Payment_Type> payments = new List<Payment_Type>();
                foreach (PaymentModel model in paymentList)
                {
                    Payment_Type payment = new Payment_Type();
                    MonAmnt_Type amount = new MonAmnt_Type();

                    amount.Value = (decimal)model.Amount;

                    try
                    {
                        amount.currCode = (currCode_Type)Enum.Parse(typeof(currCode_Type), model.CurrCode);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("CurrCode Parsing Exception:" + e.Message);
                        amount.currCode = currCode_Type.HKD; // default currcod if not specified (HKD)
                    }

                    payment.PaymentAmnt = amount;

                    try
                    {
                        payment.Type = (CrsPaymentType_EnumType)Enum.Parse(typeof(CrsPaymentType_EnumType), model.PaymentType);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("PaymentType Parse Exception:" + e.Message);
                        payment.Type = CrsPaymentType_EnumType.CRS504; //corresponds to OTHERS
                    }

                    payments.Add(payment);
                }

                return payments;
            }
        }

        public MonAmnt_Type MonAmntBalance(AccountDetailsModel acctDetails)
        {
            MonAmnt_Type accountBalance = new MonAmnt_Type();

            accountBalance.Value = acctDetails.AccountBalance;

            try
            {
                accountBalance.currCode = (currCode_Type)Enum.Parse(typeof(currCode_Type), acctDetails.ACurrCode);
            }
            catch (Exception e)
            {
                Debug.WriteLine("CurrCode is out or range or is NULL:" + acctDetails.ACurrCode);
                accountBalance.currCode = currCode_Type.HKD; // default currency if nothing is specified (HKD)
            }

            return accountBalance;
        }

        public FIAccountNumber_Type FIAccountNumber(AccountDetailsModel acctDetails)
        {
            FIAccountNumber_Type fiAcctDetails = new FIAccountNumber_Type();
            if (!acctDetails.AcctNumberType.Equals("") && acctDetails.AcctNumberType != null)
            {
                try
                {
                    fiAcctDetails.AcctNumberType = (AcctNumberType_EnumType)Enum.Parse(typeof(AcctNumberType_EnumType), acctDetails.AcctNumberType);
                    fiAcctDetails.AcctNumberTypeSpecified = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Account Number Type Error:" + e.Message);
                }
            }

            if (acctDetails.isClosed != null)
            {
                fiAcctDetails.ClosedAccount = (bool)acctDetails.isClosed;
                fiAcctDetails.ClosedAccountSpecified = true;
            }
            else
                fiAcctDetails.ClosedAccountSpecified = false;

            if (acctDetails.isDormant != null)
            {
                fiAcctDetails.DormantAccount = (bool)acctDetails.isDormant;
                fiAcctDetails.DormantAccountSpecified = true;
            }
            else
                fiAcctDetails.DormantAccountSpecified = false;

            if (acctDetails.isUndocumented != null)
            {
                fiAcctDetails.UndocumentedAccount = (bool)acctDetails.isUndocumented;
                fiAcctDetails.UndocumentedAccountSpecified = true;
            }
            else
                fiAcctDetails.UndocumentedAccountSpecified = false;

            return fiAcctDetails;
        }

        public DocSpec_Type DocSpec(string docType, int acctIndex)
        {
            DocSpec_Type docSpec = new DocSpec_Type();

            try
            {
                docSpec.DocTypeIndic = (OECDDocTypeIndic_EnumType)Enum.Parse(typeof(OECDDocTypeIndic_EnumType), docType);
            }
            catch (Exception e)
            {
                Debug.WriteLine("DocTypeIndic Parse Error: " + e.Message);
            }

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime date = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            docSpec.DocRefId = "DOC" + date.ToString("yyyyMMddHHmmssfff") + acctIndex.ToString().PadLeft(3, '0');

            return docSpec;

        }

        public MessageSpec_Type MessageSpec(string returnYear, string aeoiId, string msgType)
        {
            MessageSpec_Type msg = new MessageSpec_Type();

            msg.AeoiId = aeoiId;
            msg.ReturnYear = returnYear;

            //get time in HK
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            msg.Timestamp = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            msg.MessageRefId = returnYear + aeoiId + msg.Timestamp.ToString("yyyyMMddHHmmss") + 00;

            if (msgType.Equals(CrsMessageTypeIndic_EnumType.CRS701))
                msg.MessageTypeIndic = CrsMessageTypeIndic_EnumType.CRS701; // new data
            else
                msg.MessageTypeIndic = CrsMessageTypeIndic_EnumType.CRS702; // correction data

            //get FI name based on ID
            DbExportManager db = new DbExportManager();
            msg.FIName = db.GetFIName(aeoiId);

            return msg;
        }
    }
}