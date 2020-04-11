using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAgenciesDatabaseApp
{
    public partial class SpaceAgencies
    {
        public SpaceAgencies()
        {
            Administrators = new HashSet<Administrators>();
            AgenciesPrograms = new HashSet<AgenciesPrograms>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Field shouldn't be empty")]
        [Display(Name = "Agency name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name of agency can contain only letters")]
        public string Name { get; set; }

        [Display(Name = "Establishment date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("04.10.1957", ErrorMessage = "Date of Establishment should be less than current date and greater than first agency est. date")]
        public DateTime DateOfEstablishment { get; set; }
        
        [BudgetValidation(ErrorMessage ="Budget can't be less or equal to zero")]
        public double Budget { get; set; }

        [Display(Name = "Headquarters country")]
        public int HeadquarterCountryId { get; set; }

        
        public virtual Countires HeadquarterCountry { get; set; }
        public virtual ICollection<Administrators> Administrators { get; set; }
        public virtual ICollection<AgenciesPrograms> AgenciesPrograms { get; set; }
    }
}
