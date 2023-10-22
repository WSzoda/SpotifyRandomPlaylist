using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyRandomPlaylist
{
    internal class PlaylistCreate
    {
        public List<string> uris { get; set; } = new List<string>();
        public int position { get; set; } = 0;
    }
}
