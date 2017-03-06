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

        public string GetFIName(string aeoiId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT FIName From AeoiProfile WHERE AeoiId=@aeoiId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@aeoiId", SqlDbType.VarChar, 12));
                    cmd.Prepare();

                    cmd.Parameters["@aeoiId"].Value = aeoiId;

                    try
                    {
                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get FIName Error: " + e.Message);
                        return null;
                    }
                }
            }
        }

        public AccountDetailsModel GetAccountDetials(string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT AcctNumber, AcctNumberType, isUndocumented, isClosed, isDormant, AccountBalance, CurrCode
                                        FROM Account
                                        WHERE AcctNumber=@acctNumber";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@acctNumber"].Value = acctNumber;

                    AccountDetailsModel model = new AccountDetailsModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            model.AcctNumber = reader[0].ToString();
                            model.AcctNumberType = reader[1].ToString();

                            if (reader[2] is DBNull)
                                model.isUndocumented = null;
                            else
                                model.isUndocumented = Convert.ToBoolean(reader[2]);

                            if (reader[3] is DBNull)
                                model.isClosed = null;
                            else
                                model.isClosed = Convert.ToBoolean(reader[3]);

                            if (reader[4] is DBNull)
                                model.isDormant = null;
                            else
                                model.isDormant = Convert.ToBoolean(reader[4]);

                            model.AccountBalance = Convert.ToDecimal(reader[5]);
                            model.ACurrCode = reader[6].ToString();
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

        public List<PaymentModel> GetPayments(string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT PaymentType, Amount, P.CurrCode
                                        FROM Payment P, Account A
                                        WHERE P.AcctNumber = A.AcctNumber AND A.AcctNumber=@acctNumber";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@acctNumber"].Value = acctNumber;

                    List<PaymentModel> payments = new List<PaymentModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                            return null;
                        else
                        {
                            while (reader.Read())
                            {
                                PaymentModel model = new PaymentModel();

                                model.PaymentType = reader[0].ToString();
                                if (reader[1] == null)
                                    model.Amount = null;
                                else
                                    model.Amount = Convert.ToDecimal(reader[1]);
                                model.CurrCode = reader[2].ToString();

                                payments.Add(model);
                            }

                            return payments;
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Payments Error:" + e.Message);
                        return null;
                    }
                }
            }
        }

        public EntityDetailsModel GetEntityDetails(int acctHolderId, string countryCode)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT EntityId,Name,NameType,AcctHolderType,
	                                        STUFF((SELECT ';'+Value+','+CountryCode+','+IType 
			                                        FROM INType, Entity
			                                        WHERE P_Ent_Id=EntityId AND EntityId=@acctHolderId
			                                        FOR XML PATH('')),1,1,'') AS INVal
                                        FROM Entity E, ResCountryCode
                                        WHERE EntityId=P_Ent_Id AND EntityId=@acctHolderId AND CountryCode=@countryCode";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctHolderId", SqlDbType.VarChar, 72));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Prepare();

                    cmd.Parameters["@acctHolderId"].Value = acctHolderId;
                    cmd.Parameters["@countryCode"].Value = countryCode;

                    

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        EntityDetailsModel model = new EntityDetailsModel();

                        while (reader.Read())
                        {
                            model.EntityId = Convert.ToInt32(reader["EntityId"]);
                            model.Name = reader["Name"].ToString();
                            model.NameType = reader["NameType"].ToString();
                            model.AcctHolderType = reader["AcctHolderType"].ToString();
                            model.INVal = reader["INVal"].ToString();

                            model.Address = GetEntityAddress(model.EntityId);
                        }
                        return model;

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Entity Details Error: " + e.Message);
                        return null;
                    }
                }
            }

        }

        public PersonDetailsModel GetPersonDetails(int pId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT Person.*,
	                                        STUFF((SELECT ';'+Value+','+CountryCode+','+IType 
			                                    FROM INType, Person WHERE P_Ent_Id=PId AND PId=@pId
			                                    FOR XML PATH('')),1,1,'') AS INVal
                                        FROM Person WHERE PId=@pId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;

                    PersonDetailsModel model = new PersonDetailsModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.PId = Convert.ToInt32(reader["PId"]);
                            model.PrecedingTitle = reader["PrecedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.Suffix = reader["Suffix"].ToString();
                            model.GeneralSuffix = reader["GeneralSuffix"].ToString();
                            model.NameType = reader["NameType"].ToString();

                            model.Birthdate = new BirthDateModel();
                            model.Birthdate.BirthDate = reader["BirthDate"].ToString();
                            model.Birthdate.BirthCity = reader["BirthCity"].ToString();
                            model.Birthdate.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.Birthdate.BirthCountry = reader["BirthCountry"].ToString();

                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.Address = GetPersonAddress(pId);
                            model.ResCountryCode = GetPersonResCountry(model.PId);
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
                    string cmdstr = @"SELECT A.* FROM Address A, Entity WHERE P_Ent_Id=EntityId AND EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;

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

        public List<ControllingPersonModel> GetEntityCtrlPerson(int entityId, string countryCode, string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.PId FROM Person P, ControllingPerson C, Account A, ResCountryCode R, Entity E
                                        WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND E.AcctNumber=A.AcctNumber 
		                                AND R.CountryCode=@countryCode AND A.AcctNumber=@acctNumber AND EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@acctNumber"].Value = acctNumber;

                    List<ControllingPersonModel> ctrlList = new List<ControllingPersonModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            ControllingPersonModel model = new ControllingPersonModel();
                            model = GetCtrlPersonDetails(Convert.ToInt32(reader[0]), acctNumber);
                            ctrlList.Add(model);
                        }
                        return ctrlList;
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Get Controller Person Error:" + e.Message);
                        return null;
                    }

                }
            }
        }

        public List<ControllingPersonModel> GetIndividualCtrlPerson(int pId, string countryCode, string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.PId FROM Person P, ControllingPerson C, Account A, ResCountryCode R, PersonAcctHolder PH
                                        WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND A.AcctNumber=PH.AcctNumber
		                                AND R.CountryCode=@countryCode AND A.AcctNumber=@acctNumber AND P.PId=@pId";
                                                            cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.VarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@acctNumber"].Value = acctNumber;

                    List<ControllingPersonModel> ctrlList = new List<ControllingPersonModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ControllingPersonModel model = new ControllingPersonModel();
                            model = GetCtrlPersonDetails(Convert.ToInt32(reader[0]), acctNumber);
                            ctrlList.Add(model);
                        }
                        return ctrlList;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Controller Person Ids Error:" + e.Message);
                        return null;
                    }

                }
            }
        }

        public ControllingPersonModel GetCtrlPersonDetails(int pId, string acctNumber)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.*,STUFF((SELECT ';'+Value+','+CountryCode+','+IType 
			                                        FROM INType, Person WHERE P_Ent_Id=PId AND PId=1004
			                                        FOR XML PATH('')),1,1,'') AS INVal,
			                                        CtrlPersonType
                                        FROM Person P, ControllingPerson C, Account A
                                        WHERE C.PId=P.PId AND C.AcctNumber=A.AcctNumber AND A.AcctNumber=@acctNumber AND P.PId=@pId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.VarChar, 72));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@acctNumber"].Value = acctNumber;
                    

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        ControllingPersonModel model = new ControllingPersonModel();

                        while (reader.Read())
                        {
                            model.PId = Convert.ToInt32(reader["PId"]);
                            model.PrecedingTitle = reader["PrecedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.Suffix = reader["Suffix"].ToString();
                            model.GeneralSuffix = reader["GeneralSuffix"].ToString();
                            model.NameType = reader["NameType"].ToString();

                            model.Birthdate = new BirthDateModel();
                            model.Birthdate.BirthDate = reader["BirthDate"].ToString();
                            model.Birthdate.BirthCity = reader["BirthCity"].ToString();
                            model.Birthdate.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.Birthdate.BirthCountry = reader["BirthCountry"].ToString();

                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.CtrlPersonType = reader["CtrlPersonType"].ToString();
                            model.Address = GetPersonAddress(pId);
                            model.ResCountryCode = GetPersonResCountry(model.PId);
                            model.INVal = reader["INVal"].ToString();
                        }

                        return model;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get CtrlPerson Details Error: " + e.Message);
                        Debug.WriteLine(e.StackTrace);
                        return null;
                    }
                }
            }
        }

        public List<ControllingPersonModel> GetPersonCtrlPerson(int pId, string countryCode, string acctNumber)
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

                    List<ControllingPersonModel> ctrlList = new List<ControllingPersonModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ControllingPersonModel model = new ControllingPersonModel();

                            model.PrecedingTitle = reader["PreceedingTitle"].ToString();
                            model.Title = reader["Title"].ToString();
                            model.Firstname = reader["Firstname"].ToString();
                            model.MiddleName = reader["MiddleName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.GenerationIdentifier = reader["GenerationIdentifier"].ToString();
                            model.NameType = reader["NameType"].ToString();
                            model.Birthdate.BirthDate = reader["BirthDate"].ToString();
                            model.Birthdate.BirthCity = reader["BirthCity"].ToString();
                            model.Birthdate.BirthCitySubentity = reader["BirthCitySubentity"].ToString();
                            model.Birthdate.BirthCountry = reader["BirthCountry"].ToString();
                            model.isIndividual = Convert.ToBoolean(reader["isIndividual"]);
                            model.CtrlPersonType = reader["CtrlPersonType"].ToString();
                            //model.ResCountryCode.Add(countryCode);
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

        public bool isEntity(int acctHolderId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = @"SELECT CAST(CASE
		                                WHEN (SELECT EntityId FROM Entity WHERE EntityId=@acctHolderId)IS NOT NULL
		                                THEN 1
		                                ELSE 0
		                                END AS BIT) AS isEntity";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctHolderId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@acctHolderId"].Value = acctHolderId;

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
        }

        public string[] GetPersonResCountry(int pId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = @"SELECT CountryCode
                                        FROM Person P, ResCountryCode R
                                        WHERE P.PId=R.P_Ent_Id AND P.PId=@pId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;

                    try
                    {
                        List<string> ctr = new List<string>();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            ctr.Add(reader[0].ToString());
                        }
                        return ctr.ToArray(); ;
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Get Person ResCountry Error:" + e.Message);
                        return null;
                    }
                }
            }
        }

        public string[] GetEntityResCountry(int entityId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = @"SELECT CountryCode
                                        FROM Entity, ResCountryCode
                                        WHERE EntityId=P_Ent_Id AND EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;

                    List<string> resCountry = new List<string>(); ;

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            resCountry.Add(reader[0].ToString());
                        }
                        return resCountry.ToArray();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Entity ResCountry Error:" + e.Message);
                        return null;
                    }
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