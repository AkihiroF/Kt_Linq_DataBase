using System.Net;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;


namespace Kulikov_Valery_Linq_DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var id = "1ZSIA-T6bqmDfq9XcDx8YqcX2qm-KdUfHHElJ8dmxRPQ";
            var name = "Levels";
            var url = $"http://gsx2json.com/api?id={id}&sheet={name}";
            SecretInfo[]? si;
            List<Person> per = new List<Person>();
            string json = "";
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse result = req.GetResponse();
                Stream rStream = result.GetResponseStream();
                StreamReader sr = new StreamReader(rStream);
                json = sr.ReadToEnd();
                sr.Dispose();
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            json = json.Split("\"rows\":")[1];
            json = json[..^1];
            si = JsonConvert.DeserializeObject<SecretInfo[]>(json);
            
            string dbPath = "/Users/akihirof/Documents/Projects/Kt_Linq_DataBase/Kulikov_Valery_Linq_DataBase/TopSecret.db";
            string connectionRequest = $"Data Source={dbPath}";
            using (SqliteConnection connection = new SqliteConnection(connectionRequest))
            {
                connection.Open();
                string tableName = "";
                SqliteCommand peopleRequest = new SqliteCommand($"select * from {tableName}", connection);
                using (SqliteDataReader reader = peopleRequest.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person();
                        person.Id = int.Parse(reader["Id"].ToString());
                        person.Name = reader["Name"].ToString();
                        person.Level = reader["Level"].ToString();
                        // = reader["SecretColor"].ToString();\
                        per.Add(person);
                    }
                }
            }

        }

    }
}