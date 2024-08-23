using System;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
//using Microsoft.Office.Interop.Excel;
using System.Web;
using System.Data.OleDb;

namespace WdS.ElioPlus.Lib.Utils
{
    public class ExcelReportEngine
    {
        string _filePath;

        public ExcelReportEngine(string filePath)
        {
            _filePath = filePath;
        }

        public bool ExportDataToExcel1(DataSet dataSet)
        {
            try
            {
                //Microsoft.Office.Interop.Excel.ApplicationClass excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

                //Workbook xlWorkbook = excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                //Sheets xlSheets = null;

                //Worksheet xlWorksheet = null;

                //// Loop over DataTables in DataSet.

                //DataTableCollection collection = dataSet.Tables;

                //for (int i = collection.Count; i > 0; i--)
                //{
                //    //Create Excel Sheets

                //    xlSheets = excelApp.Sheets;

                //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);

                //    System.Data.DataTable table = collection[i - 1];

                //    xlWorksheet.Name = table.TableName;

                //    for (int j = 1; j < table.Columns.Count + 1; j++)
                //    {
                //        excelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                //    }

                //    // Storing Each row and column value to excel sheet

                //    for (int k = 0; k < table.Rows.Count; k++)
                //    {
                //        for (int l = 0; l < table.Columns.Count; l++)
                //        {
                //            excelApp.Cells[k + 2, l + 1] =

                //            table.Rows[k].ItemArray[l].ToString();
                //        }
                //    }

                //    excelApp.Columns.AutoFit();
                //}

                //#region Delete if file exists

                //if (File.Exists(_filePath))
                //{
                //    File.Delete(_filePath);
                //}

                //#endregion

                //xlWorkbook.Close(true, _filePath, Type.Missing);
                //excelApp.Quit();

                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //Marshal.FinalReleaseComObject(xlWorkbook);
                //Marshal.FinalReleaseComObject(xlWorksheet);
                //Marshal.FinalReleaseComObject(excelApp);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteToCsvFile(DataSet dataSet, ref StringBuilder fileContent)
        {
            try
            {
                fileContent = new StringBuilder();

                foreach (var col in dataSet.Tables[0].Columns)
                {
                    fileContent.Append('"' + col.ToString() + '"' + ";");
                }

                fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {

                    foreach (var column in dr.ItemArray)
                    {
                        fileContent.Append("\"" + column.ToString() + "\";");
                    }

                    fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
                }

                //System.IO.File.WriteAllText(FileHelper.AddRootToPath("Yuniverse\\trunk\\WdS.ElioPlus\\test.csv"), fileContent.ToString(), System.Text.UTF8Encoding.UTF8);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DataTableToCsv(DataSet dataSet)
        {
            StringBuilder sb = new StringBuilder();

            var columnNames = dataSet.Tables[0].Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(_filePath, sb.ToString(), Encoding.UTF8);

            return true;
        }   
    }
}