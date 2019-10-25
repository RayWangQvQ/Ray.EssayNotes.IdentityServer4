using System.Collections.Generic;
//
using IdentityServer4.Models;//添加引用
using IdentityServer4.Test;
using IdentityServer4;

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 受保护的身份资源集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),//添加
            };
        }

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
                    AllowedGrantTypes=GrantTypes.Hybrid,//混合流程模式

                    ClientId = "hybrid.mvc",
                    ClientName = "我的MVC客户端",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // 登陆成功后允许重定向的地址集合
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // 注销后允许重定向的地址集合
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    //允许该客户端访问的资源域
                    AllowedScopes = new List<string>
                    {
                        //身份资源域：
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //Api资源域：
                        "MyApiResourceScope1",
                        "MyApiResourceScope2"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        /// <summary>
        /// 用户集合
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "张三",
                    Password = "123",
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "李四",
                    Password = "456"
                }
            };
        }
    }
}
