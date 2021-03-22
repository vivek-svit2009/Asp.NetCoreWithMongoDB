using ASPWithMongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MongoDBTrial : ControllerBase
	{
		private IMongoCollection<EmployeeDetails> _EmployeeCollection;

		public MongoDBTrial(IMongoClient client)
		{
			var database = client.GetDatabase("DevHubs");
			_EmployeeCollection = database.GetCollection<EmployeeDetails>("EmployeeDetails");
		}

		[HttpGet]
		public IEnumerable<EmployeeDetails> wheatherForecast()
		{

            /*Update Parant Document*/
            /* var id = ObjectId.Parse("6011212baf6f4a9d4c06a63f");
             var Addid = ObjectId.Parse("60572f7fbdb353187bff35f6");
             var filter = Builders<EmployeeDetails>.Filter;
             var empployeeId = filter.And(
                 filter.Eq(x => x.Id, id));
             var employee = _EmployeeCollection.Find(empployeeId).SingleOrDefault();
             var updatee = Builders<EmployeeDetails>.Update;
             var nameupdate = updatee.Set("Name", "ajnata");
             _EmployeeCollection.UpdateOne(empployeeId, nameupdate);
             return _EmployeeCollection.Find(_ => true).ToList();*/


            /*Update Specific Nested Document*/
            /*var id = ObjectId.Parse("605764c67d2e861b2cb9d1fd");
            var Addid = ObjectId.Parse("605764c67d2e861b2cb9d1fc");
            var filter = Builders<EmployeeDetails>.Filter;
            var empployeeIdandAddId = filter.And(
                filter.Eq(x => x.Id, id),
                filter.ElemMatch(x => x.Address, c => c.Id == Addid));
            var employee = _EmployeeCollection.Find(empployeeIdandAddId).SingleOrDefault();
            var updatee = Builders<EmployeeDetails>.Update;
            var nameupdate = updatee.Set("Address.$.City", "Varanasi");
            _EmployeeCollection.UpdateOne(empployeeIdandAddId, nameupdate);*/


            /*Update A nested Document*/
            /* Address[] emptyStringArray = new Address[0];
             var id = ObjectId.Parse("60575f334e4f4cdd1bda46f6"); // your document Id
             var builder = Builders<EmployeeDetails>.Filter;
             var filter = builder.Eq(x => x.Id, id);
             var update = Builders<EmployeeDetails>.Update
                 .AddToSet(x => x.Address, new Address
                 {
                     Id = new ObjectId(),
                     City = "sonaha"

                 });

             _EmployeeCollection.UpdateOne(filter, update);*/



            /*Add New Document*/
            var rootCategories = new Address[]
            {
                new Address()
                {
                    Id = ObjectId.GenerateNewId(),
                    City = "Ghaziabad"
                }
            };
            EmployeeDetails Emp = new EmployeeDetails();
            Emp.Name = "Vivek Kumar Sharma";
            Emp.Address = rootCategories;
            _EmployeeCollection.InsertOne(Emp);

            /*Return All Document*/
            return _EmployeeCollection.Find(_ => true).ToList();

		}

	}
}
