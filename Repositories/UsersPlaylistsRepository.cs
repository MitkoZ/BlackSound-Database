using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UsersPlaylistsRepository:BaseRepository<UsersPlaylist>
    {
        public override void Save(UsersPlaylist usersPlaylist)
        {
            if (usersPlaylist.Id == 0)
            {
                base.Create(usersPlaylist);
            }
            else
            {
                base.Update(usersPlaylist, item => item.Id == usersPlaylist.Id);
            }
        }
    }
}
