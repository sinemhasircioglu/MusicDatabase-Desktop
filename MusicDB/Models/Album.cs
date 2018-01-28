using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Barcode { get; set; }
        public bool IsSingle { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseYear { get; set; }
    }
}
