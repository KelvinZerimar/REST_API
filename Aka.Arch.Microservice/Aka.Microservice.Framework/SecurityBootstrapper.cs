using Aka.Microservice.Framework.Exceptions;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Owin;
using Nancy.TinyIoc;
using System.Security.Claims;

namespace Aka.Microservice.Framework
{
    /// <summary>
    /// Nancy SecurityBootstrapper class
    /// </summary>
    public class SecurityBootstrapper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            //container.Register<IAdminService, AdminService>();
            //container.Register<IAuthService, AuthService>();

            base.ApplicationStartup(container, pipelines);
        }

        /// <summary>
        /// Pipeline for a web api request
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelines"></param>
        /// <param name="context"></param>
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            //CORS
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });

            //Custom error handling
            pipelines.OnError.AddItemToEndOfPipeline((ctx, exc) =>
            {
                return StatusCodeHandler.GetStatusCode(ctx, exc);
            });

            //Authenticathion
            var owinEnvironment = context.GetOwinEnvironment();
            var principal = owinEnvironment?["server.User"] as ClaimsPrincipal;
            if (principal == null) return;
            //context.CurrentUser = principal;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("App", @"App"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", @"fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", @"Scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Static", @"Static"));
        }


    }
}
