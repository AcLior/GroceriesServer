namespace groceriesApp.Models
{
    public class Item
    {
        public string ProductName { get; set; }    
        public string Category { get; set; }

        public string Photo { get; set; }

        public int Quantity { get; set; }

        public bool IsDone { get; set; }    
                 
                

        public static Item[] AddProduct(Item item, string email)
        {
            ItemsDB dbs = new ItemsDB();
            return dbs.AddProduct(item,email);
        }
        public static Item[]  DeleteProduct(string productName,string email)
        {
            ItemsDB dbs = new ItemsDB();
            return dbs.DeleteProduct(productName,email);
        }

        public static Item[] UpdateProduct(string productName, string email)
        {
            ItemsDB dbs = new ItemsDB();
            return dbs.UpdateProduct(productName, email);
        }

        public static List<Item> GetListByEmail(string email)
        {
            ItemsDB dbs = new ItemsDB();
            return dbs.GetListByEmail(email);
        }
        
    }
}
