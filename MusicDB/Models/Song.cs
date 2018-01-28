using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB.Models
{
    public class Song
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public bool IsFeaturing { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

    }
}
