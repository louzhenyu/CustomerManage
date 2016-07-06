/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
using System.Data.SqlClient;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipCardBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private VipCardDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new VipCardDAO(pUserInfo);
        }
        #endregion
        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity, pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public VipCardEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>        
        public void Update(VipCardEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity, pTran);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(VipCardEntity pEntity)
        {
            _currentDAO.Update(pEntity);
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity, pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardEntity[] pEntities)
        {
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            _currentDAO.Delete(pIDs, pTran);
        }
        #endregion

        /// <summary>
        /// ���������ͷ��ֵ��������VIPCARD�����ദ��
        /// </summary>
        /// <param name="para"></param>
        /// <param name="pChangeEntity"></param>
        /// <param name="pLoggingSessionInfo"></param>
        /// <param name="pTran"></param>
        /// <param name="pNewMoney"></param>
        /// <param name="pOldMoney"></param>
        public void VipCardProcess(SetVipCardRP para, VipCardEntity pChangeEntity
            , LoggingSessionInfo pLoggingSessionInfo, SqlTransaction pTran, decimal pNewMoney
            , decimal pOldMoney)
        {
            //��Ա��
            var VipCardBLL = new VipCardBLL(pLoggingSessionInfo);

            if (para.AmountSourceID == "23")
            {
                //�����ۼƳ�ֵ��С��0�Ĳ���
                if (para.BalanceMoney > 0)
                {
                    pChangeEntity.RechargeTotalAmount += para.BalanceMoney;
                    if (pChangeEntity.RechargeTotalAmount < 0)
                    {
                        throw new APIException("����������С��0��") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                }
                pChangeEntity.BalanceAmount = pNewMoney;
            }
            else if (para.AmountSourceID == "24")
            {
                pChangeEntity.CumulativeBonus = pChangeEntity.CumulativeBonus ?? 0;
                pChangeEntity.BalanceBonus = pChangeEntity.BalanceBonus ?? 0;
                //�����ۼƷ��֣�С��0�Ĳ���
                if (para.BalanceMoney > 0)
                {
                    pChangeEntity.CumulativeBonus += para.BalanceMoney;
                    if (pChangeEntity.CumulativeBonus < 0)
                    {
                        throw new APIException("��������ۼƷ���С��0��") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                }
                pChangeEntity.BalanceBonus += para.BalanceMoney;
            }

            //ִ�и���
            VipCardBLL.Update(pChangeEntity, pTran);
        }
        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public VipCardEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return _currentDAO.Query(pWhereConditions, pOrderBys);
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipCardEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VipCardEntity[] QueryByEntity(VipCardEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<VipCardEntity> PagedQueryByEntity(VipCardEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion
    }
}