using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.Models.Entities
{
    public class Video
    {
        public string site { get; set; }
        public string name { get; set; }
        public long id { get; set; }
        public string type { get; set; }
        public string key { get; set; }
    }
}
