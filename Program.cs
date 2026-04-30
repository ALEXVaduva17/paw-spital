using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Repositories;
using PawSpital.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Construieste calea absoluta catre baza de date, relativ la directorul proiectului
// Astfel PawSpital.db va fi mereu in acelasi loc, indiferent de working directory
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "PawSpital.db");

builder.Services.AddDbContext<SpitalContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Repositories (Repository Pattern)
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IServiciuRepository, ServiciuRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IProgramareRepository, ProgramareRepository>();

// Business logic services (one per entity)
builder.Services.AddScoped<IDepartamentService, DepartamentService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IServiciuService, ServiciuService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IProgramareService, ProgramareService>();
builder.Services.AddSingleton<IAuthService, InMemoryAuthService>();

var app = builder.Build();

// rulează automat migrațiile pe baza de date la pornirea aplicației
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SpitalContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
