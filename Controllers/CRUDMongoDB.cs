using ASPWithMongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        /*     [HttpPost]
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

             }*/
        [HttpPost]
        public IEnumerable<EmployeeDetails> Post(IFormFile files,[FromForm]string empdata)
        {
            var id = ObjectId.Parse("605764c67d2e861b2cb9d1fd");
            
            EmployeeDetails empList = JsonConvert.DeserializeObject<EmployeeDetails>(empdata);

            Debug.WriteLine("City"+ empList.Address[0].City);
            Debug.WriteLine("City" + empList.Name);
            // Full path to file in temp location
            var filePath = Path.GetTempFileName();

                if (files.Length > 0)
                {
                    var Addresses = new Address[]
                          {
                                  new Address()
                                  {
                                      Id = ObjectId.GenerateNewId(),
                                      City = empList.Address[0].City
                                  }
                          };
                    EmployeeDetails Emp = new EmployeeDetails();
                    Emp.Name = empList.Name;
                    Emp.Address = Addresses;
                    _EmployeeCollection.InsertOne(Emp);
                    return _EmployeeCollection.Find(_ => true).ToList();
                 }
                    /*using (var stream = new FileStream(filePath, FileMode.Create))
                        await formFile.CopyToAsync(stream);*/
            

            // Process uploaded files

            return _EmployeeCollection.Find(_ => true).ToList();
        }



        // PUT api/<CRUDMongoDB>/5
        [HttpPut]
        public IEnumerable<EmployeeDetails> Put([FromBody] EmployeeUpdate emp)
        {
            if(emp.queryType == "Parant")
            {
                var id = ObjectId.Parse(emp.ParentId);
                int i;
                var filter = Builders<EmployeeDetails>.Filter;
                var empployeeId = filter.And(
                    filter.Eq(x => x.Id, id));
                var employee = _EmployeeCollection.Find(empployeeId).SingleOrDefault();
                var updatee = Builders<EmployeeDetails>.Update;
                var updateValue = new List<UpdateDefinition<EmployeeDetails>>();
                int count = emp.Field.Count();

                for (i = 0; i < count; i++)
                {
                    updateValue.Add(updatee.Set(emp.Field[i], emp.value[i]));
                    //nameupdate = nameupdate+".Set(\""+emp.Field[i]+"\":\""+emp.value[i]+"\")";
                }
                var update = updatee.Combine(updateValue);
                // System.Diagnostics.Debug.WriteLine(nameupdate);
                _EmployeeCollection.UpdateOne(empployeeId, update); 
            return _EmployeeCollection.Find(x => x.Id == id).ToList();

            }
            else if(emp.queryType == "Child")
            {
                
                var field = "x."+emp.queryField;
                var id = ObjectId.Parse(emp.ParentId);
                var childId = ObjectId.Parse(emp.ChildId);
                
                var filter = Builders<EmployeeDetails>.Filter;
                var empployeeIdandAddId = filter.And(filter.Eq(x => x.Id, id), filter.ElemMatch(x => x.Address , c => c.Id == childId));

                var employee = _EmployeeCollection.Find(empployeeIdandAddId).SingleOrDefault();
                var updatee = Builders<EmployeeDetails>.Update;
                var nameupdate = updatee.Set("Address.$.City", emp.value[0]);
                _EmployeeCollection.UpdateOne(empployeeIdandAddId, nameupdate);
                return _EmployeeCollection.Find(x => x.Id == id).ToList();
            }

            /*var Id = ObjectId.Parse(id);
            int i;
            var filter = Builders<EmployeeDetails>.Filter;
            var empployeeId = filter.And(
                filter.Eq(x => x.Id, Id));
            var employee = _EmployeeCollection.Find(empployeeId).SingleOrDefault();
            var updatee = Builders<EmployeeDetails>.Update;
            var updateValue = new List<UpdateDefinition<EmployeeDetails>>();
            int count = emp.Field.Count();
            
            for (i = 0; i < count; i++)
            {
                updateValue.Add(updatee.Set(emp.Field[i], emp.value[i]));
                //nameupdate = nameupdate+".Set(\""+emp.Field[i]+"\":\""+emp.value[i]+"\")";
            }
            var update = updatee.Combine(updateValue);
           // System.Diagnostics.Debug.WriteLine(nameupdate);
            _EmployeeCollection.UpdateOne(empployeeId, update);*/
            return _EmployeeCollection.Find(x => x.Id == ObjectId.Parse(emp.ParentId)).ToList();

        }

        // DELETE api/<CRUDMongoDB>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
