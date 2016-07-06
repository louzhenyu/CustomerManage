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
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.Common;
using System.Transactions;
using System.Data.SqlClient;
using JIT.CPOS.DTO.Module.Report.DayReport.Response;
using JIT.CPOS.DTO.Base;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipCardBLL
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        public VipCardEntity GetByID(object pID, string CustomerID)
        {
            return this._currentDAO.GetByID(pID, CustomerID);
        }

        #region ��ѯ��Ա����Ϣ
        /// <summary>
        /// ��ѯ��Ա����Ϣ
        /// </summary>
        public VipCardEntity SearchTopVipCard(VipCardEntity entity)
        {
            try
            {
                VipCardEntity vipCardInfo = new VipCardEntity();
                DataSet ds = _currentDAO.SearchTopVipCard(entity);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipCardInfo = DataTableToObject.ConvertToObject<VipCardEntity>(ds.Tables[0].Rows[0]);
                }
                return vipCardInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ���ݻ�Ա��ѯ��Ա����Ϣ
        /// <summary>
        /// ��ѯ��Ա����Ϣ
        /// </summary>
        public VipCardEntity SearchVipCardByVip(string vipid)
        {
            try
            {
                VipCardEntity vipCardInfo = new VipCardEntity();
                DataSet ds = _currentDAO.SearchVipCardByVip(vipid);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipCardInfo = DataTableToObject.ConvertToObject<VipCardEntity>(ds.Tables[0].Rows[0]);
                }
                return vipCardInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ��Ա�б��ѯ(Jermyn20130624)
        /// <summary>
        /// ��Ա�б��ѯ
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public VipCardEntity SearchVipCard(VipCardEntity searchInfo)
        {
            VipCardEntity vipCardInfo = new VipCardEntity();
            IList<VipCardEntity> vipCardInfoList = new List<VipCardEntity>();
            vipCardInfo.ICount = _currentDAO.SearchVipCardCount(searchInfo);
            DataSet ds = _currentDAO.SearchVipCardList(searchInfo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipCardInfoList = DataTableToObject.ConvertToList<VipCardEntity>(ds.Tables[0]);
            }
            vipCardInfo.VipCardInfoList = vipCardInfoList;
            return vipCardInfo;
        }
        #endregion

        #region ��Ա��ѯ
        /// <summary>
        /// ��Ա��ѯ
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity GetVipList(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.GetVipListCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.GetVipList(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region �����Ա����Ϣ

        /// <summary>
        /// �����Ա����Ϣ
        /// </summary>
        /// <param name="vipCardID">��Ա��ID</param>
        /// <param name="vipCardEntity">��Ա��ʵ��</param>
        /// <returns></returns>
        public bool SaveVipCardInfo(string vipCardID, VipCardEntity vipCardEntity)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (vipCardID.Trim().Length == 0)
                    {
                        var basicServer = new VipCardBasicBLL(this.CurrentUserInfo);
                        //����
                        vipCardEntity.VipCardID = Utils.NewGuid();
                        vipCardEntity.VipCardCode = basicServer.GetVipCardCode(vipCardEntity.UnitID);
                        vipCardEntity.MembershipTime = Convert.ToDateTime(vipCardEntity.BeginDate);
                        vipCardEntity.TotalAmount = 0;
                        vipCardEntity.PurchaseTotalAmount = 0;
                        vipCardEntity.PurchaseTotalCount = 0;
                        vipCardEntity.CustomerID = CurrentUserInfo.CurrentUser.customer_id;
                        this._currentDAO.Create(vipCardEntity);

                        //��ӻ�Ա����VIP��ϵ
                        var vipMapping = new VipCardVipMappingBLL(this.CurrentUserInfo);
                        var vipMappingEntity = new VipCardVipMappingEntity()
                        {
                            MappingID = Utils.NewGuid(),
                            VipCardID = vipCardEntity.VipCardID,
                            VIPID = vipCardEntity.VipId
                        };
                        vipMapping.Create(vipMappingEntity);
                    }
                    else
                    {
                        //�޸�
                        vipCardEntity.VipCardID = vipCardID;
                        this._currentDAO.Update(vipCardEntity);
                    }

                    var mappingServer = new VipCardUnitMappingBLL(this.CurrentUserInfo);
                    var mappingEntity = new VipCardUnitMappingEntity()
                    {
                        VipCardID = vipCardEntity.VipCardID,
                        UnitID = vipCardEntity.UnitID
                    };
                    var mappingEntitys = mappingServer.QueryByEntity(mappingEntity, null);

                    //���ݻ�Ա��IDɾ����Ա�����ŵ��ϵ��
                    this._currentDAO.DeteleVipCardUnitMapping(vipCardEntity.VipCardID);

                    if (mappingEntitys != null && mappingEntitys.Length > 0)
                    {
                        //���»�Ա�����ŵ��ϵ��
                        this._currentDAO.UpdateVipCardUnitMapping(vipCardEntity.VipCardID, vipCardEntity.UnitID);
                    }
                    else
                    {
                        //������Ա�����ŵ��ϵ��
                        mappingEntity.MappingID = Utils.NewGuid();
                        mappingServer.Create(mappingEntity);
                    }

                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region ɾ����Ա����Ϣ

        /// <summary>
        /// ɾ����Ա����Ϣ
        /// </summary>
        /// <param name="vipCardID">��Ա��ID</param>
        /// <returns></returns>
        public bool DeleteVipCardInfo(string vipCardID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //ɾ����Ա����Ϣ
                    this._currentDAO.Delete(new VipCardEntity() { VipCardID = vipCardID });

                    //ɾ����Ա�����ŵ��ϵ��
                    this._currentDAO.DeteleVipCardUnitMapping(vipCardID);

                    //ɾ����Ա����VIP��ϵ
                    this._currentDAO.DeteleVipCardVipMapping(vipCardID);

                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


        public DataSet GetVipCardTypeList(string customerId)
        {
            return this._currentDAO.GetVipCardTypeList(customerId);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_BatchNo"></param>
        /// <returns></returns>
        public DataSet ExportVipCardCode(string p_BatchNo)
        {
            return this._currentDAO.ExportVipCardCode(p_BatchNo);
        }

        #region ��Ա��
        /// <summary>
        /// ��Ա���б��ѯ
        /// </summary>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="pPageSize">Ҷ��¼��</param>
        /// <param name="pCurrentPageIndex">Ҷ����</param>
        /// <returns>����</returns>
        public PagedQueryResult<VipCardEntity> GetVipCardList(string VipID, string Phone, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipCardList(VipID, Phone, pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        /// <summary>
        /// ���ݿ�������������ȡ��Ա����Ϣ
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardEntity GetByCodeOrISN(object pID)
        {
            return this._currentDAO.GetByCodeOrISN(pID);
        }
        #endregion

        #region ��Ա������
        /// <summary>
        /// ��ȡ�ս��ۿ�ͳ��
        /// </summary>
        /// <param name="StareDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DayVendingRD GetDayVendingCount(string StareDate, string EndDate)
        {
            DayVendingRD Data = null;
            DataSet ds = this._currentDAO.GetDayVendingCount(StareDate, EndDate);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Data = new DayVendingRD();
                Data.DayVendingInfoList = new List<DayVendingInfo>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DayVendingInfo m = new DayVendingInfo();
                    if (ds.Tables[0].Rows[i]["MembershipTime"] != DBNull.Value)
                    {
                        m.MembershipTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["MembershipTime"]).ToString("yyyy-MM-dd");
                    }
                    if (ds.Tables[0].Rows[i]["VipCardTypeName"] != DBNull.Value)
                    {
                        m.VipCardTypeName = ds.Tables[0].Rows[i]["VipCardTypeName"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["GiftCardCount"] != DBNull.Value)
                    {
                        m.GiftCardCount = int.Parse(ds.Tables[0].Rows[i]["GiftCardCount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SaleCardCount"] != DBNull.Value)
                    {
                        m.SaleCardCount = int.Parse(ds.Tables[0].Rows[i]["SaleCardCount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SalesAmount"] != DBNull.Value)
                    {
                        m.SalesAmount = decimal.Parse(ds.Tables[0].Rows[i]["SalesAmount"].ToString());
                    }
                    //�б�ֵ
                    Data.DayVendingInfoList.Add(m);
                }

                //
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["VendingCount"] != DBNull.Value)
                    {
                        Data.VendingCount = int.Parse(ds.Tables[1].Rows[0]["VendingCount"].ToString());
                    }
                    if (ds.Tables[1].Rows[0]["GiftCardCount"] != DBNull.Value)
                    {
                        Data.GiftCardCount = int.Parse(ds.Tables[1].Rows[0]["GiftCardCount"].ToString());
                    }
                    if (ds.Tables[1].Rows[0]["VendingAmountCount"] != DBNull.Value)
                    {
                        Data.VendingAmountCount = int.Parse(ds.Tables[1].Rows[0]["VendingAmountCount"].ToString());
                    }
                }
            }

            return Data;
        }

        /// <summary>
        /// �ս����ͳ��
        /// </summary>
        /// <param name="StareDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public DayReconciliationRD GetDayReconciliation(DateTime StareDate, DateTime EndDate, int Days, string UnitID, string CustomerID)
        {
            DayReconciliationRD Data = null;
            DataSet ds = this._currentDAO.GetDayReconciliation(StareDate, EndDate, Days, UnitID, CustomerID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Data = new DayReconciliationRD();
                Data.DayReconciliationInfoList = new List<DayReconciliationInfo>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DayReconciliationInfo m = new DayReconciliationInfo();
                    if (ds.Tables[0].Rows[i]["MembershipTime"] != DBNull.Value)
                    {
                        m.MembershipTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["MembershipTime"]).ToString("yyyy-MM-dd");
                    }
                    if (ds.Tables[0].Rows[i]["SaleCardCount"] != DBNull.Value)
                    {
                        m.SaleCardCount = int.Parse(ds.Tables[0].Rows[i]["SaleCardCount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SalesToAmount"] != DBNull.Value)
                    {
                        m.SalesToAmount = decimal.Parse(ds.Tables[0].Rows[i]["SalesToAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["RechargeTotalAmount"] != DBNull.Value)
                    {
                        m.RechargeTotalAmount = decimal.Parse(ds.Tables[0].Rows[i]["RechargeTotalAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["StoredSalesTotalAmount"] != DBNull.Value)
                    {
                        m.StoredSalesTotalAmount = decimal.Parse(ds.Tables[0].Rows[i]["StoredSalesTotalAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["IntegratDeductibleCount"] != DBNull.Value)
                    {
                        m.IntegratDeductibleCount = decimal.Parse(ds.Tables[0].Rows[i]["IntegratDeductibleCount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["IntegratDeductibleToAmount"] != DBNull.Value)
                    {
                        m.IntegratDeductibleToAmount = decimal.Parse(ds.Tables[0].Rows[i]["IntegratDeductibleToAmount"].ToString());
                    }
                    //�б�ֵ
                    Data.DayReconciliationInfoList.Add(m);
                }


            }
            return Data;
        }
        #endregion

        public VipCardEntity GetVipCardByVipMapping(string vipId)
        {
            return this._currentDAO.GetVipCardByVipMapping(vipId);
        }
		
        #region ��Ա�����֡�������
        /// <summary>
        /// ��ӷ��ֲ���
        /// </summary>
        /// <param name="pVipCardCode">����</param>
        /// <param name="pTotalReturnAmount">���ֽ��</param>
        /// <param name="tran">����</param>
        public void AddReturnCash(string pVipCardCode, decimal pTotalReturnAmount, t_unitEntity unitInfo, SqlTransaction tran, string pImageUrl, VipCardEntity pVipCardEntity)
        {
            bool isUpdate = false;// ��pVipCardEntityΪ�յ�ʱ����Ҫֱ�Ӹ������ݿ�����Ƿ��ظ��º��ʵ�壬��Ϊ��Ϊ�յ�ʱ���ʾ����Ҫ�����ύ�ģ�������뷵�ظ��º��ʵ�壬��Ϊ��ǣ��������޸�
            var vipCardTransLogBLL = new VipCardTransLogBLL(CurrentUserInfo); //�����ս��׼�¼����ʾ����
            var vipCardBLL = new VipCardBLL(CurrentUserInfo); //�����ս��׼�¼����ʾ����
            if (pVipCardEntity == null)
            {
                isUpdate = true;
                pVipCardEntity = vipCardBLL.QueryByEntity(new VipCardEntity()
                {
                    VipCardCode = pVipCardCode
                }, null).FirstOrDefault();
            }
            //ͼƬ
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            // 
            var unitBLL2 = new TUnitBLL(CurrentUserInfo);//�ŵ�ҵ�����
            //���䶯��¼
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(CurrentUserInfo);

            var vipCardTransLogEntity = new VipCardTransLogEntity()
            {
                VipCardCode = pVipCardCode,
                UnitCode = unitInfo != null ? unitInfo.unit_code : string.Empty,
                TransContent = "����",
                TransType = "B",
                TransTime = DateTime.Now,
                TransAmount = pTotalReturnAmount,
                LastValue = Convert.ToInt32(pVipCardEntity.BalanceBonus ?? 0m),        //�ڳ����
                NewValue = Convert.ToInt32((pVipCardEntity.BalanceBonus ?? 0m) + pTotalReturnAmount), //��ĩ���
                CustomerID = pVipCardEntity.CustomerID
            };
            //�������ֱ䶯��¼
            string blaneId = System.Guid.NewGuid().ToString();
            VipCardBalanceChangeEntity AddEntity = new VipCardBalanceChangeEntity()
            {
                ChangeID = blaneId,
                VipCardCode = pVipCardCode,
                ChangeAmount = pTotalReturnAmount,
                //�䶯ǰ�������
                ChangeBeforeBalance = (pVipCardEntity.BalanceBonus ?? 0m),
                //�䶯�������
                ChangeAfterBalance = (pVipCardEntity.BalanceBonus ?? 0m) + pTotalReturnAmount,
                ChangeReason = "����",
                Status = 1,
                Remark = "",
                CustomerID = CurrentUserInfo.ClientID,
                UnitID = unitInfo != null ? unitInfo.unit_id : string.Empty,
                ImageURL = ""
            };
            //����ͼƬ�ϴ�
            if (!string.IsNullOrEmpty(pImageUrl))
            {
                var objectImagesEntity = new ObjectImagesEntity()
                {
                    ImageId = System.Guid.NewGuid().ToString(),
                    ObjectId = AddEntity.ChangeID,
                    ImageURL = pImageUrl
                };
                objectImagesBLL.Create(objectImagesEntity, tran);
            }
            //ִ������
            VipCardBalanceChangeBLL.Create(AddEntity, tran);
            vipCardTransLogBLL.Create(vipCardTransLogEntity, tran);
            // �ж��ǿۻ�����
            if (pTotalReturnAmount > 0)
            {
                pVipCardEntity.CumulativeBonus = (pVipCardEntity.CumulativeBonus ?? 0m) + pTotalReturnAmount;
            }
            pVipCardEntity.BalanceBonus = (pVipCardEntity.BalanceBonus ?? 0m) + pTotalReturnAmount;

            if (isUpdate)
            {
                vipCardBLL.Update(pVipCardEntity, tran);
            }
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="pVipCardCode">����</param>
        /// <param name="pTotalReturnAmount">���</param>
        /// <param name="tran">����</param>
        public void AddBalance(string pVipCardCode, decimal pTotalReturnAmount, t_unitEntity unitInfo, SqlTransaction tran, string pImageUrl, VipCardEntity pVipCardEntity)
        {
            var vipCardTransLogBLL = new VipCardTransLogBLL(CurrentUserInfo); //�����ս��׼�¼����ʾ����
            bool isUpdate = false;// ��pVipCardEntityΪ�յ�ʱ����Ҫֱ�Ӹ������ݿ�����Ƿ��ظ��º��ʵ�壬��Ϊ��Ϊ�յ�ʱ���ʾ����Ҫ�����ύ�ģ�������뷵�ظ��º��ʵ�壬��Ϊ��ǣ��������޸�
            var vipCardBLL = new VipCardBLL(CurrentUserInfo); //�����ս��׼�¼����ʾ����
            if (pVipCardEntity == null)
            {
                isUpdate = true;
                pVipCardEntity = vipCardBLL.QueryByEntity(new VipCardEntity()
                {
                    VipCardCode = pVipCardCode
                }, null).FirstOrDefault();
            }
            //ͼƬ
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            // 
            var unitBLL2 = new TUnitBLL(CurrentUserInfo);//�ŵ�ҵ�����
            //���䶯��¼
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(CurrentUserInfo);

            var vipCardTransLogEntity = new VipCardTransLogEntity()
            {
                VipCardCode = pVipCardCode,
                UnitCode = unitInfo != null ? unitInfo.unit_code : string.Empty,
                TransContent = "���",
                TransType = "C",
                TransTime = DateTime.Now,
                TransAmount = pTotalReturnAmount,
                LastValue = Convert.ToInt32(pVipCardEntity.BalanceAmount ?? 0m),        //�ڳ����
                NewValue = Convert.ToInt32((pVipCardEntity.BalanceAmount ?? 0m) + pTotalReturnAmount), //��ĩ���
                CustomerID = pVipCardEntity.CustomerID
            };

            //�������䶯��¼
            string blaneId = System.Guid.NewGuid().ToString();
            VipCardBalanceChangeEntity AddEntity = new VipCardBalanceChangeEntity()
            {
                ChangeID = blaneId,
                VipCardCode = pVipCardCode,
                ChangeAmount = pTotalReturnAmount,
                //�䶯ǰ�������
                ChangeBeforeBalance = (pVipCardEntity.BalanceAmount ?? 0m),
                //�䶯�������
                ChangeAfterBalance = (pVipCardEntity.BalanceAmount ?? 0m) + pTotalReturnAmount,
                ChangeReason = "���",
                Status = 1,
                Remark = "",
                CustomerID = CurrentUserInfo.ClientID,
                UnitID = unitInfo != null ? unitInfo.unit_id : string.Empty,
                ImageURL = pImageUrl
            };

            //����ͼƬ�ϴ�
            if (!string.IsNullOrEmpty(pImageUrl))
            {
                var objectImagesEntity = new ObjectImagesEntity()
                {
                    ImageId = Guid.NewGuid().ToString(),
                    ObjectId = AddEntity.ChangeID,
                    ImageURL = pImageUrl
                };
                objectImagesBLL.Create(objectImagesEntity, tran);
            }
            //ִ������
            VipCardBalanceChangeBLL.Create(AddEntity, tran);
            vipCardTransLogBLL.Create(vipCardTransLogEntity, tran);
            // �ж��ǿۻ�����
            if (pTotalReturnAmount > 0)
            {
                pVipCardEntity.RechargeTotalAmount = (pVipCardEntity.RechargeTotalAmount ?? 0m) + pTotalReturnAmount;
            }
            pVipCardEntity.BalanceAmount = (pVipCardEntity.BalanceAmount ?? 0m) + pTotalReturnAmount;

            if (isUpdate)
            {
                vipCardBLL.Update(pVipCardEntity, tran);
            }
        }

        /// <summary>
        /// ���»���
        /// </summary>
        /// <param name="pVipCardCode">����</param>
        /// <param name="pTotalAmount">���û���</param>
        /// <param name="tran"></param>
        public void UpdateIntegral(string pVipCardCode, Int32? pTotalAmount, SqlTransaction tran)
        {
            VipCardEntity vipCardEntity = _currentDAO.QueryByEntity(new VipCardEntity { VipCardCode = pVipCardCode }, null).FirstOrDefault();
            vipCardEntity.BalancePoints = pTotalAmount;
            _currentDAO.Update(vipCardEntity, tran);
        }

        #endregion

    }
}