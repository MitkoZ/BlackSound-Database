using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public override void Save(User user)
        {
            if (user.Id == 0)
            {
                base.Create(user);
            }
            else
            {
                base.Update(user, item => item.Id == user.Id);
            }
        }
    }
}
