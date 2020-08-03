using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ITaras.Models;
using ITaras.Models.JsonReturnViewModels;
using ITaras.Models.ViewModels;

namespace ITaras.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolesManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> rolesManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesManager = rolesManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {                   
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {            
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            UserViewModel viewModel = new UserViewModel();            
            return PartialView(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]       
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            JsonModelReturnViewUser json = new JsonModelReturnViewUser();
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime dateTime;
                    DateTime? BirthDateConverted = DateTime.MinValue;
                    if (DateTime.TryParse(model.BirthDate, out dateTime))
                    {
                        BirthDateConverted = dateTime;
                    }                                       

                    var user = new ApplicationUser
                    {
                        UserName = "Usr" + Helper.GenerateRandom(10),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = BirthDateConverted,
                        PhoneNumber = model.Phone,
                        FavoriteColors = model.FavoriteColors,
                        FavoriteDrinks = model.FavoriteDrinks
                    };
                    
                    var result = await _userManager.CreateAsync(user, "Qwerty123+");

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }

                    return Json(json);
                }
                return null;
            }
            catch(Exception ex)
            {
                json.messages = ex.Message;
                return Json(json);
            }
        }


        [Authorize(Roles = "admin")]           
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Seed()
        {
            JsonModelReturnViewUser json = new JsonModelReturnViewUser();
            try
            {                
                await RoleInitializer.InitializeAsync(_userManager, _rolesManager);
            }
            catch (Exception ex)
            {
                json.messages = ex.Message;
                return Json(json);
            }
            return View();
        }

        [HttpGet]
        public ResultList<UserViewModel> Get(QueryOptions queryOptions)
        {  
            IQueryable<ApplicationUser> AppUsers = _userManager.Users;

            //Colors
            if (queryOptions.FavColors != "All" && queryOptions.FavColors != null && queryOptions.FavColors != "undefined")
            {
                AppUsers = from u in AppUsers
                           where Helper.ConvertColors(u.FavoriteColors.ToString().Trim()).ToLower().Contains(queryOptions.FavColors.ToLower()) 
                           select u;
            }

            //Drinks
            if (queryOptions.FavDrinks  != "All" && queryOptions.FavDrinks != null && queryOptions.FavDrinks != "undefined")
            {
                AppUsers = from u in AppUsers
                           where Helper.ConvertDrinks(u.FavoriteDrinks.ToString().Trim()).ToLower().Contains(queryOptions.FavDrinks.ToLower())
                           select u;
            }

            //Page
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            IQueryable<ApplicationUser> SelectedAppUsers = AppUsers.Skip(start).Take(queryOptions.PageSize);
            queryOptions.TotalPages = (int)Math.Ceiling((double)AppUsers.Count() / queryOptions.PageSize);

            List<UserViewModel> model = new List<UserViewModel>();

            foreach (ApplicationUser appUser in SelectedAppUsers.ToList())
            {
                UserViewModel user = new UserViewModel
                {
                    FirstName =  appUser.FirstName ?? "-",
                    LastName = appUser.LastName ?? "-",
                    BirthDate = Convert.ToDateTime(appUser.BirthDate).ToShortDateString() ?? "-",
                    Phone = appUser.PhoneNumber ?? "-",
                    FavoriteColors = Helper.ConvertColors(appUser.FavoriteColors) ?? "-",
                    FavoriteDrinks = Helper.ConvertDrinks(appUser.FavoriteDrinks) ?? "-"
                };

                model.Add(user);
            }
           
            return new ResultList<UserViewModel>
            {
                Results = model,
                QueryOptions = queryOptions
            };
        }
    }
}
