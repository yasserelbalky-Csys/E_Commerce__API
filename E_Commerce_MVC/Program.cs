using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Stripe;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

void RegisterGenericApiService<T>(IServiceCollection services, string baseUrl) where T : class
{
    services.AddHttpClient<GenericApiService<T>>(client => { client.BaseAddress = new Uri(baseUrl); });
}

RegisterGenericApiService<Category>(builder.Services, "http://localhost:5097/api/Category/");

// Register the Authentication with Services
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options => {
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/AccessDenied/AccessDenied";
});

// add session support
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<AccountApiService>();
builder.Services.AddHttpClient<SubCategoryService>();
builder.Services.AddHttpClient<BrandService>();
builder.Services.AddHttpClient<ProductApiService>();
builder.Services.AddHttpClient<StoreService>();
builder.Services.AddHttpClient<ShoppingCartService>();
builder.Services.AddHttpClient<OrderService>();
builder.Services.AddHttpClient<ProductBalanceService>();
builder.Services.AddHttpClient<CurrentProductBalanceService>();
// starting the application 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

Stripe.StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();