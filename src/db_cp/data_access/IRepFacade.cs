using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.data_access
{
    public interface IRepFacade
    {
        User GetUserByLogin(string Login);
        List<User> GetAllUsers();
        void InsertUser(User u);
        bool CheckUserLogin(string Login);
        void UpdatePassword(int UId, string Password);
        void UpdateLogin(int UId, string Login);
        void UpdateMail(int UId, string Mail);
        List<Track> GetAllTracks();
        List<Track> GetTracksByUser(int IdUser);
        void InsertTrack(Track t);
        void InsertTrackToUser(Track t, int UId);
        List<Playlist> GetUserPlaylists(int IdUser);
        void InsertPlaylist(Playlist playlist);
        void InsertTrackToPLaylist(Track track, Playlist playlist);
        void UpdateTrack(Track t);
        void UpdateUser(User u);
        void DeleteTrack(Track t);
        void DeleteTrackFromUser(Track t, User u);
        void DeleteTrackFromPlaylist(Track t, Playlist p);
    }
}
