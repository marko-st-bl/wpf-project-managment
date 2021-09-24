using MySql.Data.MySqlClient;
using ProjectManagment.DataAccess.Exceptions;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagment.DataAccess
{
    public class MySqlTask
    {
        private static string INSERT = "INSERT INTO task (title, description, isDone) VALUES (@title, @description, @isDone)";
        private static string INSERT_BLANK = "INSERT INTO task";
        private static string INSERT_INTO_PROJECT_HAS_TASK = "INSERT INTO project_has_task (project_id, task_id) VALUES (@project_id, @task_id)";
        private static string SELECT_BY_PROJECT_ID = "SELECT t.id, title, description, isDone, u.id, firstName, lastName FROM task t INNER JOIN project_has_task pht ON t.id=pht.task_id LEFT OUTER JOIN user u ON pht.user_id=u.id WHERE project_id=@project_id";
        private static string SELECT_BY_USER_ID = "";
        private static string UPDATE = "UPDATE TASK SET title=@title, description=@description, isDone=@isDone WHERE id=@id";
        private static string ASSIGN_TASK_TO_USER = "UPDATE project_has_task SET user_id=@user_id WHERE project_id=@project_id AND task_id=@task_id";
        private static string DELETE_FROM_PROJECT_HAS_TASK = "DELETE FROM project_has_task WHERE task_id=@task_id";
        private static string DELETE = "DELETE FROM task WHERE id=@task_id";

        public void AddTask(Task task, int project_id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@isDone", task.IsDone);
                cmd.ExecuteNonQuery();

                int id = (int)cmd.LastInsertedId;

                cmd.CommandText = INSERT_INTO_PROJECT_HAS_TASK;
                cmd.Parameters.AddWithValue("@project_id", project_id);
                cmd.Parameters.AddWithValue("@task_id", id);
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

        public Task AddBlankTask(int project_id)
        {
            Task result = null;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@title", "New Task");
                cmd.Parameters.AddWithValue("@description", "Description...");
                cmd.Parameters.AddWithValue("@isDone", 0);
                cmd.ExecuteNonQuery();

                int id = (int)cmd.LastInsertedId;

                cmd.CommandText = INSERT_INTO_PROJECT_HAS_TASK;
                cmd.Parameters.AddWithValue("@project_id", project_id);
                cmd.Parameters.AddWithValue("@task_id", id);
                cmd.ExecuteNonQuery();

                result = new Task()
                {
                    Id = id,
                    Title = "New Task",
                    Description = "Description...",
                    IsDone = false
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlGroup", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
            return result;
        }

        public void UpdateTask(Task task)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@id", task.Id);
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@IsDone", task.IsDone);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlTask", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public void AssignTaskToUser(int task_id, int project_id, int user_id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = ASSIGN_TASK_TO_USER;
                cmd.Parameters.AddWithValue("@task_id", task_id);
                cmd.Parameters.AddWithValue("@project_id", project_id);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlTask", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Task> GetTasksByProjectId(int project_id)
        {
            List<Task> result = new List<Task>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_BY_PROJECT_ID;
                cmd.Parameters.AddWithValue("@project_id", project_id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Task newTask = new Task()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        IsDone = reader.GetBoolean(3)
                    };
                    if (!reader.IsDBNull(4))
                    {
                        newTask.Assignee = new User() { Id = reader.GetInt32(4), FirstName = reader.GetString(5), LastName = reader.GetString(6) };
                    }
                    result.Add(newTask);
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlGroup", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void DeleteTask(int task_id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE_FROM_PROJECT_HAS_TASK;
                cmd.Parameters.AddWithValue("@task_id", task_id);
                cmd.ExecuteNonQuery();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@id", task_id);
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
