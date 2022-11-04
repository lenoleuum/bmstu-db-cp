using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class ChooseTrackPresenter
    {
        private Model model;
        private IChooseTrackView view;
        public ChooseTrackPresenter(IChooseTrackView view, Model model)
        {
            this.view = view;  
            this.model = model;

            this.view.FormLoadedTracks += LoadTracks;
            this.view.FormLoadedUserTracks += LoadUserTracks;
            this.view.TrackSelected += AddTrackToUser;
        }
        public void LoadTracks()
        {
            var tracks = this.model.GetTracks();
            view.SetTracks(tracks);
        }
        public void LoadUserTracks()
        {
            var tracks = this.model.GetUserTracks();
            view.SetTracks(tracks);
        }
        public void AddTrackToUser(Track t)
        {
            if (this.model.CheckTrackName(t.Name))
            {
                this.model.AddTrackToUserList(t);
                this.view.SetAddTrackToUser();
            }
            else
                this.view.SetAddTrackToUserFail();
        }
    }
}
