using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 依赖注入容器
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //认证
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>//来自包 Microsoft.AspNetCore.Authentication.JwtBearer
                {
                    options.Authority = "http://localhost:5000";//授权服务器地址
                    options.RequireHttpsMetadata = false;

                    options.Audience = "MyApiResourceScope1";//token颁发的受众
                });

            //授权
            services.AddAuthorization();

            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// 配置管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();//认证
            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
