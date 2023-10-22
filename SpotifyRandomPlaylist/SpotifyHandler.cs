using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyRandomPlaylist
{
    internal static class SpotifyHandler
    {
        public static string Authorize(string ClientId, string ClientSecret)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
            return "";
        }
    }
}
