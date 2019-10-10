using System;
using System.Net.Http;
using System.Threading.Tasks;
//
using IdentityModel.Client;//添加引用
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        private static async Task Main()
        {
            //获取与token
            string accessToken = await GetAccessToken();

            // 调用api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);
            var response = await apiClient.GetAsync("http://localhost:5001/WeatherForecast");

            Console.WriteLine("\r\n调用接口:");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 根据用户名+密码获取token
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetAccessToken()
        {
            var client = new HttpClient();

            // 获取导航文件
            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return "";
            }

            // 客户端凭证模式获取token
            var request = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "张三",
                Password = "123",

                Scope = "scope1"
            };
            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(request);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return "";
            }

            Console.WriteLine("token:");
            Console.WriteLine(tokenResponse.Json);

            return tokenResponse.AccessToken;
        }
    }
}
