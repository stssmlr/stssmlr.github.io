using System.Numerics;
using System.Text.RegularExpressions;

namespace user_manager
{
    internal class Program
    {
        class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }

            public override string ToString()
            {
                return $"Name: {FirstName} {LastName}, \nEmail: {Email}, \nPhone: {Phone}\n";
            }
        }

        class UserManager
        {
            private List<User> users = new List<User>();
            private const string FilePath = "users.txt";

            public void Run()
            {
                while (true)
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                AddNewUser();
                                Console.Clear();
                                break;
                            case "2":
                                ShowAllUsers();
                                Console.Clear();
                                break;
                            case "3":
                                ShowUserByNameOrEmail();
                                Console.Clear();
                                break;
                            case "4":
                                DeleteUser();
                                Console.Clear();
                                break;
                            case "5":
                                UpdateUser();
                                Console.Clear();
                                break;
                            case "6":
                                SaveToFile();
                                Console.Clear();
                                break;
                            case "7":
                                ReadFromFile();
                                Console.Clear();
                                break;
                            case "8":
                                Environment.Exit(0);
                                Console.Clear();
                                break;
                            default:
                                Console.WriteLine("Wrong choice!!!");
                                Console.Clear();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occured {ex.Message}");
                    }
                }
            }

            private void DisplayMenu()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n_  _ ____ ____ ____    _  _ ____ _  _ ____ ____ ____ _  _ ____ _  _ ___    ____ _   _ ____ ___ ____ _  _ \r\n|  | [__  |___ |__/    |\\/| |__| |\\ | |__| | __ |___ |\\/| |___ |\\ |  |     [__   \\_/  [__   |  |___ |\\/| \r\n|__| ___] |___ |  \\    |  | |  | | \\| |  | |__] |___ |  | |___ | \\|  |     ___]   |   ___]  |  |___ |  | \r\n                                                                                                         ");
                Console.ResetColor();
                Console.WriteLine("1. Add new user");
                Console.WriteLine("2. Show all users");
                Console.WriteLine("3. Show user by name/email");
                Console.WriteLine("4. Delete user");
                Console.WriteLine("5. Update user");
                Console.WriteLine("6. Save to file");
                Console.WriteLine("7. Read from file");
                Console.WriteLine("8. Exit");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter your choice: ");
                Console.ResetColor();
            }
            private void AddNewUser()
            {
                Console.Write("\nEnter user first name: ");
                string firstName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    throw new ArgumentException("\nFirst name cannot be empty or whitespace.");
                }

                Console.Write("\nEnter user last name: ");
                string lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(lastName))
                {
                    throw new ArgumentException("\nLast name cannot be empty or whitespace.");
                }

                Console.Write("\nEnter user email: ");
                string email = Console.ReadLine();

                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("\nInvalid email format.");
                }

                Console.Write("\nEnter user phone: ");
                string phone = Console.ReadLine();

                User newUser = new User { FirstName = firstName, LastName = lastName, Email = email, Phone = phone };
                users.Add(newUser);

                Console.WriteLine("\nUser added successfully.");

                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }
            private void ShowAllUsers()
            {
                if (users.Any())
                {
                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }
                }
                else
                {
                    Console.WriteLine("\nNO USERS HERE!");
                }
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private void ShowUserByNameOrEmail()
            {
                Console.Write("\nEnter user name or email: ");
                string search = Console.ReadLine();

                var foundUsers = users.Where(user =>
                Regex.IsMatch(user.FirstName, search, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(user.LastName, search, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(user.Email, search, RegexOptions.IgnoreCase));

                if (foundUsers.Any())
                {
                    foreach (var user in foundUsers)
                    {
                        Console.WriteLine(user);
                    }
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private void DeleteUser()
            {
                Console.Write("\nEnter user name, email, or phone to delete: ");
                string searchTerm = Console.ReadLine();

                Regex regex = new Regex(searchTerm, RegexOptions.IgnoreCase);

                var userToDelete = users.FirstOrDefault(user =>
                    regex.IsMatch(user.FirstName) ||
                    regex.IsMatch(user.LastName) ||
                    regex.IsMatch(user.Email) ||
                    regex.IsMatch(user.Phone));

                if (userToDelete != null)
                {
                    users.Remove(userToDelete);
                    Console.WriteLine("\nUser deleted successfully.");
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private void UpdateUser()
            {
                Console.Write("\nEnter user name, email, or phone to update: ");
                string searchTerm = Console.ReadLine();

                var userToUpdate = users.FirstOrDefault(user =>
                    user.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    user.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    user.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

                if (userToUpdate != null)
                {
                    Console.Write("\nEnter new user first name: ");
                    string newFirstName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newFirstName))
                    {
                        throw new ArgumentException("\nFirst name cannot be empty or whitespace.");
                    }

                    Console.Write("\nEnter new user last name: ");
                    string newLastName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newLastName))
                    {
                        throw new ArgumentException("\nLast name cannot be empty or whitespace.");
                    }

                    Console.Write("\nEnter new user email: ");
                    string newEmail = Console.ReadLine();

                    if (!IsValidEmail(newEmail))
                    {
                        throw new ArgumentException("\nInvalid email format.");
                    }

                    Console.Write("\nEnter new user phone: ");
                    string newPhone = Console.ReadLine();

                    userToUpdate.FirstName = newFirstName;
                    userToUpdate.LastName = newLastName;
                    userToUpdate.Email = newEmail;
                    userToUpdate.Phone = newPhone;

                    Console.WriteLine("\nUser updated successfully.");
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private void SaveToFile()
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (var user in users)
                    {
                        writer.WriteLine($"{user.FirstName},{user.LastName},{user.Email},{user.Phone}");
                    }
                }

                Console.WriteLine("\nUsers saved to file successfully.");
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private void ReadFromFile()
            {
                
                users.Clear(); 

                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            
                            
                        }
                    }
                }

                Console.WriteLine("\nUsers read from file successfully.");
                
                
                Console.WriteLine("\nPRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
            }

            private bool IsValidEmail(string email)
            {
                
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                return Regex.IsMatch(email, pattern);
            }
        }



        static void Main(string[] args)
        {
            UserManager system = new UserManager();
            system.Run();
        }
    }
}