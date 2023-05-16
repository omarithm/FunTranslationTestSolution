
using FT_UILibrary.API;
using FT_UILibrary.Endpoint;
using FT_UILibrary.Endpoint.UserEndpoints;
using FT_UILibrary.Models.UserModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region My services:
builder.Services.AddMvc();

builder.Services.AddSingleton<IFTAPIHelper, FTAPIHelper>();
builder.Services.AddSingleton<IAPIHelper, APIHelper>();
builder.Services.AddSingleton<ILoggedInUserModel, LoggedInUserModel>();

builder.Services.AddTransient<IFTHistoryEndpoint, FTHistoryEndpoint>();
builder.Services.AddTransient<IFTEndpoint, FTEndpoint>();
builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();
//Add the rest...

#endregion

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
app.UseAuthorization();

#region My configurations:
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Translation}/{action=Index}/{id?}");
#endregion

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
