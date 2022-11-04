using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI
{
    public class HomePresenter
    {
        private Model model;
        private IHomeView view;
        public HomePresenter(IHomeView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.FormLoaded += LoadTracks;
            this.view.FormLoaded += LoadUserInfo;
            this.view.FormLoaded += LoadPlaylists;

            this.view.RefreshForm += LoadTracks;
            this.view.RefreshForm += LoadPlaylists;
        }
        public void LoadTracks()
        {
            var Tracks = this.model.GetUserTracks();
            this.view.SetTracks(Tracks);
        }
        public void LoadUserInfo()
        {
            User u = this.model.GetUserInfo();
            this.view.SetUserInfo(u);
        }
        public void LoadPlaylists()
        {
            var Playlists = this.model.GetUserPlaylists();
            this.view.SetPlaylists(Playlists);
        }
    }
}
