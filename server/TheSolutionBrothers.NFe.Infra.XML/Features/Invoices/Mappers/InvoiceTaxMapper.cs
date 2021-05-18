using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class InvoiceTaxMapper : IMapper<InvoiceTax, TotalModel>
    {
        public TotalModel Map(InvoiceTax entity)
        {
            return new TotalModel()
            {
                ICMSTot = new ICMSTotModel()
                {
                    VFrete = string.Format("{0:0.00}", entity.Freight),
                    VICMS = string.Format("{0:0.00}", entity.IcmsValue),
                    VIPI = string.Format("{0:0.00}", entity.IpiValue),
                    VNF = string.Format("{0:0.00}", entity.TotalValueProducts),
                    VTotTrib = string.Format("{0:0.00}", entity.TotalValueInvoice)
                }
            };
        }
    }
}
