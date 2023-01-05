using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopDemoApp.Core.Contracts;
using WebShopDemoApp.Core.Services;
using WebShopDemoApp.Core.Data;
using WebShopDemoApp.Core.Data.Common;
using WebShopDemoApp.Core.Data.Models.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

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
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
