namespace SpotifyRandomPlaylist.Data
{
    internal class PlaylistCreate
    {
        public List<string> uris { get; set; } = new List<string>();
        public int position { get; set; } = 0;
    }
}
