using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Administrators {

        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid name input")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "Invalid surname input")]
        public string Surname { get; set; }

        [Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        [DateValidation("31.12.1870", ErrorMessage = "Administrator couldn't be older than oldest human in the world or come us from future")]
        [MinAge(23)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Agency")]
        public int SpaceAgencyId { get; set; }

        [Display(Name = "Agency")]
        public virtual SpaceAgencies SpaceAgency { get; set; }
    }
}
