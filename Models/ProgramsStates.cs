using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class ProgramsStates
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int ProgramId { get; set; }

        public virtual SpacePrograms Program { get; set; }
        public virtual States State { get; set; }
    }
}
