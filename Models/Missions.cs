using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpaceAgenciesDatabaseApp
{
    public partial class Missions
    {
        public Missions()
        {
            Crews = new HashSet<Crews>();
        }

        public int Id { get; set; }
        
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("08.06.1959", ErrorMessage = "Your mission is older than the oldest mission in history or come to us from future")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DateValidation("08.06.1959", ErrorMessage = "Your mission is older than the oldest mission in history or come to us from future")]
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        [Display(Name = "Is robotic")]
        public bool IsRobotic { get; set; }
      
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        public virtual SpacePrograms Program { get; set; }
        public virtual ICollection<Crews> Crews { get; set; }
    }
}
