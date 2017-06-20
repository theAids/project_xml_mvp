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
                    string cmdstr = @"SELECT DISTINCT A.AcctID, A.AcctNumber, t2.Name, t2.AcctHolderId, B.CountryCode
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
			                                        FROM Account A, Entity E, ResCountryCode R
			                                        WHERE A.AcctID=E.AcctID and R.isReportable != 0
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
			                                        FROM Account A, Person P, PersonAcctHolder PH
		                                            WHERE A.AcctID=PH.AcctID AND P.PId=PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN ResCountryCode B 
	                                        ON B.P_Ent_Id = t2.AcctHolderId
                                        ORDER BY t2.Name ASC";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<AccountModel> accounts = new List<AccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AccountModel model = new AccountModel();

                            model.AcctID = Convert.ToInt32(reader[0]);
                            model.AcctNumber = reader[1].ToString();
                            model.AcctHolder = reader[2].ToString();
                            model.AcctHolderId = Convert.ToInt32(reader[3]);
                            model.Country = reader[4].ToString();

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

        public List<AccountModel> GetAllReportableAccounts()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId, R.CountryCode
                                        FROM Account A, Entity E, ResCountryCode R
                                        WHERE A.AcctID=E.AcctID AND E.EntityId=R.P_Ent_Id AND R.isReportable != 0
                                        UNION
                                        SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId, R.CountryCode
                                        FROM Account A, Person P, PersonAcctHolder PH,ResCountryCode R
                                        WHERE A.AcctID=PH.AcctID AND P.PId=PH.PId AND P.PId=R.P_Ent_Id AND R.isReportable != 0
                                        ORDER BY Name ASC";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<AccountModel> accounts = new List<AccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AccountModel model = new AccountModel();

                            model.AcctID = Convert.ToInt32(reader[0]);
                            model.AcctNumber = reader[1].ToString();
                            model.AcctHolder = reader[2].ToString();
                            model.AcctHolderId = Convert.ToInt32(reader[3]);
                            model.Country = reader[4].ToString();

                            accounts.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get All Reportable Accounts Error: " + e.Message);
                        return null;
                    }

                    return accounts;
                }

            }
        }

        public List<CorrAccountModel> GetCorrAccounts(string messageRef)//, List<string> corrAccountsList)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = string.Empty; 
                    if (string.IsNullOrEmpty(messageRef))
                    {
                        cmdstr = @"SELECT DISTINCT A.AcctNumber, t2.Name, t2.AcctHolderId, B.CountryCode, DS.DocRefId
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
			                                        FROM Account A, Entity E 
			                                        WHERE A.AcctNumber=E.AcctNumber
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
			                                        FROM Account A, Person P, PersonAcctHolder PH
		                                            WHERE A.AcctNumber=PH.AcctNumber AND P.PId=PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN ResCountryCode B 
	                                        ON B.P_Ent_Id = t2.AcctHolderId
	                                        -- AND B.isReportable = 1
	                                    LEFT JOIN DocSpec DS 
											ON A.AcctID = DS.AcctID
                                        ORDER BY t2.Name ASC";
                        cmd.CommandText = cmdstr;
                        cmd.Prepare();
                    }
                    else
                    {
                        cmdstr = @"SELECT DISTINCT A.AcctNumber, t2.Name, t2.AcctHolderId, B.CountryCode, DS.[DocRefId]
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
                                                    FROM Account A, Entity E
                                                    WHERE A.AcctNumber=E.AcctNumber
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
                                                    FROM Account A, Person P, PersonAcctHolder PH
                                                    WHERE A.AcctNumber=PH.AcctNumber AND P.PId=PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN dbo.ResCountryCode B 
                                            ON B.P_Ent_Id = t2.AcctHolderId
                                        LEFT JOIN dbo.DocSpec DS
	                                        ON A.AcctID = DS.AcctID
                                        LEFT JOIN dbo.MessageSpec MS 
	                                        ON DS.MessageRefId = MS.MessageRefid
                                        WHERE MS.MessageRefid = @MessageRef
                                        ORDER BY t2.Name ASC";
                        cmd.CommandText = cmdstr;
                        cmd.Parameters.Add(new SqlParameter("@MessageRef", SqlDbType.NVarChar, 40));
                        cmd.Prepare();
                        cmd.Parameters["@MessageRef"].Value = messageRef;
                    }

                    List<CorrAccountModel> corrAccounts = new List<CorrAccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CorrAccountModel model = new CorrAccountModel();

                            model.AcctNumber = reader[0].ToString();
                            model.AcctHolder = reader[1].ToString();
                            model.AcctHolderId = Convert.ToInt32(reader[2]);
                            model.Country = reader[3].ToString();
                            model.DocRefId = reader[4].ToString();
                            corrAccounts.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Corrected Accounts Error: " + e.Message);
                        return null;
                    }

                    return corrAccounts;
                }
            }
        }

        public List<DeleteAccountModel> GetDeletedAccounts(string messageRef)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = string.Empty;
                    if (string.IsNullOrEmpty(messageRef))
                    {
                        cmdstr = @"SELECT DISTINCT A.AcctNumber, t2.Name, t2.AcctHolderId, B.CountryCode
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
			                                        FROM Account A, Entity E 
			                                        WHERE A.AcctNumber=E.AcctNumber
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
			                                        FROM Account A, Person P, PersonAcctHolder PH
		                                            WHERE A.AcctNumber=PH.AcctNumber AND P.PId=PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN ResCountryCode B 
	                                        ON B.P_Ent_Id = t2.AcctHolderId
                                        ORDER BY t2.Name ASC";
                        cmd.CommandText = cmdstr;
                        cmd.Prepare();
                    }
                    else
                    {
                        cmdstr = @"SELECT DISTINCT A.AcctNumber, t2.Name, t2.AcctHolderId, B.CountryCode
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
                                                    FROM Account A, Entity E
                                                    WHERE A.AcctNumber=E.AcctNumber
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
                                                    FROM Account A, Person P, PersonAcctHolder PH
                                                    WHERE A.AcctNumber=PH.AcctNumber AND P.PId=PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN dbo.ResCountryCode B 
                                            ON B.P_Ent_Id = t2.AcctHolderId
                                        LEFT JOIN dbo.DocSpec DS
	                                        ON A.AcctID = DS.AcctID
                                        LEFT JOIN dbo.MessageSpec MS 
	                                        ON DS.MessageRefId = MS.MessageRefid
                                        WHERE MS.MessageRefid = @MessageRef
                                        ORDER BY t2.Name ASC";
                        cmd.CommandText = cmdstr;
                        cmd.Parameters.Add(new SqlParameter("@MessageRef", SqlDbType.NVarChar, 40));
                        cmd.Prepare();
                        cmd.Parameters["@MessageRef"].Value = messageRef;
                    }

                    List<DeleteAccountModel> delAccounts = new List<DeleteAccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DeleteAccountModel model = new DeleteAccountModel();

                            model.AcctNumber = reader[0].ToString();
                            model.AcctHolder = reader[1].ToString();
                            model.AcctHolderId = Convert.ToInt32(reader[2]);
                            model.Country = reader[3].ToString();
                            delAccounts.Add(model);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Deleted Accounts Error: " + e.Message);
                        return null;
                    }

                    return delAccounts;
                }
            }
        }

        public List<string> GetCorrAcctNum ()
        {
        using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT DISTINCT A.AcctNumber, B.CountryCode 	
                                            /*
                                            , CASE WHEN t2.Name LIKE '%,%'
		                                        THEN 
		                                            LTRIM(RTRIM(RIGHT(t2.Name, CHARINDEX(',', REVERSE(t2.Name))-1))) --FIRST NAME
		                                            + ' ' +  LTRIM(RTRIM(LEFT(t2.Name, CHARINDEX(',', t2.Name)-1))) -- LAST NAME 
                                                ELSE t2.Name		
		                                    END AS [Name]
                                            */
                                            , t2.Name
                                            , t2.AcctHolderID 
                                        FROM Account A
                                        LEFT JOIN(SELECT A.AcctID, A.AcctNumber, E.Name, EntityId AS AcctHolderId
                                                    FROM Account A, Entity E
                                                    WHERE A.AcctNumber = E.AcctNumber
                                                  UNION
                                                  SELECT A.AcctID, A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
                                                    FROM Account A, Person P, PersonAcctHolder PH
                                                    WHERE A.AcctNumber = PH.AcctNumber AND P.PId = PH.PId)t2 
                                        ON t2.AcctID=A.AcctID
                                        LEFT JOIN dbo.ResCountryCode B 
                                            ON B.P_Ent_Id = t2.AcctHolderId
                                        WHERE A.AcctType LIKE 'Corrected'
                                        ORDER BY A.AcctNumber
                                        ";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<string> corrAccounts = new List<string>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            corrAccounts.Add(reader[0].ToString() + ':' + reader[1].ToString()  + ':' + reader[2].ToString() + ':' + reader[3].ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Corrected Account Numbers Error: " + e.Message);
                        return null;
                    }

                    return corrAccounts;
                }

            }
        }

        public List<string> GetFileSerialNumber()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT DISTINCT [FileSerialNumber] FROM [dbo].[MessageSpec]";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<string> fileSerialNumbers = new List<string>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            fileSerialNumbers.Add(reader[0].ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get File Serial Numbers Error: " + e.Message);
                        return null;
                    }

                    return fileSerialNumbers;
                }

            }
        }

        public string GetAccountNumber(int AcctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT DISTINCT [AcctNumber] FROM [dbo].[Account] WHERE [AcctID] = @AcctID";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@AcctID", SqlDbType.Int));
                    cmd.Prepare();
                    cmd.Parameters["@AcctID"].Value = AcctID;

                    try
                    {
                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Account ID Error: " + e.Message);
                    }

                    return null;
                }

            }
        }

        /*
        public int GetAccountID(string AcctNumber, string AcctType)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT DISTINCT [AcctID] FROM [dbo].[Account] WHERE [AcctNumber] = @AcctNumber AND [AcctType] = @AcctType";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@AcctNumber", SqlDbType.NVarChar, 72));
                    cmd.Parameters.Add(new SqlParameter("@AcctType", SqlDbType.NVarChar, 10));
                    cmd.Prepare();
                    cmd.Parameters["@AcctNumber"].Value = AcctNumber;
                    cmd.Parameters["@AcctType"].Value = AcctType;

                    List<int> AccountIDs = new List<int>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AccountIDs.Add(Convert.ToInt32(reader[0].ToString()));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Get Account ID Error: " + e.Message);
                    }

                    return AccountIDs.FirstOrDefault();
                }

            }
        }
        */
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

        public string GetFIName(string aeoiId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT FIName From AeoiProfile WHERE AeoiId=@aeoiId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@aeoiId", SqlDbType.NVarChar, 12));
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
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.NVarChar, 72));
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

        public List<PaymentModel> GetPayments(int acctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT PaymentType, Amount, P.CurrCode
                                        FROM Payment P, Account A
                                        WHERE P.AcctID = A.AcctID AND A.AcctID=@acctID";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctID", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@acctID"].Value = acctID;

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
	                                        STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
			                                        FROM INType, Entity
			                                        WHERE P_Ent_Id=EntityId AND EntityId=@acctHolderId
			                                        FOR XML PATH('')),1,1,'') AS INVal
                                        FROM Entity E, ResCountryCode
                                        WHERE EntityId=P_Ent_Id AND EntityId=@acctHolderId AND CountryCode=@countryCode";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@acctHolderId", SqlDbType.NVarChar, 72));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.NVarChar, 2));
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
	                                        STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
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

        public List<ControllingPersonModel> GetEntityCtrlPerson(int entityId, string countryCode, int acctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.PId FROM Person P, ControllingPerson C, Account A, ResCountryCode R, Entity E
                                        WHERE C.PId=P.PId AND A.AcctID=C.AcctID AND P.PId=R.P_Ent_Id AND E.AcctID=A.AcctID
		                                AND R.CountryCode=@countryCode AND A.AcctID=@acctID AND EntityId=@entityId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@entityId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.NVarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctID", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@entityId"].Value = entityId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@acctID"].Value = acctID;

                    List<ControllingPersonModel> ctrlList = new List<ControllingPersonModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            ControllingPersonModel model = new ControllingPersonModel();
                            model = GetCtrlPersonDetails(Convert.ToInt32(reader[0]), acctID);
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

        public List<ControllingPersonModel> GetIndividualCtrlPerson(int pId, string countryCode, int acctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.PId FROM Person P, ControllingPerson C, Account A, ResCountryCode R, PersonAcctHolder PH
                                        WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND A.AcctID=PH.AcctID
		                                AND R.CountryCode=@countryCode AND A.AcctID=@acctID AND P.PId=@pId";
                                                            cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.NVarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@countryCode"].Value = countryCode;
                    cmd.Parameters["@acctID"].Value = acctID;

                    List<ControllingPersonModel> ctrlList = new List<ControllingPersonModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ControllingPersonModel model = new ControllingPersonModel();
                            model = GetCtrlPersonDetails(Convert.ToInt32(reader[0]), acctID);
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

        public ControllingPersonModel GetCtrlPersonDetails(int pId, int acctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT P.*,STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
			                                        FROM INType, Person WHERE P_Ent_Id=PId AND PId=@pId
			                                        FOR XML PATH('')),1,1,'') AS INVal,
			                                        CtrlPersonType
                                        FROM Person P, ControllingPerson C, Account A
                                        WHERE C.PId=P.PId AND C.AcctID=A.AcctID AND A.AcctID=@acctID AND P.PId=@pId";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@acctID", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;
                    cmd.Parameters["@acctID"].Value = acctID;
                    

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
                    cmd.Parameters.Add(new SqlParameter("@countryCode", SqlDbType.NVarChar, 2));
                    cmd.Parameters.Add(new SqlParameter("@acctNumber", SqlDbType.NVarChar, 72));
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

        public bool isMsgIdExists(string msgId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT CAST(CASE
		                                WHEN (SELECT MessageRefId FROM MessageSpec WHERE MessageRefid=@msgId)IS NOT NULL
		                                THEN 1
		                                ELSE 0
		                                END AS BIT) AS isMsgIdExists";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@msgId", SqlDbType.NVarChar, 40));
                    cmd.Prepare();

                    cmd.Parameters["@msgId"].Value = msgId;

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

        public string[] GetPersonReportableResCountry(int pId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = @"SELECT CountryCode
                                        FROM Person P, ResCountryCode R
                                        WHERE P.PId=R.P_Ent_Id AND P.PId=@pId AND isReportable=1";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@pId"].Value = pId;

                    try
                    {
                        List<string> ctr = new List<string>();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ctr.Add(reader[0].ToString());
                        }
                        return ctr.ToArray(); ;
                    }
                    catch (Exception e)
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
                                        WHERE EntityId=P_Ent_Id AND EntityId=@entityId AND isReportable=1";
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
                return "Corrected";
            }

            return null;
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
       
    }
}