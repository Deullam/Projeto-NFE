using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{

    public class TotalModel
    {

        [XmlElement(ElementName = "ICMSTot")]
        public ICMSTotModel ICMSTot { get; set; }

    }

}