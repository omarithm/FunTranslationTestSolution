using FT_Api.Data;
using FT_Api.Models;
using FT_ApiLibrary.DataAccess;
using FT_ApiLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FT_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _userData;
        private readonly ILogger _logger;

        public UserController(ApplicationDbContext context,
                              UserManager<IdentityUser> userManager,
                              IUserData userData,
                              ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
            _logger = logger;
        }



        [HttpGet]
        public UserModel GetUserId()
        {
            //Get current logged in userId
            string? Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userData.GetUserById(Id);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUser()
        {
            List<ApplicationUserModel> output = new();

            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel u = new()
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name);

                output.Add(u);
            }
            return output;
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);
            return roles;
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            //To capture more information about the user who did this AddRole
            //and To log it
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); //.NET Framework: RequestContext.Principal.Identity.GetUserId();

            //To protect user Id from being exposed to firewall or other apps
            //pass it through a model
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            //NOW: Log the user information before doing the actual AddRole
            //because if the app crashed, at least we have the information
            //about who tried to do so!!
            //Follow the pattern below, and do NOT put $ at the beginning
            _logger.LogInformation("Admin {Admin} is trying to add user {User} to role {Role}...",
                loggedInUserId, user.Id, pairing.RoleName);

            //Add Role
            await _userManager.AddToRoleAsync(user, pairing.RoleName);

            //Log the result
            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
                loggedInUserId, user.Id, pairing.RoleName);
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pairing)
        {
            //To capture more information about the user who did this AddRole
            //and To log it
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); //.NET Framework: RequestContext.Principal.Identity.GetUserId();

            //To protect user Id from being exposed to firewall or other apps
            //pass it through a model
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            //NOW: Log the user information before doing the actual RemoveRole
            //because if the app crashed, at least we have the information
            //about who tried to do so!!
            //Follow the pattern below, and do NOT put $ at the beginning
            _logger.LogInformation("Admin {Admin} is trying to remove user {User} from role {Role}",
                loggedInUserId, user.Id, pairing.RoleName);

            //Remove the role
            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);

            //Log the result
            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
                loggedInUserId, user.Id, pairing.RoleName);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task Register(CreateUserModel userToCreate)
        {
            //Create the account
            IdentityUser user = new()
            {
                UserName = userToCreate.UserName
                ,Email = userToCreate.Email
                //, EmailConfirmed = true
                
            };

            //Create the user 
            // the password must meets all requirement for the result to be succeeded
            await _userManager.CreateAsync(user, userToCreate.Password);

            //Add Role
            var CreatedUser = await _userManager.FindByEmailAsync(userToCreate.Email);
            if (CreatedUser != null)
            {
                await _userManager.AddToRoleAsync(CreatedUser, userToCreate.RoleName);
                
                //Save the username to the FunTranslationDB with the other user info
                UserModel userModel = new()
                {
                    Id = CreatedUser.Id
                    ,FirstName = userToCreate.FirstName
                    ,LastName = userToCreate.LastName
                    ,EmailAddress = userToCreate.Email
                };

                _userData.SaveUser(userModel);
            }
        }
    }
}
