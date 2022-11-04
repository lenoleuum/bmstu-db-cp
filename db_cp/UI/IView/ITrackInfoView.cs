using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.IView
{
    public interface ITrackInfoView
    {
        event Action<Track> TrackChanged;
        event Action<Track> TrackDeletedFromUser;
        event Action<Track, Playlist> TrackDeletedFromPlaylist;
        event Action<Track> TrackDeletedFromBib;
        void SetChanges();
        void SetDeleteTrackFromUser();
        void SetDeleteTrackFromPlaylist();
        void SetDeleteTrack();
    }
}
