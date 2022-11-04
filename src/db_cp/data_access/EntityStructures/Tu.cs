using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_cp.data_access.EntityStructures
{
    public partial class Tu
    {
        public int Id { get; set; }
        public int IdTrack { get; set; }
        public int IdUser { get; set; }

        public virtual Tracks Track { get; set; }
        public virtual Users User { get; set; }
    }
}
