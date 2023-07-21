/*********************************************************************
 *
 * PROPRIETARY and CONFIDENTIAL
 *
 * This is licensed from, and is trade secret of:
 *
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 *
 * RESTRICTED RIGHTS LEGEND
 *
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the
 * 2005 Civil Code, the Intellectual Property Law and Decree
 * No. 85/2011/ND-CP amending and supplementing a number of
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 *
 *
 * Copy right 2023 � PRN231 - SU23 - Group 10 �. All Rights Reserved
 *
 * Unpublished - All rights reserved under the copyright laws
 * of the Government of Viet Nam
*********************************************************************/
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicWeb.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ElectronicMediaDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ElectronicMediaDbContextConnection' not found.");

builder.Services.AddDbContext<ElectronicMediaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ElectronicMediaDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITokenService, TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
