using System;
using System.IO;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;
using iTextSharp.text.pdf.parser;
using System.util.collections;
using iTextSharp.text;
using System.Net.Mail;
using System.Collections.Generic;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices
{
    public class InvoicePdfGenerator
    {
        private string _path;
        private string pathTemplate;

        public void Export(Invoice invoice, string path)
        {
            _path = path;
            string template = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            string templatePath = System.IO.Path.Combine(template, "template.pdf");
            File.WriteAllBytes(templatePath, Properties.Resources.NFE_RETRATO_PG1_V1);
            pathTemplate = templatePath;

            FillPDFForm(invoice);
        }

        private void FillPDFForm(Invoice invoice)
        {
            string formFile = pathTemplate;
            string newFile = _path;
            using (PdfReader reader = new PdfReader(formFile))
            {
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.OpenOrCreate)))
                {
                    AcroFields fields = stamper.AcroFields;

                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        foreach (KeyValuePair<string, AcroFields.Item> kvp in fields.Fields)
                        {
                                    int fileType = fields.GetFieldType(kvp.Key);
                                    string fieldValue = fields.GetField(kvp.Key);
                                    string translatedFileName = fields.GetTranslatedFieldName(kvp.Key);
                                    fields.SetField(translatedFileName, " ");
                                    break;
                        }
                    }

                    // set form fields of Invoice
                    fields.SetField("BRANCO", "");
                    fields.SetField("IDE_NNF", invoice.Number.ToString());
                    fields.SetField("IDE_NATOP", invoice.NatureOperation);
                    fields.SetField("IDE_REFNFEMASKIDE", invoice.KeyAccess.Value);
                    fields.SetField("IDE_DEMI", invoice.IssueDate.Value.ToLongTimeString());
                    fields.SetField("IDE_DSAIENT", invoice.EntryDate.ToShortDateString());
                    fields.SetField("DADOSADIC_HORA", invoice.EntryDate.ToLongTimeString());

                    // set form fields of Sender
                    fields.SetField("NOME", invoice.InvoiceSender.Sender.CompanyName);
                    fields.SetField("LOGRADOURO", invoice.InvoiceSender.Sender.Address.StreetName + ", " + invoice.InvoiceSender.Sender.Address.Number);
                    fields.SetField("BAIRRO", invoice.InvoiceSender.Sender.Address.Neighborhood);
                    fields.SetField("EMIT_IE", invoice.InvoiceSender.Sender.StateRegistration);
                    fields.SetField("EMIT_CNPJ", invoice.InvoiceSender.Sender.Cnpj.FormattedValue);
                    fields.SetField("EMIT_IM", invoice.InvoiceSender.Sender.MunicipalRegistration);


                    // set form fields of Receiver
                    fields.SetField("DEST_XNOME", invoice.InvoiceReceiver.Receiver.Name == null ? invoice.InvoiceReceiver.Receiver.CompanyName : invoice.InvoiceReceiver.Receiver.Name);
                    fields.SetField("DEST_CPF", invoice.InvoiceReceiver.Receiver.Cpf == null ? invoice.InvoiceReceiver.Receiver.Cnpj.FormattedValue : invoice.InvoiceReceiver.Receiver.Cpf.FormattedValue);
                    fields.SetField("DEST_ENDERDEST_XLGR", invoice.InvoiceReceiver.Receiver.Address.StreetName + ", " + invoice.InvoiceReceiver.Receiver.Address.Number);
                    fields.SetField("DEST_ENDERDEST_XBAIRRO", invoice.InvoiceReceiver.Receiver.Address.Neighborhood);
                    fields.SetField("DEST_ENDERDEST_XMUN", invoice.InvoiceReceiver.Receiver.Address.City);
                    fields.SetField("DEST_ENDERDEST_UF", invoice.InvoiceReceiver.Receiver.Address.State);
                    fields.SetField("DEST_ENDERDEST_NRO", invoice.InvoiceReceiver.Receiver.Address.Number.ToString());
                    fields.SetField("DEST_IE", invoice.InvoiceReceiver.Receiver.StateRegistration);

                    //Set Form fields of Invoice
                    string TOTAL_ICMSTOT_VBC = string.Format("{0:0.00}", invoice.InvoiceTax.TotalValueProducts);
                    string TOTAL_ICMSTOT_VFRETE = string.Format("{0:0.00}", invoice.InvoiceTax.Freight);
                    string TOTAL_ICMSTOT_VICMS = string.Format("{0:0.00}", invoice.InvoiceTax.IcmsValue);
                    string TOTAL_ICMSTOT_VPROD = string.Format("{0:0.00}", invoice.InvoiceTax.TotalValueProducts);
                    string TOTAL_ICMSTOT_VIPI = string.Format("{0:0.00}", invoice.InvoiceTax.IpiValue);
                    string TOTAL_ICMSTOT_VNF = string.Format("{0:0.00}", invoice.InvoiceTax.TotalValueInvoice);
                    fields.SetField("TOTAL_ICMSTOT_VBC", TOTAL_ICMSTOT_VBC);
                    fields.SetField("TOTAL_ICMSTOT_VFRETE", TOTAL_ICMSTOT_VFRETE);
                    fields.SetField("TOTAL_ICMSTOT_VICMS", TOTAL_ICMSTOT_VICMS);
                    fields.SetField("TOTAL_ICMSTOT_VPROD", TOTAL_ICMSTOT_VPROD);
                    fields.SetField("TOTAL_ICMSTOT_VIPI", TOTAL_ICMSTOT_VIPI);
                    fields.SetField("TOTAL_ICMSTOT_VNF", TOTAL_ICMSTOT_VNF);
                    //set form fields of Carrier

                    fields.SetField("TRANSP_TRANSPORTA_XNOME", invoice.InvoiceCarrier.Carrier.Name == null ? invoice.InvoiceCarrier.Carrier.CompanyName : invoice.InvoiceCarrier.Carrier.Name);
                    fields.SetField("TRANSP_TRANSPORTA_XENDER", invoice.InvoiceCarrier.Carrier.Address.StreetName + ", " + invoice.InvoiceCarrier.Carrier.Address.Number);
                    if (invoice.InvoiceCarrier.Carrier.FreightResponsability == Domain.Features.Carriers.FreightResponsability.SENDER)
                    {
                        fields.SetField("TRANSP_MODFRETE", "Emissor");
                    }
                    else
                    {
                        fields.SetField("TRANSP_MODFRETE", "Destinatário");
                    }
                    fields.SetField("TRANSP_TRANSPORTA_XMUN", invoice.InvoiceCarrier.Carrier.Address.City);
                    fields.SetField("TRANSP_TRANSPORTA_IE", invoice.InvoiceCarrier.Carrier.StateRegistration);
                    fields.SetField("TRANSP_TRANSPORTA_UF", invoice.InvoiceCarrier.Carrier.Address.State);
                    fields.SetField("TRANSP_TRANSPORTA_CPF", invoice.InvoiceCarrier.Carrier.CPF == null ? invoice.InvoiceCarrier.Carrier.CNPJ.FormattedValue : invoice.InvoiceCarrier.Carrier.CPF.FormattedValue);
                    fields.SetField("TRANSP_TRANSPORTA_XIE", invoice.InvoiceCarrier.Carrier.StateRegistration);


                    //set form fields of Products
                    for (int i = 0; i < invoice.InvoiceItems.Count; i++)
                    {
                        fields.SetField("DET_PROD_CPROD." + i, invoice.InvoiceItems[i].Product.Id.ToString());
                        fields.SetField("DET_PROD_XPROD." + i, invoice.InvoiceItems[i].Product.Description.ToString());
                        fields.SetField("DET_PROD_UCOM." + i, "UN");
                        fields.SetField("DET_PROD_QCOM." + i, invoice.InvoiceItems[i].Amount.ToString());
                        fields.SetField("DET_PROD_VPROD." + i, invoice.InvoiceItems[i].TotalValue.ToString());
                        fields.SetField("DET_IMPOSTO_ICMS_ICMS_PICMS." + i, invoice.InvoiceItems[i].IcmsAliquot.ToString());
                        fields.SetField("DET_IMPOSTO_IPI_IPITRIB_PIPI." + i, invoice.InvoiceItems[i].IpiAliquot.ToString());
                        fields.SetField("DET_IMPOSTO_IPI_IPITRIB_VIPI." + i, invoice.InvoiceItems[i].IpiValue.ToString());
                        fields.SetField("DET_PROD_VUNCOM." + i, invoice.InvoiceItems[i].UnitValue.ToString());
                        fields.SetField("DET_IMPOSTO_ICMS_ICMS_VBC." + i, invoice.InvoiceItems[i].UnitValue.ToString());
                        fields.SetField("DET_IMPOSTO_ICMS_ICMS_VICMS." + i, invoice.InvoiceItems[i].IcmsValue.ToString());
                    }


                    // flatten form fields and close document
                    stamper.FormFlattening = true;
                    stamper.Close();
                }
            }
        }
    }
}
