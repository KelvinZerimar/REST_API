using Aka.Arch.Microservice.Managers;
using Aka.Arch.Microservice.Models;
using Aka.Microservice.Framework.Extensions;
using Nancy;
using Nancy.ModelBinding;
using Sharpility.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        ResponseAPI response = new ResponseAPI();
        /// <summary>
        /// Swagger documentation module for API First design.
        /// </summary>
        public UserModule(UserService service) : base(Extensions.APIBasePath)
        {
            // get all user
            Get("/api/user/getall", (parameters, token) =>
            {
                List<ApiUser> ListResponse = service.GetAllUser();
                if (ListResponse.Count > 0)
                    response.Status = (int)HttpStatusCode.OK;
                else
                    response.Status = (int)HttpStatusCode.NotFound;

                response.objResponse = ListResponse;

                return Task.FromResult<dynamic>(Response.AsJson(response));
            });

            // get one by id
            Get("/api/user/get/{id:int}", (parameters, token) =>
            {
                int idUser = parameters.id;
                ApiUser apiUser = service.GetUser(idUser);
                if (apiUser != null)
                    response.Status = (int)HttpStatusCode.OK;
                else
                    response.Status = (int)HttpStatusCode.NotFound;
                response.objResponse = apiUser;

                return Task.FromResult<dynamic>(Response.AsJson(response));
            });

            // create user
            Post("/api/user/create", (parameters, token) =>
            {
                ApiUser apiUserRequest = this.Bind<ApiUser>();
                Preconditions.IsNotNull(apiUserRequest, "Required parameter: 'body' is missing at 'create'");
                //Validar Campos del Json:
                string message = ValidarDataJson(apiUserRequest);
                if (message.Equals("OK"))
                {
                    response.Status = (int)HttpStatusCode.Created;
                    response.objResponse = service.CreasteUser(apiUserRequest);
                }
                else
                {
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.objResponse = message;
                }

                return Task.FromResult<dynamic>(Response.AsJson(response));
            });

            // update user
            Put("/api/user/update", (parameters, token) =>
            {
                ApiUser apiUserRequest = this.Bind<ApiUser>();
                Preconditions.IsNotNull(apiUserRequest, "Required parameter: 'body' is missing at 'update'");
                string message = ValidarDataJson(apiUserRequest);
                if (message.Equals("OK"))
                {
                    response.Status = (int)HttpStatusCode.OK;
                    response.objResponse = service.UpdateUser(apiUserRequest);
                }
                else
                {
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.objResponse = message;
                }

                return Task.FromResult<dynamic>(Response.AsJson(response));
            });

            // delete user
            Delete("/api/user/remove/{id:int}", (parameters, token) =>
            {
                int idUser = parameters.id;
                int resp = service.DeleteUser(idUser);

                response.Status = 204;
                response.objResponse = resp + " row(s) remove";
                
                return Task.FromResult<dynamic>(Response.AsJson(response));
            });
        }

        #region method privates
        private string ValidarDataJson(ApiUser apiUser)
        {
            ICollection<ValidationResult> results = null;
            if (!Validate(apiUser, out results))
            {
                return String.Join("\n", results.Select(o => o.ErrorMessage));
            }
            DateTime dateTime;
            if (!string.IsNullOrEmpty(apiUser.Birthdate))
                if (!DateTime.TryParse(apiUser.Birthdate, out dateTime))
                    return "Bad parameter: 'Birthdate' must be a valid date format 'create'";
                else
                    return "OK";
            else
                return "OK";
        }

        static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
        #endregion
    }


    /// <summary>
    /// Service handling User requests.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// This can only be done by an User.
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <returns>System.IO.Stream</returns>
        List<ApiUser> GetAllUser();

        /// <summary>
        /// Register new API users with a specified role
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="body">Register a new API user</param>
        /// <returns>string</returns>
        //string Register(NancyContext context, ApiUser body);
    }

    /// <summary>
    /// Abstraction of UserService.
    /// </summary>
    public abstract class AbstractUserService : IUserService
    {
        public virtual List<ApiUser> GetAllUser()
        {
            return GetAllUser();
        }

        //public virtual string Register(NancyContext context, ApiUser body)
        //{
        //    return Register(body);
        //}

        //protected abstract List<ApiUser> GetAllUser();

        //protected abstract string Register(ApiUser body);
    }
}