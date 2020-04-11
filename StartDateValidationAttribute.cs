using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SpaceAgenciesDatabaseApp
{
    public class DateValidationAttribute : ValidationAttribute
    {
        DateTime date;
        public DateValidationAttribute(string date_)
        {
            date = DateTime.Parse(date_);
        }
        public override bool IsValid(object value)
        {
            if(value != null)
            {
                DateTime input = DateTime.Parse(value.ToString());
                if(input <= DateTime.Now && input >= date)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
