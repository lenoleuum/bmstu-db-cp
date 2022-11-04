using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;
using db_cp.UI.IView;

namespace db_cp.UI.Presenters
{
    public class BibTracksPresenter
    {
        private Model model;
        private IBibTracksView view;
        public BibTracksPresenter(IBibTracksView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.FormLoaded += LoadTracks;
            this.view.RefreshForm += LoadTracks;
        }
        public void LoadTracks()
        {
            var tracks = this.model.GetTracks();
            this.view.SetTracks(tracks);
        }
    }
}
