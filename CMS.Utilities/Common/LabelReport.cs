using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Words;
using System.Collections;
using Aspose.Words.MailMerging;
using UtilityLayer.Enum;
using DocumentFormat.OpenXml.Drawing;
using System.Configuration;
using System.IO;


namespace UtilityLayer.Common
{
    public static class LabelReport
    {
        #region private variables

        private static HandleMergeField handleMergeFieldObj;

        #endregion
        public static void GenerateReport(DataSet reportData, string templatePath, string reportPath, string filepath)
        {
            //Applying a license
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Fetchind Document from the template path
            Document doc = new Document(templatePath);

            if (reportData != null)
            {
                DataTable dtMergeFields = reportData.Tables[0];
                if (dtMergeFields != null && dtMergeFields.Rows.Count > 0)
                {
                    //Binding data in PDF
                    ProcessAndBindData(ref doc, reportData);
                }
            }

            doc.Save(reportPath, SaveFormat.Docx);

            CreateChart(ref doc, reportPath, reportData, filepath);


        }
        public static void CreateChart(ref Document doc, string reportPath, DataSet reportData, string filepath)
        {
            string chartPartRId = "Chart_EnvironmentGraph";
            string excelSourceId = "rId200";
            DataTable table = reportData.Tables[1];
            ChartArea objCompanyPerformanceChartArea = BindChartData(table, "Column");
            (new ReportChart()).GenerateWordChart(objCompanyPerformanceChartArea, reportPath, chartPartRId, excelSourceId, filepath, true);

            string chartPartRId1 = "Chart_SocialGraph";
            string excelSourceId1 = "rId201";
            DataTable table1 = reportData.Tables[2];
            ChartArea objCompanyPerformanceChartArea1 = BindChartData(table1, "Column");
            (new ReportChart()).GenerateWordChart(objCompanyPerformanceChartArea1, reportPath, chartPartRId1, excelSourceId1, filepath, true);

            string chartPartRId2 = "Chart_GovernanceGraph";
            string excelSourceId2 = "rId202";
            DataTable table2 = reportData.Tables[3];
            ChartArea objCompanyPerformanceChartArea2 = BindChartData(table2, "Column");
            (new ReportChart()).GenerateWordChart(objCompanyPerformanceChartArea2, reportPath, chartPartRId2, excelSourceId2, filepath, true);
        }
        public static ChartArea BindChartData(DataTable table, string type)
        {
            ChartArea chartArea = new ChartArea();
            chartArea.ChartType = type;
            chartArea.ChartSubAreaList = new List<ChartSeries>();

            for (var j = 0; j < table.Columns.Count - 1; j++)
            {
                ChartSeries series = new ChartSeries();
                series.SeriesTitle = table.Columns[j + 1].ToString();
                series.SeriesType = type;
                series.ChartDataList = new List<ChartData>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    ChartData data = new ChartData();
                    data.XaxisLabel = table.Rows[i][0].ToString();
                    data.YaxisValue = table.Rows[i][j + 1].ToString();
                    data.DataType = "number";
                    series.ChartDataList.Add(data);
                }
                chartArea.ChartSubAreaList.Add(series);
            }
            return chartArea;
        }
        public static void ProcessAndBindData(ref Document doc, DataSet reportData)
        {
            // Add a handler for the MergeField event.
            handleMergeFieldObj = new HandleMergeField();
            doc.MailMerge.FieldMergingCallback = handleMergeFieldObj;

            //Supply RichTextField name to HandleMerge class object
            //handleMergeFieldObj.RichTextFieldKeyValue = new ArrayList() { "RichText_ExecutiveSummary" };

            for (var index = 0; index < reportData.Tables.Count; index++)
            {
                DataTable dtMergeFields = reportData.Tables[index];
                if (dtMergeFields.TableName.StartsWith("Table_"))
                {
                    doc.MailMerge.ExecuteWithRegions(dtMergeFields);
                }
                else if (dtMergeFields.TableName.ToLower().StartsWith("tbl_"))
                {
                    doc.MailMerge.Execute(new string[] { dtMergeFields.TableName }, new object[] { dtMergeFields });
                }
                else
                {
                    if (!dtMergeFields.TableName.StartsWith("Chart_"))
                    {
                        doc.MailMerge.Execute(dtMergeFields);
                    }
                }
            }
            ProcessImage(doc, reportData);
            RemoveBlankPages(doc);
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
        private static void ProcessImage(Document doc, DataSet reportData)
        {

            try
            {
                string RatingImageName = string.Empty;
                string EnvironmentRiskImageName = string.Empty;
                string SocialRiskImageName = string.Empty;
                string GovernanceRiskImageName = string.Empty;
                string OverallRiskImageName = string.Empty;
                double TotalScore = Convert.ToDouble(reportData.Tables[0].Rows[0]["TotalScore"]);
                double EnvTotalScore = Convert.ToDouble(reportData.Tables[0].Rows[0]["EnvironmentScore"]);
                double SocTotalScore = Convert.ToDouble(reportData.Tables[0].Rows[0]["SocialScore"]);
                double EcoTotalScore = Convert.ToDouble(reportData.Tables[0].Rows[0]["GovernanceScore"]);
                double OverAllTotalScore = Convert.ToDouble(reportData.Tables[0].Rows[0]["OverallScore"]);

                if (TotalScore >= 70.00)
                {
                    RatingImageName = "platinum.png";
                }
                else if (TotalScore >= 49.00 && TotalScore < 70.00)
                {
                    RatingImageName = "gold.png";
                }
                else if (TotalScore < 49.00)
                {
                    RatingImageName = "bronze.png";
                }


                if (EnvTotalScore >= 70.00)
                {
                    EnvironmentRiskImageName = "green.png";
                }
                else if (EnvTotalScore >= 49.00 && EnvTotalScore < 70.00)
                {
                    EnvironmentRiskImageName = "yellow.png";
                }
                else if (EnvTotalScore < 49.00)
                {
                    EnvironmentRiskImageName = "red.png";
                }

                if (SocTotalScore >= 70.00)
                {
                    SocialRiskImageName = "green.png";
                }
                else if (SocTotalScore >= 49.00 && SocTotalScore < 70.00)
                {
                    SocialRiskImageName = "yellow.png";
                }
                else if (SocTotalScore < 49.00)
                {
                    SocialRiskImageName = "red.png";
                }

                if (EcoTotalScore >= 70.00)
                {
                    GovernanceRiskImageName = "green.png";
                }
                else if (EcoTotalScore >= 49.00 && EcoTotalScore < 70.00)
                {
                    GovernanceRiskImageName = "yellow.png";
                }
                else if (EcoTotalScore < 49.00)
                {
                    GovernanceRiskImageName = "red.png";
                }

                if (OverAllTotalScore >= 70.00)
                {
                    OverallRiskImageName = "green.png";
                }
                else if (OverAllTotalScore >= 49.00 && OverAllTotalScore < 70.00)
                {
                    OverallRiskImageName = "yellow.png";
                }
                else if (OverAllTotalScore < 49.00)
                {
                    OverallRiskImageName = "red.png";
                }

                DataTable table = new DataTable();
                table.Columns.Add("RatingImage", typeof(byte[]));
                table.Columns.Add("OverallRiskImage", typeof(byte[]));
                table.Columns.Add("GovernanceRiskImage", typeof(byte[]));
                table.Columns.Add("SocialRiskImage", typeof(byte[]));
                table.Columns.Add("EnvironmentRiskImage", typeof(byte[]));
                string dataDir = ConfigurationManager.AppSettings["FileOutputPath"].ToString();
                dataDir = System.Web.HttpContext.Current.Server.MapPath(dataDir);
                DataRow row = table.NewRow();
                row["RatingImage"] = File.ReadAllBytes(dataDir + RatingImageName);
                row["OverallRiskImage"] = File.ReadAllBytes(dataDir + OverallRiskImageName);
                row["GovernanceRiskImage"] = File.ReadAllBytes(dataDir + GovernanceRiskImageName);
                row["SocialRiskImage"] = File.ReadAllBytes(dataDir + SocialRiskImageName);
                row["EnvironmentRiskImage"] = File.ReadAllBytes(dataDir + EnvironmentRiskImageName);
                table.Rows.Add(row);
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                doc.MailMerge.Execute(table);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
