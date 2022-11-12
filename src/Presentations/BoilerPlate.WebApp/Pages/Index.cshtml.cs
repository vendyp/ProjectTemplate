using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BoilerPlate.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoilerPlate.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _client;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient("Identity");
    }

    [BindProperty] public LoginViewModel Input { get; set; }


    public string ReturnUrl { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public Task OnGetAsync(string returnUrl = null)
    {
        return Task.CompletedTask;
    }

    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        using (var httpClient = new HttpClient())
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri("http://localhost:5000/api/identity/sign-in");
            httpRequestMessage.Content =
                new StringContent(JsonSerializer.Serialize(new
                {
                    clientId = Guid.NewGuid().ToString(),
                    username = "admin",
                    password = "Qwerty@1234"
                }), Encoding.UTF8, "application/json");

            var results = await httpClient.SendAsync(httpRequestMessage);
        }

        return Page();
    }
}