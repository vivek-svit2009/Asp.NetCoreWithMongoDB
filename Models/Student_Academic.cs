using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{
    public class Student_Academic
    {
        public int _id { get; set; }
        public string Class { get; set; }
        public string add_type { get; set; }
        public string session { get; set; }
        public string date { get; set; }
        public string current_status { get; set; }

    }
}
