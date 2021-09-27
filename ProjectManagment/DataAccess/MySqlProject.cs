using MySql.Data.MySqlClient;
using ProjectManagment.DataAccess.Exceptions;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = ProjectManagment.Models.Task;

namespace ProjectManagment.DataAccess
{
    public class MySqlProject
    {
        public static readonly string INSERT = "INSERT INTO project(title, manager_id) VALUES (@title, @manager_id)";
        public static readonly string DELETE = "DELETE FROM project WHERE id=@id";
        public static readonly string SELECT_BY_MANAGER_ID = "SELECT id, title, manager_id FROM project WHERE manager_id=@manager_id";

        public void AddProject(Project project)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@title",project.Title);
                cmd.Parameters.AddWithValue("@manager_id", project.ManagerId);
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

        public List<Project> GetProjectsByManagerId(int manager_id)
        {
            List<Project> result = new List<Project>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_BY_MANAGER_ID;
                cmd.Parameters.AddWithValue("@manager_id", manager_id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Project()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        ManagerId = reader.GetInt32(2)
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

        public void DeleteProjectById(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlTask mySqlTask = new MySqlTask();
            List<Task> tasks = mySqlTask.GetTasksByProjectId(id);
            tasks.ForEach(task => mySqlTask.DeleteTask(task.Id));
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@id", id);
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
