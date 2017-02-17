using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace Project_XML.Models.DbManager
{
    public class DbExportManager: DbConnManager
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
                        
                        while(reader.Read())
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
                using(SqlCommand cmd = conn.CreateCommand())
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

                        while(reader.Read())
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