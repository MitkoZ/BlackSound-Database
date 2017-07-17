using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PlaylistsSongsRepository:BaseRepository<PlaylistsSong>
    {
        public override void Save(PlaylistsSong playlistsSong)
        {
            if (playlistsSong.Id == 0)
            {
                base.Create(playlistsSong);
            }
            else
            {
                base.Update(playlistsSong, item => item.Id == playlistsSong.Id);
            }
        }
    }
}
