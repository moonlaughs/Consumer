using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        static readonly HttpClient Client = new HttpClient();
        private static string _uri = "https://localhost:44374/api/Person";

        private static ObservableCollection<Person> listOfPeople = new ObservableCollection<Person>()
        {
            new Person(1,"Iza", "Ku", 18, "ooo", "ooo")
        };
        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return listOfPeople;
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            foreach (var item in listOfPeople)
            {
                if (item.Id == id)
                {
                    return item.ToString();
                }
                else
                {
                    break;
                }
            }
            return "No such object";
        }

        // POST: api/Person
        [HttpPost]
        public async Task<Person> PostAsync([FromBody] Person newPerson)
        {
            var jsonString = JsonConvert.SerializeObject(newPerson);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(_uri, content);   //makes a loop here
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("Already exists. Try another id.");
            }
            response.EnsureSuccessStatusCode();
            string str = await response.Content.ReadAsStringAsync();
            var newlyCreatedObject = JsonConvert.DeserializeObject<Person>(str);
            listOfPeople.Add(newlyCreatedObject); //? check if working with internet and postman
            
            return newlyCreatedObject;
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Person value)
        {
            string uriId = _uri + "/" + id;
            foreach (var item in listOfPeople)
            {
                if (item.Id == id)
                {
                    item.FirstName = value.FirstName;
                    item.LastName = value.LastName;
                    item.Age = value.Age;
                    item.Pass = value.Pass;
                    item.UserLogin = value.UserLogin;

                    var jsonString = JsonConvert.SerializeObject(value);
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await Client.PutAsync(uriId, content);
                    /*if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        throw new Exception("sth went wrong");
                    }
                    response.EnsureSuccessStatusCode();*/
                }
                else
                {
                    break;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            foreach (var item in listOfPeople)
            {
                if (item.Id == id)
                {
                    listOfPeople.Remove(item);
                }
                else
                {
                    break;
                }
            }
        }
    }
}           //check if it works with postman
            //connect to LocalDB
            //make a DB on the Azure
            //connect to the new DB on Azure
            //make a console consumer == myOwnPostMan
            //make a TypeScriptApp for this, without connectin, only Visual parts
            //connect this project to the TS
            //check if it is possible to make changes for uploaded wabsite ;)
