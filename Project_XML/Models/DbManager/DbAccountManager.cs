﻿using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project_XML.Models.DbManager
{
    public class DbAccountManager : DbConnManager
    {
        public void CreateAccountsTable()
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'users')
                                        CREATE TABLE[dbo].[users] (
                                            [userID] [bigint] NOT NULL IDENTITY(1,1) PRIMARY KEY,
                                            [username] [varchar](20) NOT NULL UNIQUE,
                                            [password] [varchar](40) NOT NULL,
                                            [salt] [varchar](40) NOT NULL,
                                            [firstname] [varchar](20) NOT NULL,
                                            [lastname] [varchar](20) NOT NULL,
                                            [role] [varchar] (20) NOT NULL,
                                            [version] rowversion);";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("CREATE INITIAL TABLE ERROR: " + e.ToString());
                    }
                }
            }
        }

        public string GetUsername(string username)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = "SELECT username from users WHERE username=@username";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Prepare();

                    cmd.Parameters["@username"].Value = username;

                    try
                    {
                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("GET USERNAME ERROR: " + e.ToString());
                        return null;
                    }
                }
            }
        }

        public Boolean AddUser(string username, string firstname, string lastname, string password, string role)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = "INSERT INTO users VALUES(@username, @password, @salt, @firstname, @lastname, @role, DEFAULT)";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@salt", SqlDbType.VarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@firstname", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@lastname", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@role", SqlDbType.VarChar, 20));

                    cmd.Prepare();
                    cmd.Parameters["@username"].Value = username;
                    cmd.Parameters["@firstname"].Value = firstname;
                    cmd.Parameters["@lastname"].Value = lastname;
                    cmd.Parameters["@role"].Value = role;

                    string salt = GetSalt();
                    string hashPassword = HashPassword(password, salt);
                    cmd.Parameters["@salt"].Value = salt;
                    cmd.Parameters["@password"].Value = hashPassword;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("ADD USER ERROR: " + e.ToString());
                        return false;
                    }

                }
            }
        }

        public Boolean EditUserInfo(String username, string firstname, string lastname, string role)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = "UPDATE users set firstname=@firstname, lastname=@lastname, role=@role WHERE username=@username";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@firstname", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@lastname", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@role", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Prepare();

                    cmd.Parameters["@firstname"].Value = firstname;
                    cmd.Parameters["@lastname"].Value = lastname;
                    cmd.Parameters["@role"].Value = role;
                    cmd.Parameters["@username"].Value = username;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DB QUERY ERROR: " + e.ToString());
                        return false;
                    }
                }
            }
        }

        public bool ChangeUserPassword(string username, string password)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = "UPDATE users set password=@password, salt=@salt WHERE username=@username";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@salt", SqlDbType.VarChar, 40));
                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Prepare();

                    cmd.Parameters["@username"].Value = username;

                    string salt = GetSalt();
                    string hashPassword = HashPassword(password, salt);
                    cmd.Parameters["@salt"].Value = salt;
                    cmd.Parameters["@password"].Value = hashPassword;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DB QUERY ERROR: " + e.ToString());
                        return false;
                    }

                }
            }
        }
        public Boolean RemoveUser(long userID)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = "DELETE FROM users WHERE userID=@userID";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.BigInt));
                    cmd.Prepare();

                    cmd.Parameters["@userID"].Value = userID;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("SQL ERROR: " + e.ToString());
                        return false;
                    }
                }
            }
        }

        public UserAccountModel ValidateUser(string username, string password)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {

                string salt = GetUserSalt(username);

                // get username, lastname firstname and role for the cookies
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = "SELECT userID, username, lastname, firstname, role FROM users WHERE username=@username and password=@password";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 50));
                    cmd.Prepare();

                    cmd.Parameters["@username"].Value = username;
                    cmd.Parameters["@password"].Value = GetHash(password, salt);    // generate the hashed password w/ salt

                    UserAccountModel user = new UserAccountModel();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.userID = Convert.ToInt64(reader[0]);
                                user.username = reader[1].ToString();
                                user.lastname = reader[2].ToString();
                                user.firstname = reader[3].ToString();
                                user.role = reader[4].ToString();

                                return user;
                            }
                        }
                        else
                            return null;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("VALIDATE USER ERROR: " + e.ToString());
                    }

                }
            }

            return null;
        }

        public string GetUserSalt(string username)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = "SELECT salt FROM users WHERE username=@username";
                    cmd.CommandText = cmdstr;
                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Prepare();

                    cmd.Parameters["@username"].Value = username;

                    try
                    {
                        string salt = cmd.ExecuteScalar().ToString();
                        return salt;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DB QUERY ERROR: " + e.Message);
                        return null;
                    }
                }
            }
        }

        public string GetPassword(string username)
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    string cmdstr = "SELECT password FROM users WHERE username=@username";
                    cmd.CommandText = cmdstr;

                    cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 20));
                    cmd.Prepare();

                    cmd.Parameters["@username"].Value = username;

                    try
                    {
                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("GET PASSWORD ERROR: " + e.Message);
                        return null;
                    }
                }
            }
        }

        public List<UserAccountModel> GetAllUsers()
        {
            using (SqlConnection conn = base.GetDbConnection("AccountConnection"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string cmdstr = "SELECT userID, username, lastname, firstname, role FROM users ORDER BY userID ASC";
                    cmd.CommandText = cmdstr;
                    cmd.Prepare();

                    List<UserAccountModel> accounts = new List<UserAccountModel>();

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserAccountModel user = new UserAccountModel();

                            user.userID = Convert.ToInt64(reader[0]);
                            user.username = reader[1].ToString();
                            user.lastname = reader[2].ToString();
                            user.firstname = reader[3].ToString();
                            user.role = reader[4].ToString();

                            accounts.Add(user);

                        }
                        return accounts;

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DB QUERY ERROR: " + e.ToString());
                        return null;
                    }

                }
            }
        }

        /*************** Password Functions *****************************/
        public string HashPassword(string pword, string salt)
        {
            pword += salt;

            MD5 md5hasher = MD5.Create();

            byte[] hashPword = md5hasher.ComputeHash(Encoding.ASCII.GetBytes(pword));

            StringBuilder strbuilder = new StringBuilder();

            // convert to bytes to hex
            for (int i = 0; i < hashPword.Length; i++)
            {
                strbuilder.Append(hashPword[i].ToString("x2"));
            }

            return strbuilder.ToString();
        }

        public string GetSalt()
        {
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] rand = new byte[32]; // create 32 character for salt. length is the same as the output of md5hash (recommended length)
                rngCsp.GetNonZeroBytes(rand);   // generate random values
                string saltStr = Encoding.ASCII.GetString(rand, 0, rand.Length);
                return saltStr;
            }
        }

        protected static string GetHash(string password, string salt)
        {
            password += salt;   // concatenate password and salt before hashing

            MD5 md5hasher = MD5.Create();

            byte[] hashPword = md5hasher.ComputeHash(Encoding.ASCII.GetBytes(password));    // compute for hash

            // convert the computed hash to hexadecimal
            StringBuilder strbuilder = new StringBuilder();

            for (int i = 0; i < hashPword.Length; i++)
            {
                strbuilder.Append(hashPword[i].ToString("x2"));
            }

            return strbuilder.ToString();
        }

    }
}