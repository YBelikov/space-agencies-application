using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpaceAgenciesDatabaseApp.Models;

namespace SpaceAgenciesDatabaseApp
{
    public class CustomUserValidator : UserValidator<User>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError { Description = "Nickname can't contain 'admin' word" });
            }
            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError { Description = "Your email in spam base" });

            }
            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
