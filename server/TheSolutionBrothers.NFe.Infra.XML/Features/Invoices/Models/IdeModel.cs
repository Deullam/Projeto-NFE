using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{
    public class IdeModel
    {

        [XmlElement(ElementName = "natOp")]
        public String NatOp { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public String DhEmi { get; set; }

    }
}
