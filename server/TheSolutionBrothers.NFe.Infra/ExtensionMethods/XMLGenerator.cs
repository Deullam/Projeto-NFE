using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.Nfe.Infra.ExtensionMethods
{
    public static class XMLGenerator
    {

        public static void GenerateXML<T>(this T entity, string path)
        {
            using (System.IO.FileStream file = System.IO.File.Create(path))
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                writer.Serialize(file, entity);
            }
        }

    }
}
