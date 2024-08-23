using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using iTextSharp;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Text;

namespace WdS.ElioPlus.Lib.Utils
{
    public class InvoicesPdfGenerator
    {
        private int UserId { get; set; }
        private int OrderId { get; set; }

        public InvoicesPdfGenerator(int userId, int orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public static byte[] GetUserInvoiceByOrder(int userId, int paymentId, DBSession session)
        {
            string elioName = "Elioplus, Inc.";
            string p3 = "Invoice";
            string elioPhone = "+1 510-558-1230";

            string p1 = "Payable to";
            string p2 = "Bill to";
            
            string companyName = "";
            string companyAddress = "";
            string companyPostCode = "";
            string companyVatNumber = "";

            string invoiceNumber = "C48310D-0031";
            string planPacket = "";

            decimal price = 0M;
            DateTime issueDate = DateTime.Now;

            ElioUsers user = Sql.GetUserById(userId, session);
            if (user != null)
            {
                companyName = user.CompanyName;
            }

            ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(userId, session);

            if (account != null)
            {
                companyAddress = (account.CompanyBillingAddress == "") ? user.Address : account.CompanyBillingAddress;
                companyPostCode = account.CompanyPostCode;
                companyVatNumber = account.CompanyVatNumber;
            }
            else
            {
                companyAddress = user.Address;
            }

            ElioBillingUserOrdersPayments payment = Sql.GetUserOrdersPaymentById(user.Id, paymentId, session);
            if (payment != null)
            {
                ElioPackets packet = Sql.GetPacketById(payment.PackId, session);
                if (packet != null)
                {
                    planPacket = packet.PackDescription;

                    if (packet.Id == Convert.ToInt32(Packets.PremiumService))
                        planPacket = "Service";
                }

                //vat = order.CostVat;
                //priceProVat = order.CostWithNoVat;
                price = payment.Amount;
                invoiceNumber = payment.ChargeId;
                issueDate = Convert.ToDateTime(payment.CurrentPeriodStart);
            }

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 50, 50);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Paragraph paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 22f, BaseColor.GRAY);
                paragraph.Add(p3);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.GRAY);
                paragraph.Add(elioName);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(@"Invoice number: " + invoiceNumber);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 20;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(p1);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(elioName);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(elioPhone);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.DARK_GRAY);
                paragraph.Add("US Office");
                document.Add(paragraph);
                paragraph.SpacingBefore = 5;

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add("108 West 13th Street, Suite 105 Wilmington, Delaware 19801");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.DARK_GRAY);
                paragraph.Add("Europe Office");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add("33 Saronikou St , 163 45, Ilioupoli, Athens, Greece");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.SpacingBefore = 30;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20f, BaseColor.BLACK);
                paragraph.Add("$ " + price.ToString() + " paid " + GlobalMethods.GetMonth(issueDate.Month) + " " + issueDate.Day + ", " + issueDate.Year);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.SpacingBefore = 20;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(p2);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(companyName);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(companyPostCode);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.DARK_GRAY);
                paragraph.Add("Address");
                document.Add(paragraph);
                paragraph.SpacingBefore = 5;

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(companyAddress);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(companyVatNumber);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 20;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add("Description                                                                                        price                                           Amount");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.GRAY);
                paragraph.Add("----------------------------------------------------------------------------------------------------------------------------------------------------");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(planPacket + "                                                                                                " + price + " $                                      " + price + " $");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.GRAY);
                paragraph.Add("----------------------------------------------------------------------------------------------------------------------------------------------------");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.SpacingBefore = 10;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, BaseColor.GRAY);
                paragraph.Add("Subtotal       " + price.ToString() + " $");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, BaseColor.GRAY);
                paragraph.Add("Amount paid      " + price.ToString() + " $");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_BOTTOM;
                paragraph.SpacingBefore = 100;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.GRAY);
                paragraph.Add("----------------------------------------------------------------------------------------------------------------------------------------------------");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add("Pay by card");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.SpacingBefore = 5;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.GRAY);
                paragraph.Add(@"Visit pay.stripe.com/invoice.");
                paragraph.Add(@"Support for Visa, Mastercard, American Express, Discover, and Diners Club");
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.SpacingBefore = 20;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(@"Questions? Call Elioplus, Inc. at " + elioPhone);
                document.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.SpacingBefore = 10;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.DARK_GRAY);
                paragraph.Add(@"Invoice number: " + invoiceNumber);
                document.Add(paragraph);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                return bytes;
            }
        }

        public static Byte[] GeneratePDF()
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Chunk chunk = new Chunk("This is from chunk. ");
                document.Add(chunk);

                Phrase phrase = new Phrase("This is from Phrase.");
                document.Add(phrase);

                Paragraph para = new Paragraph("This is from paragraph.");
                document.Add(para);

                string text = @"you are successfully created PDF file.";
                Paragraph paragraph = new Paragraph();
                paragraph.SpacingBefore = 10;
                paragraph.SpacingAfter = 10;
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                paragraph.Add(text);
                document.Add(paragraph);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                return bytes;
                //Response.Clear();  
                //Response.ContentType = "application/pdf";  

                //string pdfName = "User";  
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");  
                //Response.ContentType = "application/pdf";  
                //Response.Buffer = true;  
                //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);  
                //Response.BinaryWrite(bytes);  
                //Response.End();  
                //Response.Close();  
            }
        }

        [Obsolete]
        public static void GenerateInvoicePDF()
        {
            //Dummy data for Invoice (Bill).
            string companyName = "ASPSnippets";
            int orderNo = 2303;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("ProductId", typeof(string)),
                            new DataColumn("Product", typeof(string)),
                            new DataColumn("Price", typeof(int)),
                            new DataColumn("Quantity", typeof(int)),
                            new DataColumn("Total", typeof(int))});
            dt.Rows.Add(101, "Sun Glasses", 200, 5, 1000);
            dt.Rows.Add(102, "Jeans", 400, 2, 800);
            dt.Rows.Add(103, "Trousers", 300, 3, 900);
            dt.Rows.Add(104, "Shirts", 550, 2, 1100);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Order No: </b>");
                    sb.Append(orderNo);
                    sb.Append("</td><td align = 'right'><b>Date: </b>");
                    sb.Append(DateTime.Now);
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Company Name: </b>");
                    sb.Append(companyName);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
                        sb.Append(column.ColumnName);
                        sb.Append("</th>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td align = 'right' colspan = '");
                    sb.Append(dt.Columns.Count - 1);
                    sb.Append("'>Total</td>");
                    sb.Append("<td>");
                    sb.Append(dt.Compute("sum(Total)", ""));
                    sb.Append("</td>");
                    sb.Append("</tr></table>");

                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfDoc);
                    //Response.End();

                }
            }
        }

        public void ExportToPdf(DataTable dt, string strFilePath)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            //PdfPRow row = null;
            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
                widths[i] = 4f;

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            //int iCol = 0;
            //string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        table.AddCell(new Phrase(r[h].ToString(), font5));
                    }
                }
            }
            document.Add(table);
            document.Close();
        }
    }
}