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
       
        public string ParentId { get; set; }
        
       
        public string ChildId { get; set; }

        public string queryType { get; set; }

        public string queryField { get; set; }

        public String[] Field { get; set; }

        public String[] value { get; set; }

    }
}
