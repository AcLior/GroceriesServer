
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace groceriesApp.Models
{
    public class UsersDB
    {

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        public bool AddUser(string email, string password)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = connect("myProjDB"); // create the connection
                cmd = CreateCommandWithStoredProcedure("AddUser", con, new Dictionary<string, object>
        {
            { "@Email", email },
            { "@Password", password }
        }); // create the command

                // Execute the command and get the return value
                object result = cmd.ExecuteScalar();

                // Check if the result is not null and is convertible to int
                if (result != null && int.TryParse(result.ToString(), out int returnValue))
                {
                    return returnValue == 1;
                }
                else
                {
                    // Log unexpected result
                    Console.WriteLine("Unexpected return value from stored procedure.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine("Error occurred: " + ex.Message);
                throw; // Rethrow the exception to propagate it to the caller
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public void DeleteUser(string email)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);
            



            cmd = CreateCommandWithStoredProcedure("DeleteUser", con, paramDic);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command


            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public bool CheckUser(string email, string password)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            try
            {
                // Create the command with the CheckUserExistence stored procedure
                cmd = CreateCommandWithStoredProcedure("CheckUser", con, null);

                // Add parameters for email and password
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                // Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                // Check if any rows are returned (i.e., if the user exists)
                bool userExists = false;
                if (dataReader.Read())
                {
                    // Check if the result is 1 (user exists)
                    userExists = Convert.ToInt32(dataReader[0]) == 1;
                }

                // Close the data reader
                dataReader.Close();

                // Return true if the user exists, false otherwise
                return userExists;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }


            return cmd;
        }




    }
}
