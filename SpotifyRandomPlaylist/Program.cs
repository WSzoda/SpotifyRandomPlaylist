using Microsoft.Extensions.Configuration;
using SpotifyRandomPlaylist;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

IConfiguration configuration = builder.Build();

string ClientId = configuration.GetValue<string>("ClientId")!;
string ClientSecret = configuration.GetValue<string>("ClientSecret")!;

if(ClientId == "" ||  ClientSecret == "")
{
    Console.WriteLine("Missing ClientId or ClientSecret");
    return;
}

string token = SpotifyHandler.Authorize(ClientId, ClientSecret);