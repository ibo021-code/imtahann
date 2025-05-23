using System.ComponentModel.DataAnnotations;

namespace Strategy.ViewModels.AccountVm
{
    public class RegisterVm
    {
        [Required , MinLength(3), MaxLength(25)]
        public string FullName { get; set; }

        [Required, MinLength(3), MaxLength(25)]
        public string UserName { get; set; }

        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password) ,Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
