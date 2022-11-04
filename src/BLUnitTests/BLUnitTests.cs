using Xunit;
using System;

using db_cp.data_access;
using db_cp.buisiness_logic.bl_structures;
using System.Collections.Generic;
using System.Linq;

namespace BLUnitTests
{
    public class BLUnitTests
    {
        private IRepFacade facade = new MocRepFacade();

        static void CheckEqualUser(User u1, User u2)
        {
            Assert.Equal(u1.Id, u2.Id);
            Assert.Equal(u1.Name, u2.Name);
            Assert.Equal(u1.Country, u2.Country);
            Assert.Equal(u1.Mail, u2.Mail);
            Assert.Equal(u1.Login, u2.Login);
            Assert.Equal(u1.Password, u2.Password);
            Assert.Equal(u1.UserType, u2.UserType);
        }

        static void CheckEqualTrack(Track t1, Track t2)
        {
            Assert.Equal(t1.Id, t2.Id);
            Assert.Equal(t1.Name, t2.Name);
            Assert.Equal(t1.Description, t2.Description);
            Assert.Equal(t1.Duration, t2.Duration);
            Assert.Equal(t1.AuthorId, t2.AuthorId);
            Assert.Equal(t1.DateOfCreation, t2.DateOfCreation);
            Assert.Equal(t1.MidiFile, t2.MidiFile);
        }
        static void CheckEqualPlaylist(Playlist p1, Playlist p2)
        {
            Assert.Equal(p1.Id, p2.Id);
            Assert.Equal(p1.Name, p2.Name);
            Assert.Equal(p1.Description, p2.Description);
            Assert.Equal(p1.AuthorId, p2.AuthorId);
            Assert.Equal(p1.DateOfCreation, p2.DateOfCreation);

            for (int i = 0; i < p1.tracks.Count; i++)
            {
                CheckEqualTrack(p1.tracks[i], p2.tracks[i]);
            }
        }

        [Fact]
        public void TestGetUserInfo()
        {
            // bl method
            User u = this.facade.GetUserByLogin("test");

            // expected result
            User defuser = new User();
            defuser.Id = 123;
            defuser.Name = "Test User";
            defuser.Country = "Test Country";
            defuser.Mail = "test@test.ru";
            defuser.Login = "test";
            defuser.Password = "test1234";
            defuser.UserType = 2;

            // equal => correct work
            CheckEqualUser(u, defuser);
        }

        [Fact]
        public void TestGetUserTracks()
        {
            // bl method
            int Id = 1;
            List<Track> tracks = this.facade.GetTracksByUser(Id);

            // expected result
            List<Track> deftracks = new List<Track>();

            Track t1 = new Track();
            t1.Id = 123;
            t1.Name = "User Track 1";
            t1.AuthorId = Id;
            t1.Duration = null;
            t1.DateOfCreation = DateTime.Today;
            t1.Description = "user track";
            t1.MidiFile = null;

            Track t2 = new Track();
            t2.Id = 321;
            t2.Name = "User Track 2";
            t2.AuthorId = Id;
            t2.Duration = null;
            t2.DateOfCreation = DateTime.Today;
            t2.Description = "user track";
            t2.MidiFile = null;

            deftracks.Append(t1);
            deftracks.Append(t2);

            // compare
            for (int i = 0; i < tracks.Count; i++)
            {
                CheckEqualTrack(deftracks[i], tracks[i]);
            }
        }

        [Fact]
        public void TestGetTracks()
        {
            // bl method
            List<Track> tracks = this.facade.GetAllTracks();

            // expected result
            List<Track> deftracks = new List<Track>();

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

            deftracks.Append(t1);
            deftracks.Append(t2);
            deftracks.Append(t3);

            // compare
            for (int i = 0; i < tracks.Count; i++)
            {
                CheckEqualTrack(deftracks[i], tracks[i]);
            }
        }

        [Fact]
        public void TestGetUsersPlaylist()
        {
            // bl method 
            int Id = 1;
            List<Playlist> playlists = this.facade.GetUserPlaylists(Id);

            // expected result
            List<Playlist> defplaylists = new List<Playlist>();

            List<Track> tracks1 = new List<Track>();

            Track t1 = new Track();
            t1.Id = 123;
            t1.Name = "User Track 1";
            t1.AuthorId = Id;
            t1.Duration = null;
            t1.DateOfCreation = DateTime.Today;
            t1.Description = "user track";
            t1.MidiFile = null;

            Track t2 = new Track();
            t2.Id = 321;
            t2.Name = "User Track 2";
            t2.AuthorId = Id;
            t2.Duration = null;
            t2.DateOfCreation = DateTime.Today;
            t2.Description = "user track";
            t2.MidiFile = null;

            tracks1.Append(t1);
            tracks1.Append(t2);

            Playlist p1 = new Playlist();
            p1.Id = 123;
            p1.Name = "Test Playlist 1";
            p1.AuthorId = Id;
            p1.DateOfCreation = DateTime.Today;
            p1.Description = "just a test playlist";
            p1.tracks = tracks1;

            defplaylists.Append(p1);

            List<Track> tracks2 = new List<Track>();

            Track t3 = new Track();
            t3.Id = 213;
            t3.Name = "User Track 3";
            t3.AuthorId = Id;
            t3.Duration = null;
            t3.DateOfCreation = DateTime.Today;
            t3.Description = "user track";
            t3.MidiFile = null;

            tracks2.Append(t3);

            Playlist p2 = new Playlist();
            p2.Id = 321;
            p2.Name = "Test Playlist 2";
            p2.AuthorId = Id;
            p2.DateOfCreation = DateTime.Today;
            p2.Description = "just a test playlist";
            p2.tracks = tracks1;

            defplaylists.Append(p2);

            // compare
            for (int i = 0; i < defplaylists.Count(); i++)
            {
                CheckEqualPlaylist(defplaylists[i], playlists[i]);
            }
        }
    }
}