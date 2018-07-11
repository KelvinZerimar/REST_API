using Aka.Microservice.Framework.Extensions;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aka.Arch.Microservice.Modules
{
    /// <summary>
    /// Module processing requests of Auth domain.
    /// </summary>
    public sealed class UserModule : NancyModule
    {

        /// <summary>
        /// Swagger documentation module for API First design.
        /// </summary>
        public UserModule() : base(Extensions.APIBasePath)
        {
            Get("/api/user/getall", (p, token) =>
            {
                return Task.FromResult<dynamic>(Response.AsFile("static/doc.html"));
            });

        }
    }

}