using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;
using JIT.Utility;
using System.Data;
using JIT.CPOS.BS.BLL.Utility.ETCL;

namespace JIT.CPOS.BS.BLL.Module.VIP.VIPImport
{
    public class VIPImportChecker : ITransformer
    {
        private ETCLWorkSheetInfo worksheetInfo;

        public VIPImportChecker(ETCLWorkSheetInfo worksheetInfo)
        {
            this.worksheetInfo = worksheetInfo;
        }

        public bool Process(DataTable pDataSource, BasicUserInfo pUserInfo, out IETCLDataItem[] oData, out IETCLResultItem[] oResult)
        {
            bool isPass = true;
            List<ETCLCommonResultItem> lstCheckResult = new List<ETCLCommonResultItem>();
            ////将原始数据按表拆分到各个DataTable
            List<TenantPlatformExcelDataItem> lstDataItem = new List<TenantPlatformExcelDataItem>();
            if (pDataSource != null)
            {
                bool isAllNullOrEmpty = true;
                //移除全为null或string.Empty的记录
                for (int i = 0; i < pDataSource.Rows.Count; i++)
                {
                    isAllNullOrEmpty = true;
                    DataRow dr = pDataSource.Rows[i];
                    for (int j = 0; j < pDataSource.Columns.Count; j++)
                    {
                        if (dr[j] != DBNull.Value && dr[j].ToString().Trim() != string.Empty)
                        {
                            isAllNullOrEmpty = false;
                            break;
                        }
                    }
                    if (isAllNullOrEmpty)
                    {
                        pDataSource.Rows.RemoveAt(i);
                        i--;
                    }
                }
                pDataSource.AcceptChanges();

                foreach (var tableItem in this.worksheetInfo.Tables)
                {
                    if (tableItem.ReferenceOnly == 1)
                    {
                        continue;
                    }
                    tableItem.Data = pDataSource.Copy();
                    tableItem.Data.TableName = tableItem.TableName;
                    Dictionary<string, string> columnTextNameMapping = new Dictionary<string, string>();
                    foreach (var columnItem in tableItem.Columns)
                    {
                        columnTextNameMapping.Add(columnItem.ColumnText, columnItem.ColumnName);
                    }
                    //去掉多余列
                    foreach (DataColumn columnItem in pDataSource.Columns)
                    {
                        if (columnTextNameMapping.ContainsKey(columnItem.ColumnName))
                        {//文本改成列名
                            tableItem.Data.Columns[columnItem.ColumnName].ColumnName = columnTextNameMapping[columnItem.ColumnName];
                        }
                        else
                        {
                            tableItem.Data.Columns.Remove(columnItem.ColumnName);
                        }
                    }
                }
            }
            else
            {
                oData = null;
                oResult = null;
                return true;
            }
            var index = 0;
            //转换到Entity 
            foreach (DataRow item in pDataSource.Rows)
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                try
                {
                    for (int j = 0; j < pDataSource.Columns.Count; j++)
                    {

                        d.Add(pDataSource.Columns[j].ColumnName, item[j] == DBNull.Value ? "" : item[j].ToString().Replace("'", " "));
                    }


                    TenantPlatformExcelDataItem dItem = new TenantPlatformExcelDataItem();
                    dItem.OriginalRow = new Dictionary<string, string>();
                    for (int i = 0; i < pDataSource.Columns.Count; i++)
                    {
                        dItem.OriginalRow.Add(pDataSource.Columns[i].ColumnName, item[i].ToString().Replace("'", " "));
                    }
                    dItem.Entity = d;
                    dItem.TableName = "User";
                    dItem.Index = index;
                    lstDataItem.Add(dItem);
                }
                catch (Exception ex)
                {
                    ETCLCommonResultItem etclCommonResultItem = new ETCLCommonResultItem();
                    etclCommonResultItem.ColumnOrder = index;
                    etclCommonResultItem.Message = ex.Message;
                    etclCommonResultItem.OPType = OperationType.Extract;
                    etclCommonResultItem.ResultCode = 500;
                    etclCommonResultItem.RowIndex = pDataSource.Rows.IndexOf(item);
                    etclCommonResultItem.ServerityLevel = ServerityLevel.Error;
                    etclCommonResultItem.TableIndex = 0;
                    lstCheckResult.Add(etclCommonResultItem);
                    isPass = false;
                }
                index++;

            }
            oResult = lstCheckResult.ToArray();
            oData = lstDataItem.ToArray();
            return isPass;
        }

    }
}