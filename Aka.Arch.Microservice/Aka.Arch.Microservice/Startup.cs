using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Aka.Arch.Microservice.Startup))]
namespace Aka.Arch.Microservice
{
    /// <summary>
    /// Owin Startup class to run the Web API as Owin middleware.
    /// </summary>
    public class Startup
    {



        /// <summary>
        /// Startup configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var pathsToIgnoreAuth = new[]
            {
                "/",
                "/api/user/getall",
                "/api/user/{id:int}",
                "/api/user/create",
            };


            app.UseNancy(opt => opt.Bootstrapper = new Bootstrapper());

        }



    }
}