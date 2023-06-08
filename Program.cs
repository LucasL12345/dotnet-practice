using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreetingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();

            Console.WriteLine($"Hello, {name}!");

            await GetApiResponse();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static async Task GetApiResponse()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://jsonplaceholder.typicode.com/posts/1";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("API Response:");
                        Console.WriteLine(responseData);
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
