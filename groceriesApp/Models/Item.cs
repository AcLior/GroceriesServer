namespace groceriesApp.Models
{
    public class Item
    {
        public string Name { get; set; }    
        public string Category { get; set; }

        public override string ToString()
        {
            return $" {Name} {Category}";
        }
        public void AddProduct(string email,string productName,string listName)
        {
            ItemsDB dbs = new ItemsDB();
            dbs.AddProduct(email, productName, listName);
        }
        public void DeleteProduct(string email,string listName,string productName)
        {
            ItemsDB dbs = new ItemsDB();
            dbs.DeleteProduct(email,listName,productName);
        }
        public void AddCategory(string email, string categoryName)
        {
            ItemsDB dbs = new ItemsDB();
            dbs.AddCategory(email, categoryName);
        }
        //public void AddPhoto(string productName, string photo)
        //{
        //    ItemsDB dbs = new ItemsDB();
        //    dbs.AddCategory(productName, photo);
        //}
    }
}
