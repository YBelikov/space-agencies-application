using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class SpacePrograms
    {
        public SpacePrograms()
        {
            AgenciesPrograms = new HashSet<AgenciesPrograms>();
            Missions = new HashSet<Missions>();
            ProgramsStates = new HashSet<ProgramsStates>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Target { get; set; }

        public virtual ICollection<AgenciesPrograms> AgenciesPrograms { get; set; }
        public virtual ICollection<Missions> Missions { get; set; }
        public virtual ICollection<ProgramsStates> ProgramsStates { get; set; }
    }
}
