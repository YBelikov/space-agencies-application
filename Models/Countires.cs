using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Countires
    {
        public Countires()
        {
            Astronauts = new HashSet<Astronauts>();
            SpaceAgencies = new HashSet<SpaceAgencies>();
        }

        public int Id { get; set; }
        [Display(Name = "Name")]
        public string CountryName { get; set; }
        public decimal Gdp { get; set; }
        public double Population { get; set; }

        public virtual ICollection<Astronauts> Astronauts { get; set; }
        public virtual ICollection<SpaceAgencies> SpaceAgencies { get; set; }
    }
}
