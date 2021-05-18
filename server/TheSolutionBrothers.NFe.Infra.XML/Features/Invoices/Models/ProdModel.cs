using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class ProdModel
    {

        [XmlElement(ElementName = "cProd")]
        public string CProd { get; set; }

        [XmlElement(ElementName = "xProd")]
        public string XProd { get; set; }

        [XmlElement(ElementName = "qCom")]
        public string QCom { get; set; }

        [XmlElement(ElementName = "vUnCom")]
        public string VUmCom { get; set; }

        [XmlElement(ElementName = "vProd")]
        public string VProd { get; set; }

    }
}