using System;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using Xunit;
using Aka.Arch.Microservice.Auth;

namespace Aka.Arch.Microservice.Tests
{
    public class CreateModuleTest : IDisposable
    {
        private Browser browser;
        public void Dispose()
        {
            this.browser = null;
        }

        public CreateModuleTest()
        {
            // Do "global" initialization here; Only called once.
            SecureTokenCreator secToken = new SecureTokenCreator();
            var boot = new Bootstrapper();
            //browser = new Browser(boot);
        }

        [Fact]
        public void TestCreateUser_Ok()
        {
            //seeds
            string Name = "Test user";
            string Birthdate = "2018-01-01";

            //Logic
            //1. 
            var response = browser.Post("/api/user/create", with => { with.Body("Name", Name); with.Body("Birthdate", Birthdate); });
            var result = response.Result.Body.DeserializeJson<string>();

            //Asserts
            result.Should().NotBeNullOrEmpty();
            //result.Should().Be(true);
            //result.Result.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
