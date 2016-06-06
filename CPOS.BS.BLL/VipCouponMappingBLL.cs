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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipCouponMappingBLL
    {
        /// <summary>
        /// �жϷ��������û����Ż�ȯ�Ƿ����� 
        /// </summary>
        /// <param name="strCouponId"></param>
        /// <returns></returns>
        public int HadBeGranted(string strCouponId, string strGiver)
        {
            return this._currentDAO.HadBeGranted(strCouponId, strGiver);
        }
        /// <summary>
        /// �Ż�ȯת��
        /// </summary>
        /// <param name="strGiver">������</param>
        /// <param name="strGrantee">������</param>
        /// <param name="strCouponId">�Ż�ȯID</param> 
        /// 
        /// <returns></returns>
        public  int  GrantCoupon(string strGiver,string strGrantee,string strCouponId)
        {
            return this._currentDAO.GrantCoupon(strGiver, strGrantee, strCouponId);
        }
        /// <summary>
        /// �ж�ͬ��ȯ���Ż�ȯ�Ƿ�����ȡ(�����������Ҫ��)
        /// </summary>
        /// <param name="pVipID">��ȡ��ID</param>
        /// <param name="pCouponTypeID">ȯ��ID</param>
        /// <param name="pSourceType">ȯ��Դ����</param>
        /// <returns>0=��ʾ����ȯδ��ȡ����>0��ʾ����ȡ��</returns>
        public int GetReceiveCouponCount(string pVipID, string pCouponTypeID, string pSourceType)
        {
            return this._currentDAO.GetReceiveCouponCount(pVipID, pCouponTypeID, pSourceType    );
        }
    }
}