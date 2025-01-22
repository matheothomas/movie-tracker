using Filmotopia.Components;
using Filmotopia.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Filmotopia;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddScoped<FilmService>();
        builder.Services.AddScoped<FavoriteService>();
        builder.Services.AddScoped<OMDBService>();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
        builder.Services.AddHttpClient();
        builder.Services.AddAuthenticationCore();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
