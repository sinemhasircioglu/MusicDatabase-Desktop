using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB.Models
{
    public class Featuring
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
    }
}
