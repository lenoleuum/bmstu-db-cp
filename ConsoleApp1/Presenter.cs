using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp_console
{
    public class Presenter
    {
        private Model model;
        private IView view;
        public Presenter(Model model, IView view)
        {
            this.view = view;
            this.model = model;

            this.view.UserInfoSelected += LoadUserInfo;
            this.view.TrackListSelected += LoadTrackList;
            this.view.TrackUserListSelected += LoadTrackUserList;
            this.view.PlaylistListSelected += LoadPlaylistList;
            this.view.UserListSelected += LoadUserList;
            this.view.TrackInfoSelected += LoadTrackInfo;
            this.view.PlaylistInfoSelected += LoadPlaylistInfo;
            this.view.TrackDeleteFromUserSelected += DeleteTrackFromUser;
            this.view.DeleteTrackFromPlaylistSelected += DeleteTrackFromPlaylist;
            this.view.TrackAddToUserSelected += AddTrackToUser;
            this.view.TrackAddToPlaylistSelected += AddTrackToPlaylist;
        }
        public void LoadUserInfo()
        {
            User u = this.model.GetUserInfo();
            this.view.SetUserInfo(u);
        }
        public void LoadTrackList()
        {
            List<Track> tracks = this.model.GetTracks();
            this.view.SetTrackList(tracks);
        }
        public void LoadTrackUserList()
        {
            List<Track> tracks = this.model.GetUserTracks();
            this.view.SetTrackUserList(tracks);
        }
        public void LoadPlaylistList()
        {
            List<Playlist> playlists = this.model.GetUserPlaylists();
            this.view.SetPlaylistList(playlists);
        }
        public void LoadUserList()
        {
            List<User> users = this.model.GetAllUsers();
            this.view.SetUserList(users);
        }
        public void LoadTrackInfo(int Id)
        {
            Track t = this.model.GetTracks().Find(a => a.Id == Id);
            this.view.SetTrackInfo(t);
        }
        public void LoadPlaylistInfo(int Id)
        {
            Playlist p = this.model.GetUserPlaylists().Find(a => a.Id == Id);
            this.view.SetPlaylistInfo(p);
        }
        public void DeleteTrackFromUser(int Id)
        {
            Track t = this.model.GetUserTracks().Find(a => a.Id == Id);
            this.model.DeleteTrackFromUser(t);
        }
        public void DeleteTrackFromPlaylist(int TId, int PId)
        {
            Playlist p = this.model.GetUserPlaylists().Find(a => a.Id == PId);
            Track t = p.tracks.Find(a => a.Id == TId);
            this.model.DeleteTrackFromPlaylist(t, p);
        }
        public void AddTrackToUser(int Id)
        {
            Track t = this.model.GetTracks().Find(a => a.Id == Id);
            this.model.AddTrackToUserList(t);
        }
        public void AddTrackToPlaylist(int PId, int TId)
        {
            Playlist p = this.model.GetUserPlaylists().Find(a => a.Id == PId);
            Track t = this.model.GetUserTracks().Find(a => a.Id == TId);
            this.model.AddTrackToPlaylist(t, p);
        }
    }
}
