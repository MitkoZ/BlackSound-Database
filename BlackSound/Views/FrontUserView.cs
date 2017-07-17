using BlackSound.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Views
{
    public class FrontUserView
    {
        public void Show()
        {
            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                FrontAdminView frontAdminView = new FrontAdminView();
                frontAdminView.Show();
            }
            else
            {
                PlaylistsView ordinaryUserView = new PlaylistsView();
                ordinaryUserView.Show();
            }

        }
    }
}
