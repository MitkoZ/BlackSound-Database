using BlackSound.Service;
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
    class SongsView : BaseView<Song>
    // the admin can CRUD songs
    {
        protected override List<Song> GetListView(BaseRepository<Song> baseRepo)
        {
            return baseRepo.GetAll();
        }

        protected override void DeleteMoreLogic(int id)
        {
            PlaylistsSongsRepository playlistsSongsRepo = new PlaylistsSongsRepository();
            playlistsSongsRepo.Delete(item => item.SongId == id);
        }

        protected override List<Song> GetListUpdate(BaseRepository<Song> baseRepo)
        {
            return baseRepo.GetAll();
        }

        protected override void UpdateItem(Song itemDb)
        {
            Console.WriteLine("ID:" + itemDb.Id);
            Console.WriteLine("Title: " + itemDb.Title);
            Console.Write("New Title: ");
            string newTitle = Console.ReadLine();
            Console.WriteLine("Artist name: " + itemDb.ArtistName);
            Console.Write("New artist name: ");
            string newArtistName = Console.ReadLine();
            Console.WriteLine("Year: " + itemDb.Year);
            Console.Write("New year: ");
            string newYear = Console.ReadLine();

            if (!string.IsNullOrEmpty(newTitle))
            {
                itemDb.Title = newTitle;
            }
            if (!string.IsNullOrEmpty(newArtistName))
            {
                itemDb.ArtistName = newArtistName;
            }
            if (!string.IsNullOrEmpty(newYear))
            {
                itemDb.Year = Int32.Parse(newYear);
            }
        }

        protected override BaseRepository<Song> CreateRepository()
        {
            return new SongsRepository();
        }

        protected override void RenderItem(Song item)
        {
            Console.WriteLine("ID: " + item.Id);
            Console.WriteLine("Title: " + item.Title);
            Console.WriteLine("Artist Name: " + item.ArtistName);
            Console.WriteLine("Year: " + item.Year);
            Console.WriteLine("==============================================================================");
        }

        protected override void PopulateItem(Song item)
        {
            Console.Write("Song name: ");
            item.Title = Console.ReadLine();
            Console.Write("Song artist name: ");
            item.ArtistName = Console.ReadLine();
            Console.Write("Song year: ");
            item.Year = Int32.Parse(Console.ReadLine());
        }

        protected override void RenderShortInfo(Song item)
        {
            Console.WriteLine("Song ID: " + item.Id);
            Console.WriteLine("Song Name: " + item.Title);
            Console.WriteLine("==============================================");
        }
    }
}
