using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using s3loginbackend.Models;

namespace s3loginbackend.Logic
{
    public class LoginLogic
    {
        MySqlDataReader reader;
        MySqlConnection databaseConnection = DbConn.connection();

        public bool CreateUser(string username, string password)
        {
            string query = "INSERT INTO users (username, password) VALUES (@username, @password);";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteNonQuery();
            return true;
        }

        public int UpdateUser(string username, string password, string newUsername, int userId)
        {
            string query = "UPDATE users SET username = @newUsername WHERE userid = @userId AND username = @username AND password = @password;";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@newUsername", newUsername);
            return command.ExecuteNonQuery();
        }

        public UserModel Login(string username, string password)
        {
            string query = "SELECT * FROM users WHERE username=@username;";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@username", username);
            reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string DbPassword = reader.GetString("password");
                        if (password == DbPassword)
                        {
                            int userId = reader.GetInt32("userid");
                            UserModel userModel = new UserModel(username, userId);
                            return userModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public int DeleteUser(string username, string password, int userId)
        {
            string query = "DELETE FROM users WHERE userid = @userId AND username = @username AND password = @password;";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@userId", userId);
            return command.ExecuteNonQuery();
        }
    }
}
