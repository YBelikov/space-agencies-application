using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class States
    {
        public States()
        {
            ProgramsStates = new HashSet<ProgramsStates>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<ProgramsStates> ProgramsStates { get; set; }
    }
}
