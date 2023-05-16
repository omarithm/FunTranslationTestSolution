using FT_Api.Data;
using FT_ApiLibrary.DataAccess;
using FT_ApiLibrary.Internal.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // My added service
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();



//My services
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddTransient<IFTApiPropertiesData, FTApiPropertiesData>();
builder.Services.AddTransient<IFTHistoryData, FTHistoryData>();


#region my added code about Token
//To make sure that token is not expired
string secretKey = builder.Configuration.GetValue<string>("SecretKeys:SecurityKey");

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "JwtBearer";
    option.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", JwtBearerOptions =>
{
    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
}
);
#endregion


#region configure Swagger
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Fun Translation API Swagger",
            Version = "v1"
        });

    //setup.OperationFilter<AuthorizationOperationFilter>();
});
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


#region My Added code about swagger
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Fun Translation API v1");
    //x.DocumentTitle = "Fun Translation API";

    //To make swagger as home page, uncomment the following line
    //c.RoutePrefix = string.Empty;
});
#endregion


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
