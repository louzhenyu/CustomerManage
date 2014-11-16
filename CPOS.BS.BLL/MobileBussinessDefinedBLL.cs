/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/16 11:07:22
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Register.Response;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MobileBussinessDefinedBLL
    {
        public DataSet GetPagesInfo(string eventCode, string customerId)
        {
            return this._currentDAO.GetPagesInfo(eventCode, customerId);
        }

        public void UpdateDynamicColumnValue(string columnList, string userId,string tableName)
        {
            this._currentDAO.UpdateDynamicColumnValue(columnList, userId, tableName);
        }

        public string GetColumnName(string id)
        {
            return this._currentDAO.GetColumnName(id);
        }

        public DataSet GetObjectPagesInfo(string objectId, string customerId)
        {
            return this._currentDAO.GetObjectPagesInfo(objectId, customerId);
        }

        public void EditMobileBussinessDefined(MobileBussinessDefinedEntity[] addEntities, MobileBussinessDefinedEntity[] updatEntities, MobileBussinessDefinedEntity[] deletEntities)
        {
            _currentDAO.EditMobileBussinessDefined(addEntities, updatEntities, deletEntities);
        }

        /// <summary>
        /// 获取一个MobilePageBlockID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetMobilePageBlockIDByMobileModuleID(object id)
        {
            return _currentDAO.GetMobilePageBlockIDByMobileModuleID(id);
        }

        public string GetTableNameByObjectId(string objectId, string customerId)
        {
            return this._currentDAO.GetTableNameByObjectId(objectId, customerId);
        }

        public GetRegisterFormItemsRD GetEvevtFormItemValue(string objectId, string fieldName, string value)
        {
            string customerId = this.CurrentUserInfo.ClientID;
            GetRegisterFormItemsRD rd = new GetRegisterFormItemsRD();

            JIT.CPOS.DTO.Module.VIP.Register.Response.PageInfo[] pageInfoArray;
            
            var ds = GetObjectPagesInfo(objectId, customerId);
            pageInfoArray = GetForm(ds);

            if (!string.IsNullOrEmpty(value))
            {
                string tableName = "";
                if (ds != null && ds.Tables.Count > 4 && ds.Tables[3].Rows.Count > 0)
                    tableName = ds.Tables[3].Rows[0]["TableName"].ToString();

                if (!string.IsNullOrEmpty(tableName))
                {
                    string columnName = "";
                    List<string> columnNameList = GetDefineColumnName(pageInfoArray);
                    if (columnNameList != null && columnNameList.Count > 0)
                        columnName = columnNameList.ToArray().ToJoinString(",");

                    if (!string.IsNullOrEmpty(columnName))
                    {
                        string getValueSql = "select " + columnName + " from " + tableName + " where " + fieldName + " = '" + value + "'";
                        DataSet valueDataSet = this._currentDAO.GetValue(getValueSql);
                        if (valueDataSet != null && valueDataSet.Tables.Count > 0 && valueDataSet.Tables[0].Rows.Count > 0)
                            SetDefineColumnValue(pageInfoArray, valueDataSet.Tables[0].Rows[0]);
                    }
                }
            }

            rd.Pages = pageInfoArray.OrderBy(t => t.DisplayIndex).ToArray();
            return rd;
        }

        private List<string> GetDefineColumnName(DTO.Module.VIP.Register.Response.PageInfo[] pageInfoArray)
        {
            List<string> columnNameList = new List<string>();
            foreach (var page in pageInfoArray)
            {
                foreach (var block in page.Blocks)
                {
                    foreach (var property in block.PropertyDefineInfos)
                    {
                        columnNameList.Add(property.ControlInfo.ColumnName);
                    }
                }
            }

            return columnNameList;
        }

        private void SetDefineColumnValue(DTO.Module.VIP.Register.Response.PageInfo[] pageInfoArray, DataRow dataRow)
        {
            List<string> columnNameList = new List<string>();
            foreach (var page in pageInfoArray)
            {
                foreach (var block in page.Blocks)
                {
                    foreach (var property in block.PropertyDefineInfos)
                    {
                        property.ControlInfo.DefaultValue = dataRow[property.ControlInfo.ColumnName];
                    }
                }
            }
        }

        public JIT.CPOS.DTO.Module.VIP.Register.Response.PageInfo[] GetForm(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().OrderBy(t => t["DisplayIndex"]).Select(t => new JIT.CPOS.DTO.Module.VIP.Register.Response.PageInfo()
            {
                ID = t["ID"].ToString(),
                ValidFlag = Convert.ToInt32(t["IsVerification"].ToString()),
                Blocks = dataSet.Tables[1].AsEnumerable().OrderBy(b => b["DisplayIndex"]).Where(b => b["PageId"].ToString() == t["ID"].ToString()).Select(b => new BlockInfo
                {
                    ID = b["ID"].ToString(),
                    PageID = b["PageId"].ToString(),
                    DisplayIndex = Convert.ToInt32(b["DisplayIndex"]),
                    PropertyDefineInfos = dataSet.Tables[2].AsEnumerable().OrderBy(c => c["DisplayIndex"]).Where(c => c["BlockId"].ToString() == b["ID"].ToString()).Select(c => new PropertyDefineInfo
                    {
                        ID = c["MobileBussinessDefinedID"].ToString(),
                        BlockID = c["BlockId"].ToString(),
                        Title = c["Title"].ToString(),
                        DisplayIndex = Convert.ToInt32(c["DisplayIndex"]),

                        ControlInfo = dataSet.Tables[3].AsEnumerable().Where(d => d["MobileBussinessDefinedID"].ToString() == c["MobileBussinessDefinedID"].ToString()).Select(d => new ControlInfo
                        {
                            ControlType = Convert.ToInt32(d["ControlType"]),
                            ColumnDesc = d["ColumnDesc"].ToString(),
                            ColumnName = d["ColumnName"].ToString(),
                            OptionValues = dataSet.Tables[4].AsEnumerable().Where(e => e["MobileBussinessDefinedID"].ToString() == d["MobileBussinessDefinedID"].ToString()).OrderBy(e => e["OptionValue"]).Select(e => new KeyValueInfo
                            {
                                Key = e["OptionValue"].ToString(),
                                Value = e["OptionText"].ToString()
                            }).ToArray(),
                        }).First(),

                    }).ToArray(),
                }).ToArray(),
                DisplayIndex = Convert.ToInt32(t["DisplayIndex"])
            }).ToArray();
        }
    }
}