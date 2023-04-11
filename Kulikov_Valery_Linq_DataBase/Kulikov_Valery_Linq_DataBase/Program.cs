using System.Net;
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
        }

    }
}