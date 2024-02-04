using Microsoft.EntityFrameworkCore;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository;
using SellBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using SellBook.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SellBook.DataAccess.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{

	options.LogoutPath = $"/Identity/Account/Logout";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
	options.LoginPath = $"/Identity/Account/Login";
});

// Add facebook login
builder.Services.AddAuthentication().AddFacebook(options =>
{
	options.AppId = "712335950966708";
	options.AppSecret = "b6b85b9349127f337a22f9d011089cac";
});

// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(100);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});



builder.Services.AddRazorPages();
// Register with container
// if you don't add scoped, you cannot dependency injection to controller
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//init db initialize
builder.Services.AddScoped<IDbInitializer, DbInitializer>();


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
app.MapRazorPages();
app.UseSession();
SeedDatabase();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.MapControllerRoute(
	  name: "areas",
		pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"

	);

app.Run();

void SeedDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
		dbInitializer.Initialize();
	}
}
