using Xunit;
using System;
using System.Linq;
using db_cp;

using db_cp.data_access.PSQLRepository;
using db_cp.data_access.EntityStructures;
using db_cp.data_access;

namespace DAUnitTests
{
    public class UnitTestTrack
    {
        PSQLRepositoryTrack rep_track = new PSQLRepositoryTrack();

        private void CheckEqual(Tracks t1, Tracks t2)
        {
            Assert.Equal(t1.Name, t2.Name);
            Assert.Equal(t1.Id, t2.Id);
            Assert.Equal(t1.Description, t2.Description);
            Assert.Equal(t1.DateOfCreation, t2.DateOfCreation);
            Assert.Equal(t1.Duration, t2.Duration);
            Assert.Equal(t1.MidiFile, t2.MidiFile);
        }

        [Fact]
        public void TestInsertTrack()
        {
            Tracks track = new Tracks();
            track.Name = "Test";
            track.Id = 1000;
            track.DateOfCreation = DateTime.Today;
            track.Description = "this is a test track";
            track.Duration = TimeSpan.Zero;
            track.MidiFile = "C:\\midi\\test.mid";

            rep_track.InsertTrack(track);

            using (PSQLContext db = new PSQLContext())
            {
                var t = db.Tracks.Find(track.Id);

                CheckEqual(track, t);
            }
        }

        [Fact]
        public void TestDeleteTrack()
        {
            Tracks track = new Tracks();
            track.Name = "Test";
            track.DateOfCreation = DateTime.Today;
            track.Description = "this is a test track";
            track.Duration = TimeSpan.Zero;
            track.MidiFile = "C:\\midi\\test.mid";

            using (PSQLContext db = new PSQLContext())
            {
                track.Id = 1000;

                rep_track.DeleteTrack(track);

                if (db.Tracks.Find(track.Id) != null)
                    throw new Exception("problem with delete_track!");
            }
        }

        [Fact]
        public void TestInsertTrackToUser()
        {
            using (PSQLContext db = new PSQLContext())
            {
                Tu tu = new Tu();
                tu.Id = 1000;
                tu.IdUser = 1;
                tu.IdTrack = db.Tracks.Count();

                rep_track.InsertTrackToUser(tu);

                if (db.Tu.Find(tu.Id) == null)
                    throw new Exception("problem with insert_track_user!");
            }
        }

        [Fact]
        public void TestDeleteTrackFromUser()
        {
            using (PSQLContext db = new PSQLContext())
            {
                Tu tu = new Tu();
                tu.IdTrack = db.Tracks.Count();
                tu.IdUser = 1;
                tu.Id = 1000;

                rep_track.DeleteTrackFromUser(tu); 

                if (db.Tu.Find(tu.Id) != null)
                    throw new Exception("problem with delete_track_from_user!");
            }
        }
    }
}
