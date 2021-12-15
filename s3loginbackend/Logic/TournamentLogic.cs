using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using s3loginbackend.Models;

namespace s3loginbackend.Logic
{

    public class TournamentLogic
    {
        MySqlDataReader reader;
        MySqlConnection databaseConnection = DbConn.connection();

        private string GetUsernameById(int userId)
        {
            string query = "SELECT username FROM users WHERE userid=@userid";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            //databaseConnection.Open();
            command.Parameters.AddWithValue("@userid", userId);
            reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    { 
                        string username = reader.GetString("username");
                        reader.Close();
                        return username;
                    }
                }
            }
            catch(Exception error)
            {
                return "Error";
            }
            return "Error";
        }

        private int GetIdByUsername(string userName)
        {
            string query = "SELECT userid FROM users WHERE username=@userName";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            //databaseConnection.Open();
            command.Parameters.AddWithValue("@userName", userName);
            reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int userid = reader.GetInt32("userid");
                        reader.Close();
                        return userid;
                    }
                }
            }
            catch (Exception error)
            {
                throw (error);
            }

            return 0;
        }

        public List<TournamentModel> GetAllTournaments()
        {
            string query = "SELECT tournamentid, organisor, tournamentdescription, winner FROM tournaments";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            List<TournamentModel> tournamentList = new List<TournamentModel>();
            databaseConnection.Open();
            reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tourid = reader.GetInt32("tournamentid");
                        var tourorg = reader.GetString("organisor");
                        var tourdesc = reader.GetString("tournamentdescription");
                        TournamentModel tournament = new TournamentModel(tourid, tourorg, tourdesc);
                        tournamentList.Add(tournament);
                    }
                }
                databaseConnection.Close();
                return tournamentList;
            }
            catch (Exception error)
            {
                databaseConnection.Close();
                return null;
            }
        }

        public List<UserModel> GetTournamentUsers(int tournamentId)
        {
            string query = "SELECT userid FROM usertournament WHERE tournamentid= @tournamentid";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            List<UserModel> userList = new List<UserModel>();
            List<int> userIdList = new List<int>();
            databaseConnection.Open();
            command.Parameters.AddWithValue("@tournamentid", tournamentId);
            reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userIdList.Add(reader.GetInt32("userid"));
                    }
                }
                reader.Close();
                foreach (var userId in userIdList)
                {
                    UserModel userModel = new UserModel(GetUsernameById(userId), userId);
                    userList.Add(userModel);
                }
                databaseConnection.Close();
                return userList;
            }
            catch(Exception error)
            {
                databaseConnection.Close();
                return null;
            }
        }

        public bool CreateTournament(string organisor, string tournamentDescription)
        {
            string query = "INSERT INTO tournaments (organisor, tournamentdescription) VALUES (@organisor, @tournamentdescription);";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@organisor", organisor);
            command.Parameters.AddWithValue("@tournamentdescription", tournamentDescription);

            command.ExecuteNonQuery();
            databaseConnection.Close();
            return true;
        }

        public bool AddPlayerToTournament(string userName, int tournamentId)
        {
            string query = "INSERT INTO usertournament (userid, tournamentid) VALUES (@userid, @tournamentid);";
            MySqlCommand command = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            command.Parameters.AddWithValue("@userid", GetIdByUsername(userName));
            command.Parameters.AddWithValue("@tournamentid", tournamentId);

            command.ExecuteNonQuery();
            databaseConnection.Close();
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
            databaseConnection.Close();
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
                            databaseConnection.Close();
                            return userModel;
                        }
                        else
                        {
                            databaseConnection.Close();
                            return null;
                        }
                    }
                }
                else
                {
                    databaseConnection.Close();
                    return null;
                }
            }
            catch
            {
                databaseConnection.Close();
                return null;
            }
            databaseConnection.Close();
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
            databaseConnection.Close();
            return command.ExecuteNonQuery();
        }
    }
}
