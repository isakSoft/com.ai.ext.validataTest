using System;

namespace com.ai.ext.validataTest.Models
{
    [Serializable()]
    public class Contact
    {
        public string contactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }

        public Contact()
        {
            contactID = Guid.NewGuid().ToString();
        }

        public string FullName()
        {
            return FirstName + LastName;
        }
    }
}