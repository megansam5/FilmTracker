using FilmTracker.Data;
using FilmTracker.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IOmdbApiService, OmdbApiService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IComparisonService, ComparisonService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
