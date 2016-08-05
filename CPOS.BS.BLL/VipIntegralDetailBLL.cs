/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipIntegralDetailBLL
    {
        #region GetVipIntegralDetailList
        /// <summary>
        /// GetVipIntegralDetailList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public IList<VipIntegralDetailEntity> GetVipIntegralDetailList(VipIntegralDetailEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<VipIntegralDetailEntity> list = new List<VipIntegralDetailEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipIntegralDetailList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipIntegralDetailEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetVipIntegralDetailListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetVipIntegralDetailListCount(VipIntegralDetailEntity entity)
        {
            return _currentDAO.GetVipIntegralDetailListCount(entity);
        }
        #endregion

        /// <summary>
        /// ��ȡ�����ѽ��
        /// </summary>
        public decimal GetVipSalesAmount(string vipId)
        {
            return _currentDAO.GetVipSalesAmount(vipId);
        }

        /// <summary>
        /// ��ȡ�ܲ�������
        /// </summary>
        public decimal GetVipIntegralAmount(string vipId)
        {
            return _currentDAO.GetVipIntegralAmount(vipId);
        }

        /// <summary>
        /// �����û���ע��ȡ��������
        /// </summary>
        public decimal GetVipNextLevelIntegralAmount(string vipId)
        {
            return _currentDAO.GetVipNextLevelIntegralAmount(vipId);
        }

        public decimal GetVipIntegralByOrder(string orderId, string userId)
        {
            return this._currentDAO.GetVipIntegralByOrder(orderId, userId);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
    }
}