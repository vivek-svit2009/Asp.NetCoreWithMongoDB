using Microsoft.Graph;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{
    public class Student_Basic
    {
        
        public int _id { get; set; }

        public string Student_Name { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }

        public string old_school { get; set; }
        public string DOB { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string Mobile_No { get; set; }
        public string Address { get; set; }
        public string Pic { get; set; }
        public string date { get; set; }
        public string session { get; set; }
        public string status { get; set; }
        public string Adhaar { get; set; }
        public string form_no { get; set; }
        public string lastclass { get; set; }

        public string dol { get; set; }
        public string foradd { get; set; }
        public string fromadd { get; set; }
        public string caste { get; set; }
        public string birthplace { get; set; }
        public Student_Subject[] Subjects { get; set; }
        public Student_Academic[] Academics { get; set; }

    }
}
