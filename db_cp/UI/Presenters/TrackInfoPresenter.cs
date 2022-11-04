using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class TrackInfoPresenter
    {
        private Model model;
        private ITrackInfoView view;
        public TrackInfoPresenter(ITrackInfoView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.TrackChanged += LoadChanges;
            this.view.TrackDeletedFromUser += DeleteTrackFromUser;
            this.view.TrackDeletedFromPlaylist += DeleteTrackFromPlaylist;
            this.view.TrackDeletedFromBib += DeleteTrack;
        }
        public void LoadChanges(Track track)
        {
            this.model.UpdateTrack(track);
            this.view.SetChanges();
        }
        public void DeleteTrackFromUser(Track track)
        {
            this.model.DeleteTrackFromUser(track);
            this.view.SetDeleteTrackFromUser();
        }
        public void DeleteTrackFromPlaylist(Track track, Playlist playlist)
        {
            this.model.DeleteTrackFromPlaylist(track, playlist);
            this.view.SetDeleteTrackFromPlaylist();
        }
        public void DeleteTrack(Track track)
        {
            this.model.DeleteTrack(track);
            this.view.SetDeleteTrack();
        }
    }
}
