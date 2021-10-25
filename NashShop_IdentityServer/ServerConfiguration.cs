using IdentityServer4.Models;
using NashShop_BackendApi.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace NashShop_IdentityServer
{
    public static class ServerConfiguration
    {
        //public static List<IdentityResource> IdentityResources { get; set; }
        //public static List<ApiScope> ApiScopes { get; set; }
        //public static List<ApiResource> ApiResources { get; set; }
        //public static List<Client> Clients { get; set; }
        //public static List<User> User { get; set; }
        public static List<IdentityResource> IdentityResources
        {
            get
            {
                List<IdentityResource> idResources = new List<IdentityResource>();
                idResources.Add(new IdentityResources.OpenId());
                idResources.Add(new IdentityResources.Profile());
                idResources.Add(new IdentityResources.Email());
                idResources.Add(new IdentityResources.Phone());
                idResources.Add(new IdentityResources.Address());
                idResources.Add(new IdentityResource("roles", "User roles", new List<string> { "role" }));
                return idResources;
            }
        }
        public static List<ApiScope> ApiScopes
        {
            get
            {
                List<ApiScope> apiScopes = new List<ApiScope>();
                apiScopes.Add(new ApiScope("employeesWebApi", "Employees Web API"));
                return apiScopes;
            }
        }
        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new
        ApiResource("employeesWebApiResource",
        "Employees Web API")
                {
                    Scopes = { "employeesWebApi" },
                    UserClaims = { "role",
                            "given_name",
                            "family_name",
                            "email",
                            "phone",
                            "address"
                            }
                };

                List<ApiResource> apiResources = new
        List<ApiResource>();
                apiResources.Add(apiResource1);

                return apiResources;
            }
        }
        public static List<Client> Clients
        {
            get
            {
                Client client1 = new Client
                {
                    ClientId = "client1",
                    ClientName = "Client 1",
                    ClientSecrets = new[] {
new Secret("client1_secret_code".Sha512()) },
                    AllowedGrantTypes = GrantTypes.
        ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {
                "openid",
                "roles",
                "employeesWebApi"
            }
                };

                List<Client> clients = new List<Client>();
                clients.Add(client1);

                return clients;
            }
        }
        public static List<User> TestUsers
        {
            get
            {
                User usr1 = new User()
                {
                    SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                    Username = "user1",
                    Password = "password1",
                    Claims = new List<Claim>
            {
                new Claim("given_name", "firstName1"),
                new Claim("family_name", "lastName1"),
                new Claim("address", "USA"),
                new Claim("email","user1@localhost"),
                new Claim("phone", "123"),
                new Claim("role", "Admin")
            }
                };

                User usr2 = new User()
                {
                    SubjectId = "5747df40-1bff-49ee-aadf-905bacb39a3a",
                    Username = "user2",
                    Password = "password2",
                    Claims = new List<Claim>
            {
                new Claim("given_name", "firstName2"),
                new Claim("family_name", "lastName2"),
                new Claim("address", "UK"),
                new Claim("email","user2@localhost"),
                new Claim("phone", "456"),
                new Claim("role", "Operator")
            }
                };

                List<User> testUsers = new List<User>();
                testUsers.Add(usr1);
                testUsers.Add(usr2);

                return testUsers;
            }
        }
    }


}
