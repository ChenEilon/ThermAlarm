using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace ThermAlarm.BackEnd.API
{
    //https://www.w3schools.com/sql/sql_quickref.asp
    public class SQLConnection
    {
        private static SqlConnectionStringBuilder builder;
        private static SqlConnection connection;

        public SQLConnection()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "webapptry2server.database.windows.net";
            builder.UserID = "thermadmin";
            builder.Password = "IRadmin2018";
            builder.InitialCatalog = "webapptry2db";
            connection = new SqlConnection(builder.ConnectionString);
            newTables();
        }

        private void newTables()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE usersInfo (UserName TEXT , Email TEXT, Password TEXT, Phone TEXT");
            sb.Append("CREATE TABLE IMGS (UserName TEXT, Date TIMESTAP(), ImgAsText TEXT");//TODO might remove username for checks
            sb.Append("CREATE TABLE VOLUME(UserName TEXT, Date TIMESTAMP(), PIR TINYINT(2)");//pir's "size" will be up to 2 digits
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        continue;//TODO check if i can simplisize this, to make it just run without a reader...
                        //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            //https://www.w3schools.com/sql/sql_datatypes.asp

        }
        /* not finished
        public bool insertNewUser(IdentityUser user)
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            string name = user.UserName;
            string email = user.Email;
            string phone = user.PhoneNumber;
            string pass = user.PasswordHash;//TODO check if rlly hashed
            sb.Append("INSERT INTO usersInfo (UserName, Email, Password, Phone) VALUES ({1}, {2}, {3}, {4})", name, email, pass, phone);
            return false;
        }
        // https://stackoverflow.com/questions/7263893/how-to-use-string-variable-in-sql-statement
        */
        /*
         * try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "your_server.database.windows.net"; 
                builder.UserID = "your_user";            
                builder.Password = "your_password";     
                builder.InitialCatalog = "your_database";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();       
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName ");
                    sb.Append("FROM [SalesLT].[ProductCategory] pc ");
                    sb.Append("JOIN [SalesLT].[Product] p ");
                    sb.Append("ON pc.productcategoryid = p.productcategoryid;");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
            */
    }


}
