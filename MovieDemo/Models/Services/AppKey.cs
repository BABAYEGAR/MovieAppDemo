using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.Models.Services
{
    public class AppKey
    {
        public string FetchAllMoviesUrl => "https://wootlab-moviedb.herokuapp.com/api/movie/list/all";
    }
}
