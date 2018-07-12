using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aka.Arch.Microservice.Models
{
    public class ApiUser
    {
        /// <summary>
        /// Id : database identity
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// Birthdate
        /// </summary>
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }
    }
}