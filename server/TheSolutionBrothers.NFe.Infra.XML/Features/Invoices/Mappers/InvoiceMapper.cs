using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System.Collections.Generic;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class InvoiceMapper : IMapper<Invoice, NFeModel>
    {

        public IMapper<Sender, EmitModel> _senderMapper;
        public IMapper<Receiver, DestModel> _receiverMapper;
        public IMapper<InvoiceItem, DetModel> _invoiceItemMapper;
        public IMapper<InvoiceTax, TotalModel> _invoiceTaxMapper;

        public InvoiceMapper(IMapper<Sender, EmitModel> senderMapper,
            IMapper<Receiver, DestModel> receiverMapper, 
            IMapper<InvoiceItem, DetModel> invoiceItemMapper, 
            IMapper<InvoiceTax, TotalModel> invoiceTaxMapper)
        {
            _senderMapper = senderMapper;
            _receiverMapper = receiverMapper;
            _invoiceItemMapper = invoiceItemMapper;
            _invoiceTaxMapper = invoiceTaxMapper;
        }

        public NFeModel Map(Invoice entity)
        {
            int index = 1;
            List<DetModel> dets = new List<DetModel>();
            foreach(InvoiceItem item in entity.InvoiceItems)
            {
                DetModel det = _invoiceItemMapper.Map(item);
                det.NItem = index++;

                dets.Add(det);
            }

            return new NFeModel()
            {
                InfNFe = new InfNFeModel()
                {
                    Id = string.Format("NFe{0}", entity.KeyAccess.Value),
                    Ide = new IdeModel()
                    {
                        DhEmi = entity.IssueDate.Value.ToString("yyyy-MM-dd'T'HH:mm:sszzz"),
                        NatOp = entity.NatureOperation
                    },
                    Emit = _senderMapper.Map(entity.InvoiceSender.Sender),
                    Dest = _receiverMapper.Map(entity.InvoiceReceiver.Receiver),
                    Dets = dets.ToArray(),
                    Total = _invoiceTaxMapper.Map(entity.InvoiceTax)
                }
            };
        }
    }
}
