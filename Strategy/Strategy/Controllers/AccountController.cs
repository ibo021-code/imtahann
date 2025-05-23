using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strategy.Enums;
using Strategy.Models;
using Strategy.ViewModels.AccountVm;
using System.Threading.Tasks;

namespace Strategy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
           
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new AppUser
            {
                FullName = vm.FullName,
                UserName = vm.UserName,
                Email = vm.Email,
            };
            var result = await userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    
                }
                return View(vm);

            }
            await userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = await userManager.FindByNameAsync(vm.UserNameOrEmile)??
                                await userManager.FindByEmailAsync(vm.UserNameOrEmile);
            if (user == null)
            {
                ModelState.AddModelError("", "UsernameEmail or password  is incorrect");
                return View(vm);
            }
            var result  =  await signInManager.CheckPasswordSignInAsync(user, vm.Password, vm.IsRemember);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameEmail or password  is incorrect");
                return View(vm);
            }
            await signInManager.SignInAsync(user , vm.IsRemember);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task< IActionResult> CreateRole()
        {if(roleManager.Roles.Count() == 0)
            {
                foreach (var role in Enum.GetValues(typeof(UserRole)))
                {
                   await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                   
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
