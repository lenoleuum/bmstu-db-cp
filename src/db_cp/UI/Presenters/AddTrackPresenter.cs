using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class AddTrackPresenter
    {
        private Model model;
        private IAddTrackView view;
        public AddTrackPresenter(IAddTrackView view, Model model)
        {
            this.view = view;
            this.model = model;
            this.view.TrackAddSelected += AddTrack;
        }
        public void AddTrack(Track track)
        {
            this.model.AddTrack(track);
            this.view.SetAddTrack();
        }
    }
}
