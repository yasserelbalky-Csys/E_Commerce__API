using BLL;
using BLL.DTOs.CategoryDTOs;
using BLL.DTOs.OrderDtos;
using BLL.DTOs.ProductDtos;
using DAL;
using E_Commerce_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

void RegisterGenericApiService<T>(IServiceCollection services, string baseUrl) where T : class {
	services.AddHttpClient<GenericApiService<T>>(client => {
		client.BaseAddress = new Uri(baseUrl);
	});
}

RegisterGenericApiService<CategoryListDto>(builder.Services, "http://localhost:5097/api/Category/");
RegisterGenericApiService<ProductListDto>(builder.Services, "http://localhost:5097/api/Product/");
RegisterGenericApiService<OrderListDto>(builder.Services, "http://localhost:5097/api/Order/");


// add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationLayerServices();
builder.Services.AddDataAccessServices(builder.Configuration);

// Add authentication


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();