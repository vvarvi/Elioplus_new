using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.DB;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.UI;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ClosedXML.Excel;

namespace WdS.ElioPlus.Lib.Utils
{
    public static class ExportLib
    {
        public static string ExcelFilenamePath(ElioUsers user, DBSession session)
        {
            string filePath = "";

            List<ElioOpportunitiesUsersIJUsersIJStatus> opportunities = Sql.GeAlltUsersOpportunitiesWithStatusDescription(user.Id, session);

            if (opportunities.Count > 0)
            {
                #region Opportunities

                DataTable tblOpp = new DataTable();

                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");
                tblOpp.Columns.Add("");

                foreach (ElioOpportunitiesUsersIJUsersIJStatus opportunity in opportunities)
                {
                    List<ElioOpportunitiesNotes> notes = Sql.GetUsersOpportunityNotesByOpportunityId(opportunity.Id, session);

                    List<ElioOpportunitiesUsersTasks> tasks = Sql.GetUsersTasks(opportunity.UserId, opportunity.Id, session);
                    
                    if (notes.Count > 0)
                    {
                        #region Notes

                        DataTable tblNotes = new DataTable();

                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");
                        tblNotes.Columns.Add("");

                        foreach (ElioOpportunitiesNotes note in notes)
                        {
                            tblNotes.Rows.Add(note.Id);

                        }

                        #endregion
                    }

                    if (tasks.Count > 0)
                    {
                        #region Tasks

                        DataTable tblTasks = new DataTable();

                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");
                        tblTasks.Columns.Add("");

                        foreach (ElioOpportunitiesUsersTasks task in tasks)
                        {
                            tblTasks.Rows.Add(task.Id);
                        }

                        #endregion
                    }
                }

                #endregion
            }

            return filePath;
        }

        public static void ExportDataTableToPdf(DataTable dt, string companyName)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    Document document = new Document();

                    //FileStream fs = File.Create(strFilePath);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    iTextSharp.text.Font font6 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);
                    iTextSharp.text.Font font7 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 7);

