using Application.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services.Implementations;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAppDbContext, AppDbContext>();
var connetion = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connetion));
//builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IEnergyRepository, EnergyRepository>();
//builder.Services.AddTransient<IEnergyMeterRepository, EnergyMeterRepository>();
builder.Services.AddTransient<IHVSRepository, HVSRepository>();
builder.Services.AddTransient<IGVSRepository, GVSRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IStatisticService, StatisticService>();
builder.Services.AddTransient<IEnergyService, EnergyService>();
//builder.Services.AddTransient<IEnergyMeterService, EnergyMeterService>();
builder.Services.AddTransient<IGVSService, GVSService>();
builder.Services.AddTransient<IHVSService, HVSService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	var context = serviceProvider.GetRequiredService<AppDbContext>();
	DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");

app.Run();