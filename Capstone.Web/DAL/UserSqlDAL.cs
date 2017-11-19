using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserSqlDAL
    {
        private string connectionString;
        private const string registerUserSql = "INSERT into website_user VALUES (@user_name, @password, @email, @authorization)";

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

    }
}