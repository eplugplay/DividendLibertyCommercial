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
            DataTable dtFinal = uti.ConvertExcelData(dt, yields, annualDiv, companies);
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
