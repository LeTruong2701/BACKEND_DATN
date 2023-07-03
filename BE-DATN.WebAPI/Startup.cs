using BE_DATN.Application.BUS.Admin;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.BUS.Home;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Authenticate;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
//using BE_DATN.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //db
            services.AddDbContext<BEDATNDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BEDATNDb")));

            //identity
            services.AddIdentity<Users, Roles>().AddEntityFrameworkStores<BEDATNDbContext>().AddDefaultTokenProviders();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            //services DI
            services.AddTransient<IManageSanPham, ManageSanPham>();
            services.AddTransient<IManageUser1, ManageUser1>();
            services.AddTransient<IManageDanhMuc, ManageDanhMuc>();
            services.AddTransient<IManageKhachHang, ManageKhachHang>();
            services.AddTransient<IManageNhaCungCap, ManageNhaCungCap>();
            services.AddTransient<IManageAccount, ManageAccount>();
            services.AddTransient<IManageKhuyenMai, ManageKhuyenMai>();
            services.AddTransient<IManageNews, ManageNews>();
            services.AddTransient<IManageNguoiDung, ManageNguoiDung>();
            services.AddTransient<IManageNhanVien, ManageNhanVien>();
            services.AddTransient<IManageThuongHieu, ManageThuongHieu>();
            services.AddTransient<IManageHoaDonNhap, ManageHoaDonNhap>();
            services.AddTransient<IManageKho, ManageKho>();
            services.AddTransient<IManageDonHang, ManageDonHang>();
            services.AddTransient<IManageUser, ManageUser>();
            services.AddTransient<IManageRole, ManageRole>();
            services.AddTransient<ITrangChu, TrangChu>();
            services.AddTransient<ISanPham, SanPhamManage>();
            services.AddTransient<IShop, ShopManage>();
            services.AddTransient<IUserManage, UserManage>();
            services.AddTransient<ICheckout, CheckoutManage>();
            services.AddTransient<IDonHangKhachHang, DonHangKhachHangManage>();
            services.AddTransient<IThongKe, ManageThongKe>();
            services.AddTransient<INews, NewsManage>();
            services.AddTransient<IDanhGiaSanPham,DanhGiaSanPhamManage>();


            services.AddHttpClient();
            services.AddControllers();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //cors them dau tien
            app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //swagger
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
