/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表EclubMicroNumber的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubMicroNumberDAO : Base.BaseCPOSDAO, ICRUDable<EclubMicroNumberEntity>, IQueryable<EclubMicroNumberEntity>
    {
        /// <summary>
        /// 获取刊号信息
        /// </summary>
        /// <param name="microNumEn">刊号实体</param>
        /// <returns></returns>
        public DataSet GetMicroNums(EclubMicroNumberEntity microNumEn)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MicroNumberID,MicroNumber from EclubMicroNumber ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.Append("Order by MicroNumberNo DESC ;");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        /// <summary>
        /// 获取刊号列表
        /// </summary>
        /// <param name="number">期刊号</param>
        /// <param name="keyword">关键字</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>数据集</returns>
        public DataSet GetNumberList(string number, string keyword, string sortField, int sortOrder, int pageIndex, int pageSize)
        {
            //Instance Obj
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbCond = new StringBuilder();

            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (!string.IsNullOrEmpty(number))
            {
                sbCond.AppendFormat("and MicroNumberID = '{0}' ", number);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sbCond.AppendFormat("and MicroNumberName like '%{0}%' ", keyword);
            }
            //Build SQL Text
            sbSQL.Append("select NumberId, NumberName, MicroNumberNo, Number, CoverPath, Intro, Description, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete,'http://www.ckgsb.edu.cn/emba/pages/index/133' NumberUrl from(");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by {0} {1}) rowNum, MicroNumberID NumberId, MicroNumberName NumberName, MicroNumberNo, MicroNumber Number, CoverPath, Intro, Description, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete from dbo.EclubMicroNumber  ", sortField, sort);
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' {1} ", CurrentUserInfo.ClientID, sbCond.ToString());
            sbSQL.Append(") as Res ");
            sbSQL.AppendFormat("where rowNum between {0} and {1}  ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.AppendFormat("select COUNT(MicroNumberID) from EclubMicroNumber ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' {1} ", CurrentUserInfo.ClientID, sbCond.ToString());

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

    }
}
