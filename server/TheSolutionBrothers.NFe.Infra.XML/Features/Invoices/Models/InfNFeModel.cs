using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models
{

    public class InfNFeModel
    {

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get => "3.10"; }

        [XmlElement(ElementName = "ide")]
        public IdeModel Ide { get; set; }

        [XmlElement(ElementName = "emit")]
        public EmitModel Emit { get; set; }

        [XmlElement(ElementName = "dest")]
        public DestModel Dest { get; set; }

        [XmlElement("det")]
        public DetModel[] Dets { get; set; }

        [XmlElement(ElementName = "total")]
        public TotalModel Total { get; set; }

    }

}
