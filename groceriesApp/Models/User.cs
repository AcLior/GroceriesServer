namespace groceriesApp.Models
{
    public class User
    {
       
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AddUser(string email, string password)
        {
            UsersDB dbs = new UsersDB();
            return dbs.AddUser(email,password);
        }
        public void DeleteUser(string email)
        {
            UsersDB dbs = new UsersDB();
            dbs.DeleteUser(email);
        }
        public bool CheckUser(string email,string password)
        {
            UsersDB dbs = new UsersDB();
            return dbs.CheckUser(email,password);
        }
    }
}
