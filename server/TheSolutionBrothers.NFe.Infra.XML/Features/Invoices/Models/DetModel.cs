using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{

    public class DetModel
    {

        [XmlAttribute(AttributeName = "nItem")]
        public int NItem { get; set; }

        [XmlElement(ElementName = "prod")]
        public ProdModel Prod { get; set; }

        [XmlElement(ElementName = "imposto")]
        public ImpostoModel Imposto { get; set; }

    }
}
