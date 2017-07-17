using BlackSound.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Views
{
    public class GuestView
    {
        public void Show()
        {
            GuestViewEnum choice = RenderMenu();
            switch (choice)
            {
                case GuestViewEnum.Register:
                    {
                        RegisterView registerView = new RegisterView();
                        registerView.Show();
                        break;
                    }
                case GuestViewEnum.Login:
                    {
                        LoginView loginView = new LoginView();
                        loginView.Show();
                        FrontUserView frontUserView = new FrontUserView();
                        frontUserView.Show();
                        break;
                    }
                case GuestViewEnum.Exit:
                    {
                        return;
                    }
            }
        }

        public GuestViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[R]egister");
                Console.WriteLine("[L]ogin");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "R":
                        {
                            return GuestViewEnum.Register;
                        }
                    case "L":
                        {
                            return GuestViewEnum.Login;
                        }
                    case "X":
                        {
                            return GuestViewEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
    }
}
