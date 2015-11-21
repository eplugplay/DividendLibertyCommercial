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
using System.Collections;

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
            List<int> lstForumaCol = new List<int>();
            int totalColCount = 0;
            DataTable dtFinal = ConvertExcelData(dt, yields, annualDiv, companies, out lstForumaCol, out totalColCount);
            excelObj.WriteData(dtFinal, "My Dividends", true);
            excelObj.createStyle("headers");
            excelObj.setFont("headers", true);
            for (int i = 0; i < dtFinal.Columns.Count; i++)
            {
                excelObj.getCell(0, i, "My Dividends").CellStyle = excelObj.getStyle("headers");
            }

            int count = dtFinal.Rows.Count;
            for (int a = 0; a < lstForumaCol.Count; a++)
            {
                if (lstForumaCol[a] == 6)
                {
                    HSSFCell AvgYield = excelObj.getCell(count, a, "My Dividends");
                    AvgYield.SetCellType(CellType.FORMULA);
                    AvgYield.CellFormula = string.Format("ROUND(SUM({0}{1}:{0}{2})/{3}, 2)", uti.GetExcelColLetter(a), 2, count, count - 1);
                }

                //HSSFCell AnnualDiv = excelObj.getCell(count, 6, "My Dividends");
                //AnnualDiv.SetCellType(CellType.FORMULA);
                //AnnualDiv.CellFormula = string.Format("ROUND(SUM(G{0}:G{1})/{2}, 2)", 2, count, count - 1);

                if (lstForumaCol[a] == 7)
                {
                    HSSFCell MonthlyDiv = excelObj.getCell(count, a, "My Dividends");
                    MonthlyDiv.SetCellType(CellType.FORMULA);
                    MonthlyDiv.CellFormula = string.Format("ROUND(SUM({0}{1}:{0}{2}), 2)", uti.GetExcelColLetter(a), 2, count);
                }

                if (lstForumaCol[a] == 8)
                {
                    HSSFCell QuarterlyDiv = excelObj.getCell(count, a, "My Dividends");
                    QuarterlyDiv.SetCellType(CellType.FORMULA);
                    QuarterlyDiv.CellFormula = string.Format("ROUND(SUM({0}{1}:{0}{2}), 2)", uti.GetExcelColLetter(a), 2, count);
                }

                if (lstForumaCol[a] == 9)
                {
                    HSSFCell YearlyDiv = excelObj.getCell(count, a, "My Dividends");
                    YearlyDiv.SetCellType(CellType.FORMULA);
                    YearlyDiv.CellFormula = string.Format("ROUND(SUM({0}{1}:{0}{2}), 2)", uti.GetExcelColLetter(a), 2, count);
                }
                if (lstForumaCol[a] == 10)
                {
                    HSSFCell TotalCostBasis = excelObj.getCell(count, a, "My Dividends");
                    TotalCostBasis.SetCellType(CellType.FORMULA);
                    TotalCostBasis.CellFormula = string.Format("SUM({0}{1}:{0}{2})", uti.GetExcelColLetter(a), 2, count);
                }
            }

            for (int i = lstForumaCol.Count; i < dtFinal.Columns.Count; i++)
            {
                excelObj.getCell(count, i, "My Dividends").CellStyle = excelObj.getStyle("headers");
            }

            AutoSizeColumns(excelObj, dtFinal, "My Dividends");
            excelObj.Save(savePath);
            excelObj.Dispose();
            System.Diagnostics.Process.Start(savePath);
        }


        public static DataTable ConvertExcelData(DataTable dt, string[] yields, string[] annualDiv, string[] company, out List<int> lstForumaCol, out int totalColCount)
        {
            //decimal totalCost = 0;
            //decimal yearlyDividends = 0;
            DataTable dtFinal = GetFinalTables();
            string[] col = INIFileOptions.ReadINIKeyValues(KeyPartition.columnName);
            string[] visible = INIFileOptions.ReadINIKeyValues(KeyPartition.visible);
            lstForumaCol = new List<int>();
            totalColCount = GetTotalColCount(visible);
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
                        //if (!lstForumaCol.Contains(cnt))
                        //{
                        //    lstForumaCol.Add(cnt);
                        //}
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(Math.Round(yearlyDiv / 12, 2));
                        //if (!lstForumaCol.Contains(cnt))
                        //{
                        //    lstForumaCol.Add(cnt);
                        //}
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(Math.Round(yearlyDiv / 4, 2));
                        //if (!lstForumaCol.Contains(cnt))
                        //{
                        //    lstForumaCol.Add(cnt);
                        //}
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Convert.ToDouble(yearlyDiv);
                        //if (!lstForumaCol.Contains(cnt))
                        //{
                        //    lstForumaCol.Add(cnt);
                        //}
                    }
                    cnt++;
                    if (visible[visCnt++] == "true")
                    {
                        dr[col[cnt]] = Math.Round(Convert.ToDouble(shares * cost), 2);
                        //if (!lstForumaCol.Contains(cnt))
                        //{
                        //    lstForumaCol.Add(cnt);
                        //}
                    }
                    cnt = 0;
                    visCnt = 0;
                    //if (visible[cnt] == "true")
                    //{
                    //    yearlyDividends += Convert.ToDecimal(yearlyDiv);
                    //}
                    //totalCost += Math.Round(Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(dt.Rows[i]["cost"]), 2);
                    dtFinal.Rows.Add(dr);
                }
            }

            for (int a = 0; a < visible.Length; a++)
            {
                if (visible[a] == "true")
                {
                    //if (a == 6 || a == 7 || a == 8 || a == 9 || a == 10)
                    //{
                        lstForumaCol.Add(a);
                    //}
                }
            }

            DataRow drEmpty = dtFinal.NewRow();
            if (visible[6] == "true")
            {
                //industry
                drEmpty[col[6]] = 0;
            }
            //drEmpty["Annual Dividend"] = 0;
            if (visible[7] == "true")
            {
                drEmpty[col[7]] = 0;
            }
            if (visible[8] == "true")
            {
                drEmpty[col[8]] = 0;
            }
            if (visible[9] == "true")
            {
                drEmpty[col[9]] = 0;
            }
            if (visible[10] == "true")
            {
                drEmpty[col[10]] = 0;
            }
            dtFinal.Rows.Add(drEmpty);
            return dtFinal;
        }

        public static int GetTotalColCount(string[] visible)
        {
            int count = 0;
            for (int i = 0; i < visible.Length; i++)
            {
                if (visible[i] == "true")
                {
                    count++;
                }
            }
            return count;
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
