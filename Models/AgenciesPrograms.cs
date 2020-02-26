using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class AgenciesPrograms
    {
        public int Id { get; set; }
        public int SpaceAgencyId { get; set; }
        public int SpaceProgramId { get; set; }

        public virtual SpaceAgencies SpaceAgency { get; set; }
        public virtual SpacePrograms SpaceProgram { get; set; }
    }
}
