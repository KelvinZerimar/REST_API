using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aka.Microservice.Framework.Exceptions
{
    public class StatusCodeHandler //: IStatusCodeHandler
    {

        /// <summary>
        /// Handle status code 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static HttpStatusCode GetStatusCode(NancyContext ctx, Exception ex)
        {
            //Handle status codes depending of exception here.
            if (ex is ArgumentException)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.InternalServerError;
        }

        //public void Handle(HttpStatusCode statusCode, NancyContext context)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
