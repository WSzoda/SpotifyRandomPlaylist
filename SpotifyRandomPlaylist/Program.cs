// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
