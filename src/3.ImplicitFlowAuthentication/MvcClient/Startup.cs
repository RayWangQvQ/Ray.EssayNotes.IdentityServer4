using System.IdentityModel.Tokens.Jwt;
//
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//���԰�Microsoft.AspNetCore.Authentication.OpenIdConnect

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "MyCookieScheme";//ʹ��Cookie��Ϊ��֤��Ĭ�Ϸ���
                    options.DefaultChallengeScheme = "oidc";//����Ҫ�û���¼ʱ������oidcΪĬ�Ϸ���
                })
                .AddCookie("MyCookieScheme")
                .AddOpenIdConnect("oidc", options =>//���԰�Microsoft.AspNetCore.Authentication.OpenIdConnect
                {
                    options.Authority = "http://localhost:5000";//��Ȩ��֤��������ַ
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "implicit.client";
                    //options.Scope.Add("phone");//scopeĬ���С�openid���͡�profile�������OpenIdConnectOptions����ʱ����ӣ������Ը�������������ӻ��Ƴ�

                    options.SaveTokens = true;//��Cookie�б�������
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
            app.UseStaticFiles();//������ʾ�̬�ļ�

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
