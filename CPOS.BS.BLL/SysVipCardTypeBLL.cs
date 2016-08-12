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
    /// ҵ����  
    /// </summary>
    public partial class SysVipCardTypeBLL
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// ��ȡ��Ա���б�
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public SysVipCardTypeEntity[] GetVipCardList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return this._currentDAO.GetVipCardList(pWhereConditions, pOrderBys);
        }
        /// <summary>
        /// ��ȡ��Ա����ϵ��Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeSystemList(string CustomerId)
        {
            return _currentDAO.GetVipCardTypeSystemList(CustomerId);
        }
        /// <summary>
        /// ���ݻ�Ա���ȼ���Ϣ��ȡ�������������Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetCardUpgradeRewardList(string CustomerId)
        {
            return _currentDAO.GetCardUpgradeRewardList(CustomerId);
        }
        /// <summary>
        /// ��ȡ��ǰ��Ա��صĿ���ʵ�忨��Ϣ
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
        /// ���ݿ��͵�ǰ�ȼ���ȡ�����ۿ����б� 1=΢�ŵ��ۿ�չʾ 2=APP���ۿ�չʾ
        /// </summary>
        /// <param name="CustomerId">�̻�ID</param>
        /// <param name="CurLevel">��ǰ��Ա�ȼ�</param>
        /// <param name="pType">��������(1=΢�ŵ��ۿ�չʾ 2=APP���ۿ�չʾ)</param>
        /// <returns></returns>
        public DataSet GetVipCardTypeVirtualItemList(string CustomerId, int? CurLevel, string pType,string pVipID)
        {
            return _currentDAO.GetVipCardTypeVirtualItemList(CustomerId, CurLevel, pType, pVipID);
        }
        /// <summary>
        /// ���ݵ�ǰ���ȼ���Ϣ��ȡ����������Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CurLevel"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeUpGradeInfo(string CustomerId, int? CurLevel)
        {
            return _currentDAO.GetVipCardTypeUpGradeInfo(CustomerId,CurLevel);
        }
            /// <summary>
        /// ���ݻ�Ա���ȼ���Ϣ��ȡ�������������Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeByIsprepaid(string CustomerId, int IsOnLineSale)
        {
            return _currentDAO.GetVipCardTypeByIsprepaid(CustomerId, IsOnLineSale);
        }
        
    }
}