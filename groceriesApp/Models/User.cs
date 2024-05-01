namespace groceriesApp.Models
{
    public class User
    {
       
        public string Email { get; set; }
        public string Password { get; set; }

     
        public override string ToString()
        {
            return $"  {Email} {Password}";
        }

        public void AddUser(User user)
        {
            UsersDB dbs = new UsersDB();
            dbs.AddUser(user);
        }
        public void DeleteUser(string email)
        {
            UsersDB dbs = new UsersDB();
            dbs.DeleteUser(email);
        }
        public int GetID(string email)
        {
            UsersDB dbs = new UsersDB();
            return dbs.GetID(email);
        }
    }
}
