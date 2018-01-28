using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool IsGroup { get; set; }
        public int StartedYear { get; set; }
        public string RealName { get; set; }
    }
}
