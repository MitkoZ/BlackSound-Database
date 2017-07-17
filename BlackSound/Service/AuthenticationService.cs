using BlackSound.Tools;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Service
{
    class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepo = new UsersRepository();
            AuthenticationService.LoggedUser = userRepo.GetAll(user => user.Username == username && user.Password == password).FirstOrDefault();
        }
    }
}
