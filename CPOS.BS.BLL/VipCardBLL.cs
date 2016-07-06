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
    /// 业务处理：  
    /// </summary>
    public partial class VipCardBLL
    {
        /// <summary>
        /// 事务
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

        #region 查询会员卡信息
        /// <summary>
        /// 查询会员卡信息
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

        #region 根据会员查询会员卡信息
        /// <summary>
        /// 查询会员卡信息
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

        #region 会员列表查询(Jermyn20130624)
        /// <summary>
        /// 会员列表查询
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

        #region 会员查询
        /// <summary>
        /// 会员查询
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

        #region 保存会员卡信息

        /// <summary>
        /// 保存会员卡信息
        /// </summary>
        /// <param name="vipCardID">会员卡ID</param>
        /// <param name="vipCardEntity">会员卡实体</param>
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
                        //新增
                        vipCardEntity.VipCardID = Utils.NewGuid();
                        vipCardEntity.VipCardCode = basicServer.GetVipCardCode(vipCardEntity.UnitID);
                        vipCardEntity.MembershipTime = Convert.ToDateTime(vipCardEntity.BeginDate);
                        vipCardEntity.TotalAmount = 0;
                        vipCardEntity.PurchaseTotalAmount = 0;
                        vipCardEntity.PurchaseTotalCount = 0;
                        vipCardEntity.CustomerID = CurrentUserInfo.CurrentUser.customer_id;
                        this._currentDAO.Create(vipCardEntity);

                        //添加会员卡与VIP关系
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
                        //修改
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

                    //根据会员卡ID删除会员卡与门店关系表
                    this._currentDAO.DeteleVipCardUnitMapping(vipCardEntity.VipCardID);

                    if (mappingEntitys != null && mappingEntitys.Length > 0)
                    {
                        //更新会员卡与门店关系表
                        this._currentDAO.UpdateVipCardUnitMapping(vipCardEntity.VipCardID, vipCardEntity.UnitID);
                    }
                    else
                    {
                        //新增会员卡与门店关系表
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

        #region 删除会员卡信息

        /// <summary>
        /// 删除会员卡信息
        /// </summary>
        /// <param name="vipCardID">会员卡ID</param>
        /// <returns></returns>
        public bool DeleteVipCardInfo(string vipCardID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //删除会员卡信息
                    this._currentDAO.Delete(new VipCardEntity() { VipCardID = vipCardID });

                    //删除会员卡与门店关系表
                    this._currentDAO.DeteleVipCardUnitMapping(vipCardID);

                    //删除会员卡与VIP关系
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
        /// 导出卡号
        /// </summary>
        /// <param name="p_BatchNo"></param>
        /// <returns></returns>
        public DataSet ExportVipCardCode(string p_BatchNo)
        {
            return this._currentDAO.ExportVipCardCode(p_BatchNo);
        }

        #region 会员卡
        /// <summary>
        /// 会员卡列表查询
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <param name="pPageSize">叶记录数</param>
        /// <param name="pCurrentPageIndex">叶索引</param>
        /// <returns>集合</returns>
        public PagedQueryResult<VipCardEntity> GetVipCardList(string VipID, string Phone, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipCardList(VipID, Phone, pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
        /// <summary>
        /// 根据卡号码或者内码获取会员卡信息
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardEntity GetByCodeOrISN(object pID)
        {
            return this._currentDAO.GetByCodeOrISN(pID);
        }
        #endregion

        #region 会员卡报表
        /// <summary>
        /// 获取日结售卡统计
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
                    //列表赋值
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
        /// 日结对账统计
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
                    //列表赋值
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
		
        #region 会员卡返现、余额操作
        /// <summary>
        /// 添加返现操作
        /// </summary>
        /// <param name="pVipCardCode">卡号</param>
        /// <param name="pTotalReturnAmount">返现金额</param>
        /// <param name="tran">事务</param>
        public void AddReturnCash(string pVipCardCode, decimal pTotalReturnAmount, t_unitEntity unitInfo, SqlTransaction tran, string pImageUrl, VipCardEntity pVipCardEntity)
        {
            bool isUpdate = false;// 当pVipCardEntity为空的时候需要直接更新数据库而不是返回更新后的实体，因为当为空的时候表示不需要事务提交的，否则必须返回更新后的实体，因为会牵扯到多次修改
            var vipCardTransLogBLL = new VipCardTransLogBLL(CurrentUserInfo); //丰收日交易记录对象示例化
            var vipCardBLL = new VipCardBLL(CurrentUserInfo); //丰收日交易记录对象示例化
            if (pVipCardEntity == null)
            {
                isUpdate = true;
                pVipCardEntity = vipCardBLL.QueryByEntity(new VipCardEntity()
                {
                    VipCardCode = pVipCardCode
                }, null).FirstOrDefault();
            }
            //图片
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            // 
            var unitBLL2 = new TUnitBLL(CurrentUserInfo);//门店业务对象
            //余额变动记录
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(CurrentUserInfo);

            var vipCardTransLogEntity = new VipCardTransLogEntity()
            {
                VipCardCode = pVipCardCode,
                UnitCode = unitInfo != null ? unitInfo.unit_code : string.Empty,
                TransContent = "返现",
                TransType = "B",
                TransTime = DateTime.Now,
                TransAmount = pTotalReturnAmount,
                LastValue = Convert.ToInt32(pVipCardEntity.BalanceBonus ?? 0m),        //期初金额
                NewValue = Convert.ToInt32((pVipCardEntity.BalanceBonus ?? 0m) + pTotalReturnAmount), //期末金额
                CustomerID = pVipCardEntity.CustomerID
            };
            //新增返现变动记录
            string blaneId = System.Guid.NewGuid().ToString();
            VipCardBalanceChangeEntity AddEntity = new VipCardBalanceChangeEntity()
            {
                ChangeID = blaneId,
                VipCardCode = pVipCardCode,
                ChangeAmount = pTotalReturnAmount,
                //变动前卡内余额
                ChangeBeforeBalance = (pVipCardEntity.BalanceBonus ?? 0m),
                //变动后卡内余额
                ChangeAfterBalance = (pVipCardEntity.BalanceBonus ?? 0m) + pTotalReturnAmount,
                ChangeReason = "返现",
                Status = 1,
                Remark = "",
                CustomerID = CurrentUserInfo.ClientID,
                UnitID = unitInfo != null ? unitInfo.unit_id : string.Empty,
                ImageURL = ""
            };
            //增加图片上传
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
            //执行新增
            VipCardBalanceChangeBLL.Create(AddEntity, tran);
            vipCardTransLogBLL.Create(vipCardTransLogEntity, tran);
            // 判断是扣还是增
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
        /// 添加余额操作
        /// </summary>
        /// <param name="pVipCardCode">卡号</param>
        /// <param name="pTotalReturnAmount">金额</param>
        /// <param name="tran">事务</param>
        public void AddBalance(string pVipCardCode, decimal pTotalReturnAmount, t_unitEntity unitInfo, SqlTransaction tran, string pImageUrl, VipCardEntity pVipCardEntity)
        {
            var vipCardTransLogBLL = new VipCardTransLogBLL(CurrentUserInfo); //丰收日交易记录对象示例化
            bool isUpdate = false;// 当pVipCardEntity为空的时候需要直接更新数据库而不是返回更新后的实体，因为当为空的时候表示不需要事务提交的，否则必须返回更新后的实体，因为会牵扯到多次修改
            var vipCardBLL = new VipCardBLL(CurrentUserInfo); //丰收日交易记录对象示例化
            if (pVipCardEntity == null)
            {
                isUpdate = true;
                pVipCardEntity = vipCardBLL.QueryByEntity(new VipCardEntity()
                {
                    VipCardCode = pVipCardCode
                }, null).FirstOrDefault();
            }
            //图片
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            // 
            var unitBLL2 = new TUnitBLL(CurrentUserInfo);//门店业务对象
            //余额变动记录
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(CurrentUserInfo);

            var vipCardTransLogEntity = new VipCardTransLogEntity()
            {
                VipCardCode = pVipCardCode,
                UnitCode = unitInfo != null ? unitInfo.unit_code : string.Empty,
                TransContent = "余额",
                TransType = "C",
                TransTime = DateTime.Now,
                TransAmount = pTotalReturnAmount,
                LastValue = Convert.ToInt32(pVipCardEntity.BalanceAmount ?? 0m),        //期初金额
                NewValue = Convert.ToInt32((pVipCardEntity.BalanceAmount ?? 0m) + pTotalReturnAmount), //期末金额
                CustomerID = pVipCardEntity.CustomerID
            };

            //新增余额变动记录
            string blaneId = System.Guid.NewGuid().ToString();
            VipCardBalanceChangeEntity AddEntity = new VipCardBalanceChangeEntity()
            {
                ChangeID = blaneId,
                VipCardCode = pVipCardCode,
                ChangeAmount = pTotalReturnAmount,
                //变动前卡内余额
                ChangeBeforeBalance = (pVipCardEntity.BalanceAmount ?? 0m),
                //变动后卡内余额
                ChangeAfterBalance = (pVipCardEntity.BalanceAmount ?? 0m) + pTotalReturnAmount,
                ChangeReason = "余额",
                Status = 1,
                Remark = "",
                CustomerID = CurrentUserInfo.ClientID,
                UnitID = unitInfo != null ? unitInfo.unit_id : string.Empty,
                ImageURL = pImageUrl
            };

            //增加图片上传
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
            //执行新增
            VipCardBalanceChangeBLL.Create(AddEntity, tran);
            vipCardTransLogBLL.Create(vipCardTransLogEntity, tran);
            // 判断是扣还是增
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
        /// 更新积分
        /// </summary>
        /// <param name="pVipCardCode">卡号</param>
        /// <param name="pTotalAmount">可用积分</param>
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