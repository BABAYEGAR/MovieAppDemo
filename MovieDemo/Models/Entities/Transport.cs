using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.Models.Entities
{
    public class Transport
    {
        public long? CreatedBy { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
