using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access.MSSQLRepository
{
    public class MSSQLRepositoryTrack : IRepositoryTrack
    {
        private MSSQLContext db;

        public MSSQLRepositoryTrack()
        {
            this.db = new MSSQLContext();
        }
        public MSSQLRepositoryTrack(string ConString)
        {
            this.db = new MSSQLContext(ConString);
        }
        public List<Tracks> GetAllTracks()
        {
            return this.db.Tracks.ToList();
        }
        public List<Tracks> GetTracksByUser(int IdUser)
        {
            var res = from t in this.db.Tracks
                      join tu in this.db.Tu
                      on t.Id equals tu.IdTrack
                      where tu.IdUser == IdUser
                      select t;

            return res.ToList();
        }

        public void InsertTrack(Tracks track)
        {
            this.db.Tracks.Add(track);
            this.db.SaveChanges();
        }
        public int GetNewId()
        {
            try
            {
                return this.db.Tracks.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public bool CheckTrackName(string Name)
        {
            var res = this.db.Tracks.Where(t => t.Name == Name).ToList();

            if (res == null)
                return true;
            else
                return false;
        }

        public List<Tracks> GetTracksByPlaylist(int PId)
        {
            var selection = from tp in this.db.Tp
                            join t in this.db.Tracks on tp.IdTrack equals t.Id
                            where tp.IdPlaylist == PId
                            select t;

            return selection.ToList();
        }

        public void InsertTrackToUser(Tu tu)
        {
            this.db.Tu.Add(tu);
            this.db.SaveChanges();
        }

        public int GetNewIdTu()
        {
            try
            {
                return this.db.Tu.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public void DeleteTrackFromUser(Tracks t, Users u)
        {
            Tu tu = this.db.Tu.Where(a => a.IdTrack == t.Id && a.IdUser == u.Id).FirstOrDefault();
            this.db.Tu.Remove(tu);
            this.db.SaveChanges();
        }
        public void DeleteTrackFromUser(Tu tu)
        {
            this.db.Tu.Remove(tu);
            this.db.SaveChanges();
        }

        public void DeleteTrack(Tracks track)
        {
            this.db.Tracks.Remove(track); // error
            this.db.SaveChanges();
        }
        public void DeleteTrack(int Id)
        {
            var t = this.db.Tracks.Find(Id);
            this.db.Tracks.Remove(t);
            this.db.SaveChanges();
        }
        public void UpdateTrack(Tracks track)
        {
            var t = this.db.Tracks.Find(track.Id);
            t.Name = track.Name;
            t.Description = track.Description;
            this.db.SaveChanges();
        }
    }
}
