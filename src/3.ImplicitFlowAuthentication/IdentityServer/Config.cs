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
        /// 身份资源类型集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),//用户简介信息（姓、名等）
                //new IdentityResources.Phone(),//手机号
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
                    AllowedGrantTypes = GrantTypes.Implicit,//颁发类型为【简化模式】，即OpenID Connect的implicit模式

                    ClientId = "implicit.client",
                    ClientName = "【我的MVC客户端】",

                    // 登陆成功后允许重定向的地址集合
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // 注销后允许重定向的地址集合
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        //与OAuth相同，Scopes代表要保护的资源域；
                        //与OAuth不同的是，OIDC中的资源域不代表API，而是代表用户ID、姓名或电子邮件地址等用户的身份信息。
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,//用户简介
                        //IdentityServerConstants.StandardScopes.Phone,//手机
                    }
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
