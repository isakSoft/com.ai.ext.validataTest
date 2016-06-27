using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ai.ext.validataTest.Models
{
    public interface IRepository
    {
        IEnumerable<Contact> PhoneBook { get; }
        void SaveContact(Contact contact);
        bool DeleteContact(string id);
    }
}
