using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

using db_cp.data_access.PSQLRepository;

using db_cp.buisiness_logic.bl_structures;
using db_cp.data_access.EntityStructures;

using db_cp.Util;

namespace db_cp.data_access
{
    public class DAFacade : IRepFacade
    {
        private IRepositoryUser RepUser;
        private IRepositoryTrack RepTrack;
        private IRepositoryPlaylist RepPlaylist;

        private Converter converter;

        private IOC Dep;

        public DAFacade()
        {
            Dep = new IOC();

            RepUser = Dep.ninjectKernel.Get<IRepositoryUser>();
            RepTrack = Dep.ninjectKernel.Get<IRepositoryTrack>();
            RepPlaylist = Dep.ninjectKernel.Get<IRepositoryPlaylist>();

            converter = new Converter();
        }
        public DAFacade(string Login, string Password)
        {
            Dep = new IOC(Login, Password);

            RepUser = Dep.ninjectKernel.Get<IRepositoryUser>();
            RepTrack = Dep.ninjectKernel.Get<IRepositoryTrack>();
            RepPlaylist = Dep.ninjectKernel.Get<IRepositoryPlaylist>();

            converter = new Converter();
        }
        
        public List<User> GetAllUsers()
        {
            List<Users> users = this.RepUser.GetAllUsers();

            List<User> res = new List<User>();

            foreach (Users u in users)
            {
                res.Add(this.converter.ConvertUserDBToBL(u));
            }

            return res;
        }

        public User GetUserByLogin(string Login)
        {
            Users res = this.RepUser.GetUserByLogin(Login);
            return this.converter.ConvertUserDBToBL(res);
        }

        public void InsertUser(User u)
        {
            RepUser.InsertUser(this.converter.ConvertUserBLToDB(u));
        }

        public bool CheckUserLogin(string Login)
        {
            return this.RepUser.CheckUserLogin(Login);
        }

        public int GetNewIdUser()
        {
            return RepUser.GetNewId();
        }

        public void UpdatePassword(int UId, string Password)
        {
            this.RepUser.UpdatePassword(UId, Password);
        }
        public void UpdateLogin(int UId, string Login)
        {
            this.RepUser.UpdateLogin(UId, Login);
        }
        public void UpdateMail(int UId, string Mail)
        {
            this.RepUser.UpdateMail(UId, Mail);
        }

        // track

        public List<Track> GetAllTracks()
        {
            List<Tracks> tracks = this.RepTrack.GetAllTracks();
            List<Track> res = new List<Track>();

            foreach (Tracks t in tracks)
            {
                res.Add(this.converter.ConvertTrackDBToBL(t));
            }

            return res;
        }
        public List<Track> GetTracksByUser(int IdUser)
        {
            List<Tracks> tracks = this.RepTrack.GetTracksByUser(IdUser);
            List<Track> res = new List<Track>();

            foreach (Tracks t in tracks)
            {
                res.Add(this.converter.ConvertTrackDBToBL(t));
            }

            return res;
        }

        public int GetNewIdTrack()
        {
            return RepTrack.GetNewId();
        }
        public void InsertTrack(Track t)
        {
            RepTrack.InsertTrack(this.converter.ConvertTrackBLToDB(t));
        }

        public void InsertTrackToUser(Track t, int UId)
        {
            Tu tu = new Tu();
            tu.Id = RepTrack.GetNewIdTu();
            tu.IdTrack = t.Id;
            tu.IdUser = UId;

            RepTrack.InsertTrackToUser(tu);
        }
        public bool CheckTrackName(string Name)
        {
            return this.RepTrack.CheckTrackName(Name);
        }

        // playlist

        public List<Playlist> GetUserPlaylists(int IdUser)
        {
            List<Playlists> playlists = this.RepPlaylist.GetUserPlaylists(IdUser);
            List<Playlist> res = new List<Playlist>();

            foreach (Playlists p in playlists)
            {
                Playlist pl = new Playlist();
                pl.Id = p.Id;
                pl.AuthorId = p.AuthorId;
                pl.Name = p.Name;
                pl.Description = p.Description;
                pl.tracks = this.converter.ConvertTrackListDBToBL(this.RepTrack.GetTracksByPlaylist(p.Id));
                res.Add(pl);
            }

            return res;
        }
        public int GetNewIdPlaylist()
        {
            return this.RepPlaylist.GetNewId();
        }

        public void InsertPlaylist(Playlist playlist)
        {
            this.RepPlaylist.InsertPlaylist(this.converter.ConvertPlaylistBLToDB(playlist));

            foreach (Track t in playlist.tracks)
            {
                Tp tp = new Tp();
                tp.Id = RepPlaylist.GetNewIdTp();
                tp.IdPlaylist = playlist.Id;
                tp.IdTrack = t.Id;

                this.RepPlaylist.InsertTrackToPLaylist(tp);
            }
        }

        public void InsertTrackToPLaylist(Track track, Playlist playlist)
        {
            Tp tp = new Tp();
            tp.Id = RepPlaylist.GetNewIdTp();
            tp.IdPlaylist = playlist.Id;
            tp.IdTrack = track.Id;

            this.RepPlaylist.InsertTrackToPLaylist(tp);
        }
        public void UpdateTrack(Track t)
        {
            this.RepTrack.UpdateTrack(this.converter.ConvertTrackBLToDB(t));
        }
        public void UpdateUser(User u)
        {
            this.RepUser.UpdateUser(this.converter.ConvertUserBLToDB(u));
        }
        public void DeleteTrack(Track t)
        {
            this.RepTrack.DeleteTrack(this.converter.ConvertTrackBLToDB(t));
        }
        public void DeleteTrackFromUser(Track t, User u)
        {
            this.RepTrack.DeleteTrackFromUser(this.converter.ConvertTrackBLToDB(t), 
                this.converter.ConvertUserBLToDB(u));
        }
        public void DeleteTrackFromPlaylist(Track t, Playlist p)
        {
            this.RepPlaylist.DeleteTrackFromPlaylist(this.converter.ConvertTrackBLToDB(t), 
                this.converter.ConvertPlaylistBLToDB(p));
        }
        public void DeletePlaylist(Playlist p)
        {
            //this.RepPlaylist.DeletePlaylist(this.converter.ConvertPlaylistBLToDB(p));
            this.RepPlaylist.DeletePlaylist(p.Id);
        }
    }
}
