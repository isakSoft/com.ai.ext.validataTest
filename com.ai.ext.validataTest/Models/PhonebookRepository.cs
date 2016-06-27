using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace com.ai.ext.validataTest.Models
{
    public class PhonebookRepository : IRepository
    {
        List<Contact> Phonebook;
        BinFile binFile;
        public PhonebookRepository()
        {
            binFile = new BinFile();
            //Phonebook = binFile.LocalRead();    
            Phonebook = new List<Contact>();
            Phonebook = (from l_contact in binFile.LocalRead()
                         orderby l_contact.FirstName, l_contact.LastName
                         select l_contact).ToList();
        }

        public IEnumerable<Contact> PhoneBook
        {                        
            get
            {              
                //Phonebook = binFile.LocalRead();
                return Phonebook;
            }
        }

        public void SaveContact(Contact contact)
        {
            var _contact = Phonebook.FirstOrDefault(item => item.ContactID == contact.ContactID);
            //new contact
            if (_contact == null)
            {
                var newContact = new Contact
                {
                    ContactID = Guid.NewGuid().ToString(),
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Type = contact.Type,
                    Number = contact.Number
                };
                Phonebook.Add(newContact);
            }
            else //contact exist
            {
                var updatedContact = new Contact
                {                    
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Type = contact.Type,
                    Number = contact.Number
                };

                Phonebook.Remove(_contact);
                Phonebook.Add(updatedContact);
            }

            try
            {
                binFile.LocalWrite(Phonebook);
            }
            catch(Exception ex)
            {
                // LOG error
                throw ex;
            }
        }

        public bool DeleteContact(string Id)
        {
            var _contact = Phonebook.FirstOrDefault(item => item.ContactID == Id);
            if(_contact != null)
            {
                Phonebook.Remove(_contact);
                try
                {
                    binFile.LocalWrite(Phonebook);
                    return true;
                }
                catch (Exception ex)
                {
                    // LOG error
                    throw ex;
                }
            }
            return false;            
        }
    }
}