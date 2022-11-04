using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_cp.data_access.EntityStructures
{
    public partial class Tracks
    {
        public Tracks()
        {
            Tp = new HashSet<Tp>();
            Tu = new HashSet<Tu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string Description { get; set; }
        public string MidiFile { get; set; }

        public virtual ICollection<Tp> Tp { get; set; }
        public virtual ICollection<Tu> Tu { get; set; }
    }
}
