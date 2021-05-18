using System.Data.Entity;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Users;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Products;
using TheSolutionBrothers.NFe.Infra.Data.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Users;

namespace TheSolutionBrothers.NFe.Infra.Data.Contexts
{
    public class ContextNfe : DbContext
    {
        public ContextNfe() : base("DbContext")
        {
        }

        public ContextNfe(string connectionStringName) : base(string.Format("name={0}", connectionStringName))
        {
            Database.Initialize(true);
        }

        protected ContextNfe(System.Data.Common.DbConnection connection) : base(connection, true) { }

        public DbSet<User> Users { get; set; }
		public DbSet<Sender> Senders { get; set; }
		public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Product> Products { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceItem> InvoiceItems { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.ProxyCreationEnabled = false;
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new ReceiverMapper());
            modelBuilder.Configurations.Add(new CarrierMapper());
			modelBuilder.Configurations.Add(new SenderMapper());
            modelBuilder.Configurations.Add(new AddressMapper());
            modelBuilder.Configurations.Add(new ProductMapper());
			modelBuilder.Configurations.Add(new InvoiceMapper());
			modelBuilder.Configurations.Add(new InvoiceItemMapper());
		}
    }
}
