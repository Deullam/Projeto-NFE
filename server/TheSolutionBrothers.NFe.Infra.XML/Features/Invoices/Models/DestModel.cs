using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{

    public class DestModel
    {

        [XmlElement(ElementName = "CNPJ", IsNullable = false)]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "CPF", IsNullable = false)]
        public string CPF { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string XName { get; set; }
        
        [XmlElement(ElementName = "indIEDest", IsNullable = false)]
        public string IE { get; set; }

        [XmlElement(ElementName = "enderDest")]
        public EnderModel Ender { get; set; }

    }

}
