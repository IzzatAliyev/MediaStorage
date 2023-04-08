using MediaStorage.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MediaStorageViewModel>(provider =>
{
    var env = provider.GetService<IWebHostEnvironment>();
    var mediaFolder = Path.Combine(env.WebRootPath, "media");
    return new MediaStorageViewModel(mediaFolder);
});;

System.IO.Directory.CreateDirectory("wwwroot/media");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Upload}/{id?}");

app.Run();
