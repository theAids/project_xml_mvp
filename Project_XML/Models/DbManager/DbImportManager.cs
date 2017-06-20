using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Project_XML.Models.DbManager
{
    public class DbImportManager : DbConnManager
    {
        public int isTableExists(string tableName)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tableName";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@tableName", SqlDbType.NVarChar, 120));
                    cmd.Prepare();

                    cmd.Parameters["@tableName"].Value = tableName;

                    try
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("isTableExists Error: " + e.Message);
                        return 0;
                    }
                }
            }

        }

        public void CreateIndividualTable()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"CREATE TABLE  Individual_tbl (" +
                                        "[FirstName] [nvarchar](255) NOT NULL," +
                                        "[LastName] [nvarchar](255) NOT NULL," +
                                        "[AddressFree] [nvarchar](750) NULL," +
                                        "[City] [nvarchar](255) NOT NULL," +
                                        "[Country] [nvarchar](255) NOT NULL," +
                                        "[Date of birth] [nvarchar](255) NULL," +
                                        "[Place of birth] [nvarchar](255) NULL," +
                                        "[Jurisdiction 1] [nvarchar](255) NULL," +
                                        "[Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[TIN 1] [nvarchar](255) NULL," +
                                        "[TIN 1 issuedBy ] [nvarchar](255) NULL," +
                                        "[TIN 2] [nvarchar](255) NULL," +
                                        "[TIN 2 issuedBy ] [nvarchar](255) NULL," +
                                        "[TIN 3 ] [nvarchar](255) NULL," +
                                        "[TIN 3 issuedBy ] [nvarchar](255) NULL," +
                                        "[Account Number] [nvarchar](255) NOT NULL," +
                                        "[Currency Code] [nvarchar](255) NOT NULL," +
                                        "[Account Balance] [decimal](16, 2) NOT NULL," +
                                        "[Gross amount of interest] [decimal](16, 2) NULL," +
                                        "[Gross amount of dividend] [decimal](16, 2) NULL," +
                                        "[Gross amount of other income] [decimal](16, 2) NULL," +
                                        "[Gross proceeds] [decimal](16, 2) NULL" +
                                        ")";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Creating Individual Table Error:" + e.Message);
                    }
                }
            }
        }

        public void CreateEntityTable()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"CREATE TABLE Entity_tbl (" +
                                        "[Name][nvarchar](255) NOT NULL," +
                                        "[AddressFree][nvarchar](255) NULL," +
                                        "[City][nvarchar](255) NOT NULL," +
                                        "[Country][nvarchar](255) NOT NULL," +
                                        "[Jurisdiction 1] [nvarchar](255) NULL," +
                                        "[CRS Status 1] [nvarchar](255) NULL,"+
                                        "[Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[CRS Status 2] [nvarchar](255) NULL,"+
                                        "[Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[CRS Status 3] [nvarchar](255) NULL,"+
                                        "[TIN 1][nvarchar](255) NULL," +
                                        "[TIN 1 issuedBy][nvarchar](255) NULL," +
                                        "[TIN 2][nvarchar](255) NULL," +
                                        "[TIN 2 issuedBy][nvarchar](255) NULL," +
                                        "[TIN 3][nvarchar](255) NULL," +
                                        "[TIN 3 issuedBy][nvarchar](255) NULL," +
                                        "[Account Number][nvarchar](255) NOT NULL," +
                                        "[Currency Code][nvarchar](255) NOT NULL," +
                                        "[Account Balance][decimal](16, 2) NOT NULL," +
                                        "[Gross amount of interest][decimal](16, 2) NULL," +
                                        "[Gross amount of dividend][decimal](16, 2) NULL," +
                                        "[Gross amount of other income][decimal](16, 2) NULL," +
                                        "[Gross proceeds][decimal](16, 2) NULL," +
                                        "[CP1 FirstName][nvarchar](255) NULL," +
                                        "[CP1 LastName][nvarchar](255) NULL," +
                                        "[CP1 AddressFree][nvarchar](750) NULL," +
                                        "[CP1 City][nvarchar](255) NULL," +
                                        "[CP1 Country][nvarchar](255) NULL," +
                                        "[CP1 Date of birth][nvarchar](255) NULL," +
                                        "[CP1 Place of birth][nvarchar](255) NULL," +
                                        "[CP1 Jurisdiction 1] [nvarchar](255) NULL," +
                                        "[CP1 Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[CP1 Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[CP1 TIN 1][nvarchar](255) NULL," +
                                        "[CP1 TIN 1 issuedBy][nvarchar](255) NULL," +
                                        "[CP1 TIN 2][nvarchar](255) NULL," +
                                        "[CP1 TIN 2 issuedBy][nvarchar](255) NULL," +
                                        "[CP1 TIN 3][nvarchar](255) NULL," +
                                        "[CP1 TIN 3 issuedBy][nvarchar](255) NULL," +
                                        "[CP2 FirstName][nvarchar](255) NULL," +
                                        "[CP2 LastName][nvarchar](255) NULL," +
                                        "[CP2 AddressFree][nvarchar](750) NULL," +
                                        "[CP2 City][nvarchar](255) NULL," +
                                        "[CP2 Country][nvarchar](255) NULL," +
                                        "[CP2 Date of birth][nvarchar](255) NULL," +
                                        "[CP2 Place of birth][nvarchar](255) NULL," +
                                        "[CP2 Jurisdiction 1] [nvarchar](255) NULL," +
                                        "[CP2 Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[CP2 Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[CP2 TIN 1][nvarchar](255) NULL," +
                                        "[CP2 TIN 1 issuedBy][nvarchar](255) NULL," +
                                        "[CP2 TIN 2][nvarchar](255) NULL," +
                                        "[CP2 TIN 2 issuedBy][nvarchar](255) NULL," +
                                        "[CP2 TIN 3][nvarchar](255) NULL," +
                                        "[CP2 TIN 3 issuedBy][nvarchar](255) NULL," +
                                        "[CP3 FirstName][nvarchar](255) NULL," +
                                        "[CP3 LastName][nvarchar](255) NULL," +
                                        "[CP3 AddressFree][nvarchar](750) NULL," +
                                        "[CP3 City][nvarchar](255) NULL," +
                                        "[CP3 Country][nvarchar](255) NULL," +
                                        "[CP3 Date of birth][nvarchar](255) NULL," +
                                        "[CP3 Place of birth][nvarchar](255) NULL," +
                                        "[CP3 Jurisdiction 1] [nvarchar](255) NULL," +
                                        "[CP3 Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[CP3 Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[CP3 TIN 1][nvarchar](255) NULL," +
                                        "[CP3 TIN 1 issuedBy][nvarchar](255) NULL," +
                                        "[CP3 TIN 2][nvarchar](255) NULL," +
                                        "[CP3 TIN 2 issuedBy][nvarchar](255) NULL," +
                                        "[CP3 TIN 3][nvarchar](255) NULL," +
                                        "[CP3 TIN 3 issuedBy][nvarchar](255) NULL" +
                                        ")";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Creating Entity Table Error:" + e.Message);
                    }
                }
            }
        }

        public void DeleteCorrAcctNum()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd= conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"DELETE FROM [AEOIDB].[dbo].[DocSpec]
                                    WHERE AcctID IN 
	                                    (SELECT A.AcctID FROM [AEOIDB].[dbo].[Account] A
	                                    WHERE A.AcctType LIKE 'Corrected') ; 

                                    DELETE FROM [AEOIDB].dbo.Payment
                                    WHERE AcctID IN 
	                                    (SELECT A.AcctID FROM [AEOIDB].[dbo].[Account] A
	                                    WHERE A.AcctType LIKE 'Corrected') ; 

                                    DELETE FROM [AEOIDB].dbo.ControllingPerson 
                                    WHERE AcctID IN 
	                                    (SELECT A.AcctID FROM [AEOIDB].[dbo].[Account] A
	                                    WHERE A.AcctType LIKE 'Corrected') ; 

                                    DELETE FROM [AEOIDB].dbo.PersonAcctHolder 
                                    WHERE AcctID IN 
	                                    (SELECT A.AcctID FROM [AEOIDB].[dbo].[Account] A
	                                    WHERE A.AcctType LIKE 'Corrected') ; 

                                    DELETE FROM [AEOIDB].dbo.Entity 
                                    WHERE AcctID IN 
	                                    (SELECT A.AcctID FROM [AEOIDB].[dbo].[Account] A
	                                    WHERE A.AcctType LIKE 'Corrected') ; 
            
                                    DELETE FROM [AEOIDB].[dbo].[Account] WHERE [AcctType] LIKE 'Corrected'";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Delete Corrected Account Numbers Error: " + e.Message);
                    }

                }

            }
        }

        public void InsertFSN(string FileSerialNumber, string MessageRefid)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"UPDATE [dbo].[MessageSpec]
                                      SET [FileSerialNumber] = @FileSerialNumber
                                      WHERE [MessageRefid] = @MessageRefid";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@FileSerialNumber", SqlDbType.NVarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@MessageRefid", SqlDbType.NVarChar, 40));
                    cmd.Prepare();
                    cmd.Parameters["@FileSerialNumber"].Value = FileSerialNumber; 
                    cmd.Parameters["@MessageRefid"].Value = MessageRefid;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Update Message Reference Error: " + e.Message);
                    }

                }

            }
        }

        public void DeleteSourceTables()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"DELETE FROM [AEOIDB].[dbo].[Entity_tbl]; DELETE FROM [AEOIDB].[dbo].[Individual_tbl]; ";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Delete Source Tables Error: " + e.Message);
                    }

                }

            }
        }

        public void ImportIndividualTable(string scriptPath)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = File.ReadAllText(scriptPath + "/import_indiv_table_v1.sql");                   

                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Import Individual Table Error: " + e.Message);
                    }
                }
            }
        }

        public void ImportEntityTable(string scriptPath)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = File.ReadAllText(scriptPath+"/import_ent_table_v1.sql");

                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Import Entity Table Error: " + e.Message);
                    }
                }
            }
        }

        public int NewMessageSpec(string msgRefId, string msgType, string returnYear, string note, string contact, string aeoiId)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"INSERT INTO MessageSpec(MessageRefid, MessageTypeIndic, ReturnYear, AttentionNote, Contact,AeoiId)
                                        VALUES(@msgRefId,@msgType,@returnYear,@note,@contact,@aeoiId)";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@msgRefId", SqlDbType.NVarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@msgType", SqlDbType.NVarChar, 12));
                    cmd.Parameters.Add(new SqlParameter("@returnYear", SqlDbType.NVarChar, 4));
                    cmd.Parameters.Add(new SqlParameter("@note", SqlDbType.NVarChar, 150));
                    cmd.Parameters.Add(new SqlParameter("@contact", SqlDbType.NVarChar, 120));
                    cmd.Parameters.Add(new SqlParameter("@aeoiId", SqlDbType.NVarChar, 12));

                    cmd.Parameters["@note"].IsNullable = true;
                    cmd.Parameters["@contact"].IsNullable = true;
                    cmd.Prepare();

                    cmd.Parameters["@msgRefId"].Value = msgRefId;
                    cmd.Parameters["@msgType"].Value = msgType;
                    cmd.Parameters["@returnYear"].Value = returnYear;
                    cmd.Parameters["@note"].Value = note == null ? (object)DBNull.Value : note;
                    cmd.Parameters["@contact"].Value = contact == null ? (object)DBNull.Value : contact;
                    cmd.Parameters["@aeoiId"].Value = aeoiId;

                    try
                    {
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Insert New MessageSpec Error: " + e.Message);
                        return 0;
                    }

                }
            }
        }

        public void NewDocSpec(string docRefId, string docType, string msgRefId, string corrFSN, string corrDocRef, string corrAcct, string acctNum, int acctID)
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"INSERT INTO DocSpec(DocRefId, DocTypeIndic, MessageRefId, CorrFileSerialNumber, CorrDocRefId, CorrAccountNumber, AcctNumber, AcctID)
                                        VALUES(@docRefId,@docType,@msgRefId,@corrFSN, @corrDocRef, @corrAcct, @acctNum, @acctID)";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@docRefId", SqlDbType.NVarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@docType", SqlDbType.NVarChar, 12));
                    cmd.Parameters.Add(new SqlParameter("@msgRefId", SqlDbType.NVarChar, 40));

                    cmd.Parameters.Add(new SqlParameter("@corrFSN", SqlDbType.NVarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@corrDocRef", SqlDbType.NVarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@corrAcct", SqlDbType.NVarChar, 72));

                    cmd.Parameters.Add(new SqlParameter("@acctNum", SqlDbType.NVarChar, 72));
                    cmd.Parameters.Add(new SqlParameter("@acctID", SqlDbType.Int));
                    cmd.Prepare();

                    cmd.Parameters["@docRefId"].Value = docRefId;
                    cmd.Parameters["@docType"].Value = docType;
                    cmd.Parameters["@msgRefId"].Value = msgRefId;

                    cmd.Parameters["@corrFSN"].Value = corrFSN == null ? (object)DBNull.Value : corrFSN;
                    cmd.Parameters["@corrDocRef"].Value = corrDocRef == null ? (object)DBNull.Value : corrDocRef;
                    cmd.Parameters["@corrAcct"].Value = corrAcct == null ? (object)DBNull.Value : corrAcct;

                    cmd.Parameters["@acctNum"].Value = acctNum;
                    cmd.Parameters["@acctID"].Value = acctID;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Insert New DocSpec Error: " + e.Message);
                    }

                }
            }
        }
    }
}