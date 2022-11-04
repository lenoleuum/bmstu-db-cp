using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access.MSSQLRepository
{
    public class MSSQLRepositoryPlaylist : IRepositoryPlaylist
    {
        private MSSQLContext db;

        public MSSQLRepositoryPlaylist()
        {
            this.db = new MSSQLContext();
        }

        public MSSQLRepositoryPlaylist(string ConString)
        {
            this.db = new MSSQLContext(ConString);
        }

        public List<Playlists> GetUserPlaylists(int uid)
        {
            var res = from p in this.db.Playlists
                      where p.AuthorId == uid
                      select p;

            return res.ToList();
        }
        public int GetNewId()
        {
            try
            {
                return this.db.Playlists.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {

                return 1;
            }
        }

        public void InsertPlaylist(Playlists playlist)
        {
            this.db.Playlists.Add(playlist);
            this.db.SaveChanges();
        }

        public void DeletePlaylist(Playlists playlist)
        {
            this.db.Playlists.Remove(playlist);
            this.db.SaveChanges();
        }

        public void DeletePlaylist(int Id)
        {
            var pl = this.db.Playlists.Find(Id);
            this.db.Playlists.Remove(pl);
            this.db.SaveChanges();
        }

        public int GetNewIdTp()
        {
            try
            {
                return this.db.Tp.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public void InsertTrackToPLaylist(Tp tp)
        {
            this.db.Tp.Add(tp);
            this.db.SaveChanges();
        }

        public void DeleteTrackFromPlaylist(Tracks t, Playlists p)
        {
            try
            {
                Tp tp = this.db.Tp.Where(a => a.IdTrack == t.Id && a.IdPlaylist == p.Id).FirstOrDefault();

                this.db.Tp.Remove(tp);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex.Message);
            }
        }
        public void DeleteTrackFromPlaylist(Tp tp)
        {
            this.db.Tp.Remove(tp);
            this.db.SaveChanges();
        }
    }
}
