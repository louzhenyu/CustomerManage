/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
    /// ҵ����  
    /// </summary>
    public partial class CouponTypeBLL
    {


        /// <summary>
        /// ��ȡ�ѱ���Ա��ȡ���Ż�ȯͳ��
        /// </summary>
        /// <param name="CouponTypeID"></param>
        /// <returns></returns>
        public int GetCouponCount(string CouponTypeID)
        {
            return _currentDAO.GetCouponCount(CouponTypeID);
        }

        #region GetCouponType
        /// <summary>
        /// ��ȡ�����Ż݄����
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponType()
        {
            return this._currentDAO.GetCouponType();
        }
        #endregion

        #region GetCouponTypeList
        public DataSet GetCouponTypeList()
        {
            return this._currentDAO.GetCouponTypeList();
        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public Guid CreateReturnID(CouponTypeEntity pEntity, IDbTransaction pTran)
        {
            return this._currentDAO.CreateReturnID(pEntity, pTran);
        }
        /// <summary>
        /// �Ż�ȯʹ�����
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponTypeCount()
        {
            return this._currentDAO.GetCouponTypeCount();
        }
        /// <summary>
        /// ����CouponTypeID��ȡ�����˶���ȯ
        /// </summary>
        /// <param name="strCouponTypeID"></param>
        /// <returns></returns>
        public int GetCouponCountByCouponTypeID(string strCouponTypeID)
        {
            return this._currentDAO.GetCouponCountByCouponTypeID(strCouponTypeID);
        }
        public int ExistsCouponTypeName(string strConponTypeName)
        {
            return this._currentDAO.ExistsCouponTypeName(strConponTypeName);

        }
        /// <summary>
        /// �����Ż�ȯ��ʹ����
        /// </summary>
        /// <returns></returns>
        public void UpdateCouponTypeIsVoucher(string strCustomerId)
        {
             this._currentDAO.UpdateCouponTypeIsVoucher(strCustomerId);
        }
    }
}