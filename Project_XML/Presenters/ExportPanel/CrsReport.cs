using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;
using Project_XML.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Project_XML.Presenters.ExportPanel
{
    public class CrsReport
    {
        private string messageRefId;

        public object[] NewReport(List<Dictionary<string, string>> entries, Dictionary<string, string> reportArgs, string schemaPath, string acctTypeCheck)
        {

            //var ent = entries.Split(',');

            DbExportManager db = new DbExportManager();
            AEOI_Report report = new AEOI_Report();

            //MessageSpec_Type
            report.MessageSpec = MessageSpec(reportArgs["year"], reportArgs["aeoiId"], reportArgs["msgSpecType"], reportArgs["contact"], reportArgs["attentionNote"]);

            List<CorrectableAccountReport_Type> correctableAccounts = new List<CorrectableAccountReport_Type>();
            int i = 0;

            foreach (Dictionary<string, string> d in entries)
            {

                int acctID = Convert.ToInt32(d["AcctID"]);
                int acctHolderId = Convert.ToInt32(d["AcctHolderId"]);
                string country = d["Country"];
                string acctNum = db.GetAccountNumber(acctID);

                if (db.isEntity(acctHolderId))
                {
                    //Account Reports
                    CorrectableAccountReport_Type account = new CorrectableAccountReport_Type();
                    //DocSpec
                    account.DocSpec = DocSpec
                        (d["DocSpecType"], i, acctNum, d["CorrFileSerialNumber"], d["CorrDocRefId"], d["CorrAcctNumber"], acctID);

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

                    //ControllingPerson
                    Debug.WriteLine("Input: {0} {1} {2}", acctHolderId, country, acctNum);
                    List<ControllingPersonModel> ctrlList = db.GetEntityCtrlPerson(acctHolderId, country, acctID);
                    if (ctrlList != null && ctrlList.Count != 0)
                    {
                        ControllingPerson_Type[] ctrlPersons = ControllingPerson(ctrlList);
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
                else // account holder is an Individual/Person
                {
                    Debug.WriteLine("Individual Account.");
                    string[] resCountries = db.GetPersonReportableResCountry(acctHolderId);

                    //Account Reports
                    CorrectableAccountReport_Type account = new CorrectableAccountReport_Type();
                    //DocSpec
                    account.DocSpec = DocSpec
                        (d["DocSpecType"], i, acctNum, d["CorrFileSerialNumber"], d["CorrDocRefId"], d["CorrAcctNumber"], acctID);

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
                    PersonDetailsModel model = db.GetPersonDetails(acctHolderId);

                    //Get AcctHolder details
                    PersonParty_Type person = PersonParty(model, false);

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

            //ReportingGroup
            CrsBody_Type body = new CrsBody_Type();
            body.ReportingGroup = correctableAccounts.ToArray();
            report.CrsBody = body;


            //Serialize class to XML document.
            XmlSerializer serializer = new XmlSerializer(typeof(AEOI_Report));
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, report);
                stream.Position = 0;
                xmlDoc.Load(stream);
            }

            if (Validate_XML(xmlDoc, schemaPath))
            {
                object[] obj = { xmlDoc, report.MessageSpec.MessageRefId };//[0] - xmldoc, [1] - name of the file
                return obj;
            }
            else
            {
                return null;
            }

        }
        /***********************************************************************************
        * 
        * 
        * CRS Report Generation Function 
        * 
        * ********************************************************************************/

        //MessageSpec_Type
        public MessageSpec_Type MessageSpec(string returnYear, string aeoiId, string msgType, string contact, string attentionNote)
        {
            MessageSpec_Type msg = new MessageSpec_Type();
            DbExportManager db = new DbExportManager();

            msg.AeoiId = aeoiId;
            msg.ReturnYear = returnYear;

            //get time in HK
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            msg.Timestamp = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            int i = 0;
            string msgRefId;

            do
            {
                msgRefId = returnYear + aeoiId + msg.Timestamp.ToString("yyyyMMddHHmmss") + i.ToString().PadLeft(2, '0');
                i++;
            }
            while (db.isMsgIdExists(msgRefId)); // check if msgRefId exists in db

            msg.MessageRefId = msgRefId;

            string note;
            string con;

            if (!contact.Equals("") && contact != null)
            {
                con = contact;
                msg.Contact = contact;
            }
            else
                con = null;

            if (!attentionNote.Equals("") && attentionNote != null)
            {
                note = attentionNote;
                msg.AttentionNote = attentionNote;
            }
            else
                note = null;

            if (msgType.Equals(CrsMessageTypeIndic_EnumType.CRS701))
                msg.MessageTypeIndic = CrsMessageTypeIndic_EnumType.CRS701; // new data
            else
                msg.MessageTypeIndic = CrsMessageTypeIndic_EnumType.CRS702; // correction data

            //get FI name based on ID
            msg.FIName = db.GetFIName(aeoiId);

            //insert into MessageSpec table
            DbImportManager dbImport = new DbImportManager();
            dbImport.NewMessageSpec(msgRefId, msgType, returnYear, note, con, aeoiId);

            messageRefId = msgRefId;

            return msg;
        }

        /**********************************************************************************
         * 
         * 
         * CRSBody Type Function
         * 
         * *******************************************************************************/

        //DocSpec_ype
        public DocSpec_Type DocSpec(string docType, int acctIndex, string acctNum, string corrFSN, string corrDocRef, string corrAcct, int acctID)
        {
            DocSpec_Type docSpec = new DocSpec_Type();

            // if corraccountnumber != null
            if ((corrFSN != null || corrFSN != "") && (corrDocRef != null || corrDocRef != "") && (corrAcct != null || corrAcct != ""))
            {
                docSpec.CorrFileSerialNumber = corrFSN;
                docSpec.CorrDocRefId = corrDocRef;
                docSpec.CorrAccountNumber = corrAcct;
            }
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

            string refId = "DOC" + date.ToString("yyyyMMddHHmmssfff") + acctIndex.ToString().PadLeft(3, '0');
            docSpec.DocRefId = refId;

            //Add entry to docspec table
            DbImportManager dbImport = new DbImportManager();
            dbImport.NewDocSpec(refId, docType, messageRefId, corrFSN, corrDocRef, corrAcct, acctNum, acctID);

            return docSpec;

        }

        //FIAccountNumber_Type
        public FIAccountNumber_Type FIAccountNumber(AccountDetailsModel acctDetails)
        {
            FIAccountNumber_Type fiAcctDetails = new FIAccountNumber_Type();

            fiAcctDetails.ClosedAccountSpecified = false;
            fiAcctDetails.DormantAccountSpecified = false;
            fiAcctDetails.UndocumentedAccountSpecified = false;

            foreach (PropertyInfo p in typeof(AccountDetailsModel).GetProperties())
            {
                if (p.GetValue(acctDetails) != null && !p.GetValue(acctDetails).Equals(""))
                {
                    switch (p.Name)
                    {
                        case "AcctNumber":
                            fiAcctDetails.Value = p.GetValue(acctDetails).ToString();
                            break;
                        case "AcctNumberType":
                            try
                            {
                                fiAcctDetails.AcctNumberType = (AcctNumberType_EnumType)Enum.Parse(typeof(AcctNumberType_EnumType), p.GetValue(acctDetails).ToString());
                                fiAcctDetails.AcctNumberTypeSpecified = true;
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("Account Number Type Error:" + e.Message);
                            }
                            break;
                        case "isClosed":
                            fiAcctDetails.ClosedAccount = (bool)acctDetails.isClosed;
                            fiAcctDetails.ClosedAccountSpecified = true;
                            break;
                        case "isDormant":
                            fiAcctDetails.DormantAccount = (bool)acctDetails.isDormant;
                            fiAcctDetails.DormantAccountSpecified = true;
                            break;
                        case "isUndocumented":
                            fiAcctDetails.UndocumentedAccount = (bool)acctDetails.isUndocumented;
                            fiAcctDetails.UndocumentedAccountSpecified = true;
                            break;
                    }
                }
            }

            return fiAcctDetails;
        }

        //MonAmnt_Type
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
                Debug.WriteLine(e.StackTrace); accountBalance.currCode = currCode_Type.HKD; // default currency if nothing is specified (HKD)
            }

            return accountBalance;
        }


        //Payment_Type
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


        /************************************************************
         * 
         * 
         * Person/Individual Function
         * 
         * *********************************************************/

        //PersonParty_Type
        public PersonParty_Type PersonParty(PersonDetailsModel person, bool isControlling)
        {
            PersonParty_Type personType = new PersonParty_Type();
            //NamePersonType
            personType.Name = NamePerson(person);

            //ResCountryCode
            //get only all res country codes if it is a controlling person
            if (isControlling)
            {
                List<CountryCode_Type> cList = new List<CountryCode_Type>();
                foreach (string c in person.ResCountryCode)
                {
                    cList.Add((CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), c));
                }
                CountryCode_Type[] countries = cList.ToArray();
                personType.ResCountryCode = countries;
            }

            //Address
            personType.Address = Address(person.Address);

            //TIN
            Debug.WriteLine("TIN:" + person.INVal);
            if (person.INVal != null && !person.INVal.Equals(""))
            {
                List<TIN_Type> tinList = new List<TIN_Type>();

                var inStr = person.INVal.Split(';');
                foreach (string s in inStr)
                {
                    TIN_Type inType = new TIN_Type();

                    string[] inCsv = s.Split(',');

                    inType.Value = inCsv[0];
                    if (inCsv[1] != null && !inCsv[1].Equals(""))
                    {
                        Debug.WriteLine(inCsv[1]);
                        inType.issuedBy = (CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), inCsv[1]);
                        inType.issuedBySpecified = true;
                    }
                    else
                        inType.issuedBySpecified = false;

                    tinList.Add(inType);
                }

                personType.TIN = tinList.ToArray();
            }

            //BirthInfo
            PersonParty_TypeBirthInfo birthdate = BirthDateInfo(person.Birthdate);
            if (birthdate != null)
                personType.BirthInfo = birthdate;

            return personType;
        }

        //NamePerson_Type
        public NamePerson_Type[] NamePerson(PersonDetailsModel model)
        {
            NamePerson_Type name = new NamePerson_Type();

            foreach (PropertyInfo p in typeof(PersonDetailsModel).GetProperties())
            {
                // instantiate/assign only NON-NULL values
                if (p.GetValue(model) != null && !p.GetValue(model).Equals(""))
                {
                    switch (p.Name)
                    {
                        case "PrecedingTitle":
                            name.PrecedingTitle = p.GetValue(model).ToString();
                            break;
                        case "Title":
                            name.Title = p.GetValue(model).ToString().Split(',');
                            break;
                        case "Firstname":
                            name.FirstName = p.GetValue(model).ToString();
                            break;
                        case "MiddeleName":
                            name.MiddleName = p.GetValue(model).ToString();
                            break;
                        case "NamePrefix":
                            name.NamePrefix = p.GetValue(model).ToString();
                            break;
                        case "LastName":
                            name.LastName = p.GetValue(model).ToString();
                            break;
                        case "GenerationIdentifier":
                            name.GenerationIdentifier = p.GetValue(model).ToString().Split(',');
                            break;
                        case "Suffix":
                            name.Suffix = p.GetValue(model).ToString().Split(',');
                            break;
                        case "GeneralSuffix":
                            name.GeneralSuffix = p.GetValue(model).ToString();
                            break;
                        case "NameType":
                            name.nameType = (OECDNameType_EnumType)Enum.Parse(typeof(OECDNameType_EnumType), p.GetValue(model).ToString());
                            name.nameTypeSpecified = true;
                            break;

                    }
                }
            }

            NamePerson_Type[] nameList = new NamePerson_Type[] { name }; //we assume that a person has only 1 name
            return nameList;
        }

        //PersonParty_TypeBirthInfo
        public PersonParty_TypeBirthInfo BirthDateInfo(BirthDateModel birthdate)
        {
            bool flag = false;
            PersonParty_TypeBirthInfo bday = new PersonParty_TypeBirthInfo();

            foreach (PropertyInfo p in typeof(BirthDateModel).GetProperties())
            {
                // instantiate/assign only NON-NULL valuess
                if (p.GetValue(birthdate) != null && !p.GetValue(birthdate).Equals(""))
                {
                    switch (p.Name)
                    {
                        case "BirthDate":
                            bday.BirthDate = ConvertBirthDate(p.GetValue(birthdate).ToString());
                            bday.BirthDateSpecified = true;
                            break;
                        case "BirthCity":
                            bday.City = p.GetValue(birthdate).ToString();
                            break;
                        case "BirthCitySubentity":
                            bday.CitySubentity = p.GetValue(birthdate).ToString();
                            break;
                        case "BirthCountry":
                            CountryCode_Type country;
                            PersonParty_TypeBirthInfoCountryInfo bCountry = new PersonParty_TypeBirthInfoCountryInfo();
                            if (Enum.TryParse<CountryCode_Type>(p.GetValue(birthdate).ToString(), out country) == true)
                            {
                                bCountry.Item = country;
                                bday.CountryInfo = bCountry;
                            }
                            else
                            {
                                bCountry.Item = p.GetValue(birthdate).ToString();
                                bday.CountryInfo = bCountry;
                            }
                            break;
                    }

                    flag = true;
                }
            }

            if (flag)   //return only if at least one of the attributes is NOT NULL
                return bday;
            else
                return null;
        }

        //Controlling Person 
        public ControllingPerson_Type[] ControllingPerson(List<ControllingPersonModel> ctrlList)
        {
            List<ControllingPerson_Type> ctrlPersons = new List<ControllingPerson_Type>();

            foreach (ControllingPersonModel model in ctrlList)
            {
                //CtrlPersonType
                ControllingPerson_Type person = new ControllingPerson_Type();

                if (model.CtrlPersonType != null && !model.CtrlPersonType.Equals(""))
                {
                    person.CtrlgPersonType = (CrsCtrlgPersonType_EnumType)Enum.Parse(typeof(CrsCtrlgPersonType_EnumType), model.CtrlPersonType);
                    person.CtrlgPersonTypeSpecified = true;
                }
                else
                    person.CtrlgPersonTypeSpecified = false;

                person.Individual = PersonParty(model, true);

                ctrlPersons.Add(person);
            }

            return ctrlPersons.ToArray();
        }

        /*************************************************
         * 
         * 
         * Organisation/Entity Functions
         * 
         * *************************************************/


        public OrganisationParty_Type OrganisationType(EntityDetailsModel entity, string resCountryCode)
        {
            OrganisationParty_Type org = new OrganisationParty_Type();

            //Res Country Code
            CountryCode_Type countryCode = (CountryCode_Type)Enum.Parse(typeof(CountryCode_Type), resCountryCode);
            CountryCode_Type[] countries = { countryCode };
            org.ResCountryCode = countries;

            //INType
            if (entity.INVal != null && !entity.INVal.Equals(""))
            {
                List<OrganisationIN_Type> inList = new List<OrganisationIN_Type>();

                var inStr = entity.INVal.Split(';');
                Debug.WriteLine("INVAL Values: " + entity.INVal);
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
            }

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
            org.Address = Address(entity.Address);

            return org;

        }


        /***************************************************
         * 
         * 
         * Address Functions
         * 
         * *************************************************/

        //Address main function
        public Address_Type[] Address(List<AddressModel> addrList)
        {

            List<Address_Type> addrTypeList = new List<Address_Type>();

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


        //Address_Type object[], AdressFixed_Type
        public object[] AddressType(AddressModel addr)
        {
            List<object> addrArr = new List<object>();

            AddressFix_Type fix = new AddressFix_Type();
            string freeStr = "";

            foreach (PropertyInfo p in typeof(AddressModel).GetProperties())
            {
                if (p.GetValue(addr) != null && !p.GetValue(addr).Equals(""))
                {
                    switch (p.Name)
                    {
                        case "Street":
                            fix.Street = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "BuildingIdentifier":
                            fix.BuildingIdentifier = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "SuiteIdentifier":
                            fix.SuiteIdentifier = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "FloorIdentifier":
                            fix.FloorIdentifier = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "DistrictName":
                            fix.DistrictName = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "POB":
                            fix.POB = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "PostCode":
                            fix.PostCode = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "City":
                            fix.City = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                        case "CountrySubentity":
                            fix.CountrySubentity = p.GetValue(addr).ToString();
                            freeStr += p.GetValue(addr).ToString() + ", ";
                            break;
                    }

                }

            }
            //addrArr.Add(fix);

            if (!freeStr.Equals(""))
            {
                addrArr.Add(fix);
                addrArr.Add(AddressFree(freeStr.Substring(0, freeStr.Length - 2))); // concatination of fixed address fields
            }
            else
                addrArr.Add(AddressFree(addr.FreeLine));

            return addrArr.ToArray();

        }

        //AddressFree_Type
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

        /*********************************************
         * 
         * 
         * XML Validation Functions
         * 
         * *******************************************/
        public static bool Validate_XML(XmlDocument doc, string schemaPath)
        {
            /*
            XmlSchemaSet schema = new XmlSchemaSet();
            
            schema.Add("http://www.ird.gov.hk/AEOI/crs/v1", schemaPath+"/HK_XMLSchema_v0.1.xsd");
            schema.Add("urn:oecd:ties:isocrstypes:v1", schemaPath + "/isocrstypes_v1.0.xsd");
            schema.Add("http://www.ird.gov.hk/AEOI/aeoitypes/v1", schemaPath + "/aeoitypes_v0.1.xsd");
            */
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("http://www.ird.gov.hk/AEOI/crs/v1", schemaPath + "/HK_XMLSchema_v0.1.xsd");
            settings.Schemas.Add("urn:oecd:ties:isocrstypes:v1", schemaPath + "/isocrstypes_v1.0.xsd");
            settings.Schemas.Add("http://www.ird.gov.hk/AEOI/aeoitypes/v1", schemaPath + "/aeoitypes_v0.1.xsd");
            settings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create(new StringReader(doc.OuterXml), settings);
            XmlDocument document = new XmlDocument();

            Debug.WriteLine("Schema validation starting...");
            try
            {
                document.Load(reader);
                Debug.WriteLine("XML Validation Successful!");
                return true;
            }
            catch (XmlSchemaValidationException e)
            {
                Debug.WriteLine("XML Validation Error: " + e.Message);
                return false;
            }
        }
        //for testing
        public void Validate_XML()
        {
            AEOI_Report report = new AEOI_Report();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("http://www.ird.gov.hk/AEOI/crs/v1", "C:\\Users\\adrian.m.perez\\Documents\\Visual Studio 2015\\Projects\\Project_XML\\Project_XML\\schema\\HK_XMLSchema_v0.1.xsd");
            settings.Schemas.Add("urn:oecd:ties:isocrstypes:v1", "C:\\Users\\adrian.m.perez\\Documents\\Visual Studio 2015\\Projects\\Project_XML\\Project_XML\\schema\\isocrstypes_v1.0.xsd");
            settings.Schemas.Add("http://www.ird.gov.hk/AEOI/aeoitypes/v1", "C:\\Users\\adrian.m.perez\\Documents\\Visual Studio 2015\\Projects\\Project_XML\\Project_XML\\schema\\aeoitypes_v0.1.xsd");

            settings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create("C:/Users/adrian.m.perez/Desktop/sample1.xml", settings);
            XmlDocument document = new XmlDocument();

            document.Load(reader);


            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

            try
            {
                document.Validate(eventHandler);
                Debug.WriteLine("Success!");
            }
            catch (XmlSchemaValidationException e)
            {
                Debug.WriteLine("Error: " + e.Message);
            }

            //reader.Close();

        }


        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }

        }


        /****************************************************
         * 
         * 
         * Other Functions
         * 
         * ***********************************************/

        public DateTime ConvertBirthDate(string birthDate)
        {
            try
            {
                DateTime date = DateTime.Parse(birthDate, CultureInfo.InvariantCulture);
                Debug.WriteLine("Converting Successful!: " + date.ToString());
                return date;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Date Conversion Error: " + e.Message);
                return default(DateTime);
            }
        }
    }
}