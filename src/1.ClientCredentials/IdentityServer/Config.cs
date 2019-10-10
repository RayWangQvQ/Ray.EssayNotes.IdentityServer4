using System.Collections.Generic;
//
using IdentityServer4.Models;//添加引用

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 身份资源类型集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// api资源集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("scope1", "My API"),
                new ApiResource("scope2", "My API2"),
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,// 不存在用户, 使用客户端Id+密码模式进行认证

                    ClientId = "client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "scope1", "scope2" }//允许访问的资源域
                }
            };
        }
    }
}
