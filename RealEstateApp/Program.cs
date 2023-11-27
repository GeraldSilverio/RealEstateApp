using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application;
using RealEstateApp.Infraestructure.Identity;
using RealEstateApp.Infraestructure.Identity.Entities;
using RealEstateApp.Infraestructure.Identity.Seeds;
using RealEstateApp.Infraestructure.Persistence;
using RealEstateApp.Infraestructure.Shared;
using RealEstateApp.Presentation.WebApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ValidateUserSession, ValidateUserSession>();
builder.Services.AddSession();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, rolesManager);
        await DefaultAdminUser.SeedAsync(userManager, rolesManager);
        await DefaultClientUser.SeedAsync(userManager, rolesManager);
        await DefaultAgentUser.SeedAsync(userManager, rolesManager);
        await DefaultDeveloperUser.SeedAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=PrincipalView}/{id?}");

app.Run();

/* 
Creado por:
Russel Brian 2021-1742
Christopher Peguero 2022-1024
Gerald Silverio 2022-1068*/