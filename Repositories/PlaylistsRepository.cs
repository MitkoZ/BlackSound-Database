using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PlaylistsRepository:BaseRepository<Playlist>
    {
        public override void Save(Playlist playlist)
        {
            if (playlist.Id == 0)
            {
                base.Create(playlist);
            }
            else
            {
                base.Update(playlist, item => item.Id == playlist.Id);
            }
        }
    }
}
