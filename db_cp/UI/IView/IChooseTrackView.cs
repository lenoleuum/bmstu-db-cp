using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.IView
{
    public interface IChooseTrackView
    {
        event Action FormLoadedTracks;
        event Action FormLoadedUserTracks;
        event Action<Track> TrackSelected;
        void SetTracks(List<Track> tracks);
        void SetAddTrackToUser();
        void SetAddTrackToUserFail();
    }
}
