using Xunit;
using System;
using System.Linq;
using db_cp;

using db_cp.data_access.PSQLRepository;
using db_cp.data_access.EntityStructures;
using db_cp.data_access;

namespace DAUnitTests
{
    public class UnitTestPlaylist
    {
        PSQLRepositoryPlaylist rep_playlist = new PSQLRepositoryPlaylist();

        [Fact]
        public void TestInsertPlaylist()
        {
            Playlists playlist = new Playlists();
            playlist.Id = 1000;
            playlist.Name = "Test Playlist";
            playlist.AuthorId = 1;
            playlist.Description = "this is a test playlist";
            playlist.DateOfCreation = DateTime.Today;

            rep_playlist.InsertPlaylist(playlist);

            using (PSQLContext db = new PSQLContext())
            {
                var pl = db.Playlists.Find(playlist.Id);

                Assert.Equal(playlist.Id, pl.Id);
                Assert.Equal(playlist.Name, pl.Name);
                Assert.Equal(playlist.AuthorId, pl.AuthorId);
                Assert.Equal(playlist.Description, pl.Description);
                Assert.Equal(playlist.DateOfCreation, pl.DateOfCreation);
            }
        }

        [Fact]
        public void TestDeletePlaylist()
        {
            using (PSQLContext db = new PSQLContext())
            {
                int Id = 1000;
                rep_playlist.DeletePlaylist(Id);

                if (db.Playlists.Find(Id) != null)
                    throw new Exception("problem with delete_playlist");
            }
        }

        [Fact]
        public void TestInsertTrackToPlaylist()
        {
            using (PSQLContext db = new PSQLContext())
            {
                Tp tp = new Tp();
                tp.Id = 1000;
                tp.IdPlaylist = db.Playlists.Count();
                tp.IdTrack = db.Tracks.Count();

                rep_playlist.InsertTrackToPLaylist(tp);

                var res = db.Tp.Find(tp.Id);

                Assert.Equal(tp.Id, res.Id);
                Assert.Equal(tp.IdTrack, res.IdTrack);
                Assert.Equal(tp.IdPlaylist, res.IdPlaylist);
            }
        }

        [Fact]
        public void TestDeleteTrackFromPlaylist()
        {
            using (PSQLContext db = new PSQLContext())
            {
                Tracks t = new Tracks();
                t.Id = db.Tracks.Count();

                Playlists p = new Playlists();
                p.Id = db.Playlists.Count();

                Tp tp = new Tp();
                tp.Id = 1000;

                rep_playlist.DeleteTrackFromPlaylist(t, p);

                if (db.Tp.Find(tp.Id) != null)
                    throw new Exception("problem with delete_track_from_playlist");
            }
        }
    }
}
