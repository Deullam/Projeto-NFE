using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class EmitModel
    {

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string XName { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string XFant { get; set; }

        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        [XmlElement(ElementName = "IM")]
        public string IM { get; set; }

        [XmlElement(ElementName = "enderEmit")]
        public EnderModel Ender { get; set; }

    }
}
