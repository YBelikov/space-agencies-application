using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Duty { get; set; }
        public int CrewId { get; set; }
        public int CountryId { get; set; }

        public virtual Countires Country { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
