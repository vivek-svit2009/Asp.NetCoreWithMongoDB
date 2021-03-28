﻿using ASPWithMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPWithMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlToMongoMigrate : ControllerBase
    {
        private IMongoCollection<Student_Basic> _Student_BasicCollection;

        public SqlToMongoMigrate(IMongoClient client)
        {
            var database = client.GetDatabase("svicDB");
            _Student_BasicCollection = database.GetCollection<Student_Basic>("Student_Basic");
        }
        // GET: api/<SqlToMongoMigrate>
        [HttpGet]
        public IEnumerable<Student_Basic> Get()
        {
           /* SqlConnection con = new SqlConnection("Data Source=LAPTOP-T89MOPER\\SQLEXPRESS;Initial Catalog=svicBak;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from Student_Basic", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student_Basic sb = new Student_Basic()
                    {
                        _id = Convert.ToInt32(dr["Sr_No"].ToString()),
                        Student_Name = dr["Student_Name"].ToString(),
                        Father_Name = dr["Father_Name"].ToString(),
                        Mother_Name = dr["Mother_Name"].ToString(),
                        old_school = dr["old_school"].ToString(),
                        DOB = dr["DOB"].ToString(),
                        Religion = dr["Religion"].ToString(),
                        Category = dr["Category"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Mobile_No = dr["Mobile_No"].ToString(),
                        Address = dr["Address"].ToString(),
                        Pic = dr["Pic"].ToString(),
                        date = dr["date"].ToString(),
                        session = dr["session"].ToString(),
                        status = dr["status"].ToString(),
                        Adhaar = dr["Adhaar"].ToString(),
                        form_no = dr["form_no"].ToString(),
                        lastclass = dr["lastclass"].ToString(),
                        dol = dr["dol"].ToString(),
                        foradd = dr["foradd"].ToString(),
                        fromadd = dr["fromadd"].ToString(),
                        caste = dr["caste"].ToString(),
                        birthplace = dr["birthplace"].ToString(),
                        Subjects = new Student_Subject[] { },
                        Academics = new Student_Academic[] { }

                    };
                    _Student_BasicCollection.InsertOne(sb);
                }
                dr.Close();

                SqlCommand cmd1 = new SqlCommand("Select * from Student_Academic", con);

                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        var id = Convert.ToInt32(dr1["Sr_No"].ToString()); // your document Id
                        var builder = Builders<Student_Basic>.Filter;
                        var filter = builder.Eq(x => x._id, id);
                        var update = Builders<Student_Basic>.Update
                            .AddToSet(x => x.Academics, new Student_Academic
                            {
                                _id = id,
                                Class = dr1["Class"].ToString(),
                                add_type = dr1["add_type"].ToString(),
                                session = dr1["session"].ToString(),
                                date = dr1["date"].ToString(),
                                current_status = dr1["current_status"].ToString()
                            });
                        _Student_BasicCollection.UpdateOne(filter, update);

                    }
                    dr1.Close();
                    SqlCommand cmd2 = new SqlCommand("Select * from Student_Subject", con);

                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            var id = Convert.ToInt32(dr2["Sr_No"].ToString()); // your document Id
                            var builder = Builders<Student_Basic>.Filter;
                            var filter = builder.Eq(x => x._id, id);
                            var update = Builders<Student_Basic>.Update
                                .AddToSet(x => x.Subjects, new Student_Subject
                                {
                                    _id = id,
                                    Class = dr2["Class"].ToString(),
                                    Session = dr2["Session"].ToString(),
                                    Date = dr2["Date"].ToString(),
                                    Sub1 = dr2["s1"].ToString(),
                                    Sub2 = dr2["s2"].ToString(),
                                    Sub3 = dr2["s3"].ToString(),
                                    Sub4 = dr2["s4"].ToString(),
                                    Sub5 = dr2["s5"].ToString(),
                                    Sub6 = dr2["s6"].ToString(),
                                    Sub7 = dr2["s7"].ToString(),
                                    Sub8 = dr2["s8"].ToString(),
                                    Sub9 = dr2["s9"].ToString()
                                });
                            _Student_BasicCollection.UpdateOne(filter, update);

                        }
                        dr2.Close();
                    }
                }

            }

            con.Close();*/

            return _Student_BasicCollection.Find(_ => true).ToList();

        }

        // GET api/<SqlToMongoMigrate>/5
        [HttpGet("{id}/{clas}")]
        public IActionResult Get(int id,string clas)
        {
            /*Filter sepcific document*/


            List<Student_Basic> lst =  _Student_BasicCollection.Aggregate()
           .Match(Builders<Student_Basic>.Filter.Eq(x => x._id, id) & Builders<Student_Basic>.Filter.ElemMatch(x => x.Academics, Builders<Student_Academic>.Filter.Eq(x => x.Class, clas)) & Builders<Student_Basic>.Filter.ElemMatch(x => x.Subjects, Builders<Student_Subject>.Filter.Eq(x => x.Class, clas)))
           .AppendStage<Student_Basic>(BsonDocument.Parse(@"{ $addFields: { ""Academics"" : { $filter: { input: ""$Academics"", as: ""item"", cond: { $eq: [""$$item.Class"", '"+clas+"'] } } } } }"))
           .AppendStage<Student_Basic>(BsonDocument.Parse(@"{ $addFields: { ""Subjects"" : { $filter: { input: ""$Subjects"", as: ""item"", cond: { $eq: [""$$item.Class"", '" + clas + "'] } } } } }"))
           .ToList();
            if (lst.Count > 0)
            {
                return Ok(lst);
            }
            else
            {
                return NotFound(new { response = "Not found any data for"+ id });
            }

        }

        // POST api/<SqlToMongoMigrate>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SqlToMongoMigrate>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SqlToMongoMigrate>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
