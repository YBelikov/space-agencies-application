using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class States
    {
        public States()
        {
            ProgramsStates = new HashSet<ProgramsStates>();
        }

        public int Id { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }

        public virtual ICollection<ProgramsStates> ProgramsStates { get; set; }
    }
}
