using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.ai.ext.validataTest.Models
{
    public class BinFile
    {
        string fileName { get; set; }

        public BinFile()
        {
            fileName = "C:\\data.bin";
        }

        public List<Contact> LocalRead()
        {
            List<Contact> Phonebook = new List<Contact>();
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    if (stream != null)
                    {
                        Phonebook = (List<Contact>)bin.Deserialize(stream);
                    }
                }
            }
            catch (IOException IOex)
            {
                return null;
            }
            return Phonebook;
        }
        public bool LocalWrite(List<Contact> contacts)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, contacts);
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}