/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 14:57:03
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;
using JIT.Utility.Web.ComponentModel.ExtJS.Data.Field;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class ClientBussinessDefinedBLL
    {
        #region RequestParameter
        public class DynamicVipPropertySaveRP : IAPIRequestParameter
        {
            public string PropertyID { get; set; }
            public string DisplayType { get; set; }
            public string PropertyName { get; set; }
            public JIT.CPOS.BS.BLL.OptionsBLL.Option[] OptionList { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(PropertyID))
                //    throw new APIException(201, "属性ID不能为空！");
            }
        }
        #endregion
        public string DynamicVipPropertySave(DynamicVipPropertySaveRP dynamicVipPropertySaveRP)
        {
            string result = "";

            DataTable dataTable = Utils.TableParameterCommon;
            foreach (var item in dynamicVipPropertySaveRP.OptionList)
            {
                DataRow dr = dataTable.NewRow();
                dr["Column1"] = item.OptionText;
                dataTable.Rows.Add(dr);
            }

            result = this._currentDAO.DynamicVipPropertySave(dynamicVipPropertySaveRP.PropertyID, dynamicVipPropertySaveRP.DisplayType, dynamicVipPropertySaveRP.PropertyName, dataTable);

            return result;
        }
        #region 使用动态属性
        #region  Add wen wu 使用动态属性
        public class DynamicControlDisplayListRP : IAPIRequestParameter
        {
            public string Type { get; set; }
            public string TableName { get; set; }
            public string ClientID { get; set; }
            public FieldCDListRP[] FieldList { get; set; }
            public FieldCDListRP[] AllFieldList { get; set; }
            public void Validate()
            {
                if (string.IsNullOrEmpty(TableName))
                    throw new APIException(201, "查询表名不为空！");
                //else if (string.IsNullOrEmpty(ClientID))
                //    throw new APIException(202, "客户id不能为空！");
            }
        }
        #endregion
        /// <summary>
        /// 查询正在使用的动态属性
        /// Add by wen wu 20140928
        /// </summary>
        /// <param name="dynamicVipPropertySaveRP"></param>
        /// <returns></returns>
        public FieldCDLoadRP DynamicControlDisplayList(DynamicControlDisplayListRP dynamicControlDisplayListRP)
        {
            FieldCDLoadRP fieldCDList = new FieldCDLoadRP();
            DataSet dataSet = new DataSet();
            DataTable dataTable = Utils.TableParameterCommon;
            dataSet = this._currentDAO.DynamicControlDisplayList(dynamicControlDisplayListRP.Type, dynamicControlDisplayListRP.TableName);
            if (Utils.IsDataSetValid(dataSet))
          {
                if (dynamicControlDisplayListRP.Type == "2")
                {
                    fieldCDList.UsedFieldList = (from f in dataSet.Tables[0].AsEnumerable()
                                                 where ((!string.IsNullOrWhiteSpace(f["EditOrder"].ToString())
                                                 && f["EditOrder"].ToString() != "0" 
                                                 && int.Parse(f["EditOrder"].ToString())>20))
                                                 select new FieldCDListRP()
                                                 {
                                                     FormControlID = f["ClientBussinessDefinedID"].ToString(),
                                                     ColumnDesc = f["ColumnDesc"].ToString(),
                                                     EditOrder = f["EditOrder"].ToString(),
                                                     ControlType = f["ControlType"].ToString(),
                                                     TableName = f["TableName"].ToString(),
                                                     ClientID = f["ClientID"].ToString(),
                                                     IsMustDo = f["IsMustDo"].ToString(),
                                                     IsRead = f["IsRead"].ToString()
                                                 }).ToArray();
                }
                else
                {
                    fieldCDList.UsedFieldList = (from f in dataSet.Tables[0].AsEnumerable()
                                                 where ((!string.IsNullOrWhiteSpace(f["EditOrder"].ToString())
                                                  && f["EditOrder"].ToString() != "0"
                                                  &&int.Parse(f["EditOrder"].ToString())<=20))
                                                 select new FieldCDListRP()
                                                 {
                                                     FormControlID = f["ClientBussinessDefinedID"].ToString(),
                                                     ColumnDesc = f["ColumnDesc"].ToString(),
                                                     EditOrder = f["EditOrder"].ToString(),
                                                     ControlType = f["ControlType"].ToString(),
                                                     TableName = f["TableName"].ToString(),
                                                     ClientID = f["ClientID"].ToString(),
                                                     IsMustDo = f["IsMustDo"].ToString(),
                                                     IsRead = f["IsRead"].ToString()
                                                 }).ToArray();
                }
                fieldCDList.AllFieldList = (from f in dataSet.Tables[0].AsEnumerable()
                                           select new FieldCDListRP()
                                                  {
                                                      FormControlID = f["ClientBussinessDefinedID"].ToString(),
                                                     // ColumnName = f["ColumnName"].ToString(),
                                                      ColumnDesc = f["ColumnDesc"].ToString(),
                                                      EditOrder = f["EditOrder"].ToString(),
                                                      ControlType = f["ControlType"].ToString(),
                                                      TableName = f["TableName"].ToString(),
                                                      ClientID = f["ClientID"].ToString(),
                                                      IsMustDo = f["IsMustDo"].ToString(),
                                                      IsRead = f["IsRead"].ToString()
                                                  }).ToArray();
            }
            return fieldCDList;
        }
        /// <summary>
        /// 保存正在使用的动态属性
        /// Add by wen wu 20140928
        /// </summary>
        /// <param name="dynamicVipPropertySaveRP"></param>
        /// <returns></returns>
        public string DynamicControlDisplaySave(DynamicControlDisplayListRP dynamicControlDisplayListRP)
        {
            string result = "";
            DataTable dataTable = new DataTable();
            dataTable = Utils.ToDataTable(dynamicControlDisplayListRP.FieldList);
            result = this._currentDAO.DynamicControlDisplaySave(dynamicControlDisplayListRP.Type, dynamicControlDisplayListRP.TableName, dataTable);
            return result;
        }
        #endregion
        public class Field
        {
            public string FormControlID { get; set; }
            //public string FormControlID { get; set; }
            //public string ColumnDesc { get; set; }
            //public int ControlType { get; set; }
            //public int DisplayType { get; set; }
            //public int IsMustDo { get; set; }
            //public int EditOrder { get; set; }
            //public int IsUsed { get; set; }
            //public string Hierarchy { get; set; }
        }

        public class FieldCDListRP
        {
            public string FormControlID { get; set; }
            public string ColumnDesc { get; set; }
            public string EditOrder { get; set; }
            //public string ListOrder { get; set; }
            //public string ConditionOrder { get; set; }
            public string ControlType { get; set; }
            public string TableName { get; set; }
           // public string Type { get; set; }
            public string IsMustDo { get; set; }
            public string IsRead { get; set; }
           // public string IsUsed { get; set; }
            public string ClientID { get; set; }
        }

        public class FieldCDLoadRP : IAPIResponseData
        {
            public FieldCDListRP[] UsedFieldList { get; set; }
            public FieldCDListRP[] AllFieldList { get; set; }
        }
    }
}