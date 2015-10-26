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
        private static MyApplicationAdoUow _unitOfWork;

        static void Main(string[] args)
        {
            _unitOfWork = GetUow();

            ShowDatabase();

            var input = string.Empty;
            while (!input.Equals("5", StringComparison.InvariantCulture))
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
                        CommitChanges();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CommitChanges()
        {
            _unitOfWork.Commit();
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Show users currently in the database");
            Console.WriteLine("2. Add a new user to the database");
            Console.WriteLine("3. Delete a user from the database");
            Console.WriteLine("4. Commit Changes");
            Console.WriteLine("5. Quit");
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
        }

        static void ShowDatabase()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            Console.WriteLine($"******* Here are the {users.Count} users currently in the database *******");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }

        static void AddUser()
        {
            Console.WriteLine("Please enter the user's information...");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            
            var user = new User {FirstName = firstName, LastName = lastName};
            _unitOfWork.Users.Add(user);
        }

        static void DeleteUser()
        {
            Console.WriteLine("Please enter the user's information...");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            
            var user = new User {FirstName = firstName, LastName = lastName};
            _unitOfWork.Users.Remove(user);
        }

        static MyApplicationAdoUow GetUow()
        {
            var conn = new SQLiteConnection(ConnectionString);
            return new MyApplicationAdoUow(conn);
        }
    }
}
