using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;
using db_cp.data_access.EntityStructures;

namespace db_cp.data_access
{
    internal class Converter
    {
        public User ConvertUserDBToBL(Users u)
        {
            User res = new User();
            res.Id = u.Id;
            res.Name = u.Name;
            res.Country = u.Country;
            res.Mail = u.Mail;
            res.DateOfBirth = u.DateOfBirth;
            res.Login = u.Login;
            res.Password = u.Password;
            res.UserType = u.UserType;

            return res;
        }

        public Users ConvertUserBLToDB(User u)
        {
            Users res = new Users();
            res.Id = u.Id;
            res.Name = u.Name;
            res.Country = u.Country;
            res.Mail = u.Mail;
            res.DateOfBirth = u.DateOfBirth;
            res.Login = u.Login;
            res.Password = u.Password;
            res.UserType = u.UserType;

            return res;
        }
        
        public Track ConvertTrackDBToBL(Tracks t)
        {
            Track res = new Track();
            res.Id = t.Id;
            res.Name = t.Name;
            res.Duration = t.Duration;
            res.DateOfCreation = t.DateOfCreation;
            res.Description = t.Description;
            res.MidiFile = t.MidiFile;

            return res;
        }
        public Tracks ConvertTrackBLToDB(Track t)
        {
            Tracks res = new Tracks();
            res.Id = t.Id;
            res.Name = t.Name;
            res.Duration = t.Duration;
            res.DateOfCreation = t.DateOfCreation;
            res.Description = t.Description;
            res.MidiFile = t.MidiFile;

            return res;
        }

        public List<Track> ConvertTrackListDBToBL(List<Tracks> tracks)
        {
            List<Track> res = new List<Track>();
            
            foreach (Tracks t in tracks)
            {
                res.Add(ConvertTrackDBToBL(t));
            }

            return res;
        }
        public Playlists ConvertPlaylistBLToDB(Playlist p)
        {
            Playlists res = new Playlists();
            res.Id = p.Id;
            res.AuthorId = p.AuthorId;
            res.Name = p.Name;
            res.Description = p.Description;
            //DateTime dt = new DateTime();
            res.DateOfCreation = p.DateOfCreation;

            Console.WriteLine(res.DateOfCreation);

            return res;
        }
        public Playlist ConvertPlaylistDBToBL(Playlists p)
        {
            Playlist res = new Playlist();
            res.Id = p.Id;
            res.AuthorId = p.AuthorId;
            res.Name = p.Name;
            res.Description= p.Description;
            res.DateOfCreation = p.DateOfCreation;

            return res;
        }
    }
}
