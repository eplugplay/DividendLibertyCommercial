using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DividendLiberty.Excel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;
using System.IO;

namespace DividendLiberty
{
    public static class PortfolioExcel
    {
        public static void GeneratePortfolioExcel()
        {
            DataTable dt = uti.SortDataTable(uti.GetXMLData(), "asc");
            string stocks = uti.GetStockSymbols(dt);
            string[] annualDiv = uti.SplitStockData(YahooFinance.GetValues(stocks, YahooFinance.GetCodes(YahooCodes.annualDividend), true));
            string[] yields = uti.SplitStockData(YahooFinance.GetValues(stocks, YahooFinance.GetCodes(YahooCodes.dividendYield), true));
            string[] companies = uti.SplitStockData(YahooFinance.GetValues(stocks, YahooFinance.GetCodes(YahooCodes.stockname), true));

            string savePath = uti.GetFilePath(FileTypes.excel);
            ExcelNPOIWriter excelObj = new ExcelNPOIWriter();
            excelObj.CreateWorksheet("My Dividends");
            DataTable dtFinal = ConvertExcelData(dt, yields, annualDiv, companies);
            excelObj.WriteData(dtFinal, "My Dividends", true);
            excelObj.createStyle("headers");
            excelObj.setFont("headers", true);
            for (int i = 0; i < dtFinal.Columns.Count; i++)
            {
                excelObj.getCell(0, i, "My Dividends").CellStyle = excelObj.getStyle("headers");
            }

            int count = dtFinal.Rows.Count;

            HSSFCell AvgYield = excelObj.getCell(count, 6, "My Dividends");
            AvgYield.SetCellType(CellType.FORMULA);
            AvgYield.CellFormula = string.Format("ROUND(SUM(F{0}:F{1})/{2}, 2)", 2, count, count - 1);

            //HSSFCell AnnualDiv = excelObj.getCell(count, 6, "My Dividends");
            //AnnualDiv.SetCellType(CellType.FORMULA);
            //AnnualDiv.CellFormula = string.Format("ROUND(SUM(G{0}:G{1})/{2}, 2)", 2, count, count - 1);

            HSSFCell MonthlyDiv = excelObj.getCell(count, 7, "My Dividends");
            MonthlyDiv.SetCellType(CellType.FORMULA);
            MonthlyDiv.CellFormula = string.Format("ROUND(SUM(H{0}:H{1}), 2)", 2, count);

            HSSFCell QuarterlyDiv = excelObj.getCell(count, 8, "My Dividends");
            QuarterlyDiv.SetCellType(CellType.FORMULA);
            QuarterlyDiv.CellFormula = string.Format("ROUND(SUM(I{0}:I{1}), 2)", 2, count);

            HSSFCell YearlyDiv = excelObj.getCell(count, 9, "My Dividends");
            YearlyDiv.SetCellType(CellType.FORMULA);
            YearlyDiv.CellFormula = string.Format("ROUND(SUM(J{0}:J{1}), 2)", 2, count);

            HSSFCell TotalCostBasis = excelObj.getCell(count, 10, "My Dividends");
            TotalCostBasis.SetCellType(CellType.FORMULA);
            TotalCostBasis.CellFormula = string.Format("SUM(K{0}:K{1})", 2, count);

            for (int i = 6; i < 11; i++)
            {
                excelObj.getCell(count, i, "My Dividends").CellStyle = excelObj.getStyle("headers");
            }

            AutoSizeColumns(excelObj, dtFinal, "My Dividends");
            excelObj.Save(savePath);
            excelObj.Dispose();
            System.Diagnostics.Process.Start(savePath);
        }


        public static DataTable ConvertExcelData(DataTable dt, string[] yields, string[] annualDiv, string[] company)
        {
            //decimal totalCost = 0;
            //decimal yearlyDividends = 0;
            DataTable dtFinal = GetFinalTables();
            string[] col = INIFileOptions.ReadINIKeyValues(KeyPartition.columnName);
            string[] visible = INIFileOptions.ReadINIKeyValues(KeyPartition.visible);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["active"].ToString() == "true")
                {
                    DataRow dr = dtFinal.NewRow();
                    decimal shares = Convert.ToDecimal(dt.Rows[i]["shares"]);
                    decimal cost = Convert.ToDecimal(dt.Rows[i]["cost"]);
                    decimal annDiv = annualDiv[i] == "N/A" ? 0 : Convert.ToDecimal(annualDiv[i]);
                    decimal yearlyDiv = Math.Round(shares * annDiv, 2);
                    INIFile ini = new INIFile(uti.GetFilePath(FileTypes.ini));
                    int visCnt = 0;
                    int cnt = 0;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = dt.Rows[i]["symbol"].ToString();
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = company[i].Length == 0 ? "" : company[i].ToString();
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = dt.Rows[i]["industry"].ToString();
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(shares);
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(cost);
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(annDiv);
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = yields[i] == "N/A" ? 0 : Convert.ToDouble(yields[i]);
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(Math.Round(yearlyDiv / 12, 2));
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(Math.Round(yearlyDiv / 4, 2));
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(yearlyDiv);
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Math.Round(Convert.ToDouble(shares * cost), 2);
                    }
                    cnt = 0;
                    //if (visible[cnt] == "true")
                    //{
                    //    yearlyDividends += Convert.ToDecimal(yearlyDiv);
                    //}
                    //totalCost += Math.Round(Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(dt.Rows[i]["cost"]), 2);
                    dtFinal.Rows.Add(dr);
                }
            }

            DataRow drEmpty = dtFinal.NewRow();
            drEmpty[col[6]] = 0;
            //drEmpty["Annual Dividend"] = 0;
            drEmpty[col[7]] = 0;
            drEmpty[col[8]] = 0;
            drEmpty[col[9]] = 0;
            drEmpty[col[10]] = 0;
            dtFinal.Rows.Add(drEmpty);
            return dtFinal;
        }

        public static DataTable GetFinalTables()
        {
            DataTable dtFinal = new DataTable();
            string[] col = INIFileOptions.ReadINIKeyValues(KeyPartition.columnName);
            string[] visible = INIFileOptions.ReadINIKeyValues(KeyPartition.visible);
            for (int i = 0; i < visible.Length; i++)
            {
                if (i <= 2)
                {
                    if (visible[i] == "true")
                    {
                        dtFinal.Columns.Add(col[i], typeof(string));
                    }
                }
                else
                {
                    if (visible[i] == "true")
                    {
                        dtFinal.Columns.Add(col[i], typeof(double));
                    }
                }
            }
          
            //dtFinal.Columns.Add(col[1], typeof(string));
            //dtFinal.Columns.Add(col[2], typeof(string));
            //dtFinal.Columns.Add(col[3], typeof(double));
            //dtFinal.Columns.Add(col[4], typeof(double));
            //dtFinal.Columns.Add(col[5], typeof(double));
            //dtFinal.Columns.Add(col[6], typeof(double));
            //dtFinal.Columns.Add(col[7], typeof(double));
            //dtFinal.Columns.Add(col[8], typeof(double));
            //dtFinal.Columns.Add(col[9], typeof(double));
            //dtFinal.Columns.Add(col[10], typeof(double));
            return dtFinal;
        }

        private static void AutoSizeColumns(Excel.ExcelNPOIWriter exObj, DataTable tbl, string sheetName)
        {
            for (int i = 0; i <= tbl.Columns.Count; i++)
            {
                exObj.AutoSizeColumn(i, sheetName);
            }
        }
    }
}
