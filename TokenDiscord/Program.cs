using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TokenDiscord
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Ingrese Correo: ");
            string email = Console.ReadLine();
            Console.Write("Ingrese Contrasena: ");
            string pass = Console.ReadLine();
            await getToken(email, pass);
        }

        async static Task getToken(string email, string pass)
        {
            string url = "https://discord.com/api/v9/auth/login";
            var payload = new Dictionary<string, string>()
            {
                { "login", email }, { "password", pass }
            };
            string jsonPayload = JsonConvert.SerializeObject(payload);
            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nSesion Iniciada!");
                string responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseContent);
                JObject json = JObject.Parse(responseContent);
                Console.WriteLine($"ID Usuario: {json["user_id"]}");
                Console.WriteLine($"Token: {json["token"]}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
            }
        }
    }
}
