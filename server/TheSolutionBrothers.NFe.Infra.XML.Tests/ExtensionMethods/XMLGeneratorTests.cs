using TheSolutionBrothers.Nfe.Infra.ExtensionMethods;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.XML.ExtensionMethods;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Tests.ExtensionMethods
{

    [TestFixture]
    public class XMLGeneratorTests
    {

        [Test]
        public void Test_XMLGenerator_GenerateXML_ShouldBeOk()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - teste método de extensão (Valores aleatórios).xml";

            NFeModel nfe = new NFeModel()
            {
                InfNFe = new InfNFeModel()
                {
                    Id = "NFe42180308723218000186656620000000011873157004",
                    Ide = new IdeModel()
                    {
                        DhEmi = "2018-03-09T09:29:16-03:00",
                        NatOp = "VENDA DE MERCADORIA ADQUIRIDA OU RECEB.DE TERCEIRO"
                    },
                    Emit = new EmitModel()
                    {
                        CNPJ = "08723218000186",
                        IE = "562377111111",
                        IM = "123123",
                        XFant = "Teste nome fantasia",
                        XName = "Teste razão social",
                        Ender = new EnderModel()
                        {
                            XLgr = "RUA TESTE NFCE",
                            Nro = 23,
                            XBairro = "BIRIBIRI",
                            XMun = "MIMIMIMI",
                            UF = "AC",
                            XPais = "UM QUALQUER"
                        }
                    },
                    Dest = new DestModel()
                    {
                        CNPJ = "08723218000186",
                        IE = "562377111111",
                        XName = "Teste razão social",
                        Ender = new EnderModel()
                        {
                            XLgr = "RUA TESTE NFCE",
                            Nro = 23,
                            XBairro = "BIRIBIRI",
                            XMun = "MIMIMIMI",
                            UF = "AC",
                            XPais = "UM QUALQUER"
                        }
                    },
                    Dets = new DetModel[] 
                    {
                        new DetModel(){
                            Imposto = new ImpostoModel()
                            {
                                Icms = new ICMSModel()
                                {
                                    Icms00 = new ICMS00Model()
                                    {
                                        PIcms = "1.00",
                                        VIcms = "0.12"
                                    }
                                }
                            },
                            Prod = new ProdModel()
                            {
                                CProd = "0001",
                                XProd = "TESTE DESC PROD",
                                QCom = "1.0000",
                                VUmCom = "12.30",
                                VProd = "12.30"
                            }
                        }
                    },
                    Total = new TotalModel()
                    {
                        ICMSTot = new ICMSTotModel()
                        {
                            VFrete = "100.00",
                            VICMS = "0.12",
                            VIPI = "0.23",
                            VNF = "12.30",
                            VTotTrib = "112.55"
                        }
                    }
                }
            };

            nfe.GenerateXML(path);

            FileAssert.Exists(path);
        }

    }
}
