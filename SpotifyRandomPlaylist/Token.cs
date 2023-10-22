using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyRandomPlaylist
{
    internal class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }


        public static Token Authorize(string ClientId, string ClientSecret)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")));

            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials" }
            });
            HttpResponseMessage response = client.PostAsync("", content).Result;

            if (response.IsSuccessStatusCode)
            {
                Token? token = JsonSerializer.Deserialize<Token>(response.Content.ReadAsStringAsync().Result);
                client.Dispose();
                return token!;
            }
            else
            {
                client.Dispose();
                throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}

