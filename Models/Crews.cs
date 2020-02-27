using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Crews
    {
        public Crews()
        {
            CrewsAstronauts = new HashSet<CrewsAstronauts>();
        }

        public int Id { get; set; }

        [Display(Name = "Number of crew members")]
        public int NumberOfMembers { get; set; }
        public int MissionId { get; set; }

        public virtual Missions Mission { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
