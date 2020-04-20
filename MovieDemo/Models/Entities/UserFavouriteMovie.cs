using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace MovieDemo.Models.Entities
{
    public class UserFavouriteMovie : Transport
    {
        public long UserFavouriteMovieId { get; set; }
        public long MovieId { get; set; }
        public long AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [JsonIgnore]
        public AppUser AppUser { get; set; }
    }
}
