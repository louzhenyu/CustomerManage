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
    /// 业务处理：  
    /// </summary>
    public partial class VipOrderSubRunObjectMappingBLL
    {
        private LoggingSessionInfo loggingSessionInfo; 
        private BasicUserInfo CurrentUserInfo;
        private VipOrderSubRunObjectMappingDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipOrderSubRunObjectMappingBLL(LoggingSessionInfo pUserInfo)
        {
            this.loggingSessionInfo = pUserInfo;
            this._currentDAO = new VipOrderSubRunObjectMappingDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipOrderSubRunObjectMappingEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipOrderSubRunObjectMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity,pTran);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipOrderSubRunObjectMappingEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public VipOrderSubRunObjectMappingEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipOrderSubRunObjectMappingEntity pEntity , IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity,pTran);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(VipOrderSubRunObjectMappingEntity pEntity )
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipOrderSubRunObjectMappingEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipOrderSubRunObjectMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity,pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID,pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipOrderSubRunObjectMappingEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities,pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipOrderSubRunObjectMappingEntity[] pEntities)
        { 
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            _currentDAO.Delete(pIDs,pTran);
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public VipOrderSubRunObjectMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
           return _currentDAO.Query(pWhereConditions,pOrderBys);
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipOrderSubRunObjectMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQuery(pWhereConditions,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public VipOrderSubRunObjectMappingEntity[] QueryByEntity(VipOrderSubRunObjectMappingEntity pQueryEntity, OrderBy[] pOrderBys)
        {
           return _currentDAO.QueryByEntity(pQueryEntity,pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<VipOrderSubRunObjectMappingEntity> PagedQueryByEntity(VipOrderSubRunObjectMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
           return _currentDAO.PagedQueryByEntity(pQueryEntity,pOrderBys,pPageSize,pCurrentPageIndex);
        }

        #endregion

        public void setJiKeGift(string UserID,string vipID)
        {
            //如果扫码用户有上线会员就不给扫码奖励。
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
                    Message = "集客反还积分:返回"
                }); 
                return;
            }

            ////查询集客奖励设置
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
            
            ////积分换算为金额
            //decimal GiftAmount = GiftIntegral / 100;
                        
            ////保存
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
            //            Message = "集客反积分:" + "集客信息有" + amountEntity.TotalAmount == null ? "00000" : amountEntity.TotalAmount.ToString()
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
            //            Message = "集客反还积分:" + ex.Message.ToString()
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
            //        Remark ="集客反积分兑换为余额"
            //    }
            //);


            //查询集客奖励设置
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
                Message = "集客反还积分:得到积分" + GiftIntegral.ToString()
            }); 

            //保存
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
                        Message = "集客反积分:" + "集客信息有" + integralEntity.EndIntegral == null ? "00000" : integralEntity.EndIntegral.ToString()
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
                        Message = "集客反还积分:" + ex.Message.ToString()
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
                    Message = "集客反还积分:" + ex.Message.ToString()
                }); 
            }
            
        }
    }
}