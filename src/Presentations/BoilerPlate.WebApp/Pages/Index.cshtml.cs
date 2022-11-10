using BoilerPlate.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoilerPlate.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty] public LoginViewModel Input { get; set; }


    public string ReturnUrl { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public Task OnGetAsync(string returnUrl = null)
    {
        return Task.CompletedTask;
    }

    public IActionResult OnPostLoginAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        _logger.LogInformation("{Username}", Input.Username);
        _logger.LogInformation("{Password}", Input.Password);

        return Page();
    }
}