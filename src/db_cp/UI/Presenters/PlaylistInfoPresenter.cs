using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class PlaylistInfoPresenter
    {
        private Model model;
        private IPlaylistInfoView view;
        public PlaylistInfoPresenter(IPlaylistInfoView view, Model model)
        {
            this.view = view;   
            this.model = model;

            this.view.FormLoaded += LoadTracks;
            this.view.TrackAdded += AddTrackToPlaylist;
            this.view.DeletePlaylistSelected += DeletePlaylist; 
        }
        public void LoadTracks(Playlist p)
        {
            var t = this.model.GetUserPlaylists().Find(a => a.Id == p.Id).tracks;
            this.view.SetTracks(t);
        }
        public void AddTrackToPlaylist(Track t, Playlist p)
        {
            this.model.AddTrackToPlaylist(t, p);
            this.view.SetAddTrackToPlaylist(t);
        }
        public void DeletePlaylist(Playlist p)
        {
            this.model.DeletePlaylist(p);
        }
    }
}
