using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace groceriesApp.Models
{
    public class GroceryListDB
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

        public void CreateList(string nameList,string email)
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
            paramDic.Add("@ListName", nameList);
            paramDic.Add("@Email", email);



            cmd = CreateCommandWithStoredProcedure("AddList", con, paramDic);             // create the command

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


        public void DeleteList(string email, string listName)
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
            paramDic.Add("@ListName", listName);



            cmd = CreateCommandWithStoredProcedure("DeleteList", con, paramDic);             // create the command

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


        public int GetListID(string email, string listName)
        {
            SqlConnection con;
            SqlCommand cmd;
            // Add output parameter to get the return value
            SqlParameter returnValue = new SqlParameter();
            returnValue.ParameterName = "@ListItemID";
            returnValue.Direction = System.Data.ParameterDirection.ReturnValue;

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
            paramDic.Add("@ListName", listName);

            cmd = CreateCommandWithStoredProcedure("GetListItemID", con, paramDic); // create the command

            // Add the returnValue parameter to the command
            cmd.Parameters.Add(returnValue);

            try
            {
                cmd.ExecuteNonQuery();
                // Print the return value (optional)
                Console.WriteLine("Return Value: " + returnValue.Value.ToString());

                // Get the return value
                return Convert.ToInt32(returnValue.Value);
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


        ////---------------------------------------------------------------------------------
        //// Create the SqlCommand using a stored procedure
        ////---------------------------------------------------------------------------------
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
