using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aka.Arch.Microservice.Models
{
    public class ApiUser
    {
        /// <summary>
        /// Id : database identity
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Birthdate
        /// </summary>
        public string Birthdate { get; set; }
    }
}