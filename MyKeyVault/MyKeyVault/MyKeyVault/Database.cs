namespace MyKeyVault;
using Microsoft.Data.Sqlite;

public class Database
{
    private readonly string _connection = "Data Source=MyDatabase.db";
    // private readonly string admin_name = "admin";
    // private readonly string admin_secret = "Admin123";

    // Class constructor
    public Database()
    {
        Console.WriteLine("Opening database connection");
        _connection.Open();
    }
    
    /// <summary>
    /// Method to get user data from database
    /// </summary>
    public async Task GetUser(string? username = null, string? secret = null)
    {
        Console.WriteLine($"Get user data");
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(secret))
        {
            throw new Exception("No username or secret provided");
        }

        if (await AuthorizeUser(username, secret))
        {
            
        }
    }

    /// <summary>
    /// Method to add user to database
    /// </summary>
    public async Task AddUser(string username, string secret, string password)
    {
        Console.WriteLine("Add user data");
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(secret))
        {
            throw new Exception("No username or secret provided");
        }

        if (await AuthorizeUser(username, secret))
        {
            
        }
        
    }

    /// <summary>
    /// Method to edit user in database
    /// </summary>
    public async Task EditUser(string username, string secret, string password)
    {
        Console.WriteLine("Editing user data");
        
    }

    /// <summary>
    /// Method to delete user from database
    /// </summary>
    public async Task DeleteUser(string username, string secret)
    {
        
    }

    /// <summary>
    /// Method for authorization if user should have access to Other users keys
    /// </summary>
    public async Task<bool> AuthorizeUser(string? username = null, string? secret = null)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(secret))
        {
            throw new Exception("No username or secret provided");
        }

        await using (var connection = new SqliteConnection(_connection))
        {
            connection.OpenAsync().Wait();
            var command = connection.CreateCommand();
            command.CommandText = 
                "SELECT COUNT(1) FROM DbUsers WHERE Name = @name AND Secret = @secret";
            
            command.Parameters.AddWithValue("@name", username);
            command.Parameters.AddWithValue("@secret", secret);
            
            var count = (long?)command.ExecuteScalar();

            return count > 0;
        }
    }
}