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
                using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    if (stream.Length > 0)
                    {
                        Phonebook = (List<Contact>)bin.Deserialize(stream);
                    }
                }
            }
            catch (IOException IOex)
            {
                // LOG error
                throw IOex;
            }
            catch(Exception ex)
            {
                // LOG error
                throw ex;
            }

            return Phonebook;
        }
        public bool LocalWrite(List<Contact> contacts)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, contacts);
                }

                return true;
            }
            catch (IOException IOex)
            {                
                // LOG error
                throw IOex;
            }
            catch (Exception ex)
            {
                // LOG error
                throw ex;
            }
            return false;
        }
    }
}