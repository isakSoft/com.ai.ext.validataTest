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

        [Route(""), HttpGet]
        public IEnumerable<Contact> Phonebook()
        {
            return Repository.PhoneBook;
        }

        [Route("contact"), Route("contact/{Id}"), HttpGet]
        public IHttpActionResult GetContact(string Id)
        {            
            Contact item = Repository.PhoneBook.Where(_item => _item.ContactID == Id).SingleOrDefault();
            return item == null ? (IHttpActionResult)BadRequest("No record found") : Ok(item);
        }

        [Route("search"), Route("search/{keyword}"), HttpGet]
        public IHttpActionResult SearchContact(string keyword)
        {
            var items = Repository.PhoneBook.Where(_item => _item.FirstName.ToLower().Contains(keyword.ToLower())
            || _item.LastName.ToLower().Contains(keyword.ToLower())).ToList();
            return items.Count == 0 ? (IHttpActionResult)BadRequest("No record found") : Ok(items);
        }

        [Route(""), HttpPost]  
        public IHttpActionResult PostContact([FromBody]Contact contact)
        {
            if(contact != null)
            {
                try
                {
                    Repository.SaveContact(
                        new Contact
                        {
                            ContactID = contact.ContactID,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            Type = contact.Type,
                            Number = contact.Number
                        });
                    return Ok("Contact saved successfully");
                }
                catch(Exception ex)
                {
                    //LOG error
                    return BadRequest("Couldnt save the contact. Something went wrong.");
                }
            }
            else
            {
                return BadRequest("Bad request");
            }
        }

        [Route("delete"), Route("delete/{Id}"), HttpPost]
        public IHttpActionResult DeleteContact(string Id)
        {
            try
            {
                if (Repository.DeleteContact(Id))
                {
                    return Ok("Contact was deleted.");
                }
                else
                {
                    return BadRequest("Contact not found for deletion.");
                }
            }
            catch
            {
                //LOG error
                return BadRequest("Error. Please contact your Administrator.");
            }
        }

        private IRepository Repository { get; set; }
    }
}
