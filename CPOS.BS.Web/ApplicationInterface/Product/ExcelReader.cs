using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Aspose.Cells;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product
{
    public class ExcelReader
    {
        /// <summary>
        /// 读取execel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ReadExcelToDataTable(string path)
        {
            Aspose.Cells.License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
            Workbook workbook = new Workbook();
            workbook.Open(path);//"C:\\test.xlsx"
            Cells cells = workbook.Worksheets[0].Cells;
            //System.Data.DataTable dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn);//noneTitle
            System.Data.DataTable dataTable = cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, cells.MaxColumn, true);//showTitle
            return dataTable;
        }
    }
}