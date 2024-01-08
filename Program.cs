using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MySql.Data.MySqlClient;

{
namespace overzetten
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=csharp_novice;Uid=root;Pwd=test;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(100), gender VARCHAR(10), age INT, favorite_color VARCHAR(50))";
                createTableCommand.ExecuteNonQuery();
                MySqlCommand createTableCommand = new MySqlCommand(createTableQuery, connection);

                string jsonFilePath = "users.json";
                var jsonString = File.ReadAllText(jsonFilePath);

                var users = JsonSerializer.Deserialize<List<User>>(jsonString);

                int usersAdded = 0;

                foreach (User user in users)
                {
                    string insertQuery = "INSERT INTO Users (name, gender, age, favorite_color) VALUES (@name, @gender, @age, @FavColor)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@name", user.Name);
                    insertCommand.Parameters.AddWithValue("@gender", user.Gender);
                    insertCommand.Parameters.AddWithValue("@age", user.Age);
                    insertCommand.Parameters.AddWithValue("@FavColor", user.FavColor);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        usersAdded++;
                    }
                }

                Console.WriteLine($"{usersAdded} gebruikers toegevoegd.");
            }
        }
    }
}