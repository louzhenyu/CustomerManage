/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_InoutBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private T_InoutDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_InoutBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new T_InoutDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_InoutEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity, pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_InoutEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public T_InoutEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity, pTran);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(T_InoutEntity pEntity)
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_InoutEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_InoutEntity pEntity, IDbTransaction pTran)
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
        public void Delete(T_InoutEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_InoutEntity[] pEntities)
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

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public T_InoutEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_InoutEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public T_InoutEntity[] QueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<T_InoutEntity> PagedQueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion
        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public T_InoutEntity GetInoutInfo(string orderId, LoggingSessionInfo pUserInfo)
        {
            var inoutInfo = this._currentDAO.GetByID(orderId);
            if (inoutInfo != null)
            {
                var vipIntegralDetailBll = new VipIntegralDetailBLL(pUserInfo);
                var vipBll = new VipBLL(pUserInfo);
                var tOrderCouponMappingBll = new TOrderCouponMappingBLL(pUserInfo);
                var vipAmountDetailBll = new VipAmountDetailBLL(pUserInfo);
                var deliveryBll = new TOrderCustomerDeliveryStrategyMappingBLL(pUserInfo);
                //ʹ�û���
                inoutInfo.pay_points = Math.Abs(vipIntegralDetailBll.GetVipIntegralByOrder(orderId,inoutInfo.vip_no));
                if (inoutInfo.pay_points > 0)
                {
                    decimal integralAmountPre = vipBll.GetIntegralAmountPre(pUserInfo.ClientID);//��ȡ���ֽ�����
                    //���ֵֿ�
                    inoutInfo.IntegralAmount = inoutInfo.pay_points.Value * (integralAmountPre > 0 ? integralAmountPre : 0.01M);
                }
                else
                    inoutInfo.IntegralAmount = 0;//���ֵֿ�
                //�Ż�ȯ�ֿ�
                var couponParValue = tOrderCouponMappingBll.GetCouponParValue(orderId);
                inoutInfo.CouponAmount = couponParValue;

                //ʹ�õ��˻����
                inoutInfo.VipEndAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId,inoutInfo.vip_no, 1));
                //ʹ�õķ��ֽ��
                inoutInfo.ReturnAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, inoutInfo.vip_no, 13));
                //���ͷ� 
                inoutInfo.DeliveryAmount = deliveryBll.GetDeliverAmount(orderId);
            }
            return inoutInfo;
        }
    }
}