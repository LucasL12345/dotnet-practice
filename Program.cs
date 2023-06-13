using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Polly;

namespace GreetingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await GetApiResponse();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static async Task GetApiResponse()
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/posts/1";

            var policy = Policy.Handle<HttpRequestException>().RetryAsync(3);

            HttpResponseMessage response = await policy.ExecuteAsync(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage res = await client.GetAsync(apiUrl);
                    return res;
                }
            });

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                Post post = JsonSerializer.Deserialize<Post>(responseData);

                Console.WriteLine("API Response:");
                Console.WriteLine(responseData);
                Console.WriteLine($"Title: {post.Title}");
            }
            else
            {
                Console.WriteLine($"API request failed with status code {response.StatusCode}");
            }
        }
    }

    public class Post
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }
    }
}
