using Microsoft.Data.Sqlite;

namespace Kulikov_Valery_Linq_DataBase;

public static class FilesController
{
    // public static void CreateNewSheet(List<Person> persons, List<SecretInfo> infos)
    // {
    //     using (var writer = new StreamWriter("newPersons.csv"))
    //     {
    //         using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //         {
    //             csv.WriteHeader<Person>();
    //             csv.NextRecord();
    //             var currentPerson = (from person in persons
    //                 join info in infos on person.Level equals info.level.ToString()
    //                 select new Person()
    //                 {
    //                     Id = person.Id,
    //                     Name = person.Name,
    //                     Level = info.level.ToString(),
    //                     SecretColor = person.SecretColor
    //                 }).ToList();
    //             csv.WriteRecords(currentPerson);
    //         }
    //         writer.Dispose();
    //     }
    // }

    public static void AddPersonToTable(Person person)
    {
        string dbPath = "Data/NewTopSecret.db";
        string connectionRequest = $"Data Source={dbPath}";
        using (SqliteConnection connection = new SqliteConnection(connectionRequest))
        {
            connection.Open();

            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                SqliteCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandText = 
                    "INSERT OR IGNORE INTO NewPeopleLevels(Level, Name, SecretColor) " +
                    "VALUES($Level, $Name, $SecretColor)";
                
                var levelParam = insertCommand.CreateParameter();
                levelParam.ParameterName = "$Level";
                levelParam.Value = person.Level;
                insertCommand.Parameters.Add(levelParam);
                
                var nameParam = insertCommand.CreateParameter();
                nameParam.ParameterName = "$Name";
                nameParam.Value = person.Name;
                insertCommand.Parameters.Add(nameParam);
                
                var secretColorParam = insertCommand.CreateParameter();
                secretColorParam.ParameterName = "$SecretColor";
                secretColorParam.Value = person.SecretColor;
                insertCommand.Parameters.Add(secretColorParam);
                
                insertCommand.ExecuteNonQuery();
                
                transaction.Commit();
            }
        }
    }
    
    public static void CreateNewTable(List<Person> people)
    {
        string dbPath = "Data/NewTopSecret.db";
        string connectionRequest = $"Data Source={dbPath}";
        using (SqliteConnection connection = new SqliteConnection(connectionRequest))
        {
            connection.Open();
            string createQuery = 
                "CREATE TABLE IF NOT EXISTS NewPeopleLevels (Id INTEGER PRIMARY KEY AUTOINCREMENT, Level TEXT NOT NULL, Name TEXT NOT NULL, SecretColor TEXT NOT NULL)";
            SqliteCommand createCommand = new SqliteCommand(createQuery, connection);
            createCommand.ExecuteNonQuery();

            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                SqliteCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandText = 
                    "INSERT OR IGNORE INTO NewPeopleLevels(Level, Name, SecretColor) " +
                    "VALUES($Level, $Name, $SecretColor)";
                
                var levelParam = insertCommand.CreateParameter();
                levelParam.ParameterName = "$Level";
                insertCommand.Parameters.Add(levelParam);
                
                var nameParam = insertCommand.CreateParameter();
                nameParam.ParameterName = "$Name";
                insertCommand.Parameters.Add(nameParam);
                
                var secretColorParam = insertCommand.CreateParameter();
                secretColorParam.ParameterName = "$SecretColor";
                insertCommand.Parameters.Add(secretColorParam);
                
                foreach (var person in people)
                {
                    levelParam.Value = person.Level;
                    nameParam.Value = person.Name;
                    secretColorParam.Value = person.SecretColor.ToString();
                        
                    insertCommand.ExecuteNonQuery();
                }
                
                transaction.Commit();
            }
        }

    }

    public static void DeletePersonFromTable(string requirement)
    {
        string dbPath = "Data/NewTopSecret.db";
        string connectionRequest = $"Data Source={dbPath}";
        using (SqliteConnection connection = new SqliteConnection(connectionRequest))
        {
            connection.Open();

            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                SqliteCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandText = requirement;
                insertCommand.ExecuteNonQuery();
                
                transaction.Commit();
            }
        }
    }
    
}