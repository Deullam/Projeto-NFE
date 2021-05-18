using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class InvoiceItemMapper : IMapper<InvoiceItem, DetModel>
    {
        public DetModel Map(InvoiceItem entity)
        {
            return new DetModel()
            {
                Imposto = new ImpostoModel()
                {
                    Icms = new ICMSModel()
                    {
                        Icms00 = new ICMS00Model()
                        {
                            PIcms = string.Format("{0:0.00}", (entity.IcmsAliquot * 100)),
                            VIcms = string.Format("{0:0.00}", entity.IcmsValue)
                        }
                    }
                },
                Prod = new ProdModel()
                {
                    CProd = entity.Code,
                    XProd = entity.Description,
                    QCom = string.Format("{0:0.00000}", entity.Amount),
                    VUmCom = string.Format("{0:0.00}", entity.UnitValue),
                    VProd = string.Format("{0:0.00}", entity.TotalValue),
                }
            };
        }
    }
}
