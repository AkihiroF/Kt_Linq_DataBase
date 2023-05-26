namespace Kulikov_Valery_Linq_DataBase;

public static class MethodsAdded
{
    public static void AddPerson(this List<Person> persons, Person person)
    {
        persons.Add(person);
        FilesController.AddPersonToTable(person);
    }

    public static void GroupByLevel(this List<Person> persons)
    {
        persons  = persons.OrderBy(p => p.Level).ToList();
    }

    public static void GroupByName(this List<Person> persons)
    {
        persons  = persons.OrderBy(p => p.Name).ToList();
    }

    public static void PrintInformation(this List<Person> persons)
    {
        foreach (var person in persons)
        {
            if (person.SecretColor == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), person.SecretColor.ToString());
            }
            Console.WriteLine($"Person {person.Name} = {person.Level}");
        }

        Console.ResetColor();
    }
}