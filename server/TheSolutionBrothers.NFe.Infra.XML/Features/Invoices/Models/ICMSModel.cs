using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class ICMSModel
    {
        
        [XmlElement(ElementName = "ICMS00")]
        public ICMS00Model Icms00 { get; set; }

    }
}