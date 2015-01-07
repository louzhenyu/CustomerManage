/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/18 10:28:46
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
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipOrderSubRunObjectMappingBLL
    {
        private LoggingSessionInfo loggingSessionInfo; 
        private BasicUserInfo CurrentUserInfo;
        private VipOrderSubRunObjectMappingDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipOrderSubRunObjectMappingBLL(LoggingSessionInfo pUserInfo)
        {
            this.loggingSessionInfo = pUserInfo;
            this._currentDAO = new VipOrderSubRunObjectMappingDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipOrderSubRunObjectMappingEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipOrderSubRunObjectMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity,pTran);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipOrderSubRunObjectMappingEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public VipOrderSubRunObjectMappingEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipOrderSubRunObjectMappingEntity pEntity , IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity,pTran);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(VipOrderSubRunObjectMappingEntity pEntity )
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipOrderSubRunObjectMappingEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipOrderSubRunObjectMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity,pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID,pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipOrderSubRunObjectMappingEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities,pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipOrderSubRunObjectMappingEntity[] pEntities)
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
            _currentDAO.Delete(pIDs,pTran);
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public VipOrderSubRunObjectMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
           return _currentDAO.Query(pWhereConditions,pOrderBys);
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipOrderSubRunObjectMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQuery(pWhereConditions,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VipOrderSubRunObjectMappingEntity[] QueryByEntity(VipOrderSubRunObjectMappingEntity pQueryEntity, OrderBy[] pOrderBys)
        {
           return _currentDAO.QueryByEntity(pQueryEntity,pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<VipOrderSubRunObjectMappingEntity> PagedQueryByEntity(VipOrderSubRunObjectMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQueryByEntity(pQueryEntity,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        #endregion

        public void setJiKeGift(string UserID,string vipID)
        {
            //���ɨ���û������߻�Ա�Ͳ���ɨ�뽱����
            VipDAO vipDao = new VipDAO(loggingSessionInfo);
            var vip = vipDao.QueryByEntity(
                    new VipEntity()
                    {
                        VIPID = vipID
                    },
                    null
                ).FirstOrDefault();

            if (vip!=null&&!string.IsNullOrWhiteSpace(vip.CouponInfo))
            {

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "���ͷ�������:����"
                }); 
                return;
            }

            ////��ѯ���ͽ�������
            //int GiftIntegral = 0;

            //CustomerBasicSettingDAO CustomerBasicSettingDao = new CustomerBasicSettingDAO(loggingSessionInfo);
            //CustomerBasicSettingEntity CustomerBasicSettingEntity = CustomerBasicSettingDao.GetBasicSettings(UserID, "GetVipIntegral").FirstOrDefault();

            //if (CustomerBasicSettingEntity!=null&&!string.IsNullOrWhiteSpace(CustomerBasicSettingEntity.SettingValue))
            //{
            //    int.TryParse(CustomerBasicSettingEntity.SettingValue, out GiftIntegral);
            //}
            
            //if (GiftIntegral == 0)
            //{
            //    GiftIntegral = 100;
            //}
            
            ////���ֻ���Ϊ���
            //decimal GiftAmount = GiftIntegral / 100;
                        
            ////����
            //VipAmountDAO VipAmountDao = new VipAmountDAO(loggingSessionInfo);
            //VipAmountDetailDAO VipAmountDetailDao = new VipAmountDetailDAO(loggingSessionInfo);


            //VipAmountEntity amountEntity = VipAmountDao.QueryByEntity(
            //        new VipAmountEntity()
            //        {
            //            VipId = UserID
            //        }
            //        , null
            //    ).FirstOrDefault();

            //if (amountEntity==null)
            //{
            //    VipAmountDao.Create(
            //        new VipAmountEntity(){
            //            VipId = UserID,
            //            BeginAmount = 0,
            //            EndAmount = GiftAmount,
            //            InAmount = GiftAmount,
            //            OutAmount = 0,
            //            IsLocking =0,
            //            TotalAmount = GiftAmount 
            //        }
            //    );                
            //}
            //else
            //{
            //    try
            //    {
            //        Loggers.Debug(new DebugLogInfo()
            //        {
            //            Message = "���ͷ�����:" + "������Ϣ��" + amountEntity.TotalAmount == null ? "00000" : amountEntity.TotalAmount.ToString()
            //        });

            //        amountEntity.TotalAmount = amountEntity.TotalAmount + GiftAmount;
            //        amountEntity.InAmount = amountEntity.InAmount + GiftAmount;
            //        amountEntity.EndAmount = amountEntity.EndAmount + GiftAmount;
            //        VipAmountDao.Update(amountEntity);
            //    }
            //    catch (Exception ex)
            //    {
            //        Loggers.Debug(new DebugLogInfo()
            //        {
            //            Message = "���ͷ�������:" + ex.Message.ToString()
            //        }); ;
            //    }
                
            //}

            //VipAmountDetailDao.Create(
            //    new VipAmountDetailEntity(){
            //        VipAmountDetailId = Guid.NewGuid(),
            //        VipId = UserID,
            //        Amount = GiftAmount,
            //        AmountSourceId = "12",
            //        ObjectId = string.Empty,
            //        Remark ="���ͷ����ֶһ�Ϊ���"
            //    }
            //);


            //��ѯ���ͽ�������
            int GiftIntegral = 0;

            CustomerBasicSettingDAO CustomerBasicSettingDao = new CustomerBasicSettingDAO(loggingSessionInfo);
            CustomerBasicSettingEntity CustomerBasicSettingEntity = CustomerBasicSettingDao.GetBasicSettings(UserID, "GetVipIntegral").FirstOrDefault();

            if (CustomerBasicSettingEntity != null && !string.IsNullOrWhiteSpace(CustomerBasicSettingEntity.SettingValue))
            {
                int.TryParse(CustomerBasicSettingEntity.SettingValue, out GiftIntegral);
            }

            if (GiftIntegral == 0)
            {
                GiftIntegral = 100;
            }


            Loggers.Debug(new DebugLogInfo()
            {
                Message = "���ͷ�������:�õ�����" + GiftIntegral.ToString()
            }); 

            //����
            VipIntegralDAO VipIntegralDao = new VipIntegralDAO(loggingSessionInfo);
            VipIntegralDetailDAO vipIntegralDetailDao = new VipIntegralDetailDAO(loggingSessionInfo);


            VipIntegralEntity integralEntity = VipIntegralDao.QueryByEntity(
                    new VipIntegralEntity()
                    {
                        VipID = UserID
                    }
                    , null
                ).FirstOrDefault();

            if (integralEntity == null)
            {
                VipIntegralDao.Create(
                    new VipIntegralEntity()
                    {
                        VipID = UserID,
                        BeginIntegral = GiftIntegral,
                        InIntegral = GiftIntegral,
                        OutIntegral =0,
                        EndIntegral = GiftIntegral,
                        InvalidIntegral =0,
                        ValidIntegral =0,
                        IsDelete =0
                    }
                );
            }
            else
            {
                try
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "���ͷ�����:" + "������Ϣ��" + integralEntity.EndIntegral == null ? "00000" : integralEntity.EndIntegral.ToString()
                    });

                    integralEntity.BeginIntegral += GiftIntegral;
                    integralEntity.InvalidIntegral += GiftIntegral;
                    integralEntity.EndIntegral += GiftIntegral;
                    VipIntegralDao.Update(integralEntity);
                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "���ͷ�������:" + ex.Message.ToString()
                    }); 
                }

            }

            try
            {
                vipIntegralDetailDao.Create(
                    new VipIntegralDetailEntity()
                    {
                        VipIntegralDetailID = Guid.NewGuid().ToString().Replace("-", ""),
                        VIPID = UserID,
                        SalesAmount = 0,
                        Integral = GiftIntegral,
                        IntegralSourceID = "25",
                        IsDelete = 0
                    }
                );
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "���ͷ�������:" + ex.Message.ToString()
                }); 
            }
            
        }
    }
}