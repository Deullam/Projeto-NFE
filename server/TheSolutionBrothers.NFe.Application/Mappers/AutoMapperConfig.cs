using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Addresses;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Senders;

namespace TheSolutionBrothers.NFe.Application.Mappers
{
    public class AutoMapperConfig
    {

        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AddressProfile>();
                x.AddProfile<SenderProfile>();
                x.AddProfile<ReceiverProfile>();
                x.AddProfile<CarrierProfile>();
                x.AddProfile<ProductProfile>();
                x.AddProfile<InvoiceProfile>();
            }
            );

        }
        public static void Reset() => Mapper.Reset();


    }
}