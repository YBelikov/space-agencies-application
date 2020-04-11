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
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid country name input")]
        public string CountryName { get; set; }
        [BudgetValidation(ErrorMessage = "GDP can't be less or equal to zero")]
        public decimal Gdp { get; set; }

        [BudgetValidation(ErrorMessage = "Population can't be less or equal to zero")]
        public double Population { get; set; }

        public virtual ICollection<Astronauts> Astronauts { get; set; }
        public virtual ICollection<SpaceAgencies> SpaceAgencies { get; set; }
    }
}
