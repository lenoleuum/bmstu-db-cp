using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_cp.data_access.EntityStructures
{
    public partial class Tp
    {
        public int Id { get; set; }
        public int IdTrack { get; set; }
        public int IdPlaylist { get; set; }

        public virtual Playlists IdPlaylistNavigation { get; set; }
        public virtual Tracks IdTrackNavigation { get; set; }
    }
}
