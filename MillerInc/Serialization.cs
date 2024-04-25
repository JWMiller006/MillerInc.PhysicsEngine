using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;

namespace MillerInc.Convert.Classes
{
    public class Serialization<T> : Loadable
    {
        public static void Serialize(T obj, string filePath)
        {
            FileStream f = new(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter b = new();
            b.Serialize(f, obj);
            f.Close();
        }
        
        public void Serialize(string filePath)
        {
            FileStream f = new(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter b = new();
            b.Serialize(f, this);
            f.Close();
        }

        public static T DeSerialize(string filePath)
        {
            T temp;
            FileStream f = new(filePath, FileMode.OpenOrCreate, FileAccess.Read); 
            BinaryFormatter b = new();
            temp = (T)b.Deserialize(f);
            return temp; 
        }
    }
}
