using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels
{
	public class SenderViewModel
	{
		public virtual long Id { get; set; }
		public virtual string FancyName { get; set; }
		public virtual string CompanyName { get; set; }
		public virtual string Cnpj { get; set; }
		public virtual string StateRegistration { get; set; }
		public virtual string MunicipalRegistration { get; set; }
		public virtual AddessViewModel Address { get; set; }
	}
}
