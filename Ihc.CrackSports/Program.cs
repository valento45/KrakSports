using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.WebApp.Configurations.DependenciasInjection;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInjecaoDependencias(builder.Configuration);

#region Config Identity Core

//Configurando session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUsuarioContext, UsuarioContext>();

builder.Services.AddIdentityCore<Usuario>(options => { });
builder.Services.AddScoped<IUserStore<Usuario>, UsuarioStore>();

builder.Services.AddAuthentication("cookies")
	.AddCookie("cookies", options =>
	options.LoginPath = "/Home/Login"
	);
#endregion

#region Config SignalR
//builder.Services.AddSignalR();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


///Configure Globalization Culture
var cultures = new[] { 
	new CultureInfo("pt-BR"),
	new CultureInfo("en-US")
};


app.UseRequestLocalization(new RequestLocalizationOptions
{
	DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"),
	SupportedCultures = cultures
});

//app.UseHttpsRedirection();
app.UseForwardedHeaders();
app.UseDefaultFiles();
app.UseStaticFiles();

//Adiciona uso de session
app.UseSession();

app.UseRouting();
app.UseAuthorization();

//app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
