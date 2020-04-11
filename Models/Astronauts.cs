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

        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid name input")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid name input")]
        public string Surname { get; set; }

        [Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("01.12.1910", ErrorMessage = "Correct your input for birth date")]
        public DateTime BirthDate { get; set; }

        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid duty input")]
        public string Duty { get; set; }
        public int CrewId { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public virtual Countires Country { get; set; }
        public virtual ICollection<CrewsAstronauts> CrewsAstronauts { get; set; }
    }
}
