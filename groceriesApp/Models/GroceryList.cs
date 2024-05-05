using System.Collections.Generic;

namespace groceriesApp.Models
{
    public class GroceryList
    {
        public string ListName { get; set; }

        public void CreateList(string nameList,string email)
        {
            GroceryListDB dbs = new GroceryListDB();
            dbs.CreateList(nameList,email);
        }
        public void DeleteList(string email,string listName)
        {
            GroceryListDB dbs = new GroceryListDB();
            dbs.DeleteList(email,listName);
        }
        public static int GetListID(string email,string listName)
        {
            GroceryListDB gl=new GroceryListDB();

            return gl.GetListID(email,listName);
        }
        //public static int GetListByEmail(string email)
        //{
        //    GroceryListDB gl = new GroceryListDB();

        //    return gl.GetListByEmail(email);
        //}

    }
}
