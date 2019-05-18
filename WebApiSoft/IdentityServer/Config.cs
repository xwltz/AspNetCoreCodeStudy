using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }

        /// <summary>
        /// 返回应用列表
        /// ApiResource第一个参数是应用的名字，第二个参数是描述
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resources = new List<ApiResource>
            {
                new ApiResource("MessageApi", "消息API"),
                new ApiResource("ProductApi", "产品API")
            };
            return resources;
        }
        /// <summary>
        /// 返回账号列表
        /// </summary>         
        /// <returns></returns>         
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "client1", //API账号、客户端Id
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("123456".Sha256()) //秘钥
                    },
                    AllowedScopes = { "MessageApi", "ProductApi" }
                }
            };
            return clients;
        }
    }
}
