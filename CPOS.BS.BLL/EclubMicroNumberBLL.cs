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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class EclubMicroNumberBLL
    {
        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <returns>符合条件的实体集</returns>
        public EclubMicroNumberEntity[] MicroIssueNperGet(EclubMicroNumberEntity pQueryEntity)
        {
            OrderBy[] pOrderBys = new OrderBy[] { 
            new OrderBy(){ FieldName="MicroNumber", Direction= OrderByDirections.Desc}
            };
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// 获取刊号信息
        /// </summary>
        /// <param name="microNumEn">刊号实体</param>
        /// <returns></returns>
        public DataTable GetMicroNums(EclubMicroNumberEntity microNumEn)
        {
            return _currentDAO.GetMicroNums(microNumEn).Tables[0];
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
        public DataTable GetNumberList(string number, string keyword, string sortField, int sortOrder, int pageIndex, int pageSize, ref int pageCount, ref int rowCount)
        {
            DataSet ds = _currentDAO.GetNumberList(number, keyword, sortField, sortOrder, pageIndex, pageSize);
            if (ds == null && ds.Tables.Count <= 0)
            {
                return null;
            }
            rowCount = 0;
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            //Data Table 
            DataTable dt = ds.Tables[0];

            //Instance Ojb
            LNumberTypeMappingDAO NumTypeMap = new LNumberTypeMappingDAO(CurrentUserInfo);

            dt.Columns.Add("TypeList", typeof(DataTable));

            foreach (DataRow row in dt.Rows)
            {
                //获取板块信息
                DataSet dsChild = NumTypeMap.GetTypeInfoList(row["NumberId"].ToString());
                if (dsChild != null && dsChild.Tables.Count > 0)
                {
                    row["TypeList"] = dsChild.Tables[0];
                }
                else
                {
                    row["TypeList"] = null;
                }
            }
            return dt;
        }
    }
}