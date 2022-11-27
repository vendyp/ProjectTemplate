using System.Net;
using BoilerPlate.App.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BoilerPlate.App.Commons;

public class ApplicationRouteView : RouteView
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Inject] public IAccountService AccountService { get; set; } = null!;

    protected override void Render(RenderTreeBuilder builder)
    {
        var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
        if (authorize && AccountService.User == null)
        {
            var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
            NavigationManager.NavigateTo($"identity/login?returnUrl={returnUrl}");
        }
        else
        {
            base.Render(builder);
        }
    }
}