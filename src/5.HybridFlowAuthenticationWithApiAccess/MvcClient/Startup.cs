using System.IdentityModel.Tokens.Jwt;
//
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
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
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//来自包Microsoft.AspNetCore.Authentication.OpenIdConnect

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";//使用Cookie作为认证的默认方案
                    options.DefaultChallengeScheme = "oidc";//当需要用户登录时，设置oidc为默认方案
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>//来自包Microsoft.AspNetCore.Authentication.OpenIdConnect
                {
                    options.Authority = "http://localhost:5000";//授权认证服务器地址
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;//在Cookie中保存令牌（包括身份令牌、访问令牌、刷新令牌）
                    options.GetClaimsFromUserInfoEndpoint = true;

                    //欲访问的资源域
                    options.Scope.Add("MyApiResourceScope1");
                    options.Scope.Add("MyApiResourceScope2");
                    options.Scope.Add("offline_access");

                    options.ClaimActions.MapJsonKey("website", "website");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();//允许访问静态文件

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
