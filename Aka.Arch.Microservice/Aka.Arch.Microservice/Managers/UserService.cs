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


    }
}