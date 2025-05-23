using System.ComponentModel.DataAnnotations;

namespace Strategy.ViewModels.EmployeesVM
{
    public class UpdateEmployeeVM
    {
         
        [MinLength(3, ErrorMessage = "min 3 herf olmalidir")]
        [MaxLength(15, ErrorMessage = "max 15 herf olmalidir")]
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }


    }

}