                    Paragraph paragraph = new Paragraph();
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.SpacingAfter = 20;
                    paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 22f, BaseColor.GRAY);
                    paragraph.Add("Partners of " + companyName);
                    document.Add(paragraph);

                    PdfPTable table = new PdfPTable(dt.Columns.Count);
                    //PdfPRow row = null;
                    float[] widths = new float[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                        widths[i] = 4f;

                    table.SetWidths(widths);

                    table.WidthPercentage = 100;
                    //int iCol = 0;
                    //string colname = "";
                    PdfPCell cell = new PdfPCell(new Phrase("Partners"));

                    cell.Colspan = dt.Columns.Count;

                    foreach (DataColumn c in dt.Columns)
                    {
                        table.AddCell(new Phrase(c.ColumnName, font7));
                    }

                    foreach (DataRow r in dt.Rows)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int h = 0; h < dt.Columns.Count; h++)
                            {
                                table.AddCell(new Phrase(r[h].ToString(), font6));
                            }
                        }
                    }
                    document.Add(table);
                    document.Close();

                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    if (bytes != null)
                    {
                        try
                        {
                            string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();

                            HttpContext.Current.Response.ContentType = "application/pdf";
                            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Partners_report_" + fileName + ".pdf");
                            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            HttpContext.Current.Response.BinaryWrite(bytes);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.End();
                        }
                        catch (System.Threading.ThreadAbortException ex)
                        {
                            Logger.Debug(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }
                }
            }
        }

        public static void ExportToExcel(DataSet ds)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=test.csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";

            //DataSet ds = new DataSet();     // DB.GetCertifiedCandidatesDateRangetData(session);

            DataTable table = new DataTable();

            //table = session.GetDataTable("Select * from Elio_users");

            ds.Tables.Add(table);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string excelFilePath = "";

                ExcelReportEngine excelEngine = new ExcelReportEngine(excelFilePath);

                StringBuilder fileContent = new StringBuilder();

                bool fileCreated = excelEngine.WriteToCsvFile(ds, ref fileContent);

                if (fileCreated)
                {
                    HttpContext.Current.Response.Output.Write(fileContent.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                else
                {
                    //string results = "Excel could not be created. Email could not be sent";
                }
            }
        }

        /*--- Mitsos --*/

        public static HttpRequest Request { get; set; }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.
                 ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }

            return dt;
        }

        public static void ExportToWordWithGrid(object sender, EventArgs e)
        {
            //Get the data from database into datatable
            string strQuery = "select CustomerID, ContactName, City, PostalCode" +
                              " from customers";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetDataTable(cmd);

            //Create a dummy GridView
            GridView gridView1 = new GridView();
            gridView1.AllowPaging = false;
            gridView1.DataSource = dt;
            gridView1.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=DataTable.doc");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridView1.RenderControl(hw);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void ExportToExcelWithGrid(object sender, EventArgs e)
        {
            //Get the data from database into datatable
            string strQuery = "select CustomerID, ContactName, City, PostalCode" +
                " from customers";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetDataTable(cmd);

            //Create a dummy GridView
            GridView gridView1 = new GridView();
            gridView1.AllowPaging = false;
            gridView1.DataSource = dt;
            gridView1.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
             "attachment;filename=DataTable.xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < gridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                gridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            gridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void ExportToCsvWithSb(DataSet ds, string opportName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + opportName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();

            foreach (DataTable table in ds.Tables)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    //add separator                   
                    sb.Append(table.Columns[k].ColumnName + ',');
                }

                //append new line
                sb.Append("\r\n");
                sb.Append("\r\n");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(table.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }

                sb.Append("\r\n");
                sb.Append("\r\n");
                sb.Append("\r\n");
            }

            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void ExportToPdfWithSb(DataSet ds, string fileName)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                //HttpContext.Current.Response.BinaryWrite(b);
                ////Response.Flush();
                HttpContext.Current.Response.End();
                HttpContext.Current.Response.Close();
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                Logger.Debug(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/pdf";

            StringBuilder sb = new StringBuilder();
                       

            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void WriteDelimitedData(DataTable dt, string fileName, string delimiter)
        {
            //prepare the output stream
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                string.Format("attachment; filename={0}", fileName));

            //write the csv column headers
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(dt.Columns[i].ColumnName);
                HttpContext.Current.Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
            }

            //write the data
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
                }
            }

            HttpContext.Current.Response.End();
        }
        
        public static void Button1_Click(object sender, EventArgs e)
        {
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            SqlConnection conn = new SqlConnection(strConn);

            SqlDataAdapter da = new SqlDataAdapter("select * from employees", conn);

            DataSet ds = new DataSet();

            da.Fill(ds, "Emp");

            GridView gridView1 = new GridView();

            gridView1.DataSource = ds.Tables["Emp"].DefaultView;

            gridView1.DataBind();

            DataTable dt = ds.Tables["Emp"];

            //CreateCsvFile(dt, "c:\\csvData.csv");
        }

        public static void CreateCsvFile(DataTable dt, string strFilePath)
        {
            #region Export Grid to CSV

            // Create the CSV file to which grid data will be exported.

            StreamWriter sw = new StreamWriter(strFilePath, false);

            // First we will write the headers.

            //DataTable dt = m_dsProducts.Tables[0];

            int iColCount = dt.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {

                sw.Write(dt.Columns[i]);

                if (i < iColCount - 1)
                {

                    sw.Write(",");

                }

            }

            sw.Write(sw.NewLine);

            // Now write all the rows.

            foreach (DataRow dr in dt.Rows)
            {

                for (int i = 0; i < iColCount; i++)
                {

                    if (!Convert.IsDBNull(dr[i]))
                    {

                        sw.Write(dr[i].ToString());

                    }

                    if (i < iColCount - 1)
                    {

                        sw.Write(",");

                    }

                }

                sw.Write(sw.NewLine);

            }

            sw.Close();



            #endregion

        }

        public static void ExportDataTableToCsv()
        {
            DataTable dt = new DataTable();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Customers.csv");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + ',');
            }
            sb.Append(Environment.NewLine);

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Rows[j][k].ToString() + ',');
                }
                sb.Append(Environment.NewLine);
            }
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        public static void DataTableToCSVFile(string filename)
        {
            HttpContext context = HttpContext.Current;

            context.Response.Clear();

            //foreach (DataColumn column in dtExcelUpdown.Columns)
            //{
            //    context.Response.Write(column.ColumnName + ",");
            //}

            context.Response.Write(Environment.NewLine);

            //foreach (DataRow row in dtExcelUpdown.Rows)
            //{
            //    for (int i = 0; i < dtExcelUpdown.Columns.Count; i++)
            //    {
            //        context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
            //    }

            //    context.Response.Write(Environment.NewLine);
            //}

            context.Response.ContentType = "text/csv";

            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");

            context.Response.End();

        }

        public static DataSet GetCVSFile(string pathName, string fileName)
        {
            OleDbConnection ExcelConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties=Text;");

            OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM " + fileName, ExcelConnection);


            OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);


            ExcelConnection.Open();


            DataSet ExcelDataSet = new DataSet();

            ExcelAdapter.Fill(ExcelDataSet);


            ExcelConnection.Close();

            return ExcelDataSet;
        }

        public static DataTable CleanUpDataTable(DataView dv)
        {
            DataTable dt = new DataTable(dv.Table.TableName);
            DataRow dtRow = null;

            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                dt.Columns.Add(dv.Table.Columns[i].ColumnName, dv.Table.Columns[i].DataType);
                bool isInt = dv.Table.Columns[i].DataType == typeof(double);

                foreach (DataRow row in dv.Table.Rows)
                {
                    if (isInt)
                    {
                        if (row[i] is DBNull)
                            row[i] = "0";
                    }
                }
            }

            foreach (DataRowView row in dv)
            {
                dtRow = dt.NewRow();

                for (int i = 0; i < dv.Table.Columns.Count; i++)
                {
                    dtRow[i] = row[i].ToString();
                }

                dt.Rows.Add(dtRow);
            }

            return dt;
        }

        public static void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dtTable = new DataTable();
            DataRow dtRow;

            dtTable.Columns.Add("SNo", typeof(int));
            dtTable.Columns.Add("Address", typeof(string));

            for (int i = 0; i <= 9; i++)
            {
                dtRow = dtTable.NewRow();
                dtRow[0] = i;
                dtRow[1] = "Address " + i.ToString();
                dtTable.Rows.Add(dtRow);
            }

            HttpContext.Current.Response.ContentType = "Application/x-msexcel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            HttpContext.Current.Response.Write(ExportToCSVFile(dtTable));
            HttpContext.Current.Response.End();
        }

        public static string ExportToCSVFileNew(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            if (dt.Columns.Count != 0)
            {
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
            }

            return sb.ToString();
        }

        public static string ExportToCSVFile(DataTable dtTable)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName.ToUpper() + ',');
                }

                sbldr.Append("\r\n");

                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        sbldr.Append(row[column].ToString().Replace(",", ".").Replace("<br />", "").Replace("<br /><br />", "").Replace("< /br>", "").Replace("\r\n", "").Replace("\r\n\r\n", "") + ',');
                    }

                    sbldr.Append("\r\n");
                }
            }

            return sbldr.ToString();
        }

        public static string ExportDSToCSVFile(DataSet dsTable)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dsTable.Tables.Count > 0)
            {
                foreach (DataTable dtTable in dsTable.Tables)
                {
                    if (dtTable.Columns.Count != 0)
                    {
                        foreach (DataColumn col in dtTable.Columns)
                        {
                            sbldr.Append(col.ColumnName + ',');
                        }
                        
                        sbldr.Append("\r\n");

                        foreach (DataRow row in dtTable.Rows)
                        {
                            foreach (DataColumn column in dtTable.Columns)
                            {
                                sbldr.Append(row[column].ToString().Replace(",", ".").Replace("<br />", "").Replace("<br /><br />", "").Replace("< /br>", "").Replace("\r\n", "").Replace("\r\n\r\n", "") + ',');
                            }

                            sbldr.Append("\r\n");
                        }
                    }

                    sbldr.Append("\r\n\r\n");
                }
            }

            return sbldr.ToString();
        }

        public static string ExportDSToCSVFileByColumn(DataSet dsTable)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dsTable.Tables.Count > 0)
            {
                foreach (DataTable dtTable in dsTable.Tables)
                {
                    if (dtTable.Columns.Count != 0)
                    {
                        for (int column = 0; column < dtTable.Columns.Count; column++)
                        {
                            sbldr.Append(dtTable.Columns[column].ColumnName + ',');

                            for (int row = 0; row < dtTable.Rows.Count; row++)
                            {
                                sbldr.Append(dtTable.Rows[row][dtTable.Columns[column]].ToString().Replace(",", ".").Replace("<br />", "").Replace("<br /><br />", "").Replace("< /br>", "").Replace("\r\n", "").Replace("\r\n\r\n", "") + ',');

                                if (row > 0 && column == 0)
                                {
                                    for (int colIndex = 0; colIndex < column; colIndex++)
                                    {
                                        sbldr.Append(",");
                                    }

                                    sbldr.Append(",");
                                    sbldr.Append("\r\n");
                                }
                            }

                            sbldr.Append("\r\n");
                        }

                        sbldr.Append("\r\n\r\n");
                    }
                }
            }

            return sbldr.ToString();
        }

        public static void ExportDataTableToCSV(DataTable dt, string fileName)
        {
            HttpContext.Current.Response.ContentType = "Application/csv";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_Export.csv");
            HttpContext.Current.Response.Write(ExportToCSVFile(dt));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
        }

        public static void ExportPartnersDataTableToCSV(DataTable dt)
        {
            string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();

            HttpContext.Current.Response.ContentType = "Application/csv";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Partners_report_" + fileName + ".csv");
            HttpContext.Current.Response.Write(ExportToCSVFile(dt));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
        }
        
        public static void ExportDataSetToCSV(DataSet ds, string fileName)
        {
            HttpContext.Current.Response.ContentType = "Application/csv";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_Export.csv");
            HttpContext.Current.Response.Write(ExportDSToCSVFileByColumn(ds));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
        }

        public static void ExportCSV(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.Buffer = true;
                            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
                            HttpContext.Current.Response.Charset = "";
                            HttpContext.Current.Response.ContentType = "application/text";
                            HttpContext.Current.Response.Output.Write(csv);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                }
            }
        }

        //Export to .CSV file
        public static void BtnExporttoCSV_Click(object sender, EventArgs e)
        {
            DataTable dtTable = new DataTable();
            HttpContext.Current.Response.ContentType = "Application/x-msexcel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            HttpContext.Current.Response.Write(ExportToCSVFile(dtTable));
            HttpContext.Current.Response.End();
        }

        //DataTable to stringbuilder
        public static string ExportToCSVFile1(DataTable dtTable)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        sbldr.Append(row[column].ToString() + ',');
                    }
                    sbldr.Append("\r\n");
                }
            }
            return sbldr.ToString();
        }

        public static void WriteDelimitedData1(DataTable dt, string fileName, string delimiter)
        {
            //prepare the output stream
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                string.Format("attachment; filename={0}", fileName));

            string value = "";
            StringBuilder builder = new StringBuilder();

            //write the csv column headers
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                value = dt.Columns[i].ColumnName;
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                {
                    builder.Append(value);

                }

                HttpContext.Current.Response.Write(value);
                HttpContext.Current.Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
                builder.Clear();
            }

            //write the data
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    value = row[i].ToString();
                    // Implement special handling for values that contain comma or quote
                    // Enclose in quotes and double up any double quotes

                    if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                        builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                    else
                    {
                        builder.Append(value);

                    }

                    HttpContext.Current.Response.Write(builder.ToString());
                    HttpContext.Current.Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
                    builder.Clear();
                }
            }

            HttpContext.Current.Response.End();
        }


        //get datatable and file path that the file will be created, fill the file
        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        //get dataset, create a string with stringbuilder
        public static string ConvertToCSV(DataSet objDataSet)
        {
            StringBuilder content = new StringBuilder();

            if (objDataSet.Tables.Count >= 1)
            {
                DataTable table = objDataSet.Tables[0];

                if (table.Rows.Count > 0)
                {
                    DataRow dr1 = (DataRow)table.Rows[0];
                    int intColumnCount = dr1.Table.Columns.Count;
                    int index = 1;

                    //add column names
                    foreach (DataColumn item in dr1.Table.Columns)
                    {
                        content.Append(String.Format("\"{0}\"", item.ColumnName));
                        if (index < intColumnCount)
                            content.Append(",");
                        else
                            content.Append("\r\n");
                        index++;
                    }

                    //add column data
                    foreach (DataRow currentRow in table.Rows)
                    {
                        string strRow = string.Empty;
                        for (int y = 0; y <= intColumnCount - 1; y++)
                        {
                            strRow += "\"" + currentRow[y].ToString() + "\"";

                            if (y < intColumnCount - 1 && y >= 0)
                                strRow += ",";
                        }
                        content.Append(strRow + "\r\n");
                    }
                }
            }

            return content.ToString();
        }        

        /*-- End Mitsos --*/
    }
}