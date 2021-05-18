using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class ICMSTotModel
    {

        [XmlElement(ElementName = "vICMS")]
        public string VICMS { get; set; }

        [XmlElement(ElementName = "vFrete")]
        public string VFrete { get; set; }

        [XmlElement(ElementName = "vIPI")]
        public string VIPI { get; set; }

        [XmlElement(ElementName = "vNF")]
        public string VNF { get; set; }

        [XmlElement(ElementName = "vTotTrib")]
        public string VTotTrib { get; set; }

    }
}