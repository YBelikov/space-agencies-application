using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAgenciesDatabaseApp
{
    public partial class SpacePrograms : IValidatableObject
    {
        public SpacePrograms()
        {
            AgenciesPrograms = new HashSet<AgenciesPrograms>();
            Missions = new HashSet<Missions>();
            ProgramsStates = new HashSet<ProgramsStates>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        

        [Display(Name = "Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("08.06.1959", ErrorMessage = "Your program is older than the oldest program in history or come to us from future")]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("08.06.1959", ErrorMessage = "Your program is older than the oldest program in history or come to us from future")]
        public DateTime? EndDate { get; set; }
        public string Target { get; set; }

        public virtual ICollection<AgenciesPrograms> AgenciesPrograms { get; set; }
        public virtual ICollection<Missions> Missions { get; set; }
        public virtual ICollection<ProgramsStates> ProgramsStates { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (StartDate > EndDate) errors.Add(new ValidationResult("Program can't start after own end"));
            return errors;
        }

    }
}
