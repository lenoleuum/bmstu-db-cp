using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI
{
    public interface IHomeView
    {
        event Action FormLoaded;
        event Action RefreshForm;
        void SetUserInfo(User user);
        void SetTracks(List<Track> tracks);
        void SetPlaylists(List<Playlist> playlists);
        void RefreshInfo();
    }
}
