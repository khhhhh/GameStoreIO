using GameStore.Classes;


Store store = new Store();

//Accounts
store.AddAccount(new Account("email", "name", "pass", store, "firstName1", "lastName1"));
store.AddAccount(new Account("email", "coolGamer", "pass", store, "firstName1", "lastName1"));
store.AddAccount(new Account("email", "cheater228", "pass", store, "firstName1", "lastName1"));
store.AddAccount(new Account("email", "lolNoob1337", "pass", store, "firstName1", "lastName1"));

//Games
store.AddGame(new Game("RDR2", "Action, Shooter", 50));
store.AddGame(new Game("GTA5", "Action, Shooter", 50));
store.AddGame(new Game("Valorant", "Action, Shooter", 0));
store.AddGame(new Game("Dota 2", "Action, Shooter", 0));
store.AddGame(new Game("League of legends", "Action, Shooter", 0));



while (true)
{
    Console.WriteLine("Welcome to GameStore!");
    Console.WriteLine("Please, choose your option:");
    Console.WriteLine("1. Log in");
    Console.WriteLine("2. Register");

    var option = Console.ReadKey();
    Console.Clear();

    if (option.Key == ConsoleKey.D1)
    {
        Console.WriteLine("Enter username:");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter password:");
        string? password = Console.ReadLine();

        Console.Clear();
        if(password == "admin" && username == "admin")
        {
            Admin admin = new Admin(store);
            admin.ShowCLI();
        }
        else
        {
            Account? account = store.GetAccount(username, password);
            if(account == null)
                Console.WriteLine("Wrong username/password!");
            else
                account.ShowCLI();
        }
    } else if(option.Key == ConsoleKey.D2)
    {
        string email, username, password, firstName, lastName;
        Console.Write("Email: ");
        email = Console.ReadLine();
        Console.Write("Username: ");
        username = Console.ReadLine();
        Console.Write("Password: ");
        password = Console.ReadLine();
        Console.Write("First Name: ");
        firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        lastName = Console.ReadLine();

        Console.Clear();

        if(string.IsNullOrEmpty(email) ||
           string.IsNullOrEmpty(username) ||
           string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Wrong data!");
            continue;
        }

        Account account = new Account(email, username, password, store, firstName, lastName);
        store.AddAccount(account);
    }
}

