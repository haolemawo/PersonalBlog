// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IDentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("blog","My Personal Blog")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "Multiply Page Client",
                    AccessTokenLifetime = 216000,//有效期为60小时
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    
                    ClientSecrets =
                    {
                        new Secret("mulsecret".Sha256())
                    },
                    AllowedScopes = {
                        "blog",
                        "picture",
                        "gender",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}