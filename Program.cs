using Microsoft.EntityFrameworkCore;
using Beehive;
using Microsoft.AspNetCore.Authentication.Cookies;
using Beehive.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddSignalR();

var conf = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.Development.json")).Build();
builder.Services.AddOptions();
builder.Services.Configure<ConnectionStrings>(conf.GetSection("ConnectionStrings"));

//builder.Services.AddHttpContextAccessor();

string connection = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Authorize/Login");
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseHsts();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<DirectHub>("/dmhub");
app.MapHub<ChatHub>("/chathub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorize}/{action=Login}/{id?}");

app.Run();

