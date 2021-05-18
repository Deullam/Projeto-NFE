using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class EnderModel
    {

        [XmlElement(ElementName = "xLgr")]
        public string XLgr { get; set; }

        [XmlElement(ElementName = "nro")]
        public int Nro { get; set; }

        [XmlElement(ElementName = "xBairro")]
        public string XBairro { get; set; }

        [XmlElement(ElementName = "xMun")]
        public string XMun { get; set; }

        [XmlElement(ElementName = "UF")]
        public string UF{ get; set; }

        [XmlElement(ElementName = "xPais")]
        public string XPais { get; set; }

    }
}
