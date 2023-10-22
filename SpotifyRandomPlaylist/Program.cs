using Microsoft.Extensions.Configuration;
using SpotifyRandomPlaylist;
using SpotifyRandomPlaylist.UserNS;

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

Token token = Token.Authorize(ClientId, ClientSecret);
SpotifyHandler spotifyHandler = new SpotifyHandler(token);

List<string> songIds = new List<string>();
for(int i = 0; i < 10; i++)
{
    songIds.Add(spotifyHandler.GetRandomSong());
}
User user = spotifyHandler.GetUser();

Console.WriteLine(user.id);