# Spotify Random Playlist
## Description
The project is an application written in C# with the aim of generating random playlists on the Spotify platform. 
It allows users to quickly create unique sets of music by adding a touch of randomness to their music experience. 
This tool enables users to discover new songs and genres that can surprise and provide fresh musical experiences.
## Installation
Clone the Repository: Begin by cloning this repository to your local machine using the following command:
```
https://github.com/WSzoda/SpotifyRandomPlaylist.git
```
Build the Project: Open the solution file in Visual Studio or use the command line to build the project:
```
dotnet restore
dotnet build
```
Configuration
Before running the program, you need to configure the connection to spotify. Create app in Spotify Developer dashboard. Create copy file named config.json and paste config from below.
```
{
  "ClientId": "",
  "ClientSecret": "",
  "RedirectUri": ""
}
```
Replace the placeholder values with your specific ClientId, ClientSecret and RedirectUri from Spotify dashboard.

Usage
After configuring the config.json file, you can run the program using the following command:
```
dotnet run
```
Program will connect to Spotify API, select ten random songs and create new playlist containing them.
