using Microsoft.EntityFrameworkCore;
using Ogani.BLL;
using Ogani.BLL.Constants;
using Ogani.DAL;
using Ogani.DAL.DataContext;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.AddDalServices(builder.Configuration);
builder.Services.AddBllServices(builder.Configuration);

FilePathConstants.CategoryImagePath = Path.Combine(builder.Environment.WebRootPath, "img", "categories");

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    //var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //await appDbContext.Database.MigrateAsync();
    //var dataInit = new DataInitalizer(appDbContext,);
    //await dataInit.SeedDataAsync();

    var initalizer = scope.ServiceProvider.GetRequiredService<DataInitalizer>();
    await initalizer.SeedDataAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );
app.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();




//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

//    endpoints.MapControllerRoute(
//      name: "default",
//      pattern: "{controller=Home}/{action=Index}/{id?}");

//});



