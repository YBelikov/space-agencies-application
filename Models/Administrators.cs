using System;
using System.Collections.Generic;

namespace SpaceAgenciesDatabaseApp
{
    public partial class Administrators
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int SpaceAgencyId { get; set; }

        public virtual SpaceAgencies SpaceAgency { get; set; }
    }
}
