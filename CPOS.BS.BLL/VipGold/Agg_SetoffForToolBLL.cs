/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/20 9:22:52
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class Agg_SetoffForToolBLL
    {
        /// <summary>
        /// 获取最近几天信息 根据时间、商户编号 获取
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="DateCode">统计日期</param>
        /// <param name="SetoffToolTypeId">集客工具类型（CTW：创意仓库、Coupon：优惠券、SetoffPoster：集客报、Goods：商品）</param>
        /// <returns></returns>
        public DataSet GetSetofToolListByCustomerId(string CustomerId, string DateCode, string SetoffRoleId, string begintime, string endtime)
        {
            return _currentDAO.GetSetofToolListByCustomerId(CustomerId, SetoffRoleId, begintime, endtime, DateCode);
        }


        /// <summary>
        /// 分页获取 集客内容分析 列表
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="PageSize">每页分多少条</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForToolEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            return _currentDAO.FindAllByPage(pWhereConditions, pOrderBys, PageSize, PageIndex);
        }
        /// <summary>
        /// 将CTW Coupon Goods 转换为 中文字 CTW：创意仓库   Coupon：优惠券   SetoffPoster：集客报   Goods：商品   其他
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetSetoffToolTypeNameBySetoffToolType(string type)
        {
            if (type == "CTW")
            {
                return "创意仓库活动";
            }
            else if (type == "Coupon")
            {
                return "优惠券";
            }
            else if (type == "SetoffPoster")
            {
                return "集客海报";
            }
            else if (type == "Goods")
            {
                return "商品";
            }
            else
            {
                return "";
            }
        }
    }
}