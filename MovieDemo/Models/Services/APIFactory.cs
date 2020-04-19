using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MovieDemo.Models.Entities;
using Newtonsoft.Json;

namespace MovieDemo.Models.Services
{
    public class APIFactory
    { 
        /// <summary>
    /// This method retrieves the list of movies from a specified base url
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
        public async Task<List<Movie>> GetAllMovies(string baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));
            List<Movie> movies = new List<Movie>();
            try
            {
                using (var httpClient = new HttpClient()) 
                {
                    //do the actual request and await response
                    httpClient.DefaultRequestHeaders.Add("Accept","application/json");

                    //fetch response from base url
                    var httpResponse = await httpClient.GetAsync(baseUrl);
                    if (httpResponse != null)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            //get response json string
                            var stringData = httpResponse.Content.ReadAsStringAsync().Result;

                            //deserialize json string to movie object
                            var fetchedMovies =
                                await Task.Run(() => JsonConvert.DeserializeObject<List<Movie>>(stringData));
                            movies = fetchedMovies;
                            return movies;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return movies;
        }
    }
}
