using Nancy.Diagnostics;
using Aka.Microservice.Framework;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy;

namespace Aka.Arch.Microservice
{
    /// <summary>
    /// Nancy Bootstrapper class
    /// </summary>
    public class Bootstrapper : SecurityBootstrapper
    {

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }

        public override void Configure(Nancy.Configuration.INancyEnvironment environment)
        {
            environment.Diagnostics(password: "passw0rd!");
            base.Configure(environment);
        }

    }
}