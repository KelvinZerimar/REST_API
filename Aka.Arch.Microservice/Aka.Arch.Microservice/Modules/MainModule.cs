using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aka.Arch.Microservice.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get("/api/{id}", (p, token) => { return Task.FromResult<dynamic>(APIWelcome()); });
            Get("/", (p, token) => { return Task.FromResult<dynamic>(APIWelcome()); });
            Post("/", (p, token) => { return Task.FromResult<dynamic>(APIWelcome()); });
            Get("/async", async (p, token) => { return await Task.FromResult<dynamic>("This is an API Asynchronous response."); });

        }

        public Response APIWelcome()
        {
            var welcome = @"<style>.container { width: 500px; margin:0 auto; display:table; } 
ul.li { display:inline; } </style><div class='container'><p style='text-align: center;'>
<span style='color: #000000;'></br>Welcome&nbsp;to <span style='color: #008000;'>Aka API</span> Architecture:&nbsp;</span></p>
<p style='text-align: center;'><span style='color: #000000;'>&nbsp;</span></p><ul><li>
<span style='color: #808080;'>Fast, scalable and high performance web API ready to use.</span></li>
<li><span style='color: #808080;'>Security Owin module based on Tokens.</span></li>
<li><span style='color: #808080;'>Swagger standard documentation ready.</span></li>
<li><span style='color: #808080;'>Ready for unit testing with XUnit.</span></li>
<li><span style='color: #808080;'>OWIN ready.</span></li> <li><p><a href='http://google.es'><span style='color: #808080;'>+ Info Digital Architecture</span></a></p> </li></ul></div>";
            var resp = Response.AsText(welcome, "text/html");
            return resp;
        }

    }
}