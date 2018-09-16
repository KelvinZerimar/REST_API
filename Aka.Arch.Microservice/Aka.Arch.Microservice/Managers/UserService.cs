using Aka.Arch.Microservice.Models;
using Aka.Arch.Microservice.Modules;
using Nancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SQLite;
using System.Threading.Tasks;


namespace Aka.Arch.Microservice.Managers
{
    public class UserService : IUserService
    {
        /// <summary>
        /// GetAllUser
        /// </summary>
        /// <returns></returns>
        public List<ApiUser> GetAllUser()
        {
            #region Vars
            List<ApiUser> Response = new List<ApiUser>();
            #endregion //Vars


            if (!File.Exists(DBContext.DbFile))
            {
                SeedDatabase();
                SeedData();
            }

            using (var cnn = DBContext.DbConnection())
            {
                cnn.Open();

                var dataReader = cnn.ExecuteReader("SELECT Id, Name, BirthDate FROM ApiUser");
                while (dataReader.Read())
                {
                    ApiUser user = new ApiUser();
                    user.Id = dataReader["Id"].ToString();
                    user.Name = dataReader["Name"].ToString();
                    user.Birthdate = dataReader["BirthDate"].ToString();
                    Response.Add(user);
                }
            }

            // Calls to Task_MethodAsync
            Task returnedTask = Task_MethodAsync("GetAllUser");
           
            return Response;
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="intId"></param>
        /// <returns></returns>
        public ApiUser GetUser(int intId)
        {
            string sql = "SELECT Id, Name,  IFNULL(BirthDate, 'unknown') as BirthDate  FROM ApiUser WHERE Id = @id";
            ApiUser user = new ApiUser();
            using (var cnn = DBContext.DbConnection())
            {
                user = cnn.Query<ApiUser>(sql, new { Id = intId}).FirstOrDefault(); 
            }

            // Calls to Task_MethodAsync
            Task returnedTask = Task_MethodAsync("GetUser");
            return user;
        }

        /// <summary>
        /// CreasteUser
        /// </summary>
        /// <param name="userInto"></param>
        /// <returns></returns>
        public ApiUser CreasteUser(ApiUser userInto)
        {
            long rowid = 0;
            string sql = @"INSERT INTO ApiUser(Name, BirthDate) VALUES (@Name, @BirthDate); ";

            using (var cnn = DBContext.DbConnection())
            {
                rowid = cnn.Query<long>(sql + " select last_insert_rowid(); ", userInto).First();
                userInto.Id = rowid.ToString();
            }
            // Calls to Task_MethodAsync
            Task returnedTask = Task_MethodAsync("CreasteUser");
            return userInto;
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="userInto"></param>
        /// <returns></returns>
        public ApiUser UpdateUser(ApiUser userInto)
        {
            string sql = @"UPDATE ApiUser SET Name = @Name, BirthDate = @BirthDate WHERE Id = @Id; ";

            using (var cnn = DBContext.DbConnection())
            {
                cnn.Query(sql, userInto);
            }
            // Calls to Task_MethodAsync
            Task returnedTask = Task_MethodAsync("UpdateUser");
            return userInto;
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userInto"></param>
        /// <returns></returns>
        public int DeleteUser(int Id)
        {
            int resp = 0;
            string sql = @"DELETE FROM ApiUser WHERE Id = @Id; ";

            using (var cnn = DBContext.DbConnection())
            {
                resp = cnn.Execute(sql, new { Id = Id });
            }
            // Calls to Task_MethodAsync
            Task returnedTask = Task_MethodAsync("DeleteUser");
            return resp;
        }

        /// <summary>
        /// Creates database schema
        /// </summary>
        internal static void SeedDatabase()
        {
            using (var cnn = DBContext.DbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table IF NOT EXISTS ApiUser
                      (
                        Id integer primary key,
                        Name varchar(100) not null,
                        BirthDate datetime null
                      ); ");

                //
                cnn.Execute(
                    @"create table IF NOT EXISTS ApiTrace
                      (
                        Id integer primary key,
                        Method varchar(100) not null,
                        HostName varchar(100) not null
                      ); ");
            }

        }


        internal static void SeedData()
        {
            if (!File.Exists(DBContext.DbFile))
            {
                SeedDatabase();
            }

            using (var cnn = DBContext.DbConnection())
            {
                string date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                cnn.Open();
                cnn.Execute(
                   @"INSERT INTO ApiUser 
                    (Name,BirthDate) VALUES 
                    ('User Test','" + date + "');");
                
            }
        }

        // Signature specifies Task
        async Task Task_MethodAsync(string Method)
        {
            // . . .
            // The method has no return statement.  
            // Trace
            trace _trace = new trace();
            _trace.Method = Method;
            _trace.clientAddress = HttpContext.Current.Request.UserHostAddress;

            using (var cnn = DBContext.DbConnection())
            {
                cnn.Open();
                string sql = @"INSERT INTO ApiTrace(Method, HostName) VALUES (@Method,@clientAddress); ";
                //cnn.Query(sql);
                long rowid = cnn.Query<long>(sql + " select last_insert_rowid(); ", _trace).First();
                
            }
        }

        // Signature specifies Task<TResult>
        async Task<int> TaskOfTResult_MethodAsync()
        {
            int hours;
            // . . .
            // Return statement specifies an integer result.
            hours = 20;
            return hours;
        }
        
    }

    internal class trace
    {
        public string Method { get; set; }
        public string clientAddress { get; set; }
    }
}