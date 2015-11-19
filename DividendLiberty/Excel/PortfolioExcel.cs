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
            string[] annualDiv = uti.SplitStockData(YahooFinance.GetValues(stocks, "d", true));
            string[] yields = uti.SplitStockData(YahooFinance.GetValues(stocks, "y", true));
            string[] companies = uti.SplitStockData(YahooFinance.GetValues(stocks, "n", true));
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
            decimal totalCost = 0;
            decimal yearlyDividends = 0;
            DataTable dtFinal = GetFinalTables();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["active"].ToString() == "true")
                {
                    DataRow dr = dtFinal.NewRow();
                    decimal shares = Convert.ToDecimal(dt.Rows[i]["shares"]);
                    decimal cost = Convert.ToDecimal(dt.Rows[i]["cost"]);
                    decimal annDiv = annualDiv[i] == "N/A" ? 0 : Convert.ToDecimal(annualDiv[i]);
                    decimal yearlyDiv = Math.Round(shares * annDiv, 2);
                    dr["Symbol"] = dt.Rows[i]["symbol"].ToString();
                    dr["Company"] = company[i].Length == 0 ? "" : company[i].ToString();
                    dr["Industry"] = dt.Rows[i]["industry"].ToString();
                    dr["Shares"] = Convert.ToDouble(shares);
                    dr["Price"] = Convert.ToDouble(cost);
                    dr["Annual Dividend"] = Convert.ToDouble(annDiv);
                    dr["Yield"] = yields[i] == "N/A" ? 0 : Convert.ToDouble(yields[i]);
                    dr["Monthly Dividends"] = Convert.ToDouble(Math.Round(yearlyDiv / 12, 2));
                    dr["Quarterly Dividends"] = Convert.ToDouble(Math.Round(yearlyDiv / 4, 2));
                    dr["Yearly Dividends"] = Convert.ToDouble(yearlyDiv);
                    dr["Cost Basis"] = Math.Round(Convert.ToDouble(shares * cost), 2);
                    yearlyDividends += Convert.ToDecimal(yearlyDiv);
                    totalCost += Math.Round(Convert.ToDecimal(dt.Rows[i]["shares"]) * Convert.ToDecimal(dt.Rows[i]["cost"]), 2);
                    dtFinal.Rows.Add(dr);
                }
            }

            DataRow drEmpty = dtFinal.NewRow();
            drEmpty["Yield"] = 0;
            //drEmpty["Annual Dividend"] = 0;
            drEmpty["Monthly Dividends"] = 0;
            drEmpty["Quarterly Dividends"] = 0;
            drEmpty["Yearly Dividends"] = 0;
            drEmpty["Cost Basis"] = 0;
            dtFinal.Rows.Add(drEmpty);
            return dtFinal;
        }

        public static DataTable GetFinalTables(string[,] keySections, string[] values)
        {
            DataTable dtFinal = new DataTable();
            for (int i = 0; i < values.Length; i++)
            {
                dtFinal.Columns.Add(keySections[1, 0], typeof(string));
                dtFinal.Columns.Add(keySections[1, 1], typeof(string));
                dtFinal.Columns.Add(keySections[1, 2], typeof(string));
                dtFinal.Columns.Add(keySections[1, 3], typeof(double));
                dtFinal.Columns.Add(keySections[1, 4], typeof(double));
                dtFinal.Columns.Add(keySections[1, 5], typeof(double));
                dtFinal.Columns.Add(keySections[1, 6], typeof(double));
                dtFinal.Columns.Add(keySections[1, 7], typeof(double));
                dtFinal.Columns.Add(keySections[1, 8], typeof(double));
                dtFinal.Columns.Add(keySections[1, 9], typeof(double));
                dtFinal.Columns.Add(keySections[1, 10], typeof(double));
            }
            return dtFinal;
        }

        public static DataTable GetFinalTables()
        {
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("Symbol", typeof(string));
            dtFinal.Columns.Add("Company", typeof(string));
            dtFinal.Columns.Add("Industry", typeof(string));
            dtFinal.Columns.Add("Shares", typeof(double));
            dtFinal.Columns.Add("Price", typeof(double));
            dtFinal.Columns.Add("Annual Dividend", typeof(double));
            dtFinal.Columns.Add("Yield", typeof(double));
            dtFinal.Columns.Add("Monthly Dividends", typeof(double));
            dtFinal.Columns.Add("Quarterly Dividends", typeof(double));
            dtFinal.Columns.Add("Yearly Dividends", typeof(double));
            dtFinal.Columns.Add("Cost Basis", typeof(double));
            return dtFinal;
        }

        private static void AutoSizeColumns(Excel.ExcelNPOIWriter exObj, DataTable tbl, string sheetName)
        {
            for (int i = 0; i <= tbl.Columns.Count; i++)
            {
                exObj.AutoSizeColumn(i, sheetName);
            }
        }

        public static void SaveExcelSettings(string[,] keys, string[] values)
        {
            uti.SaveIniFile(keys[0, 0], keys[1, 0], values[0]);
            uti.SaveIniFile(keys[0, 1], keys[1, 1], values[1]);
            uti.SaveIniFile(keys[0, 2], keys[1, 2], values[2]);
            uti.SaveIniFile(keys[0, 3], keys[1, 3], values[3]);
            uti.SaveIniFile(keys[0, 4], keys[1, 4], values[4]);
            uti.SaveIniFile(keys[0, 5], keys[1, 5], values[5]);
            uti.SaveIniFile(keys[0, 6], keys[1, 6], values[6]);
            uti.SaveIniFile(keys[0, 7], keys[1, 7], values[7]);
            uti.SaveIniFile(keys[0, 8], keys[1, 8], values[8]);
            uti.SaveIniFile(keys[0, 9], keys[1, 9], values[9]);
            uti.SaveIniFile(keys[0, 10], keys[1, 10], values[10]);
        }

        public static void HideExcelColumns(string [] Newvalues)
        {
            string[,] sectionKeys = INIFileOptions.GetINISectionKeys();
            string[] values = INIFileOptions.SetINIValues(Newvalues);

            PortfolioExcel.SaveExcelSettings(sectionKeys, values);
        }
    }
}
