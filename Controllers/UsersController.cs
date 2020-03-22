using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpaceAgenciesDatabaseApp.Models;
using Microsoft.AspNetCore.Identity;

namespace SpaceAgenciesDatabaseApp.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());
        public IActionResult Create() => View();

    }
}