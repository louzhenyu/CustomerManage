/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipWithdrawDepositApplyBLL
    {
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByToday(string vipId)
        {
            return this._currentDAO.GetVipWDApplyByToday(vipId);
        }
         /// <summary>
        /// �������ִ����������ͻ�ȡ���ִ���
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByNumType(string vipId, int WithDrawNumType)
        {
            return this._currentDAO.GetVipWDApplyByNumType(vipId, WithDrawNumType);
        }


        

        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// ���ݻ�Ա/��Ա����ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipWithdrawDepositApplyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex, int isVip)
        {
            if (isVip == 1)//��Ա
                return this._currentDAO.PagedQueryByVipName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
            else if (isVip == 2)//��Ա
                return this._currentDAO.PagedQueryByUserName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
            else // (isVip ==3)//������
                return this._currentDAO.PagedQueryByRetailName(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        public DataSet PagedQueryDbSet(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex, out int rowCount, out int pageCount)
        {
            return _currentDAO.PagedQueryDbSet(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex, out  rowCount, out  pageCount);
        }
        public bool MultiCheck(string[] ids, int type, string remark)
        {
            int status = -1;
            if (type == 1)
                status = 2;
            else if (type == 2)
                status = 4;
            return _currentDAO.MultiCheck(ids, status, remark);
        }
        public bool MultiComplete(string[] ids)
        {
            return _currentDAO.MultiCheck(ids, 3);
        }
    }
}