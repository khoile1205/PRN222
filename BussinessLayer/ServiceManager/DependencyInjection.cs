using BussinessLayer.Services;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using BussinessLayer.Authentication;

namespace BussinessLayer.ServiceManager
{
    public class DependencyInjection
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.EnableRetryOnFailure();
                });
            });

            #region Repositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IPaginationRepository<>), typeof(PaginationRepository<>));
            #endregion

            #region Services
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBeverageService, BeverageService>();
            services.AddScoped<IBeverageCategoryService, BeverageCategoryService>();
            services.AddScoped<IBeverageSizeService, BeverageSizeService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ITableDetailService, TableDetailService>();
            services.AddScoped<IBeverageDetailService, BeverageDetailService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IShiftStaffService, ShiftStaffService>();
            services.AddScoped<ISalaryService, SalaryService>();

            services.AddAutoMapper(typeof(DependencyInjection));


            #endregion

            #region JWT Authentication

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Auth/Login");
                            return Task.CompletedTask;
                        }
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnForbidden = context =>
                        {
                            context.Response.Redirect("/Auth/AccessDenied");
                            return Task.CompletedTask;

                        }
                    };
                })
                 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                 {
                     options.LoginPath = "/Auth/Login";
                     options.LogoutPath = "/Auth/Logout";
                     options.AccessDeniedPath = "/Auth/AccessDenied";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                     options.SlidingExpiration = true;
                 });
            services.AddAuthorization();
            #endregion
        }
    }
}
