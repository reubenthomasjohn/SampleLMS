using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Dal.Repositories;
using SampleLMS.Data;
using SampleLMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CourseDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CoursesDbConnectionString"))
);

builder.Services.AddDbContext<AuthDbContext>(options =>
{
	options.EnableSensitiveDataLogging();
	options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnectionString"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddScoped<IImageInterface, CloudinaryImageRepository>();
builder.Services.AddScoped<ICourseInterface, CourseRepository>();
builder.Services.AddScoped<ICategoryInterface, CategoryRepository>();
builder.Services.AddScoped<IModuleInterface, ModuleRepository>();
builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<IStorageServiceInterface, StorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.Services.InitializeDb();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
