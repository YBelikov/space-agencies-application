using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Crews
    {
        public Crews()
        {
            CrewsAstronauts = new HashSet<CrewsAstronauts>();
        }

        public int Id { get; set; }
        public int NumberOfMembers { get; set; }
        public int MissionId { get; set; }

        public virtual Missions Mission { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
