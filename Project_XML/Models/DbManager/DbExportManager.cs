using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace Project_XML.Models.DbManager
{
    public class DbExportManager : DbConnManager
    {
        public List<AccountModel> GetAllAccounts()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT AcctNumber, Name, FirstName+' '+LastName AS PName
                                        FROM Account
                                        LEFT JOIN Entity ON EntityId = P_Ent_Id
                                        LEFT JOIN Person ON PId = P_Ent_Id
                                        ORDER BY AcctNumber";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<AccountModel> accounts = new List<AccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AccountModel model = new AccountModel();

                            model.AcctNumber = reader[0].ToString();

                            model.AcctHolder = GetAcctHolderName(reader[1].ToString(), reader[2].ToString());

                            accounts.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get All Accounts Error: " + e.Message);
                        return null;
                    }

                    return accounts;
                }

            }
        }

        public List<MessageSpecModel> GetAllMessageSpec()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT ReturnYear, MessageRefId, MessageTypeIndic
                                        FROM MessageSpec ORDER BY ReturnYear";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<MessageSpecModel> msgSpecs = new List<MessageSpecModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MessageSpecModel model = new MessageSpecModel();

                            model.ReturnYear = reader[0].ToString();
                            model.MessageRefId = reader[1].ToString();

                            model.MessageType = GetMessageType(reader[2].ToString());

                            msgSpecs.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get All Message Specs Error: " + e.Message);
                        return null;
                    }

                    return msgSpecs;
                }
            }
        }

        public List<string> GetAllMsgRefId()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT MessageRefId FROM MessageSpec";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<string> msgRefId = new List<string>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            msgRefId.Add(reader[0].ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get All Message Ref Id Error: " + e.Message);
                        return null;
                    }

                    return msgRefId;
                }
            }
        }

        public List<DocSpecModel> GetAllDocSpec(string msgRefId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT DocRefId, Account.AcctNumber, Name, FirstName+' '+LastName as PName
                                        FROM DocSpec, MessageSpec, Account
                                        LEFT JOIN Entity on P_Ent_Id = EntityId
                                        LEFT JOIN Person on P_Ent_Id = PId
                                        WHERE DocSpec.AcctNumber = Account.AcctNumber 
                                        AND DocSpec.MessageRefId = MessageSpec.MessageRefid 
                                        AND DocSpec.MessageRefId = @msgRefId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@msgRefId", SqlDbType.VarChar, 40));
                    cmd.Prepare();

                    cmd.Parameters["@msgRefId"].Value = msgRefId;

                    List<DocSpecModel> docSpec = new List<DocSpecModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocSpecModel model = new DocSpecModel();

                            model.DocRefId = reader[0].ToString();
                            model.AcctNumber = reader[1].ToString();
                            model.AcctHolder = GetAcctHolderName(reader[2].ToString(), reader[3].ToString());

                            docSpec.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get All Doc Spec Error: " + e.Message);
                        return null;
                    }

                    return docSpec;
                }
            }
        }

        public AccountDetailsModel GetAccountDetials(string acctNumber, string acctHolderId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT A.AcctNumber, AcctNumberType, isUndocumented, isClosed, isDormant, AccountBalance, A.CurrCode, PaymentType, Amount, P.CurrCode
                                        FROM Account A, Entity E,Payment P
                                        WHERE A.AcctNumber=P.AcctNumber and A.P_Ent_Id=E.EntityId AND A.AcctNumber=@acctNumber AND E.EntityId=@acctHolderId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Parameters.Add(new SqlParameter("@acctHolderId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@acctNumber"].Value = acctNumber;
                    cmd.Parameters["@acctHolderId"].Value = acctHolderId;

                    AccountDetailsModel model = new AccountDetailsModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            model.AcctNumber = reader[0].ToString();
                            model.AcctNumberType = reader[1].ToString();
                            model.isUndocumented = Convert.ToBoolean(reader[2]);
                            model.isClosed = Convert.ToBoolean(reader[3]);
                            model.isDormant = Convert.ToBoolean(reader[4]);
                            model.AccountBalance = Convert.ToDecimal(reader[5]);
                            model.ACurrCode = reader[6].ToString();
                            model.PaymentType = reader[7].ToString();
                            model.Amount = Convert.ToDecimal(reader[8]);
                            model.PCurrCode = reader[9].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Account Detials Error: " + e.Message);
                        return null;
                    }

                    return model;
                }
            }
        }

        public EntityDetailsModel GetEntityDetails(int entityId, string countryCode)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT CountryCode,Name,NameType,
	                                        STUFF((SELECT ';'+Value+','+CountryCode+','+IType 
			                                        FROM INType, Entity WHERE P_Ent_Id=EntityId AND EntityId=@entityId
			                                        FOR XML PATH('')),1,1,'') AS INVal
                                        FROM Entity, ResCountryCode WHERE EntityId=P_Ent_Id AND EntityId=@entityId AND CountryCode=@countryCode ";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;
                    cmd.Parameters["@countryCode"].Value = countryCode;

                    EntityDetailsModel model = new EntityDetailsModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            model.CountryCode = reader[0].ToString();
                            model.Name = reader[1].ToString();
                            model.NameType = reader[2].ToString();
                            model.INVal = reader[3].ToString();
                        }

                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Get Entity Details Error: " + e.Message);
                        return null;
                    }

                    return model;
                }
            }
            
        }

        public PersonDetailsModel GetPersonDetails(int pId, string countryCode)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT CountryCode,Person.*,
	                                        STUFF((SELECT ';'+Value+','+CountryCode+','+IType 
			                                        FROM INType, Person WHERE P_Ent_Id=PId AND PId=@pId
			                                        FOR XML PATH('')),1,1,'') AS INVal
                                        FROM Person, ResCountryCode WHERE PId=P_Ent_Id AND PId=@pId AND CountryCode=@countryCode";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar,2));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@countrCode"].Value = countryCode;

                    PersonDetailsModel model = new PersonDetailsModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.CountryCode = reader["CountryCode"].ToString();
                            model.PreceedingTitle = reader["PreceedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.NameType = reader["NameType"].ToString();
                            model.BirthDate = ConvertBirthDate(reader["BirthDate"].ToString());
                            model.BirthCity = reader["BirthCity"].ToString();
                            model.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.BirthCountry = reader["BirthCountry"].ToString();
                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.INVal = reader["INVal"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Person Details Error: " + e.Message);
                        return null;
                    }

                    return model;
                }
            }
        }

        public List<AddressModel> GetEntityAddress(int entityId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT * FROM Address, Entity WHERE P_Ent_Id=EntityId AND EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;

                    List<AddressModel> addrList = new List<AddressModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            AddressModel model = new AddressModel();

                            model.CountryCode = reader["CountryCode"].ToString();
                            model.Street = reader["Street"].ToString();
                            model.BuildingIdentifier = reader["BuildingIdentifier"].ToString();
                            model.SuiteIdentifier = reader["SuiteIdentifier"].ToString();
                            model.FloorIdentifier = reader["FloorIdentifier"].ToString();
                            model.DistrictName = reader["DistrictName"].ToString();
                            model.POB = reader["POB"].ToString();
                            model.PostCode = reader["PostCode"].ToString();
                            model.City = reader["City"].ToString();
                            model.CountrySubentity = reader["CountrySubentity"].ToString();
                            model.FreeLine = reader["FreeLine"].ToString();
                            model.AddressType = reader["AddressType"].ToString();
                            model.isFixed = Convert.ToBoolean(reader["isFixed"]);

                            addrList.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Entity Address Error: " + e.Message);
                        return null;
                    }
                    return addrList;
                }
            }
        }

        public List<AddressModel> GetPersonAddress(int pId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT A.* FROM Address A, Person WHERE A.P_Ent_Id=PId AND PId=@pId ";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;

                    List<AddressModel> addrList = new List<AddressModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AddressModel model = new AddressModel();

                            model.CountryCode = reader["CountryCode"].ToString();
                            model.Street = reader["Street"].ToString();
                            model.BuildingIdentifier = reader["BuildingIdentifier"].ToString();
                            model.SuiteIdentifier = reader["SuiteIdentifier"].ToString();
                            model.FloorIdentifier = reader["FloorIdentifier"].ToString();
                            model.DistrictName = reader["DistrictName"].ToString();
                            model.POB = reader["POB"].ToString();
                            model.PostCode = reader["PostCode"].ToString();
                            model.City = reader["City"].ToString();
                            model.CountrySubentity = reader["CountrySubentity"].ToString();
                            model.FreeLine = reader["FreeLine"].ToString();
                            model.AddressType = reader["AddressType"].ToString();
                            model.isFixed = Convert.ToBoolean(reader["isFixed"]);

                            addrList.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Person Address Error: " + e.Message);
                        return null;
                    }
                    return addrList;
                }
            }
        }

        public List<ControllingPerson> GetEntityCtrlPerson(int entityId, string countryCode, string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnetion"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.*, CtrlPersonType
                                        FROM Person P, ControllingPerson C, Account A, ResCountryCode R, Entity E
                                        WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND E.EntityId=A.P_Ent_Id
		                                        AND R.CountryCode=@countryCode AND A.AcctNumber=@acctNumber AND E.EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@accNumber"].Value = acctNumber;

                    List<ControllingPerson> ctrlList = new List<ControllingPerson>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            ControllingPerson model = new ControllingPerson();

                            model.PreceedingTitle = reader["PreceedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.NameType = reader["NameType"].ToString();
                            model.BirthDate = ConvertBirthDate(reader["BirthDate"].ToString());
                            model.BirthCity = reader["BirthCity"].ToString();
                            model.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.BirthCountry = reader["BirthCountry"].ToString();
                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.CtrlPersonType = reader["CtrlPersonType"].ToString();

                            ctrlList.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Entity CtrlPerson Error: " + e.Message);
                        return null;
                    }
                    return ctrlList;
                }
            }
        }

        public List<ControllingPerson> GetPersonCtrlPerson(int pId, string countryCode, string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnetion"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.*, CtrlPersonType
                                        FROM Person P, ControllingPerson C, Account A, ResCountryCode R
                                        WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND P.PId=A.P_Ent_Id
		                                        AND R.CountryCode=@countryCode AND A.AcctNumber=@acctNumber AND P.PId=@pId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@accNumber"].Value = acctNumber;

                    List<ControllingPerson> ctrlList = new List<ControllingPerson>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ControllingPerson model = new ControllingPerson();

                            model.PreceedingTitle = reader["PreceedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.NameType = reader["NameType"].ToString();
                            model.BirthDate = ConvertBirthDate(reader["BirthDate"].ToString());
                            model.BirthCity = reader["BirthCity"].ToString();
                            model.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.BirthCountry = reader["BirthCountry"].ToString();
                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.CtrlPersonType = reader["CtrlPersonType"].ToString();

                            ctrlList.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Entity CtrlPerson Error: " + e.Message);
                        return null;
                    }
                    return ctrlList;
                }
            }
        }

        public string GetMessageType(string codeType)
        {
            if (codeType.Equals("CRS701"))
            {
                return "New";
            }
            else if (codeType.Equals("CRS702"))
            {
                return "Correction";
            }

            return null;
        }

        public string GetAcctHolderName(string eName, string pName)
        {
            if (eName != null && eName != "")
            {
                return eName;
            }
            else
            {
                return pName;
            }
        }

        public DateTime ConvertBirthDate(string birthDate)
        {
            try
            {
                DateTime date = DateTime.ParseExact(birthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return date;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Date Conversion Error: " + e.Message);
                return default(DateTime);
            }
        }
        

        private static XmlDocument GenerateXMLDoc(XmlReader reader, string rootName)
        {
            StringBuilder sb = new StringBuilder();

            //create header and root name for the xml document
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
            sb.AppendLine(String.Format("<{0}>", rootName));


            /* refererence code for parsing xml
             * http://stackoverflow.com/questions/26787765/xmlreader-skips-elements
             */

            reader.Read();
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element) // check xml for each element occurence. 
                    sb.Append(reader.ReadOuterXml());
                else
                    reader.Read();
            }

            sb.AppendLine(string.Format("</{0}>", rootName));   // append the root end tag

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }
    }
}