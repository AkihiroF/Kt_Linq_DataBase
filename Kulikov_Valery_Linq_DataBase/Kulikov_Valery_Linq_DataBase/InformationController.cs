using System.Net;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace Kulikov_Valery_Linq_DataBase;

public class InformationController
{
    [Obsolete("Obsolete")]
    public void GetSecretInfos(string idSheet, string nameSheet, out List<SecretInfo>? secretInfos)
    {
        var url = $"http://gsx2json.com/api?id={idSheet}&sheet={nameSheet}";
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
        //Console.WriteLine(json);
        secretInfos = JsonConvert.DeserializeObject<List<SecretInfo>>(json);

    }

    public void GetPersons(out List<Person> persons, bool isNewTable = false)
    {
        persons = new List<Person>();
        string dbPath = isNewTable ? "Data/NewTopSecret.db":"TopSecret.db";
        string connectionRequest = $"Data Source={dbPath}";
        using (SqliteConnection connection = new SqliteConnection(connectionRequest))
        {
            connection.Open();
            //Console.WriteLine(connection.GetSchema());
            string tableName = "PeopleLevels";
            SqliteCommand peopleRequest = new SqliteCommand($"select * from {tableName}", connection);
            using (SqliteDataReader reader = peopleRequest.ExecuteReader())
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = int.Parse(reader["Id"].ToString());
                    person.Name = reader["Name"].ToString();
                    person.Level = reader["SecretLevel"].ToString();
                    persons.Add(person);
                }
            }
        }
    }

    public void SetColorInSecretInfo(ref List<SecretInfo> infos)
    {
        foreach (var info in infos)
        {
            info.secretColor = Enum.Parse<SecretColor>(info.color);
        }
    }

    public List<Person> SetSecretColors(List<Person> persons, List<SecretInfo>? info)
    {
        var a = from person in persons
            join infoSecret in info on Enum.Parse<SecretColor>(person.Level) equals infoSecret.secretColor
            select new Person()
            {
                Id = person.Id,
                Name = person.Name,
                Level = person.Level,
                SecretColor = infoSecret.secretColor
            };
        return a.ToList();
    }
    public List<Person> GroupPeople(List<Person> people, List<Person> newPeople) => 
        newPeople.Concat(people).DistinctBy(person => person.Id).ToList();


}