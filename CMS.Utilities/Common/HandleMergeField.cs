using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Reporting;
using System.IO;
using HtmlAgilityPack;
using Aspose.Words.Lists;
using Aspose.Words.Tables;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Web;
using Aspose.Words.MailMerging;
using System.Net;

namespace UtilityLayer.Common
{
    /// <summary>
    /// This class implements interfaces to control how data is inserted into merge fields during a mail merge operation.
    /// </summary>
    public class HandleMergeField : IFieldMergingCallback
    {
        public Hashtable FootNoteKeyValue = new Hashtable();
        public Hashtable SpecialNoteKeyValue = new Hashtable();
        public ArrayList RichTextFieldKeyValue = new ArrayList();

        #region private variables

        private DocumentBuilder mBuilder;

        private Run run = null;
        private Paragraph para = null;
        private Body body = null;
        private Cell cell = null;
        private Row row = null;
        private Table table = null;
        bool newRichTextField = false;
        List documentList = null;
        int maxIndentLevel = 8; // Max level of list indent supported; starting index is 0.

        #endregion

        #region implement interfaces

        /// <summary>
        /// This is called for every merge field(except Image:XXX) encountered in the document.
        /// We can either return some data to the mail merge engine or do something
        /// else with the document. In this case we modify cell formatting.
        /// </summary>
        void IFieldMergingCallback.FieldMerging(FieldMergingArgs e)
        {
            try
            {
                if (mBuilder == null)
                    mBuilder = new DocumentBuilder(e.Document);

                Document doc = e.Document;

                if (RichTextFieldKeyValue != null && (RichTextFieldKeyValue.Contains(e.FieldName)) && e.FieldValue != null && !string.IsNullOrWhiteSpace(Convert.ToString(e.FieldValue)))
                {
                    if (e.Field.Start.GetAncestor(NodeType.HeaderFooter) != null)
                        mBuilder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);

                    mBuilder.MoveToMergeField(e.FieldName);
                    mBuilder.InsertHtml(Convert.ToString(e.FieldValue));
                    e.Text = "";
                }
                else if (FootNoteKeyValue != null && FootNoteKeyValue.ContainsKey(e.FieldName))
                {
                    mBuilder.MoveToField(e.Field, true);

                    Run runFootnote = new Run(doc);
                    runFootnote.Text = " " + Convert.ToString(FootNoteKeyValue[e.FieldName]);
                    runFootnote.Font.Size = 8;
                    Footnote footnote = mBuilder.InsertFootnote(FootnoteType.Footnote, "");
                    footnote.FirstParagraph.Runs.Add(runFootnote);
                }
                else if (e.FieldName.ToLower().Contains("tbl_") && e.FieldValue != null)
                {
                    if (e.FieldValue.GetType().Name.ToLower() == "string")
                    {
                        e.Text = WebUtility.HtmlDecode(Convert.ToString(e.FieldValue));
                    }
                    else
                    {
                        this.MergeDynamicTableFields(mBuilder, e);
                    }
                }
                else if (e.FieldName.ToLower().Contains("sectionf") && e.FieldValue != null)
                {
                    if (e.FieldValue.GetType().Name.ToLower() == "string")
                    {
                    }
                    else
                    {
                        this.ProcessAndBindDataSet(mBuilder, e);
                    }
                }
                else if (e.FieldValue != null && Convert.ToString(e.FieldValue) != string.Empty)
                {
                    //decode every other text than rich text as well
                    e.Text = WebUtility.HtmlDecode(Convert.ToString(e.FieldValue));
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while making Merge Field Handler function call || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while making Merge Field Handler function call || " + ex.Message;
                }

                throw ex;
            }
        }

