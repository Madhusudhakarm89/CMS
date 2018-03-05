// -----------------------------------------------------------------------
// <copyright file="ExcelTools.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UtilityLayer.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Drawing.Spreadsheet;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using C = DocumentFormat.OpenXml.Drawing.Charts;
    using Drawing = DocumentFormat.OpenXml.Drawing;

    /// <summary>
    /// Tools to build Excel documents using openXML API.
    /// </summary>
    public static class ExcelTools
    {
        /// <summary>
        /// Creates a SpreadsheetDocument
        /// </summary>
        /// <param name="filePath">file to write to</param>
        /// <param name="dtSource">source to populate</param>
        /// <param name="encloseDataInTable">True to enclose data in a table with blue borders (allows pptx to bind source range to a chart)</param>
        public static void CreatePackage(string filePath, DataTable dtSource, bool encloseDataInTable)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package, dtSource, encloseDataInTable);
            }
        }

        // Adds child parts and generates content of the specified part
        private static void CreateParts(SpreadsheetDocument document, DataTable dtSource, bool encloseDataInTable)
        {
            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            if (encloseDataInTable)
            { // need to render table border style
                WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId3");
                GenerateWorkbookStylesContent(workbookStylesPart1);
            }

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPart1Content(worksheetPart1, dtSource, encloseDataInTable);
        }

        // Generates content of workbookStylesPart1.
        // (borders needed for user to resize tables). Other style content skipped.
        private static void GenerateWorkbookStylesContent(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet();
            // if your data includes a table, this renders the blue borders to make it easier to see.
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)2U };
            DifferentialFormat differentialFormat1 = new DifferentialFormat();
            NumberingFormat numberingFormat1 = new NumberingFormat() { NumberFormatId = (UInt32Value)1U, FormatCode = "0" };
            differentialFormat1.Append(numberingFormat1);
            DifferentialFormat differentialFormat2 = new DifferentialFormat();
            Border border3 = new Border() { DiagonalUp = false, DiagonalDown = false };
            LeftBorder leftBorder3 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color5 = new Color() { Indexed = (UInt32Value)12U };
            leftBorder3.Append(color5);
            RightBorder rightBorder3 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color6 = new Color() { Indexed = (UInt32Value)12U };
            rightBorder3.Append(color6);
            TopBorder topBorder3 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color7 = new Color() { Indexed = (UInt32Value)12U };
            topBorder3.Append(color7);
            BottomBorder bottomBorder3 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color8 = new Color() { Indexed = (UInt32Value)12U };
            bottomBorder3.Append(color8);
            border3.Append(leftBorder3);
            border3.Append(rightBorder3);
            border3.Append(topBorder3);
            border3.Append(bottomBorder3);
            differentialFormat2.Append(border3);
            differentialFormats1.Append(differentialFormat1);
            differentialFormats1.Append(differentialFormat2);
            stylesheet1.Append(differentialFormats1); // border used for table
            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        // Generates content of tableDefinitionPart1.
        private static void GenerateTableDefinitionPart1Content(TableDefinitionPart tableDefinitionPart1, DataTable dtSource)
        {
            if (dtSource.Columns.Count > 26)
                throw new NotSupportedException("More than 26 columns are currently not supported in ExcelTools."); // add later if needed
            char colA = 'A';
            char colEnd = (char)((int)colA + (dtSource.Columns.Count - 1));
            int row1 = 1;
            int rowEnd = dtSource.Rows.Count + 1; // include header row
            string tblReference = string.Format("{0}{1}:{2}{3}", colA, row1, colEnd, rowEnd);
            Table table1 = new Table() { Id = (UInt32Value)1U, Name = "Table1", DisplayName = "Table1", Reference = tblReference, TotalsRowShown = false, BorderFormatId = (UInt32Value)1U };

            TableColumns tableColumns1 = new TableColumns() { Count = (UInt32Value)(uint)dtSource.Columns.Count };
            int colId = 1;
            foreach (DataColumn dc in dtSource.Columns)
            {
                TableColumn tableColumn1 = new TableColumn() { Id = (UInt32Value)(uint)colId, Name = dc.ColumnName };
                tableColumns1.Append(tableColumn1);
                colId++;
            }

            TableStyleInfo tableStyleInfo1 = new TableStyleInfo() { ShowFirstColumn = false, ShowLastColumn = false, ShowRowStripes = true, ShowColumnStripes = false };
            table1.Append(tableColumns1);
            table1.Append(tableStyleInfo1);
            tableDefinitionPart1.Table = table1;
        }


        // Generates content of workbookPart1.
        private static void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
            sheets1.Append(sheet1);

            workbook1.Append(sheets1);
            workbookPart1.Workbook = workbook1;
        }

        // Generates content of worksheetPart1.
        private static void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1, DataTable dtSource, bool encloseInDataTable)
        {
            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            SheetData sheetData1 = new SheetData();
            // todo: refactor to use bulk xml insert if performance is slow due to large data sets
            Row row1 = new Row() { RowIndex = 1U };
            Cell cell1;
            InlineString inlineString1;
            Text text1;
            CellValue cv;
            // add header row
            char charCol = 'A';
            foreach (DataColumn dc in dtSource.Columns)
            {
                cell1 = new Cell() { CellReference = charCol.ToString() + "1", DataType = CellValues.InlineString };
                inlineString1 = new InlineString();
                text1 = new Text();
                text1.Text = dc.ColumnName;
                inlineString1.Append(text1);
                cell1.Append(inlineString1);
                row1.Append(cell1);
                charCol = (char)((int)charCol + 1);
            }
            sheetData1.Append(row1);
            // add rows
            int rowIx = 2;
            foreach (DataRow dr in dtSource.Rows)
            {
                row1 = new Row() { RowIndex = (uint)rowIx };
                charCol = 'A';
                foreach (DataColumn dc in dtSource.Columns)
                {
                    if (dc.DataType == typeof(int) || dc.DataType == typeof(decimal))
                    {
                        cell1 = new Cell() { CellReference = charCol.ToString() + rowIx.ToString() };
                    }
                    else
                    {
                        cell1 = new Cell() { CellReference = charCol.ToString() + rowIx.ToString(), DataType = CellValues.String };
                    }

                    cv = new CellValue();
                    cv.Text = dr[dc.ColumnName].ToString();
                    cell1.Append(cv);
                    row1.Append(cell1);
                    charCol = (char)((int)charCol + 1);
                }
                sheetData1.Append(row1);
                rowIx++;
            }
            worksheet1.Append(sheetData1);
            string tableReferenceId = "rId2";
            if (encloseInDataTable)
            {
                TableParts tableParts1 = new TableParts() { Count = (UInt32Value)1U };
                TablePart tablePart1 = new TablePart() { Id = tableReferenceId };
                tableParts1.Append(tablePart1);
                worksheet1.Append(tableParts1);
            }

            worksheetPart1.Worksheet = worksheet1;

            if (encloseInDataTable)
            {
                TableDefinitionPart tableDefinitionPart1 = worksheetPart1.AddNewPart<TableDefinitionPart>(tableReferenceId);
                GenerateTableDefinitionPart1Content(tableDefinitionPart1, dtSource);
            }
        } // generate content
    } // class
} // ns 
