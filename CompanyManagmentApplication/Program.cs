using CompanyManagmentApplication.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Data>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
});

//for Authentication meddel ware
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(ox =>
{
    ox.LoginPath = ("/SignUp/SignIn");
    ox.AccessDeniedPath = ("/SignUp/SignIn");
}
);
builder.Services.AddDistributedMemoryCache();
//for session meddel ware
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(120);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
