using DataMem;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Data;
using RazorRepoUI.Data;
using UI.Data;
using Microsoft.AspNetCore.Identity;
using UI.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ItemsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<ItemsContext>();

builder.Services.AddAuthorization(options =>
{
    // in our authorization options we add a policy
    // that requires the user to have the admin role
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddRazorPages(options =>
{
    // secure anything in the Pages/Items folder 
    // by assigning it the admin policy
    // which we created above 
    // saying it requires a user to have the admin role
    options.Conventions.AuthorizeFolder("/Items", "AdminPolicy");
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();

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

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await AdminHelper.SeedAdminAsync(scope.ServiceProvider);
}

app.Run();
