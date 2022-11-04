using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_cp.buisiness_logic.bl_structures
{
    public class Track
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string Description { get; set; }
        public string MidiFile { get; set; }

        public Track() { }
        public Track(int _Id, int _AuthorId, string _Name, TimeSpan? _Duration,
            DateTime? _DateOfCreation, string _Description, string _MidiFile)
        {
            this.Id = _Id;
            this.AuthorId = _AuthorId;
            this.Name = _Name;
            this.Duration = _Duration;
            this.DateOfCreation = _DateOfCreation;
            this.Description = _Description;
            this.MidiFile = _MidiFile;
        }
    }
}
