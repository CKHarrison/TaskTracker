using TodoDataLibrary.Data;
using TodoDataLibrary.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Web.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PgSqlDb"); 
builder.Services.AddDbContext<TodoDbContext>(options =>
     options.UseNpgsql(connectionString)); 
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddEntityFrameworkStores<TodoDbContext>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IDatabaseData, SqlData>();
builder.Services.AddTransient<ISqlDataAccess, PgSqlDataAccess>();
builder.Services.AddAutoMapper(typeof(Program));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
