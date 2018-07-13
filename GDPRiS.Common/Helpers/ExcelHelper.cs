using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Common.Helpers
{
    public class ExcelHelper
    {
        public static readonly string INVALID_CELL = "xxxINVALID";

        public static byte[] ExportToExcel<T>(List<Tuple<string, string>> columns, List<T> objects)
        {
            var contentData = new List<List<object>>(objects.Count);

            foreach (T item in objects)
            {

                var row = new List<object>();

                foreach (var column in columns)
                {
                    if (typeof(T).IsPrimitive || item is string)
                    {
                        row.Add(item.ToString());
                    }
                    else
                    {
                        row.Add(typeof(T).GetProperty(column.Item1).GetValue(item, null).ToString());
                    }
                }

                contentData.Add(row);
            }

            return SaveToExcelFile(@"C:\Users\Nikola\Desktop\export.xls", columns.Select(c => c.Item2).ToList(), contentData);
        }

        public static byte[] SaveToExcelFile(string path, List<string> headerData, List<List<object>> contentData, string sheetName = "List")
        {
            var headers = new List<Tuple<int, int, object>>();
            for (int i = 0; i < headerData.Count; i++)
            {
                headers.Add(new Tuple<int, int, object>(0, i, headerData[i]));
            }

            var content = new List<Tuple<int, int, object>>();

            int headerSize = 0;
            if (headers.Count > 0)
                headerSize = 1;

            for (int i = 0; i < contentData.Count; i++)
            {
                var row = contentData[i];
                for (int j = 0; j < row.Count; j++)
                {
                    content.Add(new Tuple<int, int, object>(i + headerSize, j, row[j]));
                }
            }

            var totalContent = new List<Tuple<int, int, object>>();
            if (headers.Count > 0)
            {
                totalContent.AddRange(headers);
            }
            totalContent.AddRange(content);

            return SaveToExcelFile(totalContent, sheetName);
        }

        private static byte[] SaveToExcelFile(List<Tuple<int, int, object>> content, string sheetName)
        {
            HSSFWorkbook hssfwb = new HSSFWorkbook();
            var sheet = hssfwb.CreateSheet(sheetName);
            Dictionary<int, IRow> rows = new Dictionary<int, IRow>();

            double output;
            DateTime dt;

            var numberStyle = hssfwb.CreateCellStyle();
            var formatId = HSSFDataFormat.GetBuiltinFormat("0");
            if (formatId == -1)
            {
                var newDataFormat = hssfwb.CreateDataFormat();
                numberStyle.DataFormat = newDataFormat.GetFormat("0");
            }
            else
                numberStyle.DataFormat = formatId;

            var dateStyle = hssfwb.CreateCellStyle();
            formatId = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy");
            if (formatId == -1)
            {
                var newDataFormat = hssfwb.CreateDataFormat();
                dateStyle.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy");
            }
            else
                dateStyle.DataFormat = formatId;

            var invalidStyle = (HSSFCellStyle)hssfwb.CreateCellStyle();
            invalidStyle.FillForegroundColor = IndexedColors.Yellow.Index;
            invalidStyle.FillPattern = FillPattern.SolidForeground;

            foreach (var cell in content)
            {
                if (string.IsNullOrEmpty(cell.Item3?.ToString()))
                    continue;

                var rowId = cell.Item1;
                var columnId = cell.Item2;

                IRow row = sheet.GetRow(rowId);
                if (row == null)
                {
                    row = sheet.CreateRow(rowId);
                }

                var excelCell = row.GetCell(columnId);
                if (excelCell == null)
                {
                    excelCell = row.CreateCell(columnId);
                }

                if (cell.Item3 is DateTime)
                {
                    excelCell.SetCellValue((DateTime)cell.Item3);
                    excelCell.CellStyle = dateStyle;
                }
                else if (cell.Item3 is bool)
                {
                    excelCell.SetCellValue((bool)cell.Item3);
                }
                else if (cell.Item3 is string)
                {
                    excelCell.SetCellValue((string)cell.Item3);
                }
                else if (double.TryParse(cell.Item3.ToString(), out output))
                {
                    excelCell.SetCellValue(output);
                    excelCell.CellStyle = numberStyle;
                }
                //else if (cell.Item3.StartsWith(INVALID_CELL))
                //{
                //    excelCell.CellStyle = invalidStyle;
                //    if (cell.Item3 != INVALID_CELL)
                //        excelCell.SetCellValue(cell.Item3.Replace(INVALID_CELL, ""));
                //}
                else
                {
                    excelCell.SetCellValue(cell.Item3.ToString());
                }
            }

            var ms = new MemoryStream();
            hssfwb.Write(ms);

            return ms.ToArray();
        }

        public static List<ExcelSheet<T>> ReadFromExcelFile<T>(Stream stream)
        {
            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var result = Activator.CreateInstance<List<ExcelSheet<T>>>();
                var modelProperties = typeof(T).GetProperties().ToList();

                foreach (var worksheet in pck.Workbook.Worksheets)
                {
                    var sheet = Activator.CreateInstance<ExcelSheet<T>>();
                    sheet.Rows = new List<T>();

                    for (int row = 2; row <= worksheet.Dimension.Rows; ++row)
                    {
                        bool isValidRow = false;
                        var rowObject = Activator.CreateInstance<T>();
                        foreach (var property in modelProperties)
                        {
                            var propertyIndex = modelProperties.IndexOf(property);
                            var cell = worksheet.Cells[row, propertyIndex + 1];
                            var cellValue = cell == null ? null : cell.Value == null ? "" : cell.Value.ToString();
                            if (cell.Value != null)
                            {
                                isValidRow = true;
                            }
                            rowObject.GetType().GetProperty(property.Name).SetValue(rowObject, cellValue, null);
                        }
                        if (isValidRow)
                        {
                            sheet.SheetName = worksheet.Name;
                            sheet.Rows.Add(rowObject);
                        }
                    }

                    result.Add(sheet);
                }

                return result;
            }
        }

        public class ExcelSheet<T>
        {
            public string SheetName { get; set; }

            public List<T> Rows { get; set; }
        }
    }
}
