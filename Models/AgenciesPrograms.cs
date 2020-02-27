using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class AgenciesPrograms
    {
        public int Id { get; set; }
        public int SpaceAgencyId { get; set; }
        public int SpaceProgramId { get; set; }
        [Display(Name = "Agency")]
        public virtual SpaceAgencies SpaceAgency { get; set; }
        [Display(Name = "Program")]
        public virtual SpacePrograms SpaceProgram { get; set; }
    }
}
