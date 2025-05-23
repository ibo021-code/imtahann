using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strategy.DAL;
using Strategy.ViewModels;


namespace Strategy.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Employees = _context.employees.ToList(),
            };

            return View(vm);
        }

       
    }
}
