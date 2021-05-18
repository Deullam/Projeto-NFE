using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class ICMS00Model
    {
        
        [XmlElement(ElementName = "pICMS")]
        public string PIcms { get; set; }

        [XmlElement(ElementName = "vICMS")]
        public string VIcms { get; set; }

    }
}