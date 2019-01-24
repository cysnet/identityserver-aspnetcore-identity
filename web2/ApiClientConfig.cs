using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web2
{
    public class ApiClientConfig
    {
        public const string GateWayClient_Name = "cobbler.gateway";
        public const string GateWayClient_Credentials_Id = "cobbler.gateway.clientcredentials.id.20181219";
        public const string GateWayClient_Credentials_Secret = "cobbler.gateway.clientcredentials.secret.20181219";
        public const string GateWayClient_Password_Id = "cobbler.gateway.password.id.20181219";
        public const string GateWayClient_Password_Secret = "cobbler.gateway.password.secret.20181219";

        public const string ApiResource_GateWay_Secret = "apiresource.gateway.secret";
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(GateWayClient_Name,GateWayClient_Name)
                //{ApiSecrets={new Secret(ApiResource_GateWay_Secret.Sha256())} }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = GateWayClient_Credentials_Id,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(GateWayClient_Credentials_Secret.Sha256())
                    },
                    AllowedScopes = { GateWayClient_Name }
                },
                new Client
                {
                    ClientId = GateWayClient_Password_Id,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    //AccessTokenType = AccessTokenType.Reference,
                    ClientSecrets =
                    {
                        new Secret(GateWayClient_Password_Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                        GateWayClient_Name,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
             };
        }
    }
}
