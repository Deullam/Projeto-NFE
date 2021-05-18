using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{

    [XmlRoot("NFe")]
    public class NFeModel
    {

        [XmlElement(ElementName = "infNFe")]
        public InfNFeModel InfNFe { get; set; }

    }
}
