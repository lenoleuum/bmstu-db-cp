using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class CreatePlaylistPresenter
    {
        private Model model;
        private ICreatePlaylistView view;
        public CreatePlaylistPresenter(ICreatePlaylistView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.PlaylistCreated += CreatePlaylist;
        }
        public void CreatePlaylist(Playlist playlist)
        {
            playlist.AuthorId = this.model.GetUserInfo().Id;
            this.model.AddPlaylist(playlist);
            this.view.SetCreatePlaylist();
        }
    }
}
