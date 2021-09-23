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
    public class MySqlSettings
    {
        public static readonly string INSERT = "INSERT INTO settings (user_id, darkMode, primaryColor, language) VALUES (@user_id, @darkMode, @primaryColor, @language)";
        public static readonly string SELECT_ONE = "SELECT user_id, darkMode, primaryColor, language FROM settings WHERE user_id=@user_id";
        public static readonly string UPDATE = "UPDATE settings SET darkMode=@darkMode, primaryColor=@primaryColor, language=@language WHERE user_id=@user_id";

        public Settings GetSettingsByUserId(int user_id)
        {
            Settings result = null;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ONE;
                cmd.Parameters.AddWithValue("@user_id", user_id);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = new Settings()
                    {
                        User_Id = reader.GetInt32(0),
                        DarkMode = reader.GetBoolean(1),
                        PrimaryColor = reader.GetString(2),
                        Language = reader.GetString(3)          
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

        public void SaveSettings(Settings settings)
        {
            Settings settingsTest = GetSettingsByUserId(settings.User_Id);
            if(settingsTest == null)
            {
                InsertSettings(settings);
            }
            else
            {
                UpdateSettings(settings);
            }
        }

        private void InsertSettings(Settings settings)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@user_id", settings.User_Id);
                cmd.Parameters.AddWithValue("@darkMode", settings.DarkMode);
                cmd.Parameters.AddWithValue("@primaryColor", settings.PrimaryColor);
                cmd.Parameters.AddWithValue("@language", settings.Language);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlGroup", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        private void UpdateSettings(Settings settings)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@user_id", settings.User_Id);
                cmd.Parameters.AddWithValue("@darkMode", settings.DarkMode);
                cmd.Parameters.AddWithValue("@primaryColor", settings.PrimaryColor);
                cmd.Parameters.AddWithValue("@language", settings.Language);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlGroup", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }
    }
}
