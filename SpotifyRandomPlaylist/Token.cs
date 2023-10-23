﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
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


        public static Token Authorize(string ClientId)
        {
            string url = $"https://accounts.spotify.com/authorize?response_type=token&client_id={ClientId}&redirect_uri=http%3A%2F%2Flocalhost%3A5543%2Fcallback&scope=playlist-modify-public%20playlist-modify-private%20playlist-read-private%20user-read-private";
            try
            {
                OpenBrowser(url);
            }
            catch
            {
                Console.WriteLine("Failed to open browser, please open the following url in your browser:\n" + url);
            }

            string urlResult = Console.ReadLine();
            string token = urlResult.Split("access_token=")[1].Split("&")[0];
            Token tokenObject = new Token();
            tokenObject.access_token = token;
            tokenObject.token_type = "Bearer";
            tokenObject.expires_in = 3600;
            return tokenObject;
        }
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

