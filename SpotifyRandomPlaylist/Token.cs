using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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


        public static Token Authorize(string ClientId, string ClientSecret)
        {
            string url = $"https://accounts.spotify.com/authorize?response_type=code&client_id={ClientId}&redirect_uri=http%3A%2F%2Flocalhost%3A5543%2Fcallback&scope=playlist-modify-public%20playlist-modify-private%20playlist-read-private%20user-read-private";
            try
            {
                OpenBrowser(url);
            }
            catch
            {
                Console.WriteLine("Failed to open browser, please open the following url in your browser:\n" + url);
            }

            string code = ListenAnswer("http://localhost:5543/callback/");
            Token tokenObject = GetToken(ClientId, ClientSecret, code);

            return tokenObject;
        }
        private static void OpenBrowser(string url)
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

        private static string ListenAnswer(string url)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);

            listener.Start();

            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY>You are authorized. Close this window</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            listener.Stop();
            return request.QueryString["code"]!;
        }

        private static Token GetToken(string ClientId, string ClientSecret, string code)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")));

            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", "http://localhost:5543/callback" }
            });

            HttpResponseMessage response = client.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode)
            {
                Token? token = JsonSerializer.Deserialize<Token>(response.Content.ReadAsStringAsync().Result);
                return token!;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}

