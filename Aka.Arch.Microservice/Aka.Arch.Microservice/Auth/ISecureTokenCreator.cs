using Aka.Arch.Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aka.Arch.Microservice.Auth
{
    public interface ISecureTokenCreator
    {
        string CreateToken(IApiUserAuth user);
    }
}