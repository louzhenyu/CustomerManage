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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipCardBLL
    {
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
                        vipCardEntity.MembershipTime = vipCardEntity.BeginDate;
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
                        this._currentDAO.Update(vipCardEntity, false);
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

    }
}