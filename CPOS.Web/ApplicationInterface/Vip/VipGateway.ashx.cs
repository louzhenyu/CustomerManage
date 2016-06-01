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
using JIT.Utility.DataAccess.Query;
using System.Collections;
using JIT.CPOS.BS.Entity.Interface;
using System.Text;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// VipGateway 的摘要说明
    /// </summary>
    public class VipGateway : BaseGateway
    {
        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion

        #region 获取会员积分和余额
        /// <summary>
        /// 获取会员积分和余额
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetVipIntegral(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipIntegralRP>>();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            var rd = new GetVipIntegralRD();
            var bll = new VipBLL(loggingSessionInfo);
            var skuIdAndQty = rp.Parameters.SkuIdAndQtyList;

            var vipInfo = bll.GetByID(rp.UserID);//会员信息
            if (skuIdAndQty == null)
            {
                throw new APIException("缺少参数【skuIdAndQty】或参数值为空") { ErrorCode = 135 };
            }
            //获取社会化销售配置和积分返现配置
            var basicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
            Hashtable htSetting = basicSettingBll.GetSocialSetting();

            rd.EnableIntegral = int.Parse(htSetting["enableIntegral"].ToString());
            rd.EnableRewardCash = int.Parse(htSetting["enableRewardCash"].ToString());

            //1.根据skuId和数量获取该商品的积分和金额
            string skuIdList = skuIdAndQty.Aggregate("",
                (current, skuIdAndQtyInfo) =>
                    current +
                    (skuIdAndQtyInfo.SkuId + "," + skuIdAndQtyInfo.Qty.ToString(CultureInfo.InvariantCulture) + ";"));

            ////应付金额
            var totalPayAmount = bll.GetTotalSaleAmountBySkuId(skuIdList);

            #region 启用积分
            if (rd.EnableIntegral == 1)
            {
                //2.获取会员的积分和账户余额
                var vipIntegralbll = new VipIntegralBLL(loggingSessionInfo);
                //var vipIntegralEntity = vipIntegralbll.GetByID(rp.UserID);
                //根据会员和会员卡号获取积分
                var vipIntegralEntity = vipIntegralbll.QueryByEntity(new VipIntegralEntity() { VipID = rp.UserID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                if (vipIntegralEntity == null)
                {
                    rd.Integral = 0;
                    rd.IntegralAmount = 0;
                    rd.IntegralDesc = "无积分可兑换";
                }
                else
                {
                    decimal validIntegral = vipIntegralEntity.ValidIntegral ?? 0;//会员积分

                    int totalIntegral = 0;      //可使用积分(取整)                
                    if (int.Parse(htSetting["rewardsType"].ToString()) == 1)//按商品奖励
                        totalIntegral = (int)Math.Round(bll.GetIntegralBySkuId(skuIdList), 1);
                    else//按订单奖励
                    {
                        //积分使用上限比例
                        decimal pointsRedeemUpLimit = decimal.Parse(htSetting["pointsRedeemUpLimit"].ToString()) / 100;
                        //3.获取积分与金额的兑换比例
                        var integralAmountPre = bll.GetIntegralAmountPre(rp.CustomerID);
                        if (integralAmountPre == 0)
                            integralAmountPre = (decimal)0.01;

                        totalIntegral = (int)Math.Round(totalPayAmount * pointsRedeemUpLimit * integralAmountPre, 1); //可使用的积分

                    }
                    rd.Integral = validIntegral > totalIntegral ? totalIntegral : validIntegral;

                    //rd.IntegralAmount = rd.Integral * integralAmountPre;
                    rd.IntegralAmount = bll.GetAmountByIntegralPer(loggingSessionInfo.ClientID, rd.Integral);
                    rd.IntegralDesc = "使用积分" + rd.Integral.ToString("0") + ",可兑换"
                                      + rd.IntegralAmount.ToString("0.00") + "元";
                    rd.PointsRedeemLowestLimit = int.Parse(htSetting["pointsRedeemLowestLimit"].ToString());
                }
            }
            #endregion

            //根据会员和会员卡号获取余额和返现
            var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
            var vipAmountInfo = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = rp.UserID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
            #region 启用返现
            if (rd.EnableRewardCash == 1)
            {
                if (vipAmountInfo != null)
                {
                    //累计返现金额
                    decimal returnAmount = vipAmountInfo.ValidReturnAmount == null ? 0 : vipAmountInfo.ValidReturnAmount.Value;
                    //订单可使用最大返现金额
                    decimal returnAmountOrder = totalPayAmount * (decimal.Parse(htSetting["cashRedeemUpLimit"].ToString()) / 100);
                    rd.ReturnAmount = returnAmount > returnAmountOrder ? returnAmountOrder : returnAmount;
                    rd.CashRedeemLowestLimit = decimal.Parse(htSetting["cashRedeemLowestLimit"].ToString());
                }
            }
            #endregion
            //账户余额
            //var vipEndAmount = bll.GetVipEndAmount(rp.UserID);
            //rd.VipEndAmount = totalPayAmount > vipEndAmount ? vipEndAmount : totalPayAmount;
            if (vipAmountInfo != null)
                rd.VipEndAmount = vipAmountInfo.EndAmount.Value;

            //获取会员折扣
            var sysVipCardGradeBLL = new SysVipCardGradeBLL(loggingSessionInfo);
            decimal vipDiscount =1;//会员折扣
            if (rp.Parameters.DiscountType == 0)
                vipDiscount = sysVipCardGradeBLL.GetVipDiscount();
            rd.VipDiscount = vipDiscount;

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
            var bll = new VipBLL(loggingSessionInfo);
            var skuIdAndQty = rp.Parameters.SkuIdAndQtyList;

            //if (skuIdAndQty == null)
            //{
            //    throw new APIException("缺少参数【skuIdAndQty】或参数值为空") { ErrorCode = 135 };
            //}
            decimal totalPayAmount = 0;
            if (skuIdAndQty != null)
            {
                //1.根据skuId和数量获取该商品的积分和金额
                string skuIdList = skuIdAndQty.Aggregate("",
                    (current, skuIdAndQtyInfo) =>
                        current + (skuIdAndQtyInfo.SkuId + ","
                                   + skuIdAndQtyInfo.Qty.ToString(CultureInfo.InvariantCulture) + ";"));
                //应付金额
                totalPayAmount = bll.GetTotalSaleAmountBySkuId(skuIdList);
            }
            var rd = new GetVipCouponRD();
            var ds = bll.GetVipCouponDataSet(rp.UserID, totalPayAmount, rp.Parameters.UsableRange, rp.Parameters.ObjectID, rp.Parameters.Type);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new CouponInfo()
                {
                    CouponId = t["CouponID"].ToString(),
                    CouponCode = t["CouponCode"].ToString(),
                    CouponAmount = Convert.ToDecimal(t["parValue"]),
                    CouponName = t["CoupnName"].ToString(),
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

            //用户信息
            var vipEntity = new VipEntity();

            if (!string.IsNullOrWhiteSpace(rp.Parameters.OwnerVipID))
            {
                vipEntity = bll.GetByID(rp.Parameters.OwnerVipID);
            }
            else
            {
                if (string.IsNullOrEmpty(rp.UserID) && !string.IsNullOrEmpty(rp.OpenID))
                {
                    vipEntity = bll.QueryByEntity(new VipEntity()
                            {
                                WeiXinUserId = rp.OpenID
                            }, null)[0];
                }
                else
                {
                    vipEntity = bll.GetByID(rp.UserID);
                }
            }

            if (vipEntity == null)
            {
                throw new APIException("无效的会员ID【VipId】") { ErrorCode = 121 };
            }
            else
            {
                rd.VipId = rp.UserID;
                rd.Status = vipEntity.Status ?? 0;
                rd.isStore = vipEntity.IsSotre ?? 0;
                rd.HeadImgUrl = vipEntity.HeadImgUrl;
                rd.VipName = vipEntity.VipName;
                rd.UnitId = vipEntity.CouponInfo;
            }

            //用户余额信息
            var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
            var vipAmountEntity = vipAmountBll.GetByID(rp.UserID);
            if (vipAmountEntity != null)
            {
                rd.LockFlag = vipAmountEntity.IsLocking ?? 1;
                rd.PasswordFlag = string.IsNullOrEmpty(vipAmountEntity.PayPassword) ? 0 : 1;
                rd.EndAmount = vipAmountEntity.EndAmount;
            }
            else
            {
                rd.EndAmount = 0;
            }

            //用户积分信息
            var vipIntegralBll = new VipIntegralBLL(loggingSessionInfo);
            var vipIntegralEntity = vipIntegralBll.GetByID(rp.UserID);
            if (vipIntegralEntity != null)
            {
                rd.EndIntegral = vipIntegralEntity.EndIntegral;
            }
            else
            {
                rd.EndIntegral = 0;
            }

            //邀请的小伙伴
            rd.inviteCount = bll.GetInviteCount(rp.UserID);


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
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            var tInoutBll = new TInoutBLL(loggingSessionInfo);          //订单业务表
            var service = new ShoppingCartBLL(loggingSessionInfo);      //购物车业务表
            var inoutService = new InoutService(loggingSessionInfo);    //订单业务表
            var sysVipCardGradeBLL = new SysVipCardGradeBLL(loggingSessionInfo);    //获取折扣表
            var vipBLL = new VipBLL(loggingSessionInfo);
            var unitBLL = new t_unitBLL(loggingSessionInfo);
            var inoutServiceBLL = new InoutService(loggingSessionInfo);
            var skuPriceBLL = new T_Sku_PriceBLL(loggingSessionInfo);
            var inoutBll = new T_InoutBLL(loggingSessionInfo);//订单业务对象实例化


            var orderId = rp.Parameters.OrderId;//订单ID
            var status = rp.Parameters.Status;  //当前状态
            decimal discountAmount = 0;         //抵扣金额汇总
            decimal vipDiscount = sysVipCardGradeBLL.GetVipDiscount();//会员折扣

            if (rp.OpenID == "" || string.IsNullOrEmpty(rp.OpenID))
                throw new APIException("OpenID不能为空【OpenID】") { ErrorCode = 121 };
            if (rp.UserID == "" || string.IsNullOrEmpty(rp.UserID))
                throw new APIException("UserID不能为空【UserID】") { ErrorCode = 121 };
            if (orderId == "" || string.IsNullOrEmpty(orderId))
                throw new APIException("订单标识不能为空【OrderId】") { ErrorCode = 121 };
            if (status == "" || string.IsNullOrEmpty(status))
                throw new APIException("订单状态不能为空【Status】") { ErrorCode = 121 };

            var tInoutEntity = tInoutBll.GetByID(orderId);
            if (tInoutEntity == null)
                throw new APIException("此订单Id无效") { ErrorCode = 103 };
            if (tInoutEntity.Field7 == "100")
                throw new APIException("此订单已经提交") { ErrorCode = 122 };

            #region 根据渠道判断订单来源

            if (rp.ChannelId.Equals("6"))
            {
                tInoutEntity.DataFromID = "16";
                if (!string.IsNullOrWhiteSpace(rp.Parameters.OwnerVipID))//店主vipid
                {
                    tInoutEntity.SalesUser = rp.Parameters.OwnerVipID;
                }
            }
            #endregion


            //获取订单明细
            var inoutDetailList = inoutServiceBLL.GetInoutDetailInfoByOrderId(orderId);

            #region 判断库存是否足够
            if (tInoutEntity.OrderReasonID == "2F6891A2194A4BBAB6F17B4C99A6C6F5") //普通商品订单判断
            {
                foreach (var detail in inoutDetailList)
                {
                    //sku库存
                    var skuStockInfo = skuPriceBLL.QueryByEntity(new T_Sku_PriceEntity() { sku_id = detail.sku_id, item_price_type_id = "77850286E3F24CD2AC84F80BC625859E", status = "1" }, null).FirstOrDefault();
                    if (skuStockInfo != null)
                    {
                        int skuStock = Convert.ToInt32(skuStockInfo.sku_price.Value);
                        int qty = Convert.ToInt32(detail.enter_qty);
                        if (skuStock < qty)
                            throw new APIException("库存不足") { ErrorCode = 121 };
                    }
                }
            }
            #endregion

            IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {

                    //首次提交订单处理库存和销量
                    CustomerBasicSettingBLL customerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
                    string AfterPaySetStock = customerBasicSettingBll.GetSettingValueByCode("AfterPaySetStock");
                    if (AfterPaySetStock != "1")
                    {
                        if (tInoutEntity.Status == "100" && tInoutEntity.Field7 == "-99")
                            inoutBll.SetStock(orderId, inoutDetailList, 1, loggingSessionInfo);
                    }
                    // 根据订单ID修改订单配送状态
                    //推送ios、android信息
                    var flag = inoutService.UpdateOrderDeliveryStatus(orderId, status, Utils.GetNow(), null, (SqlTransaction)tran);
                    if (!flag)
                        throw new APIException("更新订单状态失败") { ErrorCode = 103 };
                    //优惠券
                    var couponFlag = rp.Parameters.CouponFlag;
                    var couponId = rp.Parameters.CouponId;
                    //积分
                    var integralFlag = rp.Parameters.IntegralFlag;
                    var integral = rp.Parameters.Integral;
                    var integralAmount = rp.Parameters.IntegralAmount;
                    //余额
                    var vipEndAmountFlag = rp.Parameters.VipEndAmountFlag;
                    var vipEndAmount = rp.Parameters.VipEndAmount;
                    //返现
                    var returnAmountFlag = rp.Parameters.ReturnAmountFlag;  //是否使用返现金额（1=使用；0=不适用）
                    var returnAmount = rp.Parameters.ReturnAmount;
                    //会员抵扣金额
                    var vipDiscountRp = rp.Parameters.VipDiscount;
                    //运费
                    var deliveryAmount = rp.Parameters.DeliveryAmount;

                    //实付金额 = (应付金额 - 优惠券抵扣金额 - 余额抵扣金额 - 积分抵扣金额 - 返现抵扣金额)*会员折扣

                    //获取会员信息
                    var vipInfo = vipBLL.GetByID(rp.UserID);
                    //更新会员最后一次交易时间
                    vipInfo.RecentlySalesTime = DateTime.Now;
                    vipBLL.Update(vipInfo, tran);
                    //获取门店信息
                    t_unitEntity unitInfo = null;
                    if (!string.IsNullOrEmpty(tInoutEntity.SalesUnitID))
                        unitInfo = unitBLL.GetByID(tInoutEntity.SalesUnitID);

                    #region 处理订单发货门店,无会籍店用户sales_unit_id置为空
                    if (string.IsNullOrEmpty(vipInfo.CouponInfo))
                    {
                        if (tInoutEntity.Field8 == "1")//等于送货到家处理
                            tInoutEntity.SalesUnitID = "";
                    }
                    #endregion

                    #region 积分抵扣业务处理
                    if (integralFlag == 1)
                    {
                        var vipIntegralBll = new VipIntegralBLL(loggingSessionInfo);

                        #region annotation by Henry 2015-4-15
                        //获取该订单可用的总积分

                        //var orderDetail = new TInoutDetailBLL(loggingSessionInfo);

                        //var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity()
                        //{
                        //    order_id = orderId
                        //}, null);

                        //if (orderDetailList == null || orderDetailList.Length == 0)
                        //{
                        //    throw new APIException("该订单没有商品") { ErrorCode = 121 };
                        //}
                        //var str = orderDetailList.Aggregate("", (i, j) =>
                        //{
                        //    i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                        //    return i;
                        //});

                        //var bll = new VipBLL(loggingSessionInfo);

                        ////判断该积分和vip个人积分的大小
                        //var vipIntegralEntity = vipIntegralBll.GetByID(rp.UserID);

                        ////商品可兑换的总积分
                        //var vipIntegral = bll.GetIntegralBySkuId(str);
                        ////个人的有效积分
                        //var validIntegral = vipIntegralEntity.ValidIntegral ?? 0;

                        //var integralPre = bll.GetIntegralAmountPre(rp.CustomerID);

                        //if (integralPre == 0)
                        //{
                        //    integralPre = 0.01M;
                        //}

                        //var point = validIntegral > vipIntegral ? vipIntegral : validIntegral;

                        //discountAmount = discountAmount +
                        //                 integralPre * point;

                        //获取积分与金额的百分比
                        //获取积分金额
                        #endregion

                        discountAmount = discountAmount + integralAmount;
                        string sourceId = "20"; //积分抵扣
                        //vipIntegralBll.ProcessPoint(sourceId, rp.CustomerID, rp.UserID, orderId, (SqlTransaction)tran, null, -integral, null, rp.UserID);
                        var IntegralDetail = new VipIntegralDetailEntity()
                        {
                            Integral = -integral,
                            IntegralSourceID = sourceId,
                            ObjectId = orderId
                        };
                        //变动前积分
                        string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                        //变动积分
                        string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                        var vipIntegralDetailId = vipIntegralBll.AddIntegral(ref vipInfo, unitInfo, IntegralDetail, (SqlTransaction)tran, loggingSessionInfo);
                        //发送微信积分变动通知模板消息
                        if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                        {
                            var CommonBLL = new CommonBLL();
                            CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, loggingSessionInfo);
                        }
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

                        #region Insert CouponUse

                        //var tOrderCouponMapping = new TOrderCouponMappingBLL(loggingSessionInfo);

                        //var tOrderCouponMappingEntity = new TOrderCouponMappingEntity
                        //{
                        //    MappingId = Utils.NewGuid(),
                        //    OrderId = orderId,
                        //    CouponId = couponId
                        //};

                        //tOrderCouponMapping.Create(tOrderCouponMappingEntity, tran);

                        var couponUseBll = new CouponUseBLL(loggingSessionInfo);
                        var couponUseEntity = new CouponUseEntity()
                        {
                            CouponUseID = Guid.NewGuid(),
                            CouponID = couponId,
                            VipID = rp.UserID,
                            UnitID = tInoutEntity.UnitID,
                            OrderID = tInoutEntity.OrderID,
                            Comment = "商城使用电子券",
                            CustomerID = rp.CustomerID,
                            CreateBy = rp.UserID,
                            CreateTime = DateTime.Now,
                            LastUpdateBy = rp.UserID,
                            LastUpdateTime = DateTime.Now,
                            IsDelete = 0
                        };
                        couponUseBll.Create(couponUseEntity, tran);
                        #endregion
                        
                        #region 更新CouponType数量
                        var conponTypeBll = new CouponTypeBLL(loggingSessionInfo);
                        var conponTypeEntity = conponTypeBll.QueryByEntity(new CouponTypeEntity() { CouponTypeID = new Guid(couponEntity.CouponTypeID), CustomerId = rp.CustomerID }, null).FirstOrDefault();
                        conponTypeEntity.IsVoucher += 1;
                        conponTypeBll.Update(conponTypeEntity, tran);

                        #endregion

                        #region Update Coupon Status = 1

                        couponEntity.Status = 1;
                        couponBll.Update(couponEntity, tran);

                        #endregion
                    }

                    #endregion

                    #region 余额和返现修改

                    var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                    var vipAmountDetailBll = new VipAmountDetailBLL(loggingSessionInfo);

                    var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = rp.UserID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                    if (vipAmountEntity != null)
                    {
                        //判断该会员账户是否被冻结
                        if (vipAmountEntity.IsLocking == 1)
                            throw new APIException("账户已被冻结，请先解冻") { ErrorCode = 103 };

                        //判断该会员的账户余额是否大于本次使用的余额
                        if (vipAmountEntity.EndAmount < vipEndAmount)
                            throw new APIException(string.Format("账户余额不足，当前余额为【{0}】", vipAmountEntity.EndAmount)) { ErrorCode = 103 };
                    }
                    //使用返现
                    if (returnAmountFlag == 1)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = -returnAmount,
                            ObjectId = orderId,
                            AmountSourceId = "13"
                        };
                        var vipAmountDetailId= vipAmountBll.AddReturnAmount(vipInfo, unitInfo, vipAmountEntity,ref detailInfo, (SqlTransaction)tran, loggingSessionInfo);
                        if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                        {//发送返现到账通知微信模板消息
                            var CommonBLL = new CommonBLL();
                            CommonBLL.CashBackMessage(tInoutEntity.OrderNo, detailInfo.Amount, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);

                        }
                        discountAmount = discountAmount + returnAmount;
                    }
                    //使用余额
                    if (vipEndAmountFlag == 1)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = -vipEndAmount,
                            AmountSourceId = "1",
                            ObjectId = orderId
                        };
                        var vipAmountDetailId= vipAmountBll.AddVipAmount(vipInfo, unitInfo,ref vipAmountEntity, detailInfo, (SqlTransaction)tran, loggingSessionInfo);
                        if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                        {//发送微信账户余额变动模板消息
                            var CommonBLL = new CommonBLL();
                            CommonBLL.BalanceChangedMessage(tInoutEntity.OrderNo, vipAmountEntity, detailInfo, vipInfo.WeiXinUserId, vipInfo.VIPID, loggingSessionInfo);
                        }
                        discountAmount = discountAmount + vipEndAmount;
                    }

                    #endregion

                    #region 会员折扣
                    if (vipDiscount > 0 && vipDiscountRp > 0)
                    {
                        //discountAmount = discountAmount + (tInoutEntity.TotalAmount.Value - (tInoutEntity.TotalAmount.Value * vipDiscount));
                        discountAmount = discountAmount + vipDiscountRp;
                        tInoutEntity.DiscountRate = vipDiscount * 100;
                    }
                    #endregion

                    if (tInoutEntity.TotalAmount < discountAmount)
                        tInoutEntity.ActualAmount = 0;
                    else
                        tInoutEntity.ActualAmount = tInoutEntity.TotalAmount - discountAmount;


                    //订单金额正值(正常的买单),订单金额负值(退订订单)
                    //if (tInoutEntity.ActualAmount >= 0)
                    //    tInoutEntity.OrderReasonID = "2F6891A2194A4BBAB6F17B4C99A6C6F5";  //正常pos订单Order_Reason_id
                    //else
                    //    tInoutEntity.OrderReasonID = "21B88CE9916A4DB4A1CAD8E3B4618C10";


                    //如果实付金额 = 各种优惠活动的综合 设置付款状态=1【已付款】
                    if (tInoutEntity.TotalAmount + deliveryAmount == discountAmount || tInoutEntity.ActualAmount + deliveryAmount == 0)
                        tInoutEntity.Field1 = "1";
                    tInoutEntity.Remark = rp.Parameters.Remark;    //备注
                    tInoutEntity.Field19 = rp.Parameters.Invoice;  //发票信息
                    tInoutEntity.Field20 = rp.Parameters.RetailTraderId; //分销商Id
                    tInoutEntity.VipCardCode = vipInfo.VipCode;    //会员卡号
                    tInoutEntity.Field7 = status;  
                    tInoutEntity.OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  ///提交订单时间
                    tInoutBll.Update(tInoutEntity, tran);          //修改订单信息

                    #region [弃用]订单扩展表用于(人人销售、分润数据） add by donal 2014-10-14 09:40:12
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
                    //if (intoutExpandBll.GetByID(orderId) == null)
                    ////如果有数据就修改，没有数据就插入
                    //{
                    //    intoutExpandBll.Create(inoutExpandEntity, tran);
                    //}
                    //else
                    //{
                    //    intoutExpandBll.Update(inoutExpandEntity, tran);
                    //}
                    #endregion

                    #region 增加订单操作记录 Add By Henry 2015-7-29
                    var tinoutStatusBLL = new TInoutStatusBLL(loggingSessionInfo);
                    TInoutStatusEntity statusEntity = new TInoutStatusEntity()
                    {
                        OrderID = tInoutEntity.OrderID,
                        OrderStatus = int.Parse(tInoutEntity.Status),
                        CustomerID = loggingSessionInfo.ClientID,
                        StatusRemark = "提交订单[操作人:客户]"
                    };
                    tinoutStatusBLL.Create(statusEntity, tran);
                    #endregion

                    #region 抢购团购砍价处理库存
                    if (AfterPaySetStock != "1")
                    {
                        if (tInoutEntity.OrderReasonID == "CB43DD7DD1C94853BE98C4396738E00C" || tInoutEntity.OrderReasonID == "671E724C85B847BDA1E96E0E5A62055A")
                        {
                            if (string.IsNullOrEmpty(rp.Parameters.EventId))
                            {
                                throw new APIException("活动商品,EventId不能为空");
                            }
                            //下订单，修改抢购商品的数量信息存储过程ProcPEventItemQty
                            var eventbll = new vwItemPEventDetailBLL(loggingSessionInfo);
                            eventbll.ExecProcPEventItemQty(loggingSessionInfo.ClientID, rp.Parameters.EventId, orderId, tInoutEntity.VipNo, (SqlTransaction)tran);
                        }
                        if (tInoutEntity.OrderReasonID == "096419BFDF394F7FABFE0DFCA909537F")
                        {
                            if (string.IsNullOrEmpty(rp.Parameters.EventId))
                            {
                                throw new APIException("活动商品,EventId不能为空");
                            }
                            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);
                            eventBll.SetKJEventOrder(loggingSessionInfo.ClientID, orderId, rp.Parameters.EventId,rp.Parameters.KJEventJoinId,inoutDetailList.ToList());
                        }

                    }
                    #endregion

                    tran.Commit();

                    if (deliveryAmount != 0)
                    {
                        //如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
                        TOrderCustomerDeliveryStrategyMappingBLL tOrderCustomerDeliveryStrategyMappingBLL = new TOrderCustomerDeliveryStrategyMappingBLL(loggingSessionInfo);
                        tOrderCustomerDeliveryStrategyMappingBLL.UpdateOrderAddDeliveryAmount(orderId, rp.CustomerID);
                    }
                    #region 清空购物车 update by wzq
                    var shopchatbll = new ShoppingCartBLL(loggingSessionInfo);
                    shopchatbll.DeleteShoppingCart(orderId);
                    #endregion

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new APIException(ex.Message);
                }
            }
            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion


        #region 订单提交

        /// <summary>
        /// 用兑换券提货
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string VoucherCouponOrder(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VoucherCouponOrderRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);//会员标识
            InoutService service = new InoutService(loggingSessionInfo);
            //      decimal discountAmount = 0;
            if (string.IsNullOrEmpty(rp.CustomerID))
            {
                throw new APIException("CustomerID不能为空【CustomerID】") { ErrorCode = 121 };
            }
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("UserID不能为空【UserID】") { ErrorCode = 121 };
            }
            //if (rp.OpenID == "" || string.IsNullOrEmpty(rp.OpenID))
            //{
            //    throw new APIException("OpenID不能为空【OpenID】") { ErrorCode = 121 };
            //}
            if (rp.Parameters.CouponId == "" || string.IsNullOrEmpty(rp.Parameters.CouponId))
            {
                throw new APIException("CouponId不能为空【CouponId】") { ErrorCode = 121 };
            }

            //var couponFlag = rp.Parameters.CouponFlag;
            var couponId = rp.Parameters.CouponId;
            #region 判断优惠券是否可用


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
            var couponEntity = couponBll.GetByID(couponId);//获取到优惠券信息
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
            //  discountAmount = discountAmount + couponTypeEntity.ParValue ?? 0;
            //根据优惠券标识查找对应到的商品
            CouponTypeItemMappingBLL _CouponTypeItemMappingBLL = new CouponTypeItemMappingBLL(loggingSessionInfo);
            CouponTypeItemMappingEntity CouponTypeItemMappingTemp = new CouponTypeItemMappingEntity();
            CouponTypeItemMappingTemp.CouponTypeID = new Guid(couponEntity.CouponTypeID.ToString());//转换成guid类型
            var couponTypeItemList = _CouponTypeItemMappingBLL.QueryByEntity(CouponTypeItemMappingTemp, null);
            CouponTypeItemMappingEntity CouponTypeItemMappingEn = null;
            if (couponTypeItemList != null && couponTypeItemList.Length > 0)
            {
                CouponTypeItemMappingEn = couponTypeItemList[0];
            }

            if (CouponTypeItemMappingEn == null)
            {
                throw new APIException("该优惠券没有可以兑换的商品") { ErrorCode = 103 };
            }
            //查找商品，并且查找商品对应的唯一的sku
            T_ItemBLL t_ItemBLL = new T_ItemBLL(loggingSessionInfo);
            T_ItemEntity ItemEn = t_ItemBLL.GetByID(CouponTypeItemMappingEn.ObjectId);
            T_SkuBLL t_SkuBLL = new T_SkuBLL(loggingSessionInfo);
            T_SkuEntity t_SkuTemp = new T_SkuEntity();
            t_SkuTemp.item_id = CouponTypeItemMappingEn.ObjectId;
            var t_SkuList = t_SkuBLL.QueryByEntity(t_SkuTemp, null);
            T_SkuEntity t_SkuEn = null;
            if (t_SkuList != null && t_SkuList.Length != 0)
            {
                t_SkuEn = t_SkuList[0];
            }


            IList<setOrderDetailClass> orderDetailLis = new List<setOrderDetailClass>();
            setOrderDetailClass setOrderDetail = new setOrderDetailClass();
            setOrderDetail.skuId = t_SkuEn.sku_id;     //商品SKU标识
            setOrderDetail.salesPrice = "0";  //商品销售单价
            setOrderDetail.qty = "1";       //商品数量
            //public string beginDate { get; set; }
            //public string endDate { get; set; }
            // public string appointmentTime { get; set; }//added by zhangwei 2014-2-26预约时间
            setOrderDetail.itemName = ItemEn.item_name;//返现金额
            setOrderDetail.ReturnCash = "0";
            orderDetailLis.Add(setOrderDetail);
            #endregion

            VipBLL vipBLL = new VipBLL(loggingSessionInfo);
            VipEntity vipInfo = vipBLL.GetByID(rp.UserID);
            if (vipInfo == null)
            {
                throw new APIException("找不到该会员信息") { ErrorCode = 121 };
            }
            SetOrderEntity orderInfo = new SetOrderEntity();//订单

            //try
            //{
            //string reqContent = HttpContext.Current.Request["ReqContent"];

            //Loggers.Debug(new DebugLogInfo()
            //{
            //    Message = string.Format("setOrderInfo: {0}", reqContent)
            //});

            #region 解析请求字符串 chech
            //  var reqObj = reqContent.DeserializeJSONTo<setOrderInfoNewReqData>();//解析request参数,转换成对象
            //  reqObj = reqObj == null ? new setOrderInfoNewReqData() : reqObj;
            var reqObj = new setOrderInfoNewReqData();
            if (reqObj.special == null)
            {
                reqObj.special = new setOrderInfoNewReqSpecialData();
            }
            reqObj.special.isGroupBy = "";  //活动标识EventID
            //if (reqObj.special.orderDetailList == null || reqObj.special.orderDetailList.Count == 0)//商品数量为0
            //{
            //    respData.code = "2201";
            //    respData.description = "必须选择商品";
            //    return respData.ToJSON().ToString();
            //}
            reqObj.special.orderDetailList = orderDetailLis;//上面根据优惠券获取到的商品信息
            //
            reqObj.special.qty = "1";		    //商品数量
            reqObj.special.storeId = null;		//门店标识
            reqObj.special.totalAmount = "0";		//订单总价

            reqObj.special.deliveryId = "1";//		配送方式标识,送货到家*****
            reqObj.special.deliveryAddress = "";//	配送地址
            reqObj.special.deliveryTime = "";//		提货时间（配送时间）
            reqObj.special.email = "";
            reqObj.special.remark = rp.Parameters.Remark;
            reqObj.special.username = vipInfo.VipRealName;
            reqObj.special.mobile = vipInfo.Phone;	//手机号码

            reqObj.special.tableNumber = "";
            reqObj.special.couponsPrompt = ""; //优惠券提示语（Jermyn20131213--Field16）
            reqObj.special.actualAmount = "0";    //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
            reqObj.special.reqBy = "1";  //请求0-wap,1-手机.
            reqObj.special.joinNo = "1"; //餐饮--人数
            // reqObj.special.status = "99";
            reqObj.special.isGroupBy = "";  //是否团购订单（Jermyn20140318—Field15）
            reqObj.special.dataFromId = 1;
            reqObj.special.SalesUser = ""; //店员ID add by donal 2014-9-25 18:07:11

            #endregion

            #region 设置参数


            //orderInfo.SkuId = reqObj.special.skuId;
            int itemTotalQty = 0;
            foreach (var detail in reqObj.special.orderDetailList)
            {
                itemTotalQty += ToInt(detail.qty);
            }
            reqObj.special.qty = itemTotalQty.ToString();

            string purchaseUnitId = string.Empty;
            orderInfo.TotalQty = ToInt(reqObj.special.qty);
            UnitService unitServer = new UnitService(loggingSessionInfo);

            orderInfo.CarrierID = reqObj.special.storeId;
            #region 如果选择到店自提sales_unit_id保存门店id，否则保存在线商城的Unit_id update by wzq
            //if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
            //{                  
            orderInfo.StoreId = unitServer.GetUnitByUnitTypeForWX("OnlineShopping", null).Id; //获取在线商城的门店标识
            //}
            //else    //送货到家***
            //{
            //    orderInfo.StoreId = ToStr(reqObj.special.storeId);
            //    orderInfo.PurchaseUnitId = ToStr(reqObj.special.storeId);
            //}
            #endregion


            #region 重新计算商品价格以及总价

            SkuPriceService SkuPriceBll = new SkuPriceService(loggingSessionInfo);

            StringBuilder skuIds = new StringBuilder();
            for (int j = 0; j < reqObj.special.orderDetailList.Count(); j++)
            {
                if (j != 0)
                {
                    skuIds.Append(",");
                }
                skuIds.Append(string.Format("'{0}'", reqObj.special.orderDetailList[j].skuId));
            }

            //团购活动ID                
            //if (!string.IsNullOrWhiteSpace(reqObj.special.isGroupBy))
            //{
            //    Guid EventId;
            //    bool B_EventID = Guid.TryParse(reqObj.special.isGroupBy, out EventId);
            //    if (!B_EventID)
            //    {
            //        respData.code = "2207";
            //        respData.description = "团购id有误.";
            //        return respData.ToJSON().ToString();
            //    }
            //}

            IList<SkuPrice> skuPriceList = SkuPriceBll.GetPriceListBySkuIds(skuIds.ToString(), reqObj.special.isGroupBy);
            decimal TotalAmount = 0.00m;
            decimal TotalReturnCash = 0.00m;

            var basicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);
            Hashtable htSetting = basicSettingBll.GetSocialSetting();   //获取社会化销售配置

            foreach (var item in reqObj.special.orderDetailList)
            {
                var skuprice = skuPriceList.Where(m => m.sku_id == item.skuId).FirstOrDefault();
                if (skuprice != null)
                {
                    item.salesPrice = skuprice.price.ToString();

                }
                TotalAmount += (Convert.ToDecimal(item.salesPrice) * Convert.ToInt32(item.qty));
                TotalReturnCash += (Convert.ToDecimal(item.ReturnCash) * Convert.ToInt32(item.qty));
            }
            reqObj.special.totalAmount = TotalAmount.ToString("f2");

            #endregion

            //orderInfo.SalesPrice = Convert.ToDecimal(reqObj.special.salesPrice);
            //orderInfo.StdPrice = Convert.ToDecimal(reqObj.special.stdPrice);
            orderInfo.TotalAmount = Convert.ToDecimal(reqObj.special.totalAmount);
            orderInfo.ReturnCash = TotalReturnCash;

            orderInfo.Mobile = ToStr(reqObj.special.mobile);
            orderInfo.Email = ToStr(reqObj.special.email);
            orderInfo.Remark = ToStr(reqObj.special.remark);
            //orderInfo.CreateBy = ToStr(reqObj.common.userId);
            //orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);

            //不能从common直接获取userId，ALD同步会员是userID已经重新被赋值到loggingSessionInfo.UserID中
            orderInfo.CreateBy = ToStr(loggingSessionInfo.UserID);
            orderInfo.LastUpdateBy = ToStr(loggingSessionInfo.UserID);

            orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);//****
            orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);//****
            orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);//****
            orderInfo.CustomerId = loggingSessionInfo.ClientID;
            orderInfo.OpenId = ToStr(rp.OpenID);
            orderInfo.username = ToStr(reqObj.special.username);
            //orderInfo.tableNumber = ToStr(reqObj.special.tableNumber);
            //orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
            orderInfo.Remark = ToStr(reqObj.special.remark);
            //orderInfo.JoinNo = ToInt(reqObj.special.joinNo);
            //orderInfo.IsALD = ToStr(reqObj.common.isALD);
            orderInfo.IsGroupBuy = ToStr(reqObj.special.isGroupBy);
            orderInfo.DataFromId = reqObj.special.dataFromId;
            orderInfo.SalesUser = reqObj.special.SalesUser; //店员ID add by donal 2014-9-25 18:09:49
            // orderInfo.ChannelId = reqObj.common.channelId; //渠道 add by donal 2014-9-28 14:32:05

            //如果是【人人销售/我的小店】销售人为空，销售人就是他自己
            //if ((orderInfo.ChannelId == "6" || orderInfo.ChannelId == "10") && string.IsNullOrWhiteSpace(orderInfo.SalesUser))
            //{
            //    orderInfo.SalesUser = reqObj.common.userId;
            //}

            if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") && !ToStr(reqObj.special.actualAmount).Equals(""))
            {
                orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
            }
            //这里对没有订单做了判断
            if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
            {
                orderInfo.OrderId = BaseService.NewGuidPub();
                orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                orderInfo.Status = (string.IsNullOrEmpty(reqObj.special.reqBy) || reqObj.special.reqBy == "0") ? "-99" : "100";   //Jermyn20140219 //haibo.zhou20140224,这里这么写，status=-99,是针对微信平台里的，在提交表单之前就生成了订单，所以状态设为-99，而状态设为100是针对app里提交订单时才生成真实订单***
                orderInfo.StatusDesc = "未审核";
                //orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                //获取订单号
                TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                //if (!string.IsNullOrEmpty(orderInfo.StoreId))  //发货门店为空时，订单编号根据商户ID保存订单的最大值处理 update by Henry 2014-11-27 21:01:21
                orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo();//生成订单号
                //else
                //    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo,loggingSessionInfo.ClientID);
            }

            if (orderInfo.Status == null || orderInfo.Status.Equals(""))
            {
                orderInfo.Status = ToStr(reqObj.special.status);
            }


            int i = 1;
            orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
            foreach (var detail in reqObj.special.orderDetailList)
            {
                InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                orderDetailInfo.order_id = orderInfo.OrderId;
                orderDetailInfo.item_name = detail.itemName;
                orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
                orderDetailInfo.sku_id = ToStr(detail.skuId);
                orderDetailInfo.enter_qty = ToInt(detail.qty);
                orderDetailInfo.order_qty = ToInt(detail.qty);
                orderDetailInfo.std_price = Convert.ToDecimal(detail.salesPrice);
                orderDetailInfo.unit_id = orderInfo.StoreId;
                orderDetailInfo.display_index = i;
                #region update by wzq
                //orderDetailInfo.enter_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                orderDetailInfo.enter_price = orderDetailInfo.std_price;
                #endregion
                orderDetailInfo.enter_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                orderDetailInfo.retail_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                orderDetailInfo.retail_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;

                orderDetailInfo.Field1 = ToStr(detail.beginDate);
                orderDetailInfo.Field2 = ToStr(detail.endDate);
                orderDetailInfo.Field3 = ToStr(detail.appointmentTime);
                //前台存放会员折扣（根据会员卡等级判断折扣率）
                var vipID = rp.UserID;
                //根据vipID获取当前折扣信息
                var rate = new VipBLL(loggingSessionInfo).GetVipSale(vipID);
                orderDetailInfo.discount_rate = rate;
                //orderDetailInfo.discount_rate = 100

                decimal ReturnCash = 0.00m;
                decimal.TryParse(detail.ReturnCash, out ReturnCash);
                orderDetailInfo.ReturnCash = ReturnCash;

                i++;
                orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
            }
            #endregion

            string strError = string.Empty;
            string strMsg = string.Empty;
            bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);

            #region 更新发货门店
            /**
                //人人销售分配发货门店 add by Henry 2014-11-27
                if (reqObj.common.channelId == "6" || reqObj.common.channelId == "11" || reqObj.common.channelId == "10")
                {
                    TInoutBLL inoutBll = new TInoutBLL(loggingSessionInfo);
                    TUnitBLL unitBll = new TUnitBLL(loggingSessionInfo);
                    string unit = string.Empty;                 //发货门店
                    var inoutEntity = inoutBll.GetByID(orderInfo.OrderId);
                    if (inoutEntity != null)
                    {
                        var vipInfo = new VipBLL(loggingSessionInfo).GetByID(reqObj.common.userId);
                        //判断会员会集店是否是当前卖家的
                        if (!string.IsNullOrEmpty(vipInfo.CouponInfo))
                        {
                            var unitEntity = unitBll.GetByID(vipInfo.CouponInfo);
                            if (unitEntity != null)
                            {
                                if (unitEntity.CustomerID == loggingSessionInfo.ClientID)
                                {
                                    unit = vipInfo.CouponInfo;
                                    inoutEntity.SalesUnitID = unit; //消费门店
                                    inoutEntity.UnitID = unit;       //配送门店
                                }
                            }
                        }
                        inoutBll.Update(inoutEntity);
                    }
                    //TO DO 根据收货地址获取经纬度，后期处理
                }
                 * **/
            #endregion

            //如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
            //TOrderCustomerDeliveryStrategyMappingBLL tOrderCustomerDeliveryStrategyMappingBLL = new TOrderCustomerDeliveryStrategyMappingBLL(loggingSessionInfo);
            //tOrderCustomerDeliveryStrategyMappingBLL.UpdateOrderAddDeliveryAmount(orderInfo.OrderId, customerId);

            #region 返回信息设置

            //respData.content = new setOrderInfoNewRespContentData();
            //respData.content.orderId = orderInfo.OrderId;
            //respData.exception = strError;
            //respData.description = strMsg;
            //if (!bReturn)
            //{
            //    respData.code = "111";
            //}
            //}
            //catch (Exception ex)
            //{
            //    //respData.code = "103";
            //    //respData.description = "数据库操作错误" + ex.Message;
            //    //respData.exception = ex.ToString();
            //}
            #endregion

            var inoutbll = new InoutService(loggingSessionInfo);
            var flag = inoutbll.UpdateOrderDeliveryStatus(orderInfo.OrderId, "100",
                                   Utils.GetNow(), null, null);
            if (!flag)
            {
                throw new APIException("更新订单状态失败") { ErrorCode = 103 };
            }

            #region 订单扩展属性
            TInoutExpandBLL intoutExpandBll = new TInoutExpandBLL(loggingSessionInfo);
            TInoutExpandEntity inoutExpandEntity = new TInoutExpandEntity();
            inoutExpandEntity.OrderId = orderInfo.OrderId;
            inoutExpandEntity.APPChannelID = Convert.ToInt32(rp.ChannelId);
            //inoutExpandEntity.OSSId = rp.Parameters.OSSId;
            inoutExpandEntity.CreateBy = loggingSessionInfo.CurrentUser.Create_User_Id;
            inoutExpandEntity.CreateTime = DateTime.Now;
            inoutExpandEntity.LastUpdateBy = loggingSessionInfo.CurrentUser.Create_User_Id;
            inoutExpandEntity.LastUpdateTime = DateTime.Now;
            inoutExpandEntity.IsDelete = 0;
            //inoutExpandEntity.RecommandVip = rp.Parameters.RecommandVip;
            if (intoutExpandBll.GetByID(orderInfo.OrderId) == null)
            //如果有数据就修改，没有数据就插入
            {
                intoutExpandBll.Create(inoutExpandEntity, null);
            }
            else
            {
                intoutExpandBll.Update(inoutExpandEntity, null);
            }
            #endregion

            orderInfo.Status = "100";

            //增加订单操作记录 Add By Henry 2015-7-29
            var tinoutStatusBLL = new TInoutStatusBLL(loggingSessionInfo);
            TInoutStatusEntity statusEntity = new TInoutStatusEntity()
            {
                OrderID = orderInfo.OrderId,
                OrderStatus = int.Parse(orderInfo.Status),
                CustomerID = loggingSessionInfo.ClientID,
                StatusRemark = "提交订单[操作人:用户]"
            };
            tinoutStatusBLL.Create(statusEntity);

            //设置订单为已经支付
            TInoutEntity Tinout = new TInoutEntity();
            Tinout.OrderID = orderInfo.OrderId;
            Tinout.Field1 = "1";
            TInoutBLL TInoutBLL = new BS.BLL.TInoutBLL(loggingSessionInfo);
            TInoutBLL.Update(Tinout, null, false);

            #region 使用优惠券

            #region Insert优惠券与订单的映射表，使用优惠券
            var tOrderCouponMapping = new TOrderCouponMappingBLL(loggingSessionInfo);

            var tOrderCouponMappingEntity = new TOrderCouponMappingEntity
            {
                MappingId = Utils.NewGuid(),
                OrderId = orderInfo.OrderId,
                CouponId = couponId
            };
            tOrderCouponMapping.Create(tOrderCouponMappingEntity, null);
            #endregion
            #region Update Coupon Status = 1
            couponEntity.Status = 1;
            couponBll.Update(couponEntity, null);
            #endregion

            #endregion

            #region 清空购物车 update by wzq
            var shopchatbll = new ShoppingCartBLL(loggingSessionInfo);
            shopchatbll.DeleteShoppingCart(orderInfo.OrderId);
            #endregion

            OnlineShoppingItemBLL service2 = new OnlineShoppingItemBLL(loggingSessionInfo);
            orderInfo.DeliveryId = "1";
            orderInfo.OrderId = ToStr(orderInfo.OrderId);
            orderInfo.linkMan = ToStr(rp.Parameters.linkMan);
            orderInfo.linkTel = ToStr(rp.Parameters.linkTel);
            orderInfo.address = ToStr(rp.Parameters.address);
            service2.SetOrderAddress(orderInfo);

            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 取消订单

        public string SetCancelOrder(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetOrderStatusRP>>();
            var rd = new EmptyResponseData();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var orderId = rp.Parameters.OrderId;
            var inoutBll = new T_InoutBLL(loggingSessionInfo);              //订单业务实例化

            var inoutInfo = inoutBll.GetInoutInfo(orderId, loggingSessionInfo);

            var errorRsp = new SuccessResponse<IAPIResponseData>(rd);
            if (inoutInfo.status == "800")//已取消
            {
                errorRsp.Message = "操作已处理";
                errorRsp.ResultCode = 302;
                return errorRsp.ToJSON();
            }
            else if (inoutInfo.status == "700")//已完成
            {
                errorRsp.Message = "订单已完成，不能进行取消订单操作";
                errorRsp.ResultCode = 302;
                return errorRsp.ToJSON();
            }
            //执行取消订单业务 reconstruction By Henry 2015-10-20
            inoutBll.SetCancelOrder(orderId, 0, loggingSessionInfo);

            //增加订单操作记录 Add By Henry 2015-8-18
            var tinoutStatusBLL = new TInoutStatusBLL(loggingSessionInfo);
            TInoutStatusEntity statusEntity = new TInoutStatusEntity()
            {
                OrderID = orderId,
                OrderStatus = 800,
                CustomerID = loggingSessionInfo.ClientID,
                StatusRemark = "取消订单[操作人:用户]"
            };
            tinoutStatusBLL.Create(statusEntity);

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
                case "VoucherCouponOrder":
                    rst = this.VoucherCouponOrder(pRequest);
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
        /// <summary>
        /// 启用积分;0=不启用；1=启用
        /// </summary>
        public int EnableIntegral { get; set; }
        /// <summary>
        /// 启用返现;0=不启用；1=启用
        /// </summary>
        public int EnableRewardCash { get; set; }
        //可用积分
        public decimal Integral { get; set; }
        //描述
        public string IntegralDesc { get; set; }
        //可兑换金额
        public decimal IntegralAmount { get; set; }
        //账户余额
        public decimal VipEndAmount { get; set; }
        //可使用的返现
        public decimal ReturnAmount { get; set; }
        //积分最低使用限制
        public int PointsRedeemLowestLimit { get; set; }
        //返现最低使用限制
        public decimal CashRedeemLowestLimit { get; set; }
        //会员折扣
        public decimal VipDiscount { get; set; }
    }

    public class GetVipIntegralRP : IAPIRequestParameter
    {
        public SkuIdAndQtyInfo[] SkuIdAndQtyList { get; set; }

        /// <summary>
        /// 优惠券适用范围(1=购物券；2=服务券)
        /// </summary>
        public int UsableRange { get; set; }
        /// <summary>
        /// 优惠券使用门店/分销商ID
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 是否是抵用券（0=包含抵用券；1=不包含抵用券）
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否有折扣（1=无折扣；0=有折扣）
        /// </summary>
        public int DiscountType { get; set; }

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
        /// <summary>
        /// 编码
        /// </summary>
        public string CouponCode { get; set; }
        //优惠券名称
        public string CouponName { get; set; }
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
        //是否开通我的小店
        public int? isStore { get; set; }
        //余额
        public decimal? EndAmount { get; set; }
        //积分
        public decimal? EndIntegral { get; set; }
        //邀请人数
        public int inviteCount { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string VipName { get; set; }

        /// <summary>
        /// 用户汇集店
        /// </summary>
        public string UnitId { get; set; }

    }

    /// <summary>
    /// 地址列表
    /// </summary>
    public class AddressInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string VipAddressID { get; set; }
        public string VipID { get; set; }
        public string LinkMan { get; set; }
        public string LinkTel { get; set; }
        public string Province { get; set; }//省
        public string CityID { get; set; }
        public string CityName { get; set; }//市
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public int? isDefault { get; set; }
    }

    public class SetVipInfoRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 会员名（微信名）
        /// </summary>
        public string VipName { get; set; }

        /// <summary>
        /// 微信公众号ID
        /// </summary>
        public string WeiXin { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        public void Validate()
        {
        }
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
        /// 可使用积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 可使用积分抵扣金额
        /// </summary>
        public decimal IntegralAmount { get; set; }
        /// <summary>
        /// 是否选用余额 1=是，0=否
        /// </summary>
        public int VipEndAmountFlag { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal VipEndAmount { get; set; }
        /// <summary>
        /// 是否使用返现 1=是，0=否
        /// </summary>
        public int ReturnAmountFlag { get; set; }
        /// <summary>
        /// 返现金额
        /// </summary>
        public decimal ReturnAmount { get; set; }
        /// <summary>
        /// 会员折扣价格
        /// </summary>
        public decimal VipDiscount { get; set; }
        public string Remark { get; set; }

        //场景
        //add by donal 2014-10-14 10:03:30
        public int OSSId { get; set; }

        //推荐人
        //add by donal 2014-10-14 10:03:17
        public string RecommandVip { get; set; }
        /// <summary>
        /// 发票信息
        /// </summary>
        public string Invoice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryAmount { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 店主VIPID
        /// </summary>
        public string OwnerVipID { get; set; }

        /// <summary>
        /// 分销商ID 
        /// </summary>
        public string RetailTraderId { get; set; }

        /// <summary>
        /// 砍价参与主标识
        /// </summary>
        public string KJEventJoinId { get; set; }
        public void Validate()
        {
        }
    }



    public class VoucherCouponOrderRP : IAPIRequestParameter
    {
        //public string OrderId { get; set; }
        //public string Status { get; set; }

        /// <summary>
        /// 是否选用优惠券  1=是，0=否
        /// </summary>
        //  public int CouponFlag { get; set; }

        /// <summary>
        /// 优惠券标识
        /// </summary>
        public string CouponId { get; set; }


        public string Remark { get; set; }

        //public string deliveryId { get; set; }
        //public string orderID { get; set; }
        public string linkMan { get; set; }
        public string linkTel { get; set; }
        public string address { get; set; }

        //场景
        //add by donal 2014-10-14 10:03:30
        //public int OSSId { get; set; }

        //推荐人
        //add by donal 2014-10-14 10:03:17
        // public string RecommandVip { get; set; }
        public void Validate()
        {
        }
    }


    public class setOrderDetailClass
    {
        public string skuId { get; set; }       //商品SKU标识
        public string salesPrice { get; set; }  //商品销售单价
        public string qty { get; set; }         //商品数量
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string appointmentTime { get; set; }//added by zhangwei 2014-2-26预约时间
        public string itemName { get; set; }

        public string ReturnCash { get; set; } //返现金额
    }

    /// <summary>
    /// 传输的参数对象
    /// </summary>
    public class setOrderInfoNewReqData
    {
        public setOrderInfoNewReqSpecialData special;
    }
    /// <summary>
    /// 特殊参数对象
    /// </summary>
    public class setOrderInfoNewReqSpecialData
    {
        public string qty { get; set; }		    //商品数量
        public string storeId { get; set; }		//门店标识
        public string totalAmount { get; set; }		//订单总价
        public string mobile { get; set; }		//手机号码
        public string deliveryId { get; set; }//		配送方式标识
        public string deliveryAddress { get; set; }//	配送地址
        public string deliveryTime { get; set; }//		提货时间（配送时间）
        public string email { get; set; }
        public string remark { get; set; }
        public string username { get; set; }
        public string tableNumber { get; set; }
        public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
        public string actualAmount { get; set; }    //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
        public string reqBy { get; set; }   //请求0-wap,1-手机.
        public string joinNo { get; set; }  //餐饮--人数
        public string status { get; set; }
        public string isGroupBy { get; set; }  //是否团购订单（Jermyn20140318—Field15）
        public IList<setOrderDetailClass> orderDetailList { get; set; }
        //    public IList<setOrderCouponClass> couponList { get; set; }  //优惠券集合 （Jermyn20131213--tordercouponmapping
        public int dataFromId { get; set; }
        public string SalesUser { get; set; } //店员ID add by donal 2014-9-25 18:07:11
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