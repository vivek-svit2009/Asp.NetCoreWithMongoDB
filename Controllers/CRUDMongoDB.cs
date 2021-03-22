﻿using ASPWithMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class CRUDMongoDB : ControllerBase
    {
        private IMongoCollection<EmployeeDetails> _EmployeeCollection;

        public CRUDMongoDB(IMongoClient client)
        {
            var database = client.GetDatabase("DevHubs");
            _EmployeeCollection = database.GetCollection<EmployeeDetails>("EmployeeDetails");
        }

        // GET: api/<CRUDMongoDB>
        [HttpGet]
        public IEnumerable<EmployeeDetails> Get()
        {
            return _EmployeeCollection.Find(_ => true).ToList();
        }

        // GET api/<CRUDMongoDB>/5
        [HttpGet("{id}")]
        public IEnumerable<EmployeeDetails> Get(string id)
        {
            var idd = ObjectId.Parse(id);
            return _EmployeeCollection.Find(x => x.Id == idd).ToList();
        }

        // POST api/<CRUDMongoDB>
        [HttpPost]
        public IEnumerable<EmployeeDetails> Post([FromBody] EmployeeDetails emp)
        {
            var Addresses = new Address[]
             {
                 new Address()
                 {
                     Id = ObjectId.GenerateNewId(),
                     City = emp.Address[0].City
                 }
             };
            EmployeeDetails Emp = new EmployeeDetails();
            Emp.Name = emp.Name;
            Emp.Address = Addresses;
            _EmployeeCollection.InsertOne(Emp);
            return _EmployeeCollection.Find(_ => true).ToList();

        }

        // PUT api/<CRUDMongoDB>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<CRUDMongoDB>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
