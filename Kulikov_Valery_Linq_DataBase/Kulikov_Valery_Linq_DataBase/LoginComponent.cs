namespace Kulikov_Valery_Linq_DataBase;

public class LoginComponent
{
    private List<Person> _dataPerson;
    private DataBaseComponent _dataBase;

    public LoginComponent(List<Person> dataPerson, DataBaseComponent dataBase)
    {
        this._dataPerson = dataPerson;
        _dataBase = dataBase;
    }

    private void InputLogin()
    {
        var input = Console.ReadLine();
        if (input.Length == 0)
        {
            Console.WriteLine("Ошибка! Ты шо, придурошный? Зачем ты отправляешь пустую строчку?");
            return;
        }

        var person = GetPerson(input);
        if(person == null)
            return;
        Console.WriteLine($"Уровень вашего доступа = {person.Level}\n" +
                          $"Информацию какого уровня вы хотите получить?");
        var inputLevel = int.Parse(Console.ReadLine());
        _dataBase.PrintInfoLevel(inputLevel);
    }

    private Person GetPerson(string name)
    {
        foreach (var person in _dataPerson)
        {
            if (person.Name == name)
                return person;
        }
        Console.WriteLine("Такого пользователя немае");
        return null;
    }
}