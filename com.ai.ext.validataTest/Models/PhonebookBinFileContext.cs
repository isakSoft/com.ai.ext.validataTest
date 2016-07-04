using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace com.ai.ext.validataTest.Models
{
    public class PhonebookBinFileContext
    {
        private string Filename { get; set; }
        private List<Contact> phonebook = null;

        public PhonebookBinFileContext() {
            Filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "data.bin");   
            //Use only for the first time to populate the Binary File         
            //InitializePhonebook();
        }

        public void InitializePhonebook()
        {
            phonebook = new List<Contact> {
                new Contact { FirstName="Ardit", LastName="isaku", Number=01, Type="Home" },
                new Contact { FirstName="Zana", LastName="isaku", Number=03, Type="Tel" },
                new Contact { FirstName="Gent", LastName="isaku", Number=02, Type="Tel" }
            };

            try
            {
                using (Stream stream = File.Open(Filename, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, phonebook);
                }
            }
            catch (Exception ex)
            {
                //LOG ex
            }
        }
      
        public List<T> Items<T>() where T: class
        {
            List<T> items = new List<T>();
            try
            {
                using (Stream stream = File.Open(Filename, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    if (stream.Length > 0)
                    {                        
                        items = (List<T>)bin.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                //LOG ex
                //phonebook = null;
            }
            return items;
        } 

        //public List<Contact> Phonebook 
        //{
        //    get{
        //        try
        //        {
        //            using (Stream stream = File.Open(Filename, FileMode.OpenOrCreate))
        //            {
        //                BinaryFormatter bin = new BinaryFormatter();
        //                if (stream.Length > 0)
        //                {
        //                    phonebook = (List<Contact>)bin.Deserialize(stream);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //LOG ex
        //            //phonebook = null;
        //        }
        //        return phonebook;
        //    }
        //    set{
        //        phonebook = value;
        //    }
        //}

        public bool SaveChanges<T>(List<T> _items)
        {
            try
            {
                using (Stream stream = File.Open(Filename, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, _items);
                }
                return true;
            }
            catch (Exception ex)
            {
                //LOG ex
                return false;
            }
        }

    }
}