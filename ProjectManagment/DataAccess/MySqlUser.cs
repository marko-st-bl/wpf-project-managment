using MySql.Data.MySqlClient;
using ProjectManagment.DataAccess.Exceptions;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.DataAccess
{
    public class MySqlUser
    {
        private static readonly string SELECT_ONE = "SELECT id, firstName, lastName, username, password, type from user WHERE username=@username";
        private static readonly string SELECT_ALL_NON_MANAGER = "SELECT id, firstName, lastName, type FROM user WHERE type<>\"manager\"";

        public User GetUserByUsername(String username)
        {
            User result = null;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ONE;
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    result = new User()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Username = reader.GetString(3),
                        Password = reader.GetString(4),
                        Type = reader.GetString(5)
                };
                }
            }
            catch (Exception e)
            {
                throw new DataAccessException("Exception in MySqlUser", e);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public List<User> GetAllNonManagerUsers()
        {
            List<User> result = new List<User>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ALL_NON_MANAGER;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new User()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Type = reader.GetString(3)
                    });
                }
            }
            catch (Exception e)
            {
                throw new DataAccessException("Exception in MySqlUser", e);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }
    }
}
