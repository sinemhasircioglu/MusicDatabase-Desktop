﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDB.Models
{
    public class Lyric
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public int SongId { get; set; }
    }
}
