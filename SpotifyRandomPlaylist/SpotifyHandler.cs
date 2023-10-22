using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SpotifyRandomPlaylist.UserNS;
using System.Threading.Tasks;

namespace SpotifyRandomPlaylist
{
    internal class SpotifyHandler
    {
        private readonly Token _token;
        private readonly Random _random = new Random();
        private readonly HttpClient _client = new HttpClient();

        public SpotifyHandler(Token token)
        {
            _token = token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
            _client.BaseAddress = new Uri("https://api.spotify.com/v1/");
        }
        
        public string GetRandomSong()
        {
            string randomId = GetRandomId(3);
            HttpResponseMessage response = _client.GetAsync($"search?q={randomId}&type=track&limit=1").Result;
            if (response.IsSuccessStatusCode)
            {
                Search? rootobject = JsonSerializer.Deserialize<Search>(response.Content.ReadAsStringAsync().Result);
                if (rootobject!.tracks.items.Length > 0)
                {
                    return rootobject.tracks.items[0].id;
                }
                else
                {
                    return GetRandomSong();
                }
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
        private string GetRandomId(int length)
        {
            string randomId = "";
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < length; i++)
            {
                randomId += characters[_random.Next(characters.Length)];
            }
            return randomId;
        }
        public User GetUser()
        {
            HttpResponseMessage response = _client.GetAsync("me").Result;
            if (response.IsSuccessStatusCode)
            {
                User? user = JsonSerializer.Deserialize<User>(response.Content.ReadAsStringAsync().Result);
                return user!;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}
