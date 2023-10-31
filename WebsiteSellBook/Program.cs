using Microsoft.EntityFrameworkCore;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository;
using SellBook.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DB context 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")
));

// Register with container
// if you don't add scoped, you cannot dependency injection to controller
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


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

app.UseAuthorization();

app.MapControllerRoute(
	  name: "areas",
		pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"

	);

app.Run();
