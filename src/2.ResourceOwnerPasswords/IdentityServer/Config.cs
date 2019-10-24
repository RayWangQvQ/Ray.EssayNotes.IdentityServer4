using System.Collections.Generic;
//
using IdentityServer4.Models;//添加引用
using IdentityServer4.Test;

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// api资源集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//颁发类型为【密码模式】，即使用账号+密码进行认证

                    ClientId = "resourceOwnerPwd.client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "MyApiResourceScope1",
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
                    Password = "123"
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
