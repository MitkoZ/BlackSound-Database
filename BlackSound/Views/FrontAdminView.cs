using BlackSound.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Views
{
    class FrontAdminView
    {
        public void Show()
        {
            while (true)
            {
                FrontUserViewEnum choice = RenderMenu();
                try
                {
                    switch (choice)
                    {
                        case FrontUserViewEnum.AdminView:
                            {
                                SongsView songsView = new SongsView();
                                songsView.Show();
                                break;
                            }
                        case FrontUserViewEnum.OrdinaryUserView:
                            {
                                PlaylistsView playlistsView = new PlaylistsView();
                                playlistsView.Show();
                                break;
                            }
                        case FrontUserViewEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {

                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        private FrontUserViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[A]dmin View");
                Console.WriteLine("[O]rdinary View");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "A":
                        {
                            return FrontUserViewEnum.AdminView;
                        }
                    case "O":
                        {
                            return FrontUserViewEnum.OrdinaryUserView;
                        }
                    case "X":
                        {
                            return FrontUserViewEnum.Exit;
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
