using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace DatApp.ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var client = new HttpClient();

            // grab a bearer token using ClientCredentials
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // grab a bearer token
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);
            Console.WriteLine("\n\n");

            client.SetBearerToken(response.AccessToken);

            var valuesResposne = await client.GetAsync("http://localhost:9000/api/values/claims");

            if (!valuesResposne.IsSuccessStatusCode)
            {
                Console.WriteLine(valuesResposne.StatusCode);
            }
            else
            {
                var content = await valuesResposne.Content.ReadAsStringAsync();

                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
