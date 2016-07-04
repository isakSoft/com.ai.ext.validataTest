using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using com.ai.ext.validataTest.Models;

namespace com.ai.ext.validataTest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/phonebook")]
    public class PhoneBookController : ApiController
    {
        private UoW uow = null;
        private List<Contact> phonebook = null;
        /// <summary>
        /// 
        /// </summary>
        public PhoneBookController()
        {
            //Repository = new PhonebookRepository();
            uow = new UoW();
            phonebook = new List<Contact>();
        }

        public PhoneBookController(UoW _uow)
        {
            uow = _uow;
        }

        /// <summary>
        /// Get Phonebook of all contacts
        /// </summary>
        /// <returns>The Phonebook as a list of contacts</returns>
        [Route(""), HttpGet]
        public IEnumerable<Contact> Phonebook()
        {
            return uow.Repository<Contact>().GetAll().ToList();
            //return Repository.PhoneBook;
        }

        /// <summary>
        /// Get a single contact details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[ApiExplorerSettings(IgnoreApi=true)]
        [Route("contact"), Route("contact/{Id}"), HttpGet]
        public IHttpActionResult GetContact(string Id)
        {
            Contact contact = uow.Repository<Contact>().Get(_item => _item.ContactID == Id);        
            return contact == null ? (IHttpActionResult)BadRequest("No record found") : Ok(contact);
        }

        /// <summary>
        /// Search Phonebook using keyword
        /// </summary>
        /// <param name="keyword">E.g. Ardit</param>
        /// <returns>May return a list of contacts</returns>
        [Route("search"), Route("search/{keyword}"), HttpGet]
        public IHttpActionResult SearchContact(string keyword)
        {
            var items = uow.Repository<Contact>().GetAll(_item => _item.FirstName.ToLower().Contains(keyword.ToLower())
            || _item.LastName.ToLower().Contains(keyword.ToLower())).ToList();
            return items.Count == 0 ? (IHttpActionResult)BadRequest("No record found") : Ok(items);            
        }

        /// <summary>
        /// Both functionalities Add & Update are in this single func
        /// </summary>
        /// <param name="contact">This is a model</param>
        /// <returns>Http response codes</returns>
        [Route(""), HttpPost]
        public IHttpActionResult PostContact([FromBody]Contact contact)
        {                        
            try
            {
                if (contact.ContactID == null) //new contact
                {
                    uow.Repository<Contact>().Save(
                        new Contact
                        {
                            ContactID = Guid.NewGuid().ToString(), //if null => new contact
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            Type = contact.Type,
                            Number = contact.Number
                        }, ref phonebook);
                }
                else //update contact
                {
                    Contact oldContact = uow.Repository<Contact>().Get(_item => _item.ContactID == contact.ContactID);
                    uow.Repository<Contact>().Attach(
                        oldContact, //Old value
                        new Contact
                        {
                            ContactID = contact.ContactID,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            Type = contact.Type,
                            Number = contact.Number
                        }, ref phonebook);
                }

                uow.SaveChanges(phonebook);
                return Ok("Contact saved successfully");
            }
            catch (Exception ex)
            {
                //LOG error
                return BadRequest("Couldnt save the contact. Something went wrong.");
            }
        }

        [Route("delete"), Route("delete/{Id}"), HttpPost]
        public IHttpActionResult DeleteContact(string Id)
        {
            try
            {
                Contact _contact = uow.Repository<Contact>().Get(_item => _item.ContactID == Id);
                if (uow.Repository<Contact>().Remove(_contact, ref phonebook))
                {
                    uow.SaveChanges(phonebook);
                    return Ok("Contact was deleted.");
                }
                else
                {
                    return BadRequest("Contact not found for deletion.");
                }
            }
            catch (Exception ex)
            {
                //LOG error
                return BadRequest("Error. Please contact your Administrator.");
            }
        }
    }
}
