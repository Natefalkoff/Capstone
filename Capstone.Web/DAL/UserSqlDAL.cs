﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserSqlDAL
    {
        private string connectionString;
        private const string registerUserSql = "INSERT into website_user VALUES (@user_name, @password, @email, @authorization)";
        private const string loginUserSql = "SELECT* FROM website_user WHERE user_name = @username";

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool RegisterUser(UserModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(registerUserSql, conn);
                    cmd.Parameters.AddWithValue("@user_name", model.UserName);
                    cmd.Parameters.AddWithValue("@password", model.Password);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@authorization", model.AuthorizationLevel);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }

            }

            catch (SqlException e)
            {
                throw new NotImplementedException();
            }
        }

        public UserModel GetUser(string username)
        {
            UserModel result = new UserModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {   
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(loginUserSql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.UserName = reader["user_name"].ToString();
                        result.Password = reader["password"].ToString();
                    }

                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}