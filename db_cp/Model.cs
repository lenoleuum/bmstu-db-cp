using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp
{
    public class Model
    {
        private User LoggedUser;
        private List<Track> Tracks;
        private List<Playlist> Playlists;
        private BLFacade facade;

        public Model()
        {
            this.facade = new BLFacade();
            this.LoggedUser = null;
            this.Tracks = new List<Track>();
            this.Playlists = new List<Playlist>();
        }
        public Model(string Login, string Password)
        {
            string ConString = "Host=localhost;Port=5432;Database=db_cp;Username=" + Login + ";Password=" + Password;

            this.facade = new BLFacade(Login, Password);

            this.LoggedUser = this.facade.GetUserInfo(Login);

            this.Tracks = this.facade.GetUsersTracks(this.LoggedUser.Id);

            this.Playlists = this.facade.GetUsersPlaylist(this.LoggedUser.Id);
        }
        public Model(User u)
        {
            string ConString = "Host=localhost;Port=5432;Database=db_cp;Username=" + u.Login + ";Password=" + u.Password;

            this.facade = new BLFacade(u.Login, u.Password);

            this.LoggedUser = this.facade.GetUserInfo(u.Login);

            this.Tracks = this.facade.GetUsersTracks(this.LoggedUser.Id);

            this.Playlists = this.facade.GetUsersPlaylist(this.LoggedUser.Id);
        }
        public Model(Model model) // не надо
        {
            this.facade = new BLFacade(model.GetUserInfo().Login, model.GetUserInfo().Password);

            this.LoggedUser = model.GetUserInfo();
            this.Tracks = model.GetUserTracks();
            this.Playlists = model.GetUserPlaylists();
        }
        public bool CheckUser(string Login, string Password)
        {
            // это не модель делает
            if (this.facade.CheckUserLogin(Login) && this.facade.GetUserInfo(Login).Password == Password)
                return true;
            else
                return false;
        }
        public User GetUserInfo()
        {
            return this.LoggedUser;
        }
        public User GetUserInfo(string Login)
        {
            return this.facade.GetUserInfo(Login);
        }

        public bool CheckLogin(string Login)
        {
            return this.facade.CheckUserLogin(Login);
        }

        public void Register(User u)
        {
            this.facade.RegisterUser(u);
        }
        public List<Track> GetUserTracks()
        {
            //return this.Tracks;
            return this.facade.GetUsersTracks(this.LoggedUser.Id);
        }

        public List<Track> GetTracks()
        {
            return this.facade.GetTracks();
        }

        public void AddTrack(Track track)
        {
            track.Id = this.facade.AddTrack(track);
        }
        public void AddTrackToUserList(Track track)
        {
            this.facade.AddTrackToUserList(track, this.LoggedUser.Id);
            this.Tracks.Add(track);
        }

        public bool CheckTrackName(string Name)
        {
            foreach (Track t in this.Tracks)
            {
                if (t.Name == Name)
                    return false;
            }

            return true;
        }

        public List<Playlist> GetUserPlaylists()
        {
            //return this.Playlists;
            return this.facade.GetUsersPlaylist(this.LoggedUser.Id);
        }

        public void AddPlaylist(Playlist playlist)
        {
            playlist.Id = this.facade.AddPlaylist(playlist);
            this.Playlists.Add(playlist);
        }

        public void AddTrackToPlaylist(Track track, Playlist playlist)
        {
            foreach (Playlist pl in this.Playlists)
            {
                if (pl.Id == playlist.Id)
                    pl.tracks.Add(track);
            }

            this.facade.AddTrackToPlaylist(track, playlist);
        }

        public bool ChangePassword(string Old, string New)
        {
            if (this.LoggedUser.Password == Old)
            {
                this.LoggedUser.Password = New;
                this.facade.ChangePassword(this.LoggedUser.Id, New);
                return true;
            }

            return false;
        }
        public void ChangeLogin(string Login)
        {
            this.LoggedUser.Login = Login;
            this.facade.ChangeLogin(this.LoggedUser.Id, Login);
        }
        public void ChangeMail(string Mail)
        {
            this.LoggedUser.Mail = Mail;
            this.facade.ChangeMail(this.LoggedUser.Id, Mail);
        }
        public void ChangeTrackName(Track track)
        {
            var t = this.Tracks.Find(a => a.Id == track.Id);
            t.Name = track.Name;
            this.facade.UpdateTrack(t);
        }
        public void UpdateTrack(Track track)
        {
            var t = this.Tracks.Find(a => a.Id == track.Id);
            t.Name = track.Name;
            t.Description = track.Description;

            this.facade.UpdateTrack(t);
        }
        public void ChangeTrackDescription(Track track)
        {
            var t = this.Tracks.Find(a => a.Id == track.Id);
            t.Description = track.Description;
            this.facade.UpdateTrack(t);
        }
        public List<User> GetAllUsers()
        {
            if (this.LoggedUser.UserType == 0)
                return this.facade.GetAllUsers();
            else
                return null;
        }
        public void ChangeUserRoleToModer(User user)
        {
            user.UserType = 1;
            this.facade.UpdateUserType(user);
        }
        public void ChangeUserRoleFromModer(User user)
        {
            user.UserType = 2;
            this.facade.UpdateUserType(user);
        }
        public void DeleteTrack(Track t)
        {
            this.Tracks.Remove(t);
            
            foreach(Playlist p in this.Playlists)
                p.tracks.Remove(t);

            this.facade.DeleteTrack(t);
        }
        public void DeleteTrackFromUser(Track t)
        {
            var track = this.Tracks.Find(a => a.Id == t.Id);
            this.Tracks.Remove(track);

            foreach (Playlist p in this.Playlists) // error not deleted
            {
                var tr = p.tracks.Find(a => a.Id == t.Id);

                if (tr != null)
                {
                    p.tracks.Remove(tr);

                    this.facade.DeleteTrackFromPlaylist(t, p);
                }
            }

            this.facade.DeleteTrackFromUser(t, this.LoggedUser);
        }
        public void DeleteTrackFromPlaylist(Track t, Playlist p)
        {
            var pl = this.Playlists.Find(a => a.Id == p.Id);
            var track = pl.tracks.Find(a => a.Id == t.Id);
            pl.tracks.Remove(track);

            this.facade.DeleteTrackFromPlaylist(t, p);
        }
        public void DeletePlaylist(Playlist p)
        {
            this.Playlists.Remove(p);

            this.facade.DeletePlaylist(p);
        }
    }
}
