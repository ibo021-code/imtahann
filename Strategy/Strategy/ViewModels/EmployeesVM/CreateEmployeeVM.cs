using System.ComponentModel.DataAnnotations;

namespace Strategy.ViewModels.EmployeesVM
{


    public class CreateEmployeeVM
    {
        public int Id { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Link { get; set; }
        
    }
}






