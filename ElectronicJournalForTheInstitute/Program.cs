using ElectronicJournalForTheInstitute.Data;
using ElectronicJournalForTheInstitute.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        var featureFolders = new[] { "Grades", "Groups", "Lessons", "Students", "Subjects", "Teachers", "Users" };
        var pageNames = new[] { "Index", "Details", "Create", "Edit", "Delete", "Login", "Register", "Logout", "Welcome" };

        foreach (var folder in featureFolders)
        {
            foreach (var pageName in pageNames)
            {
                options.Conventions.AddPageRoute($"/Features/{folder}/{pageName}", $"/{folder}/{pageName}");
            }
        }
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Вказуємо, куди перекидати користувача, якщо він не авторизований
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Features/Users/Login";
    options.AccessDeniedPath = "/Index";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ElectronicJournalForTheInstitute.Middleware.LoginRequiredMiddleware>();
app.UseMiddleware<ElectronicJournalForTheInstitute.Middleware.RoleAccessMiddleware>();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

// Автоматичне створення ролей і тестового адміністратора
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roles = new[] { "admin", "user" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    const string adminLogin = "admin";
    const string adminPassword = "admin";
    const string adminEmail = "admin@local.test";

    var adminUser = await userManager.FindByNameAsync(adminLogin)
                    ?? await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminLogin,
            Email = adminEmail,
            FullName = "Administrator"
        };

        var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (!createAdminResult.Succeeded)
        {
            throw new InvalidOperationException($"Не вдалося створити тестового адміністратора: {string.Join(", ", createAdminResult.Errors.Select(e => e.Description))}");
        }
    }

    if (!await userManager.IsInRoleAsync(adminUser, "admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "admin");
    }
}

app.Run();