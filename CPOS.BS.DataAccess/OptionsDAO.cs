/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:17
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OptionsDAO
    {
        #region GetOptionNameList
        public PagedQueryResult<OptionsViewEntity> GetOptionNameList(IWhereCondition[] pWhereConditions, string ClientID, string ClientDistributorID, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            if (ClientID == null || ClientID == "")
            {
                ClientID = CurrentUserInfo.ClientID;
            }
            if (ClientDistributorID == null || ClientDistributorID == "")
            {
                ClientDistributorID = CurrentUserInfo.ClientDistributorID;
            }
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }
            //options
            StringBuilder sql = new StringBuilder();
            sql.Append(
            @"select OptionName,0 as IsDelete,max(clientid) as ClientID,max(CreateTime) as CreateTime,COUNT(*) as OptionCount from Options where isdelete=0 "
            + sqlWhere.ToString()
            + " and ClientID='" + ClientID + "'"
            + " and ClientDistributorID=" + ClientDistributorID
            + " group by optionName");
            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + sql.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageWhere = sqlWhere.ToString();
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<OptionsViewEntity> pEntity = new PagedQueryResult<OptionsViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<OptionsViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region 获取Options Info By Option Name
        /// <summary>
        /// 通过optionname 获取DataSet，其中 optionname  为必传参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetOptionByName(string optionName)
        {
            //Create SQL Text
            StringBuilder sqlSB = new StringBuilder();
            sqlSB.Append("select OptionValue as OptionID,OptionText from Options ");
            sqlSB.AppendFormat("where OptionName='{0}' ", optionName);
            sqlSB.AppendFormat("and isnull(ClientID,'{0}')='{0}' and IsDelete = 0 ;", CurrentUserInfo.CurrentUser.customer_id);

            return this.SQLHelper.ExecuteDataset(sqlSB.ToString());
        }
        /// <summary>
        /// 通过optionname 获取DataSet，其中 optionname  为必传参数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="optionName">选项名</param>
        /// <returns></returns>
        public DataSet GetOptionByName_V2(string optionName)
        {
            //Create SQL Text
            StringBuilder sqlSB = new StringBuilder();
            sqlSB.Append("select OptionValue as OptionID,OptionText from Options ");
            sqlSB.AppendFormat("where OptionName='{0}' ", optionName);
            sqlSB.AppendFormat("and isnull(ClientID,'{0}')='{0}' and IsDelete = 0  ", CurrentUserInfo.CurrentUser.customer_id);
            sqlSB.Append("order by Sequence desc;");

            return this.SQLHelper.ExecuteDataset(sqlSB.ToString());
        }

        /// <summary>
        /// 通过optionname 获取DataSet，其中 optionname  为必传参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetOptionByName_V1(string optionName)
        {
            //Create SQL Text
            StringBuilder sqlSB = new StringBuilder();
            sqlSB.Append("select CONVERT(nvarchar(50),OptionValue) as ID,OptionText as Text from Options ");
            sqlSB.AppendFormat("where OptionName='{0}' ", optionName);
            sqlSB.AppendFormat("and isnull(ClientID,'{0}')='{0}' and IsDelete = 0 ;", CurrentUserInfo.CurrentUser.customer_id);

            return this.SQLHelper.ExecuteDataset(sqlSB.ToString());
        }
        #endregion

        #region get options by PropertyID
        public DataSet DynamicVipPropertyOptionList(string propertyID)
        {
            DataSet dataSet = new DataSet();

            List<SqlParameter> lsp = new List<SqlParameter>();
            lsp.Add(new SqlParameter("@PropertyID", propertyID));
            lsp.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));

            dataSet = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "DynamicVipPropertyOptionList", lsp.ToArray());

            return dataSet;
        }
        #endregion
    }
}