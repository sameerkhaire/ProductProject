using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace IdentityServer
{
    public class Configg
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
       new List<IdentityResource>
       {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
       };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret { Value = "test".Sha512() } },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },
            new Client
                {
                  ClientId = "angular-client",
                  ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                  AllowedGrantTypes = GrantTypes.Code,
                  RedirectUris = new List<string>{ "http://localhost:4200/signin-oidc"},
                  RequirePkce = true,
                  AllowAccessTokensViaBrowser = true,
                  AllowedScopes =
                  {
                     IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                    "productAPI",
                    "roles"
                  },
                  AllowedCorsOrigins = { "http://localhost:4200" },
                  RequireClientSecret = false,
                  PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-oidc" },
                  RequireConsent = true,
                  AllowPlainTextPkce=false

                },
            // interactive ASP.NET Core MVC client
            new Client
            {
                ClientId = "mvc",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                }
            }
            };
        //public static IEnumerable<IdentityResource> IdentityResources =>
        //    new[]
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        new IdentityResource
        //        {
        //            Name = "role",
        //            UserClaims = new List<string> { "role" }
        //        }
        //    };

        //public static IEnumerable<ApiScope> ApiScopes =>
        //    new[] { new ApiScope("productAPI", "Product API") };
        //public static IEnumerable<ApiResource> ApiResources =>
        //    new[]
        //    {
        //        new ApiResource("Product API")
        //        {
        //            Scopes = new List<string> { "productAPI" },
        //            ApiSecrets = new List<Secret> { new Secret("secret".Sha256()) },
        //            UserClaims = new List<string> { "role" }
        //        }
        //    };
        //public static List<TestUser> testUsers =>
        //    new List<TestUser>
        //    {
        //        new TestUser
        //        {
        //            SubjectId="5BE86359-073C-434B-AD2D-A3932222DABE",
        //            Username="sameer",
        //            Password="sameer",
        //            Claims=new List<Claim>
        //            {
        //                new Claim(IdentityModel.JwtClaimTypes.GivenName,"sameer"),
        //                new Claim(IdentityModel.JwtClaimTypes.FamilyName,"khaire")
        //            }
        //        }
        //    };

        //public static IEnumerable<Client> Clients =>
        //    new[]
        //    {

        //        new Client
        //        {
        //            ClientId = "company-product",
        //            AllowedGrantTypes = GrantTypes.ClientCredentials,
        //            ClientSecrets = { new Secret("secret".Sha256()) },
        //            AllowedScopes = { "productAPI" }
        //        }
        //        ,
        //        new Client
        //        {
        //          ClientId = "angular-client",
        //          ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
        //          AllowedGrantTypes = GrantTypes.Code,
        //          RedirectUris = new List<string>{ "http://localhost:4200/signin-oidc"},
        //          RequirePkce = true,
        //          AllowAccessTokensViaBrowser = true,
        //          AllowedScopes =
        //          {
        //             IdentityServerConstants.StandardScopes.OpenId,
        //             IdentityServerConstants.StandardScopes.Profile,
        //            "productAPI",
        //            "roles"
        //          },
        //          AllowedCorsOrigins = { "http://localhost:4200" },
        //          RequireClientSecret = false,
        //          PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-oidc" },
        //          RequireConsent = true,
        //          AllowPlainTextPkce=false

        //        }
        //        // interactive client using code flow + pkce
        //        //new Client
        //        //{
        //        //    ClientId = "interactive",
        //        //    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
        //        //    AllowedGrantTypes = GrantTypes.Code,
        //        //    RedirectUris = { "https://localhost:5444/signin-oidc" },
        //        //    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
        //        //    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
        //        //    AllowOfflineAccess = true,
        //        //    AllowedScopes = { "openid", "profile", "CoffeeAPI.read" },
        //        //    RequirePkce = true,
        //        //    RequireConsent = true,
        //        //    AllowPlainTextPkce = false
        //        //},
        //    };




















        //public static IEnumerable<Client> GetClients() =>
        //  new[]
        //  {
        //    new Client
        //    {
        //      ClientId = "company-product",
        //      ClientName="Product Credentials client",
        //      ClientSecrets = new [] { new Secret("secret".Sha256()) },
        //      AllowedGrantTypes = GrantTypes.ClientCredentials,
        //      AllowedScopes = { "productAPI.read", "productAPI.write" }
        //    },
        //    new Client
        //    {
        //        ClientId="productMyClient",
        //        ClientName="Product MVC Web App",
        //        AllowedGrantTypes=GrantTypes.Code,
        //        RequirePkce=true,
        //        RequireConsent=true,
        //        AllowPlainTextPkce=false,
        //        RedirectUris=new List<string>()
        //        {
        //            "https://localhost:4200/signin-oidc"
        //        },
        //        PostLogoutRedirectUris= new List<string>()
        //        {
        //            "https://localhost:4200/signout-callback-oidc"
        //        },
        //        AllowOfflineAccess=true
        //        ,
        //        ClientSecrets =new List<Secret>()
        //        {
        //            new Secret("secret".Sha256())

        //        }
        //        ,
        //        AllowedScopes= new List<string>
        //        {
        //            "openid","profile","productAPI.read"
        //        }

        //    }
        //  };
        //public static IEnumerable<ApiScope> ApiScopes =>
        //   new [] { new ApiScope("productAPI.read"), new ApiScope("productAPI.write") };
        //public static IEnumerable<ApiResource> ApiResources =>
        //new []
        //{
        //   new ApiResource("productAPI")
        //   {
        //    Scopes = new List<string>{ "productAPI.read", "productAPI.write" },
        //    ApiSecrets=new List<Secret>{ new Secret("secret".Sha256()) },
        //    UserClaims = new List < string > { "role" }
        //   }
        //};
        //public static IEnumerable<IdentityResource> IdentityResources =>
        // new[]
        // {
        //  new IdentityResources.OpenId(),
        //  new IdentityResources.Profile(),
        //  new IdentityResource
        //  {
        //      Name="role",
        //      UserClaims=new List<string>{"role"}
        //  }
        // };

    }
}

