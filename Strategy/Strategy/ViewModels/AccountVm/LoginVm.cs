using System.ComponentModel.DataAnnotations;

namespace Strategy.ViewModels.AccountVm
{
    public class LoginVm
    {
        [Required, MinLength(3), MaxLength(35)]
        public string UserNameOrEmile { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; } = false;
    }
}
