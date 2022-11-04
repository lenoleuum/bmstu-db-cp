using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_cp.data_access.EntityStructures
{
    public partial class Playlists
    {
        public Playlists()
        {
            Tp = new HashSet<Tp>();
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? DateOfCreation { get; set; }
        public virtual Users Author { get; set; }
        public virtual ICollection<Tp> Tp { get; set; }
    }
}
