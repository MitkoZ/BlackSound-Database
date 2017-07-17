using BlackSound.Tools;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Views
{
    public class RegisterView
    {
        public void Show()
        {
            Console.Clear();
            Console.Write("Please enter username: ");
            string username = Console.ReadLine();
            User userInput = new User();
            if (!string.IsNullOrWhiteSpace(username))
            {
                userInput.Username = username;
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey(true);
                return;
            }
            UsersRepository usersRepo = new UsersRepository();
            List<User> usersDb = usersRepo.GetAll();
            if (usersDb.Any(user => user.Username == userInput.Username))
            {
                Console.WriteLine("This username is already used!");
                Console.ReadKey(true);
                return;
            }
            Console.Write("Please enter email: ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
            {
                userInput.Email = email;
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey(true);
                return;
            }
            if (usersDb.Any(user => user.Email == userInput.Email))
            {
                Console.WriteLine("This email is already used!");
                Console.ReadKey(true);
                return;
            }
            Console.Write("Please enter password: ");
            string password = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(password))
            {
                userInput.Password = password;
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey(true);
                return;
            }
            usersRepo.Save(userInput);
            Console.WriteLine("Registration completed successfully!");
            Console.ReadKey(true);
        }
    }
}
