using System;
using System.Data.SQLite;
using System.Linq;
using UnitOfWorkAdoDemo2.DataAccess;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2
{
    class Program
    {
        private const string ConnectionString = "Data Source = uowdemo.s3db";

        static void Main(string[] args)
        {
            ShowDatabase();

            var input = string.Empty;
            while (!input.Equals("6", StringComparison.InvariantCulture))
            {
                ShowMenu();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowDatabase();
                        break;
                    case "2":
                        AddUser();
                        break;
                    case "3":
                        DeleteUser();
                        break;
                    case "4":
                        AddDelAddAddWithCommit();
                        break;
                    case "5":
                        AddDelAddAddNoCommit();
                        break;
                    default:
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Show users currently in the database");
            Console.WriteLine("2. Add a new user to the database");
            Console.WriteLine("3. Delete a user from the database");
            Console.WriteLine("4. Add > Delete > Add > Add and Commit");
            Console.WriteLine("5. Add > Delete > Add > Add and NOT commit");
            Console.WriteLine("6. Quit");
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
        }

        static void ShowDatabase()
        {
            using (var uow = GetUow())
            {
                var users = uow.Users.GetAll().ToList();
                Console.WriteLine($"******* Here are the {users.Count} users currently in the database *******");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.FirstName} {user.LastName}");
                }
            }
        }

        static void AddUser()
        {
            Console.WriteLine("Please enter the user's information...");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            
            using (var uow = GetUow())
            {
                var user = new User {FirstName = firstName, LastName = lastName};
                uow.Users.Add(user);
                uow.Complete();
            }
        }

        static void DeleteUser()
        {
            Console.WriteLine("Please enter the user's information...");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            using (var uow = GetUow())
            {
                var user = new User {FirstName = firstName, LastName = lastName};
                uow.Users.Remove(user);
                uow.Complete();
            }
        }

        static void AddDelAddAddNoCommit()
        {
            var user1 = new User {FirstName = "Dummy User 1", LastName = "Scenario 2"};
            var user2 = new User { FirstName = "Dummy User 2", LastName = "Scenario 2" };

            using (var uow = GetUow())
            {
                uow.Users.Add(user1);
                uow.Users.Remove(user1);
                uow.Users.Add(user1);
                uow.Users.Add(user2);
            }
        }

        static void AddDelAddAddWithCommit()
        {
            var user1 = new User { FirstName = "Dummy User 1", LastName = "Scenario 1" };
            var user2 = new User { FirstName = "Dummy User 2", LastName = "Scenario 1" };

            using (var uow = GetUow())
            {
                uow.Users.Add(user1);
                uow.Users.Remove(user1);
                uow.Users.Add(user1);
                uow.Users.Add(user2);
                uow.Complete();
            }
        }

        static MyApplicationAdoUow GetUow()
        {
            var conn = new SQLiteConnection(ConnectionString);
            return new MyApplicationAdoUow(conn);
        }
    }
}
