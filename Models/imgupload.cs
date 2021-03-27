using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{
    public class imgupload
    {
        public IFormFile DpImg { get; set; }
    }
}
