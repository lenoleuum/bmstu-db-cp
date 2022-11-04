using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.data_access;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.buisiness_logic
{
    public class BLFacade
    {
        private DAFacade facade;

        public BLFacade()
        {
            this.facade = new DAFacade();
        }
        public BLFacade(string Login, string Password)
        {
            this.facade = new DAFacade(Login, Password);
        }

        public User GetUserInfo(string Login)
        {
            return this.facade.GetUserByLogin(Login);
        }
        public void RegisterUser(User u)
        {
            u.Id = facade.GetNewIdUser();
            this.facade.InsertUser(u);
        }

        public bool CheckUserLogin(string Login)
        {
            return facade.CheckUserLogin(Login);    
        }

        public string GetUserPassword(string Login)
        {
            return this.facade.GetUserByLogin(Login).Password;
        }

        public List<Track> GetUsersTracks(int id)
        {
            return this.facade.GetTracksByUser(id);
        }

        public List<Track> GetTracks()
        {
            return this.facade.GetAllTracks();
        }

        public int AddTrack(Track track)
        {
            track.Id = facade.GetNewIdTrack();
            this.facade.InsertTrack(track);

            return track.Id;
        }

        public void AddTrackToUserList(Track track, int UId)
        {
            this.facade.InsertTrackToUser(track, UId);
        }

        public List<Playlist> GetUsersPlaylist(int id)
        {
            return this.facade.GetUserPlaylists(id);
        }
        public int AddPlaylist(Playlist playlist)
        {
            playlist.Id = this.facade.GetNewIdPlaylist();
            this.facade.InsertPlaylist(playlist);

            return playlist.Id;
        }

        public void AddTrackToPlaylist(Track track, Playlist playlist)
        {
            this.facade.InsertTrackToPLaylist(track, playlist);
        }

        public void ChangePassword(int UId, string Password)
        {
            this.facade.UpdatePassword(UId, Password);
        }
        public void ChangeLogin(int UId, string Login)
        {
            this.facade.UpdateLogin(UId, Login);
        }
        public void ChangeMail(int UId, string Mail)
        {
            this.facade.UpdateMail(UId, Mail);
        }
        public void UpdateTrack(Track track)
        {
            this.facade.UpdateTrack(track);
        }
        public List<User> GetAllUsers()
        {
            return this.facade.GetAllUsers();
        }
        public void UpdateUserType(User user)
        {
            this.facade.UpdateUser(user);
        }
        public void DeleteTrack(Track t)
        {
            this.facade.DeleteTrack(t);
        }
        public void DeleteTrackFromUser(Track track, User u)
        {
            this.facade.DeleteTrackFromUser(track, u);
        }
        public void DeleteTrackFromPlaylist(Track t, Playlist p)
        {
            this.facade.DeleteTrackFromPlaylist(t, p);
        }
        public void DeletePlaylist(Playlist p)
        {
            this.facade.DeletePlaylist(p);
        }
    }
}
