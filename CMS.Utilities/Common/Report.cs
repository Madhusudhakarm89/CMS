using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.MailMerging;
using ModelAccessLayer.Awards.Model.SurveyStructure;
using UtilityLayer.Enum;

namespace UtilityLayer.Common
{
    public static class Report
    {
        #region private variables

        private static HandleMergeField handleMergeFieldObj;
        private static string reportType = "DOCX";
        private static Document destdoc;

        #endregion

        public static void GeneratePreliminaryReport(ReportData reportData, string templatePath, string reportPath)
        {
            reportType = "PDF";

            //Applying a license
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Fetchind Document from the template path
            Document doc = new Document(templatePath);

            if (reportData != null)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[0];
                if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                {
                    //Binding data in PDF
                    ProcessAndBindData(ref doc, reportData);
                }
            }

           // doc.UpdateTableLayout();
            doc.Save(reportPath, Aspose.Words.SaveFormat.Pdf);

            GC.Collect();
        }

        public static void GenerateFeedbackReport(ReportData reportData, string templatePath, string reportPath, string filepath)
        {
            //Applying a license
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Fetchind Document from the template path
            Document doc = new Document(templatePath);

            if (reportData != null)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[0];
                if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                {
                    //Binding data in Docx
                    ProcessAndBindData(ref doc, reportData);
                }
            }
            doc.Save(reportPath, Aspose.Words.SaveFormat.Docx);

            GC.Collect();

