using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using The_fifth_group_FinalProject.Controllers;
using The_fifth_group_FinalProject.Data;
using The_fifth_group_FinalProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddSingleton<WebSocketController>();

var TheFifthGroupOfTopicsConnectionString = builder.Configuration.GetConnectionString("TheFifthGroupOfTopics");
builder.Services.AddDbContext<TheFifthGroupOfTopicsContext>(options =>
	options.UseSqlServer(TheFifthGroupOfTopicsConnectionString), ServiceLifetime.Singleton);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] {
        UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs }));

// �]�w session middleware
builder.Services.AddSession(options =>
{
    // �]�w session �O�ɮɶ��� 20 ����
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
} 
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseWebSockets(new WebSocketOptions
{
	KeepAliveInterval = TimeSpan.FromSeconds(30)
});
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
