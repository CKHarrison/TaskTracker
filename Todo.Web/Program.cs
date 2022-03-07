using TodoDataLibrary.Data;
using TodoDataLibrary.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Web.Data;
using Todo.Web.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Todo.Web.Utilities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = DataUtility.GetConnectionString(builder.Configuration); 
builder.Services.AddDbContext<TodoDbContext>(options =>
     options.UseNpgsql(connectionString)); 
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    })
      .AddEntityFrameworkStores<TodoDbContext>()
      .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IDatabaseData, SqlData>();
builder.Services.AddTransient<ISqlDataAccess, PgSqlDataAccess>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IEmailSender, EmailSender>();

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
