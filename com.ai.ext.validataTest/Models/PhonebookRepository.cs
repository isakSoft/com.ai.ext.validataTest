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
            Phonebook = new List<Contact>();
        }

        public IEnumerable<Contact> PhoneBook
        {                        
            get
            {              
                Phonebook = binFile.LocalRead();
                return Phonebook;
            }
        }

        public void SaveContact(Contact contact)
        {
            var _contact = Phonebook.FirstOrDefault(item => item.contactID == contact.contactID);
            //new contact
            if (_contact == null)
            {
                var newContact = new Contact
                {
                    contactID = Guid.NewGuid().ToString(),
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
                    contactID = _contact.contactID,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Type = contact.Type,
                    Number = contact.Number
                };

                Phonebook.Remove(_contact);
                Phonebook.Add(updatedContact);

                var sortedPhonebook = from l_contact in Phonebook
                                      orderby l_contact.FirstName
                                      select l_contact;

                Phonebook = (List<Contact>)sortedPhonebook;
            }

            binFile.LocalWrite(Phonebook);
        }

        public Contact DeleteContact(string id)
        {
            var _contact = Phonebook.FirstOrDefault(item => item.contactID == id);
            if(_contact != null)
            {
                Phonebook.Remove(_contact);
            }

            binFile.LocalWrite(Phonebook);
            return _contact;
        }
    }
}