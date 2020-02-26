using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class CrewsAstronauts
    {
        public int Id { get; set; }
        public int CrewId { get; set; }
        public int AstronautId { get; set; }

        public virtual Astronauts Astronaut { get; set; }
        public virtual Crews Crew { get; set; }
    }
}
