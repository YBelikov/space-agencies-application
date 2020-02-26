using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Astronauts
    {
        public Astronauts()
        {
            CrewsAstronauts = new HashSet<CrewsAstronauts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Duty { get; set; }
        public int CrewId { get; set; }
        public int CountryId { get; set; }

        public virtual Countires Country { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
