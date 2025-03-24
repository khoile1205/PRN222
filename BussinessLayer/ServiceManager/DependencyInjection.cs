using BussinessLayer.Services;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using BussinessLayer.Authentication;
using AutoMapper;

namespace BussinessLayer.ServiceManager
{
    public class DependencyInjection
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IPaginationRepository<>), typeof(PaginationRepository<>));
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryCategoryRepository, InventoryCategoryRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IShiftStaffRepository, ShiftStaffRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
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

            services.AddAutoMapper(typeof(DependencyInjection));


            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IInventoryCategoryService, InventoryCategoryService>();
            #endregion

            #region JWT Authentication
            var jwtSettings = configuration.GetSection("Jwt");
            var issuer = jwtSettings["Issuer"];
            var key = jwtSettings["Key"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Jwt:Issuer and Jwt:Key are required in appsettings.json");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        IssuerSigningKey = signingKey,
                        ClockSkew = TimeSpan.Zero
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