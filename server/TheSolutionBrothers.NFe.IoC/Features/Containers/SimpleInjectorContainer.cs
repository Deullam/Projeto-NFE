using SimpleInjector;
using AutoMapper;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using TheSolutionBrothers.NFe.Domain.Features.Users;
using TheSolutionBrothers.NFe.Infra.Data.Features.Users;
using TheSolutionBrothers.NFe.Application.Features.Users;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Infra.Data.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;

namespace TheSolutionBrothers.NFe.IoC.Features.Containers
{
    public static class SimpleInjectorContainer
    {

        public static Container Instance { get; internal set; }

        public static Container RegisterServices()
        {
            var container = new Container();

            container.Register<ContextNfe>(() => new ContextNfe("DbContext"), Lifestyle.Singleton);
            container.Register<IMapper>(() => Mapper.Instance, Lifestyle.Singleton);

            container.Register<IReceiverService, ReceiverService>();
            container.Register<ICarrierService, CarrierService>();
			container.Register<ISenderService, SenderService>();
			container.Register<IUserService, UserService>();
            container.Register<IProductService, ProductService>();
            container.Register<IInvoiceService, InvoiceService>();

            container.Register<IAddressRepository, AddressRepository>();
            container.Register<ICarrierRepository, CarrierRepository>();
            container.Register<IReceiverRepository, ReceiverRepository>();
            container.Register<IUserRepository, UserRepository>();
			container.Register<ISenderRepository, SenderRepository>();
			container.Register<IProductRepository, ProductRepository>();
			container.Register<IInvoiceRepository, InvoiceRepository>();
			container.Register<IInvoiceItemRepository, InvoiceItemRepository>();

            container.Verify();

            Instance = container;

            return container;
        }

    }
}
