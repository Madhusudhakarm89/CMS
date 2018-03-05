using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;
using dc = DocumentFormat.OpenXml.Drawing.Charts;
using d = DocumentFormat.OpenXml.Drawing;
using C14 = DocumentFormat.OpenXml.Office2010.Drawing.Charts;
using dw = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using wp = DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Collections;

namespace UtilityLayer.Common
{
    # region Entity Classes

    public class ChartArea
    {
        public string YaxisLabel1 { get; set; }

        public string YaxisLabel2 { get; set; }

        public List<ChartSeries> ChartSubAreaList { get; set; }

        public string ChartType { get; set; }
    }

    public class ChartSeries
    {
        public List<ChartData> ChartDataList { get; set; }

        public string SeriesTitle { get; set; }

        public SchemeColorValues SeriesColor { get; set; }

        public string SeriesType { get; set; }
    }

    public class ChartData
    {
        public string XaxisLabel { get; set; }

        public string YaxisValue { get; set; }

        public string DataType { get; set; }

        public string XaxisLabelValueToSort { get; set; }
    }

    #endregion

    public class ReportChart
    {

        // Specify whether the instance is disposed.
        private bool disposed = false;

        // The Word package
        private WordprocessingDocument document = null;

        private static readonly Regex instructionRegEx =
          new Regex(
                      @"^[\s]*MERGEFIELD[\s]+(?<name>[#\w]*){1}               # This retrieves the field's name (Named Capture Group -> name)
                            [\s]*(\\\*[\s]+(?<Format>[\w]*){1})?                # Retrieves field's format flag (Named Capture Group -> Format)
                            [\s]*(\\b[\s]+[""]?(?<PreText>[^\\]*){1})?         # Retrieves text to display before field data (Named Capture Group -> PreText)
                                                                                # Retrieves text to display after field data (Named Capture Group -> PostText)
                            [\s]*(\\f[\s]+[""]?(?<PostText>[^\\]*){1})?",
                      RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

        //public void OpenAndReplaceWithChart(List<ChartData> chartList, string sourceFile)

        public void GenerateWordChart(ChartArea objChartArea, string sourceFile, string chartPartRId, string excelSourceId, string filepath,bool IsLabel =false)
        {
            WordprocessingDocument objWordDocx = null;

            try
            {
                if (objChartArea != null && !string.IsNullOrEmpty(sourceFile))
                {
                    string barChartAxisLabel = string.Empty;
                    string radarChartAxisLabel = string.Empty;
                    string lineChartAxisLabel = string.Empty;
                    string barChartFormatCode = string.Empty;
                    string radarChartFormatCode = string.Empty;
                    string lineChartFormatCode = string.Empty;

                    // xAxisValue
                    UInt32 axisValue1 = 172466944U;

                    UInt32 axisValue2 = 172468480U;

                    //yAxisValue
                    UInt32 axisValue3 = 172653184U;

                    UInt32 axisValue4 = 172651264U;
                   
                    List<ChartSeries> lstChartSubArea = objChartArea.ChartSubAreaList;
                    List<ChartData> chartList = null;

                    //declare and open a Word document object
                    objWordDocx = WordprocessingDocument.Open(sourceFile, true);

                    // Get MainDocumentPart of Document
                    MainDocumentPart mainPart = objWordDocx.MainDocumentPart;

                    // Create ChartPart object in Word Document
                    ChartPart chartPart = mainPart.AddNewPart<ChartPart>(chartPartRId);

                    dc.ChartSpace chartSpace1 = new dc.ChartSpace();
                    chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
                    chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                    dc.Date1904 date19041 = new dc.Date1904() { Val = false };
                    dc.EditingLanguage editingLanguage1 = new dc.EditingLanguage() { Val = "en-US" };
                    dc.RoundedCorners roundedCorners1 = new dc.RoundedCorners() { Val = false };

                    AlternateContent alternateContent1 = new AlternateContent();
                    alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

                    AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice() { Requires = "c14" };
                    alternateContentChoice1.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
                    C14.Style style1 = new C14.Style() { Val = 102 };

                    alternateContentChoice1.Append(style1);

                    AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
                    dc.Style style2 = new dc.Style() { Val = 2 };

                    alternateContentFallback1.Append(style2);

                    alternateContent1.Append(alternateContentChoice1);
                    alternateContent1.Append(alternateContentFallback1);

                    dc.Chart chart1 = new dc.Chart();
                    dc.AutoTitleDeleted autoTitleDeleted1 = new dc.AutoTitleDeleted() { Val = true };

                    #region plotArea layout

                    dc.PlotArea plotArea1 = new dc.PlotArea();

                    dc.Layout layout1 = new dc.Layout();

                    dc.ManualLayout manualLayout1 = new dc.ManualLayout();
                    dc.LayoutTarget layoutTarget1 = new dc.LayoutTarget() { Val = dc.LayoutTargetValues.Inner };
                    dc.LeftMode leftMode1 = new dc.LeftMode() { Val = dc.LayoutModeValues.Edge };
                    dc.TopMode topMode1 = new dc.TopMode() { Val = dc.LayoutModeValues.Edge };

                    //dc.Left left1 = new dc.Left() { Val = 9.2878008033707174E-2D };
                    //dc.Top top1 = new dc.Top() { Val = 7.5665266908490111E-2D };
                    //dc.Width width1 = new dc.Width() { Val = 0.99451812088231565D };
                    //dc.Height height1 = new dc.Height() { Val = 0.73546639435052352D };

                    //dc.Left left1 = new dc.Left() { Val = 0.15949125443289056D };
                    //dc.Top top1 = new dc.Top() { Val = 7.5665266908490111E-2D };
                    //dc.Width width1 = new dc.Width() { Val = 0.68052317124481576D };
                    //dc.Height height1 = new dc.Height() { Val = 0.73546639435052352D };

                    dc.Left left1 = new dc.Left() { Val = 0.30841840890578336D };
                    dc.Top top1 = new dc.Top() { Val = 1.5665266908490111E-2D };
                    dc.Width width1 = new dc.Width() { Val = 0.50075300932211062D };
                    dc.Height height1 = new dc.Height() { Val = 1.0 };
                    if (objChartArea.ChartType.Contains("Radar"))
                    {
                        left1.Val = 0.29841840890578336D;
                        width1.Val = 0.50075300932211062D;
                        height1.Val = 0.50546639435052352D;
                    }

                    if (IsLabel)
                    {
                        width1.Val = 1;// 0.2;
                        height1.Val = 1;//0.33546639435052352D;
                        left1.Val = 0.0;
                    }

                    manualLayout1.Append(layoutTarget1);
                    manualLayout1.Append(leftMode1);
                    manualLayout1.Append(topMode1);
                    manualLayout1.Append(left1);
                    manualLayout1.Append(top1);
                    manualLayout1.Append(width1);
                    manualLayout1.Append(height1);

                    layout1.Append(manualLayout1);

                    #endregion

                    dc.BarChart barChart1 = null;
                    dc.RadarChart radarChart1 = null;
                    dc.RadarStyle radarStyle = null;
                    dc.BarDirection barDirection1 = null;
                    dc.BarGrouping barGrouping1 = null;

                    dc.LineChart lineChart1 = null;

                    if (objChartArea.ChartType.Contains("Bar") || objChartArea.ChartType.Contains("Column"))
                    {
                        barChart1 = new dc.BarChart();
                    }

                    if (objChartArea.ChartType.Contains("Radar"))
                    {
                        radarChart1 = new dc.RadarChart();
                    }

                    if (objChartArea.ChartType.Contains("Line"))
                    {
                        lineChart1 = new dc.LineChart();
                    }

                    string[] chartTypes = objChartArea.ChartType.Split('+');

                    foreach (string chartType in chartTypes)
                    {
                        switch (chartType)
                        {
                            case "Bar":
                                barDirection1 = new dc.BarDirection() { Val = dc.BarDirectionValues.Bar };
                                barGrouping1 = new dc.BarGrouping() { Val = dc.BarGroupingValues.Clustered };
                                break;
                            case "Radar":
                                radarStyle = new dc.RadarStyle() { Val = dc.RadarStyleValues.Marker };
                                break;
                            case "StackedBar":
                                barDirection1 = new dc.BarDirection() { Val = dc.BarDirectionValues.Bar };
                                barGrouping1 = new dc.BarGrouping() { Val = dc.BarGroupingValues.Stacked };
                                break;
                            case "Column":
                                barDirection1 = new dc.BarDirection() { Val = dc.BarDirectionValues.Column };
                                barGrouping1 = new dc.BarGrouping() { Val = dc.BarGroupingValues.Clustered };
                                break;
                            case "StackedColumn":
                                barDirection1 = new dc.BarDirection() { Val = dc.BarDirectionValues.Column };
                                barGrouping1 = new dc.BarGrouping() { Val = dc.BarGroupingValues.Stacked };
                                break;
                            default:
                                break;
                        }

                        if (chartType.Contains("Bar") || chartType.Contains("Column"))
                        {
                            dc.VaryColors varyColors1 = new dc.VaryColors() { Val = false };
                            if (IsLabel)
                            {
                                varyColors1.Val = true; 
                            }
                            barChart1.Append(barDirection1);
                            barChart1.Append(barGrouping1);
                            barChart1.Append(varyColors1);
                        }
                        else if (chartType.Contains("Radar"))
                        {
                            dc.VaryColors varyColors1 = new dc.VaryColors() { Val = true };
                            radarChart1.Append(radarStyle);
                            radarChart1.Append(varyColors1);
                        }
                        else if (chartType.Contains("Line"))
                        {
                            dc.Grouping grouping1 = new dc.Grouping() { Val = dc.GroupingValues.Standard };
                            dc.VaryColors varyColors2 = new dc.VaryColors() { Val = false };

                            lineChart1.Append(grouping1);
                            lineChart1.Append(varyColors2);
                        }
                    }

                    #region ChartSeries

                    uint c = 0;
                    uint xAxisLabelCount = default(uint);

                    foreach (ChartSeries csa in lstChartSubArea)
                    {
                        chartList = csa.ChartDataList;

                        if (xAxisLabelCount < chartList.Count)
                        {
                            xAxisLabelCount = (uint)chartList.Count;
                        }

                        if (csa.SeriesType.Contains("Bar") || csa.SeriesType.Contains("Column"))
                        {
                            #region barChartSeries

                            dc.BarChartSeries barChartSeries = new dc.BarChartSeries();
                            dc.Index index = new dc.Index() { Val = new UInt32Value(c) };
                            dc.Order order = new dc.Order() { Val = new UInt32Value(c) };
                            c++;

                            dc.SeriesText seriesText = new dc.SeriesText();
                            dc.StringReference stringReference = new dc.StringReference();

                            dc.StringCache stringCache = new dc.StringCache();
                            dc.PointCount pointCount = new dc.PointCount() { Val = (UInt32Value)1U };

                            dc.StringPoint stringPoint = new dc.StringPoint() { Index = (UInt32Value)0U };
                            dc.NumericValue numericValue = new dc.NumericValue();
                            //numericValue.Text = "Services";
                            numericValue.Text = csa.SeriesTitle;

                            stringPoint.Append(numericValue);

                            stringCache.Append(pointCount);
                            stringCache.Append(stringPoint);

                            stringReference.Append(stringCache);

                            seriesText.Append(stringReference);

                            dc.ChartShapeProperties chartShapeProperties = new dc.ChartShapeProperties();

                            d.SolidFill solidFill = new d.SolidFill();
                           
                               // d.SchemeColor schemeColor = new d.SchemeColor() { Val = csa.SeriesColor };

                               // solidFill.Append(schemeColor);
                           

                            d.Outline outline = new d.Outline() { Width = 25400 };
                            d.NoFill noFill = new d.NoFill();

                            outline.Append(noFill);

                            chartShapeProperties.Append(solidFill);
                            chartShapeProperties.Append(outline);
                            
                            dc.InvertIfNegative invertIfNegative = new dc.InvertIfNegative() { Val = false };

                            dc.DataLabels dataLabels = new dc.DataLabels();
                            dc.ShowLegendKey showLegendKey = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue = new dc.ShowValue() { Val = false };
                            if (IsLabel)
                            {
                                showValue.Val = true;
                            }
                            dc.ShowCategoryName showCategoryName = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize = new dc.ShowBubbleSize() { Val = false };
                            dc.ShowLeaderLines showLeaderLines = new dc.ShowLeaderLines() { Val = false };

                            dataLabels.Append(showLegendKey);
                            dataLabels.Append(showValue);
                            dataLabels.Append(showCategoryName);
                            dataLabels.Append(showSeriesName);
                            dataLabels.Append(showPercent);
                            dataLabels.Append(showBubbleSize);
                            dataLabels.Append(showLeaderLines);

                            dc.CategoryAxisData categoryAxisData = new dc.CategoryAxisData();
                            dc.StringReference strReference = new dc.StringReference();
                            dc.StringCache stringCacheXaxis = new dc.StringCache();
                            dc.PointCount pointCountXaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            uint i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.StringPoint numericPointXaxis = new dc.StringPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueXaxis = new dc.NumericValue();
                                numericValueXaxis.Text = item.XaxisLabel;

                                numericPointXaxis.Append(numericValueXaxis);
                                stringCacheXaxis.Append(numericPointXaxis);

                                i++;
                            }

                            stringCacheXaxis.Append(pointCountXaxis);
                            strReference.Append(stringCacheXaxis);

                            categoryAxisData.Append(strReference);

                            dc.Values values = new dc.Values();

                            dc.NumberReference numberReference = new dc.NumberReference();
                            dc.NumberingCache numberingCacheServices = new dc.NumberingCache();

                            i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.NumericPoint numericPointYaxis = new dc.NumericPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueYaxis = new dc.NumericValue();

                                barChartFormatCode = item.DataType;

                                if (barChartFormatCode.Contains("percent"))
                                {
                                    float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                    numericValueYaxis.Text = (yAxisValue / 100).ToString();
                                }
                                else
                                {
                                    numericValueYaxis.Text = item.YaxisValue;
                                }

                                numericPointYaxis.Append(numericValueYaxis);
                                numberingCacheServices.Append(numericPointYaxis);

                                i++;
                            }

                            dc.FormatCode formatCode = new dc.FormatCode();

                            if (barChartFormatCode.Contains("number"))
                            {
                                formatCode.Text = "#,#0.0"; //"#,##0.0";
                            }
                            else if (barChartFormatCode.Contains("percent"))
                            {
                                formatCode.Text = "0.00%";
                            }

                            dc.PointCount pointCountYaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            numberingCacheServices.Append(formatCode);
                            numberingCacheServices.Append(pointCountYaxis);

                            numberReference.Append(numberingCacheServices);

                            values.Append(numberReference);

                            barChartSeries.Append(index);
                            barChartSeries.Append(order);
                            barChartSeries.Append(seriesText);
                            barChartSeries.Append(chartShapeProperties);
                            barChartSeries.Append(invertIfNegative);
                            barChartSeries.Append(dataLabels);
                            barChartSeries.Append(categoryAxisData);
                            barChartSeries.Append(values);

                            barChart1.Append(barChartSeries);

                            #endregion
                        }
                        else if (csa.SeriesType.Contains("Radar"))
                        {
                            #region radarChartSeries

                            dc.RadarChartSeries radarChartSeries = new dc.RadarChartSeries();
                            dc.Index index = new dc.Index() { Val = new UInt32Value(c) };
                            dc.Order order = new dc.Order() { Val = new UInt32Value(c) };
                            c++;

                            dc.SeriesText seriesText = new dc.SeriesText();
                            dc.StringReference stringReference = new dc.StringReference();

                            dc.StringCache stringCache = new dc.StringCache();
                            dc.PointCount pointCount = new dc.PointCount() { Val = (UInt32Value)1U };

                            dc.StringPoint stringPoint = new dc.StringPoint() { Index = (UInt32Value)0U };
                            dc.NumericValue numericValue = new dc.NumericValue();
                            //numericValue.Text = "Services";
                            numericValue.Text = csa.SeriesTitle;

                            stringPoint.Append(numericValue);

                            stringCache.Append(pointCount);
                            stringCache.Append(stringPoint);

                            stringReference.Append(stringCache);

                            seriesText.Append(stringReference);

                            dc.ChartShapeProperties chartShapeProperties = new dc.ChartShapeProperties();

                            d.SolidFill solidFill = new d.SolidFill();
                            
                                //d.SchemeColor schemeColor = new d.SchemeColor() { Val = csa.SeriesColor };

                                //solidFill.Append(schemeColor);
                                //chartShapeProperties.Append(solidFill);
                            
                            d.Outline outline = new d.Outline() { Width = 25400 };
                            d.NoFill noFill = new d.NoFill();

                            //outline.Append(noFill);
                            
                          
                            //chartShapeProperties.Append(outline);
                            dc.InvertIfNegative invertIfNegative = new dc.InvertIfNegative() { Val = false };

                            dc.DataLabels dataLabels = new dc.DataLabels();
                            dc.ShowLegendKey showLegendKey = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue = new dc.ShowValue() { Val = false };
                            dc.ShowCategoryName showCategoryName = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize = new dc.ShowBubbleSize() { Val = false };
                            dc.ShowLeaderLines showLeaderLines = new dc.ShowLeaderLines() { Val = false };

                            dataLabels.Append(showLegendKey);
                            dataLabels.Append(showValue);
                            dataLabels.Append(showCategoryName);
                            dataLabels.Append(showSeriesName);
                            dataLabels.Append(showPercent);
                            dataLabels.Append(showBubbleSize);
                            dataLabels.Append(showLeaderLines);

                            dc.CategoryAxisData categoryAxisData = new dc.CategoryAxisData();
                            dc.StringReference strReference = new dc.StringReference();
                            dc.StringCache stringCacheXaxis = new dc.StringCache();
                            dc.PointCount pointCountXaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            uint i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.StringPoint numericPointXaxis = new dc.StringPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueXaxis = new dc.NumericValue();
                                numericValueXaxis.Text = item.XaxisLabel;

                                numericPointXaxis.Append(numericValueXaxis);
                                stringCacheXaxis.Append(numericPointXaxis);

                                i++;
                            }

                            stringCacheXaxis.Append(pointCountXaxis);
                            strReference.Append(stringCacheXaxis);

                            categoryAxisData.Append(strReference);

                            dc.Values values = new dc.Values();

                            dc.NumberReference numberReference = new dc.NumberReference();
                            dc.NumberingCache numberingCacheServices = new dc.NumberingCache();

                            i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.NumericPoint numericPointYaxis = new dc.NumericPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueYaxis = new dc.NumericValue();

                                barChartFormatCode = item.DataType;

                                if (barChartFormatCode.Contains("percent"))
                                {
                                    float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                    numericValueYaxis.Text = (yAxisValue / 100).ToString();
                                }
                                else
                                {
                                    numericValueYaxis.Text = item.YaxisValue;
                                }

                                numericPointYaxis.Append(numericValueYaxis);
                                numberingCacheServices.Append(numericPointYaxis);

                                i++;
                            }

                            dc.FormatCode formatCode = new dc.FormatCode();

                            if (barChartFormatCode.Contains("number"))
                            {
                                formatCode.Text = "#,#0.0"; //"#,##0.0";
                            }
                            else if (barChartFormatCode.Contains("percent"))
                            {
                                formatCode.Text = "0.00%";
                            }

                            dc.PointCount pointCountYaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            numberingCacheServices.Append(formatCode);
                            numberingCacheServices.Append(pointCountYaxis);

                            numberReference.Append(numberingCacheServices);

                            values.Append(numberReference);

                            radarChartSeries.Append(index);
                            radarChartSeries.Append(order);
                            radarChartSeries.Append(seriesText);
                            radarChartSeries.Append(chartShapeProperties);
                            radarChartSeries.Append(invertIfNegative);
                            radarChartSeries.Append(dataLabels);
                            radarChartSeries.Append(categoryAxisData);
                            radarChartSeries.Append(values);

                            radarChart1.Append(radarChartSeries);

                            #endregion
                        }
                        else if (csa.SeriesType == "Line")
                        {
                            #region LineChartSeries

                            dc.LineChartSeries lineChartSeries1 = new dc.LineChartSeries();
                            dc.Index index14 = new dc.Index() { Val = new UInt32Value(c) };
                            dc.Order order5 = new dc.Order() { Val = new UInt32Value(c) };
                            c++;

                            //dc.Index index14 = new dc.Index() { Val = (UInt32Value)1U };
                            //dc.Order order5 = new dc.Order() { Val = (UInt32Value)0U };

                            dc.SeriesText seriesText = new dc.SeriesText();
                            dc.StringReference stringReference = new dc.StringReference();

                            dc.StringCache stringCache = new dc.StringCache();
                            dc.PointCount pointCount = new dc.PointCount() { Val = (UInt32Value)1U };

                            dc.StringPoint stringPoint = new dc.StringPoint() { Index = (UInt32Value)0U };
                            dc.NumericValue numericValue = new dc.NumericValue();
                            numericValue.Text = csa.SeriesTitle;

                            stringPoint.Append(numericValue);

                            stringCache.Append(pointCount);
                            stringCache.Append(stringPoint);
                            stringReference.Append(stringCache);
                            seriesText.Append(stringReference);


                            //dc.NumericValue numericValue1 = new dc.NumericValue();
                            //numericValue1.Text = "EBIT";

                            //seriesText.Append(numericValue1);


                            dc.ChartShapeProperties chartShapeProperties5 = new dc.ChartShapeProperties();

                            d.Outline outline5 = new d.Outline();

                            d.SolidFill solidFill5 = new d.SolidFill();
                            //d.RgbColorModelHex rgbColorModelHex1 = new d.RgbColorModelHex() { Val = "7030A0" };

                            //solidFill5.Append(rgbColorModelHex1);

                            outline5.Append(solidFill5);

                            chartShapeProperties5.Append(outline5);

                            dc.Marker marker1 = new dc.Marker();

                            dc.ChartShapeProperties chartShapeProperties6 = new dc.ChartShapeProperties();

                            d.SolidFill solidFill6 = new d.SolidFill();
                            //d.SchemeColor schemeColor5 = new d.SchemeColor() { Val = csa.SeriesColor };

                            //solidFill6.Append(schemeColor5);

                            d.Outline outline6 = new d.Outline();

                            d.SolidFill solidFill7 = new d.SolidFill();
                            //d.RgbColorModelHex rgbColorModelHex2 = new d.RgbColorModelHex() { Val = "7030A0" };

                            //solidFill7.Append(rgbColorModelHex2);

                            outline6.Append(solidFill7);

                            chartShapeProperties6.Append(solidFill6);
                            chartShapeProperties6.Append(outline6);

                            marker1.Append(chartShapeProperties6);

                            dc.DataLabels dataLabels6 = new dc.DataLabels();

                            dc.DataLabel dataLabel10 = new dc.DataLabel();
                            dc.Index index15 = new dc.Index() { Val = (UInt32Value)0U };

                            dc.Layout layout11 = new dc.Layout();

                            dc.ManualLayout manualLayout11 = new dc.ManualLayout();
                            dc.Left left11 = new dc.Left() { Val = -4.4742729306487695E-3D };
                            dc.Top top11 = new dc.Top() { Val = 3.8338658146964855E-2D };

                            manualLayout11.Append(left11);
                            manualLayout11.Append(top11);

                            layout11.Append(manualLayout11);
                            dc.ShowLegendKey showLegendKey15 = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue15 = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName15 = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName15 = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent15 = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize15 = new dc.ShowBubbleSize() { Val = false };

                            dataLabel10.Append(index15);
                            dataLabel10.Append(layout11);
                            dataLabel10.Append(showLegendKey15);
                            dataLabel10.Append(showValue15);
                            dataLabel10.Append(showCategoryName15);
                            dataLabel10.Append(showSeriesName15);
                            dataLabel10.Append(showPercent15);
                            dataLabel10.Append(showBubbleSize15);

                            dc.DataLabel dataLabel11 = new dc.DataLabel();
                            dc.Index index16 = new dc.Index() { Val = (UInt32Value)1U };

                            dc.Layout layout12 = new dc.Layout();

                            dc.ManualLayout manualLayout12 = new dc.ManualLayout();
                            dc.Left left12 = new dc.Left() { Val = 0D };
                            dc.Top top12 = new dc.Top() { Val = 2.5559105431309903E-2D };

                            manualLayout12.Append(left12);
                            manualLayout12.Append(top12);

                            layout12.Append(manualLayout12);
                            dc.ShowLegendKey showLegendKey16 = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue16 = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName16 = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName16 = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent16 = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize16 = new dc.ShowBubbleSize() { Val = false };

                            dataLabel11.Append(index16);
                            dataLabel11.Append(layout12);
                            dataLabel11.Append(showLegendKey16);
                            dataLabel11.Append(showValue16);
                            dataLabel11.Append(showCategoryName16);
                            dataLabel11.Append(showSeriesName16);
                            dataLabel11.Append(showPercent16);
                            dataLabel11.Append(showBubbleSize16);

                            dc.DataLabel dataLabel12 = new dc.DataLabel();
                            dc.Index index17 = new dc.Index() { Val = (UInt32Value)2U };

                            dc.Layout layout13 = new dc.Layout();

                            dc.ManualLayout manualLayout13 = new dc.ManualLayout();
                            dc.Left left13 = new dc.Left() { Val = -3.3557046979865772E-2D };
                            dc.Top top13 = new dc.Top() { Val = 4.6858359957401494E-2D };

                            manualLayout13.Append(left13);
                            manualLayout13.Append(top13);

                            layout13.Append(manualLayout13);
                            dc.ShowLegendKey showLegendKey17 = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue17 = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName17 = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName17 = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent17 = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize17 = new dc.ShowBubbleSize() { Val = false };

                            dataLabel12.Append(index17);
                            dataLabel12.Append(layout13);
                            dataLabel12.Append(showLegendKey17);
                            dataLabel12.Append(showValue17);
                            dataLabel12.Append(showCategoryName17);
                            dataLabel12.Append(showSeriesName17);
                            dataLabel12.Append(showPercent17);
                            dataLabel12.Append(showBubbleSize17);
                            dc.ShowLegendKey showLegendKey18 = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue18 = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName18 = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName18 = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent18 = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize18 = new dc.ShowBubbleSize() { Val = false };
                            dc.ShowLeaderLines showLeaderLines5 = new dc.ShowLeaderLines() { Val = false };

                            dataLabels6.Append(dataLabel10);
                            dataLabels6.Append(dataLabel11);
                            dataLabels6.Append(dataLabel12);
                            dataLabels6.Append(showLegendKey18);
                            dataLabels6.Append(showValue18);
                            dataLabels6.Append(showCategoryName18);
                            dataLabels6.Append(showSeriesName18);
                            dataLabels6.Append(showPercent18);
                            dataLabels6.Append(showBubbleSize18);
                            dataLabels6.Append(showLeaderLines5);

                            dc.CategoryAxisData categoryAxisData = new dc.CategoryAxisData();
                            dc.StringReference strReference = new dc.StringReference();
                            dc.StringCache stringCacheXaxis = new dc.StringCache();
                            dc.PointCount pointCountXaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            uint i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.StringPoint numericPointXaxis = new dc.StringPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueXaxis = new dc.NumericValue();
                                numericValueXaxis.Text = item.XaxisLabel;

                                numericPointXaxis.Append(numericValueXaxis);
                                stringCacheXaxis.Append(numericPointXaxis);

                                i++;
                            }

                            stringCacheXaxis.Append(pointCountXaxis);
                            strReference.Append(stringCacheXaxis);

                            categoryAxisData.Append(strReference);

                            dc.Values values = new dc.Values();

                            dc.NumberReference numberReference = new dc.NumberReference();
                            dc.NumberingCache numberingCacheServices = new dc.NumberingCache();

                            i = 0;

                            // Fill data for chart
                            foreach (var item in chartList)
                            {
                                dc.NumericPoint numericPointYaxis = new dc.NumericPoint() { Index = new UInt32Value(i) };
                                dc.NumericValue numericValueYaxis = new dc.NumericValue();
                                lineChartFormatCode = item.DataType;

                                if (lineChartFormatCode.Contains("percent"))
                                {
                                    float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                    numericValueYaxis.Text = (yAxisValue / 100).ToString();
                                }
                                else
                                {
                                    numericValueYaxis.Text = item.YaxisValue;
                                }

                                numericPointYaxis.Append(numericValueYaxis);
                                numberingCacheServices.Append(numericPointYaxis);

                                i++;
                            }

                            dc.FormatCode formatCode = new dc.FormatCode();

                            if (lineChartFormatCode.Contains("number"))
                            {
                                formatCode.Text = "#,#0.0";// "#,##0.0";
                            }
                            else if (lineChartFormatCode.Contains("percent"))
                            {
                                formatCode.Text = "0.00%";
                            }

                            dc.PointCount pointCountYaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                            numberingCacheServices.Append(formatCode);
                            numberingCacheServices.Append(pointCountYaxis);

                            numberReference.Append(numberingCacheServices);

                            values.Append(numberReference);

                            dc.Smooth smooth1 = new dc.Smooth() { Val = false };

                            lineChartSeries1.Append(index14);
                            lineChartSeries1.Append(order5);
                            lineChartSeries1.Append(seriesText);
                            lineChartSeries1.Append(chartShapeProperties5);
                            lineChartSeries1.Append(marker1);
                            lineChartSeries1.Append(dataLabels6);
                            lineChartSeries1.Append(categoryAxisData);
                            lineChartSeries1.Append(values);
                            lineChartSeries1.Append(smooth1);

                            lineChart1.Append(lineChartSeries1);

                            #endregion
                        }
                    }

