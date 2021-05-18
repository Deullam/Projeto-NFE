using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class ImpostoModel
    {

        [XmlElement(ElementName = "ICMS")]
        public ICMSModel Icms { get; set; }

    }
}