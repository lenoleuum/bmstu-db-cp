using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp_console
{
    public interface IView
    {
        event Action UserInfoSelected;
        event Action TrackListSelected;
        event Action TrackUserListSelected;
        event Action UserListSelected;
        event Action PlaylistListSelected;
        event Action<int> TrackInfoSelected;
        event Action<int> PlaylistInfoSelected;
        event Action<int> TrackDeleteFromUserSelected;
        event Action<int, int> DeleteTrackFromPlaylistSelected;
        event Action<int> TrackAddToUserSelected;
        event Action<int, int> TrackAddToPlaylistSelected;
        void SetUserInfo(User u);
        void SetTrackList(List<Track> tracks);
        void SetTrackUserList(List<Track> tracks);
        void SetUserList(List<User> users);
        void SetPlaylistList(List<Playlist> playlists);
        void SetTrackInfo(Track t);
        void SetPlaylistInfo(Playlist p);
    }
}
