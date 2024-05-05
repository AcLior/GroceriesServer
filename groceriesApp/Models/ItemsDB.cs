using System.Data;
using System.Data.SqlClient;

namespace groceriesApp.Models
{

    public class ItemsDB
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

        public Item[] AddProduct(Item item, string email)
        {
            List<Item> itemList = new List<Item>();

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
                // Create the dictionary for stored procedure parameters
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                paramDic.Add("@Email", email);
                paramDic.Add("@ProductName", item.ProductName);
                paramDic.Add("@CategoryName", item.Category);
                paramDic.Add("@Quantity", item.Quantity);
                paramDic.Add("@IsDone", item.IsDone);
                paramDic.Add("@Photo", item.Photo); // Add Photo parameter

                // Add output parameter for ProductID
                SqlParameter productIdParam = new SqlParameter("@ProductID", SqlDbType.Int);
                productIdParam.Direction = ParameterDirection.Output;

                // Execute the AddProduct stored procedure to add the new item
                cmd = CreateCommandWithStoredProcedure("AddProduct", con, paramDic);
                cmd.Parameters.Add(productIdParam);
                cmd.ExecuteNonQuery();

                // Get the newly generated product ID
                int productId = Convert.ToInt32(productIdParam.Value);

              
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

            // Return the array of items
            return GetListByEmail(email).ToArray();
        }


        public Item[] DeleteProduct( string productName, string email)
        {
            List<Item> itemList = new List<Item>();

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
                // Execute the DeleteProduct stored procedure to delete the product
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                paramDic.Add("@Email", email);
                paramDic.Add("@ProductName", productName);

                cmd = CreateCommandWithStoredProcedure("DeleteProduct", con, paramDic);
                cmd.ExecuteNonQuery();

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

            // Return the array of items
            return GetListByEmail(email).ToArray();
        }

        public Item[] UpdateProduct(string productName, string email)
        {
            List<Item> itemList = new List<Item>();

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
                // Execute the DeleteProduct stored procedure to delete the product
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                paramDic.Add("@Email", email);
                paramDic.Add("@ProductName", productName);

                cmd = CreateCommandWithStoredProcedure("UpdateIsDone", con, paramDic);
                cmd.ExecuteNonQuery();

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

            // Return the array of items
            return GetListByEmail(email).ToArray();
        }

        public List<Item> GetListByEmail(string email)
        {
            List<Item> itemList = new List<Item>();

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

                // Fetch the updated list of items
                cmd = new SqlCommand("GetListByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                // Read the result set and populate the item list
                while (reader.Read())
                {
                    Item newItem = new Item();
                    newItem.ProductName = reader["ProductName"].ToString();
                    newItem.Category = reader["Category"].ToString();
                    newItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    newItem.IsDone = Convert.ToBoolean(reader["IsDone"]);
                    newItem.Photo = reader["Photo"].ToString(); // Include Photo
                    itemList.Add(newItem);
                }

                // Close the reader
                reader.Close();
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

            // Return the list of items
            return itemList;
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
