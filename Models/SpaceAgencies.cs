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
        public string Name { get; set; }

        [Display(Name = "Establishment date")]
        public DateTime DateOfEstablishment { get; set; }
        
        public double Budget { get; set; }
        public int HeadquarterCountryId { get; set; }
        [Display(Name = "Headquarters country")]
        public virtual Countires HeadquarterCountry { get; set; }
        public virtual ICollection<Administrators> Administrators { get; set; }
        public virtual ICollection<AgenciesPrograms> AgenciesPrograms { get; set; }
    }
}
