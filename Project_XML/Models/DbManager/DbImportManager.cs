using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Project_XML.Models.DbManager
{
    public class DbImportManager: DbConnManager
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
                    catch(Exception e)
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
                                        "[Name][nvarchar](255) NOT NULL,"+
                                        "[AddressFree][nvarchar](255) NULL,"+
                                        "[City][nvarchar](255) NOT NULL,"+
                                        "[Country][nvarchar](255) NOT NULL,"+
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
                                        "[CP3 TIN 3 issuedBy][nvarchar](255) NULL"+
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
    }
}