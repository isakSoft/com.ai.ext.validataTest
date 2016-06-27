using System;

namespace com.ai.ext.validataTest.Models
{
    [Serializable()]
    public class Contact
    {
        public string ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }

        public Contact()
        {
            ContactID = Guid.NewGuid().ToString();
        }

        public string FullName()
        {
            return FirstName + LastName;
        }
    }
}