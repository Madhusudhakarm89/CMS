// -----------------------------------------------------------------------
// <copyright file="Export.cs" company="Evalueserve">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UtilityLayer.Common
{
    using System;
    using System.IO;
    using System.Data;
    using UtilityLayer.Enum;
    using Aspose.Cells;

    /// <summary>
    /// Utility class exposing the functionalities to export data into excel
    /// </summary>
    public static class Export
    {
        public static byte[] Excel(DataSet inputDataSet, ExcelExportFileType exportType)
        {
            if (inputDataSet != null && inputDataSet.Tables.Count > 0)
            {
                int dataSheetCounter = 0;

                //Applying a license
                Aspose.Cells.License license = new Aspose.Cells.License();
                license.SetLicense("Aspose.Cells.lic");

                Workbook excelToExport = new Workbook();
                excelToExport.Worksheets.Clear();

                Worksheet dataSheet = null;

                foreach (DataTable inputTable in inputDataSet.Tables)
                {
                    if (inputTable != null && inputTable.Columns.Count > 0)
                    {
                        dataSheet = excelToExport.Worksheets.Add(!string.IsNullOrWhiteSpace(inputTable.TableName) ? inputTable.TableName : String.Format("Data Sheet {0}", ++dataSheetCounter));
                        WriteDataToWorkSheet(ref dataSheet, inputTable);
                        ApplyStyleToWorkSheet(ref excelToExport, ref dataSheet, dataSheet.Cells.MaxDataRow + 1, dataSheet.Cells.MaxDataColumn + 1);

                        if (exportType == ExcelExportFileType.SingleWorkSheet)
                        {
                            break;
                        }
                    }
                }

                if (excelToExport.Worksheets.Count > 0)
                {
                    //MemoryStream memoryStream = excelToExport.SaveToStream();
                    MemoryStream memoryStream = new MemoryStream();
                    excelToExport.Save(memoryStream, SaveFormat.Xlsx);

                    if (memoryStream != null && memoryStream.Length > 0)
                    {
                        memoryStream.Position = 0;

                        byte[] bytesInMemory = memoryStream.ToArray();
                        memoryStream.Flush();
                        memoryStream.Close();

                        return bytesInMemory;
                    }
                }
            }

            return null;
        }

        private static void WriteDataToWorkSheet(ref Worksheet sheet, DataTable inputDataTable)
        {
            int worksheetColumnIndex = 0, worksheetRowIndex = 0;

            foreach (DataColumn column in inputDataTable.Columns)
            {
                sheet.Cells[worksheetRowIndex++, worksheetColumnIndex].PutValue(column.ColumnName);

                foreach (DataRow row in inputDataTable.Rows)
                {
                    sheet.Cells[worksheetRowIndex++, worksheetColumnIndex].PutValue(row[column]);
                }

                // Reset Row & Column Index to write next column's data
                worksheetRowIndex = 0;
                worksheetColumnIndex += 1;
            }
        }

        private static void ApplyStyleToWorkSheet(ref Workbook excelToExport, ref Worksheet sheet, int rowsCount, int ColumnsCount)
        {
            sheet.IsGridlinesVisible = false;
            sheet.AutoFitRows();

            Range objRangeData = sheet.Cells.CreateRange(0, 0, rowsCount, ColumnsCount);
            objRangeData.Name = "DataRange";
            objRangeData.RowHeight = 15;

            Style StyleDataRange = excelToExport.Styles[excelToExport.Styles.Add()];
            StyleDataRange.Font.Name = "Arial";
            StyleDataRange.Font.Size = 10;
            StyleDataRange.Font.Color = System.Drawing.Color.Black;
            StyleDataRange.HorizontalAlignment = TextAlignmentType.Left;
            StyleDataRange.Borders.SetColor(System.Drawing.Color.FromArgb(221, 221, 221));
            StyleDataRange.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Medium;
            StyleDataRange.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Medium;
            StyleDataRange.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Medium;
            StyleDataRange.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Medium;

            StyleDataRange.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 255);
            StyleDataRange.Pattern = BackgroundType.Solid;

            //Define a style flag struct.
            StyleFlag flagDataRange = new StyleFlag();
            flagDataRange.CellShading = true;
            flagDataRange.FontName = true;
            flagDataRange.FontSize = true;
            flagDataRange.FontColor = true;
            flagDataRange.HorizontalAlignment = true;
            flagDataRange.Borders = true;
            flagDataRange.WrapText = true;

            objRangeData.ApplyStyle(StyleDataRange, flagDataRange);

            StyleDataRange = null;
            objRangeData = null;


            //Setting Range And Style For Header Data Row
            Range objRangeHeader = sheet.Cells.CreateRange(0, 0, 1, ColumnsCount);
            objRangeHeader.Name = "HeaderRange";
            objRangeHeader.RowHeight = 25;

            Aspose.Cells.Style StyleHeaderFooterRange = excelToExport.Styles[excelToExport.Styles.Add()];
            StyleHeaderFooterRange.Font.Name = "Arial";
            StyleHeaderFooterRange.Font.Size = 10;
            StyleHeaderFooterRange.Font.IsBold = true;
            StyleHeaderFooterRange.Font.Color = System.Drawing.Color.Black;
            StyleHeaderFooterRange.HorizontalAlignment = TextAlignmentType.Center;
            StyleHeaderFooterRange.VerticalAlignment = TextAlignmentType.Center;
            StyleHeaderFooterRange.Borders.SetColor(System.Drawing.Color.FromArgb(221, 221, 221));
            StyleHeaderFooterRange.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Medium;
            StyleHeaderFooterRange.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thick;
            StyleHeaderFooterRange.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Medium;
            StyleHeaderFooterRange.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Medium;
            StyleHeaderFooterRange.IsTextWrapped = true;
            StyleHeaderFooterRange.ShrinkToFit = true;

            StyleHeaderFooterRange.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 255);
            StyleHeaderFooterRange.Pattern = BackgroundType.Solid;

            //Define a style flag struct.
            StyleFlag flagHeaderFooterRange = new StyleFlag();
            flagHeaderFooterRange.CellShading = true;
            flagHeaderFooterRange.FontName = true;
            flagHeaderFooterRange.FontSize = true;
            flagHeaderFooterRange.FontColor = true;
            flagHeaderFooterRange.FontBold = true;
            flagHeaderFooterRange.HorizontalAlignment = true;
            flagHeaderFooterRange.VerticalAlignment = true;
            flagHeaderFooterRange.Borders = true;
            flagHeaderFooterRange.ShrinkToFit = true;
            flagHeaderFooterRange.WrapText = true;

            objRangeHeader.ApplyStyle(StyleHeaderFooterRange, flagHeaderFooterRange);
            objRangeHeader = null;

            sheet.AutoFitColumns();
        }
    }
}
