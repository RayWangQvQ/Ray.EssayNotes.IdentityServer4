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
        /// 更具客户端凭证获取token
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
            var request = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "scope1"
            };
            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(request);

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