            CreateChart(ref doc, reportPath, reportData, filepath);
        }

        public static void GenerateInitialJuryReport(DataSet ds, string templatePath, string reportPath, bool isPreSite = false)
        {
            try
            {
                //Applying a license
                Aspose.Words.License license = new Aspose.Words.License();
                license.SetLicense("Aspose.Words.lic");

                //Fetchind Document from the template path
                Document doc = new Document(templatePath);

                if (ds != null)
                {
                    doc.MailMerge.Execute(ds.Tables[0]);

                    if (isPreSite)
                        doc.MailMerge.Execute(new string[] { "ReportType" }, new object[] { "DESK ASSESSMENT" });
                    else
                        doc.MailMerge.Execute(new string[] { "ReportType" }, new object[] { "SITE ASSESSMENT" });
                }
                doc.Save(reportPath, Aspose.Words.SaveFormat.Docx);

                destdoc = doc;
                GC.Collect();
            }
            catch (Exception ex)
            {
                ex = new Exception("Report -> GenerateInitialJuryReport: \r\n\r\n", ex);
                LogManager.WriteErrorLog(ex);
                destdoc = null;
                throw ex;
            }
        }

        public static void AppendJuryCompanyQuesionareReport(ReportData reportData, string templatePath, string reportPath, string filepath,char alphabet,bool isClassificationText)
        {
            try
            {
                //Applying a license
                Aspose.Words.License license = new Aspose.Words.License();
                license.SetLicense("Aspose.Words.lic");

                //Fetchind Document from the template path
                Document doc = new Document(templatePath);

                if (reportData != null)
                {
                    DataTable dtMergeFields = reportData.NonTabularData.Tables[0];
                    if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                    {
                        //Binding data in Docx
                        ProcessAndBindData(ref doc, reportData);

                        if (isClassificationText)
                        {
                            var categoryName = "Category " + alphabet;
                            if (alphabet == 'F')
                            {
                                categoryName += " [Independent Unit]";
                            }
                            doc.MailMerge.Execute(new string[] { "company_classification" }, new object[] { categoryName });
                        }
                    }
                }

                RemoveEmptyParagraphsExceptChart(doc);
                // doc.Save(reportPath, Aspose.Words.SaveFormat.Docx);

                //Appending Document to Final Jury Document 
                destdoc.AppendDocument(doc, ImportFormatMode.KeepSourceFormatting);
            }
            catch (Exception ex)
            {
                ex = new Exception("Report -> AppendJuryCompanyQuesionareReport: \r\n\r\n", ex);
                LogManager.WriteErrorLog(ex);
                destdoc = null;
                throw ex;
            }

            GC.Collect();
        }

        public static void UpdateTableOfContent(string filepath)
        {
            try
            {
                RemoveBlankPages(destdoc);

                //Update Table of Content
                destdoc.UpdateFields();

                //Save the Final Report
                destdoc.Save(filepath, Aspose.Words.SaveFormat.Docx);
            }
            catch (Exception ex)
            {
                ex = new Exception("Report -> UpdateTableOfContent: \r\n\r\n", ex);
                LogManager.WriteErrorLog(ex);

                throw ex;
            }

            destdoc = null;
            GC.Collect();
        }

        public static void AppendJuryTabularDetail(EnumerableRowCollection<DataRow> dataRows, string filepath, string productName)
        {
            try
            {
                Aspose.Words.DocumentBuilder db = new Aspose.Words.DocumentBuilder(destdoc);
                db.MoveToDocumentEnd();

                StringBuilder sb = new StringBuilder();
                sb.Append("<html><head><style>table, th, td {border: 1px solid black;border-collapse: collapse;text-align:center;font-size:15px;}</style></head><body>");
                sb.Append("<table cellpadding='2' style='color:black;width:100%'><tbody>");
                sb.AppendFormat("<tr><td style='font-weight:bold;font-size:17px;' colspan='3'>{0}</td></tr>", productName);
                sb.AppendFormat("<tr><td style='font-weight:bold;'>Sr. No.</td><td style='font-weight:bold;'>Applicant</td><td style='font-weight:bold;'>Total Score</td></tr>");

                var count = 1;
                for (var index = 65; index <= 71; index++)
                {
                    Char alphabet = (Char)index;
                    var data = dataRows.Where(dr => dr.Field<string>("ClassificationName") == alphabet.ToString()).ToList();
                    if (data != null && data.Count > 0)
                    {
                        sb.AppendFormat("<tr><td colspan='3'>Turnover Classification {0}</td></tr>", alphabet);
                        for (var j = 0; j < data.Count(); j++)
                        {
                            sb.AppendFormat("<tr><td>{0}</td><td style='font-weight:bold;'>{1}</td><td style='font-weight:bold;'>{2}</td></tr>", count, data[j]["OrganisationName"], data[j]["Score"]);
                            count++;
                        }
                    }
                }

                sb.AppendLine("</tbody></table></body></html>");

                db.InsertBreak(BreakType.PageBreak);
                db.InsertHtml(sb.ToString(), false);

            }
            catch (Exception ex)
            {
                ex = new Exception("Report -> AppendJuryTabularDetail: \r\n\r\n", ex);
                LogManager.WriteErrorLog(ex);
                destdoc = null;
                throw ex;
            }

           
            GC.Collect();
        }

        public static void AppendCategoryText(string filepath, string categoryName)
        {
            try
            {
                Aspose.Words.DocumentBuilder db = new Aspose.Words.DocumentBuilder(destdoc);
                db.MoveToDocumentEnd();

                string categoryText = "<div style='font-weight:bold;text-decoration:underline;width:800px;text-align: center;'>Category " + categoryName + "</div>";
                if (categoryName == "F")
                {
                    categoryText = "<div style='font-weight:bold;text-decoration:underline;width:800px;text-align: center;'>Category " + categoryName + " [Independent Unit]</div>";
                }

                db.InsertBreak(BreakType.PageBreak);
                db.InsertHtml(categoryText, false);
            }
            catch (Exception ex)
            {
                ex = new Exception("Report -> AppendCategoryText: \r\n\r\n", ex);
                LogManager.WriteErrorLog(ex);
                destdoc = null;
                throw ex;
            }
            GC.Collect();
        }

        public static void CreateChart(ref Document doc, string reportPath, ReportData reportData, string filepath)
        {
            var organisationName = reportData.NonTabularData.Tables[0].Rows[0]["OrganisationName"].ToString();
            string chartPartRId = "chart_AspectWiseScore";
            string excelSourceId = "rId200";
            DataTable table = reportData.NonTabularData.Tables[2];
            ChartArea objCompanyPerformanceChartArea = BindChartData(table, "Radar", organisationName);
            (new ReportChart()).GenerateWordChart(objCompanyPerformanceChartArea, reportPath, chartPartRId, excelSourceId, filepath);

            string chartPartRId1 = "chart_AttributeWiseScore";
            string excelSourceId1 = "rId201";
            DataTable table1 = reportData.NonTabularData.Tables[4];
            ChartArea objCompanyPerformanceChartArea1 = BindChartData(table1, "Radar", organisationName);
            (new ReportChart()).GenerateWordChart(objCompanyPerformanceChartArea1, reportPath, chartPartRId1, excelSourceId1, filepath);
        }

        public static ChartArea BindChartData(DataTable table, string type, string organisationName)
        {
            ChartArea chartArea = new ChartArea();
            chartArea.ChartType = type;
            chartArea.ChartSubAreaList = new List<ChartSeries>();
            
            for (var j = 0; j < table.Columns.Count-1; j++)
            {
                ChartSeries series = new ChartSeries();
                if (j == 0)
                {
                    series.SeriesTitle = organisationName;
                }
                else
                {
                    series.SeriesTitle = table.Columns[j + 1].ToString();
                }
                series.SeriesType = type;
                series.ChartDataList = new List<ChartData>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    ChartData data = new ChartData();
                    data.XaxisLabel = table.Rows[i][0].ToString();
                    data.YaxisValue = table.Rows[i][j+1].ToString();
                    data.DataType = "number";
                    series.ChartDataList.Add(data);
                }
                chartArea.ChartSubAreaList.Add(series);
            }
            return chartArea;
        }

        public static void ProcessAndBindData(ref Document doc, ReportData reportData)
        {
            // Add a handler for the MergeField event.
            handleMergeFieldObj = new HandleMergeField();
            doc.MailMerge.FieldMergingCallback = handleMergeFieldObj;

            //Supply RichTextField name to HandleMerge class object
            handleMergeFieldObj.RichTextFieldKeyValue = new ArrayList() { "RichText_ExecutiveSummary" };

            for (var index = 0; index < reportData.NonTabularData.Tables.Count; index++)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[index];
                if (dtMergeFields.TableName.StartsWith("Table_"))
                {
                    doc.MailMerge.ExecuteWithRegions(dtMergeFields);
                }
                else if (dtMergeFields.TableName.ToLower().StartsWith("tbl_"))
                {
                    doc.MailMerge.Execute(new string[] { dtMergeFields.TableName }, new object[] { dtMergeFields });
                }
                else if (dtMergeFields.TableName == "SectionF")
                {
                    DataTable assessorViewtable = new DataTable();
                    if (reportData.NonTabularData.Tables.Count > 17)
                    {
                        assessorViewtable = reportData.NonTabularData.Tables[17];
                    }
                    ProcessAndBindSectionF(ref doc, dtMergeFields, reportData.TabularData, reportData.NonTabularData.Tables[15], reportData.NonTabularData.Tables[16], assessorViewtable);
                }
                else if (dtMergeFields.TableName == "Feedback_SectionD")
                {
                    ProcessAndBindSectionF(ref doc, dtMergeFields, reportData.TabularData, reportData.NonTabularData.Tables[6]);
                }
                else if (dtMergeFields.TableName == "JuryTable")
                {
                    ProcessAndBindSectionF(ref doc, dtMergeFields, reportData.TabularData, reportData.NonTabularData.Tables[2]);
                }
                else
                {
                    if (dtMergeFields.TableName != "AssessorView" && dtMergeFields.TableName != "AditionalInfo" && dtMergeFields.TableName != "Validation" && !dtMergeFields.TableName.StartsWith("chart_"))
                    {
                        doc.MailMerge.Execute(dtMergeFields);
                    }
                }
            }

            RemoveBlankPages(doc);
        }

        private static void ProcessAndBindSectionF(ref Document doc, DataTable sectionFData, DataSet tabularData, DataTable additionalInfo, DataTable validation = null, DataTable assessorView = null)
        {
            var completeQuestions = sectionFData.AsEnumerable();
            var totalAspects = completeQuestions.Select(dr => dr.Field<string>("AspectName")).Distinct();
            var additionalInformation = additionalInfo.AsEnumerable();
            if (assessorView == null) { assessorView = new DataTable(); }
            var assessorComment = assessorView.AsEnumerable();
            int Index = 1;

            foreach (var Aspect in totalAspects)
            {
                var strenghts = completeQuestions.Where(dr => dr.Field<string>("Qualifier") == ReportClassification.Strength.ToString() && dr.Field<string>("AspectName") == Aspect);
                var opportunities = completeQuestions.Where(dr => dr.Field<string>("Qualifier") == ReportClassification.Opportunity.ToString() && dr.Field<string>("AspectName") == Aspect);
                var informationGaps = completeQuestions.Where(dr => dr.Field<string>("Qualifier") == ReportClassification.InformationGap.ToString() && dr.Field<string>("AspectName") == Aspect);
                var validations = completeQuestions.Where(dr => dr.Field<string>("Qualifier") == ReportClassification.Validation.ToString() && dr.Field<string>("AspectName") == Aspect);
                var aspectAdditioanlInfo = additionalInformation.Where(dr => dr.Field<string>("AspectName") == Aspect).Select(dr => dr.Field<string>("Comment")).FirstOrDefault() ?? "Not Available";
                var assessorComments = assessorComment.Where(dr => dr.Field<string>("AspectName") == Aspect).Select(dr => dr.Field<string>("Comment")).FirstOrDefault() ?? "Not Available";

                if (Aspect != Constant.ComplainceToRegulation)
                {
                    //Binding Strengths
                    DataSet strengthDataSet = new DataSet();
                    strengthDataSet.DataSetName = "SECTIONF" + Index + "_STRENGTHS";
                    BindDataSet(strenghts, ref strengthDataSet, tabularData, validation);
                    if (strengthDataSet.Tables.Count > 0)
                    {
                        doc.MailMerge.Execute(new string[] { strengthDataSet.DataSetName }, new object[] { strengthDataSet });
                    }
                    else
                    {
                        doc.MailMerge.Execute(new string[] { strengthDataSet.DataSetName }, new object[] { "Not Applicable" });
                    }

                    //Binding opportunities
                    DataSet opportunityDataSet = new DataSet();
                    opportunityDataSet.DataSetName = "SECTIONF" + Index + "_OPPORTUNITIES";
                    BindDataSet(opportunities, ref opportunityDataSet, tabularData, validation);
                    if (opportunityDataSet.Tables.Count > 0)
                    {
                        doc.MailMerge.Execute(new string[] { opportunityDataSet.DataSetName }, new object[] { opportunityDataSet });
                    }
                    else
                    {
                        doc.MailMerge.Execute(new string[] { opportunityDataSet.DataSetName }, new object[] { "Not Applicable" });
                    }

                    //Binding Descriptive Questions
                    DataSet validationDataSet = new DataSet();
                    validationDataSet.DataSetName = "SECTIONF" + Index + "_DESCRIPTIVE";
                    BindDataSet(validations, ref validationDataSet, tabularData, validation);
                    if (validationDataSet.Tables.Count > 0)
                    {
                        doc.MailMerge.Execute(new string[] { validationDataSet.DataSetName }, new object[] { validationDataSet });
                    }
                    else
                    {
                        doc.MailMerge.Execute(new string[] { validationDataSet.DataSetName }, new object[] { "Not Applicable" });
                    }

                    //Binding InfomationGaps
                    DataTable infoGapDataTable = new DataTable();
                    infoGapDataTable.Columns.Add();
                    infoGapDataTable.TableName = "TBL_SECTIONF" + Index + "_INFORMATIONGAP";

                    foreach (var informationGap in informationGaps)
                    {
                        infoGapDataTable.Rows.Add(informationGap["QuestionText"]);
                    }

                    if (infoGapDataTable.Rows.Count > 0)
                    {
                        doc.MailMerge.Execute(new string[] { infoGapDataTable.TableName }, new object[] { infoGapDataTable });
                    }
                    else
                    {
                        doc.MailMerge.Execute(new string[] { infoGapDataTable.TableName }, new object[] { "Not Applicable" });
                    }
                }
                else
                {
                    DataSet complainceToRegulation = new DataSet();
                    var Question = completeQuestions.Where(dr => dr.Field<string>("AspectName") == Constant.ComplainceToRegulation);
                    complainceToRegulation.DataSetName = "SECTIONF" + Index;
                    BindDataSet(Question, ref complainceToRegulation, tabularData);

                    if (complainceToRegulation.Tables.Count > 0)
                    {
                        doc.MailMerge.Execute(new string[] { complainceToRegulation.DataSetName }, new object[] { complainceToRegulation });
                    }
                    else
                    {
                        doc.MailMerge.Execute(new string[] { complainceToRegulation.DataSetName }, new object[] { "Not Applicable" });
                    }
                }

                //Binding Additional Information
                var additionInfoMergeFieldName = "ADDITIONALINFO_F" + Index;
                doc.MailMerge.Execute(new string[] { additionInfoMergeFieldName }, new object[] { aspectAdditioanlInfo });

                //Binding Assessor Comment
                var assessorCommentMergeFieldName = "ASSESSORVIEW_F" + Index;
                doc.MailMerge.Execute(new string[] { assessorCommentMergeFieldName }, new object[] { assessorComments });
                Index++;
            }
        }

        private static DataSet BindDataSet(EnumerableRowCollection<DataRow> items, ref DataSet ds, DataSet tabularData, DataTable validation = null)
        {
            foreach (var item in items)
            {
                DataTable datatable = new DataTable();
                datatable.Columns.Add();

                datatable.Rows.Add(item["QuestionText"]);
                if (item["QuestionType"].ToString() != "Tabular")
                {
                    datatable.Rows.Add(item["AnsweredOptions"]);
                }
                else
                {
                    var rowData = item["AnsweredOptions"].ToString();
                    var tabularOptionIds = item["AnsweredOptionIds"].ToString().Split(',');
                    foreach (var optionId in tabularOptionIds)
                    {
                        DataTable dt = FindDataTable(tabularData, optionId);
                        if (dt.Rows.Count > 0)
                        {
                            var dataTableHtml = ConvertDT2HTMLString(dt);
                            rowData = rowData.Replace("#DT_" + optionId, dataTableHtml);
                        }
                    }
                    datatable.Rows.Add(rowData);
                }

                //Adding Validation Row
                if (validation != null)
                {
                    var questionId = Convert.ToInt32(item["QuestionId"]);
                    var answeredOptionIds = item["AnsweredOptionIds"].ToString().Split(',');
                    if (answeredOptionIds.Count() > 0)
                    {
                        BindValidation(questionId, answeredOptionIds, validation.AsEnumerable(), ref datatable);
                    }
                }

                ds.Tables.Add(datatable);
            }
            return ds;
        }

        //Bind Validation
        private static void BindValidation(int QuestionId,string[] answeredOptionIds, EnumerableRowCollection<DataRow> validation, ref DataTable dataTable)
        {
            string rowData = "";
            var dataRows = validation.Where(dr => dr.Field<Int32>("QuestionId") == QuestionId && answeredOptionIds.Contains(dr.Field<Int32>("OptionId").ToString()));
            if (dataRows != null && dataRows.Count() > 0)
            {
                rowData = "<b><u>Validations:</u></b><br>";
                var options = dataRows.GroupBy(dr => dr.Field<string>("OptionText"));
                foreach (var option in options)
                {
                    if (option.Key != null)
                    {
                        rowData += "<b>" + option.Key + "</b></br>";
                    }
                    var optionList = option.Where(dr => dr.Field<Int32>("ControlId") == 6);
                    if (optionList != null && optionList.Count() > 0)
                    {
                        foreach (var label in optionList)
                        {
                            var labelId = Convert.ToInt32(label["ValidationBoxScoreId"]);
                            rowData += label["ScoreText"];
                            var radioList = option.Where(dr => dr.Field<Int32>("ControlId") == 1 && dr.Field<Nullable<Int32>>("ParentValidationBoxScoreId") == labelId);
                            rowData = GenerateValidationHierarcy(rowData, option, radioList);
                        }
                    }
                    else
                    {
                        var radioList = option.Where(dr => dr.Field<Int32>("ControlId") == 1);
                        rowData = GenerateValidationHierarcy(rowData, option, radioList);
                    }
                }

                dataTable.Rows.Add(rowData);
            }
        }

        private static string GenerateValidationHierarcy(string rowData, IGrouping<string, DataRow> option, IEnumerable<DataRow> radioList)
        {
            rowData += "<ul style='margin-top:0px;list-style-type: disc;'>";
            foreach (var radio in radioList)
            {
                var radioId = Convert.ToInt32(radio["ValidationBoxScoreId"]);
                rowData += "<li>" + radio["ScoreText"] + "</li>";
                var checkList = option.Where(dr => dr.Field<Int32>("ControlId") == 2 && dr.Field<Nullable<Int32>>("ParentValidationBoxScoreId") == radioId);
                if (checkList != null && checkList.Count() > 0)
                {
                    rowData += "<ul style='margin-top:0px;list-style-type: square;'>";
                    foreach (var check in checkList)
                    {
                        rowData += "<li>" + check["ScoreText"] + "</li>";
                    }
                    rowData += "</ul>";
                }
            }
            rowData += "</ul>";
            return rowData;
        }

        //Find Matching Datatable from Dataset
        private static DataTable FindDataTable(DataSet tabularData,string optionId)
        {
            var dt = new DataTable();
            for (var index = 0; index < tabularData.Tables.Count; index++)
            {
                if (tabularData.Tables[index].Rows[0]["OptionId"].ToString() == optionId)
                {
                    dt = tabularData.Tables[index];
                    break;
                }
            }

            return dt;
        }

        // Converting datatable to HTML string 
        private static string ConvertDT2HTMLString(DataTable dt)
        {
            var tableStyle = "border:1px solid black;float:left;";
            var tableWidth = string.Empty;

            if (reportType == "PDF")
            {
                tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:650px;font-size:12px;";
                tableWidth = "650px";
                if (dt.Columns.Count > 8)
                {
                    tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:650px;font-size:8px;";
                }
                else if (dt.Columns.Count > 6 && dt.Columns.Count <= 8)
                {
                    tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:650px;font-size:10px;";
                }
            }
            else
            {
                tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:580px;font-size:12px;";
                tableWidth = "580px";
                if (dt.Columns.Count > 8)
                {
                    tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:580px;font-size:8px;";
                }
                else if (dt.Columns.Count > 6 && dt.Columns.Count <= 8)
                {
                    tableStyle = "border:1px solid black;float:left; table-layout: fixed;width:580px;font-size:10px;";
                }
            }
            var count = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellspacing='0' style='{0}' width='{1}'><thead><tr>", tableStyle, tableWidth);

            foreach (DataColumn c in dt.Columns)
            {
                if (count >= 3)
                {
                    sb.AppendFormat("<td style='border:1px solid black;font-weight:bold;'>{0}</td>", c.ColumnName);
                }
                count++;
            }
            sb.AppendLine("</tr></thead><tbody>");
            foreach (DataRow dr in dt.Rows)
            {
                count = 1;
                sb.Append("<tr>");
                foreach (object o in dr.ItemArray)
                {
                    if (count >= 3)
                    {
                        sb.AppendFormat("<td style='border:1px solid black;'><span style='display:block;'>{0}</span></td>", System.Web.HttpUtility.HtmlEncode(o.ToString()));
                    }
                    count++;
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody></table>");
            return sb.ToString();
        }

        private static void RemoveBlankPages(Document doc)
        {
            try
            {
                #region Remove last empty page from the document

                // Remove the empty paragraphs if necessary.

                while (!doc.LastSection.Body.LastParagraph.HasChildNodes)
                {

                    if (doc.LastSection.Body.LastParagraph.PreviousSibling != null &&

                            doc.LastSection.Body.LastParagraph.PreviousSibling.NodeType != NodeType.Paragraph)

                        break;

                    doc.LastSection.Body.LastParagraph.Remove();

                    // If the current section becomes empty, we should remove it.

                    if (!doc.LastSection.Body.HasChildNodes)

                        doc.LastSection.Remove();

                    // We should exit the loop if the document becomes empty.

                    if (!doc.HasChildNodes)

                        break;

                }

                //Last empty page removed from the document
                #endregion

                NodeCollection paragraphs = doc.GetChildNodes(NodeType.Paragraph, true);

                foreach (Aspose.Words.Paragraph para in paragraphs)
                {
                    if (para.ParagraphFormat.PageBreakBefore)
                    {
                        Node paraNode = para.PreviousSibling;

                        while (paraNode != null && paraNode is Aspose.Words.Paragraph && !((Aspose.Words.Paragraph)paraNode).HasChildNodes)
                        {
                            paraNode.Remove();
                            paraNode = para.PreviousSibling;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AppendDocument(Document dstDoc, Document srcDoc, ImportFormatMode mode)
        {
            // Loop through all sections in the source document. 
            // Section nodes are immediate children of the Document node so we can just enumerate the Document.
            foreach (Section srcSection in srcDoc)
            {
                // Because we are copying a section from one document to another, 
                // it is required to import the Section node into the destination document.
                // This adjusts any document-specific references to styles, lists, etc.
                //
                // Importing a node creates a copy of the original node, but the copy
                // is ready to be inserted into the destination document.
                Node dstSection = dstDoc.ImportNode(srcSection, true, mode);

                // Now the new section node can be appended to the destination document.
                dstDoc.AppendChild(dstSection);
            }
        }

        public static void GenerateApplicationDocumentReport(ReportData reportData, string templatePath, string reportPath)
        {
            //Applying a license
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Fetchind Document from the template path
            Document doc = new Document(templatePath);

            if (reportData != null)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[0];
                if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                {
                    //Binding data in PDF
                    ProcessAndBindApplicationReportData(ref doc, reportData);
                }
            }

            doc.Save(reportPath, Aspose.Words.SaveFormat.Pdf);

            GC.Collect();
        }

        public static void ProcessAndBindApplicationReportData(ref Document doc, ReportData reportData)
        {
            // Add a handler for the MergeField event.
            handleMergeFieldObj = new HandleMergeField();
            doc.MailMerge.FieldMergingCallback = handleMergeFieldObj;

            //Supply RichTextField name to HandleMerge class object
            handleMergeFieldObj.RichTextFieldKeyValue = new ArrayList() { "RichText_ExecutiveSummary" };

            for (var index = 0; index < reportData.NonTabularData.Tables.Count; index++)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[index];
                if (dtMergeFields.TableName.StartsWith("Table_"))
                {
                    doc.MailMerge.ExecuteWithRegions(dtMergeFields);
                }
                else if (dtMergeFields.TableName.ToLower().StartsWith("tbl_"))
                {
                    doc.MailMerge.Execute(new string[] { dtMergeFields.TableName }, new object[] { dtMergeFields });
                }
                else if (dtMergeFields.TableName == "SectionF")
                {
                    DataTable assessorViewtable = new DataTable();
                    if (reportData.NonTabularData.Tables.Count > 17)
                    {
                        assessorViewtable = reportData.NonTabularData.Tables[17];
                    }
                    ProcessAndBindApplicationDocuumentSectionF(ref doc, dtMergeFields, reportData.TabularData, reportData.NonTabularData.Tables[15], reportData.NonTabularData.Tables[16], assessorViewtable);
                }
                else
                {
                    if (dtMergeFields.TableName != "AssessorView" && dtMergeFields.TableName != "AditionalInfo" && dtMergeFields.TableName != "Validation" && !dtMergeFields.TableName.StartsWith("chart_"))
                    {
                        doc.MailMerge.Execute(dtMergeFields);
                    }
                }
            }

            RemoveBlankPages(doc);
        }

        public static void GenerateQuestionnaire(ReportData reportData, string templatePath, string reportPath)
        {
            //Applying a license
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Fetchind Document from the template path
            Document doc = new Document(templatePath);

            if (reportData != null)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[0];
                if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                {
                    //Binding data in PDF
                    ProcessAndBindQuestionnaireData(ref doc, reportData);
                }
            }

            doc.Save(reportPath, Aspose.Words.SaveFormat.Pdf);

            GC.Collect();
        }

        private static void ProcessAndBindApplicationDocuumentSectionF(ref Document doc, DataTable sectionFData, DataSet tabularData, DataTable additionalInfo, DataTable validation = null, DataTable assessorView = null)
        {
            var completeQuestions = sectionFData.AsEnumerable();
            var totalAspects = completeQuestions.Select(dr => dr.Field<string>("AspectName")).Distinct();
            var additionalInformation = additionalInfo.AsEnumerable();
            if (assessorView == null) { assessorView = new DataTable(); }
            var assessorComment = assessorView.AsEnumerable();
            int Index = 1;

            foreach (var Aspect in totalAspects)
            {
                var aspectwiseQuestionare = completeQuestions.Where(dr => dr.Field<string>("AspectName") == Aspect);
                var aspectAdditioanlInfo = additionalInformation.Where(dr => dr.Field<string>("AspectName") == Aspect).Select(dr => dr.Field<string>("Comment")).FirstOrDefault() ?? "Not Available";
                var assessorComments = assessorComment.Where(dr => dr.Field<string>("AspectName") == Aspect).Select(dr => dr.Field<string>("Comment")).FirstOrDefault() ?? "Not Available";


                //Binding Question Response
                DataSet aspectwiseDataSet = new DataSet();
                aspectwiseDataSet.DataSetName = "SECTIONF" + Index + "_QUESTIONRESPONSE";
                BindDataSet(aspectwiseQuestionare, ref aspectwiseDataSet, tabularData, validation);
                if (aspectwiseDataSet.Tables.Count > 0)
                {
                    doc.MailMerge.Execute(new string[] { aspectwiseDataSet.DataSetName }, new object[] { aspectwiseDataSet });
                }
                else
                {
                    doc.MailMerge.Execute(new string[] { aspectwiseDataSet.DataSetName }, new object[] { "Not Applicable" });
                }

                //Binding Additional Information
                var additionInfoMergeFieldName = "ADDITIONALINFO_F" + Index;
                doc.MailMerge.Execute(new string[] { additionInfoMergeFieldName }, new object[] { aspectAdditioanlInfo });

                //Binding Assessor Comment
                var assessorCommentMergeFieldName = "ASSESSORVIEW_F" + Index;
                doc.MailMerge.Execute(new string[] { assessorCommentMergeFieldName }, new object[] { assessorComments });
                Index++;
            }
        }

        public static void ProcessAndBindQuestionnaireData(ref Document doc, ReportData reportData)
        {
            // Add a handler for the MergeField event.
            handleMergeFieldObj = new HandleMergeField();
            doc.MailMerge.FieldMergingCallback = handleMergeFieldObj;

            //Supply RichTextField name to HandleMerge class object
            handleMergeFieldObj.RichTextFieldKeyValue = new ArrayList() { "RichText_ExecutiveSummary" };

            for (var index = 0; index < reportData.NonTabularData.Tables.Count; index++)
            {
                DataTable dtMergeFields = reportData.NonTabularData.Tables[index];
                if (dtMergeFields.TableName.StartsWith("Table_"))
                {
                    doc.MailMerge.ExecuteWithRegions(dtMergeFields);
                }
                else if (dtMergeFields.TableName.ToLower().StartsWith("tbl_"))
                {
                    doc.MailMerge.Execute(new string[] { dtMergeFields.TableName }, new object[] { dtMergeFields });
                }
                else if (dtMergeFields.TableName == "SectionF")
                {
                    ProcessAndBindQuestionnaireSectionF(ref doc, dtMergeFields, reportData.TabularData, reportData.NonTabularData.Tables[15]);
                }
                else
                {
                    if (dtMergeFields.TableName != "AssessorView" && dtMergeFields.TableName != "AditionalInfo" && dtMergeFields.TableName != "Validation" && !dtMergeFields.TableName.StartsWith("chart_"))
                    {
                        doc.MailMerge.Execute(dtMergeFields);
                    }
                }
            }

            RemoveBlankPages(doc);
        }

        private static void ProcessAndBindQuestionnaireSectionF(ref Document doc, DataTable sectionFData, DataSet tabularData, DataTable additionalInfo)
        {
            var completeQuestions = sectionFData.AsEnumerable();
            var totalAspects = completeQuestions.Select(dr => dr.Field<string>("AspectName")).Distinct();
            var additionalInformation = additionalInfo.AsEnumerable();
           
            int Index = 1;

            foreach (var Aspect in totalAspects)
            {
                var aspectwiseQuestionare = completeQuestions.Where(dr => dr.Field<string>("AspectName") == Aspect);
                var aspectAdditioanlInfo = additionalInformation.Where(dr => dr.Field<string>("AspectName") == Aspect).Select(dr => dr.Field<string>("Comment")).FirstOrDefault() ?? "Not Available";

                //Binding Question Response
                DataSet aspectwiseDataSet = new DataSet();
                aspectwiseDataSet.DataSetName = "SECTIONF" + Index + "_QUESTIONRESPONSE";
                BindDataSet(aspectwiseQuestionare, ref aspectwiseDataSet, tabularData);
                if (aspectwiseDataSet.Tables.Count > 0)
                {
                    doc.MailMerge.Execute(new string[] { aspectwiseDataSet.DataSetName }, new object[] { aspectwiseDataSet });
                }
                else
                {
                    doc.MailMerge.Execute(new string[] { aspectwiseDataSet.DataSetName }, new object[] { "Not Applicable" });
                }

                //Binding Additional Information
                var additionInfoMergeFieldName = "ADDITIONALINFO_F" + Index;
                doc.MailMerge.Execute(new string[] { additionInfoMergeFieldName }, new object[] { aspectAdditioanlInfo });

                Index++;
            }
        }

        private static void RemoveEmptyParagraphsExceptChart(Document doc)
        {
            try
            {
                DataTable dtMergeFields = new DataTable();

                foreach (string fieldName in doc.MailMerge.GetFieldNames())
                {
                    if (fieldName.ToLower().Contains("company_classification"))
                        dtMergeFields.Columns.Add(fieldName);
                }

                DataRow drIndepententFields = dtMergeFields.NewRow();
                dtMergeFields.Rows.Add(drIndepententFields);

                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                doc.MailMerge.Execute(dtMergeFields);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
