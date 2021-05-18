using System.Xml.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.XML.ExtensionMethods
{
    public static class XMLGenerator
    {

        public static void GenerateXML<T>(this T entity, string path)
        {
            using (System.IO.FileStream file = System.IO.File.Create(path))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                XmlSerializer writer = new XmlSerializer(typeof(T));
                writer.Serialize(file, entity, ns);
            }
        }

    }
}
