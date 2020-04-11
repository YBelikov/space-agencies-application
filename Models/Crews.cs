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

        [BudgetValidation(ErrorMessage = "Number of crew members can't be less or equal to zero")]
        public int NumberOfMembers { get; set; }
        [Display(Name = "Mission")]
        public int MissionId { get; set; }

        public virtual Missions Mission { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
