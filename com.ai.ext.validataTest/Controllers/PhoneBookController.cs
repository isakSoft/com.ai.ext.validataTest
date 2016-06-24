using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using com.ai.ext.validataTest.Models;

namespace com.ai.ext.validataTest.Controllers
{
    [RoutePrefix("api/phonebook")]
    public class PhoneBookController : ApiController
    {
        public PhoneBookController()
        {
            Repository = new PhonebookRepository();
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<Contact> Phonebook()
        {
            return Repository.PhoneBook;
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetContact(int id)
        {
            
Contact item = Repository.PhoneBook.Where(_item => _item.contactID == id.ToString()).SingleOrDefault();
            return item == null ? (IHttpActionResult)BadRequest("No record found") : Ok(item);
        }

        [Route("")]
        [HttpPost]        
        public IHttpActionResult PostContact(string firstname, string lastname, string type, int number)
        {
            if(firstname != null)
            {
                Repository.SaveContact(
                    new Contact {
                        FirstName = firstname,
                        LastName = lastname,
                        Type = type,
                        Number = number
                    });
                return Ok();
            }
            else
            {
                return BadRequest("Bad request");
            }
        }

        public void DeleteContact(string id)
        {
            Repository.DeleteContact(id);
        }

        private IRepository Repository { get; set; }
    }
}
