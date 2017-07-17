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
    class PlaylistsView : BaseView<Playlist>
    //the ordinary user can CRUD playlists + more
    {
        public PlaylistsView()
        {
            viewItems.Add(new ViewItem("S", "[S]hare a playlist", Share));
            viewItems.Add(new ViewItem("G", "[G]et all available songs", new SongsView().View));
            viewItems.Add(new ViewItem("A", "[A]dd a song to a playlist", AddSongToPlaylist));
            viewItems.Add(new ViewItem("E", "D[e]lete a song from a playlist", DeleteSongFromPlaylist));
        }

        protected override List<Playlist> GetListView(BaseRepository<Playlist> baseRepo)
        {
            UsersPlaylistsRepository usersPlaylistsRepo = new UsersPlaylistsRepository();
            List<UsersPlaylist> usersPlaylistsDb = usersPlaylistsRepo.GetAll(userPlaylist => userPlaylist.UserId == AuthenticationService.LoggedUser.Id); //playlists created by me or shared with me
            List<int> playlistsIds = new List<int>();
            foreach (UsersPlaylist userPlaylist in usersPlaylistsDb) //gets only the ids of the playlists for the current user
            {
                playlistsIds.Add(userPlaylist.PlaylistId);
            }

            List<Playlist> playlists = new List<Playlist>();
            PlaylistsRepository playlistsRepo = new PlaylistsRepository();
            List<Playlist> publicPlaylists = playlistsRepo.GetAll(playlist => playlist.IsPublic); //gets all public playlists
            foreach (int playlistId in playlistsIds) //gets the actual playlists for the current user
            {
                playlists.Add(playlistsRepo.GetById(playlistId));
            }
            foreach (Playlist publicPlaylist in publicPlaylists)
            {
                if (!playlists.Any(playlist => playlist.Id == publicPlaylist.Id)) //adds the public playlist in the list if it isn't added already
                {
                    playlists.Add(publicPlaylist);
                }
            }
            return playlists;
        }

        protected override void ViewMoreLogic(int id)
        {
            PlaylistsSongsRepository playlistsSongsRepo = new PlaylistsSongsRepository();
            List<PlaylistsSong> playlistsSongs = playlistsSongsRepo.GetAll(playlistsong => playlistsong.PlaylistId == id);
            if (playlistsSongs.Count == 0)
            {
                Console.WriteLine("This playlist doesn't have any songs in it!");
                return;
            }
            List<int> songsIds = new List<int>();
            foreach (PlaylistsSong playlistSong in playlistsSongs) //gets the song ids for the chosen playlist
            {
                songsIds.Add(playlistSong.SongId);
            }
            SongsRepository songsRepo = new SongsRepository();
            List<Song> songs = new List<Song>();
            foreach (int songId in songsIds) //gets the actual songs from the playlist
            {
                songs.Add(songsRepo.GetById(songId));
            }
            Console.WriteLine("Songs");
            foreach (Song song in songs)
            {
                Console.WriteLine("Song ID: " + song.Id);
                Console.WriteLine("Song Title: " + song.Title);
                Console.WriteLine("Song Artist Name: " + song.ArtistName);
                Console.WriteLine("Song Year: " + song.Year);
                Console.WriteLine("============================================");
            }
        }
        protected override List<Playlist> GetListUpdate(BaseRepository<Playlist> baseRepo)
        {
            return baseRepo.GetAll(playlist => playlist.ParentUserId == AuthenticationService.LoggedUser.Id);
        }

        protected override void DeleteMoreLogic(int id)
        {
            UsersPlaylistsRepository usersPlaylistsRepo = new UsersPlaylistsRepository();
            usersPlaylistsRepo.Delete(usersPlaylist => usersPlaylist.PlaylistId == id);
            PlaylistsSongsRepository playlistsSongsRepo = new PlaylistsSongsRepository();
            playlistsSongsRepo.Delete(playlistSong => playlistSong.PlaylistId == id);
           
        }

        protected override void UpdateItem(Playlist itemDb)
        {
            Console.WriteLine("Name: " + itemDb.Name);
            Console.Write("New name: ");
            string newName = Console.ReadLine();
            Console.WriteLine("Description: " + itemDb.Description);
            Console.Write("New Description: ");
            string newDescription = Console.ReadLine();
            Console.WriteLine("Is public?: " + itemDb.IsPublic);
            Console.Write("New is public? (true/false): ");
            string newIsPublic = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                itemDb.Name = newName;
            }
            if (!string.IsNullOrEmpty(newDescription))
            {
                itemDb.Description = newDescription;
            }
            if (!string.IsNullOrEmpty(newIsPublic))
            {
                itemDb.IsPublic = Convert.ToBoolean(newIsPublic);
            }
        }

        protected override void AddMoreLogic(Playlist item)
        {
            UsersPlaylistsRepository usersPlaylistsRepo = new UsersPlaylistsRepository();
            UsersPlaylist usersPlaylist = new UsersPlaylist(AuthenticationService.LoggedUser.Id, item.Id);
            usersPlaylistsRepo.Save(usersPlaylist);
        }

        protected override BaseRepository<Playlist> CreateRepository()
        {
            return new PlaylistsRepository();
        }

        protected override void RenderItem(Playlist item)
        {
            Console.WriteLine("ID: " + item.Id);
            Console.WriteLine("Name: " + item.Name);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("Is public?: " + item.IsPublic);
            Console.WriteLine("==============================================================================");
        }

        protected override void PopulateItem(Playlist item)
        {
            Console.Write("Playlist Name: ");
            item.Name = Console.ReadLine();
            Console.Write("Playlist Description: ");
            item.Description = Console.ReadLine();
            Console.Write("Is public? (true/false): ");
            item.IsPublic = Convert.ToBoolean(Console.ReadLine());
            item.ParentUserId = AuthenticationService.LoggedUser.Id;
        }

        protected override void RenderShortInfo(Playlist item)
        {
            Console.WriteLine("Playlist ID: " + item.Id);
            Console.WriteLine("Playlist Name: " + item.Name);
            Console.WriteLine("==========================================");
        }

        private void DeleteSongFromPlaylist()
        {
            Console.Clear();
            PlaylistsRepository playlistRepo = new PlaylistsRepository();
            List<Playlist> playlists = playlistRepo.GetAll(playlist => playlist.ParentUserId == AuthenticationService.LoggedUser.Id);
            if (playlists.Count == 0)
            {
                Console.WriteLine("You don't have any playlists");
                Console.ReadKey(true);
                return;
            }
            foreach (Playlist playlist in playlists)
            {
                Console.WriteLine("Playlist ID: " + playlist.Id);
                Console.WriteLine("Playlist Name: " + playlist.Name);
                Console.WriteLine("Playlist Description: " + playlist.Description);
                Console.WriteLine("==============================================================");
            }
            Console.Write("Playlist ID: ");
            int playlistInputId = Int32.Parse(Console.ReadLine());
            if (!playlists.Any(playlist => playlist.Id == playlistInputId))
            {
                Console.WriteLine("Playlist not found!");
                Console.ReadKey(true);
                return;
            }

            PlaylistsSongsRepository playlistsSongsRepo = new PlaylistsSongsRepository();
            List<PlaylistsSong> playlistsSongs = playlistsSongsRepo.GetAll(playlistSong => playlistSong.PlaylistId == playlistInputId);
            Console.Clear();
            if (playlistsSongs.Count == 0)
            {
                Console.WriteLine("This playlist doesn't have any songs!");
                Console.ReadKey(true);
                return;
            }
            SongsRepository songsRepo = new SongsRepository();
            List<Song> songs = new List<Song>();
            foreach (PlaylistsSong playlistSong in playlistsSongs)
            {
                songs.Add(songsRepo.GetById(playlistSong.SongId));
            }
            foreach (Song song in songs)
            {
                Console.WriteLine("Song ID: " + song.Id);
                Console.WriteLine("Song Title: " + song.Title);
                Console.WriteLine("Song Artist Name: " + song.ArtistName);
                Console.WriteLine("Song Year: " + song.Year);
                Console.WriteLine("==========================================================");
            }
            Console.Write("Song ID: ");
            int songInputId = Int32.Parse(Console.ReadLine());
            bool IsSongExist = playlistsSongsRepo.GetAll(playlistSong => playlistSong.PlaylistId == playlistInputId && playlistSong.SongId == songInputId).Count > 0;
            if (!IsSongExist)
            {
                Console.WriteLine("Song not found");
                Console.ReadKey(true);
                return;
            }
            playlistsSongsRepo.Delete(playlistSong => playlistSong.PlaylistId == playlistInputId && playlistSong.SongId == songInputId);
            Console.WriteLine("Song deleted successfully from the playlist");
            Console.ReadKey(true);
        }

        private void AddSongToPlaylist()
        {
            Console.Clear();
            SongsRepository songsRepo = new SongsRepository();
            List<Song> songsDb = songsRepo.GetAll();
            foreach (Song song in songsDb)
            {
                Console.WriteLine("Song ID: " + song.Id);
                Console.WriteLine("Song Title: " + song.Title);
                Console.WriteLine("Song ArtistName: " + song.ArtistName);
                Console.WriteLine("Song Year: " + song.Year);
                Console.WriteLine("==================================================");
            }
            Console.Write("Song ID: ");
            int songIdInput = Int32.Parse(Console.ReadLine());
            List<Song> songDb = songsRepo.GetAll((song => song.Id == songIdInput));
            if (songDb[0] == null)
            {
                Console.WriteLine("Cannot find song");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            PlaylistsRepository playlistsRepo = new PlaylistsRepository();
            List<Playlist> playlistsDb = playlistsRepo.GetAll(playlist => AuthenticationService.LoggedUser.Id == playlist.ParentUserId);
            foreach (Playlist playlist in playlistsDb)// user's playlists
            {
                Console.WriteLine("Playlist ID: " + playlist.Id);
                Console.WriteLine("Playlist Name: " + playlist.Name);
                Console.WriteLine("Playlist Description: " + playlist.Description);
                Console.WriteLine("Playlist Is public?: " + playlist.IsPublic);
                Console.WriteLine("====================================================");
            }
            Console.Write("Playlist ID: ");
            int playlistIdInput = Int32.Parse(Console.ReadLine());
            if (playlistsDb.Count == 0)
            {
                Console.WriteLine("Playlist not found");
                Console.ReadKey(true);
                return;
            }
            playlistsDb = playlistsDb.Where(playlist => playlist.Id == playlistIdInput).ToList();
            PlaylistsSong playlistsSongs = new PlaylistsSong(songDb[0], playlistsDb[0]);
            PlaylistsSongsRepository playlistsSongsRepo = new PlaylistsSongsRepository();
            playlistsSongsRepo.Save(playlistsSongs);
            Console.WriteLine("Song saved successfully to playlist!");
            Console.ReadKey(true);
        }

        private void Share()
        {
            Console.Clear();
            Console.WriteLine("Share a playlist");
            PlaylistsRepository playlistsRepo = new PlaylistsRepository();
            List<Playlist> playlistsDb = playlistsRepo.GetAll(playlist => playlist.ParentUserId == AuthenticationService.LoggedUser.Id);
            foreach (Playlist playlist in playlistsDb)
            {
                Console.WriteLine("Playlist ID: " + playlist.Id);
                Console.WriteLine("Playlist Name: " + playlist.Name);
                Console.WriteLine("Playlist Description: " + playlist.Description);
                Console.WriteLine("Playlist Is Public: " + playlist.IsPublic);
                Console.WriteLine("===============================================");
            }
            Console.Write("Playlist ID: ");
            int playlistIdInput = Int32.Parse(Console.ReadLine());
            Playlist playlistDb = playlistsDb.FirstOrDefault(playlist => playlist.Id == playlistIdInput);
            if (playlistDb == null)
            {
                Console.WriteLine("Cannot find playlist");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            UsersRepository usersRepo = new UsersRepository();
            List<User> usersDb = usersRepo.GetAll(user => user.Id != AuthenticationService.LoggedUser.Id);
            foreach (User user in usersDb)
            {
                Console.WriteLine("User ID: " + user.Id);
                Console.WriteLine("User Username: " + user.Username);
                Console.WriteLine("User Email: " + user.Email);
                Console.WriteLine("============================================");
            }

            Console.Write("User ID: ");
            int userIdInput = Int32.Parse(Console.ReadLine());
            Console.Clear();
            User userDb = usersDb.FirstOrDefault(user => user.Id == userIdInput);
            if (userDb == null)
            {
                Console.WriteLine("Cannot find user");
                Console.ReadKey(true);
                return;
            }
            UsersPlaylistsRepository usersPlaylistsRepository = new UsersPlaylistsRepository();
            UsersPlaylist usersPlaylists = new UsersPlaylist(userDb.Id, playlistDb.Id);
            usersPlaylistsRepository.Save(usersPlaylists);
            Console.WriteLine("Playlist shared successfully.");
            Console.ReadKey(true);
        }
    }
}
