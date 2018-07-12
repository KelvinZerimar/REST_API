using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aka.Arch.Microservice.Models
{
    public class ResponseAPI
    {
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// objResponse
        /// </summary>
        public object objResponse { get; set; }

    }
}