namespace Kulikov_Valery_Linq_DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var infoController = new InformationController();
            var id = "1ZSIA-T6bqmDfq9XcDx8YqcX2qm-KdUfHHElJ8dmxRPQ";
            var name = "Levels";
            List<SecretInfo> si;
            List<Person> per;
            infoController.GetSecretInfos(id, name, out si);
            infoController.SetColorInSecretInfo(ref si);
            infoController.GetPersons(out per);
            per = infoController.SetSecretColors(per, si); // 1
            
            per.GroupByLevel();
            per.PrintInformation();// 2
            
            var dataBase = new DataBaseComponent(si);
            var loginCom = new LoginComponent(per, dataBase); // 3
            
            
            FilesController.CreateNewTable(per); // 4

            
            var newPerson = new Person()
            {
                Id = 12103,
                Name = "kfsdklds fdsnls",
                Level = "3",
                SecretColor = SecretColor.Green
            };
            per.AddPerson(newPerson); // 5
            
            FilesController.DeletePersonFromTable(""); // 6
            
            
            List<Person> newPer;
            infoController.GetPersons(out newPer, true);
            newPer.GroupByName();
            newPer.PrintInformation(); //7

            var groupedPersons = infoController.GroupPeople(per, newPer); //8
        }
        
    }
}