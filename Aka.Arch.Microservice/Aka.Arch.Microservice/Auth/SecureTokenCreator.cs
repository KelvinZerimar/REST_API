using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Aka.Arch.Microservice.Models;
using JWT;

namespace Aka.Arch.Microservice.Auth
{
    public class SecureTokenCreator : ISecureTokenCreator
    {
        private const string SecretKey = "30ea254132194990837862e7d9a644c1";

        public string CreateToken(IApiUserAuth user)
        {
            throw new NotImplementedException();
        }

        //public string CreateToken(IApiUserAuth user)
        //{
        //    var claims = new List<Claim>(user.BasicClaims.Select(c => new Claim(ClaimTypes.Role, c)))
        //    {
        //        new Claim(ClaimTypes.Name, user.Username)
        //    };

        //    var token = new JwtToken
        //    {
        //        Issuer = "https://localhost",
        //        Audience = "https://localhost",
        //        Claims = claims,
        //        Expiry = DateTime.UtcNow.AddDays(1),
        //    };

        //    var encodedToken = JsonWebToken.Encode(token, SecretKey, JwtHashAlgorithm.HS512);
        //    return encodedToken;
        //}
    }
}