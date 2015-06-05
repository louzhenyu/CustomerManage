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
using System.Transactions;
using JIT.CPOS.Common;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity.Interface;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class CouponBLL
    {
        #region 根据ID获取优惠券列表

        /// <summary>
        /// 根据ID获取优惠券列表
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <returns></returns>
        public DataSet GetMyCouponList(string vipId)
        {
            return this._currentDAO.GetMyCouponList(vipId);
        }

        #endregion

        #region 获取订单使用的优惠券总计

        /// <summary>
        /// 获取订单使用的优惠券总计
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataSet OrderCouponSum(string vipId, string orderId)
        {
            return this._currentDAO.OrderCouponSum(vipId, orderId);
        }

        #endregion

        #region 订单使用的优惠券列表

        /// <summary>
        /// 订单使用的优惠券列表
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataSet OrderCouponList(string vipId, string orderId)
        {
            return this._currentDAO.OrderCouponList(vipId, orderId);
        }

        #endregion

        #region 订单中取消使用优惠券

        /// <summary>
        /// 订单中取消使用优惠券
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="couponId">优惠券ID</param>
        /// <returns></returns>
        public void CancelCouponMapping(string orderId, string couponId)
        {
            this._currentDAO.CancelCouponMapping(orderId, couponId);
        }

        #endregion

        #region 更新订单表数据

        /// <summary>
        /// 更新订单表数据
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public void CancelCouponOrder(string orderId)
        {
            this._currentDAO.CancelCouponOrder(orderId);
        }

        #endregion

        #region 计算订单使用的优惠券

        public DataSet CheckCouponForOrder(string vipId, string orderId, string couponId)
        {
            return this._currentDAO.CheckCouponForOrder(vipId, orderId, couponId);
        }

        #endregion

        #region 处理优惠券刮奖业务

        public void SetEventPrizes(string vipId, string eventId)
        {
            var tranHelper = new TransactionHelper(this.CurrentUserInfo);
            var tran = tranHelper.CreateTransaction();
            using (tran.Connection)
            {
                LLotteryLogBLL lotteryService = new LLotteryLogBLL(this.CurrentUserInfo);
                var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity() { EventId = eventId, VipId = vipId }, null);

                if (lotterys != null && lotterys.Length > 0)
                {
                    //更新
                    var lotteryEntity = lotterys.FirstOrDefault();
                    lotteryEntity.LotteryCount += 1;
                    lotteryService.Update(lotteryEntity, tran);
                }
                else
                {
                    //新增
                    var lotteryEntity = new LLotteryLogEntity()
                    {
                        LogId = CPOS.Common.Utils.NewGuid(),
                        VipId = vipId,
                        EventId = eventId,
                        LotteryCount = 1
                    };
                    lotteryService.Create(lotteryEntity, tran);
                }

                //1、判断用户是否中奖
                //2、用户中奖 添加优惠券表（Coupon）和优惠券用户关系表（VipCouponMapping）
                //3、added by zhangwei用户中奖类型为积分则更新用户积分 
                LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(this.CurrentUserInfo);
                var prize = winnerService.GetWinnerInfo(vipId, eventId);

                if (prize.Read())
                {
                    //取得奖品信息，看奖品类型是积分还是优惠券
                    var prizeMapping = winnerService.GetPrizeCouponTypeMapping(prize["PrizeWinnerID"].ToString(), tran);
                    if (prizeMapping.Tables[0].Rows.Count > 0)
                    {
                        //奖品如果是积分，则插入积分表
                        if (!string.IsNullOrEmpty(prizeMapping.Tables[0].Rows[0]["AwardPoints"].ToString()))
                        {
                            //新的积分方法 zhangwei2013-2-13
                            int IntegralSourceID = 16;
                            if (eventId.ToUpper() == "E5A304D716D14CD2B96560EBD2B6A29C")
                                IntegralSourceID = 16;
                            if (eventId.ToUpper() == "BFC41A8BF8564B6DB76AE8A8E43557BA")
                                IntegralSourceID = 17;
                            var objectID = prizeMapping.Tables[0].Rows[0]["PrizesID"].ToString();
                            if (!string.IsNullOrEmpty(objectID))
                            {
                                Loggers.DEFAULT.Debug(new DebugLogInfo
                                {
                                    Message = "抽奖成功:+IntegralSourceID：" + IntegralSourceID + "ClientID:" + CurrentUserInfo.ClientID + "VipID:" + vipId + "ObjectID:" + objectID
                                });
                                new VipIntegralBLL(CurrentUserInfo).ProcessPoint(IntegralSourceID,
                                                                                 CurrentUserInfo.ClientID, vipId,
                                                                                 objectID);
                            }
                            else
                            {
                                Loggers.DEFAULT.Debug(new DebugLogInfo
                                {
                                    Message = "抽奖失败:+IntegralSourceID：" + IntegralSourceID + "ClientID:" + CurrentUserInfo.ClientID + "VipID:" + vipId
                                });
                            }
                            /*
                            //插入积分
                            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(CurrentUserInfo);
                            VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                            decimal integralValue = decimal.Parse(prizeMapping.Tables[0].Rows[0]["AwardPoints"].ToString());
                            var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                       new VipIntegralEntity() { VipID = vipId }, null);
                            if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 ||
                                vipIntegralDataList[0] == null)
                            {

                                vipIntegralEntity.VipID = vipId;
                                vipIntegralEntity.InIntegral = integralValue; //累计积分
                                vipIntegralEntity.EndIntegral = integralValue; //积分余额
                                vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
                                vipIntegralBLL.Create(vipIntegralEntity, tran);
                            }
                            else
                            {
                                vipIntegralEntity = vipIntegralDataList[0];
                                vipIntegralEntity.VipID = vipId;
                                vipIntegralEntity.InIntegral = (vipIntegralEntity.InIntegral.HasValue ? vipIntegralEntity.InIntegral : 0) + integralValue; //累计积分
                                vipIntegralEntity.EndIntegral = (vipIntegralEntity.EndIntegral.HasValue ? vipIntegralEntity.EndIntegral : 0) + integralValue; //积分余额
                                vipIntegralEntity.ValidIntegral = (vipIntegralEntity.ValidIntegral.HasValue ? vipIntegralEntity.ValidIntegral : 0) + integralValue; // 当前有效积分
                                vipIntegralBLL.Update(vipIntegralEntity, false, tran);
                            }

                            //更新vip表积分记录
                            VipBLL vipBll = new VipBLL(CurrentUserInfo);
                            VipEntity vipEntity = vipBll.GetByID(vipId);
                            if (vipEntity != null)
                            {
                                vipEntity.Integration = (vipEntity.Integration.HasValue ? vipEntity.Integration.Value : 0) + integralValue;
                                vipBll.Update(vipEntity);
                            }


                            VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(CurrentUserInfo);
                            VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                            vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                            vipIntegralDetailEntity.VIPID = vipId;
                            vipIntegralDetailEntity.FromVipID = vipId;
                            vipIntegralDetailEntity.Integral = integralValue;
                            if (eventId.ToUpper() == "E5A304D716D14CD2B96560EBD2B6A29C")
                                vipIntegralDetailEntity.IntegralSourceID = "16";
                            if (eventId.ToUpper() == "BFC41A8BF8564B6DB76AE8A8E43557BA")
                                vipIntegralDetailEntity.IntegralSourceID = "17";

                            vipIntegralDetailBLL.Create(vipIntegralDetailEntity);
                             */

                        }//奖品如果是优惠券
                        else if (!string.IsNullOrEmpty(prizeMapping.Tables[0].Rows[0]["CouponTypeID"].ToString()))
                        {

                            var couponId = Utils.NewGuid();

                            //生成优惠券
                            this._currentDAO.CreateCoupon(vipId, eventId, couponId, tran);

                            //添加优惠券用户关系
                            VipCouponMappingBLL mappingService = new VipCouponMappingBLL(this.CurrentUserInfo);
                            mappingService.Create(new VipCouponMappingEntity
                            {
                                VipCouponMapping = Utils.NewGuid(),
                                VIPID = vipId,
                                CouponID = couponId,
                                UrlInfo = ""
                            }, tran);
                        }

                    }
                    //设置奖品为已兑换
                    var prizeWinner = winnerService.Query(new IWhereCondition[]
                            {
                                new EqualsCondition()
                                    {
                                        FieldName = "PrizeWinnerID",
                                        Value = prize["PrizeWinnerID"].ToString()
                                    }
                            }, null).FirstOrDefault();

                    if (prizeWinner != null)
                    {
                        prizeWinner.HasConvert = 1;
                        winnerService.Update(prizeWinner, false, tran);
                    }
                }

                tran.Commit();
            }
        }

        #endregion

        #region 推荐新人奖励
        public void RecommenderPrize(string VipID, string EventId)
        {
            _currentDAO.RecommenderPrize(VipID, EventId);
        }
        #endregion

        #region 获取推荐排行榜列表

        /// <summary>
        /// 获取推荐排行榜列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendList()
        {
            return this._currentDAO.GetRecommendList();
        }

        #endregion

        #region 获取推荐战绩

        /// <summary>
        /// 获取推荐战绩
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecord(string userId)
        {
            return this._currentDAO.GetRecommendRecord(userId);
        }

        #endregion

        #region 获取推荐战绩人员列表

        /// <summary>
        /// 获取推荐战绩人员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecordList(string userId)
        {
            return this._currentDAO.GetRecommendRecordList(userId);
        }

        #endregion

        public void UpdateVipRecommandTrace(string vipID, string higherVipId)
        {
            _currentDAO.UpdateVipRecommandTrace(vipID, higherVipId);
        }

        #region 判断是否已记录推荐关系
        public bool IfRecordedRecommendTrace(string vipID, string reCommandId)
        {
            bool result = true;
            object row = _currentDAO.IfRecordedRecommendTrace(vipID, reCommandId);
            if (row == null)
                result = false;

            Loggers.Debug(new DebugLogInfo() { Message = "result = " + result });
            return result;
        }
        #endregion

        #region 获取会员优惠券的列表
        /// <summary>
        /// 获取会员优惠券的列表
        /// </summary>
        /// <param name="vipID"></param>
        /// <param name="CouponTypeID"></param>
        /// <returns></returns>
        public DataSet GetCouponList(string vipID, string CouponTypeID)
        {
            return this._currentDAO.GetCouponList(vipID, CouponTypeID);
        }
        #endregion

        #region 获取会员有效优惠券的列表
        public CouponManagePagedSearchRD GetCouponList(GetCouponListRP getCouponListRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();

            DataSet dataSet = new DataSet();
            dataSet = this.GetCouponList(getCouponListRP.VipID, "");
            if (Utils.IsDataSetValid(dataSet))
            {
                var couponList = (from d in dataSet.Tables[0].AsEnumerable()
                                  where d["Status"].ToString() == "0"
                                  select new CouponManageEntity
                                  {
                                      CouponID = d["CouponID"].ToString(),
                                      CouponTypeName = d["CouponTypeName"].ToString(),
                                      CouponName = d["CouponName"].ToString(),
                                      CouponCode = d["CouponCode"].ToString(),
                                      BeginTime = d["BeginDate"].ToString() != "" ? Convert.ToDateTime(d["BeginDate"].ToString()).ToLongDateString().ToString() : "",
                                      EndTime = d["EndDate"].ToString() != "" ? Convert.ToDateTime(d["EndDate"].ToString()).ToLongDateString().ToString() : ""
                                  });

                rd.CouponList = couponList.ToArray();
            }
            return rd;
        }
        #endregion

        #region 获取详情信息
        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public DataSet GetCouponDetail(string couponID, string userID)
        {
            return this._currentDAO.GetCouponDetail(couponID, userID);
        }
        #endregion

        #region 使用优惠劵
        /// <summary>
        /// 使用优惠劵
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public int BestowCoupon(string couponID, string doorID)
        {
            return this._currentDAO.BestowCoupon(couponID, doorID);
        }
        #endregion

        /// <summary>
        /// 根据券类商品ID获取有效优惠券
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetCouponByItemId(string itemId)
        {
            return this._currentDAO.GetCouponByItemId(itemId);
        }

        #region 生成优惠券
        public EmptyRD GenerateCoupon(GenerateCouponRP generateCouponRP)
        {
            EmptyRD rd = new EmptyRD();
            string endTime = generateCouponRP.EndTime != null ? Convert.ToDateTime(generateCouponRP.EndTime).ToShortDateString() + " 23:59:59" : null;
            string result = this._currentDAO.GenerateCoupon(generateCouponRP.CouponTypeID, generateCouponRP.CouponName, generateCouponRP.BeginTime, endTime, generateCouponRP.Description, generateCouponRP.Qty);

            return rd;
        }
        #endregion

        #region 管理优惠券列表
        public CouponManagePagedSearchRD ManageCouponPagedSearch(CouponManagePagedSearchRP manageCouponSearchRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();
            DataSet dataSet = new DataSet();

            dataSet = _currentDAO.ManageCouponPagedSearch(manageCouponSearchRP.CouponTypeID, manageCouponSearchRP.CouponName, manageCouponSearchRP.CouponUseStatus, manageCouponSearchRP.CouponStatus, manageCouponSearchRP.BeginTime, manageCouponSearchRP.EndTime, manageCouponSearchRP.CouponCode, manageCouponSearchRP.Comment, manageCouponSearchRP.UseTime, manageCouponSearchRP.CreateByName, manageCouponSearchRP.UseEndTime, int.Parse(manageCouponSearchRP.PageIndex), int.Parse(manageCouponSearchRP.PageSize));
            if (dataSet.Tables.Count == 2)
            {
                var couponList = (from d in dataSet.Tables[0].AsEnumerable()
                                  select new CouponManageEntity
                                  {
                                      CouponID = d["CouponID"].ToString(),
                                      CouponTypeName = d["CouponTypeName"].ToString(),
                                      CouponName = d["CouponName"].ToString(),
                                      CouponCode = d["CouponCode"].ToString(),
                                      CouponUseStatus = d["CouponUseStatus"].ToString(),
                                      BeginTime = d["BeginDate"].ToString() != "" ? Convert.ToDateTime(d["BeginDate"].ToString()).ToLongDateString().ToString() : "",
                                      EndTime = d["EndDate"].ToString() != "" ? Convert.ToDateTime(d["EndDate"].ToString()).ToLongDateString().ToString() : "",
                                       CouponStatus = d["Status"].ToString(),
                                      IsDelete = d["IsDelete"].ToString(),
                                      CreateByName = d["CreateByName"].ToString(),
                                      Comment = d["Comment"].ToString(),
                                      UseTime = d["UseTime"].ToString() != "" ? Convert.ToDateTime(d["UseTime"].ToString()).ToLongDateString().ToString() : "" 
                                  });

                rd.CouponList = couponList.ToArray();
                rd.TotalPage = int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(dataSet.Tables[1].Rows[0][1].ToString());
            }

            return rd;
        }
        #endregion

        #region 导出分发记录 2014-10-10
        public DataTable GetExportData(CouponManagePagedSearchRP manageCouponSearchRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();
            DataSet dataSet = new DataSet();
            int pageSize = 999999999;   //int.Parse(manageCouponSearchRP.PageSize)
            int pageIndex = 0;  //int.Parse(manageCouponSearchRP.PageIndex)
            dataSet = _currentDAO.ManageCouponPagedSearch(manageCouponSearchRP.CouponTypeID, manageCouponSearchRP.CouponName, manageCouponSearchRP.CouponUseStatus, manageCouponSearchRP.CouponStatus, manageCouponSearchRP.BeginTime, manageCouponSearchRP.EndTime, manageCouponSearchRP.CouponCode, manageCouponSearchRP.Comment, manageCouponSearchRP.UseTime, manageCouponSearchRP.CreateByName, manageCouponSearchRP.UseEndTime, pageIndex, pageSize);

            DataTable dataTable = dataSet.Tables[0];

            //删除不需要的列
            dataTable.Columns.Remove("CouponID");
            dataTable.Columns.Remove("CouponDesc");
            dataTable.Columns.Remove("CouponTypeID");
            dataTable.Columns.Remove("BeginDate");
            dataTable.Columns.Remove("EndDate");
            dataTable.Columns.Remove("CouponUrl");
            dataTable.Columns.Remove("ImageUrl");
            dataTable.Columns.Remove("Status");

            dataTable.Columns.Remove("CreateTime");
            dataTable.Columns.Remove("CreateBy");
            dataTable.Columns.Remove("LastUpdateTime");
            dataTable.Columns.Remove("LastUpdateBy");
            dataTable.Columns.Remove("IsDelete");
            dataTable.Columns.Remove("CouponPwd");
            dataTable.Columns.Remove("CollarCardMode");
            dataTable.Columns.Remove("CustomerID");

            dataTable.Columns.Remove("IsDel");
            dataTable.Columns.Remove("CouponUseStatus");
            dataTable.Columns.Remove("CoupnName");
            dataTable.Columns.Remove("DoorID");

            for (int i = 1; i <= 50; i++)
            {
                dataTable.Columns.Remove("Col" + i);
            }

            dataTable.Columns["RowNo"].ColumnName = "序号";
            dataTable.Columns["CouponCode"].ColumnName = "优惠券号";
            dataTable.Columns["CouponName"].ColumnName = "优惠券名称";
            dataTable.Columns["CreateByName"].ColumnName = "核销人";
            dataTable.Columns["Comment"].ColumnName = "组单号";
            dataTable.Columns["UseTime"].ColumnName = "核销日期";
            dataTable.Columns["CouponTypeName"].ColumnName = "优惠券类型";

            if (dataSet != null)
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 导出分发记录 2014-10-10
        public DataTable GetExportBindData(CouponBindLogPagedSearchRP couponBindLogPagedSearchRP)
        {
            DataSet dataSet = new DataSet();
            string  pageSize = "999999999";   //int.Parse(manageCouponSearchRP.PageSize)
            string pageIndex = "0";  //int.Parse(manageCouponSearchRP.PageIndex)
            dataSet = this._currentDAO.BindCouponLog(couponBindLogPagedSearchRP.CouponTypeID, couponBindLogPagedSearchRP.CouponName, couponBindLogPagedSearchRP.CouponCode, couponBindLogPagedSearchRP.VipCriteria, couponBindLogPagedSearchRP.BindingBeginTime, couponBindLogPagedSearchRP.BindingEndTime, couponBindLogPagedSearchRP.Operator, pageIndex, pageSize);

            DataTable dataTable = dataSet.Tables[0];

            //删除不需要的列
            dataTable.Columns.Remove("VipCouponMapping");
            dataTable.Columns.Remove("VIPID");
            dataTable.Columns.Remove("CouponID");
            dataTable.Columns.Remove("UrlInfo");
            dataTable.Columns.Remove("IsDelete");
            dataTable.Columns.Remove("LastUpdateBy");
            dataTable.Columns.Remove("LastUpdateTime");
            dataTable.Columns.Remove("CreateBy");

            //给列命名
            dataTable.Columns["RowNo"].ColumnName = "序号";
            dataTable.Columns["CouponCode"].ColumnName = "优惠券号";
            dataTable.Columns["CouponName"].ColumnName = "优惠券名称";
            dataTable.Columns["Operator"].ColumnName = "操作人";
            dataTable.Columns["VipName"].ColumnName = "会员名";
            dataTable.Columns["CreateTime"].ColumnName = "分发日期";
            dataTable.Columns["CouponTypeName"].ColumnName = "优惠券类型";

            if (dataSet != null)
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 修改优惠券编号 2014-9-24 update
        public SuccessResponse<IAPIResponseData> SetCouponCode(SetCouponCodeRP setCouponCodeRP)
        {
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponCode", Value = setCouponCodeRP.CouponCode }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                }, null);
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();
            if (coupon.Length == 0)
            {
                this._currentDAO.Update(new CouponEntity() { CouponID = setCouponCodeRP.CouponID, CouponCode = setCouponCodeRP.CouponCode }, false);
                sr.Message = "更新成功！";
            }
            else
            {
                sr.Message = "系统已经存在了该编号！";
            }

            return sr;
        }
        #endregion

        #region 修改优惠券状态为删除 2014-9-25 update
        public SuccessResponse<IAPIResponseData> SetCouponStates(SetCouponCodeRP setCouponCodeRP)
        {
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponID", Value = setCouponCodeRP.CouponIDs[0] }
                    , new EqualsCondition() { FieldName = "IsDelete", Value = setCouponCodeRP.IsDelete}
                }, null);
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();
            if (coupon.Length == 0)
            {
                //逻辑删除或恢复优惠券
                this._currentDAO.DeleteNew(setCouponCodeRP.CouponIDs[0], null, Convert.ToInt32(setCouponCodeRP.IsDelete));

                sr.Message = "更新成功！";
            }
            else
            {
                sr.Message = "更新失败！";
            }

            return sr;
        }
        #endregion

        #region 分发优惠券
        public object BindCoupon(BindCouponRP bindCouponRP)
        {
            var rd = new EmptyRD();
            ErrorResponse er = new ErrorResponse();
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();

            //If coupon exists
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponCode", Value = bindCouponRP.CouponCode }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                    , new EqualsCondition() { FieldName = "Status", Value = "0"}
                    , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                }, null);

            if (coupon.Length == 0)
            {
                er.Message = "该优惠券编号不存在或已被使用！";
            }
            else
            {
                //if coupon bound
                VipCouponMappingBLL vipCouponMappingBLL = new VipCouponMappingBLL(CurrentUserInfo);
                var mapping = vipCouponMappingBLL.Query(
                    new IWhereCondition[] {
                        new EqualsCondition() { FieldName = "CouponID", Value = coupon[0].CouponID}
                        , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                    }, null);

                if (mapping.Length > 0)
                {
                    er.Message = "该优惠券已分发！";
                }
                else
                {
                    //bind coupon
                    vipCouponMappingBLL.Create(
                        new VipCouponMappingEntity()
                        {
                            VipCouponMapping = Guid.NewGuid().ToString(),
                            VIPID = bindCouponRP.VipID,
                            CouponID = coupon[0].CouponID,
                            IsDelete = 0,
                            CreateBy = CurrentUserInfo.UserID,
                            CreateTime = DateTime.Now
                        });

                    sr.Message = "分发成功！";
                }
            }

            object result;
            if (!string.IsNullOrEmpty(er.Message))
                result = er;
            else
                result = sr;

            return result;
        }
        #endregion

        #region 核销优惠券
        public object WriteOffCoupon(WriteOffCouponRP writeOffCouponRP)
        {
            ErrorResponse er = new ErrorResponse();
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();

            //If coupon exists
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponID", Value = writeOffCouponRP.CouponID }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                    , new EqualsCondition() { FieldName = "Status", Value = "0"}
                    , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                }, null);

            if (coupon.Length == 0)
            {
                er.Message = "该优惠券不存在或已被使用！";
            }
            else
            {
                //更新优惠券状态为已使用
                //write off coupon
                this._currentDAO.Update(
                     new CouponEntity()
                     {
                         CouponID = writeOffCouponRP.CouponID,
                         Status = 1,
                         LastUpdateBy = CurrentUserInfo.UserID,
                         //Col1 = writeOffCouponRP.Comment,
                         //Col2 = DateTime.Now.ToString(),
                         //Col3 = CurrentUserInfo.UserID,
                         LastUpdateTime = DateTime.Now
                     }, false);
                //核销之后去插入数据
                writeOffCouponRP.Comment = string.IsNullOrEmpty(writeOffCouponRP.Comment) ? "后台核销电子券" : writeOffCouponRP.Comment;
                var vipcouponMappingBll = new VipCouponMappingBLL(CurrentUserInfo);
                var vipcouponmappingList = vipcouponMappingBll.QueryByEntity(new VipCouponMappingEntity()
                {
                    CouponID = writeOffCouponRP.CouponID
                }, null);
                this._currentDAO.UpdateCouponUse(writeOffCouponRP.CouponID, writeOffCouponRP.Comment, vipcouponmappingList[0].VIPID, CurrentUserInfo.UserID, CurrentUserInfo.CurrentUserRole.UnitId, CurrentUserInfo.ClientID);
                sr.Message = "核销成功！";
            }

            object result;
            if (!string.IsNullOrEmpty(er.Message))
                result = er;
            else
                result = sr;

            return result;
        }
        #endregion

        #region 分发优惠券记录
        public CouponBindLogPagedSearchRD BindCouponLog(CouponBindLogPagedSearchRP couponBindLogPagedSearchRP)
        {
            CouponBindLogPagedSearchRD couponBindLogPagedSearchRD = new CouponBindLogPagedSearchRD();

            DataSet dataSet = this._currentDAO.BindCouponLog(couponBindLogPagedSearchRP.CouponTypeID, couponBindLogPagedSearchRP.CouponName, couponBindLogPagedSearchRP.CouponCode, couponBindLogPagedSearchRP.VipCriteria, couponBindLogPagedSearchRP.BindingBeginTime, couponBindLogPagedSearchRP.BindingEndTime, couponBindLogPagedSearchRP.Operator, couponBindLogPagedSearchRP.PageIndex, couponBindLogPagedSearchRP.PageSize);

            if (Utils.IsDataSetValid(dataSet))
            {
                var list = DataTableToObject.ConvertToList<BindCouponEntity>(dataSet.Tables[0]);
                couponBindLogPagedSearchRD.CouponList = list.ToArray();

                couponBindLogPagedSearchRD.TotalPage = int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
                couponBindLogPagedSearchRD.TotalCount = int.Parse(dataSet.Tables[1].Rows[0][1].ToString());
            }

            if (couponBindLogPagedSearchRD.CouponList != null)
            {
                foreach (BindCouponEntity item in couponBindLogPagedSearchRD.CouponList)
                {
                    item.CreateTime = item.CreateTime != "" ? Convert.ToDateTime(item.CreateTime).ToLongDateString().ToString() : "";
                }
            }

            return couponBindLogPagedSearchRD;
        }
        #endregion

        /// <summary>
        /// 批量生成优惠券
        /// </summary>
        /// <param name="htCouponInfo"></param>
        /// <returns></returns>
        public void GenerateCoupon(Hashtable htCouponInfo)
        {
           this._currentDAO.GenerateCoupon(htCouponInfo);
        }
    }


    #region RequestParameter

    public class GenerateCouponRP : IAPIRequestParameter
    {
        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponTypeID))
                throw new APIException(201, "优惠券类型不能为空！");

            if (string.IsNullOrEmpty(CouponName))
                throw new APIException(202, "优惠券名称不能为空！");

            if (string.IsNullOrEmpty(Qty) || int.Parse(Qty) <= 0)
                throw new APIException(203, "数量必须大于0！");
        }
    }

    public class CouponManagePagedSearchRP : IAPIRequestParameter
    {
        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string CouponCode { get; set; }
        public string Comment { get; set; } //组单号
        public string CreateByName { get; set; } //核销人
        public string UseTime { get; set; } //核销时间
        public string UseEndTime { get; set; } //核销结束时间

        //public 
        public void Validate()
        {
            if (!string.IsNullOrEmpty(BeginTime))
            {
                try
                {
                    DateTime.Parse(BeginTime);
                }
                catch (Exception)
                {
                    throw new APIException(201, "开始日期格式错误！");
                }
            }

            if (!string.IsNullOrEmpty(EndTime))
            {
                try
                {
                    DateTime.Parse(EndTime);
                }
                catch (Exception)
                {
                    throw new APIException(202, "结束日期格式错误！");
                }
            }

            try
            {
                int.Parse(PageIndex);
            }
            catch (Exception)
            {
                throw new APIException(203, "页码格式错误！");
            }

            try
            {
                int.Parse(PageSize);
            }
            catch (Exception)
            {
                throw new APIException(204, "页大小格式错误！");
            }
        }
    }

    public class CouponBindLogPagedSearchRP : CouponManagePagedSearchRP
    {
        public string BindingBeginTime { get; set; }
        public string BindingEndTime { get; set; }
        public string Operator { get; set; }
        public string VipCriteria { get; set; }

        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string CouponCode { get; set; }
        public string Comment { get; set; } //组单号
        public string CreateByName { get; set; } //核销人
        public string UseTime { get; set; } //核销时间
        public string UseEndTime { get; set; } //核销结束时间



        new public void Validate()
        {
            //base.Validate();

            if (!string.IsNullOrEmpty(BindingBeginTime))
            {
                try
                {
                    DateTime.Parse(BindingBeginTime);
                }
                catch (Exception)
                {
                    throw new APIException(301, "开始日期格式错误！");
                }
            }

            if (!string.IsNullOrEmpty(BindingEndTime))
            {
                try
                {
                    DateTime.Parse(BindingEndTime);
                }
                catch (Exception)
                {
                    throw new APIException(302, "结束日期格式错误！");
                }
            }
        }
    }

    public class SetCouponCodeRP : IAPIRequestParameter
    {
        public string CouponID { get; set; }
        public string CouponCode { get; set; }
        public string IsDelete { get; set; }  //卡片是否为正常状态还是废弃状态
        public string[] CouponIDs { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponID))
            {
                throw new APIException(201, "优惠券ID不能为空！");
            }

            if (string.IsNullOrEmpty(CouponCode))
            {
                throw new APIException(202, "优惠券编号不能为空！！");
            }
        }
    }

    public class BindCouponRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        public string CouponCode { get; set; }
        //public 
        public void Validate()
        {
            if (string.IsNullOrEmpty(VipID))
            {
                throw new APIException(201, "会员ID不能为空！");
            }

            if (string.IsNullOrEmpty(CouponCode))
            {
                throw new APIException(202, "优惠券编号不能为空！！");
            }
        }
    }

    public class GetCouponListRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(VipID))
            {
                throw new APIException(201, "会员ID不能为空！");
            }
        }
    }

    public class WriteOffCouponRP : IAPIRequestParameter
    {
        public string CouponID { get; set; }
        /// <summary>
        /// 组单号
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        //public string UnitID { get; set; }
        /// <summary>
        /// 核销店员ID
        /// </summary>
        //public string WriteOffUserID { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponID))
            {
                throw new APIException(201, "优惠券ID不能为空！");
            }
        }
    }

    #endregion

    #region ResponseData

    public class CouponManagePagedSearchRD : IAPIResponseData
    {
        public CouponManageEntity[] CouponList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class CouponManageEntity
    {
        public string CouponID { get; set; }
        public string CouponTypeName { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string IsDelete { get; set; }
        public string CreateByName { get; set; }
        public string Comment { get; set; }
        public string UseTime { get; set; }

    }

    public class CouponBindLogPagedSearchRD : IAPIResponseData
    {
        public BindCouponEntity[] CouponList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class BindCouponEntity : CouponManageEntity
    {
        public string VipName { get; set; }
        public string Operator { get; set; }
        public string CreateTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
    }

    #endregion

    #region 核销时创建订单类
    /// <summary>
    /// 返回对象
    /// </summary>
    public class setOrderInfoRespData
    {
        public setOrderInfoRespContentData content { get; set; }
    }

    /// <summary>
    /// 返回的具体业务数据对象
    /// </summary>
    public class setOrderInfoRespContentData
    {
        public string orderId { get; set; }
    }

    #endregion
}