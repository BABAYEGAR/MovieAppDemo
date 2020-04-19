using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.Models.Entities
{
    public class Movie
    {
        public string overview { get; set; }
        public double voteAverage { get; set; }
        public string title { get; set; }
        public int[] releaseDate { get; set; }
        public long runtime { get; set; }
        public List<Genre> genres { get; set; }
        public long id { get; set; }
        public List<Video> videos { get; set; }
        public decimal budget { get; set; }
        public string posterPath { get; set; }
        public string status { get; set; }
        public string homepage { get; set; }
    }
}
