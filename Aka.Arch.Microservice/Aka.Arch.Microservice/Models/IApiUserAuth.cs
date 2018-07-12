using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aka.Arch.Microservice.Models
{
    public interface IApiUserAuth
    {
        //
        // Resumen:
        //     The username of the authenticated user.
        string Username { get; }
        //
        // Resumen:
        //     The claims of the authenticated user.
        IEnumerable<string> BasicClaims { get; }
    }
}