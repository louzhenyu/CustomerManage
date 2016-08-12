/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class SysVipCardTypeBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 获取会员卡列表
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public SysVipCardTypeEntity[] GetVipCardList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return this._currentDAO.GetVipCardList(pWhereConditions, pOrderBys);
        }
        /// <summary>
        /// 获取会员卡体系信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeSystemList(string CustomerId)
        {
            return _currentDAO.GetVipCardTypeSystemList(CustomerId);
        }
        /// <summary>
        /// 根据会员卡等级信息获取相关联开卡礼信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetCardUpgradeRewardList(string CustomerId)
        {
            return _currentDAO.GetCardUpgradeRewardList(CustomerId);
        }
        /// <summary>
        /// 获取当前会员相关的卡绑定实体卡信息
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="Phone"></param>
        /// <param name="VipID"></param>
        /// <returns></returns>
        public DataSet GetBindVipCardTypeInfo(string CustomerID, string Phone, string VipID, int? CurLevel)
        {
            return _currentDAO.GetBindVipCardTypeInfo(CustomerID, Phone, VipID, CurLevel);
        }
        /// <summary>
        /// 根据卡和当前等级获取升级售卡的列表 1=微信的售卡展示 2=APP的售卡展示
        /// </summary>
        /// <param name="CustomerId">商户ID</param>
        /// <param name="CurLevel">当前会员等级</param>
        /// <param name="pType">操作类型(1=微信的售卡展示 2=APP的售卡展示)</param>
        /// <returns></returns>
        public DataSet GetVipCardTypeVirtualItemList(string CustomerId, int? CurLevel, string pType,string pVipID)
        {
            return _currentDAO.GetVipCardTypeVirtualItemList(CustomerId, CurLevel, pType, pVipID);
        }
        /// <summary>
        /// 根据当前卡等级信息获取升级条件信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CurLevel"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeUpGradeInfo(string CustomerId, int? CurLevel)
        {
            return _currentDAO.GetVipCardTypeUpGradeInfo(CustomerId,CurLevel);
        }
            /// <summary>
        /// 根据会员卡等级信息获取相关联开卡礼信息
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeByIsprepaid(string CustomerId, int IsOnLineSale)
        {
            return _currentDAO.GetVipCardTypeByIsprepaid(CustomerId, IsOnLineSale);
        }
        
    }
}