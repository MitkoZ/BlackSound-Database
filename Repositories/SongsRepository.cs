using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SongsRepository:BaseRepository<Song>
    {
        public override void Save(Song song)
        {
            if (song.Id == 0)
            {
                base.Create(song);
            }
            else
            {
                base.Update(song, item => item.Id == song.Id);
            }
        }
    }
}
