namespace MyKeyVault
{
    /// <summary>
    /// Class for storing user information
    /// </summary>
    public class User
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }

        public User(string userId, string name, string role)
        {
            UserId = userId;
            Name = name;
            Role = role;
        }
    }

    /// <summary>
    /// Class for storing person information
    /// </summary>
    public class Person
    {
        public string? Name { get; set; }
        public string? Password { get; set; }

        public Person(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}