        /// <summary>
        /// This is called when mail merge engine encounters Image:XXX merge field in the document.
        /// You have a chance to return an Image object, file name or a stream that contains the image.
        /// </summary>
        void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs e)
        {
            // The field value is a byte array, just cast it and create a stream on it.    
            if (e.FieldValue != null)
            {
                byte[] buffer = (byte[])e.FieldValue;

                if (buffer != null)
                {
                    MemoryStream imageStream = new MemoryStream(buffer);
                    // Now the mail merge engine will retrieve the image from the stream.
                    e.ImageStream = imageStream;
                }
            }
        }

        #endregion

        private void MergeReportSummary_HeaderColumnCells(Table table, Document doc)
        {
            try
            {
                if (table.Rows.Count > 1)
                {
                    table.Rows[0].Remove();
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Header Column Cells of Report summary' node || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Header Column Cells of Report summary' node || " + ex.Message;
                }

                throw ex;
            }
        }

        private void MergeKeyRatio_ComprasionColumnCells(Table table, Document doc)
        {
            try
            {
                Cell secondRowLastCell = null;
                Cell lastRowLastCell = null;

                if (table.Rows.Count > 1)
                {
                    Row secondRow = table.Rows[1];

                    secondRowLastCell = secondRow.Cells[secondRow.Cells.Count - 1];

                    Row lastRow = table.Rows[table.Rows.Count - 1];

                    if (lastRow.Cells.Count > 0)
                    {
                        lastRowLastCell = lastRow.Cells[lastRow.Cells.Count - 1];
                    }
                }

                if (secondRowLastCell != null && lastRowLastCell != null)
                {
                    MergeCells(secondRowLastCell, lastRowLastCell);
                    Aspose.Words.Run run = new Aspose.Words.Run(doc);

                    if (SpecialNoteKeyValue["KeyRatioComparisonText"] != null)
                    {
                        run.Text = SpecialNoteKeyValue["KeyRatioComparisonText"].ToString();
                    }

                    secondRowLastCell.FirstParagraph.RemoveAllChildren();
                    secondRowLastCell.FirstParagraph.AppendChild(run);

                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Comprasion' Column Cells of MergeKeyRatio node || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Comprasion' Column Cells of MergeKeyRatio node || " + ex.Message;
                }

                throw ex;
            }
        }

        /// <summary>
        /// Merges the range of cells found between the two specified cells both horizontally and vertically. Can span over multiple rows.
        /// </summary>
        public void MergeCells(Cell startCell, Cell endCell)
        {
            try
            {
                Table parentTable = startCell.ParentRow.ParentTable;

                // Find the row and cell indices for the start and end cell.
                Point startCellPos = new Point(startCell.ParentRow.IndexOf(startCell), parentTable.IndexOf(startCell.ParentRow));
                Point endCellPos = new Point(endCell.ParentRow.IndexOf(endCell), parentTable.IndexOf(endCell.ParentRow));
                // Create the range of cells to be merged based off these indices. Inverse each index if the end cell if before the start cell. 
                Rectangle mergeRange = new Rectangle(Math.Min(startCellPos.X, endCellPos.X), Math.Min(startCellPos.Y, endCellPos.Y),
                    Math.Abs(endCellPos.X - startCellPos.X) + 1, Math.Abs(endCellPos.Y - startCellPos.Y) + 1);

                foreach (Row row in parentTable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        Point currentPos = new Point(row.IndexOf(cell), parentTable.IndexOf(row));

                        // Check if the current cell is inside our merge range then merge it.
                        if (mergeRange.Contains(currentPos))
                        {
                            if (currentPos.X == mergeRange.X)
                                cell.CellFormat.HorizontalMerge = CellMerge.First;
                            else
                                cell.CellFormat.HorizontalMerge = CellMerge.Previous;

                            if (currentPos.Y == mergeRange.Y)
                                cell.CellFormat.VerticalMerge = CellMerge.First;
                            else
                                cell.CellFormat.VerticalMerge = CellMerge.Previous;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Comparison' Column Cells of KeyRatio node || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while merging  'Comparison' Column Cells of KeyRatio node || " + ex.Message;
                }

                throw ex;
            }
        }

        private void MergeDynamicTableFields(DocumentBuilder mBuilder, FieldMergingArgs e)
        {
            try
            {
                if (e.FieldName.Contains("tbl_PressSearch") && e.FieldValue != null)
                {
                    this.ReplaceFieldWithDynamicDataSetForPressSearchOrAppendix(mBuilder, e);
                }
                else if (e.FieldName.Contains("tbl_Appendix") && e.FieldValue != null)
                {
                    this.ReplaceFieldWithDynamicDataSetForPressSearchOrAppendix(mBuilder, e);
                }
                else if (e.FieldName.ToLower().Contains("tbl_") && e.FieldValue != null) //All merge fields for tables start with "tbl_" prefix
                {
                    this.ReplaceFieldWithDynamicDataSet(mBuilder, e);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while making function call to merge table place holder field with dynamic table || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while making function call to merges table place holder field with dynamic table || " + ex.Message;
                }

                throw ex;
            }
        }

        private void ProcessAndBindDataSet(DocumentBuilder mBuilder, FieldMergingArgs e)
        {
            try
            {
                DataSet dsGeneral = (DataSet)e.FieldValue;
                e.Text = string.Empty;
                mBuilder.MoveToField(e.Field, false);

                if (dsGeneral != null && dsGeneral.Tables.Count > 0)
                {
                    foreach (DataTable dtContent in dsGeneral.Tables)
                    {
                        Table table = mBuilder.StartTable();
                        // mBuilder.RowFormat.AllowBreakAcrossPages = true;

                        foreach (DataColumn dc in dtContent.Columns)
                        {
                            string footnotevalue = string.Empty;
                            if (dc.ExtendedProperties != null && dc.ExtendedProperties.Contains("footnote"))
                                footnotevalue = dc.ExtendedProperties["footnote"].ToString();

                            mBuilder.InsertCell();
                            mBuilder.Write(dc.ColumnName);

                            if (!string.IsNullOrEmpty(footnotevalue))
                            {
                                Run runFootnote = new Run(e.Document);
                                runFootnote.Text = " " + footnotevalue;
                                runFootnote.Font.Size = 8;
                                Footnote footnote = mBuilder.InsertFootnote(FootnoteType.Footnote, "");
                                footnote.FirstParagraph.Runs.Add(runFootnote);
                            }
                        }

                        // Call the following method to end the Header row and start a new row.
                        mBuilder.EndRow();

                        int columnCount = dtContent.Columns.Count;
                        foreach (DataRow dr in dtContent.Rows)
                        {
                            for (int i = 0; i < columnCount; i++)
                            {
                                mBuilder.InsertCell();
                               if (e.Field.Start.GetAncestor(NodeType.HeaderFooter) != null)
                                mBuilder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);

                                mBuilder.MoveToMergeField(Convert.ToString(dr[i]));
                                mBuilder.InsertHtml(Convert.ToString(dr[i]));
                                e.Text = "";
                            }

                            mBuilder.EndRow();
                        }

                        mBuilder.EndTable();
                        table.StyleName = "CustomStyle1";

                        mBuilder.InsertBreak(BreakType.ParagraphBreak);

                        
                        #region Report summary

                        this.MergeReportSummary_HeaderColumnCells(table, mBuilder.Document);

                        #endregion

                        
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder field with dynamic table || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder field with dynamic table || " + ex.Message;
                }

                throw ex;
            }
        }

        private void ReplaceFieldWithDynamicDataSet(DocumentBuilder mBuilder, FieldMergingArgs e)
        {
            try
            {
                DataTable dtContent = (DataTable)e.FieldValue;
                e.Text = string.Empty;
                mBuilder.MoveToField(e.Field, false);

                //if (dsGeneral != null && dsGeneral.Tables.Count > 0)
                //{
                    //foreach (DataTable dtContent in dsGeneral.Tables)
                    //{
                        Table table = mBuilder.StartTable();
                        // mBuilder.RowFormat.AllowBreakAcrossPages = true;

                        foreach (DataColumn dc in dtContent.Columns)
                        {
                            string footnotevalue = string.Empty;
                            if (dc.ExtendedProperties != null && dc.ExtendedProperties.Contains("footnote"))
                                footnotevalue = dc.ExtendedProperties["footnote"].ToString();

                            mBuilder.InsertCell();
                            mBuilder.Write(dc.ColumnName);

                            if (!string.IsNullOrEmpty(footnotevalue))
                            {
                                Run runFootnote = new Run(e.Document);
                                runFootnote.Text = " " + footnotevalue;
                                runFootnote.Font.Size = 8;
                                Footnote footnote = mBuilder.InsertFootnote(FootnoteType.Footnote, "");
                                footnote.FirstParagraph.Runs.Add(runFootnote);
                            }
                        }

                        // Call the following method to end the Header row and start a new row.
                        mBuilder.EndRow();

                        int columnCount = dtContent.Columns.Count;
                        foreach (DataRow dr in dtContent.Rows)
                        {
                            for (int i = 0; i < columnCount; i++)
                            {
                                mBuilder.InsertCell();
                                if (dtContent.TableName.ToLower().Contains("sectionf"))
                                {
                                    if (e.Field.Start.GetAncestor(NodeType.HeaderFooter) != null)
                                        mBuilder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);

                                    mBuilder.MoveToMergeField(Convert.ToString(dr[i]));
                                    mBuilder.InsertHtml(Convert.ToString(dr[i]));
                                    e.Text = "";
                                }
                                else
                                {
                                    mBuilder.Write(WebUtility.HtmlDecode(Convert.ToString(dr[i])));
                                    StringBuilder richTextBuilder = new StringBuilder(Convert.ToString(dr[i]));
                                }
                            }

                            mBuilder.EndRow();
                        }

                        mBuilder.EndTable();
                        if (dtContent.TableName.ToLower().Contains("sectionf"))
                        {
                            table.StyleName = "CustomStyle2";
                        }
                        else
                        {
                            table.StyleName = "CustomStyle";
                        }

                        mBuilder.InsertBreak(BreakType.ParagraphBreak); 
                     
                        if (dtContent.TableName.ToLower().Contains("sectionf"))
                        {
                            this.MergeReportSummary_HeaderColumnCells(table, mBuilder.Document);
                        }
                    //}
               // }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder field with dynamic table || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder field with dynamic table || " + ex.Message;
                }

                throw ex;
            }
        }

        private void ReplaceFieldWithDynamicDataSetForPressSearchOrAppendix(DocumentBuilder mBuilder, FieldMergingArgs e)
        {
            try
            {
                DataSet dsGeneral = (DataSet)e.FieldValue;
                e.Text = string.Empty;
                mBuilder.MoveToField(e.Field, true);
                int headlineColumnIndex = -1;

                if (dsGeneral != null && dsGeneral.Tables.Count > 0)
                {
                    foreach (DataTable dtContent in dsGeneral.Tables)
                    {
                        if (!string.IsNullOrEmpty(dtContent.TableName))
                        {
                            Paragraph para = mBuilder.InsertParagraph();
                            para.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
                            Run run = new Run(mBuilder.Document);
                            run.Font.Bold = true;
                            run.Text = dtContent.TableName;
                            para.AppendChild(run);

                            //mBuilder.InsertParagraph();
                            //mBuilder.Write(dtContent.TableName);
                            //ParagraphFormat paragraphFormat = mBuilder.ParagraphFormat;
                            //paragraphFormat.Alignment = ParagraphAlignment.Justify;
                            // paragraphFormat.StyleIdentifier = StyleIdentifier.Heading3;

                         //   mBuilder.InsertBreak(BreakType.ParagraphBreak);
                          //  mBuilder.ParagraphFormat.ClearFormatting();
                        }

                        Table table = mBuilder.StartTable();
                        // mBuilder.RowFormat.AllowBreakAcrossPages = true;

                        foreach (DataColumn dc in dtContent.Columns)
                        {
                            if (dc.ExtendedProperties["nameAttribute"] != null && dc.ExtendedProperties["nameAttribute"].ToString().ToLower() != "headline")
                            {
                                string footnotevalue = string.Empty;
                                if (dc.ExtendedProperties != null && dc.ExtendedProperties.Contains("footnote"))
                                    footnotevalue = dc.ExtendedProperties["footnote"].ToString();

                                mBuilder.InsertCell();
                                mBuilder.Write(dc.ColumnName);

                                if (!string.IsNullOrEmpty(footnotevalue))
                                {
                                    Run runFootnote = new Run(e.Document);
                                    runFootnote.Text = " " + footnotevalue;
                                    runFootnote.Font.Size = 8;
                                    Footnote footnote = mBuilder.InsertFootnote(FootnoteType.Footnote, "");
                                    footnote.FirstParagraph.Runs.Add(runFootnote);
                                }
                            }
                            else
                            {
                                headlineColumnIndex = dc.Ordinal;
                            }
                        }

                        // Call the following method to end the Header row and start a new row.
                        mBuilder.EndRow();

                        int columnCount = dtContent.Columns.Count;

                        foreach (DataRow dr in dtContent.Rows)
                        {
                            for (int i = 0; i < columnCount; i++)
                            {
                                if (dr.Table.Columns[i].ExtendedProperties["nameAttribute"].ToString().ToLower() != "headline")
                                {
                                    if (dr.Table.Columns[i].ColumnName.ToLower() == "description" && headlineColumnIndex != -1)
                                    {
                                        Cell cell = mBuilder.InsertCell();
                                        Paragraph firstPara = cell.FirstParagraph;
                                        firstPara.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                        Run run = new Run(mBuilder.Document);
                                        run.Font.Bold = true;
                                        run.Text = WebUtility.HtmlDecode(Convert.ToString(dr[headlineColumnIndex]));
                                        firstPara.AppendChild(run);

                                        Paragraph newPara = new Paragraph(mBuilder.Document);
                                        newPara.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                        Run secondRun = new Run(mBuilder.Document);
                                        secondRun.Font.Bold = false;
                                        secondRun.Text = WebUtility.HtmlDecode(Convert.ToString(dr[i]));
                                        newPara.AppendChild(secondRun);

                                        cell.AppendChild(newPara);
                                    }
                                    else
                                    {
                                        mBuilder.InsertCell();
                                        mBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                        mBuilder.Write(WebUtility.HtmlDecode(Convert.ToString(dr[i])));
                                    }
                                }
                            }
                            mBuilder.EndRow();
                        }

                        headlineColumnIndex = -1; // resetting headline Column Index

                        mBuilder.EndTable();
                        table.StyleName = "CustomStyle";

                        //   mBuilder.InsertBreak(BreakType.ParagraphBreak);
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder for Press Search  with dynamic table || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while replacing table place holder for Press Search with dynamic table || " + ex.Message;
                }

                throw ex;
            }
        }

        # region Rich Text Parser

        public void ParseHtmlContent(string content, Document doc, DocumentBuilder mBuilder)
        {
            try
            {
                //Reinitialize variables
                newRichTextField = true; // Set the newRichTextField flag to true to change paragraph to current
                run = null;
                para = null;
                body = null;
                cell = null;
                row = null;
                table = null;

                Section section = new Section(doc);
                doc.AppendChild(section);

                // Lets set some properties for the section.
                section.PageSetup.SectionStart = SectionStart.Continuous;
                section.PageSetup.PaperSize = PaperSize.Letter;

                // The section that we created is empty, lets populate it. The section needs at least the Body node.
                body = new Body(doc);
                section.AppendChild(body);

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(content);

                //#region Handling less than sign '<'
                ////Check for any error in incoming HTML
                //if (htmlDoc.ParseErrors.Count() > 0 && htmlDoc.ParseErrors.Any<HtmlParseError>(o => o.Code == HtmlParseErrorCode.TagNotClosed))
                //{
                //    // replace all 'unclosed' '<' characters with "&lt;"
                //    content = PreProcess(content);
                //}

                ////Re-load the content into Agility HTMLDoc
                //htmlDoc.LoadHtml(content);

                //#endregion

                foreach (HtmlNode node in htmlDoc.DocumentNode.ChildNodes)
                {
                    TraverseChildNodes(node, doc, mBuilder);
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while traversing Root Nodes in rich text || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while traversing Root Nodes in rich text || " + ex.Message;
                }

                throw ex;
            }
        }

        private string PreProcess(string htmlInput)
        {
            // Stores the index of the last unclosed '<' character, or -1 if the last '<' character is closed.
            int lastGt = -1;

            // This list will be populated with all the unclosed '<' characters.
            List<int> gtPositions = new List<int>();

            // Collect the unclosed '<' characters.
            for (int i = 0; i < htmlInput.Length; i++)
            {
                if (htmlInput[i] == '<')
                {
                    if (lastGt != -1)
                        gtPositions.Add(lastGt);

                    lastGt = i;
                }
                else if (htmlInput[i] == '>')
                    lastGt = -1;
            }

            if (lastGt != -1)
                gtPositions.Add(lastGt);

            // If no unclosed '<' characters are found, then just return the input string.
            if (gtPositions.Count == 0)
                return htmlInput;

            // Build the output string, replace all unclosed '<' character by "&lt;".
            StringBuilder htmlOutput = new StringBuilder(htmlInput.Length + 3 * gtPositions.Count);
            int start = 0;

            foreach (int gtPosition in gtPositions)
            {
                htmlOutput.Append(htmlInput.Substring(start, gtPosition - start));
                htmlOutput.Append("&lt;");
                start = gtPosition + 1;
            }

            htmlOutput.Append(htmlInput.Substring(start));
            return htmlOutput.ToString();

        }

        public void TraverseChildNodes(HtmlNode node, Document doc, DocumentBuilder mBuilder)
        {
            try
            {
                ProcessNode(node, doc, mBuilder);

                foreach (HtmlNode chldNode in node.ChildNodes)
                {
                    TraverseChildNodes(chldNode, doc, mBuilder);
                }

                //foreach (HtmlNode chldNode in node.ChildNodes)
                //{
                //    TraverseChildNodes(chldNode, doc, mBuilder, field);
                //}
                //if (node != null)
                //{
                //    ProcessNode(node, doc, mBuilder, field);
                //}
            }
            catch (Exception ex)
            {
                //logger.Error(ex);

                if (HttpContext.Current.Session["Error"] != null && !string.IsNullOrEmpty((string)HttpContext.Current.Session["Error"]))
                {
                    HttpContext.Current.Session["Error"] = "Error occured while traversing Child Nodes in rich text || " + (string)HttpContext.Current.Session["Error"];
                }
                else
                {
                    HttpContext.Current.Session["Error"] = "Error occured while traversing Child Nodes in rich text || " + ex.Message;
                }

                throw ex;
            }
        }

        private void ProcessNode(HtmlNode node, Document doc, DocumentBuilder mBuilder)
        {
            try
            {
                if (node.PreviousSibling != null && node.PreviousSibling.Name == "table")
                    mBuilder.EndTable();

                if (node.Name == "#text")
                {
                    if (para == null && newRichTextField == true)
                    {
                        para = mBuilder.CurrentParagraph;
                        newRichTextField = false;
                        para.ParagraphFormat.ClearFormatting();
                    }
                    else if (para == null)
                    {
                        para = mBuilder.CurrentParagraph;
                        para.ParagraphFormat.ClearFormatting();
                    }

                    //This is for plain text after any tags
                    if (node.PreviousSibling != null && (node.PreviousSibling.Name.Contains("h") || node.PreviousSibling.Name == "p" || node.PreviousSibling.Name.Contains("l") || node.PreviousSibling.Name == "table"))
                    {
                        para = null;
                        mBuilder.MoveTo(mBuilder.CurrentParagraph);
                        para = mBuilder.InsertParagraph();
                        para.ParagraphFormat.ClearFormatting();
                    }

                    if (run == null)
                    {
                        run = new Run(doc);

                        //handle bold, italic and underline tags for each text node. This also handles the nested tags
                        HtmlNode parentNode = node.ParentNode;
                        while (parentNode != null)
                        {
                            if (parentNode.Name == "b" || parentNode.Name == "strong")
                                run.Font.Bold = true;
                            else if (parentNode.Name == "i")
                                run.Font.Italic = true;
                            else if (parentNode.Name == "u")
                                run.Font.Underline = Underline.Single;

                            parentNode = parentNode.ParentNode;
                        }
                    }

                    StringBuilder textBuilder = new StringBuilder(node.InnerText);
                    textBuilder = textBuilder.Replace("[gt]", "&gt;");
                    textBuilder = textBuilder.Replace("[lt]", "&lt;");
                    textBuilder = textBuilder.Replace("[quot]", "&quot;");
                    textBuilder = textBuilder.Replace("[lsquo]", "&lsquo;");
                    textBuilder = textBuilder.Replace("[rsquo]", "&rsquo;");
                    textBuilder = textBuilder.Replace("[ldquo]", "&ldquo;");
                    textBuilder = textBuilder.Replace("[rdquo]", "&rdquo;");
                    textBuilder = textBuilder.Replace("[apos]", "&apos;");
                    textBuilder = textBuilder.Replace("[amp]", "&amp;");

                    run.Text = WebUtility.HtmlDecode(textBuilder.ToString());

                    if (cell != null) // in case of table cells
                    {
                        cell.FirstParagraph.AppendChild(run);
                    }
                    else
                    {

                        para.AppendChild(run);
                    }

                    run = null;
                    cell = null;
                    //para = null;
                    //docBuilder = null;
                }
                else if (node.Name == "p")
                {
                    para = null;
                    mBuilder.MoveTo(mBuilder.CurrentParagraph); // moving the builder pointer to end of the paragraph
                    para = mBuilder.InsertParagraph();
                    //if (node.PreviousSibling != null && (node.PreviousSibling.Name == "ul" || node.PreviousSibling.Name == "ol" || node.PreviousSibling.Name == "li"))
                    para.ParagraphFormat.ClearFormatting();

                }
                else if (node.Name == "br")
                {
                    para = null;
                    mBuilder.MoveTo(mBuilder.CurrentParagraph); // moving the builder pointer to end of the paragraph
                    para = mBuilder.InsertParagraph();

                }
                else if (node.Name == "h1" || node.Name == "h2" || node.Name == "h3" || node.Name == "h4")
                {
                    ////To insert a blank paragraph before heading in case of bulleted list before it.
                    //if (node.PreviousSibling != null && (node.PreviousSibling.Name == "ul" || node.PreviousSibling.Name == "ol"))
                    //{
                    //    para = null;
                    //    mBuilder.MoveTo(mBuilder.CurrentParagraph); // moving the builder pointer to end of the paragraph
                    //    para = mBuilder.InsertParagraph();
                    //    //if (node.PreviousSibling != null && (node.PreviousSibling.Name == "ul" || node.PreviousSibling.Name == "ol"))
                    //    para.ParagraphFormat.ClearFormatting();
                    //}

                    para = null;
                    mBuilder.MoveTo(mBuilder.CurrentParagraph);
                    para = mBuilder.InsertParagraph();
                    para.ParagraphFormat.ClearFormatting();
                    //mBuilder.InsertBreak(BreakType.ParagraphBreak);

                    switch (node.Name)
                    {
                        case "h1":
                            para.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
                            break;
                        case "h2":
                            para.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
                            break;
                        case "h3":
                            para.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading3;
                            break;
                        case "h4":
                            para.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading4;
                            break;
                        default:
                            break;
                    }
                }
                else if (node.Name == "ul" || node.Name == "ol")
                {
                    documentList = null; // resetting the list object

                    if (node.ParentNode.Name == "ul")
                        documentList = doc.Lists.Add(ListTemplate.BulletDefault);
                    else if (node.ParentNode.Name == "li")
                    {
                        documentList = doc.Lists.Add(ListTemplate.NumberDefault);
                    }
                }
                else if (node.Name == "li" && node.LastChild.Name != "ul")
                {
                    //Every list item needs to be new paragraph
                    para = null;

                    if (cell != null) //incase of table cells
                    {
                        para = cell.FirstParagraph;
                    }
                    else
                    {
                        mBuilder.MoveTo(mBuilder.CurrentParagraph);
                        para = mBuilder.InsertParagraph();

                    }
                    //para.ParagraphFormat.ClearFormatting();
                 

                    if (documentList == null)
                    {
                        if (node.ParentNode.Name == "ul")
                            documentList = doc.Lists.Add(ListTemplate.BulletDefault);
                        else
                            documentList = doc.Lists.Add(ListTemplate.NumberDefault);
                    }

                    para.ListFormat.List = documentList;

                    //finding the list indent level for each list item
                    int indentLevel = 0;
                    HtmlNode tempNode = node;
                    for (indentLevel = 0; indentLevel <= maxIndentLevel; indentLevel++)
                    {
                        tempNode = tempNode.ParentNode;
                        if (tempNode == null || (tempNode.Name != "ul" && tempNode.Name != "ol"))
                            break;
                    }

                    para.ListFormat.ListLevelNumber = --indentLevel;
                }
                else if (node.Name == "table")
                {
                    mBuilder.MoveTo(mBuilder.CurrentParagraph);
                    mBuilder.InsertParagraph().ListFormat.RemoveNumbers();

                    table = mBuilder.StartTable();
                }
                else if (node.Name == "tr" || node.Name == "th")
                {
                    if (row != null)
                    {
                        try
                        {
                            mBuilder.EndRow();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    row = null;
                }
                else if (node.Name == "td")
                {
                    cell = mBuilder.InsertCell();
                    cell.FirstParagraph.ListFormat.RemoveNumbers();

                    row = cell.ParentRow;
                    table.StyleName = "CustomStyle";
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                HttpContext.Current.Session["Error"] = "Error occured while parsing rich text || " + ex.Message;

                throw ex;
            }
        }

        #endregion
    }

    public class EmptyRegionsHandler : IFieldMergingCallback
    {
        /// <summary>
        /// Called for each field belonging to an unmerged region in the document.
        /// </summary>
        public void FieldMerging(FieldMergingArgs args)
        {
            try
            {

                // Remove the entire table of the Suppliers region. Also check if the previous paragraph
                // before the table is a heading paragraph and if so remove that too.
                if (args.TableName != string.Empty)
                {
                    Table table = (Table)args.Field.Start.GetAncestor(NodeType.Table);

                    // Check if the table has been removed from the document already.
                    if (table.ParentNode != null)
                    {
                        // Try to find the paragraph which precedes the table before the table is removed from the document.
                        /*if (table.PreviousSibling != null && table.PreviousSibling.NodeType == NodeType.Paragraph)
                        {
                            Paragraph previousPara = (Paragraph)table.PreviousSibling;
                            if (IsHeadingParagraph(previousPara))
                                previousPara.Remove();
                        }*/

                        table.Remove();
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                HttpContext.Current.Session["Error"] = ex.Message;

                throw ex;
            }
        }

        /// <summary>
        /// Returns true if the paragraph uses any Heading style e.g Heading 1 to Heading 9
        /// </summary>
        private bool IsHeadingParagraph(Paragraph para)
        {
            return (para.ParagraphFormat.StyleIdentifier >= StyleIdentifier.Heading1 && para.ParagraphFormat.StyleIdentifier <= StyleIdentifier.Heading9);
        }

        public void ImageFieldMerging(ImageFieldMergingArgs args)
        {
            // Do Nothing
        }
    }

}
