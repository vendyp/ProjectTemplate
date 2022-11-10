using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.WebApp.Models;

public class LoginViewModel
{
    [Required] [MaxLength(100)] public string Username { get; set; }

    [Required] [MaxLength(100)] public string Password { get; set; }
}