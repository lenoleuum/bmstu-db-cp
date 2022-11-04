using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_cp.buisiness_logic.bl_structures
{
    public class Playlist
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string Description { get; set; }

        public List<Track> tracks;
        public Playlist(int _Id, int _AuthorId, string _Name, string _Description, DateTime? _Date, List<Track> tracks = null)
        {
            this.Id = _Id;
            this.AuthorId = _AuthorId;
            this.Name = _Name;
            this.Description = _Description;
            this.DateOfCreation = _Date;
            this.tracks = tracks;
        }
        public Playlist() { }
    }
}
