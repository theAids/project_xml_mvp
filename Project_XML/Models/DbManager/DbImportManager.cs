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
                                        "[Jurisdiction 2] [nvarchar](255) NULL," +
                                        "[Jurisdiction 3] [nvarchar](255) NULL," +
                                        "[TIN 1][nvarchar](255) NULL," +
                                        "[TIN 1 issuedBy][nvarchar](255) NULL," +
                                        "[TIN 2][nvarchar](255) NULL," +
                                        "[TIN 2 issuedBy][nvarchar](255) NULL," +
                                        "[TIN 3][nvarchar](255) NULL," +
                                        "[TIN 3 issuedBy][nvarchar](255) NULL," +
                                        "[Account Number][nvarchar](255) NOT NULL," +
                                        "[CRS Status][nvarchar](255) NOT NULL," +
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

        public void ImportIndividualTable()
        {
            using (SqlConnection conn = base.GetDbConnection("AeoiConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    
                    string cmdstr = @"
    UPDATE Individual_tbl SET
	--Convert country name to country code
	[Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 1]) OR CountryCode=UPPER([Jurisdiction 1])),NULL),
	[Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 2]) OR CountryCode=UPPER([Jurisdiction 2])),NULL),
	[Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 3]) OR CountryCode=UPPER([Jurisdiction 3])),NULL),
	[Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Country]) OR CountryCode=UPPER([Country])),NULL),
	[Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Place of birth]) OR CountryCode=UPPER([Place of birth])),NULL),
	[TIN 1 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 1 issuedBy ]) OR CountryCode=UPPER([TIN 1 issuedBy ])),NULL),
	[TIN 2 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 2 issuedBy ]) OR CountryCode=UPPER([TIN 2 issuedBy ])),NULL),
	[TIN 3 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 3 issuedBy ]) OR CountryCode=UPPER([TIN 3 issuedBy ])),NULL)

    DECLARE @FirstName nvarchar(255) 
    DECLARE @LastName nvarchar(255) 
    DECLARE @AddressFree nvarchar(750)
    DECLARE @City nvarchar(255) 
    DECLARE @Country nvarchar(255) 
    DECLARE @BirthDate nvarchar(255)
    DECLARE @BirthPlace nvarchar(255)
    DECLARE @Jurisdiction1 nvarchar(255)
    DECLARE @Jurisdiction2 nvarchar(255)
    DECLARE @Jurisdiction3 nvarchar(255)
    DECLARE @TIN1 nvarchar(255)
    DECLARE @TIN1Ctry nvarchar(255)
    DECLARE @TIN2 nvarchar(255)
    DECLARE @TIN2Ctry  nvarchar(255)
    DECLARE @TIN3  nvarchar(255)
    DECLARE @TIN3Ctry nvarchar(255)
    DECLARE @AccountNumber nvarchar(255) 
    DECLARE @CurrencyCode nvarchar(255) 
    DECLARE @AccountBalance decimal(16, 2) 
    DECLARE @Interest decimal(16, 2)
    DECLARE @Dividend decimal(16, 2)
    DECLARE @OtherIncome decimal(16, 2)
    DECLARE @Proceeds decimal(16, 2)
    DECLARE @AcctType nvarchar(10)
    DECLARE @AcctID int
	DECLARE @PId int

  	DECLARE indiv_cursor CURSOR FOR SELECT * FROM Individual_tbl

  	OPEN indiv_cursor
	FETCH NEXT FROM indiv_cursor
	INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
	@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
	@CurrencyCode,@AccountBalance,@Interest,@Dividend,@OtherIncome,@Proceeds, @AcctType

    WHILE @@FETCH_STATUS = 0
    BEGIN
	    IF(SELECT AcctNumber FROM Account WHERE [AcctNumber]=@AccountNumber AND [AcctType]=@AcctType) IS NULL
	    BEGIN
		    INSERT INTO Account(AcctNumber, CurrCode, AccountBalance, AcctType) 
		    VALUES(@AccountNumber,@CurrencyCode, @AccountBalance, @AcctType)
			/*   
	        SET @AcctID = (SELECT DISTINCT [AcctID] 
	        FROM Account 
	        WHERE AcctNumber=@AccountNumber AND AcctType=@AcctType AND CurrCode = @CurrencyCode AND AccountBalance = @AccountBalance)
			*/
			SET @AcctID = (SELECT SCOPE_IDENTITY()) --for testing

		    INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
		    VALUES(@FirstName, @LastName, @BirthDate, @BirthPlace)

		    SET @PId = (SELECT SCOPE_IDENTITY()) --get the last identity value inserted in Person table

			INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
		    VALUES(@AddressFree, @City,@Country, @PId)
		        
		    IF(@Jurisdiction1 IS NOT NULL)
		    BEGIN
			    IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction1) = 0 
				    INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
				    VALUES(@PId,@Jurisdiction1,1)
		    END 
		    IF(@Jurisdiction2 IS NOT NULL)
		    BEGIN
			    IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction2) = 0 
				    INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
				    VALUES(@PId,@Jurisdiction2,1)
		    END 
		    IF(@Jurisdiction3 IS NOT NULL)
	        BEGIN
		        IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction3) = 0 
			        INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
			        VALUES(@PId,@Jurisdiction3,1)
	        END 
		    IF(@TIN1 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN1,@TIN1Ctry,'TIN',@PId)
		    IF(@TIN2 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN2,@TIN1Ctry,'TIN',@PId)
		    IF(@TIN3 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN3,@TIN1Ctry,'TIN',@PId)

		    INSERT INTO PersonAcctHolder(AcctID, PId)
		    VALUES(@AcctID,@PId)
		
			FETCH NEXT FROM indiv_cursor
			INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
			@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
			@CurrencyCode,@AccountBalance,@Interest,@Dividend,@OtherIncome,@Proceeds, @AcctType
		END
   	END

    CLOSE indiv_cursor
    DEALLOCATE indiv_cursor";

                    //string cmdstr = File.ReadAllText(HttpServerUtility.MapPath("~/Models/DbScripts/import_indiv_table_v1.sql"));                   

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