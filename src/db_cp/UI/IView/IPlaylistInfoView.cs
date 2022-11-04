using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.IView
{
    public interface IPlaylistInfoView
    {
        event Action<Playlist> FormLoaded;
        event Action<Track, Playlist> TrackAdded;
        event Action<Playlist> DeletePlaylistSelected;
        void SetTracks(List<Track> tracks);
        void SetAddTrackToPlaylist(Track t);
    }
}
