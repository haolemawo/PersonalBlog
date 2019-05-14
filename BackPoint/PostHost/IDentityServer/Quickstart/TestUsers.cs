// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "jyz", Password = "123456", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "JYZ"),
                    new Claim(JwtClaimTypes.GivenName, "Jiang"),
                    new Claim(JwtClaimTypes.FamilyName, "YuZhou"),
                    new Claim(JwtClaimTypes.Email, "842714673@qq.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://jyz.com"),
                    new Claim(JwtClaimTypes.Picture, @"./useravator/manager.jpg"),
                    new Claim(JwtClaimTypes.Gender, "管理员"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser{SubjectId = "88421113", Username = "dky", Password = "123456",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "DKY"),
                    new Claim(JwtClaimTypes.GivenName, "Dai"),
                    new Claim(JwtClaimTypes.FamilyName, "KeYu"),
                    new Claim(JwtClaimTypes.Email, "864520864@qq.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://dky.com"),
                    new Claim(JwtClaimTypes.Picture, @"./useravator/dky.jpg"),
                    new Claim(JwtClaimTypes.Gender, "好友"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
                        new TestUser{SubjectId = "88421114", Username = "zjj", Password = "123456",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "ZJJ"),
                    new Claim(JwtClaimTypes.GivenName, "Zhang"),
                    new Claim(JwtClaimTypes.FamilyName, "JinJin"),
                    new Claim(JwtClaimTypes.Email, "1198625640@qq.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://zjj.com"),
                    new Claim(JwtClaimTypes.Picture, @"./useravator/zjj.jpg"),
                    new Claim(JwtClaimTypes.Gender, "好友"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            }
        };
    }
}