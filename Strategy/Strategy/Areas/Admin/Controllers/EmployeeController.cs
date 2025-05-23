using System.Composition.Convention;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strategy.DAL;
using Strategy.Models;
using Strategy.Utilities.Enums;
using Strategy.Utilities.Validators;
using Strategy.ViewModels.EmployeesVM;
using Strategy.ViewModels.EmployeeVM;

namespace Strategy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.employees.ToListAsync();


            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (employeeVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo is required");
                return View();
            }
            if (!employeeVM.Photo.ValidateType("image"))
            {
                ModelState.AddModelError("photo", "photo type is not valid");
            }
            if (!employeeVM.Photo.ValidateSize(FileType.MB, 2))
            {
                ModelState.AddModelError("Photo", "Photo size is not valid");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            string fileName = await employeeVM.Photo.CreateFileAsync(_env.WebRootPath, "imgs", "team");
            Employee employee = new Employee
            {
                Image = fileName,
                Name = employeeVM.Name,
                Position = employeeVM.Position,
                Description = employeeVM.Description,
                Link = employeeVM.Link
            };
            await _context.AddAsync(employee);        
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            Employee? employee = await _context.employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            CreateEmployeeVM employeeVM = new CreateEmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Description = employee.Description,
                Link = employee.Link
            };

            return View(employeeVM);


        }
        [HttpPost]

        public async Task<IActionResult> Update(int id, CreateEmployeeVM employeeVM)
        {
            Employee employee = await _context.employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employeeVM.Photo != null)
            {
                employee.Image.Delete(_env.WebRootPath, "imgs", "team");  
                if (!employeeVM.Photo.ValidateType("image"))
                    {
                    ModelState.AddModelError("photo", "photo type is not valid");

                }
                if (!employeeVM.Photo.ValidateSize(FileType.MB, 2))
                {
                    ModelState.AddModelError("Photo", "Photo size is not valid");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                string fileName = await employeeVM.Photo.CreateFileAsync(_env.WebRootPath, "imgs", "team");
                employee.Image = fileName;
            }
            if(!ModelState.IsValid)
            {
                CreateEmployeeVM vm = new CreateEmployeeVM
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Position = employee.Position,
                    Description = employee.Description,
                    Link = employee.Link
                };
                return View(vm);
            }
            employee.Name = employeeVM.Name;
            employee.Position = employeeVM.Position;
            employee.Description = employeeVM.Description;
            employee.Link = employeeVM.Link;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));




            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            Employee? employee = await _context.employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "imgs", "team", employee.Image);
            if (Path.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction(nameof(Index));



        }
    }
}

