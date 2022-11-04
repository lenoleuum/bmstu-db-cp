using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access
{
    public interface IRepositoryUser
    {
        List<Users> GetAllUsers();
        Users GetUserByLogin(string Login);
        void InsertUser(Users U);
        bool CheckUserLogin(string Login);
        int GetNewId();
        void DeleteUser(Users U);
        void UpdatePassword(int Id, string Password);
        void UpdateMail(int Id, string Mail);
        void UpdateLogin(int Id, string Login);
        void UpdateUser(Users u);
    }

    public interface IRepositoryTrack
    {
        List<Tracks> GetAllTracks();
        List<Tracks> GetTracksByUser(int IdUser);
        List<Tracks> GetTracksByPlaylist(int PId);
        void InsertTrack(Tracks track);
        void InsertTrackToUser(Tu tu);
        int GetNewId();
        int GetNewIdTu();
        bool CheckTrackName(string Name);
        void DeleteTrack(Tracks Track);
        void DeleteTrackFromUser(Tracks t, Users u);
        void UpdateTrack(Tracks Track);
    }

    public interface IRepositoryPlaylist
    {
        List<Playlists> GetUserPlaylists(int uid);
        void InsertPlaylist(Playlists P);
        int GetNewId();
        int GetNewIdTp();
        void InsertTrackToPLaylist(Tp tp);
        void DeletePlaylist(Playlists P);
        void DeletePlaylist(int P);
        void DeleteTrackFromPlaylist(Tracks t, Playlists p);
    }

}