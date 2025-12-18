internal class Program
{
    static Dictionary<string, string> userCredentials = new Dictionary<string, string>();

    static List<User> users = new List<User>();
    static string currentUsername = "";
    static bool isAuthenticated = false;

    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            if (!isAuthenticated)
            {
                Console.WriteLine("1. Авторизоваться");
                Console.WriteLine("2. Зарегистрироваться");
                Console.WriteLine("3. Выйти из программы");
            }
            else
            {
                Console.WriteLine("1. Добавить пользователя");
                Console.WriteLine("2. Удалить пользователя");
                Console.WriteLine("3. Найти пользователя по имени");
                Console.WriteLine("4. Вывести всех пользователей");
                Console.WriteLine("5. Выйти из учетной записи");
                Console.WriteLine("6. Выйти из программы");
            }

            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            if (!isAuthenticated)
            {
                switch (choice)
                {
                    case "1":
                        isAuthenticated = Authorize();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            else
            {
                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        RemoveUser();
                        break;
                    case "3":
                        FindUser();
                        break;
                    case "4":
                        DisplayUsers();
                        break;
                    case "5":
                        isAuthenticated = false;
                        Console.WriteLine("Вы вышли из учетной записи.");
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }


    static bool Authorize()
    {
        Console.WriteLine("Введите имя пользователя:");
        currentUsername = Console.ReadLine();

        if (!userCredentials.ContainsKey(currentUsername))
        {
            Console.WriteLine("Пользователь не найден");
            return false;
        }

        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();

        if (userCredentials[currentUsername] == password)
        {
            Console.WriteLine("Успешная авторизация!");
            return true;
        }
        else
        {
            Console.WriteLine("Введён неверный пароль.");
            return false;
        }
    }

    static void Register()
    {
        try
        {
            Console.WriteLine("Введите имя пользователя для регистрации:");
            string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username) && string.IsNullOrWhiteSpace(username))
                throw new Exception("Имя пользователя не может быть пустым.");

            if (userCredentials.ContainsKey(username))
                throw new Exception("Пользователь с таким именем уже существует.");

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            if (password.Length < 8)
                throw new Exception("Пароль слишком короткий (минимум 8 символов)");

            Console.WriteLine("Введите возраст пользователя:");
            if (!int.TryParse(Console.ReadLine(), out int age))
                throw new Exception("Возвраст должен быть числом");

            if (age < 0)
                throw new Exception("Возраст не может быть отрицательным.");

            userCredentials.Add(username, password);
            users.Add(new User(username, age));
            Console.WriteLine("Пользователь успешно зарегистрирован.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    static void AddUser()
    {
        try
        {
            Console.WriteLine("Введите имя пользователя:");
            string username = Console.ReadLine();
            if (string.IsNullOrEmpty(username) && string.IsNullOrWhiteSpace(username))
                throw new Exception("Имя пользователя не может быть пустым.");

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            if (password.Length < 8)
                throw new Exception("Пароль слишком короткий (минимум 8 символов)");

            Console.WriteLine("Введите возраст пользователя:");
            if (!int.TryParse(Console.ReadLine(), out int age))
                throw new Exception("Возвраст должен быть числом");

            if (age < 0)
                throw new Exception("Возраст не может быть отрицательным.");

            userCredentials.Add(username, password);
            users.Add(new User(username, age));
            Console.WriteLine("Пользователь добавлен.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    static void RemoveUser()
    {
        Console.WriteLine("Введите имя пользователя для удаления:");
        string name = Console.ReadLine();

        User userToRemove = users.Find(u => u.Name == name);
               
        if (userToRemove != null)
        {
            users.Remove(userToRemove);
            userCredentials.Remove(name);
            Console.WriteLine("Пользователь удален.");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }


        if (currentUsername == userToRemove.Name)
        {
            Console.WriteLine("Текущий пользователь удалён, сессия завершена.");
            isAuthenticated = false;
        }
    }

    static void FindUser()
    {
        Console.WriteLine("Введите имя пользователя для поиска:");
        string name = Console.ReadLine();

        User userFound = users.Find(u => u.Name == name);

        if (userFound != null)
        {
            Console.WriteLine($"Найден пользователь: {userFound.Name}, возраст {userFound.Age}");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    static void DisplayUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Список пользователей пуст.");
            return;
        }

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"Имя: {users[i].Name}, Возраст: {users[i].Age}");
        }
    }
}

class User
{
    public string Name { get; set; }
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
