using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels
{
    public class InvoiceItemViewModel
    {
        public long Id { get; set; }
        public ProductViewModel Product { get; set; }

        public double IcmsAliquot { get; set; }
        public double IpiAliquot { get; set; }
        public long Amount { get; set; }
        public double UnitValue { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double TotalValue { get; set; }
        public double IcmsValue { get; set; }
        public double IpiValue { get; set; }

    }
}
