using System.Collections.Generic;
//
using IdentityServer4.Models;//添加引用

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 受保护的api资源集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("MyApiResourceScope1", "我的API资源域1"),
                new ApiResource("MyApiResourceScope2", "我的API资源域2"),
            };
        }

        /// <summary>
        /// 客户端集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//颁发类型为【客户端凭证模式】，即不存在用户, 使用客户端Id+密码模式进行认证

                    ClientId = "clientCredentials.client",
                    ClientName= "我的客户端",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //允许该客户端访问的资源域
                    AllowedScopes = new List<string>
                    {
                        "MyApiResourceScope1",
                        "MyApiResourceScope2"
                    }
                }
            };
        }
    }
}
