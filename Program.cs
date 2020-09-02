using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var name = HttpUtility.HtmlEncode("Luke Skywalker");
            var people = await GetTAsync<Response<Person>>($"https://swapi.dev/api/people/?search={name}");
            var person = people.Results.Single();
            var planet = await GetTAsync<Planet>(person.Homeworld);
            var films = new List<Film>();
            foreach (var filmurl in person.Films)
            {
                films.Add(await GetTAsync<Film>(filmurl));
            }
            //Console.WriteLine($"{person.Name} - {planet.Name} - {string.Join(" ,",films.Select(x=>x.Title))}");
            // Console.ReadLine();
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\gyzvi\OneDrive\Рабочий стол\Test\SATWARS.txt"))
            {
                file.WriteLine(DateTime.UtcNow);
                file.WriteLine($"{person.Name} - {planet.Name} - {string.Join(" ,", films.Select(x => x.Title))}");
            }
        }
        static async Task<T> GetTAsync<T>(string url) 
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var result = JsonConvert.DeserializeObject<T>(responseBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }
        );
            return result;
        }
    }
}
