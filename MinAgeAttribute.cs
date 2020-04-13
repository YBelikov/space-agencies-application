using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private int _Limit;
        public MinAgeAttribute(int Limit)
        { 
            this._Limit = Limit;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime bday = DateTime.Parse(value.ToString());
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age))
            {
                age--;
            }
            if (age < _Limit)
            {
                var result = new ValidationResult("Person is not old enough");
                return result;
            }


            return null;

        }
    }
}
