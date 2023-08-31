using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.WebApp.Configurations.DependenciasInjection;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInjecaoDependencias(builder.Configuration);

#region Config Identity Core

builder.Services.AddIdentityCore<Usuario>(options => { });
builder.Services.AddScoped<IUserStore<Usuario>, UsuarioStore>();

builder.Services.AddAuthentication("cookies")
    .AddCookie("cookies", options =>
    options.LoginPath = "/Home/Login"
    );
#endregion

#region Config SignalR
builder.Services.AddSignalR();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
