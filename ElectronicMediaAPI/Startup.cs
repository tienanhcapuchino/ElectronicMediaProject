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
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace ElectronicMediaAPI
{
    public class Startup
    {
        public IConfiguration ConfigRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            ConfigRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            InjectDependencyServices(services);
        }
        private void InjectDependencyServices(IServiceCollection services)
        {
            #region register services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IReplyCommentService, ReplyCommentService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostDetailService, PostDetailService>();
            services.AddTransient<IPostCategoryService, PostCategoryService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IPostStatisticService, PostStatisticService>();
            #endregion

            //configure JWT token

            services.Configure<AppSetting>(ConfigRoot.GetSection("AppSettings"));

            var secretKey = ConfigRoot["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    //sign to token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddCors(p => p.AddDefaultPolicy(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddDbContext<ElectronicMediaDbContext>(option =>
            {
                option.UseSqlServer(ConfigRoot.GetConnectionString("ElectronicStr"));
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}