                    #endregion

                    #region Bar/Column Chart

                    if (objChartArea.ChartType.Contains("Bar") || objChartArea.ChartType.Contains("Column"))
                    {
                        dc.DataLabels dataLabels5 = new dc.DataLabels();
                        dc.ShowLegendKey showLegendKey14 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue14 = new dc.ShowValue() { Val = false };
                        dc.ShowCategoryName showCategoryName14 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName14 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent14 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize14 = new dc.ShowBubbleSize() { Val = false };

                        dataLabels5.Append(showLegendKey14);
                        dataLabels5.Append(showValue14);
                        dataLabels5.Append(showCategoryName14);
                        dataLabels5.Append(showSeriesName14);
                        dataLabels5.Append(showPercent14);
                        dataLabels5.Append(showBubbleSize14);
                        dc.GapWidth gapWidth1 = new dc.GapWidth() { Val = (UInt16Value)150U };
                        dc.Overlap overlap1 = null;

                        if (objChartArea.ChartType.Contains("Stacked"))
                        {
                            overlap1 = new dc.Overlap() { Val = 100 };
                            barChart1.Append(overlap1);
                        }

                        //dc.AxisId axisId1 = new dc.AxisId() { Val = (UInt32Value)172466944U };
                        //dc.AxisId axisId2 = new dc.AxisId() { Val = (UInt32Value)172468480U };

                        #region Category  Axis for Bar Chart

                        dc.CategoryAxis categoryAxis1 = new dc.CategoryAxis();

                        dc.AxisId axisId1 = null;
                        dc.AxisId axisId2 = null;

                        dc.AxisId axisId5 = null;

                        if (chartTypes.Length == 0)
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            barChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[0].Contains("Bar") || chartTypes[0].Contains("Column") || chartTypes[0].Contains("Radar")))
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            barChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[1].Contains("Bar") || chartTypes[1].Contains("Column") || chartTypes[1].Contains("Radar")))
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue4 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            barChartAxisLabel = objChartArea.YaxisLabel2;
                        }

                        dc.Scaling scaling1 = new dc.Scaling();
                        dc.Orientation orientation1 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        scaling1.Append(orientation1);
                        dc.Delete delete1 = new dc.Delete() { Val = false };
                        dc.AxisPosition axisPosition1 = new dc.AxisPosition() { Val = dc.AxisPositionValues.Left };
                        dc.NumberingFormat numberingFormat1 = new dc.NumberingFormat() { FormatCode = "General", SourceLinked = true };
                        dc.MajorTickMark majorTickMark1 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                        dc.MinorTickMark minorTickMark1 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                        dc.TickLabelPosition tickLabelPosition1 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                        dc.ChartShapeProperties chartShapeProperties7 = new dc.ChartShapeProperties();

                        d.Outline outline7 = new d.Outline() { Width = 3175 };

                        d.SolidFill solidFill8 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex3 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill8.Append(rgbColorModelHex3);
                        d.PresetDash presetDash1 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                        outline7.Append(solidFill8);
                        outline7.Append(presetDash1);

                        chartShapeProperties7.Append(outline7);

                        dc.TextProperties textProperties1 = new dc.TextProperties();
                        d.BodyProperties bodyProperties1 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                        if (IsLabel)
                        {
                            bodyProperties1.Rotation = 90;
                            bodyProperties1.Vertical = d.TextVerticalValues.Vertical;
                        }
                        d.ListStyle listStyle1 = new d.ListStyle();

                        d.Paragraph paragraph1 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties1 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties1 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                        if (IsLabel)
                        {
                            defaultRunProperties1.FontSize = 700;
                        }
                        d.SolidFill solidFill9 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex4 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill9.Append(rgbColorModelHex4);
                        d.LatinFont latinFont1 = new d.LatinFont() { Typeface = "Verdana" };
                        d.EastAsianFont eastAsianFont1 = new d.EastAsianFont() { Typeface = "Verdana" };
                        d.ComplexScriptFont complexScriptFont1 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                        defaultRunProperties1.Append(solidFill9);
                        defaultRunProperties1.Append(latinFont1);
                        defaultRunProperties1.Append(eastAsianFont1);
                        defaultRunProperties1.Append(complexScriptFont1);

                        paragraphProperties1.Append(defaultRunProperties1);
                        d.EndParagraphRunProperties endParagraphRunProperties1 = new d.EndParagraphRunProperties() { Language = "en-US" };

                        paragraph1.Append(paragraphProperties1);
                        paragraph1.Append(endParagraphRunProperties1);

                        textProperties1.Append(bodyProperties1);
                        textProperties1.Append(listStyle1);
                        textProperties1.Append(paragraph1);
                        dc.CrossingAxis crossingAxis1 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue2 };
                        dc.Crosses crosses1 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                        dc.AutoLabeled autoLabeled1 = new dc.AutoLabeled() { Val = true };
                        dc.LabelAlignment labelAlignment1 = new dc.LabelAlignment() { Val = dc.LabelAlignmentValues.Center };
                        dc.LabelOffset labelOffset1 = new dc.LabelOffset() { Val = (UInt16Value)100U };
                        dc.TickLabelSkip tickLabelSkip1 = new dc.TickLabelSkip() { Val = 1 };//
                        dc.TickMarkSkip tickMarkSkip1 = new dc.TickMarkSkip() { Val = 1 };//
                        dc.NoMultiLevelLabels noMultiLevelLabels1 = new dc.NoMultiLevelLabels() { Val = false };

                        categoryAxis1.Append(axisId5);
                        categoryAxis1.Append(scaling1);
                        categoryAxis1.Append(delete1);
                        categoryAxis1.Append(axisPosition1);
                        categoryAxis1.Append(numberingFormat1);
                        categoryAxis1.Append(majorTickMark1);
                        categoryAxis1.Append(minorTickMark1);
                        categoryAxis1.Append(tickLabelPosition1);
                        categoryAxis1.Append(chartShapeProperties7);
                        categoryAxis1.Append(textProperties1);
                        categoryAxis1.Append(crossingAxis1);
                        categoryAxis1.Append(crosses1);
                        categoryAxis1.Append(autoLabeled1);
                        categoryAxis1.Append(labelAlignment1);
                        categoryAxis1.Append(labelOffset1);
                        categoryAxis1.Append(tickLabelSkip1);
                        categoryAxis1.Append(tickMarkSkip1);
                        categoryAxis1.Append(noMultiLevelLabels1);

                        #endregion

                        barChart1.Append(dataLabels5);
                        barChart1.Append(gapWidth1);
                        barChart1.Append(axisId1);
                        barChart1.Append(axisId2);

                        #region Value Axis for Bar Chart

                        dc.ValueAxis valueAxis1 = new dc.ValueAxis();
                        dc.AxisId axisId6 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                        dc.Scaling scaling2 = new dc.Scaling();
                        dc.MinAxisValue minAxisValue = new MinAxisValue() { Val = 0 };
                        dc.MaxAxisValue maxAxisValue = new MaxAxisValue() { Val = 100 };
                        dc.Orientation orientation2 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        if (IsLabel)
                        {
                            scaling2.Append(minAxisValue);
                            scaling2.Append(maxAxisValue);
                        }
                        scaling2.Append(orientation2);
                        dc.Delete delete2 = new dc.Delete() { Val = false };

                        dc.AxisPosition axisPosition2 = new dc.AxisPosition();

                        if (chartTypes.Length > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(chartTypes[0]) && (chartTypes[0].ToLower().Contains("bar") || chartTypes[0].ToLower().Contains("column") || chartTypes[0].ToLower().Contains("radar")))
                            {
                                axisPosition2.Val = dc.AxisPositionValues.Left;
                            }
                            else
                            {
                                axisPosition2.Val = dc.AxisPositionValues.Right;
                            }
                        }
                         
                        dc.Title title1 = new dc.Title();

                        dc.ChartText chartText1 = new dc.ChartText();

                        dc.RichText richText1 = new dc.RichText();
                        d.BodyProperties bodyProperties2 = new d.BodyProperties();
                        d.ListStyle listStyle2 = new d.ListStyle();

                        d.Paragraph paragraph2 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties2 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties2 = new d.DefaultRunProperties() { FontSize = 1000, Bold = true };
                        d.LatinFont latinFont2 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont2 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        defaultRunProperties2.Append(latinFont2);
                        defaultRunProperties2.Append(complexScriptFont2);

                        paragraphProperties2.Append(defaultRunProperties2);

                        d.Run run1 = new d.Run();

                        d.RunProperties runProperties1 = new d.RunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                        d.LatinFont latinFont3 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont3 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        runProperties1.Append(latinFont3);
                        runProperties1.Append(complexScriptFont3);
                        d.Text text1 = new d.Text();
                        text1.Text = barChartAxisLabel;

                        run1.Append(runProperties1);
                        run1.Append(text1);


                        d.EndParagraphRunProperties endParagraphRunProperties2 = new d.EndParagraphRunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                        d.LatinFont latinFont5 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont5 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        endParagraphRunProperties2.Append(latinFont5);
                        endParagraphRunProperties2.Append(complexScriptFont5);

                        paragraph2.Append(paragraphProperties2);
                        paragraph2.Append(run1);
                        paragraph2.Append(endParagraphRunProperties2);

                        richText1.Append(bodyProperties2);
                        richText1.Append(listStyle2);
                        richText1.Append(paragraph2);

                        chartText1.Append(richText1);
                        dc.Overlay overlay1 = new dc.Overlay() { Val = false };

                        title1.Append(chartText1);
                        title1.Append(overlay1);
                        dc.NumberingFormat numberingFormat2 = new dc.NumberingFormat() { FormatCode = "#,##0", SourceLinked = true };
                        dc.MajorTickMark majorTickMark2 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                        dc.MinorTickMark minorTickMark2 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                        dc.TickLabelPosition tickLabelPosition2 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                        dc.ChartShapeProperties chartShapeProperties8 = new dc.ChartShapeProperties();

                        d.Outline outline8 = new d.Outline() { Width = 3175 };

                        d.SolidFill solidFill10 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex5 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill10.Append(rgbColorModelHex5);
                        d.PresetDash presetDash2 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                        outline8.Append(solidFill10);
                        outline8.Append(presetDash2);

                        chartShapeProperties8.Append(outline8);

                        dc.TextProperties textProperties2 = new dc.TextProperties();
                        d.BodyProperties bodyProperties3 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                        d.ListStyle listStyle3 = new d.ListStyle();

                        d.Paragraph paragraph3 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties3 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties3 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                        if (IsLabel)
                        {
                            defaultRunProperties3.FontSize = 700;
                        }
                        d.SolidFill solidFill11 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex6 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill11.Append(rgbColorModelHex6);
                        d.LatinFont latinFont6 = new d.LatinFont() { Typeface = "Verdana" };
                        d.EastAsianFont eastAsianFont2 = new d.EastAsianFont() { Typeface = "Verdana" };
                        d.ComplexScriptFont complexScriptFont6 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                        defaultRunProperties3.Append(solidFill11);
                        defaultRunProperties3.Append(latinFont6);
                        defaultRunProperties3.Append(eastAsianFont2);
                        defaultRunProperties3.Append(complexScriptFont6);

                        paragraphProperties3.Append(defaultRunProperties3);
                        d.EndParagraphRunProperties endParagraphRunProperties3 = new d.EndParagraphRunProperties() { Language = "en-US" };

                        paragraph3.Append(paragraphProperties3);
                        paragraph3.Append(endParagraphRunProperties3);

                        textProperties2.Append(bodyProperties3);
                        textProperties2.Append(listStyle3);
                        textProperties2.Append(paragraph3);
                        dc.CrossingAxis crossingAxis2 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue1 };
                        dc.Crosses crosses2 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                        dc.CrossBetween crossBetween1 = new dc.CrossBetween() { Val = dc.CrossBetweenValues.Between };

                        valueAxis1.Append(axisId6);
                        valueAxis1.Append(scaling2);
                        valueAxis1.Append(delete2);
                        valueAxis1.Append(axisPosition2);
                        valueAxis1.Append(title1);
                        valueAxis1.Append(numberingFormat2);
                        valueAxis1.Append(majorTickMark2);
                        valueAxis1.Append(minorTickMark2);
                        valueAxis1.Append(tickLabelPosition2);
                        valueAxis1.Append(chartShapeProperties8);
                        valueAxis1.Append(textProperties2);
                        valueAxis1.Append(crossingAxis2);
                        valueAxis1.Append(crosses2);
                        valueAxis1.Append(crossBetween1);

                        #endregion

                        plotArea1.Append(categoryAxis1);
                        plotArea1.Append(valueAxis1);
                        plotArea1.Append(barChart1);
                    }

                    #endregion

                    #region Radar Chart
                    if (objChartArea.ChartType.Contains("Radar"))
                    {
                        dc.DataLabels dataLabels5 = new dc.DataLabels();
                        dc.ShowLegendKey showLegendKey14 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue14 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName14 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName14 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent14 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize14 = new dc.ShowBubbleSize() { Val = false };
                        
                        dataLabels5.Append(showLegendKey14);
                        dataLabels5.Append(showValue14);
                        dataLabels5.Append(showCategoryName14);
                        dataLabels5.Append(showSeriesName14);
                        dataLabels5.Append(showPercent14);
                        dataLabels5.Append(showBubbleSize14);
                        dc.GapWidth gapWidth1 = new dc.GapWidth() { Val = (UInt16Value)150U };
                        dc.Overlap overlap1 = null;

                        if (objChartArea.ChartType.Contains("Stacked"))
                        {
                            overlap1 = new dc.Overlap() { Val = 100 };
                            barChart1.Append(overlap1);
                        }

                        //dc.AxisId axisId1 = new dc.AxisId() { Val = (UInt32Value)172466944U };
                        //dc.AxisId axisId2 = new dc.AxisId() { Val = (UInt32Value)172468480U };

                        #region Category  Axis for Bar Chart

                        dc.CategoryAxis categoryAxis1 = new dc.CategoryAxis();

                        dc.AxisId axisId1 = null;
                        dc.AxisId axisId2 = null;

                        dc.AxisId axisId5 = null;

                        if (chartTypes.Length == 0)
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            radarChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[0].Contains("Radar")))
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            radarChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[1].Contains("Radar")))
                        {
                            axisId1 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            axisId2 = new dc.AxisId() { Val = (UInt32Value)axisValue4 };

                            axisId5 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            radarChartAxisLabel = objChartArea.YaxisLabel2;
                        }

                        dc.Scaling scaling1 = new dc.Scaling();
                        dc.Orientation orientation1 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        scaling1.Append(orientation1);
                        dc.Delete delete1 = new dc.Delete() { Val = false };
                        dc.AxisPosition axisPosition1 = new dc.AxisPosition() { Val = dc.AxisPositionValues.Left };
                        dc.NumberingFormat numberingFormat1 = new dc.NumberingFormat() { FormatCode = "General", SourceLinked = true };
                        dc.MajorTickMark majorTickMark1 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Inside };
                        dc.MinorTickMark minorTickMark1 = new dc.MinorTickMark() { Val = dc.TickMarkValues.Inside };
                        dc.TickLabelPosition tickLabelPosition1 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                        dc.ChartShapeProperties chartShapeProperties7 = new dc.ChartShapeProperties();

                        d.Outline outline7 = new d.Outline() { Width = 3175 };

                        d.SolidFill solidFill8 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex3 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill8.Append(rgbColorModelHex3);
                        d.PresetDash presetDash1 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                        outline7.Append(solidFill8);
                        outline7.Append(presetDash1);

                        chartShapeProperties7.Append(outline7);

                        dc.TextProperties textProperties1 = new dc.TextProperties();
                        d.BodyProperties bodyProperties1 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                        d.ListStyle listStyle1 = new d.ListStyle();

                        d.Paragraph paragraph1 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties1 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties1 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                        if (IsLabel)
                        {
                            defaultRunProperties1.FontSize = 700;
                        }
                        d.SolidFill solidFill9 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex4 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill9.Append(rgbColorModelHex4);
                        d.LatinFont latinFont1 = new d.LatinFont() { Typeface = "Verdana" };
                        d.EastAsianFont eastAsianFont1 = new d.EastAsianFont() { Typeface = "Verdana" };
                        d.ComplexScriptFont complexScriptFont1 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                        defaultRunProperties1.Append(solidFill9);
                        defaultRunProperties1.Append(latinFont1);
                        defaultRunProperties1.Append(eastAsianFont1);
                        defaultRunProperties1.Append(complexScriptFont1);

                        paragraphProperties1.Append(defaultRunProperties1);
                        d.EndParagraphRunProperties endParagraphRunProperties1 = new d.EndParagraphRunProperties() { Language = "en-US" };

                        paragraph1.Append(paragraphProperties1);
                        paragraph1.Append(endParagraphRunProperties1);

                        textProperties1.Append(bodyProperties1);
                        textProperties1.Append(listStyle1);
                        textProperties1.Append(paragraph1);
                        dc.CrossingAxis crossingAxis1 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue2 };
                        dc.Crosses crosses1 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                        dc.AutoLabeled autoLabeled1 = new dc.AutoLabeled() { Val = true };
                        dc.LabelAlignment labelAlignment1 = new dc.LabelAlignment() { Val = dc.LabelAlignmentValues.Center };
                        dc.LabelOffset labelOffset1 = new dc.LabelOffset() { Val = (UInt16Value)100U };
                        dc.TickLabelSkip tickLabelSkip1 = new dc.TickLabelSkip() { Val = 1 };//
                        dc.TickMarkSkip tickMarkSkip1 = new dc.TickMarkSkip() { Val = 1 };//
                        dc.NoMultiLevelLabels noMultiLevelLabels1 = new dc.NoMultiLevelLabels() { Val = false };

                        categoryAxis1.Append(axisId5);
                        categoryAxis1.Append(scaling1);
                        categoryAxis1.Append(delete1);
                        categoryAxis1.Append(axisPosition1);
                        categoryAxis1.Append(numberingFormat1);
                        categoryAxis1.Append(majorTickMark1);
                        categoryAxis1.Append(minorTickMark1);
                        categoryAxis1.Append(tickLabelPosition1);
                        categoryAxis1.Append(chartShapeProperties7);
                        categoryAxis1.Append(textProperties1);
                        categoryAxis1.Append(crossingAxis1);
                        categoryAxis1.Append(crosses1);
                        categoryAxis1.Append(autoLabeled1);
                        categoryAxis1.Append(labelAlignment1);
                        categoryAxis1.Append(labelOffset1);
                        categoryAxis1.Append(tickLabelSkip1);
                        categoryAxis1.Append(tickMarkSkip1);
                        categoryAxis1.Append(noMultiLevelLabels1);

                        #endregion

                        radarChart1.Append(dataLabels5);
                        radarChart1.Append(gapWidth1);
                        radarChart1.Append(axisId1);
                        radarChart1.Append(axisId2);

                        #region Value Axis for Bar Chart

                        dc.ValueAxis valueAxis1 = new dc.ValueAxis();
                        dc.AxisId axisId6 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                        dc.Scaling scaling2 = new dc.Scaling();
                        dc.MinAxisValue minAxisVal = new dc.MinAxisValue() { Val = 0 };
                        dc.MaxAxisValue maxAxisVal = new dc.MaxAxisValue() { Val = 100 };
                        dc.Orientation orientation2 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        scaling2.Append(minAxisVal);
                        scaling2.Append(maxAxisVal);
                        scaling2.Append(orientation2);

                        dc.Delete delete2 = new dc.Delete() { Val = false };

                        dc.AxisPosition axisPosition2 = new dc.AxisPosition();

                        if (chartTypes.Length > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(chartTypes[0]) && (chartTypes[0].ToLower().Contains("bar") || chartTypes[0].ToLower().Contains("column") || chartTypes[0].ToLower().Contains("radar")))
                            {
                                axisPosition2.Val = dc.AxisPositionValues.Left;
                            }
                            else
                            {
                                axisPosition2.Val = dc.AxisPositionValues.Right;
                            }
                        }

                        dc.Title title1 = new dc.Title();

                        dc.ChartText chartText1 = new dc.ChartText();

                        dc.RichText richText1 = new dc.RichText();
                        d.BodyProperties bodyProperties2 = new d.BodyProperties();
                        d.ListStyle listStyle2 = new d.ListStyle();

                        d.Paragraph paragraph2 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties2 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties2 = new d.DefaultRunProperties() { FontSize = 1000, Bold = true };
                        d.LatinFont latinFont2 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont2 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        defaultRunProperties2.Append(latinFont2);
                        defaultRunProperties2.Append(complexScriptFont2);

                        paragraphProperties2.Append(defaultRunProperties2);

                        d.Run run1 = new d.Run();

                        d.RunProperties runProperties1 = new d.RunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                        d.LatinFont latinFont3 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont3 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        runProperties1.Append(latinFont3);
                        runProperties1.Append(complexScriptFont3);
                        d.Text text1 = new d.Text();
                        text1.Text = barChartAxisLabel;

                        run1.Append(runProperties1);
                        run1.Append(text1);


                        d.EndParagraphRunProperties endParagraphRunProperties2 = new d.EndParagraphRunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                        d.LatinFont latinFont5 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont5 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        endParagraphRunProperties2.Append(latinFont5);
                        endParagraphRunProperties2.Append(complexScriptFont5);

                        paragraph2.Append(paragraphProperties2);
                        paragraph2.Append(run1);
                        paragraph2.Append(endParagraphRunProperties2);

                        richText1.Append(bodyProperties2);
                        richText1.Append(listStyle2);
                        richText1.Append(paragraph2);

                        chartText1.Append(richText1);
                        dc.Overlay overlay1 = new dc.Overlay() { Val = false };

                        title1.Append(chartText1);
                        title1.Append(overlay1);
                        dc.NumberingFormat numberingFormat2 = new dc.NumberingFormat() { FormatCode = "#,##0", SourceLinked = true };
                        dc.MajorTickMark majorTickMark2 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Inside };
                        dc.MinorTickMark minorTickMark2 = new dc.MinorTickMark() { Val = dc.TickMarkValues.Inside };
                        dc.TickLabelPosition tickLabelPosition2 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                        dc.ChartShapeProperties chartShapeProperties8 = new dc.ChartShapeProperties();

                        d.Outline outline8 = new d.Outline() { Width = 3175 };

                        d.SolidFill solidFill10 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex5 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill10.Append(rgbColorModelHex5);
                        d.PresetDash presetDash2 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                        outline8.Append(solidFill10);
                        outline8.Append(presetDash2);

                        chartShapeProperties8.Append(outline8);

                        dc.TextProperties textProperties2 = new dc.TextProperties();
                        d.BodyProperties bodyProperties3 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                        d.ListStyle listStyle3 = new d.ListStyle();

                        d.Paragraph paragraph3 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties3 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties3 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                        if (IsLabel)
                        {
                            defaultRunProperties3.FontSize = 700;
                        }
                        d.SolidFill solidFill11 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex6 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill11.Append(rgbColorModelHex6);
                        d.LatinFont latinFont6 = new d.LatinFont() { Typeface = "Verdana" };
                        d.EastAsianFont eastAsianFont2 = new d.EastAsianFont() { Typeface = "Verdana" };
                        d.ComplexScriptFont complexScriptFont6 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                        defaultRunProperties3.Append(solidFill11);
                        defaultRunProperties3.Append(latinFont6);
                        defaultRunProperties3.Append(eastAsianFont2);
                        defaultRunProperties3.Append(complexScriptFont6);

                        paragraphProperties3.Append(defaultRunProperties3);
                        d.EndParagraphRunProperties endParagraphRunProperties3 = new d.EndParagraphRunProperties() { Language = "en-US" };

                        paragraph3.Append(paragraphProperties3);
                        paragraph3.Append(endParagraphRunProperties3);

                        textProperties2.Append(bodyProperties3);
                        textProperties2.Append(listStyle3);
                        textProperties2.Append(paragraph3);
                        dc.CrossingAxis crossingAxis2 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue1 };
                        dc.Crosses crosses2 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                        dc.CrossBetween crossBetween1 = new dc.CrossBetween() { Val = dc.CrossBetweenValues.Between };
                        dc.MajorGridlines majorgridline = new MajorGridlines() { ChartShapeProperties = new ChartShapeProperties() {BlackWhiteMode=BlackWhiteModeValues.Black } };

                        valueAxis1.Append(axisId6);
                        valueAxis1.Append(scaling2);
                        valueAxis1.Append(delete2);
                        valueAxis1.Append(axisPosition2);
                        valueAxis1.Append(title1);
                        valueAxis1.Append(numberingFormat2);
                        valueAxis1.Append(majorTickMark2);
                        valueAxis1.Append(minorTickMark2);
                        valueAxis1.Append(tickLabelPosition2);
                        valueAxis1.Append(chartShapeProperties8);
                        valueAxis1.Append(textProperties2);
                        valueAxis1.Append(crossingAxis2);
                        valueAxis1.Append(crosses2);
                        valueAxis1.Append(crossBetween1);
                        valueAxis1.Append(majorgridline);

                        #endregion

                        plotArea1.Append(categoryAxis1);
                        plotArea1.Append(valueAxis1);
                        plotArea1.Append(radarChart1);
                    }
                    #endregion

                    #region Line Chart

                    if (objChartArea.ChartType.Contains("Line"))
                    {
                        dc.DataLabels dataLabels7 = new dc.DataLabels();
                        dc.ShowLegendKey showLegendKey19 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue19 = new dc.ShowValue() { Val = false };
                        dc.ShowCategoryName showCategoryName19 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName19 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent19 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize19 = new dc.ShowBubbleSize() { Val = false };

                        dataLabels7.Append(showLegendKey19);
                        dataLabels7.Append(showValue19);
                        dataLabels7.Append(showCategoryName19);
                        dataLabels7.Append(showSeriesName19);
                        dataLabels7.Append(showPercent19);
                        dataLabels7.Append(showBubbleSize19);
                        dc.ShowMarker showMarker1 = new dc.ShowMarker() { Val = true };
                        dc.Smooth smooth2 = new dc.Smooth() { Val = false };
                        //dc.AxisId axisId3 = new dc.AxisId() { Val = (UInt32Value)172653184U };
                        //dc.AxisId axisId4 = new dc.AxisId() { Val = (UInt32Value)172651264U };

                        #region Category Axis for Line Chart

                        dc.CategoryAxis categoryAxis2 = new dc.CategoryAxis();

                        dc.AxisId axisId3 = null;
                        dc.AxisId axisId4 = null;

                        dc.AxisId axisId8 = null;

                        if (chartTypes.Length == 0)
                        {
                            axisId3 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId4 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId8 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            lineChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[0].Contains("Line")))
                        {
                            axisId3 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            axisId4 = new dc.AxisId() { Val = (UInt32Value)axisValue2 };

                            axisId8 = new dc.AxisId() { Val = (UInt32Value)axisValue1 };
                            lineChartAxisLabel = objChartArea.YaxisLabel1;
                        }
                        else if (chartTypes.Length > 0 && (chartTypes[1].Contains("Line")))
                        {
                            axisId3 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            axisId4 = new dc.AxisId() { Val = (UInt32Value)axisValue4 };

                            axisId8 = new dc.AxisId() { Val = (UInt32Value)axisValue3 };
                            lineChartAxisLabel = objChartArea.YaxisLabel2;
                        }

                        dc.Scaling scaling4 = new dc.Scaling();
                        dc.Orientation orientation4 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        scaling4.Append(orientation4);
                        dc.Delete delete4 = new dc.Delete() { Val = true };
                        dc.AxisPosition axisPosition4 = new dc.AxisPosition() { Val = dc.AxisPositionValues.Bottom };
                       
                        dc.MajorTickMark majorTickMark4 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                        dc.MinorTickMark minorTickMark4 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                        dc.TickLabelPosition tickLabelPosition4 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };
                        dc.CrossingAxis crossingAxis4 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue4 };
                        dc.Crosses crosses4 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                        dc.AutoLabeled autoLabeled2 = new dc.AutoLabeled() { Val = true };
                        dc.LabelAlignment labelAlignment2 = new dc.LabelAlignment() { Val = dc.LabelAlignmentValues.Center };
                        dc.LabelOffset labelOffset2 = new dc.LabelOffset() { Val = (UInt16Value)100U };
                        dc.NoMultiLevelLabels noMultiLevelLabels2 = new dc.NoMultiLevelLabels() { Val = false };

                        categoryAxis2.Append(axisId8);
                        categoryAxis2.Append(scaling4);
                        categoryAxis2.Append(delete4);
                        categoryAxis2.Append(axisPosition4);
                        categoryAxis2.Append(majorTickMark4);
                        categoryAxis2.Append(minorTickMark4);
                        categoryAxis2.Append(tickLabelPosition4);
                        categoryAxis2.Append(crossingAxis4);
                        categoryAxis2.Append(crosses4);
                        categoryAxis2.Append(autoLabeled2);
                        categoryAxis2.Append(labelAlignment2);
                        categoryAxis2.Append(labelOffset2);
                        categoryAxis2.Append(noMultiLevelLabels2);

                        #endregion

                        lineChart1.Append(dataLabels7);
                        lineChart1.Append(showMarker1);
                        lineChart1.Append(smooth2);
                        lineChart1.Append(axisId3);
                        lineChart1.Append(axisId4);

                        #region Value Axis for Line Chart

                        dc.ValueAxis valueAxis2 = new dc.ValueAxis();
                        dc.AxisId axisId7 = new dc.AxisId() { Val = (UInt32Value)axisValue4 };

                        dc.Scaling scaling3 = new dc.Scaling();
                        dc.Orientation orientation3 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                        scaling3.Append(orientation3);
                        dc.Delete delete3 = new dc.Delete() { Val = false };
                        dc.AxisPosition axisPosition3 = new dc.AxisPosition();

                        if (chartTypes.Length > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(chartTypes[0]) && chartTypes[0].ToLower().Contains("line"))
                            {
                                axisPosition3.Val = dc.AxisPositionValues.Left;
                            }
                            else
                            {
                                axisPosition3.Val = dc.AxisPositionValues.Right;
                            }
                        }
                        
                        dc.Title title2 = new dc.Title();

                        dc.ChartText chartText2 = new dc.ChartText();

                        dc.RichText richText2 = new dc.RichText();
                        d.BodyProperties bodyProperties4 = new d.BodyProperties();
                        d.ListStyle listStyle4 = new d.ListStyle();

                        d.Paragraph paragraph4 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties4 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties4 = new d.DefaultRunProperties() { FontSize = 1000, Bold = true };
                        d.LatinFont latinFont7 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont7 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        defaultRunProperties4.Append(latinFont7);
                        defaultRunProperties4.Append(complexScriptFont7);

                        paragraphProperties4.Append(defaultRunProperties4);

                        d.Run run3 = new d.Run();

                        d.RunProperties runProperties3 = new d.RunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                        d.LatinFont latinFont8 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.ComplexScriptFont complexScriptFont8 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        runProperties3.Append(latinFont8);
                        runProperties3.Append(complexScriptFont8);
                        d.Text text3 = new d.Text();
                        text3.Text = lineChartAxisLabel;

                        run3.Append(runProperties3);
                        run3.Append(text3);

                        paragraph4.Append(paragraphProperties4);
                        paragraph4.Append(run3);

                        richText2.Append(bodyProperties4);
                        richText2.Append(listStyle4);
                        richText2.Append(paragraph4);

                        chartText2.Append(richText2);
                        dc.Overlay overlay2 = new dc.Overlay() { Val = false };

                        title2.Append(chartText2);
                        title2.Append(overlay2);
                        dc.NumberingFormat numberingFormat3 = new dc.NumberingFormat() { FormatCode = "General", SourceLinked = true };
                        dc.MajorTickMark majorTickMark3 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                        dc.MinorTickMark minorTickMark3 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                        dc.TickLabelPosition tickLabelPosition3 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };
                        dc.CrossingAxis crossingAxis3 = new dc.CrossingAxis() { Val = (UInt32Value)axisValue3 };
                        dc.Crosses crosses3 = new dc.Crosses() { Val = dc.CrossesValues.Maximum };
                        dc.CrossBetween crossBetween2 = new dc.CrossBetween() { Val = dc.CrossBetweenValues.Between };

                        valueAxis2.Append(axisId7);
                        valueAxis2.Append(scaling3);
                        valueAxis2.Append(delete3);
                        valueAxis2.Append(axisPosition3);
                        valueAxis2.Append(title2);
                        valueAxis2.Append(numberingFormat3);
                        valueAxis2.Append(majorTickMark3);
                        valueAxis2.Append(minorTickMark3);
                        valueAxis2.Append(tickLabelPosition3);
                        valueAxis2.Append(crossingAxis3);
                        valueAxis2.Append(crosses3);
                        valueAxis2.Append(crossBetween2);

                        #endregion

                        plotArea1.Append(valueAxis2);
                        plotArea1.Append(lineChart1);
                    }
                    #endregion

                    #region shapeProperties1

                    dc.ShapeProperties shapeProperties1 = new dc.ShapeProperties();
                    d.NoFill noFill5 = new d.NoFill();

                    d.Outline outline9 = new d.Outline() { Width = 26400 };
                    d.NoFill noFill6 = new d.NoFill();

                    outline9.Append(noFill6);

                    shapeProperties1.Append(noFill5);
                    shapeProperties1.Append(outline9);

                    #endregion

                    plotArea1.Append(layout1);
                    plotArea1.Append(shapeProperties1);

                    #region Legend

                    dc.Legend legend1 = new dc.Legend();
                    dc.LegendPosition legendPosition1 = new dc.LegendPosition();

                    if (objChartArea.ChartType.ToLower().Contains("bar")|| objChartArea.ChartType.ToLower().Contains("radar"))
                    {
                        legendPosition1.Val = dc.LegendPositionValues.Bottom;
                    }
                    else
                    {
                        legendPosition1.Val = dc.LegendPositionValues.Bottom;
                    }

                    dc.Overlay overlay3 = new dc.Overlay() { Val = true };

                    dc.ChartShapeProperties chartShapeProperties9 = new dc.ChartShapeProperties();
                    d.NoFill noFill7 = new d.NoFill();

                    d.Outline outline10 = new d.Outline() { Width = 26400 };
                    d.NoFill noFill8 = new d.NoFill();

                    outline10.Append(noFill8);

                    chartShapeProperties9.Append(noFill7);
                    chartShapeProperties9.Append(outline10);

                    dc.TextProperties textProperties3 = new dc.TextProperties();
                    d.BodyProperties bodyProperties5 = new d.BodyProperties();
                    d.ListStyle listStyle5 = new d.ListStyle();

                    d.Paragraph paragraph5 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties5 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties5 = new d.DefaultRunProperties() { FontSize = 900, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };

                    d.SolidFill solidFill12 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex7 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill12.Append(rgbColorModelHex7);
                    d.LatinFont latinFont9 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                    d.EastAsianFont eastAsianFont3 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont9 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                    defaultRunProperties5.Append(solidFill12);
                    defaultRunProperties5.Append(latinFont9);
                    defaultRunProperties5.Append(eastAsianFont3);
                    defaultRunProperties5.Append(complexScriptFont9);

                    paragraphProperties5.Append(defaultRunProperties5);
                    d.EndParagraphRunProperties endParagraphRunProperties4 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph5.Append(paragraphProperties5);
                    paragraph5.Append(endParagraphRunProperties4);

                    textProperties3.Append(bodyProperties5);
                    textProperties3.Append(listStyle5);
                    textProperties3.Append(paragraph5);
                    if (!IsLabel)
                    {
                        legend1.Append(legendPosition1);//Necessary
                    }
                    legend1.Append(overlay3);//Necessary
                    legend1.Append(chartShapeProperties9);
                    legend1.Append(textProperties3);

                    #endregion

                    dc.PlotVisibleOnly plotVisibleOnly1 = new dc.PlotVisibleOnly() { Val = true };
                    dc.DisplayBlanksAs displayBlanksAs1 = new dc.DisplayBlanksAs() { Val = dc.DisplayBlanksAsValues.Gap };
                    dc.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new dc.ShowDataLabelsOverMaximum() { Val = false };

                    chart1.Append(autoTitleDeleted1);
                    chart1.Append(plotArea1);
                    chart1.Append(plotVisibleOnly1);
                    chart1.Append(displayBlanksAs1);
                    chart1.Append(showDataLabelsOverMaximum1);
                    if (!IsLabel)
                    {
                        chart1.Append(legend1);
                    }
                    dc.ChartShapeProperties chartShapeProperties10 = new dc.ChartShapeProperties();
                    d.NoFill noFill9 = new d.NoFill();

                    d.Outline outline11 = new d.Outline() { Width = 6350 };
                    d.NoFill noFill10 = new d.NoFill();

                    outline11.Append(noFill10);

                    chartShapeProperties10.Append(noFill9);
                    chartShapeProperties10.Append(outline11);

                    dc.TextProperties textProperties4 = new dc.TextProperties();
                    d.BodyProperties bodyProperties6 = new d.BodyProperties();
                    d.ListStyle listStyle6 = new d.ListStyle();

                    d.Paragraph paragraph6 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties6 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties6 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                    if (IsLabel)
                    {
                        defaultRunProperties6.FontSize = 700;
                    }
                    d.SolidFill solidFill13 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex8 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill13.Append(rgbColorModelHex8);
                    d.LatinFont latinFont10 = new d.LatinFont() { Typeface = "Verdana" };
                    d.EastAsianFont eastAsianFont4 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont10 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                    defaultRunProperties6.Append(solidFill13);
                    defaultRunProperties6.Append(latinFont10);
                    defaultRunProperties6.Append(eastAsianFont4);
                    defaultRunProperties6.Append(complexScriptFont10);

                    paragraphProperties6.Append(defaultRunProperties6);
                    d.EndParagraphRunProperties endParagraphRunProperties5 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph6.Append(paragraphProperties6);
                    paragraph6.Append(endParagraphRunProperties5);

                    textProperties4.Append(bodyProperties6);
                    textProperties4.Append(listStyle6);
                    textProperties4.Append(paragraph6);

                    chartSpace1.Append(date19041);
                    chartSpace1.Append(editingLanguage1);
                    chartSpace1.Append(roundedCorners1);
                    chartSpace1.Append(alternateContent1);
                    chartSpace1.Append(chart1);
                    chartSpace1.Append(chartShapeProperties10);
                    chartSpace1.Append(textProperties4);
                    //chartSpace1.Append(externalData1);

                    chartPart.ChartSpace = chartSpace1;

                    CreateExcelFile(chartPart, lstChartSubArea, excelSourceId, filepath);

                    //Generate content of the MainDocumentPart
                    GeneratePartContent(mainPart, chartPartRId, IsLabel);

                    //save this part
                    objWordDocx.MainDocumentPart.Document.Save();
                    //save and close the document
                    objWordDocx.Close();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (objWordDocx != null)
                {
                    objWordDocx.Close();
                }

                System.Web.HttpContext.Current.Session["Error"] = ex.Message;
                throw ex;
            }
        }

        private void CreateExcelFile(ChartPart chartPart, List<ChartSeries> lstChartSubArea, string excelSourceId, string filepath)
        {
            dc.ExternalData externalData1 = new dc.ExternalData() { Id = excelSourceId };
            dc.AutoUpdate autoUpdate1 = new dc.AutoUpdate() { Val = false };

            externalData1.Append(autoUpdate1);
            chartPart.ChartSpace.Append(externalData1);

            EmbeddedPackagePart embeddedPackagePart1 = chartPart.AddNewPart<EmbeddedPackagePart>("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelSourceId);

            var dict = new Dictionary<string, ArrayList>();
            List<string> seriesTitles = new List<string>();

            foreach (ChartSeries csa in lstChartSubArea)
            {
                seriesTitles.Add(csa.SeriesTitle);
            }

            string categoryTitle = "";

            var dt = new System.Data.DataTable();
            dt.Columns.Add(categoryTitle);

            foreach (string st in seriesTitles)
            {
                dt.Columns.Add(st, typeof(decimal));
            }

            foreach (ChartSeries csa in lstChartSubArea)
            {
                List<ChartData> chartList = csa.ChartDataList;

                seriesTitles.Add(csa.SeriesTitle);

                ArrayList data = null;

                foreach (var item in chartList)
                {
                    string numericValue = string.Empty;

                    if (item.DataType.Contains("percent"))
                    {
                        float yAxisValue = Convert.ToSingle(item.YaxisValue);
                        numericValue = (yAxisValue / 100).ToString();
                    }
                    else
                    {
                        numericValue = item.YaxisValue;
                    }

                    if (!dict.ContainsKey(item.XaxisLabel))
                    {
                        data = new ArrayList();
                        data.Add(numericValue);
                        dict.Add(item.XaxisLabel, data);
                    }
                    else
                    {
                        data = new ArrayList();
                        data = dict[item.XaxisLabel];
                        data.Add(numericValue);

                        dict[item.XaxisLabel] = data;
                    }
                }
            }

            foreach (string key in dict.Keys)
            {
                var dr = dt.NewRow();
                dr[0] = key;

                ArrayList arr = dict[key];

                for (int i = 1; i <= arr.Count; i++)
                    dr[i] = arr[i - 1];
                dt.Rows.Add(dr);
            }

            string dataDir = filepath;

            string outputFile = "Worksheet_" + Guid.NewGuid().ToString() + ".xlsx"; // get random file name 

            dataDir = System.Web.HttpContext.Current.Server.MapPath(dataDir);

            string tempCsv = System.IO.Path.Combine(dataDir + outputFile);

            if (File.Exists(tempCsv))
            {
                File.Delete(tempCsv);
                ExcelTools.CreatePackage(tempCsv, dt, true);
            }
            else
            {
                ExcelTools.CreatePackage(tempCsv, dt, true);
            }

            using (System.IO.Stream data = new System.IO.MemoryStream(File.ReadAllBytes(tempCsv)))
            {
                embeddedPackagePart1.FeedData(data);
                data.Close();
            }

            if (File.Exists(tempCsv))
                File.Delete(tempCsv); // remove our file we created 
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public void GenerateWordChart_LineOnly(ChartArea objChartArea, string sourceFile, string chartPartRId, string excelSourceId)
        {
            WordprocessingDocument objWordDocx = null;

            try
            {
                if (objChartArea != null && !string.IsNullOrEmpty(sourceFile))
                {
                    string barChartAxisLabel = string.Empty;
                    string lineChartAxisLabel = string.Empty;
                    string barChartFormatCode = string.Empty;
                    string lineChartFormatCode = string.Empty;
                   
                    List<ChartSeries> lstChartSubArea = objChartArea.ChartSubAreaList;
                    List<ChartData> chartList = null;

                    //declare and open a Word document object
                    objWordDocx = WordprocessingDocument.Open(sourceFile, true);

                    // Get MainDocumentPart of Document
                    MainDocumentPart mainPart = objWordDocx.MainDocumentPart;

                    // Create ChartPart object in Word Document
                    ChartPart chartPart = mainPart.AddNewPart<ChartPart>(chartPartRId);

                    EmbeddedPackagePart embeddedPackagePart1 = chartPart.AddNewPart<EmbeddedPackagePart>("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "rId1");
                    GenerateEmbeddedPackagePart1Content(embeddedPackagePart1);

                    //ChartPart part = null;
                    dc.ChartSpace chartSpace1 = new dc.ChartSpace();
                    chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
                    chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                    dc.Date1904 date19041 = new dc.Date1904() { Val = false };
                    dc.EditingLanguage editingLanguage1 = new dc.EditingLanguage() { Val = "en-US" };
                    dc.RoundedCorners roundedCorners1 = new dc.RoundedCorners() { Val = false };

                    AlternateContent alternateContent1 = new AlternateContent();
                    alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

                    AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice() { Requires = "c14" };
                    alternateContentChoice1.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
                    C14.Style style1 = new C14.Style() { Val = 102 };

                    alternateContentChoice1.Append(style1);

                    AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
                    dc.Style style2 = new dc.Style() { Val = 2 };

                    alternateContentFallback1.Append(style2);

                    alternateContent1.Append(alternateContentChoice1);
                    alternateContent1.Append(alternateContentFallback1);

                    dc.Chart chart1 = new dc.Chart();
                    dc.AutoTitleDeleted autoTitleDeleted1 = new dc.AutoTitleDeleted() { Val = true };

                    dc.PlotArea plotArea1 = new dc.PlotArea();

                    dc.Layout layout1 = new dc.Layout();

                    dc.ManualLayout manualLayout1 = new dc.ManualLayout();
                    dc.LayoutTarget layoutTarget1 = new dc.LayoutTarget() { Val = dc.LayoutTargetValues.Inner };
                    dc.LeftMode leftMode1 = new dc.LeftMode() { Val = dc.LayoutModeValues.Edge };
                    dc.TopMode topMode1 = new dc.TopMode() { Val = dc.LayoutModeValues.Edge };
                    //dc.Left left1 = new dc.Left() { Val = 0.124885704165872D };

                    //dc.Left left1 = new dc.Left() { Val = 0.15841840890578336D };
                    //dc.Top top1 = new dc.Top() { Val = 0.1210729463770589D };
                    //dc.Width width1 = new dc.Width() { Val = 0.84494463773423667D };
                    //dc.Height height1 = new dc.Height() { Val = 0.73546639435052352D };

                    dc.Left left1 = new dc.Left() { Val = 0.13841840890578336D };
                    dc.Top top1 = new dc.Top() { Val = 7.5665266908490111E-2D };
                    dc.Width width1 = new dc.Width() { Val = 0.72075300932211062D };
                    dc.Height height1 = new dc.Height() { Val = 0.73546639435052352D };

                    manualLayout1.Append(layoutTarget1);
                    manualLayout1.Append(leftMode1);
                    manualLayout1.Append(topMode1);
                    manualLayout1.Append(left1);
                    manualLayout1.Append(top1);
                    manualLayout1.Append(width1);
                    manualLayout1.Append(height1);

                    layout1.Append(manualLayout1);

                    dc.LineChart lineChart1 = new dc.LineChart();
                    dc.Grouping grouping1 = new dc.Grouping() { Val = dc.GroupingValues.Standard };
                    dc.VaryColors varyColors1 = new dc.VaryColors() { Val = false };

                    uint c = 0;
                    uint xAxisLabelCount = default(uint);

                    foreach (ChartSeries csa in lstChartSubArea)
                    {
                        chartList = csa.ChartDataList;

                        if (xAxisLabelCount < chartList.Count)
                        {
                            xAxisLabelCount = (uint)chartList.Count;
                        }

                        //if (csa.SeriesType == "Line")
                        //{
                        dc.LineChartSeries lineChartSeries1 = new dc.LineChartSeries();
                        dc.Index index1 = new dc.Index() { Val = new UInt32Value(c) }; //(UInt32Value)1U
                        dc.Order order1 = new dc.Order() { Val = new UInt32Value(c) }; //(UInt32Value)0U
                        c++;

                        dc.SeriesText seriesText1 = new dc.SeriesText();
                        dc.NumericValue numericValue1 = new dc.NumericValue();
                        //numericValue1.Text = "ebit";
                        numericValue1.Text = csa.SeriesTitle;

                        seriesText1.Append(numericValue1);

                        dc.ChartShapeProperties chartShapeProperties1 = new dc.ChartShapeProperties();

                        d.Outline outline1 = new d.Outline();
                        d.SolidFill solidFill1 = new d.SolidFill();

                        outline1.Append(solidFill1);

                        chartShapeProperties1.Append(outline1);

                        dc.Marker marker1 = new dc.Marker();

                        dc.ChartShapeProperties chartShapeProperties2 = new dc.ChartShapeProperties();
                        d.SolidFill solidFill2 = new d.SolidFill();

                        d.Outline outline2 = new d.Outline();
                        d.SolidFill solidFill3 = new d.SolidFill();

                        outline2.Append(solidFill3);

                        chartShapeProperties2.Append(solidFill2);
                        chartShapeProperties2.Append(outline2);

                        marker1.Append(chartShapeProperties2);

                        dc.DataLabels dataLabels1 = new dc.DataLabels();

                        dc.DataLabel dataLabel1 = new dc.DataLabel();
                        dc.Index index2 = new dc.Index() { Val = (UInt32Value)0U };

                        dc.Layout layout2 = new dc.Layout();

                        dc.ManualLayout manualLayout2 = new dc.ManualLayout();
                        dc.Left left2 = new dc.Left() { Val = -4.4742729306487695E-3D };
                        dc.Top top2 = new dc.Top() { Val = 3.8338658146964855E-2D };

                        manualLayout2.Append(left2);
                        manualLayout2.Append(top2);

                        layout2.Append(manualLayout2);
                        dc.ShowLegendKey showLegendKey1 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue1 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName1 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName1 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent1 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize1 = new dc.ShowBubbleSize() { Val = false };

                        dataLabel1.Append(index2);
                        dataLabel1.Append(layout2);
                        dataLabel1.Append(showLegendKey1);
                        dataLabel1.Append(showValue1);
                        dataLabel1.Append(showCategoryName1);
                        dataLabel1.Append(showSeriesName1);
                        dataLabel1.Append(showPercent1);
                        dataLabel1.Append(showBubbleSize1);

                        dc.DataLabel dataLabel2 = new dc.DataLabel();
                        dc.Index index3 = new dc.Index() { Val = (UInt32Value)1U };

                        dc.Layout layout3 = new dc.Layout();

                        dc.ManualLayout manualLayout3 = new dc.ManualLayout();
                        dc.Left left3 = new dc.Left() { Val = 0D };
                        dc.Top top3 = new dc.Top() { Val = 2.5559105431309903E-2D };

                        manualLayout3.Append(left3);
                        manualLayout3.Append(top3);

                        layout3.Append(manualLayout3);
                        dc.ShowLegendKey showLegendKey2 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue2 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName2 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName2 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent2 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize2 = new dc.ShowBubbleSize() { Val = false };

                        dataLabel2.Append(index3);
                        dataLabel2.Append(layout3);
                        dataLabel2.Append(showLegendKey2);
                        dataLabel2.Append(showValue2);
                        dataLabel2.Append(showCategoryName2);
                        dataLabel2.Append(showSeriesName2);
                        dataLabel2.Append(showPercent2);
                        dataLabel2.Append(showBubbleSize2);

                        dc.DataLabel dataLabel3 = new dc.DataLabel();
                        dc.Index index4 = new dc.Index() { Val = (UInt32Value)2U };

                        dc.Layout layout4 = new dc.Layout();

                        dc.ManualLayout manualLayout4 = new dc.ManualLayout();
                        dc.Left left4 = new dc.Left() { Val = -3.3557046979865772E-2D };
                        dc.Top top4 = new dc.Top() { Val = 4.6858359957401494E-2D };

                        manualLayout4.Append(left4);
                        manualLayout4.Append(top4);

                        layout4.Append(manualLayout4);
                        dc.ShowLegendKey showLegendKey3 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue3 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName3 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName3 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent3 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize3 = new dc.ShowBubbleSize() { Val = false };

                        dataLabel3.Append(index4);
                        dataLabel3.Append(layout4);
                        dataLabel3.Append(showLegendKey3);
                        dataLabel3.Append(showValue3);
                        dataLabel3.Append(showCategoryName3);
                        dataLabel3.Append(showSeriesName3);
                        dataLabel3.Append(showPercent3);
                        dataLabel3.Append(showBubbleSize3);
                        dc.ShowLegendKey showLegendKey4 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue4 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName4 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName4 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent4 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize4 = new dc.ShowBubbleSize() { Val = false };
                        dc.ShowLeaderLines showLeaderLines1 = new dc.ShowLeaderLines() { Val = false };

                        dataLabels1.Append(dataLabel1);
                        dataLabels1.Append(dataLabel2);
                        dataLabels1.Append(dataLabel3);
                        dataLabels1.Append(showLegendKey4);
                        dataLabels1.Append(showValue4);
                        dataLabels1.Append(showCategoryName4);
                        dataLabels1.Append(showSeriesName4);
                        dataLabels1.Append(showPercent4);
                        dataLabels1.Append(showBubbleSize4);
                        dataLabels1.Append(showLeaderLines1);

                        dc.CategoryAxisData categoryAxisData1 = new dc.CategoryAxisData();
                        dc.StringLiteral stringLiteral1 = new dc.StringLiteral();

                        dc.PointCount pointCountXaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                        uint i = 0;

                        // Fill data for chart
                        foreach (var item in chartList)
                        {
                            dc.StringPoint stringPointXaxis = new dc.StringPoint() { Index = new UInt32Value(i) };
                            dc.NumericValue numericValueXaxis = new dc.NumericValue();
                            numericValueXaxis.Text = item.XaxisLabel;

                            stringPointXaxis.Append(numericValueXaxis);
                            stringLiteral1.Append(stringPointXaxis);

                            i++;
                        }

                        stringLiteral1.Append(pointCountXaxis);

                        categoryAxisData1.Append(stringLiteral1);

                        dc.Values values1 = new dc.Values();
                        dc.NumberLiteral numberLiteral1 = new dc.NumberLiteral();

                        i = 0;

                        // Fill data for chart
                        foreach (var item in chartList)
                        {
                            dc.NumericPoint numericPointYaxis = new dc.NumericPoint() { Index = new UInt32Value(i) };
                            dc.NumericValue numericValueYaxis = new dc.NumericValue();
                            lineChartFormatCode = item.DataType;

                            if (lineChartFormatCode.Contains("percent"))
                            {
                                float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                numericValueYaxis.Text = (yAxisValue / 100).ToString();
                            }
                            else
                            {
                                numericValueYaxis.Text = item.YaxisValue;
                            }

                            numericPointYaxis.Append(numericValueYaxis);
                            numberLiteral1.Append(numericPointYaxis);

                            i++;
                        }

                        dc.FormatCode formatCode = new dc.FormatCode();

                        if (lineChartFormatCode.Contains("number"))
                        {
                            formatCode.Text = "#,#0.0"; //"#,##0.0";
                        }
                        else if (lineChartFormatCode.Contains("percent"))
                        {
                            formatCode.Text = "0.00%";
                        }

                        dc.PointCount pointCountYaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                        numberLiteral1.Append(formatCode);
                        numberLiteral1.Append(pointCountYaxis);

                        values1.Append(numberLiteral1);

                        lineChartSeries1.Append(categoryAxisData1);
                        lineChartSeries1.Append(values1);

                        dc.Smooth smooth1 = new dc.Smooth() { Val = false };
                        lineChartSeries1.Append(smooth1);
                        lineChartSeries1.Append(index1);
                        lineChartSeries1.Append(order1);
                        lineChartSeries1.Append(seriesText1);
                        lineChartSeries1.Append(chartShapeProperties1);
                        lineChartSeries1.Append(marker1);
                        lineChartSeries1.Append(dataLabels1);

                        lineChart1.Append(lineChartSeries1);
                        //}
                    }

                    //dc.Smooth smooth1 = new dc.Smooth() { Val = false };
                    //lineChartSeries1.Append(smooth1);
                    //lineChartSeries1.Append(index1);
                    //lineChartSeries1.Append(order1);
                    //lineChartSeries1.Append(seriesText1);
                    //lineChartSeries1.Append(chartShapeProperties1);
                    //lineChartSeries1.Append(marker1);
                    //lineChartSeries1.Append(dataLabels1);

                    ////lineChartSeries1.Append(categoryAxisData1);
                    ////lineChartSeries1.Append(values1);


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////

                    dc.DataLabels dataLabels2 = new dc.DataLabels();
                    dc.ShowLegendKey showLegendKey5 = new dc.ShowLegendKey() { Val = false };
                    dc.ShowValue showValue5 = new dc.ShowValue() { Val = false };
                    dc.ShowCategoryName showCategoryName5 = new dc.ShowCategoryName() { Val = false };
                    dc.ShowSeriesName showSeriesName5 = new dc.ShowSeriesName() { Val = false };
                    dc.ShowPercent showPercent5 = new dc.ShowPercent() { Val = false };
                    dc.ShowBubbleSize showBubbleSize5 = new dc.ShowBubbleSize() { Val = false };

                    dataLabels2.Append(showLegendKey5);
                    dataLabels2.Append(showValue5);
                    dataLabels2.Append(showCategoryName5);
                    dataLabels2.Append(showSeriesName5);
                    dataLabels2.Append(showPercent5);
                    dataLabels2.Append(showBubbleSize5);
                    dc.ShowMarker showMarker1 = new dc.ShowMarker() { Val = true };
                    dc.Smooth smooth2 = new dc.Smooth() { Val = false };
                    dc.AxisId axisId1 = new dc.AxisId() { Val = (UInt32Value)137464064U };
                    dc.AxisId axisId2 = new dc.AxisId() { Val = (UInt32Value)137471104U };

                    lineChart1.Append(grouping1);
                    lineChart1.Append(varyColors1);
                    //lineChart1.Append(lineChartSeries1);
                    lineChart1.Append(dataLabels2);
                    lineChart1.Append(showMarker1);
                    lineChart1.Append(smooth2);
                    lineChart1.Append(axisId1);
                    lineChart1.Append(axisId2);

                    dc.CategoryAxis categoryAxis1 = new dc.CategoryAxis();
                    dc.AxisId axisId3 = new dc.AxisId() { Val = (UInt32Value)137464064U };

                    dc.Scaling scaling1 = new dc.Scaling();
                    dc.Orientation orientation1 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                    scaling1.Append(orientation1);
                    dc.Delete delete1 = new dc.Delete() { Val = false };
                    dc.AxisPosition axisPosition1 = new dc.AxisPosition() { Val = dc.AxisPositionValues.Bottom };
                    dc.NumberingFormat numberingFormat1 = new dc.NumberingFormat() { FormatCode = "General", SourceLinked = true };
                    dc.MajorTickMark majorTickMark1 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                    dc.MinorTickMark minorTickMark1 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                    dc.TickLabelPosition tickLabelPosition1 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                    dc.ChartShapeProperties chartShapeProperties3 = new dc.ChartShapeProperties();

                    d.Outline outline3 = new d.Outline() { Width = 3175 };

                    d.SolidFill solidFill4 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex1 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill4.Append(rgbColorModelHex1);
                    d.PresetDash presetDash1 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                    outline3.Append(solidFill4);
                    outline3.Append(presetDash1);

                    chartShapeProperties3.Append(outline3);

                    dc.TextProperties textProperties1 = new dc.TextProperties();
                    d.BodyProperties bodyProperties1 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                    d.ListStyle listStyle1 = new d.ListStyle();

                    d.Paragraph paragraph1 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties1 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties1 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };
                   
                    d.SolidFill solidFill5 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex2 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill5.Append(rgbColorModelHex2);
                    d.LatinFont latinFont1 = new d.LatinFont() { Typeface = "Verdana" };
                    d.EastAsianFont eastAsianFont1 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont1 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                    defaultRunProperties1.Append(solidFill5);
                    defaultRunProperties1.Append(latinFont1);
                    defaultRunProperties1.Append(eastAsianFont1);
                    defaultRunProperties1.Append(complexScriptFont1);

                    paragraphProperties1.Append(defaultRunProperties1);
                    d.EndParagraphRunProperties endParagraphRunProperties1 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph1.Append(paragraphProperties1);
                    paragraph1.Append(endParagraphRunProperties1);

                    textProperties1.Append(bodyProperties1);
                    textProperties1.Append(listStyle1);
                    textProperties1.Append(paragraph1);
                    dc.CrossingAxis crossingAxis1 = new dc.CrossingAxis() { Val = (UInt32Value)137471104U };
                    dc.Crosses crosses1 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                    dc.AutoLabeled autoLabeled1 = new dc.AutoLabeled() { Val = true };
                    dc.LabelAlignment labelAlignment1 = new dc.LabelAlignment() { Val = dc.LabelAlignmentValues.Center };
                    dc.LabelOffset labelOffset1 = new dc.LabelOffset() { Val = (UInt16Value)100U };
                    dc.TickLabelSkip tickLabelSkip1 = new dc.TickLabelSkip() { Val = 1 };
                    dc.TickMarkSkip tickMarkSkip1 = new dc.TickMarkSkip() { Val = 1 };
                    dc.NoMultiLevelLabels noMultiLevelLabels1 = new dc.NoMultiLevelLabels() { Val = false };

                    categoryAxis1.Append(axisId3);
                    categoryAxis1.Append(scaling1);
                    categoryAxis1.Append(delete1);
                    categoryAxis1.Append(axisPosition1);
                    categoryAxis1.Append(numberingFormat1);
                    categoryAxis1.Append(majorTickMark1);
                    categoryAxis1.Append(minorTickMark1);
                    categoryAxis1.Append(tickLabelPosition1);
                    categoryAxis1.Append(chartShapeProperties3);
                    categoryAxis1.Append(textProperties1);
                    categoryAxis1.Append(crossingAxis1);
                    categoryAxis1.Append(crosses1);
                    categoryAxis1.Append(autoLabeled1);
                    categoryAxis1.Append(labelAlignment1);
                    categoryAxis1.Append(labelOffset1);
                    categoryAxis1.Append(tickLabelSkip1);
                    categoryAxis1.Append(tickMarkSkip1);
                    categoryAxis1.Append(noMultiLevelLabels1);

                    dc.ValueAxis valueAxis1 = new dc.ValueAxis();
                    dc.AxisId axisId4 = new dc.AxisId() { Val = (UInt32Value)137471104U };

                    dc.Scaling scaling2 = new dc.Scaling();
                    dc.Orientation orientation2 = new dc.Orientation() { Val = dc.OrientationValues.MinMax };

                    scaling2.Append(orientation2);
                    dc.Delete delete2 = new dc.Delete() { Val = false };
                    dc.AxisPosition axisPosition2 = new dc.AxisPosition() { Val = dc.AxisPositionValues.Left };

                    //Title Y axis

                    dc.Title title1 = new dc.Title();

                    dc.ChartText chartText1 = new dc.ChartText();

                    dc.RichText richText1 = new dc.RichText();
                    d.BodyProperties bodyProperties2 = new d.BodyProperties();
                    d.ListStyle listStyle2 = new d.ListStyle();

                    d.Paragraph paragraph2 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties2 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties2 = new d.DefaultRunProperties() { FontSize = 1000, Bold = true };
                    d.LatinFont latinFont2 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                    d.ComplexScriptFont complexScriptFont2 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                    defaultRunProperties2.Append(latinFont2);
                    defaultRunProperties2.Append(complexScriptFont2);

                    paragraphProperties2.Append(defaultRunProperties2);

                    d.Run run1 = new d.Run();

                    d.RunProperties runProperties1 = new d.RunProperties() { Language = "en-US", FontSize = 1000, Bold = true };
                    d.LatinFont latinFont3 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                    d.ComplexScriptFont complexScriptFont3 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                    runProperties1.Append(latinFont3);
                    runProperties1.Append(complexScriptFont3);
                    d.Text text1 = new d.Text();
                    text1.Text = objChartArea.YaxisLabel1;

                    run1.Append(runProperties1);
                    run1.Append(text1);

                    paragraph2.Append(paragraphProperties2);
                    paragraph2.Append(run1);

                    richText1.Append(bodyProperties2);
                    richText1.Append(listStyle2);
                    richText1.Append(paragraph2);

                    chartText1.Append(richText1);

                    dc.Layout layout5 = new dc.Layout();

                    dc.ManualLayout manualLayout5 = new dc.ManualLayout();
                    dc.LeftMode leftMode2 = new dc.LeftMode() { Val = dc.LayoutModeValues.Edge };
                    dc.TopMode topMode2 = new dc.TopMode() { Val = dc.LayoutModeValues.Edge };
                    dc.Left left5 = new dc.Left() { Val = 1.9338422391857506E-2D };
                    dc.Top top5 = new dc.Top() { Val = 0.28488465257632267D };

                    manualLayout5.Append(leftMode2);
                    manualLayout5.Append(topMode2);
                    manualLayout5.Append(left5);
                    manualLayout5.Append(top5);

                    layout5.Append(manualLayout5);
                    dc.Overlay overlay1 = new dc.Overlay() { Val = false };

                    title1.Append(chartText1);
                    title1.Append(layout5);
                    title1.Append(overlay1);
                    dc.NumberingFormat numberingFormat2 = new dc.NumberingFormat() { FormatCode = "General", SourceLinked = true };
                    dc.MajorTickMark majorTickMark2 = new dc.MajorTickMark() { Val = dc.TickMarkValues.Outside };
                    dc.MinorTickMark minorTickMark2 = new dc.MinorTickMark() { Val = dc.TickMarkValues.None };
                    dc.TickLabelPosition tickLabelPosition2 = new dc.TickLabelPosition() { Val = dc.TickLabelPositionValues.NextTo };

                    dc.ChartShapeProperties chartShapeProperties4 = new dc.ChartShapeProperties();

                    d.Outline outline4 = new d.Outline() { Width = 3175 };

                    d.SolidFill solidFill6 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex3 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill6.Append(rgbColorModelHex3);
                    d.PresetDash presetDash2 = new d.PresetDash() { Val = d.PresetLineDashValues.Solid };

                    outline4.Append(solidFill6);
                    outline4.Append(presetDash2);

                    chartShapeProperties4.Append(outline4);

                    dc.TextProperties textProperties2 = new dc.TextProperties();
                    d.BodyProperties bodyProperties3 = new d.BodyProperties() { Rotation = 0, Vertical = d.TextVerticalValues.Horizontal };
                    d.ListStyle listStyle3 = new d.ListStyle();

                    d.Paragraph paragraph3 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties3 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties3 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };

                    d.SolidFill solidFill7 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex4 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill7.Append(rgbColorModelHex4);
                    d.LatinFont latinFont4 = new d.LatinFont() { Typeface = "Verdana" };
                    d.EastAsianFont eastAsianFont2 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont4 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                    defaultRunProperties3.Append(solidFill7);
                    defaultRunProperties3.Append(latinFont4);
                    defaultRunProperties3.Append(eastAsianFont2);
                    defaultRunProperties3.Append(complexScriptFont4);

                    paragraphProperties3.Append(defaultRunProperties3);
                    d.EndParagraphRunProperties endParagraphRunProperties2 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph3.Append(paragraphProperties3);
                    paragraph3.Append(endParagraphRunProperties2);

                    textProperties2.Append(bodyProperties3);
                    textProperties2.Append(listStyle3);
                    textProperties2.Append(paragraph3);
                    dc.CrossingAxis crossingAxis2 = new dc.CrossingAxis() { Val = (UInt32Value)137464064U };
                    dc.Crosses crosses2 = new dc.Crosses() { Val = dc.CrossesValues.AutoZero };
                    dc.CrossBetween crossBetween1 = new dc.CrossBetween() { Val = dc.CrossBetweenValues.Between };

                    valueAxis1.Append(axisId4);
                    valueAxis1.Append(scaling2);
                    valueAxis1.Append(delete2);
                    valueAxis1.Append(axisPosition2);
                    valueAxis1.Append(title1);
                    valueAxis1.Append(numberingFormat2);
                    valueAxis1.Append(majorTickMark2);
                    valueAxis1.Append(minorTickMark2);
                    valueAxis1.Append(tickLabelPosition2);
                    valueAxis1.Append(chartShapeProperties4);
                    valueAxis1.Append(textProperties2);
                    valueAxis1.Append(crossingAxis2);
                    valueAxis1.Append(crosses2);
                    valueAxis1.Append(crossBetween1);

                    //End: Tile for Y axis

                    dc.ShapeProperties shapeProperties1 = new dc.ShapeProperties();
                    d.NoFill noFill1 = new d.NoFill();

                    d.Outline outline5 = new d.Outline() { Width = 28400 };
                    d.NoFill noFill2 = new d.NoFill();

                    outline5.Append(noFill2);

                    shapeProperties1.Append(noFill1);
                    shapeProperties1.Append(outline5);

                    plotArea1.Append(layout1);
                    plotArea1.Append(lineChart1);
                    plotArea1.Append(categoryAxis1);
                    plotArea1.Append(valueAxis1);
                    plotArea1.Append(shapeProperties1);
                    dc.PlotVisibleOnly plotVisibleOnly1 = new dc.PlotVisibleOnly() { Val = true };
                    dc.DisplayBlanksAs displayBlanksAs1 = new dc.DisplayBlanksAs() { Val = dc.DisplayBlanksAsValues.Gap };
                    dc.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new dc.ShowDataLabelsOverMaximum() { Val = false };

                    #region Legend

                    dc.Legend legend1 = new dc.Legend();
                    dc.LegendPosition legendPosition1 = new dc.LegendPosition();
                    legendPosition1.Val = dc.LegendPositionValues.Bottom;

                    dc.Overlay overlay3 = new dc.Overlay() { Val = true };

                    dc.ChartShapeProperties chartShapeProperties9 = new dc.ChartShapeProperties();
                    d.NoFill noFill7 = new d.NoFill();

                    d.Outline outline10 = new d.Outline() { Width = 25400 };
                    d.NoFill noFill8 = new d.NoFill();

                    outline10.Append(noFill8);

                    chartShapeProperties9.Append(noFill7);
                    chartShapeProperties9.Append(outline10);

                    dc.TextProperties textProperties3 = new dc.TextProperties();
                    d.BodyProperties bodyProperties5 = new d.BodyProperties();
                    d.ListStyle listStyle5 = new d.ListStyle();

                    d.Paragraph paragraph5 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties5 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties5 = new d.DefaultRunProperties() { FontSize = 900, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };

                    d.SolidFill solidFill12 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex7 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill12.Append(rgbColorModelHex7);
                    d.LatinFont latinFont9 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                    d.EastAsianFont eastAsianFont3 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont9 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                    defaultRunProperties5.Append(solidFill12);
                    defaultRunProperties5.Append(latinFont9);
                    defaultRunProperties5.Append(eastAsianFont3);
                    defaultRunProperties5.Append(complexScriptFont9);

                    paragraphProperties5.Append(defaultRunProperties5);
                    d.EndParagraphRunProperties endParagraphRunProperties4 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph5.Append(paragraphProperties5);
                    paragraph5.Append(endParagraphRunProperties4);

                    textProperties3.Append(bodyProperties5);
                    textProperties3.Append(listStyle5);
                    textProperties3.Append(paragraph5);

                    legend1.Append(legendPosition1);//Necessary
                    legend1.Append(overlay3);//Necessary
                    legend1.Append(chartShapeProperties9);
                    legend1.Append(textProperties3);

                    #endregion

                    chart1.Append(legend1);
                    chart1.Append(autoTitleDeleted1);
                    chart1.Append(plotArea1);
                    chart1.Append(plotVisibleOnly1);
                    chart1.Append(displayBlanksAs1);
                    chart1.Append(showDataLabelsOverMaximum1);

                    dc.ChartShapeProperties chartShapeProperties5 = new dc.ChartShapeProperties();
                    d.NoFill noFill3 = new d.NoFill();

                    d.Outline outline6 = new d.Outline() { Width = 6350 };
                    d.NoFill noFill4 = new d.NoFill();

                    outline6.Append(noFill4);

                    chartShapeProperties5.Append(noFill3);
                    chartShapeProperties5.Append(outline6);

                    dc.TextProperties textProperties4 = new dc.TextProperties();
                    d.BodyProperties bodyProperties4 = new d.BodyProperties();
                    d.ListStyle listStyle4 = new d.ListStyle();

                    d.Paragraph paragraph4 = new d.Paragraph();

                    d.ParagraphProperties paragraphProperties4 = new d.ParagraphProperties();

                    d.DefaultRunProperties defaultRunProperties4 = new d.DefaultRunProperties() { FontSize = 925, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };

                    d.SolidFill solidFill8 = new d.SolidFill();
                    d.RgbColorModelHex rgbColorModelHex5 = new d.RgbColorModelHex() { Val = "000000" };

                    solidFill8.Append(rgbColorModelHex5);
                    d.LatinFont latinFont5 = new d.LatinFont() { Typeface = "Verdana" };
                    d.EastAsianFont eastAsianFont4 = new d.EastAsianFont() { Typeface = "Verdana" };
                    d.ComplexScriptFont complexScriptFont5 = new d.ComplexScriptFont() { Typeface = "Verdana" };

                    defaultRunProperties4.Append(solidFill8);
                    defaultRunProperties4.Append(latinFont5);
                    defaultRunProperties4.Append(eastAsianFont4);
                    defaultRunProperties4.Append(complexScriptFont5);

                    paragraphProperties4.Append(defaultRunProperties4);
                    d.EndParagraphRunProperties endParagraphRunProperties3 = new d.EndParagraphRunProperties() { Language = "en-US" };

                    paragraph4.Append(paragraphProperties4);
                    paragraph4.Append(endParagraphRunProperties3);

                    textProperties4.Append(bodyProperties4);
                    textProperties4.Append(listStyle4);
                    textProperties4.Append(paragraph4);

                    dc.ExternalData externalData1 = new dc.ExternalData() { Id = "rId1" };
                    dc.AutoUpdate autoUpdate1 = new dc.AutoUpdate() { Val = false };

                    externalData1.Append(autoUpdate1);

                    chartSpace1.Append(date19041);
                    chartSpace1.Append(editingLanguage1);
                    chartSpace1.Append(roundedCorners1);
                    chartSpace1.Append(alternateContent1);
                    chartSpace1.Append(chart1);
                    chartSpace1.Append(chartShapeProperties5);
                    chartSpace1.Append(textProperties4);
                    chartSpace1.Append(externalData1);

                    chartPart.ChartSpace = chartSpace1;
                    // CreateExcelFile(chartPart, lstChartSubArea, excelSourceId);

                    //Generate content of the MainDocumentPart
                    GeneratePartContent(mainPart, chartPartRId);

                    //save this part
                    objWordDocx.MainDocumentPart.Document.Save();
                    //save and close the document
                    objWordDocx.Close();
                }
            }
            catch (Exception ex)
            {
                if (objWordDocx != null)
                {
                    objWordDocx.Close();
                }

                System.Web.HttpContext.Current.Session["Error"] = "Error occured while creating line chart || " + ex.Message;
                throw ex;
            }
        }

        public void GenerateWordChart_PieOnly(ChartArea objChartArea, string sourceFile, string chartPartRId, string excelSourceId)
        {
            WordprocessingDocument objWordDocx = null;

            try
            {
                if (objChartArea != null && !string.IsNullOrEmpty(sourceFile))
                {
                    string pieChartFormatCode = string.Empty;
                   
                    List<ChartSeries> lstChartSubArea = objChartArea.ChartSubAreaList;
                    List<ChartData> chartList = null;

                    //declare and open a Word document object
                    objWordDocx = WordprocessingDocument.Open(sourceFile, true);

                    // Get MainDocumentPart of Document
                    MainDocumentPart mainPart = objWordDocx.MainDocumentPart;

                    // Create ChartPart object in Word Document
                    ChartPart chartPart = mainPart.AddNewPart<ChartPart>(chartPartRId);

                    dc.ChartSpace chartSpace1 = new dc.ChartSpace();
                    chartSpace1.AddNamespaceDeclaration("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
                    chartSpace1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    chartSpace1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                    dc.Date1904 date19041 = new dc.Date1904() { Val = false };
                    dc.EditingLanguage editingLanguage1 = new dc.EditingLanguage() { Val = "en-US" };
                    dc.RoundedCorners roundedCorners1 = new dc.RoundedCorners() { Val = false };

                    AlternateContent alternateContent1 = new AlternateContent();
                    alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

                    AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice() { Requires = "c14" };
                    alternateContentChoice1.AddNamespaceDeclaration("c14", "http://schemas.microsoft.com/office/drawing/2007/8/2/chart");
                    C14.Style style1 = new C14.Style() { Val = 102 };

                    alternateContentChoice1.Append(style1);

                    AlternateContentFallback alternateContentFallback1 = new AlternateContentFallback();
                    dc.Style style2 = new dc.Style() { Val = 2 };

                    alternateContentFallback1.Append(style2);

                    alternateContent1.Append(alternateContentChoice1);
                    alternateContent1.Append(alternateContentFallback1);

                    dc.Chart chart1 = new dc.Chart();

                    dc.Title title1 = new dc.Title();
                    dc.Overlay overlay1 = new dc.Overlay() { Val = false };

                    title1.Append(overlay1);
                    dc.AutoTitleDeleted autoTitleDeleted1 = new dc.AutoTitleDeleted() { Val = true };

                    dc.PlotArea plotArea1 = new dc.PlotArea();
                    dc.Layout layout1 = new dc.Layout();

                    dc.PieChart pieChart1 = new dc.PieChart();
                    dc.VaryColors varyColors1 = new dc.VaryColors() { Val = true };

                    //uint c = 0;

                    uint xAxisLabelCount = default(uint);

                    //foreach (ChartSeries csa in lstChartSubArea)
                    //{
                    if (lstChartSubArea.Count > 0)
                    {
                        chartList = lstChartSubArea[0].ChartDataList;

                        if (xAxisLabelCount < chartList.Count)
                        {
                            xAxisLabelCount = (uint)chartList.Count;
                        }

                        dc.PieChartSeries pieChartSeries1 = new dc.PieChartSeries();
                        dc.Index index1 = new dc.Index() { Val = (UInt32Value)0U };
                        dc.Order order1 = new dc.Order() { Val = (UInt32Value)0U };

                        dc.SeriesText seriesText1 = new dc.SeriesText();

                        dc.StringReference stringReference1 = new dc.StringReference();
                        dc.Formula formula1 = new dc.Formula();
                        formula1.Text = "Sheet1!$A$22";

                        dc.StringCache stringCache1 = new dc.StringCache();
                        dc.PointCount pointCount1 = new dc.PointCount() { Val = (UInt32Value)1U };

                        dc.StringPoint stringPoint1 = new dc.StringPoint() { Index = (UInt32Value)0U };
                        dc.NumericValue numericValue1 = new dc.NumericValue();
                        numericValue1.Text = lstChartSubArea[0].SeriesTitle; // "Net Revenue"; //csa.SeriesTitle;

                        stringPoint1.Append(numericValue1);

                        stringCache1.Append(pointCount1);
                        stringCache1.Append(stringPoint1);

                        stringReference1.Append(formula1);
                        stringReference1.Append(stringCache1);

                        seriesText1.Append(stringReference1);

                        dc.DataLabels dataLabels1 = new dc.DataLabels();

                        #region Data lables commented
                        //dc.DataLabel dataLabel1 = new dc.DataLabel();
                        //dc.Index index2 = new dc.Index() { Val = (UInt32Value)0U };

                        //dc.Layout layout2 = new dc.Layout();

                        //dc.ManualLayout manualLayout1 = new dc.ManualLayout();
                        //dc.Left left1 = new dc.Left() { Val = 6.9206692913385826E-2D };
                        //dc.Top top1 = new dc.Top() { Val = -4.5330271216097985E-2D };

                        //manualLayout1.Append(left1);
                        //manualLayout1.Append(top1);

                        //layout2.Append(manualLayout1);
                        //dc.ShowLegendKey showLegendKey1 = new dc.ShowLegendKey() { Val = false };
                        //dc.ShowValue showValue1 = new dc.ShowValue() { Val = true };
                        //dc.ShowCategoryName showCategoryName1 = new dc.ShowCategoryName() { Val = false };
                        //dc.ShowSeriesName showSeriesName1 = new dc.ShowSeriesName() { Val = false };
                        //dc.ShowPercent showPercent1 = new dc.ShowPercent() { Val = false };
                        //dc.ShowBubbleSize showBubbleSize1 = new dc.ShowBubbleSize() { Val = false };

                        //dataLabel1.Append(index2);
                        //dataLabel1.Append(layout2);
                        //dataLabel1.Append(showLegendKey1);
                        //dataLabel1.Append(showValue1);
                        //dataLabel1.Append(showCategoryName1);
                        //dataLabel1.Append(showSeriesName1);
                        //dataLabel1.Append(showPercent1);
                        //dataLabel1.Append(showBubbleSize1);

                        //dc.DataLabel dataLabel2 = new dc.DataLabel();
                        //dc.Index index3 = new dc.Index() { Val = (UInt32Value)1U };

                        //dc.Layout layout3 = new dc.Layout();

                        //dc.ManualLayout manualLayout2 = new dc.ManualLayout();
                        //dc.Left left2 = new dc.Left() { Val = 8.6035214348206476E-2D };
                        //dc.Top top2 = new dc.Top() { Val = -0.17671916010498687D };

                        //manualLayout2.Append(left2);
                        //manualLayout2.Append(top2);

                        //layout3.Append(manualLayout2);
                        //dc.ShowLegendKey showLegendKey2 = new dc.ShowLegendKey() { Val = false };
                        //dc.ShowValue showValue2 = new dc.ShowValue() { Val = true };
                        //dc.ShowCategoryName showCategoryName2 = new dc.ShowCategoryName() { Val = false };
                        //dc.ShowSeriesName showSeriesName2 = new dc.ShowSeriesName() { Val = false };
                        //dc.ShowPercent showPercent2 = new dc.ShowPercent() { Val = false };
                        //dc.ShowBubbleSize showBubbleSize2 = new dc.ShowBubbleSize() { Val = false };

                        //dataLabel2.Append(index3);
                        //dataLabel2.Append(layout3);
                        //dataLabel2.Append(showLegendKey2);
                        //dataLabel2.Append(showValue2);
                        //dataLabel2.Append(showCategoryName2);
                        //dataLabel2.Append(showSeriesName2);
                        //dataLabel2.Append(showPercent2);
                        //dataLabel2.Append(showBubbleSize2);

                        //dc.DataLabel dataLabel3 = new dc.DataLabel();
                        //dc.Index index4 = new dc.Index() { Val = (UInt32Value)2U };

                        //dc.Layout layout4 = new dc.Layout();

                        //dc.ManualLayout manualLayout3 = new dc.ManualLayout();
                        //dc.Left left3 = new dc.Left() { Val = 3.8919728783902013E-2D };
                        //dc.Top top3 = new dc.Top() { Val = 1.4595727617381161E-2D };

                        //manualLayout3.Append(left3);
                        //manualLayout3.Append(top3);

                        //layout4.Append(manualLayout3);
                        //dc.ShowLegendKey showLegendKey3 = new dc.ShowLegendKey() { Val = false };
                        //dc.ShowValue showValue3 = new dc.ShowValue() { Val = true };
                        //dc.ShowCategoryName showCategoryName3 = new dc.ShowCategoryName() { Val = false };
                        //dc.ShowSeriesName showSeriesName3 = new dc.ShowSeriesName() { Val = false };
                        //dc.ShowPercent showPercent3 = new dc.ShowPercent() { Val = false };
                        //dc.ShowBubbleSize showBubbleSize3 = new dc.ShowBubbleSize() { Val = false };

                        //dataLabel3.Append(index4);
                        //dataLabel3.Append(layout4);
                        //dataLabel3.Append(showLegendKey3);
                        //dataLabel3.Append(showValue3);
                        //dataLabel3.Append(showCategoryName3);
                        //dataLabel3.Append(showSeriesName3);
                        //dataLabel3.Append(showPercent3);
                        //dataLabel3.Append(showBubbleSize3);

                        //dc.DataLabel dataLabel4 = new dc.DataLabel();
                        //dc.Index index5 = new dc.Index() { Val = (UInt32Value)3U };

                        //dc.Layout layout5 = new dc.Layout();

                        //dc.ManualLayout manualLayout4 = new dc.ManualLayout();
                        //dc.Left left4 = new dc.Left() { Val = -0.10317891513560805D };
                        //dc.Top top4 = new dc.Top() { Val = -0.16322506561679789D };

                        //manualLayout4.Append(left4);
                        //manualLayout4.Append(top4);

                        //layout5.Append(manualLayout4);
                        //dc.ShowLegendKey showLegendKey4 = new dc.ShowLegendKey() { Val = false };
                        //dc.ShowValue showValue4 = new dc.ShowValue() { Val = true };
                        //dc.ShowCategoryName showCategoryName4 = new dc.ShowCategoryName() { Val = false };
                        //dc.ShowSeriesName showSeriesName4 = new dc.ShowSeriesName() { Val = false };
                        //dc.ShowPercent showPercent4 = new dc.ShowPercent() { Val = false };
                        //dc.ShowBubbleSize showBubbleSize4 = new dc.ShowBubbleSize() { Val = false };

                        //dataLabel4.Append(index5);
                        //dataLabel4.Append(layout5);
                        //dataLabel4.Append(showLegendKey4);
                        //dataLabel4.Append(showValue4);
                        //dataLabel4.Append(showCategoryName4);
                        //dataLabel4.Append(showSeriesName4);
                        //dataLabel4.Append(showPercent4);
                        //dataLabel4.Append(showBubbleSize4);

                        //dc.DataLabel dataLabel5 = new dc.DataLabel();
                        //dc.Index index6 = new dc.Index() { Val = (UInt32Value)4U };

                        //dc.Layout layout6 = new dc.Layout();

                        //dc.ManualLayout manualLayout5 = new dc.ManualLayout();
                        //dc.Left left5 = new dc.Left() { Val = -9.8690288713910765E-2D };
                        //dc.Top top5 = new dc.Top() { Val = -8.1524496937882759E-2D };

                        //manualLayout5.Append(left5);
                        //manualLayout5.Append(top5);

                        //layout6.Append(manualLayout5);
                        //dc.ShowLegendKey showLegendKey5 = new dc.ShowLegendKey() { Val = false };
                        //dc.ShowValue showValue5 = new dc.ShowValue() { Val = true };
                        //dc.ShowCategoryName showCategoryName5 = new dc.ShowCategoryName() { Val = false };
                        //dc.ShowSeriesName showSeriesName5 = new dc.ShowSeriesName() { Val = false };
                        //dc.ShowPercent showPercent5 = new dc.ShowPercent() { Val = false };
                        //dc.ShowBubbleSize showBubbleSize5 = new dc.ShowBubbleSize() { Val = false };

                        //dataLabel5.Append(index6);
                        //dataLabel5.Append(layout6);
                        //dataLabel5.Append(showLegendKey5);
                        //dataLabel5.Append(showValue5);
                        //dataLabel5.Append(showCategoryName5);
                        //dataLabel5.Append(showSeriesName5);
                        //dataLabel5.Append(showPercent5);
                        //dataLabel5.Append(showBubbleSize5);
                        #endregion

                        dc.ShowLegendKey showLegendKey1 = new dc.ShowLegendKey() { Val = false };
                        dc.ShowValue showValue1 = new dc.ShowValue() { Val = true };
                        dc.ShowCategoryName showCategoryName1 = new dc.ShowCategoryName() { Val = false };
                        dc.ShowSeriesName showSeriesName1 = new dc.ShowSeriesName() { Val = false };
                        dc.ShowPercent showPercent1 = new dc.ShowPercent() { Val = false };
                        dc.ShowBubbleSize showBubbleSize1 = new dc.ShowBubbleSize() { Val = false };
                        dc.ShowLeaderLines showLeaderLines1 = new dc.ShowLeaderLines() { Val = true };

                        dataLabels1.Append(showLegendKey1);
                        dataLabels1.Append(showValue1);
                        dataLabels1.Append(showCategoryName1);
                        dataLabels1.Append(showSeriesName1);
                        dataLabels1.Append(showPercent1);
                        dataLabels1.Append(showBubbleSize1);
                        dataLabels1.Append(showLeaderLines1);

                        dc.CategoryAxisData categoryAxisData1 = new dc.CategoryAxisData();

                        dc.NumberReference numberReference1 = new dc.NumberReference();
                        dc.Formula formula2 = new dc.Formula();
                        formula2.Text = "Sheet1!$B$21:$F$21";

                        dc.NumberingCache numberingCache1 = new dc.NumberingCache();
                        dc.FormatCode formatCode1 = new dc.FormatCode();
                        formatCode1.Text = "General";
                        dc.PointCount pointCount2 = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };

                        uint i = 0;

                        // Fill data for chart
                        foreach (var item in chartList)
                        {
                            dc.NumericPoint numericPoint1 = new dc.NumericPoint() { Index = new UInt32Value(i) };
                            dc.NumericValue numericValues1 = new dc.NumericValue();
                            numericValues1.Text = item.XaxisLabel;

                            numericPoint1.Append(numericValues1);
                            numberingCache1.Append(numericPoint1);

                            // Add data labeles for each series
                            dc.DataLabel dataLabel1 = new dc.DataLabel();
                            dc.Index index2 = new dc.Index() { Val = new UInt32Value(i) };

                            dc.Layout layout2 = new dc.Layout();

                            dc.ManualLayout manualLayout1 = new dc.ManualLayout();
                            dc.Left left1 = new dc.Left() { Val = 0 };
                            dc.Top top1 = new dc.Top() { Val = 0 };
                            //dc.Left left1 = new dc.Left() { Val = 6.9206692913385826E-2D };
                            // dc.Top top1 = new dc.Top() { Val = -4.5330271216097985E-2D };

                            manualLayout1.Append(left1);
                            manualLayout1.Append(top1);

                            layout2.Append(manualLayout1);
                            dc.ShowLegendKey showLegendKey2 = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue2 = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName2 = new dc.ShowCategoryName() { Val = false };
                            dc.ShowSeriesName showSeriesName2 = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent2 = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize2 = new dc.ShowBubbleSize() { Val = false };

                            dataLabel1.Append(index2);
                            dataLabel1.Append(layout2);
                            dataLabel1.Append(showLegendKey2);
                            dataLabel1.Append(showValue2);
                            dataLabel1.Append(showCategoryName2);
                            dataLabel1.Append(showSeriesName2);
                            dataLabel1.Append(showPercent2);
                            dataLabel1.Append(showBubbleSize2);

                            dataLabels1.Append(dataLabel1);
                            i++;
                        }

                        numberingCache1.Append(formatCode1);
                        numberingCache1.Append(pointCount2);
                        numberReference1.Append(formula2);
                        numberReference1.Append(numberingCache1);


                        dc.Values values1 = new dc.Values();

                        dc.NumberReference numberReference2 = new dc.NumberReference();
                        dc.Formula formula3 = new dc.Formula();
                        formula3.Text = "Sheet1!$B$22:$F$22";

                        dc.NumberingCache numberingCache2 = new dc.NumberingCache();
                        i = 0;

                        // Fill data for chart
                        foreach (var item in chartList)
                        {
                            dc.NumericPoint numericPoint2 = new dc.NumericPoint() { Index = new UInt32Value(i) };
                            dc.NumericValue numericValue2 = new dc.NumericValue();
                            pieChartFormatCode = item.DataType;

                            if (pieChartFormatCode.Contains("percent"))
                            {
                                float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                numericValue2.Text = (yAxisValue / 100).ToString();
                            }
                            else
                            {
                                numericValue2.Text = item.YaxisValue;
                            }

                            numericPoint2.Append(numericValue2);
                            numberingCache2.Append(numericPoint2);

                            i++;
                        }

                        dc.FormatCode formatCode = new dc.FormatCode();

                        if (pieChartFormatCode.Contains("number"))
                        {
                            formatCode.Text = "#,#0.0"; //"#,##0.0";
                        }
                        else if (pieChartFormatCode.Contains("percent"))
                        {
                            formatCode.Text = "0.00%";
                        }

                        dc.PointCount pointCountYaxis = new dc.PointCount() { Val = (UInt32Value)xAxisLabelCount };
                        numberingCache2.Append(formatCode);
                        numberingCache2.Append(pointCountYaxis);

                        numberReference2.Append(formula3);
                        numberReference2.Append(numberingCache2);

                        values1.Append(numberReference2);
                        categoryAxisData1.Append(numberReference1);

                        pieChartSeries1.Append(index1);
                        pieChartSeries1.Append(order1);
                        pieChartSeries1.Append(seriesText1);
                        pieChartSeries1.Append(dataLabels1);
                        pieChartSeries1.Append(categoryAxisData1);
                        pieChartSeries1.Append(values1);
                        pieChart1.Append(pieChartSeries1);
                    }
                    //}

                    dc.DataLabels dataLabels2 = new dc.DataLabels();
                    dc.ShowLegendKey showLegendKey7 = new dc.ShowLegendKey() { Val = false };
                    dc.ShowValue showValue7 = new dc.ShowValue() { Val = false };
                    dc.ShowCategoryName showCategoryName7 = new dc.ShowCategoryName() { Val = false };
                    dc.ShowSeriesName showSeriesName7 = new dc.ShowSeriesName() { Val = false };
                    dc.ShowPercent showPercent7 = new dc.ShowPercent() { Val = false };
                    dc.ShowBubbleSize showBubbleSize7 = new dc.ShowBubbleSize() { Val = false };
                    dc.ShowLeaderLines showLeaderLines2 = new dc.ShowLeaderLines() { Val = true };

                    dataLabels2.Append(showLegendKey7);
                    dataLabels2.Append(showValue7);
                    dataLabels2.Append(showCategoryName7);
                    dataLabels2.Append(showSeriesName7);
                    dataLabels2.Append(showPercent7);
                    dataLabels2.Append(showBubbleSize7);
                    dataLabels2.Append(showLeaderLines2);
                    dc.FirstSliceAngle firstSliceAngle1 = new dc.FirstSliceAngle() { Val = (UInt16Value)0U };

                    pieChart1.Append(varyColors1);
                    pieChart1.Append(dataLabels2);
                    pieChart1.Append(firstSliceAngle1);

                    plotArea1.Append(layout1);
                    plotArea1.Append(pieChart1);

                    dc.Legend legend1 = new dc.Legend();
                    dc.LegendPosition legendPosition1 = new dc.LegendPosition() { Val = dc.LegendPositionValues.Right };
                    dc.Overlay overlay2 = new dc.Overlay() { Val = false };

                    legend1.Append(legendPosition1);
                    legend1.Append(overlay2);
                    dc.PlotVisibleOnly plotVisibleOnly1 = new dc.PlotVisibleOnly() { Val = true };
                    dc.DisplayBlanksAs displayBlanksAs1 = new dc.DisplayBlanksAs() { Val = dc.DisplayBlanksAsValues.Gap };
                    dc.ShowDataLabelsOverMaximum showDataLabelsOverMaximum1 = new dc.ShowDataLabelsOverMaximum() { Val = false };

                    chart1.Append(title1);
                    chart1.Append(autoTitleDeleted1);
                    chart1.Append(plotArea1);
                    chart1.Append(legend1);
                    chart1.Append(plotVisibleOnly1);
                    chart1.Append(displayBlanksAs1);
                    chart1.Append(showDataLabelsOverMaximum1);

                    chartSpace1.Append(date19041);
                    chartSpace1.Append(editingLanguage1);
                    chartSpace1.Append(roundedCorners1);
                    chartSpace1.Append(alternateContent1);
                    chartSpace1.Append(chart1);

                    chartPart.ChartSpace = chartSpace1;
                    // CreateExcelFile(chartPart, lstChartSubArea, excelSourceId);

                    //Generate content of the MainDocumentPart
                    GeneratePartContent(mainPart, chartPartRId);

                    //save this part
                    objWordDocx.MainDocumentPart.Document.Save();
                    //save and close the document
                    objWordDocx.Close();
                }
            }
            catch (Exception ex)
            {
                ////logger.Error(ex);

                if (objWordDocx != null)
                {
                    objWordDocx.Close();
                }

                System.Web.HttpContext.Current.Session["Error"] = "Error occured while creating pie chart || " + ex.Message;
                throw ex;
            }
        }

        // Create Chart in Word document
        public void CreatePieChart(ChartArea objChartArea, string sourceFile, string chartPartRId, string excelSourceId)
        { 
            WordprocessingDocument objWordDocx = null;

            try
            {
                if (objChartArea != null && !string.IsNullOrEmpty(sourceFile))
                {
                    string pieChartFormatCode = string.Empty;
                    List<ChartSeries> lstChartSubArea = objChartArea.ChartSubAreaList;
                    List<ChartData> chartList = null;

                    //declare and open a Word document object
                    objWordDocx = WordprocessingDocument.Open(sourceFile, true);

                    // Get MainDocumentPart of Document
                    MainDocumentPart mainPart = objWordDocx.MainDocumentPart;

                    // Create ChartPart object in Word Document
                    ChartPart chartPart = mainPart.AddNewPart<ChartPart>(chartPartRId);

                    // the root element of chartPart 
                    dc.ChartSpace chartSpace = new dc.ChartSpace();
                    chartSpace.Append(new dc.EditingLanguage() { Val = "en-us" });

                    // Create Chart 
                    dc.Chart chart = new dc.Chart();
                    chart.Append(new dc.AutoTitleDeleted() { Val = true });

                    // Intiliazes a new instance of the PlotArea class
                    dc.PlotArea plotArea = new dc.PlotArea();
                    plotArea.Append(new dc.Layout());

                    // the type of Chart 
                    dc.PieChart pieChart = new dc.PieChart();
                    pieChart.Append(new dc.VaryColors() { Val = true });

                    uint count = default(uint);

                    if (lstChartSubArea.Count > 0)
                    {
                        chartList = lstChartSubArea[0].ChartDataList;
                        count = (uint)chartList.Count;

                        dc.PieChartSeries pieChartSers = new dc.PieChartSeries();
                        pieChartSers.Append(new dc.Index() { Val = 0U });
                        pieChartSers.Append(new dc.Order() { Val = 0U });
                        dc.SeriesText seriesText = new dc.SeriesText();
                        //seriesText.Append(new dc.NumericValue() { Text = "Series" }); 
                        seriesText.Append(new dc.NumericValue() { Text = lstChartSubArea[0].SeriesTitle });

                        uint rowcount = 0;
                        // uint count = UInt32.Parse(chartList.Count.ToString());
                        string endCell = (count + 1).ToString();
                        dc.ChartShapeProperties chartShapePros = new dc.ChartShapeProperties();

                        // Define cell for lable information
                        dc.CategoryAxisData cateAxisData = new dc.CategoryAxisData();
                        dc.StringReference stringRef = new dc.StringReference();
                        stringRef.Append(new dc.Formula() { Text = "Main!$A$2:$A$" + endCell });
                        dc.StringCache stringCache = new dc.StringCache();
                        stringCache.Append(new dc.PointCount() { Val = count });

                        // Define cells for value information
                        dc.Values values = new dc.Values();
                        dc.NumberReference numRef = new dc.NumberReference();
                        numRef.Append(new dc.Formula() { Text = "Main!$B$2:$B$" + endCell });

                        dc.NumberingCache numCache = new dc.NumberingCache();
                        numCache.Append(new dc.FormatCode() { Text = "General" });
                        numCache.Append(new dc.PointCount() { Val = count });

                        dc.DataLabels dataLabels1 = new dc.DataLabels();

                        // Fill data for chart
                        foreach (var item in chartList)
                        {
                            if (count == 0)
                            {
                               // chartShapePros.Append(new d.SolidFill(new d.SchemeColor() { Val = item.Color }));
                                pieChartSers.Append(chartShapePros);
                            }
                            else
                            {
                                dc.DataPoint dataPoint = new dc.DataPoint();
                                dataPoint.Append(new dc.Index() { Val = rowcount });
                                chartShapePros = new dc.ChartShapeProperties();
                              //  chartShapePros.Append(new d.SolidFill(new d.SchemeColor() { Val = item.Color }));
                                dataPoint.Append(chartShapePros);
                                pieChartSers.Append(dataPoint);
                            }

                            dc.StringPoint stringPoint = new dc.StringPoint() { Index = rowcount };
                            stringPoint.Append(new dc.NumericValue() { Text = item.XaxisLabel });
                            stringCache.Append(stringPoint);
                                                    
                            dc.NumericPoint numericPoint = new dc.NumericPoint() { Index = rowcount };
                            dc.NumericValue numericValue = new dc.NumericValue();
                            pieChartFormatCode = item.DataType;

                            if (pieChartFormatCode.Contains("percent"))
                            {
                                float yAxisValue = Convert.ToSingle(item.YaxisValue);
                                numericValue.Text = (yAxisValue / 100).ToString();
                            }
                            else
                            {
                                numericValue.Text = item.YaxisValue;
                            }

                            numericPoint.Append(numericValue);
                            numCache.Append(numericPoint);

                            #region Data Label

                            // Add data labeles for each series
                            dc.DataLabel dataLabel1 = new dc.DataLabel();
                            dc.Index index1 = new dc.Index() { Val = new UInt32Value(rowcount) };

                            dc.Layout layout1 = new dc.Layout();

                            dc.ManualLayout manualLayout1 = new dc.ManualLayout();
                            dc.Left left1 = new dc.Left() { Val = 0 };
                            dc.Top top1 = new dc.Top() { Val = 0 };
                            manualLayout1.Append(left1);
                            manualLayout1.Append(top1);

                            layout1.Append(manualLayout1);
                            dc.ShowLegendKey showLegendKey = new dc.ShowLegendKey() { Val = false };
                            dc.ShowValue showValue = new dc.ShowValue() { Val = true };
                            dc.ShowCategoryName showCategoryName = new dc.ShowCategoryName() { Val = true };
                            dc.ShowSeriesName showSeriesName = new dc.ShowSeriesName() { Val = false };
                            dc.ShowPercent showPercent = new dc.ShowPercent() { Val = false };
                            dc.ShowBubbleSize showBubbleSize = new dc.ShowBubbleSize() { Val = false };
                            dc.ShowLeaderLines showLeaderLines = new dc.ShowLeaderLines() { Val = false };

                            dataLabel1.Append(index1);
                            dataLabel1.Append(layout1);
                            dataLabel1.Append(showLegendKey);
                            dataLabel1.Append(showValue);
                            dataLabel1.Append(showCategoryName);
                            dataLabel1.Append(showSeriesName);
                            dataLabel1.Append(showPercent);
                            dataLabel1.Append(showBubbleSize);
                            dataLabel1.Append(showLeaderLines);

                            dataLabels1.Append(dataLabel1);

                            #endregion

                            rowcount++;
                        }

                        dc.FormatCode formatCode = new dc.FormatCode();

                        if (pieChartFormatCode.Contains("number"))
                        {
                            formatCode.Text = "#,#0.0"; //"#,##0.0";
                        }
                        else if (pieChartFormatCode.Contains("percent"))
                        {
                            formatCode.Text = "0.00%";
                        }

                        // Create c:cat and c:val element 
                        stringRef.Append(stringCache);
                        cateAxisData.Append(stringRef);

                        numCache.Append(formatCode);

                        numRef.Append(numCache);
                        values.Append(numRef);

                        // Append c:cat and c:val to the end of c:ser element
                        pieChartSers.Append(cateAxisData);
                        pieChartSers.Append(values);
                        pieChartSers.Append(dataLabels1);

                        // Append c:ser to the end of c:pieChart element
                        pieChart.Append(pieChartSers);

                        // Append c:pieChart to the end of s:plotArea element
                        plotArea.Append(pieChart);
                  
                        #region Legend

                        dc.Legend legend1 = new dc.Legend();
                        dc.LegendPosition legendPosition1 = new dc.LegendPosition();
                        legendPosition1.Val = dc.LegendPositionValues.Right;

                        dc.Overlay overlay3 = new dc.Overlay() { Val = true };

                        dc.ChartShapeProperties chartShapeProperties9 = new dc.ChartShapeProperties();
                        d.NoFill noFill7 = new d.NoFill();

                        d.Outline outline10 = new d.Outline() { Width = 25400 };
                        d.NoFill noFill8 = new d.NoFill();

                        outline10.Append(noFill8);

                        chartShapeProperties9.Append(noFill7);
                        chartShapeProperties9.Append(outline10);

                        dc.TextProperties textProperties3 = new dc.TextProperties();
                        d.BodyProperties bodyProperties5 = new d.BodyProperties();
                        d.ListStyle listStyle5 = new d.ListStyle();

                        d.Paragraph paragraph5 = new d.Paragraph();

                        d.ParagraphProperties paragraphProperties5 = new d.ParagraphProperties();

                        d.DefaultRunProperties defaultRunProperties5 = new d.DefaultRunProperties() { FontSize = 900, Bold = false, Italic = false, Underline = d.TextUnderlineValues.None, Strike = d.TextStrikeValues.NoStrike, Baseline = 0 };

                        d.SolidFill solidFill12 = new d.SolidFill();
                        d.RgbColorModelHex rgbColorModelHex7 = new d.RgbColorModelHex() { Val = "000000" };

                        solidFill12.Append(rgbColorModelHex7);
                        d.LatinFont latinFont9 = new d.LatinFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };
                        d.EastAsianFont eastAsianFont3 = new d.EastAsianFont() { Typeface = "Verdana" };
                        d.ComplexScriptFont complexScriptFont9 = new d.ComplexScriptFont() { Typeface = "Arial", Panose = "020B0604020202020204", PitchFamily = 34, CharacterSet = 0 };

                        defaultRunProperties5.Append(solidFill12);
                        defaultRunProperties5.Append(latinFont9);
                        defaultRunProperties5.Append(eastAsianFont3);
                        defaultRunProperties5.Append(complexScriptFont9);

                        paragraphProperties5.Append(defaultRunProperties5);
                        d.EndParagraphRunProperties endParagraphRunProperties4 = new d.EndParagraphRunProperties() { Language = "en-US" };

                        paragraph5.Append(paragraphProperties5);
                        paragraph5.Append(endParagraphRunProperties4);

                        textProperties3.Append(bodyProperties5);
                        textProperties3.Append(listStyle5);
                        textProperties3.Append(paragraph5);

                        legend1.Append(legendPosition1);//Necessary
                        legend1.Append(overlay3);//Necessary
                        legend1.Append(chartShapeProperties9);
                        legend1.Append(textProperties3);

                        #endregion
                                            
                        // Append c:plotArea and c:legend elements to the end of c:chart element
                        chart.Append(plotArea);
                        chart.Append(legend1);

                        // Append the c:chart element to the end of c:chartSpace element
                        chartSpace.Append(chart);

                        // Create c:spPr Elements and fill the child elements of it
                        chartShapePros = new dc.ChartShapeProperties();
                        d.Outline outline = new d.Outline();
                        outline.Append(new d.NoFill());
                        chartShapePros.Append(outline);

                        // Append c:spPr element to the end of c:chartSpace element
                        chartSpace.Append(chartShapePros);

                        chartPart.ChartSpace = chartSpace;
                       // CreateExcelFile(chartPart, lstChartSubArea, excelSourceId);

                        // Generate content of the MainDocumentPart
                        GeneratePartContent(mainPart, chartPartRId);

                        //save this part
                        objWordDocx.MainDocumentPart.Document.Save();
                        //save and close the document
                        objWordDocx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ////logger.Error(ex);

                if (objWordDocx != null)
                {
                    objWordDocx.Close();
                }

                System.Web.HttpContext.Current.Session["Error"] = "Error occured while creating pie chart || " + ex.Message;
                throw ex;
            }
        }

        // Generate content of the MainDocumentPart
        private void GeneratePartContent(MainDocumentPart mainPart, string chartPartRId, bool IsLabel = false)
        {
           // ////logger.Info("GeneratePartContent method call started in class ReportChart...(GenerateCompanyPerformanceChart() || GeneratePartContent())");

            try
            {
                // Create a new run that has an inline drawing object
                wp.Run runNetRevenueChart = new wp.Run();
                Drawing drawing = new Drawing();

                dw.Inline inline = new dw.Inline();
                if (IsLabel)
                {
                    inline.Append(new dw.Extent() { Cx = 1894310L, Cy = 1376575L });
                }
                else
                {
                    inline.Append(new dw.Extent() { Cx = 5864310L, Cy = 3876575L });
                }

                dw.DocProperties docPros = new dw.DocProperties() { Id = (UInt32Value)1U, Name = "Chart 1" };
                inline.Append(docPros);

                d.Graphic g = new d.Graphic();
                d.GraphicData graphicData = new d.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" };
                ChartReference chartReference = new ChartReference() { Id = chartPartRId };
                graphicData.Append(chartReference);
                g.Append(graphicData);
                inline.Append(g);
                drawing.Append(inline);
                runNetRevenueChart.Append(drawing);

                foreach (FieldCode objField in mainPart.Document.Descendants<FieldCode>())
                {
                    string strFieldName = string.Empty;

                    if (objField != null && objField.InnerText.Contains("MERGEFIELD"))
                    {
                        string[] strArray = objField.InnerText.Split(' ');

                        strFieldName = strArray[3];

                        if (!string.IsNullOrEmpty(strFieldName) && (strFieldName == chartPartRId))
                        {
                            wp.Paragraph paragraph = (objField.Parent as wp.Run).Parent as wp.Paragraph;
                            paragraph.RemoveAllChildren();
                            paragraph.Append(runNetRevenueChart);
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Session["Error"] = ex.Message;
                throw ex;
            }
        }

        internal static string GetFieldName(SimpleField pField)
        {
            var attr = pField.GetAttribute("instr", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            string strFieldname = string.Empty;
            string instruction = attr.Value;

            try
            {
                if (!string.IsNullOrEmpty(instruction))
                {
                    Match m = instructionRegEx.Match(instruction);
                    if (m.Success)
                    {
                        strFieldname = m.Groups["name"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            { 
                System.Web.HttpContext.Current.Session["Error"] = ex.Message;
                throw ex;
            }

            return strFieldname;
        }

        // Generates content of embeddedPackagePart1.
        private void GenerateEmbeddedPackagePart1Content(EmbeddedPackagePart embeddedPackagePart1)
        {
            System.IO.Stream data = GetBinaryDataStream(embeddedPackagePart1Data);
            embeddedPackagePart1.FeedData(data);
            data.Close();
        }

        #region Binary Data
        private string embeddedPackagePart1Data = "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";
      
        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

        #region IDisposable interface

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Protect from being called multiple times.
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Clean up all managed resources.
                if (this.document != null)
                {
                    this.document.Dispose();
                }
            }

            disposed = true;
        }

        #endregion
    }
}
