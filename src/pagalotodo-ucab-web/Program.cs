var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


var app = builder.Build();

app.UseSession();

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginView}/{id?}");


//añadi esto por un bug raro, es posible que se deba quitar despues
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=UpdateUserViewAdmin}/{id?}");

app.Run();

//"{controller=Login}/{action=LoginView}/{id?}"
//"{controller=Admin}/{action=AdminHomeView}/{id?}
//"{controller=Login}/{action=SignUpProviderView}/{id?}"
//"{controller=User}/{action=UpdateUserView}/{id?}"
//"{controller=Admin}/{action=AllUsersView}/{id?}"
//"{controller=Service}/{action=UpdateServiceView}/{id?}"