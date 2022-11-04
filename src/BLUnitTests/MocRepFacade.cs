using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;
using db_cp.data_access;

namespace BLUnitTests
{
    public class MocRepFacade : IRepFacade
    {
        public User GetUserByLogin(string Login)
        {
            User u = new User();
            u.Id = 123;
            u.Name = "Test User";
            u.Country = "Test Country";
            u.Mail = "test@test.ru";
            u.Login = "test";
            u.Password = "test1234";
            u.UserType = 2;

            return u;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            User u1 = new User();
            u1.Id = 123;
            u1.Name = "Test User 1";
            u1.Country = "Test Country";
            u1.Mail = "test1@test.ru";
            u1.Login = "test1";
            u1.Password = "test1234";
            u1.UserType = 2;

            User u2 = new User();
            u2.Id = 321;
            u2.Name = "Test User 2";
            u2.Country = "Test Country";
            u2.Mail = "test2@test.ru";
            u2.Login = "test2";
            u2.Password = "test1234";
            u2.UserType = 2;

            users.Append(u1);
            users.Append(u2);

            return users;
        }
        
        public List<Track> GetAllTracks()
        {
            List<Track> tracks = new List<Track>();

            Track t1 = new Track();
            t1.Id = 123;
            t1.Name = "Test Track 1";
            t1.AuthorId = 1;
            t1.Duration = null;
            t1.DateOfCreation = DateTime.Today;
            t1.Description = "just a test track";
            t1.MidiFile = null;

            Track t2 = new Track();
            t2.Id = 321;
            t2.Name = "Test Track 2";
            t2.AuthorId = 2;
            t2.Duration = null;
            t2.DateOfCreation = DateTime.Today;
            t2.Description = "just a test track";
            t2.MidiFile = null;

            Track t3 = new Track();
            t3.Id = 321;
            t3.Name = "Test Track 3";
            t3.AuthorId = 3;
            t3.Duration = null;
            t3.DateOfCreation = DateTime.Today;
            t3.Description = "just a test track";
            t3.MidiFile = null;

            tracks.Append(t1);
            tracks.Append(t2);
            tracks.Append(t3);

            return tracks;
        }
        public List<Track> GetTracksByUser(int id)
        {
            List<Track> utracks = new List<Track>();

            Track t1 = new Track();
            t1.Id = 123;
            t1.Name = "User Track 1";
            t1.AuthorId = id;
            t1.Duration = null;
            t1.DateOfCreation = DateTime.Today;
            t1.Description = "user track";
            t1.MidiFile = null;

            Track t2 = new Track();
            t2.Id = 321;
            t2.Name = "User Track 2";
            t2.AuthorId = id;
            t2.Duration = null;
            t2.DateOfCreation = DateTime.Today;
            t2.Description = "user track";
            t2.MidiFile = null;

            utracks.Append(t1);
            utracks.Append(t2);

            return utracks;
        }
        public void InsertTrack(Track t) { }
        public void InsertTrackToUser(Track t, int UId) { }
        public List<Playlist> GetUserPlaylists(int id)
        {
            List<Playlist> playlists = new List<Playlist>();

            List<Track> tracks1 = new List<Track>();

            Track t1 = new Track();
            t1.Id = 123;
            t1.Name = "User Track 1";
            t1.AuthorId = id;
            t1.Duration = null;
            t1.DateOfCreation = DateTime.Today;
            t1.Description = "user track";
            t1.MidiFile = null;

            Track t2 = new Track();
            t2.Id = 321;
            t2.Name = "User Track 2";
            t2.AuthorId = id;
            t2.Duration = null;
            t2.DateOfCreation = DateTime.Today;
            t2.Description = "user track";
            t2.MidiFile = null;

            tracks1.Append(t1);
            tracks1.Append(t2);

            Playlist p1 = new Playlist();
            p1.Id = 123;
            p1.Name = "Test Playlist 1";
            p1.AuthorId = id;
            p1.DateOfCreation = DateTime.Today;
            p1.Description = "just a test playlist";
            p1.tracks = tracks1;

            playlists.Append(p1);

            List<Track> tracks2 = new List<Track>();

            Track t3 = new Track();
            t3.Id = 213;
            t3.Name = "User Track 3";
            t3.AuthorId = id;
            t3.Duration = null;
            t3.DateOfCreation = DateTime.Today;
            t3.Description = "user track";
            t3.MidiFile = null;

            tracks2.Append(t3);

            Playlist p2 = new Playlist();
            p2.Id = 321;
            p2.Name = "Test Playlist 2";
            p2.AuthorId = id;
            p2.DateOfCreation = DateTime.Today;
            p2.Description = "just a test playlist";
            p2.tracks = tracks1;

            playlists.Append(p2);

            return playlists;
        }
        public void InsertPlaylist(Playlist playlist) { }
        public void InsertTrackToPLaylist(Track track, Playlist playlist) { }
        public void InsertUser(User u) { }
        public bool CheckUserLogin(string Login) { return false; }
        public void UpdatePassword(int UId, string Password) { }
        public void UpdateLogin(int UId, string Login) { }
        public void UpdateMail(int UId, string Mail) { }
        public void RegisterUser(User u) { }
        public void AuthorizeUser(string Login, string Password) { }
        public void UpdateTrack(Track t) { }
        public void UpdateUser(User u) { }
        public void DeleteTrack(Track t) { }
        public void DeleteTrackFromUser(Track t, User u) { }
        public void DeleteTrackFromPlaylist(Track t, Playlist p) { }
    }
}
