using Microsoft.Data.Sqlite;

namespace Kulikov_Valery_Linq_DataBase;

public class DataBaseComponent
{
    private List<SecretInfo> _dataSecret;

    public DataBaseComponent(List<SecretInfo> dataSecret)
    {
        _dataSecret = dataSecret;
    }

    public void PrintInfoLevel(int lvl)
    {
        foreach (var info in _dataSecret)
        {
            if (info.level <= lvl)
            {
                Console.WriteLine(info.info);
            }
        }
    }
}