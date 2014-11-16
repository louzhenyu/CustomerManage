/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    public partial class WiFiUserVisitBLL
    {

        #region  查询对应时间内的人数
        /// <summary>
        /// 查询对应时间内的人数
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="minMinute">最小分钟</param>
        /// <param name="maxMinute">最大分钟(值-1代表无最大区间)</param>
        /// <returns></returns>
        public int GetVipNum(string unitID, int minMinute, int maxMinute)
        {
            return _currentDAO.GetVipNum(unitID, minMinute, maxMinute);
        }
        #endregion


        #region  查询全部或当前人数
        /// <summary>
        /// 查询全部或当前人数
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="isNowCount">是否是计算当前人数</param>
        /// <returns></returns>
        public int GetVipNumAllOrNow(string unitID, bool isNowCount)
        {
            return _currentDAO.GetVipNumAllOrNow(unitID, isNowCount);
        }
        #endregion


        #region  分页查询当前人数信息
        /// <summary>
        /// 分页查询当前人数信息
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetVipListByPage(string unitID, int PageIndex, int PageSize)
        {
            return _currentDAO.GetVipListByPage(unitID, PageIndex, PageSize);
        }
        #endregion


        #region  返回门店某个会员详细
        /// <summary>
        /// 返回门店某个会员详细
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetVipDetailList(string strWhere)
        {
            return _currentDAO.GetVipDetailList(strWhere);
        }
        #endregion


        #region 判断条件是否存在数据
        /// <summary>
        /// 判断条件是否存在数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExists(string strWhere)
        {
            return _currentDAO.IsExists(strWhere);
        }
        #endregion


        #region  根据条件获取实例
        /// <summary>
        /// 根据条件获取实例
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public WiFiUserVisitEntity GetByWhere(string strWhere)
        {
            return _currentDAO.GetByWhere(strWhere);
        }
        #endregion

    }
}