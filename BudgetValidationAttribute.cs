using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SpaceAgenciesDatabaseApp
{
    public class BudgetValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Double.Parse(value.ToString()) <= 0) return false;
            else return true;
        }
    }
}
