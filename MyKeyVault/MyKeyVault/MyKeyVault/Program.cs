namespace MyKeyVault;

// TESTING !!!!!!

public static class Program
{
    // Zmieniono nazwę na Main i typ na async Task
    public static async Task Main(string[] args)
    {
        System.Console.WriteLine("MyKeyVault is running.");
        var db = new Database();
        
        // Zamiast .Result używamy 'await' - to bezpieczniejsze i nowocześniejsze
        var personData = await db.GetPersonData("JohnDoe");
        var data = await db.GetAllPeopleData();
        Console.WriteLine(personData);
        
        
        db.ResetCredentials("aaa", "bbb", "ccc");
        var personData2 = await db.GetPersonData("JohnDoe");
        var data2 = await db.GetAllPeopleData();
        Console.WriteLine(personData2);
    }
}