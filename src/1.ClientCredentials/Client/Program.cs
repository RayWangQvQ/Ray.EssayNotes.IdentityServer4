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
            //获取AccessToken
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
        /// 根据客户端凭证获取AccessToken
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

                ClientId = "clientCredentials.client",
                ClientSecret = "secret",

                //Scope = "MyApiResourceScope1",//欲进入的资源域，不指定则为ids中为该客户端配置的允许访问的所有资源域；指定的话，则获取到的token只能用来进入指定的资源域
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
