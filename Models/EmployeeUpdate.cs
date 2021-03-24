using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{

    public class EmployeeUpdate
    {
        public String[] Field { get; set; }

        public String[] value { get; set; }

    }
}
