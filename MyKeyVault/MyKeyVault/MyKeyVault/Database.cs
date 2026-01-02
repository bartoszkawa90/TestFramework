using Microsoft.Data.Sqlite;

namespace MyKeyVault;

public class Database
{
    private readonly string _connection = "Data Source=/Users/bartoszkawa/Desktop/REPOS/GitHub/" +
                                          "TestFramework/MyKeyVault/MyKeyVault/MyKeyVault/MyDatabase.db";
    private string? _userName;
    private string? _userPassword;
    private string? _userRole;
    
    // Fixed variables
    private Dictionary<string, string> _roles = new()
    {
        { "admin", "Admin" },
        { "reader", "Reader" },
        { "guest", "Guest" }
    };

    /// <summary>
    /// CLass constructor
    /// </summary>
    public Database(string name="admin", string password="Admin123", string role="Admin")
    {
        _userName = name;
        _userPassword = password;
        _userRole = role;
        Console.WriteLine("Opening database connection");
    }

    /// <summary>
    /// Method to set new credentials in already created Database object
    /// </summary>
    public void ResetCredentials(string username, string password, string role)
    {
        Console.WriteLine($"Reset credentials on username={username} password={password} role={role}");
        _userName = username;
        _userPassword = password;
        _userRole = role;
    }
    
    /// <summary>
    /// Method for authorization if user should have access to Other users keys
    /// </summary>
    private async Task<bool> AuthorizeUser(string? username = null, string? password = null)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new Exception("No username or password or role provided");
        }

        await using var connection = new SqliteConnection(_connection);
        await connection.OpenAsync().WaitAsync(TimeSpan.FromSeconds(30));
        await using var command = connection.CreateCommand();
        
        command.CommandText = "SELECT COUNT(1) FROM DbUsers WHERE Name = @name AND Password = @password;";
        command.Parameters.AddWithValue("@name", username);
        command.Parameters.AddWithValue("@password", password);
        
        var scalar = await command.ExecuteScalarAsync();
        var count = Convert.ToInt64(scalar ?? 0L); // handles null/DBNull
        return count > 0;
    }
    
    /// <summary>
    /// Method to get person's data from People table
    /// </summary>
    public async Task<Person?> GetPersonData(string? username = null)
    {
        Console.WriteLine($"Get person data");
        
        if (string.IsNullOrEmpty(username))
        {
            throw new Exception("No username provided");
        }

        if (!await AuthorizeUser(_userName, _userPassword))
        {
            return null;
        }
        await using (var connection = new SqliteConnection(_connection))
        {
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            await using (var command = connection.CreateCommand())
            {
                // Prepare command
                if (_userRole != _roles["guest"])
                {
                    command.CommandText = "SELECT Name, Password FROM People";
                }
                else
                {
                    command.CommandText = "SELECT Name, Password FROM People WHERE Name = @name";
                    command.Parameters.AddWithValue("@name", username);
                }

                // Read data
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var name = reader.GetString(0);
                        if (name == username)
                        {
                            var password = reader.GetString(1);
                            return new Person(name, password);
                        }
                    }
                }
            }
        }

        return null;
    }
    
    /// <summary>
    /// Method to get all people data from People table (According to role)
    /// </summary>
    public async Task<List<Person>> GetAllPeopleData()
    {
        Console.WriteLine($"Get all available people data");
        
        var people = new List<Person>();
        if (!await AuthorizeUser(_userName, _userPassword) || _userRole == _roles["guest"])
        {
            return people;
        }
        await using (var connection = new SqliteConnection(_connection))
        {
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            await using (var command = connection.CreateCommand())
            {
                // Prepare command
                command.CommandText = "SELECT Name, Password FROM People";
                // Read data
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var name = reader.GetString(0);
                        var password = reader.GetString(1);
                        people.Add(new Person(name, password));
                    }

                    return people;
                }
            }
        }
    }

    /// <summary>
    /// Method to add person to People table
    /// </summary>
    public async Task AddPerson(string username, string password)
    {
        Console.WriteLine("Add user data");
        
        if (string.IsNullOrEmpty(username))
        {
            throw new Exception("No username or secret provided");
        }

        if (await AuthorizeUser(_userName, _userPassword) && _userRole == _roles["admin"])
        {
            
        }
        
    }

    /// <summary>
    /// Method to edit person in People table
    /// </summary>
    public async Task EditPerson(string username, string password)
    {
        Console.WriteLine("Editing user data");
        
    }

    /// <summary>
    /// Method to delete person from People table
    /// </summary>
    public async Task DeletePerson(string username)
    {
        
    }
}