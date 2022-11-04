using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_cp.data_access.EntityStructures
{
    public partial class Users
    {
        public Users()
        {
            Playlists = new HashSet<Playlists>();
            Tu = new HashSet<Tu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Mail { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }

        public virtual ICollection<Playlists> Playlists { get; set; }
        public virtual ICollection<Tu> Tu { get; set; }
    }
}
