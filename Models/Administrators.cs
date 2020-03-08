using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Administrators { 
            
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int SpaceAgencyId { get; set; }

        public virtual SpaceAgencies SpaceAgency { get; set; }
    }
}
