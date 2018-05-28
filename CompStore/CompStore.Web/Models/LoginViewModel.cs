using System.ComponentModel.DataAnnotations;

namespace CompStore.Web.Models
{
    public class LoginViewModel
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}