using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// VipGateway 的摘要说明
    /// </summary>
    public class VipGateway : BaseGateway
    {
        #region 获取会员积分和余额
        /// <summary>
        /// 获取会员积分和余额
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetVipIntegral(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipIntegralRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var rd = new GetVipIntegralRD();
            var bll = new VipBLL(loggingSessionInfo);
            var skuIdAndQty = rp.Parameters.SkuIdAndQtyList;

            if (skuIdAndQty == null)
            {
                throw new APIException("缺少参数【skuIdAndQty】或参数值为空") { ErrorCode = 135 };
            }

            //1.根据skuId和数量获取该商品的积分和金额
            string skuIdList = skuIdAndQty.Aggregate("",
                (current, skuIdAndQtyInfo) =>
                    current +
                    (skuIdAndQtyInfo.SkuId + "," + skuIdAndQtyInfo.Qty.ToString(CultureInfo.InvariantCulture) + ";"));

            var totalIntegral = bll.GetIntegralBySkuId(skuIdList);
            var totalPayAmount = bll.GetTotalSaleAmountBySkuId(skuIdList);
            //3.获取积分与金额的兑换比例
            var integralAmountPre = bll.GetIntegralAmountPre(rp.CustomerID);
            if (integralAmountPre == 0)
            {
                integralAmountPre = (decimal)0.01;
            }

            //2.获取会员的积分和账户余额
            var vipIntegralbll = new VipIntegralBLL(loggingSessionInfo);

            var vipIntegralEntity = vipIntegralbll.GetByID(rp.UserID);


            if (vipIntegralEntity == null)
            {
                rd.Integral = 0;
                rd.IntegralAmount = 0;
                rd.IntegralDesc = "无积分可兑换";
            }
            else
            {
                decimal validIntegral = vipIntegralEntity.ValidIntegral ?? 0;
                rd.Integral = validIntegral > totalIntegral ? totalIntegral : validIntegral;

                rd.IntegralAmount = rd.Integral * integralAmountPre;

                rd.IntegralDesc = "使用积分" + rd.Integral.ToString("0") + ",可兑换"
                                  + rd.IntegralAmount.ToString("0.00") + "元";

            }
            var vipEndAmount = bll.GetVipEndAmount(rp.UserID);

            //rd.VipEndAmount = totalPayAmount > vipEndAmount ? vipEndAmount : totalPayAmount;
            rd.VipEndAmount = vipEndAmount;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
            //4.将此笔订单交易的总积分与会员拥有的总积分比较，两者取最小值
            //5.将此笔订单交易的总金额与会员的账户余额比较，两者取最小值
        }
        #endregion

        #region 获取优惠券
        public string GetVipCoupon(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipIntegralRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var skuIdAndQty = rp.Parameters.SkuIdAndQtyList;

            if (skuIdAndQty == null)
            {
                throw new APIException("缺少参数【skuIdAndQty】或参数值为空") { ErrorCode = 135 };
            }
            //1.根据skuId和数量获取该商品的积分和金额
            string skuIdList = skuIdAndQty.Aggregate("",
                (current, skuIdAndQtyInfo) =>
                    current + (skuIdAndQtyInfo.SkuId + ","
                               + skuIdAndQtyInfo.Qty.ToString(CultureInfo.InvariantCulture) + ";"));

            var bll = new VipBLL(loggingSessionInfo);
            //应付金额
            var totalPayAmount = bll.GetTotalSaleAmountBySkuId(skuIdList);
            var rd = new GetVipCouponRD();

            var ds = bll.GetVipCouponDataSet(rp.UserID, totalPayAmount);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new CouponInfo()
                {
                    CouponId = t["CouponID"].ToString(),
                    CouponAmount = Convert.ToDecimal(t["parValue"]),
                    CouponDesc = t["CouponDesc"].ToString(),
                    DisplayIndex = Convert.ToInt32(t["displayIndex"]),
                    EnableFlag = Convert.ToInt32(t["EnableFlag"]),
                    ValidDateDesc = t["ValidDateDesc"].ToString(),
                    StartDate = t["BeginDate"].ToString(),
                    EndDate = t["EndDate"].ToString()
                });
                rd.CouponList = temp.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 设置支付密码
        public string SetVipPayPassword(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetVipPayPasswordRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = 121 };
            }
            var rd = new EmptyResponseData();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var mobile = rp.Parameters.Mobile;
            var authCode = rp.Parameters.AuthCode;
            var password = rp.Parameters.Password;
            var passwordAgain = rp.Parameters.PasswordAgain;

            var bll = new VipBLL(loggingSessionInfo);
            var codebll = new RegisterValidationCodeBLL(loggingSessionInfo);

            #region 判断该会员账户是否有绑定手机号

            var vipEntity = bll.GetByID(rp.UserID);

            if (vipEntity == null)
            {
                throw new APIException("无效的会员ID【VipId】") { ErrorCode = 134 };
            }
            if (!string.IsNullOrEmpty(vipEntity.Phone) && vipEntity.Phone != mobile)
            {
                throw new APIException("请使用注册时的手机号码找回密码") { ErrorCode = 134 };
            }
            //如果数据库中该会员的手机号为空，则将手机号更新为传过来的Mobile
            if (string.IsNullOrEmpty(vipEntity.Phone))
            {
                throw new APIException("该会员暂无开通账户") { ErrorCode = 134 };
            }

            #endregion

            #region 验证验证码

            var entity = codebll.GetByMobile(rp.Parameters.Mobile);
            if (entity == null)
                throw new APIException("未找到此手机的验证信息") { ErrorCode = 130 };
            if (entity.IsValidated.Value == 1)
                throw new APIException("此验证码已被使用") { ErrorCode = 131 };
            if (entity.Expires.Value < DateTime.Now)
                throw new APIException("此验证码已失效") { ErrorCode = 132 };
            if (entity.Code != rp.Parameters.AuthCode)
                throw new APIException("验证码不正确") { ErrorCode = 133 };

            #endregion

            var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
            var vipAmountEntity = vipAmountBll.GetByID(rp.UserID);

            if (password != passwordAgain)
            {
                throw new APIException("前后密码不一致，请重新输入") { ErrorCode = 137 };
            }

            if (vipAmountEntity == null)
            {
                vipAmountEntity.VipId = rp.UserID;
                vipAmountEntity.PayPassword = password;
                vipAmountEntity.IsLocking = 0;
                vipAmountBll.Create(vipAmountEntity);
            }
            else
            {
                if (vipAmountEntity.IsLocking == 1)
                    throw new APIException("账户已锁定，请先解锁") { ErrorCode = 136 };
                vipAmountEntity.PayPassword = password;
                vipAmountBll.Update(vipAmountEntity);
            }


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 校验支付密码
        public string CheckVipPayPassword(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<CheckVipPayPasswordRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.Password))
            {
                throw new APIException("请求参数中缺少Password或值为空.") { ErrorCode = 121 };
            }

            var rd = new EmptyResponseData();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var vipbll = new VipBLL(loggingSessionInfo);
            var vipEntity = vipbll.GetByID(rp.UserID);
            if (vipEntity == null)
            {
                throw new APIException("无效的会员ID【VipId】") { ErrorCode = 121 };
            }


            var bll = new VipAmountBLL(loggingSessionInfo);
            var vipAmountEntity = bll.GetByID(rp.UserID);

            if (vipAmountEntity == null)
            {
                throw new APIException("该会员暂无绑定账户") { ErrorCode = 121 };
            }
            else
            {
                if (vipAmountEntity.PayPassword != rp.Parameters.Password)
                {
                    throw new APIException("密码不正确") { ErrorCode = 122 };
                }
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 获取会员信息
        public string GetVipInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");


            var rd = new GetVipInfoRD();
            var bll = new VipBLL(loggingSessionInfo);
            var vipEntity = bll.GetByID(rp.UserID);
            if (vipEntity == null)
            {
                throw new APIException("无效的会员ID【VipId】") { ErrorCode = 121 };
            }
            else
            {
                rd.VipId = rp.UserID;
                rd.Status = vipEntity.Status ?? 0;
            }
            var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
            var vipAmountEntity = vipAmountBll.GetByID(rp.UserID);
            if (vipAmountEntity != null)
            {
                rd.LockFlag = vipAmountEntity.IsLocking ?? 1;
                rd.PasswordFlag = string.IsNullOrEmpty(vipAmountEntity.PayPassword) ? 0 : 1;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 订单提交

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetOrderStatus(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetOrderStatusRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            decimal discountAmount = 0;

            #region 原订单提交接口，没有关于阿拉丁的部分

            if (rp.OpenID == "" || string.IsNullOrEmpty(rp.OpenID))
            {
                throw new APIException("OpenID不能为空【OpenID】") { ErrorCode = 121 };
            }

            var orderId = rp.Parameters.OrderId;
            var status = rp.Parameters.Status;//当前状态

            if (orderId == "" || string.IsNullOrEmpty(orderId))
            {
                throw new APIException("订单标识不能为空【OrderId】") { ErrorCode = 121 };
            }
            if (status == "" || string.IsNullOrEmpty(status))
            {
                throw new APIException("订单状态不能为空【Status】") { ErrorCode = 121 };
            }
            var service = new ShoppingCartBLL(loggingSessionInfo);
            var inoutService = new InoutService(loggingSessionInfo);

            IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();

            using (tran.Connection)
            {
                try
                {
                    var flag = inoutService.UpdateOrderDeliveryStatus(orderId, status,
                        Utils.GetNow(), null, (SqlTransaction)tran);
                    if (!flag)
                    {
                        throw new APIException("更新订单状态失败") { ErrorCode = 103 };
                    }

            #endregion

                    #region 新版本订单修改部分

                    var couponFlag = rp.Parameters.CouponFlag;
                    var couponId = rp.Parameters.CouponId;
                    var integralFlag = rp.Parameters.IntegralFlag;
                    var vipEndAmountFlag = rp.Parameters.VipEndAmountFlag;
                    var vipEndAmount = rp.Parameters.VipEndAmount;


                    if (integralFlag == 1)
                    {
                        var vipIntegralBll = new VipIntegralBLL(loggingSessionInfo);


                        //获取该订单可用的总积分

                        var orderDetail = new TInoutDetailBLL(loggingSessionInfo);

                        var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity()
                        {
                            order_id = orderId
                        }, null);

                        if (orderDetailList == null || orderDetailList.Length == 0)
                        {
                            throw new APIException("该订单没有商品") { ErrorCode = 121 };
                        }
                        var str = orderDetailList.Aggregate("", (i, j) =>
                        {
                            i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                            return i;
                        });

                        var bll = new VipBLL(loggingSessionInfo);

                        //判断该积分和vip个人积分的大小
                        var vipIntegralEntity = vipIntegralBll.GetByID(rp.UserID);

                        //商品可兑换的总积分
                        var vipIntegral = bll.GetIntegralBySkuId(str);
                        //个人的有效积分
                        var validIntegral = vipIntegralEntity.ValidIntegral ?? 0;

                        var integralPre = bll.GetIntegralAmountPre(rp.CustomerID);

                        if (integralPre == 0)
                        {
                            integralPre = 0.01M;
                        }


                        var point = validIntegral > vipIntegral ? vipIntegral : validIntegral;

                        discountAmount = discountAmount +
                                         integralPre * point;

                        //获取积分与金额的百分比
                        //获取积分金额


                        const int sourceId = 20; //消费
                        vipIntegralBll.ProcessPoint(sourceId, rp.CustomerID, rp.UserID, orderId, (SqlTransaction)tran,
                            null, -point, null, rp.UserID);
                    }

                    #endregion

                    #region 优惠券修改

                    if (couponFlag == 1)
                    {
                        #region 判断优惠券是否是该会员的

                        var vipcouponMappingBll = new VipCouponMappingBLL(loggingSessionInfo);

                        var vipcouponmappingList = vipcouponMappingBll.QueryByEntity(new VipCouponMappingEntity()
                        {
                            VIPID = rp.UserID,
                            CouponID = rp.Parameters.CouponId
                        }, null);

                        if (vipcouponmappingList == null || vipcouponmappingList.Length == 0)
                        {
                            throw new APIException("此张优惠券不是该会员的") { ErrorCode = 103 };
                        }

                        #endregion

                        #region 判断优惠券是否有效

                        var couponBll = new CouponBLL(loggingSessionInfo);

                        var couponEntity = couponBll.GetByID(couponId);

                        if (couponEntity == null)
                        {
                            throw new APIException("无效的优惠券") { ErrorCode = 103 };
                        }

                        if (couponEntity.Status == 1)
                        {
                            throw new APIException("优惠券已使用") { ErrorCode = 103 };
                        }

                        if (couponEntity.EndDate < DateTime.Now)
                        {
                            throw new APIException("优惠券已过期") { ErrorCode = 103 };
                        }
                        var couponTypeBll = new CouponTypeBLL(loggingSessionInfo);
                        var couponTypeEntity = couponTypeBll.GetByID(couponEntity.CouponTypeID);

                        if (couponTypeEntity == null)
                        {
                            throw new APIException("无效的优惠券类型") { ErrorCode = 103 };
                        }

                        #endregion

                        discountAmount = discountAmount + couponTypeEntity.ParValue ?? 0;

                        #region Insert优惠券与订单的映射表

                        var tOrderCouponMapping = new TOrderCouponMappingBLL(loggingSessionInfo);

                        var tOrderCouponMappingEntity = new TOrderCouponMappingEntity
                        {
                            MappingId = Utils.NewGuid(),
                            OrderId = orderId,
                            CouponId = couponId
                        };

                        tOrderCouponMapping.Create(tOrderCouponMappingEntity, tran);

                        #endregion

                        #region Update Coupon Status = 1

                        couponEntity.Status = 1;
                        couponBll.Update(couponEntity, tran);

                        #endregion
                    }

                    #endregion

                    #region 余额修改

                    //会员金额变化明细表VipAmountDetail
                    //如果使用会员余额，向VipAmountDetail 插入一条数据 然后更新会员的可用余额，
                    if (vipEndAmountFlag == 1)
                    {
                        var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                        var vipAmountEntity = vipAmountBll.GetByID(rp.UserID);
                        if (rp.ChannelId != "4")//自阿拉丁平台不执行判断 add by Henry 2014-10-15
                        { 
                            #region 判断该会员账户是否被冻结
                            if (vipAmountEntity.IsLocking == 1)
                            {
                                throw new APIException("账户已被冻结，请先解冻") { ErrorCode = 103 };
                            }

                            #endregion

                            #region 判断该会员的账户余额是否大于本次使用的余额

                            if (vipAmountEntity.EndAmount < vipEndAmount)
                            {
                                throw new APIException(string.Format("账户余额不足，当前余额为【{0}】", vipAmountEntity.EndAmount))
                                {
                                    ErrorCode = 103
                                };
                            }

                            #endregion

                            #region Update VipAmount.EndAmount 

                            vipAmountEntity.EndAmount = vipAmountEntity.EndAmount - vipEndAmount;
                            vipAmountEntity.OutAmount = vipAmountEntity.OutAmount + vipEndAmount;

                            vipAmountBll.Update(vipAmountEntity, tran);

                            #endregion
                        }

                        #region Insert VipAmountDetail

                        var vipAmountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);
                        var vipAmountDetailEntity = new VipAmountDetailEntity
                        {
                            VipAmountDetailId = Guid.NewGuid(),
                            ObjectId = orderId,
                            VipId = rp.UserID,
                            Amount =rp.ChannelId == "4" ? (vipEndAmount/0.01M):vipEndAmount,//如果來自阿拉丁平台，換算成阿拉幣 update by Henry 2014-10-14
                            AmountSourceId = rp.ChannelId == "4" ? "11" : "1"  //如果来自阿拉丁平台，来源=阿拉币消费(11)；否来源=订单余额支付(1) update by Henry 2014-10-14
                        };

                        vipAmountDetailBll.Create(vipAmountDetailEntity, tran);

                        #region 同步阿拉币更新记录到ALD add by Henry 2014-10-14
                        if (rp.ChannelId == "4")
                        {
                            //判断是否是ALD的订单
                            var orderInfo = inoutService.GetInoutInfoById(orderId);
                            if (orderInfo.Field3 == "1")
                            {
                                var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                                var request = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest()
                                {
                                    Parameters =
                                        new
                                        {
                                            MemberId = new Guid(rp.UserID),
                                            Amount = vipEndAmount/0.01M,
                                            AmountSourceId = "11",
                                            ObjectId = orderId,
                                            IsALD = 1
                                        }
                                };
                                try
                                {
                                    var resstr = JIT.Utility.Web.HttpClient.GetQueryString(url, string.Format("Action=AddAmountDetail&ReqContent={0}", request.ToJSON()));
                                    Loggers.Debug(new DebugLogInfo() { Message = "调用ALD更改状态接口:" + resstr });
                                    var res = resstr.DeserializeJSONTo<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                                }
                                catch (Exception ex)
                                {
                                    Loggers.Exception(new ExceptionLogInfo(ex));
                                    throw new Exception("调用ALD平台失败:" + ex.Message);
                                }
                            }

                        }
                        #endregion

                        discountAmount = discountAmount + vipEndAmount;

                    }
                        #endregion

                    #endregion

                    var tInoutBll = new TInoutBLL(loggingSessionInfo);

                    var tInoutEntity = tInoutBll.GetByID(orderId);

                    if (tInoutEntity == null)
                    {
                        throw new APIException("此订单Id无效") { ErrorCode = 103 };
                    }
                    //tInoutEntity.ActualAmount = 1;

                    //根据订单得到应付金额 
                    //分别计算出，优惠券金额，余额，积分抵挡的金额
                    //实付金额 = 应付金额 - 优惠券金额 - 余额 - 积分抵挡的金额

                    if (tInoutEntity.TotalAmount < discountAmount)
                        tInoutEntity.ActualAmount = 0;
                    else
                        tInoutEntity.ActualAmount = tInoutEntity.TotalAmount - discountAmount;

                    //如果实付金额 = 各种优惠活动的综合 设置付款状态=1【已付款】
                    if (tInoutEntity.ActualAmount == discountAmount || tInoutEntity.ActualAmount == 0)
                    {
                        tInoutEntity.Field1 = "1";
                    }

                    tInoutEntity.Remark = rp.Parameters.Remark;//备注

                    tInoutBll.Update(tInoutEntity, tran);

                    //TO DO 提交订单扩展表 
                    //这个订单扩展表用于【人人销售，分润数据】
                    //add by donal 2014-10-14 09:40:12
                    #region
                    //TInoutExpandBLL intoutExpandBll = new TInoutExpandBLL(loggingSessionInfo);
                    //TInoutExpandEntity inoutExpandEntity = new TInoutExpandEntity();
                    //inoutExpandEntity.OrderId = orderId;
                    //inoutExpandEntity.APPChannelID = Convert.ToInt32(rp.ChannelId);
                    //inoutExpandEntity.OSSId = rp.Parameters.OSSId;
                    //inoutExpandEntity.CreateBy = loggingSessionInfo.CurrentUser.Create_User_Id;
                    //inoutExpandEntity.CreateTime = DateTime.Now;
                    //inoutExpandEntity.LastUpdateBy = loggingSessionInfo.CurrentUser.Create_User_Id;
                    //inoutExpandEntity.LastUpdateTime = DateTime.Now;
                    //inoutExpandEntity.IsDelete = 0;
                    //inoutExpandEntity.RecommandVip = rp.Parameters.RecommandVip;
                    //if (intoutExpandBll.GetByID(orderId)==null)
                    //    //如果有数据就修改，没有数据就插入
                    //{
                    //    intoutExpandBll.Create(inoutExpandEntity, tran);
                    //}
                    //else
                    //{
                    //    intoutExpandBll.Update(inoutExpandEntity, tran);
                    //}
                    #endregion

                    tran.Commit();


                    //【已付款】同步ald订单
                    #region 更新阿拉丁订单状态                
                    {                        
                        var orderbll = new InoutService(loggingSessionInfo);
                        var orderInfo = orderbll.GetInoutInfoById(orderId);
                        if (orderInfo.Field3 == "1")
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态成功[OrderID={0}].", orderId) });
                            //更新阿拉丁的订单状态
                            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus aldChangeOrder = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus();
                            if (string.IsNullOrEmpty(orderInfo.vip_no))
                                throw new Exception("会员ID不能为空,OrderID:" + orderId);
                            aldChangeOrder.MemberID = new Guid(orderInfo.vip_no);
                            aldChangeOrder.SourceOrdersID = orderId;
                            aldChangeOrder.Status = int.Parse(orderInfo.status);
                            if (tInoutEntity.ActualAmount == 0)
                            {
                                aldChangeOrder.IsPaid = true;
                            }
                            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest aldRequest = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest();
                            aldRequest.BusinessZoneID = 1;//写死
                            aldRequest.Locale = 1;

                            aldRequest.UserID = new Guid(orderInfo.vip_no);
                            aldRequest.Parameters = aldChangeOrder;
                            var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                            var postContent = string.Format("Action=ChangeOrderStatus&ReqContent={0}", aldRequest.ToJSON());
                            Loggers.Debug(new DebugLogInfo() { Message = "通知ALD更改状态:" + postContent });
                            var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                            var aldRsp = strAldRsp.DeserializeJSONTo<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                            if (!aldRsp.IsSuccess())
                            {
                                Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新阿拉丁订单状态失败[Request ={0}][Response={1}]", aldRequest.ToJSON(), strAldRsp) });
                            }
                        }
                    }
                    #endregion



                    //根据传的订单ID和customerID来先取出当前的status，如果是100的话就更新total_amount
                    //orderId,  var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                    //如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
                    TOrderCustomerDeliveryStrategyMappingBLL tOrderCustomerDeliveryStrategyMappingBLL = new TOrderCustomerDeliveryStrategyMappingBLL(loggingSessionInfo);
                    tOrderCustomerDeliveryStrategyMappingBLL.UpdateOrderAddDeliveryAmount(orderId, rp.CustomerID);


                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new APIException(ex.Message);
                }
            }


            #region 清空购物车 update by wzq
            var shopchatbll = new ShoppingCartBLL(loggingSessionInfo);
            shopchatbll.DeleteShoppingCart(orderId);
            #endregion

            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        #region 取消订单

        public string SetCancelOrder(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetOrderStatusRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var orderId = rp.Parameters.OrderId;

            var vipBll = new VipBLL(loggingSessionInfo);


            vipBll.ProcSetCancelOrder(rp.CustomerID, orderId, rp.UserID);

            //var tInoutBll = new TInoutBLL(loggingSessionInfo);

            //var tInoutENtity = tInoutBll.GetByID(orderId);

            //if(tInoutENtity != null)
            //{
            //    tInoutENtity.Status = "800";
            //    tInoutENtity.StatusDesc = "已取消";

            //    tInoutBll.Update(tInoutENtity);
            //}

            #region 订单状态更新到同步到ALD copy by Henry 2014-09-29
            InoutService inoutService = new InoutService(loggingSessionInfo);
            //判断是否是ALD的订单
            var orderInfo = inoutService.GetInoutInfoById(orderId);
            if (orderInfo.Field3 == "1")
            {
                var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                var request = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest()
                {
                    Parameters =
                        new
                        {
                            SourceOrdersID = orderId,
                            Status = orderInfo.status,
                            MemberID = rp.UserID
                        }
                };
                try
                {
                    var resstr = JIT.Utility.Web.HttpClient.GetQueryString(url,
                        string.Format("Action=ChangeOrderStatus&ReqContent={0}", request.ToJSON()));
                    Loggers.Debug(new DebugLogInfo() { Message = "调用ALD更改状态接口:" + resstr });
                    var res =
                        resstr
                            .DeserializeJSONTo
                            <JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                    //if (!res.IsSuccess())
                    //{
                    //    respdata.code = "105";
                    //    respdata.description = res.message;
                    //}
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    throw new Exception("调用ALD平台失败:" + ex.Message);
                }
            }
            #endregion

            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        #region 获取表单项
        public string GetEvevtFormItems(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventFormItemsRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            MobileBussinessDefinedBLL bll = new MobileBussinessDefinedBLL(loggingSessionInfo);
            GetRegisterFormItemsRD rd = new GetRegisterFormItemsRD();

            if (string.IsNullOrEmpty(rp.Parameters.VipId))
                rp.Parameters.VipId = rp.UserID;

            rd = bll.GetEvevtFormItemValue(rp.Parameters.ObjectId, "VipId", rp.Parameters.VipId);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region GetVipByPhone
        public string GetVipByPhone(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventFormItemsRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            GetVipByPhoneRD rd = new GetVipByPhoneRD();

            if (!string.IsNullOrEmpty(rp.Parameters.Phone))
            {
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                var vip = vipBLL.GetByMobile(rp.Parameters.Phone, rp.CustomerID);

                if (vip != null)
                    rd.VipEntity = vip;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region CreateVipByPhone
        public string CreateVipByPhone(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventFormItemsRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            GetVipByPhoneRD rd = new GetVipByPhoneRD();

            if (!string.IsNullOrEmpty(rp.Parameters.Phone))
            {
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                var vip = vipBLL.GetByMobile(rp.Parameters.Phone, rp.CustomerID);

                if (vip == null)
                {
                    string vipID = Guid.NewGuid().ToString().Replace("-", "");
                    rp.OpenID = string.IsNullOrEmpty(rp.OpenID) ? Guid.NewGuid().ToString() : rp.OpenID;

                    //VipEntity vipEntity = new VipEntity() { VIPID = vipID, WeiXinUserId = string.IsNullOrEmpty(rp.OpenID)?rp.UserID:rp.OpenID, Phone = rp.Parameters.Phone, ClientID = rp.CustomerID, CreateBy = rp.UserID, CreateTime = DateTime.Now, IsDelete = 0, VipSourceId = rp.Parameters.SourceId };
                    VipEntity vipEntity = new VipEntity() { VIPID = vipID, WeiXinUserId = rp.OpenID, Phone = rp.Parameters.Phone, ClientID = rp.CustomerID, CreateBy = rp.UserID, CreateTime = DateTime.Now, IsDelete = 0, VipSourceId = rp.Parameters.SourceId };

                    vipBLL.Create(vipEntity);
                    rd.VipEntity = vipEntity;
                }
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            if (rd.VipEntity == null)
            {
                rsp.ResultCode = 201;
                rsp.Message = "手机号码已存在";
            }

            return rsp.ToJSON();
        }
        #endregion

        #region 提交表单项
        public string SetEventFormItems(string pRequest)
        {
            //var validFlag = pRequest.Parameters.ValidFlag;
            //#region 需要验证


            //#region 根据userid、phone分别查询vip表，如果存在两条记录，且两条记录不相同，进行合并，以userid为主
            ////如果一条都不存在，新增一条vip信息
            ////如果两条记录相同，继续
            //#endregion
            //#region 根据表单数据，更新vip表

            //#endregion
            //#endregion

            //#region 不需要验证 
            //#region 根据userid 取vip，如果有记录更新，没有记录，反之新增
            //#endregion
            //#endregion

            var rp = pRequest.DeserializeJSONTo<APIRequest<SetRegisterFormItemsRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            SetRegisterFormItemsRD rd = new SetRegisterFormItemsRD();

            var itemlist = rp.Parameters.ItemList;
            var objectId = rp.Parameters.ObjectId;
            if (string.IsNullOrEmpty(objectId) || objectId == "")
            {
                throw new APIException("参数【ObjectId】不能为空") { ErrorCode = 121 };
            }


            MobileBussinessDefinedBLL bll = new MobileBussinessDefinedBLL(loggingSessionInfo);
            var vipBll = new VipBLL(loggingSessionInfo);
            var vip = vipBll.GetByID(rp.UserID);

            if (itemlist == null) return new SuccessResponse<IAPIResponseData>(rd).ToJSON();
            if (vip != null)
            {
                string sql = "";
                foreach (var item in itemlist)
                {
                    if (Convert.ToBoolean(item.IsMustDo) && string.IsNullOrEmpty(item.Value))
                    {
                        throw new APIException("必填字段不能为空") { ErrorCode = 121 };
                    }
                    sql += bll.GetColumnName(item.ID) + "='" + item.Value + "',";


                }

                //根据objectid获取表明
                var tableName = bll.GetTableNameByObjectId(objectId, rp.CustomerID);
                if (tableName == "" || string.IsNullOrEmpty(tableName))
                {
                    throw new APIException("无效的ObjectId") { ErrorCode = 122 };
                }
                //根据表更新字段

                // bll.UpdateDynamicColumnValue(sql.Trim(','), "003e30e7121741beb749a230eebecfe0");
                bll.UpdateDynamicColumnValue(sql.Trim(','), rp.UserID, tableName);
            }
            else
            {
                throw new APIException("用户ID无效") { ErrorCode = 121 };
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 获取积分详细
        public string GetIntegralDetailByVipId(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipInfoDetailRP>>();

            var vipId = rp.UserID;
            int? pageSize = rp.Parameters.PageSize;
            int? pageIndex = rp.Parameters.PageIndex;
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipIntegralDetail(vipId, pageIndex ?? 0, pageSize ?? 15);
            var rd = new GetVipInfoDetailRD();
            rd.TotalPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetVipInfoDetailList
                {
                    Date = t["CreateTime"].ToString(),
                    IntegralOrAmount = Convert.ToDecimal(t["Integral"].ToString()),
                    Remark = t["IntegralSourceName"].ToString()
                }).ToArray();
                rd.GetVipInfoDetailData = temp;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();

        }
        #endregion

        #region 获取余额详细
        public string GetVipEndAmountByVipId(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipInfoDetailRP>>();

            var vipId = rp.UserID;
            int? pageSize = rp.Parameters.PageSize;
            int? pageIndex = rp.Parameters.PageIndex;
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipEndAmountDetail(vipId, pageIndex ?? 0, pageSize ?? 15);

            var rd = new GetVipInfoDetailRD();
            rd.TotalPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetVipInfoDetailList
                {
                    Date = t["CreateTime"].ToString(),
                    IntegralOrAmount = Convert.ToDecimal(t["Amount"].ToString()),
                    Remark = t["IntegralSourceName"].ToString()
                }).ToArray();
                rd.GetVipInfoDetailData = temp;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 商户APP会员修改密码

        public string UpdateUserLoginPassword(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateUserLoginPasswordRP>>();

            var rd = new EmptyResponseData();

            if (rp.CustomerID == "" || string.IsNullOrEmpty(rp.CustomerID))
            {
                throw new APIException("客户ID不能为空【CustomerId】") { ErrorCode = 121 };
            }
            if (rp.UserID == "" || string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("会员ID不能为空【UserId】") { ErrorCode = 122 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new VipBLL(loggingSessionInfo);

            var vipEntity = bll.GetByID(rp.UserID);
            if (vipEntity == null)
            {
                throw new APIException("无效的会员ID【UserId】") { ErrorCode = 123 };
            }

            vipEntity = null;
            vipEntity = bll.QueryByEntity(new VipEntity()
                {
                    VIPID = rp.UserID,
                    VipPasswrod = rp.Parameters.OldPassword
                }, null).FirstOrDefault();
            if (vipEntity == null)
            {
                throw new APIException("原密码不正确") { ErrorCode = 124 };
            }

            if (rp.Parameters.NewPassword == "" || string.IsNullOrEmpty(rp.Parameters.NewPassword))
            {
                throw new APIException("新密码不能为空") { ErrorCode = 125 };
            }

            if (rp.Parameters.NewPasswordAgain == "" || string.IsNullOrEmpty(rp.Parameters.NewPasswordAgain))
            {
                throw new APIException("新密码不能为空") { ErrorCode = 126 };
            }

            if (rp.Parameters.NewPassword != rp.Parameters.NewPasswordAgain)
            {
                throw new APIException("密码不一致") { ErrorCode = 127 };
            }

            vipEntity.VipPasswrod = rp.Parameters.NewPassword;
            bll.Update(vipEntity);


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        //设置登录密码
        public string SetVipLoginPassword(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetVipPayPasswordRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = 121 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var mobile = rp.Parameters.Mobile;

            var password = rp.Parameters.Password;
            var passwordAgain = rp.Parameters.PasswordAgain;

            var bll = new VipBLL(loggingSessionInfo);

            var vipEntity = bll.GetByID(rp.UserID);

            if (vipEntity == null)
            {
                throw new APIException("无效的UserId/手机号") { ErrorCode = 141 };
            }


            if (password != passwordAgain)
            {
                throw new APIException("前后密码不一致，请重新输入") { ErrorCode = 137 };
            }

            vipEntity.VipPasswrod = rp.Parameters.Password;
            vipEntity.Status = 2;
            bll.Update(vipEntity);

            var rd = new SetVipLoginPasswordRD { UserId = rp.UserID, UserName = vipEntity.UserName };

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #region App注册 --校验验证码

        public string UserRegisterForApp(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AuthCodeLoginRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            //参数验证
            if (string.IsNullOrEmpty(rp.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = 121 };
            }

            #region 校验手机号是否已经注册过

            var vipBll = new VipBLL(loggingSessionInfo);
            var vipObj1 = new VipEntity { Phone = rp.Parameters.Mobile, ClientID = rp.CustomerID };
            var list = vipBll.QueryByEntity(vipObj1, null);
            if (list != null && list.Length > 0)
            {
                throw new APIException("用户名已经存在") { ErrorCode = 131 };
            }
            if (!string.IsNullOrEmpty(rp.UserID))
            {
                var vipObj2 = new VipEntity { VIPID = rp.UserID };
                var list2 = vipBll.QueryByEntity(vipObj2, null);
                if (list2 != null && list2.Length > 0)
                {
                    throw new APIException("用户名已经存在") { ErrorCode = 131 };
                }
            }

            #endregion

            var bll = new VipBLL(loggingSessionInfo);
            var codebll = new RegisterValidationCodeBLL(loggingSessionInfo);

            #region 验证验证码
            var entity = codebll.GetByMobile(rp.Parameters.Mobile);
            if (entity == null)
                throw new APIException("未找到此手机的验证信息") { ErrorCode = 122 };
            if (entity.IsValidated != null && entity.IsValidated.Value == 1)
                throw new APIException("此验证码已被使用") { ErrorCode = 123 };
            if (entity.Expires != null && entity.Expires.Value < DateTime.Now)
                throw new APIException("此验证码已失效") { ErrorCode = 124 };
            if (entity.Code != rp.Parameters.AuthCode)
                throw new APIException("验证码不正确.") { ErrorCode = 125 };
            #endregion


            #region 新增会员[insert vip]

            var userId = string.IsNullOrWhiteSpace(rp.UserID) ? Guid.NewGuid().ToString("N") : rp.UserID;

            var vipEntity = new VipEntity()
            {
                Phone = rp.Parameters.Mobile,
                VipName = rp.Parameters.Mobile,
                UserName = rp.Parameters.Mobile,
                VIPID = userId,
                Status = 2, //状态为注册
                VipCode = "Vip" + bll.GetNewVipCode(rp.CustomerID),
                ClientID = rp.CustomerID,
                VipSourceId = "1",
                WeiXinUserId = userId
            };
            bll.Create(vipEntity);

            #endregion
            var rd = new AuthCodeLoginRD();
            var customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
            var memberBenefit = customerBasicSettingBll.GetMemberBenefits(rp.CustomerID);

            #region 返回用户信息
            rd.MemberInfo = new MemberInfo()
            {
                Mobile = vipEntity.Phone
                ,
                VipID = vipEntity.VIPID
                ,
                Name = vipEntity.UserName
                ,
                VipName = vipEntity.VipName
                ,
                VipRealName = vipEntity.VipRealName
                ,
                VipNo = vipEntity.VipCode
                ,
                MemberBenefits = memberBenefit
                ,
                IsActivate = false
                ,
                Status = vipEntity.Status ?? 0,
                IntegrationUsed = 0,
                UnfinishedOrdersCount = 0,
                CouponsCount = 0,
                Integration = 0
                ,
                Balance = 0
            };
            #endregion


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetVipIntegral":
                    rst = this.GetVipIntegral(pRequest);
                    break;
                case "GetVipCoupon":
                    rst = this.GetVipCoupon(pRequest);
                    break;
                case "SetVipPayPassword":
                    rst = this.SetVipPayPassword(pRequest);
                    break;
                case "CheckVipPayPassword":
                    rst = this.CheckVipPayPassword(pRequest);
                    break;
                case "GetVipInfo":
                    rst = this.GetVipInfo(pRequest);
                    break;
                case "SetOrderStatus":
                    rst = this.SetOrderStatus(pRequest);
                    break;
                case "SetCancelOrder":
                    rst = this.SetCancelOrder(pRequest);
                    break;
                case "GetEvevtFormItems":
                    rst = this.GetEvevtFormItems(pRequest);
                    break;
                case "GetIntegralDetailByVipId":
                    rst = this.GetIntegralDetailByVipId(pRequest);
                    break;
                case "GetVipEndAmountByVipId":
                    rst = this.GetVipEndAmountByVipId(pRequest);
                    break;
                case "SetEventFormItems":
                    rst = this.SetEventFormItems(pRequest);
                    break;
                case "SetVipLoginPassword":
                    rst = this.SetVipLoginPassword(pRequest);
                    break;
                case "UpdateUserLoginPassword":
                    rst = this.UpdateUserLoginPassword(pRequest);
                    break;
                case "GetVipByPhone":
                    rst = this.GetVipByPhone(pRequest);
                    break;
                case "CreateVipByPhone":
                    rst = this.CreateVipByPhone(pRequest);
                    break;
                case "UserRegisterForApp":
                    rst = this.UserRegisterForApp(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }

    public class SetVipLoginPasswordRD : IAPIResponseData
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class GetVipByPhoneRD : IAPIResponseData
    {
        public VipEntity VipEntity { get; set; }
    }

    public class GetVipInfoDetailRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        public void Validate()
        {
        }
    }
    public class GetVipInfoDetailRD : IAPIResponseData
    {
        public GetVipInfoDetailList[] GetVipInfoDetailData { get; set; }
        public int TotalPageCount { get; set; }
    }
    public class GetVipInfoDetailList
    {
        public int DisplayIndex { get; set; }
        public string Date { get; set; }
        public decimal IntegralOrAmount { get; set; }
        public string Remark { get; set; }
    }
    #region Integral
    public class GetVipIntegralRD : IAPIResponseData
    {
        //本次订单胡可用积分
        public decimal Integral { get; set; }
        //描述
        public string IntegralDesc { get; set; }
        //可兑换金额
        public decimal IntegralAmount { get; set; }
        //账户余额
        public decimal VipEndAmount { get; set; }
    }

    public class GetVipIntegralRP : IAPIRequestParameter
    {
        public SkuIdAndQtyInfo[] SkuIdAndQtyList { get; set; }

        public void Validate()
        {
        }
    }

    #endregion
    public class SkuIdAndQtyInfo
    {
        public string SkuId { get; set; }
        public int Qty { get; set; }
    }

    #region Coupon
    public class GetVipCouponRD : IAPIResponseData
    {
        public CouponInfo[] CouponList { get; set; }
    }

    public class CouponInfo
    {
        //优惠券主键标识
        public string CouponId { get; set; }
        //优惠券描述
        public string CouponDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        //有效期
        public string ValidDateDesc { get; set; }
        public int DisplayIndex { get; set; }
        public int EnableFlag { get; set; }
        //优惠券面值
        public decimal CouponAmount { get; set; }
    }
    #endregion

    #region PayPassword
    public class SetVipPayPasswordRP : IAPIRequestParameter
    {
        public string Mobile { get; set; }
        public string AuthCode { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }

        public void Validate()
        {
        }
    }

    public class CheckVipPayPasswordRP : IAPIRequestParameter
    {
        public string Password { get; set; }

        public void Validate()
        {
        }
    }
    #endregion
    public class GetVipInfoRD : IAPIResponseData
    {
        public string VipId { get; set; }
        //会员状态 2为激活
        public int Status { get; set; }
        //是否设置支付密码 1为是 0 为否
        public int PasswordFlag { get; set; }
        //账户是否冻结 1为冻结 0 为为冻结
        public int LockFlag { get; set; }
    }

    public class SetOrderStatusRP : IAPIRequestParameter
    {
        public string OrderId { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// 是否选用优惠券  1=是，0=否
        /// </summary>
        public int CouponFlag { get; set; }

        /// <summary>
        /// 优惠券标识
        /// </summary>
        public string CouponId { get; set; }

        /// <summary>
        /// 是否选用积分  1=是，0=否
        /// </summary>
        public int IntegralFlag { get; set; }

        /// <summary>
        /// 是否选用余额 1=是，0=否
        /// </summary>
        public int VipEndAmountFlag { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal VipEndAmount { get; set; }

        public string Remark { get; set; }

        //场景
        //add by donal 2014-10-14 10:03:30
        public int OSSId { get; set; }

        //推荐人
        //add by donal 2014-10-14 10:03:17
        public string RecommandVip { get; set; }
        public void Validate()
        {
        }
    }

    public class SetCancleOrderRP : IAPIRequestParameter
    {
        public string OrderId { get; set; }
        public void Validate()
        {
        }
    }


    public class GetEventFormItemsRP : IAPIRequestParameter
    {
        public string ObjectId { get; set; }
        public string VipId { get; set; }
        public string Phone { get; set; }
        public string SourceId { get; set; }
        public void Validate()
        {
        }
    }


    public class UpdateUserLoginPasswordRP : IAPIRequestParameter
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }

        public void Validate()
        {
            if (OldPassword == "" || string.IsNullOrEmpty(OldPassword))
            {
                throw new APIException("原密码不能为空") { ErrorCode = 128 };
            }
        }
    }
}