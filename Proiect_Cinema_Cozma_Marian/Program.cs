using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proiect_Cinema_Cozma_Marian.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Movies");
    options.Conventions.AllowAnonymousToPage("/Movies/Index");
    options.Conventions.AllowAnonymousToPage("/Movies/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Genres", "AdminPolicy");
});

builder.Services.AddDbContext<Proiect_Cinema_Cozma_MarianContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Proiect_Cinema_Cozma_MarianContext") ?? throw new InvalidOperationException("Connection string 'Proiect_Cinema_Cozma_MarianContext' not found.")));

builder.Services.AddDbContext<CinemaIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Proiect_Cinema_Cozma_MarianContext") ?? throw new InvalidOperationException("Connection string 'Proiect_Cinema_Cozma_MarianContext' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CinemaIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
