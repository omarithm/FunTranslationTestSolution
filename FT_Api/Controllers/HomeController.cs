using FT_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FT_Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            #region 
            //To add roles for first time when visit Privacy page BY ME
            //This is just for simplicity
            //uncomment the following and visit the Privacy page after running the app
            //Add main roles to the database

            string[] roles = { "Admin", "Manager", "Employee" };
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (roleExist == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //To add roles to user omar@sebakhi.com
            var user = await _userManager.FindByEmailAsync("omar@sebakhi.com");
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            #endregion

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}