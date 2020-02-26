using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Missions
    {
        public Missions()
        {
            Crews = new HashSet<Crews>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public bool IsRobotic { get; set; }
        public int ProgramId { get; set; }

        public virtual SpacePrograms Program { get; set; }
        public virtual ICollection<Crews> Crews { get; set; }
    }
}
