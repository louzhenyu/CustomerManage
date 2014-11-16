using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    public partial class HouseTest : System.Web.UI.Page
    {
        private const string m_customerID = "2aa965e35e14485e9eb000be599f8355";
        //      e0de27261a554482adaa50631637cf7a
        private const string m_userID = "9502c38967574538a610b68923aa1a07";
        private const string m_detailID = "611344F3-7297-460D-BEF1-0F62BF34E0C4";
        private const string m_logisticsinfo = "08881000000874111";  //交易号  
        //你买号成功，只是委托成功

        public HanAnRequestMessage Model { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);
            //WXHouseReservationRedeemBLL redeemBll = new WXHouseReservationRedeemBLL(loggingSessionInfo);
            //WXHouseReservationRedeemEntity entity = redeemBll.GetByPrePaymentID(m_customerID, "6D16CBF0-D07F-4BDA-9521-F9DEC7FF6A03");
            //if (entity != null)
            //{
            //    entity.FundState = 1;
            //    redeemBll.Update(entity);
            //}
            //else
            //{
            //    entity = new WXHouseReservationRedeemEntity();
            //    entity.RedeemID = Guid.NewGuid();
            //    entity.PrePaymentID = new Guid("6D16CBF0-D07F-4BDA-9521-F9DEC7FF6A03");
            //    entity.Fundtype = (int)FundtypeeEnum.ReservationRedeem;
            //    entity.FundState = 1;
            //    entity.Merchantdate = "20101010";  //商户日期
            //    entity.CustomerID = m_customerID;

            //    redeemBll.Create(entity);
            //}

            //WXHouseAssignbuyerBLL BLL = new WXHouseAssignbuyerBLL(loggingSessionInfo);
            //string maxAssignbuyer = BLL.GetWXHouseMaxAssignbuyer(m_customerID);

            //Int64 maxValue = 0;
            //Int64.TryParse(maxAssignbuyer, out maxValue);
            //maxValue = maxValue + 1;
            //string str = maxValue.ToString().PadLeft(10, '0');


            //GetProfit();
            Model = new HanAnRequestMessage();
            //GetProfit();
            //Model = new HanAnRequestMessage();
            //string str = Utility.GenMD5("1D2EFD0B19234B3AA1B638536244B1FE");

            //Response.Write(str);
            //GetProfit();

            //BuyFund();
            //FundRansom();

            //PayHouse();
            //Buy();
            //GetProfit();
            // PayHouse();
            // ShuHui();

            string str = @"k7OGKGvoVMzK705oR1XRG1hTEr0aqT5/0zFdWmPAJ6gAfuB92BI8a5EWNdsqILwhVX8wzhycV7CmL8yLqjLaL8kEkBiWrApb5OAUIX15A2lEUcPCABYNYz2F3fgkHiOHEWQflt2lQeTguJcaFt3dru782UobY6TvyDaB9CReqYGQFmoL7L0Vg06MILvKM2jB9ARtUw25tTFfXQxfCr7zcfaaDVWHJer0JHizALV9/1tsFGpx3eTIljqg9pLb46cpxfxe8H3YcRkSgX+PMwoWC5RJ/IlTT8jIegJEavVvRi9FehzThbffsUm5agO/n/MlnPmKc3FfibzEDY17BjPeuRzWIglaTP1Wqx9YXZDRrvdLmQbS2KWf3Iy/iIzfxaHAT4SKeISzpMQDFEPXUFcFMUvYfgvocoKxqJmkVT4okK+qpdLVbQHkxlhvDbx2nK398l/uqtnm/0HHGXg5QAjSplONU1k9xziqDoWZODDAkTTJQnbaeIAjcxwKKg9pYjL6Jo+BbuMQC26Y3OvySvzhGKKk4wqWeoIXS72vVuUZjidOjWSRtkP9n/6x7N1YJVdFawNv8HeMdHmh3X3Jtz+CbYCQ4Xk7MfZ9Tmd6/mKc3evBoOqWxXVUdmoK2j1sdsjRyhtQPHCdeJvJtHtm5B7FyCF4nG9HPbEgnzW46A4yQO2fQUAsgrBQoo73uv+1IjODKMK2ldqxJnMiiFBq+5DU9rmDalbs92olYL9iTnx2usPeRFjdclw4hoNSTFh71mMdi30qFqFRHGy8GwYeYkzXHTsY8G6Sv7KXd5CovFAZ6l+gZjc7Tc0G9UonaY2yiNkrDCb3Zeo50Pal1G70iZ5Z30BWvAh6EcDf8UEnZ+TH1cndfyaS1jE6HaWOvWbpYUA/oIbMvabjVJYBHNdYdAcB26wJUHBDkfygpDMMzkdHNNrknyfurFbVKGTuHRqU5Sud6fsHtH6zfamm1GpTbrZA3V9Rtkwxg81HQcpwagUKkpTF1noAZiDSRz25UTtOJ0pXQqY/us4HvjsIkcK+6ZiWiVjajqxM+WGKKO09cGkoB39VidCofRqn2OcADYGYwctfz7vKsj0e3GyOkx/wYs/ATfMbIBDF6z6Ays0TW8bYMOd/atrMDqwOR7mQZPQiOc4XVVqmbQIRYFQtE7SmTTzKovC9VneMs8EvK1I/cljAv85bqZlAct9CJnedsmwtFBkrT8rDs6I8uyr/f+f9nUV7byytlgX+yjZM/uqu/O5P/KrwIPVzc2u30f8Ulfx45pky";
            str = Utility.AESDecrypt(str, "PgYh9jXj5fbkFBkNYYHN/A==");
            //string content5002 = Utility.AESDecrypt(@"glW2I6VlxSnL8x6IzrJDMGHn0VCkEF93Rq1r9ERONPLnH/p7dNipapnPOrnKQ02URLk6v+2VwYWkoP93unghzQ1oVFsTYLX1DvwWu8lsazXo/BvY4TaTR52Iqb7XwfX7Yr12m0Xv24VPCgKcSTT0AzfFvv3Ks5bOgizM2+EPpX9Z8VhFGiBI6mDKDQEDf5X4qyfSEtt1BuMBtehJRXl61zGUm1vqRuYg0rfN2b9UyUwaTbnDFF+QYXyafWkXOgIa", HuaAnConst.AesKey);

            //string content3001 = Utility.AESDecrypt(@"glW2I6VlxSnL8x6IzrJDMGHn0VCkEF93Rq1r9ERONPLnH/p7dNipapnPOrnKQ02URLk6v+2VwYWkoP93unghzXt5iOgMY/LidXLJQTSZU7Y2UcNi0WmmY02O+2gpq/p5SG70u4GK0G7OPapk47YmjoVOUGvtFrkN0CBEEkmY9EUcN4owYQobe1xopDd7+D5PjzX+VYkY9FUx5L4yRQMNBTZ0aK1f9hTRz9wMea5b+NrjYVstqjKf92+ctZDK9VEk", HuaAnConst.AesKey);

            //string content500 = Utility.AESDecrypt(@"glW2I6VlxSnL8x6IzrJDMGHn0VCkEF93Rq1r9ERONPLnH/p7dNipapnPOrnKQ02URLk6v+2VwYWkoP93unghzXt5iOgMY/LidXLJQTSZU7Y3Cy/GbE5NeiZwGmDmJhETpXgwO94rScYLbEeprrzKdA==", HuaAnConst.AesKey);

            string content5001 = Utility.AESDecrypt(@"glW2I6VlxSnL8x6IzrJDMGHn0VCkEF93Rq1r9ERONPLnH/p7dNipapnPOrnKQ02URLk6v+2VwYWkoP93unghzQ1oVFsTYLX1DvwWu8lsazXo/BvY4TaTR52Iqb7XwfX7Yr12m0Xv24VPCgKcSTT0AzfFvv3Ks5bOgizM2+EPpX9Z8VhFGiBI6mDKDQEDf5X4qyfSEtt1BuMBtehJRXl61zGUm1vqRuYg0rfN2b9UyUwaTbnDFF+QYXyafWkXOgIa", HuaAnConfigurationAppSitting.AesKey);

            //string content5001 = Utility.AESDecrypt(@"glW2I6VlxSnL8x6IzrJDMGHn0VCkEF93Rq1r9ERONPJrCSdPBucyszXenLtFuYN26LTlktV3p1tTmqe+hO/8liw1kYTHkHOueMftWD8swivzok2ssOIINtU3013+d4kdyErQrBCqqOEMHq/oPiawXU2oop/PSHa9Z1Lx0NffEoRl/VvXwABAK5W2HzQj4Lydbpkb9UBE3rNWsMVTJRIHBWgmWuq2S1x3fJwXx36GOoP/vuNUUkOidq/EeCFgJz+vtQSAO0KZDUJweVRRySsD1GX9W9fAAEArlbYfNCPgvJ2HF8Cgt8bggB3VVLJwBl1KYk7zGb6VQbLoevNpiJ1pxlCw/2J2Eap4T3Ku1EjCHFyB7eZovdxfSfgEgHwo9MgQyVZVs3/RBu/dScLlGW2XrzeQORqxSyhELluR4ptUGEI=", HuaAnConst.AesKey);
        }




        #region 基金购买
        /// <summary>
        /// 基金购买
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        public void BuyFund()
        {

            DateTime dt = DateTime.Now;
            string orderNO = HuaAnFactory.GenerateSeqNO();
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);

            WXHouseDetailBLL wxhdbll = new WXHouseDetailBLL(loggingSessionInfo);
            WXHouseDetailEntity wxhde = wxhdbll.GetDetailByID(m_customerID, m_detailID);

            if (wxhde == null) throw new Exception("没有找到该楼盘信息。");

            string realPay = wxhde.RealPay.ToString();

            //产生订单号
            TUnitExpandBLL TUeBll = new TUnitExpandBLL(loggingSessionInfo);
            // string seqNO = TUeBll.GetUnitOrderNo(loggingSessionInfo, "ed7d227564b54778a4cffb7335a8b078");

            string seqNO = HuaAnFactory.GenerateSeqNO();
            //1判断会员楼盘明细映射是否存在记录
            WXHouseVipMappingBLL bll = new WXHouseVipMappingBLL(loggingSessionInfo);
            DataSet ds = bll.VerifWXHouseVipMapping(m_userID, m_detailID, m_customerID);
            Guid mappingID = Guid.NewGuid();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //1.1存在
                mappingID = new Guid(ds.Tables[0].Rows[0]["MappingID"].ToString());
            }
            else
            {
                //1.2不存在
                //插入会员楼盘明细映射表
                WXHouseVipMappingEntity WXHvmEntity = new WXHouseVipMappingEntity();
                WXHvmEntity.MappingID = mappingID;
                WXHvmEntity.DetailID = new Guid(m_detailID);
                WXHvmEntity.CustomerID = m_customerID;
                WXHvmEntity.VIPID = m_userID;
                //WXHvmEntity.HouseSaleNo = "";//预售号码
                WXHvmEntity.ReserveNo = orderNO;
                //WXHvmEntity.HoseMessage = "";//房子描述信息
                WXHvmEntity.HoseState = 0;
                WXHvmEntity.IsBuyHose = (int)PayHouseStateEnum.Unknown;
                WXHvmEntity.IsBuyFund = 1;
                WXHvmEntity.IsRedeem = (int)FundStateEnum.Unknown;
                bll.Create(WXHvmEntity);
            }
            //1.3插入交易手续费表
            Guid feeID = Guid.NewGuid();
            WXHouseTradeFeeEntity WXHtfEntity = new WXHouseTradeFeeEntity();
            WXHtfEntity.FeeID = feeID;
            WXHtfEntity.TradeType = 0;
            WXHtfEntity.FeeType = 0;
            WXHtfEntity.Fee = 0;
            WXHtfEntity.CustomerID = m_customerID;
            WXHouseTradeFeeBLL WXHtfBll = new WXHouseTradeFeeBLL(loggingSessionInfo);
            WXHtfBll.Create(WXHtfEntity);
            ////1.4插入预付款订单
            //WXHousePrePaymentEntity WXHppEntity = new WXHousePrePaymentEntity();
            //Guid prePaymentID = Guid.NewGuid();
            //WXHppEntity.PrePaymentID = prePaymentID;
            //WXHppEntity.FeeID = feeID;
            //WXHppEntity.MappingID = mappingID;
            //WXHppEntity.OrderNo = orderNO;
            //WXHppEntity.RealPay = Convert.ToDecimal(realPay);
            //WXHppEntity.OrderDate = dt.ToString();
            //WXHppEntity.CustomerID = m_customerID;
            //WXHousePrePaymentBLL WXHppBll = new WXHousePrePaymentBLL(loggingSessionInfo);
            //WXHppBll.Create(WXHppEntity);

            //创建会员客户协议号映射
            //CreateClientAgreementNO(rp, loggingSessionInfo);

            //处理调用华安请求From表单对象
            Receive.ReceiveBuyMessage pEnity = new Receive.ReceiveBuyMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Totalamt = Convert.ToDecimal(realPay);
            VipBLL vbll = new VipBLL(loggingSessionInfo);
            VipEntity ve = vbll.GetVipDetailByVipID(m_userID);
            pEnity.Assignbuyer = ve.VipCode;
            pEnity.Assbuyername = ve.VipName;
            pEnity.Assbuyermobile = ve.Phone;
            pEnity.Fee = "0";
            //用于回调更新
            string strCommon = "CustomerID=" + m_customerID;
            strCommon += "|UserID=" + m_userID;
            strCommon += "|HouseDetailID=" + m_detailID;
            strCommon += "|MappingID=" + mappingID.ToString();
            //  strCommon += "|PrePaymentID=" + prePaymentID.ToString();
            strCommon += "|ToPageURL=http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallBack.aspx?action=BuyCallBack";
            pEnity.Commonreturn = strCommon;
            //回调url
            pEnity.RetURL = "";
            pEnity.PageURL = string.Format(HuaAnConfigurationAppSitting.CallBackPageUrl, "BuyCallBack");
            //pEnity.PageURL = "http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallBack.aspx?action=BuyCallBack";
            pEnity.Memo = "";
            //请求表单对象
            var fromList = new HuaAnFactory().FormRequestContent(dt, Utility.GetRequsetXml(pEnity), HuaAnConfigurationAppSitting.Buy, orderNO);
            var rdData = new PayEntityRD();
            rdData.FormData = fromList;
            //华安url
            rdData.Url = HuaAnConfigurationAppSitting.ReservationPurchaseUrl;
            //请求表单对象
            Model = new HuaAnFactory().FormRequestContent(dt, Utility.GetRequsetXml(pEnity), HuaAnConfigurationAppSitting.Buy, seqNO);

        }
        #endregion
        #region   基金赎回
        /// <summary>
        /// 基金赎回。
        /// </summary>
        /// <param name="rRequest"></param>
        /// <returns></returns>
        private void FundRansom()
        {
            string seqNO = HuaAnFactory.GenerateSeqNO();
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);

            //处理调用华安请求From表单对象
            DateTime dt = DateTime.Now;
            ReceiveRansomMessage pEnity = AssertRanson(loggingSessionInfo, m_customerID, m_userID, m_detailID, dt);
            string strContent = Utility.GetRequsetXml(pEnity);
            //HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConst.Redemption, seqNO);

            Model = new HuaAnFactory().FormRequestContent(dt, Utility.GetRequsetXml(pEnity), HuaAnConfigurationAppSitting.Redemption, seqNO);

        }

        /// <summary>
        /// （基金赎回）2201
        /// </summary>
        /// <param name="dt">当前时间</param>
        /// <param name="pLogisticsinfo">交易号</param>
        /// <returns></returns>
        private ReceiveRansomMessage AssertRanson(LoggingSessionInfo pLoggingSessionInfo, string customerID, string userID, string detailID, DateTime dt)
        {
            //获取用户信息
            VipBLL vbll = new VipBLL(pLoggingSessionInfo);
            VipEntity vip = vbll.GetVipDetailByVipID(userID);
            if (vip == null)
            {
                throw new Exception("该用户不存在！");
            }


            //交易号
            string logisticsinfo = m_logisticsinfo;
            //客户协议号
            //string assignbuyer = GetClientAgreementNo(pLoggingSessionInfo, userID, customerID);
            string assignbuyer = vip.VipCode;

            ReceiveRansomMessage pEnity = new ReceiveRansomMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Logisticsinfo = logisticsinfo;    // 交易号
            pEnity.Assignbuyer = assignbuyer;       //客户协议号 ：客户协议号唯一的标识世联的用户和华安用户之前的绑定关系。备注：一个用户只能绑定一次，所以一个用户只能有一个协议号
            pEnity.Assbuyername = vip.VipName;
            pEnity.Assbuyermobile = vip.Phone;

            pEnity.Fee = "0";
            //用于回调更新 &=>&amp;
            pEnity.Commonreturn = "mappingID=" + 1000 + "&amp;prePaymentID=" + 100;
            //回调url
            pEnity.PageURL = string.Format(HuaAnConfigurationAppSitting.CallBackPageUrl, "RansomCallBack");
            pEnity.Memo = "";
            pEnity.RetURL = "";

            return pEnity;
        }
        #endregion


        #region   基金过户（用号）
        /// <summary>
        ///基金过户（用号） 2001
        /// </summary>
        //http://222.66.40.26/huaan-worldunion/t/ReservationPay.action
        private void PayHouse()
        {

            ////世联通讯流水号
            //string seqNO = HuaAnFactory.GenerateSeqNO();
            //DateTime dt = DateTime.Now;

            //var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);

            ////处理调用华安请求From表单对象
            //Receive.ReceivePayMessage pEnity = AssertPay(loggingSessionInfo, m_customerID, m_userID, m_detailID, dt);
            //string strContent = Utility.GetRequsetXml(pEnity);
            //HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConst.Pay, seqNO);
            //Model = rMessage;
        }

        ///// <summary>
        ///// 世联支付,过户。
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //private ReceivePayMessage AssertPay(LoggingSessionInfo pLoggingSessionInfo, string customerID, string userID, string detailID, DateTime dt)
        //{
        //    //获取会员信息
        //    VipBLL vbll = new VipBLL(pLoggingSessionInfo);
        //    VipEntity vipEntity = vbll.GetVipDetailByVipID(userID);
        //    if (vipEntity == null)
        //    {
        //        throw new Exception("没有找到该用户信息");
        //    }

        //    //获取订单相关信息。
        //    WXHousePrePaymentBLL mappingBll = new WXHousePrePaymentBLL(pLoggingSessionInfo);
        //    WXHousePrePaymentEntity pay = mappingBll.GetWXHousePrePayment(detailID, customerID);
        //    if (pay == null)
        //    {
        //        throw new Exception("未找到该用户的订单信息！");
        //    }

        //    //获取楼盘详细信息
        //    WXHouseDetailBLL wxhdbll = new WXHouseDetailBLL(pLoggingSessionInfo);
        //    DataSet ds = wxhdbll.GetHouseDetailByDetailID(customerID, detailID);
        //    HouseDetailViewModel detailEntity = null;
        //    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        detailEntity = DataTableToObject.ConvertToObject<HouseDetailViewModel>(ds.Tables[0].Rows[0]);
        //    }
        //    if (detailEntity == null) throw new Exception("该楼盘不存在。");

        //    ReceivePayMessage pEnity = new ReceivePayMessage();
        //    pEnity.MerchantID = HuaAnConst.MerchantID;
        //    pEnity.Merchantdate = dt.ToString("yyyyMMdd");
        //    pEnity.OrderNO = pay.OrderNo;  //订单号
        //    pEnity.Totalpay = pay.RealPay.HasValue ? pay.RealPay.Value.ToString() : null;
        //    pEnity.Assignbuyer = vipEntity.VipCode;  //客户协议号
        //    pEnity.Assbuyername = vipEntity.VipName;
        //    pEnity.Assbuyermobile = vipEntity.Phone;
        //    pEnity.Totaldiscount = "0";
        //    pEnity.Totaldeduction = "0";
        //    pEnity.Actualtotal = pay.RealPay.HasValue ? pay.RealPay.Value.ToString() : null;
        //    pEnity.Feetype = "0";
        //    pEnity.Fee = "0";
        //    pEnity.Logisticsinfo = m_logisticsinfo;  //交易号

        //    Goodsinfo info = new Goodsinfo
        //    {
        //        id = detailEntity.DetailID.ToString(),
        //        Goodsname = detailEntity.HouseName,
        //        GoodsURL = "",
        //        Goodspicture = "",
        //        Goodsmodle = detailEntity.HouseID.ToString(),
        //        Goodsdesc = "",
        //        Goodsunit = "",
        //        Goodsprice = "",
        //        Goodsquantity = "",
        //        Goodsdiscount = "",
        //        Goodsdeduction = "",
        //        Goodstotalpay = "",
        //        Goodsactualtotal = detailEntity.RealPay.ToString(),
        //        memo = ""
        //    };
        //    //用于回调更新 &=>&amp;
        //    pEnity.Commonreturn = "";
        //    pEnity.ISDirectRedeem = "0";
        //    //回调url
        //    pEnity.PageURL = string.Format(HuaAnConst.CallBackPageUrl, "TransferCallBack");
        //    pEnity.Memo = "";
        //    pEnity.RetURL = "";

        //    return pEnity;
        //}
        #endregion

        #region  购买基金
        /// <summary>
        /// 购买基金 2101。
        /// </summary>
        /// 
        //http://222.66.40.26/huaan-worldunion/t/ReservationPurchase.action
        private void Buy()
        {
            string mappingID = "123", prePaymentID = "456";
            //世联通讯流水号
            string seqNO = "20141517";
            DateTime dt = DateTime.Now;

            //处理调用华安请求From表单对象
            Receive.ReceiveBuyMessage pEnity = AssignmentBuy(mappingID, prePaymentID, dt);   //基金购买
            string strContent = Utility.GetRequsetXml(pEnity);
            HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConfigurationAppSitting.Buy, seqNO);

            Model = rMessage;

            /*   回调回来的。
                http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallBack.aspx?type=Project&req={}&action=PayCallBack

<?xml version="1.0" encoding="UTF-8"?

><order><MerchantID>10000008</MerchantID><Retcode>0000</Retcode><Merchantdate>20140529</Merchantdate><Orgtotalamt>20000</Orgtotalamt><Assignbuyer>Vip00003698</Assignbu

yer><Assbuyername>谢伯恩

</Assbuyername><Assbuyermobile>18621698771</Assbuyermobile><Assbuyeridtp>0</Assbuyeridtp><Logisticsinfo>08889230000002725</Logisticsinfo><Fee>0</Fee><Fundtype>1</Fundt

ype><Commonreturn>mappingID=123&prePaymentID=456</Commonreturn></order>
             */
        }


        /// <summary>
        /// 买号（基金购买2101）。
        /// </summary>
        /// <param name="mappingID"></param>
        /// <param name="prePaymentID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static Receive.ReceiveBuyMessage AssignmentBuy(string mappingID, string prePaymentID, DateTime dt)
        {
            Receive.ReceiveBuyMessage pEnity = new Receive.ReceiveBuyMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Totalamt = 20000;

            pEnity.Assignbuyer = "Vip00003698";
            pEnity.Assbuyername = "王明";
            pEnity.Assbuyermobile = "15821529639";
            pEnity.Fee = "0";
            //用于回调更新 &=>&amp;
            pEnity.Commonreturn = "mappingID=" + mappingID + "&amp;prePaymentID=" + prePaymentID;
            //回调url
            pEnity.PageURL = HuaAnConfigurationAppSitting.CallBackPageUrl + "&amp;action=PayCallBack";
            pEnity.Memo = "";
            pEnity.RetURL = "www.baidu.com";
            return pEnity;
        }

        #endregion


        #region   支付接口 过户
        /// <summary>
        /// 支付 2001
        /// </summary>
        //http://222.66.40.26/huaan-worldunion/t/ReservationPay.action
        private void Pay()
        {
            // string mappingID = "123", prePaymentID = "456";
            //世联通讯流水号
            string seqNO = "20141517";
            DateTime dt = DateTime.Now;

            //处理调用华安请求From表单对象
            Receive.ReceivePayMessage pEnity = Pay(DateTime.Now);   //基金购买
            string strContent = Utility.GetRequsetXml(pEnity);
            HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConfigurationAppSitting.Pay, seqNO);

            Model = rMessage;

            /*

             */
        }

        /// <summary>
        /// 世联支付,过户。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceivePayMessage Pay(DateTime dt)
        {
            ReceivePayMessage pEnity = new ReceivePayMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.OrderNO = "08889230000002608";  //订单号
            pEnity.Totalpay = "20000";
            pEnity.Assignbuyer = "Vip00003698";  //客户协议号
            pEnity.Assbuyername = "王明";
            pEnity.Assbuyermobile = "15821529639";
            pEnity.Totaldiscount = "0.2";
            pEnity.Totaldeduction = "10000";
            pEnity.Actualtotal = "20000";
            pEnity.Feetype = "0";
            pEnity.Fee = "0";
            pEnity.Logisticsinfo = "111111111";  //交易号
            Goodsinfo info = new Goodsinfo
            {
                id = "1",
                Goodsname = "赢家3号",
                GoodsURL = "http://baidu.com",
                Goodspicture = "",
                Goodsmodle = "",
                Goodsdesc = "",
                Goodsunit = "",
                Goodsprice = "",
                Goodsquantity = "",
                Goodsdiscount = "",
                Goodsdeduction = "",
                Goodstotalpay = "",
                Goodsactualtotal = "",
                memo = ""
            };
            //用于回调更新 &=>&amp;
            pEnity.Commonreturn = "mappingID=" + 1000 + "&amp;prePaymentID=" + 100;
            pEnity.ISDirectRedeem = "0";
            //回调url
            pEnity.PageURL = HuaAnConfigurationAppSitting.CallBackPageUrl + "&amp;action=TransferCallBack";
            pEnity.Memo = "";
            pEnity.RetURL = "www.baidu.com";
            //Test
            // 获取年华收益率。
            //string strContent = @"<?xml version='1.0' encoding='UTF-8'?><order><MerchantID>10000008</MerchantID><Merchantdate>20140521</Merchantdate><Pageno>1</Pageno><RetURL>http://baidu.com</RetURL><Memo>买菜</Memo></order>";
            //基金购买（买号。）
            //string strContent = @"<?xml version='1.0' encoding='UTF-8'?><order><MerchantID>10000008</MerchantID><Merchantdate>20140521</Merchantdate><Totalamt>20000</Totalamt><Tradeappendinfo></Tradeappendinfo><Assignbuyer>Vip00003698</Assignbuyer><Assbuyername>孔凡俊</Assbuyername><Assbuyermobile>15821529639</Assbuyermobile><Fee>0</Fee><Commonreturn>公共回传字段</Commonreturn><RetURL>http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/huaan.htm</RetURL><Memo>此订单为测试订单</Memo></order>";

            return pEnity;

        }
        #endregion


        #region  基金赎回
        /// <summary>
        /// 基金赎回 2201
        /// </summary>
        //http://222.66.40.26/huaan-worldunion/t/ReservationRedeem.action
        private void ShuHui()
        {
            //string mappingID = "123", prePaymentID = "456";
            //世联通讯流水号
            string seqNO = "2014154501";
            DateTime dt = DateTime.Now;

            //处理调用华安请求From表单对象
            ReceiveRansomMessage pEnity = Ranson(DateTime.Now);
            string strContent = Utility.GetRequsetXml(pEnity);
            HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConfigurationAppSitting.Redemption, seqNO);

            Model = rMessage;

            /*
             http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallBack.aspx?type=Project&req={}&action=RansomCallBack
             * 
             */
        }


        /// <summary>
        /// （基金赎回）2201
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceiveRansomMessage Ranson(DateTime dt)
        {
            ReceiveRansomMessage pEnity = new ReceiveRansomMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Logisticsinfo = "08889230000002608";     // 交易号  暂时使用Guid
            pEnity.Assignbuyer = "Vip00003698";             //客户协议号 ：客户协议号唯一的标识世联的用户和华安用户之前的绑定关系。备注：一个用户只能绑定一次，所以一个用户只能有一个协议号
            pEnity.Assbuyername = "王明";
            pEnity.Assbuyermobile = "13641754375";
            pEnity.Fee = "0";
            //用于回调更新 &=>&amp;
            pEnity.Commonreturn = "mappingID=" + 1000 + "&amp;prePaymentID=" + 100;
            //回调url
            pEnity.PageURL = HuaAnConfigurationAppSitting.CallBackPageUrl + "&amp;action=RansomCallBack";
            pEnity.Memo = "";
            pEnity.RetURL = "www.baidu.com";
            //Test
            // 获取年华收益率。
            //string strContent = @"<?xml version='1.0' encoding='UTF-8'?><order><MerchantID>10000008</MerchantID><Merchantdate>20140521</Merchantdate><Pageno>1</Pageno><RetURL>http://baidu.com</RetURL><Memo>买菜</Memo></order>";
            //基金购买（买号。）
            //string strContent = @"<?xml version='1.0' encoding='UTF-8'?><order><MerchantID>10000008</MerchantID><Merchantdate>20140521</Merchantdate><Totalamt>20000</Totalamt><Tradeappendinfo></Tradeappendinfo><Assignbuyer>Vip00003698</Assignbuyer><Assbuyername>孔凡俊</Assbuyername><Assbuyermobile>15821529639</Assbuyermobile><Fee>0</Fee><Commonreturn>公共回传字段</Commonreturn><RetURL>http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/huaan.htm</RetURL><Memo>此订单为测试订单</Memo></order>";

            return pEnity;

        }

        #endregion



        /// <summary>
        /// 获取华安接口中：每万份收益、年化收益率（5002）
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private void GetProfit()
        {
            DateTime dt = DateTime.Now;

            string seqNO = HuaAnFactory.GenerateSeqNO();

            //处理调用华安请求From表单对象
            //ReceiveProfitSelectMessage entity = AssignmentProfit(dt);  //查询年化收益率
            // ReceivePaySingleQueryMessage entity = AssignmentSingleQuery(dt);  //3001
            ReceiveFundProfitMessage entity = Assignment5000(dt);   //500
            string strContent = Utility.GetRequsetXml(entity);
            HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, 5001, seqNO);

            IDictionary<string, string> dic = HuaAnFactory.SetFormPara(rMessage);
            HttpWebResponse webResponse = HttpHelper.CreatePostHttpResponse(HuaAnConfigurationAppSitting.ReservationServletUrl, dic, null, null, Encoding.UTF8, null);
            string content = HttpHelper.GetResponseString(webResponse);

            Hashtable htComm = GetCommonreturn(content);
            HanAnMessage msg = new HanAnMessage
                                {
                                    verNum = htComm["verNum"].ToString(),
                                    sysdate = htComm["sysdate"].ToString(),
                                    systime = htComm["systime"].ToString(),
                                    txcode = htComm["txcode"].ToString(),
                                    seqNO = htComm["seqNO"].ToString(),
                                    maccode = htComm["maccode"].ToString(),
                                    content = htComm["content"].ToString()
                                };


            if (msg != null)
            {
                string decrypt = Utility.AESDecrypt(msg.content, HuaAnConfigurationAppSitting.AesKey);
                SendFundProfitMessage profitMsg = GetProfitMsg(decrypt);
                string[] profitN = profitMsg.Content.Split('\n');
                foreach (var item in profitN)
                {
                    string profit = item;
                    string[] profitArray = profit.Split('|');

                }
                //   string[] profit = profitMsg.Content.Split('|');



            }

            Model = rMessage;
        }

        /// <summary>
        /// 解析Xml返回对象（支付回调）
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static SendFundProfitMessage GetProfitMsg(string pXmlString)
        {
            SendFundProfitMessage send = new SendFundProfitMessage();
            //Xml解析
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pXmlString);
            //取得节点名为order的集合
            XmlNodeList xxList = doc.GetElementsByTagName("order");
            //xxNode是每一个<CL>...</CL>体
            foreach (XmlNode xxNode in xxList)
            {
                //取得CL下的子节点集合
                XmlNodeList childList = xxNode.ChildNodes;
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "MerchantID":
                            send.MerchantID = node.InnerText;
                            break;
                        case "Allpageno":
                            send.Allpageno = node.InnerText;
                            break;
                        case "Curpageno":
                            send.Curpageno = node.InnerText;
                            break;
                        case "Allcount":
                            send.Allcount = node.InnerText;
                            break;
                        case "Content":
                            send.Content = node.InnerText;
                            break;
                        case "Retcode":
                            send.Retcode = node.InnerText;
                            break;
                        case "Retmsg":
                            send.Retmsg = node.InnerText;
                            break;
                    }
                }
            }
            return send;
        }

        public Hashtable GetCommonreturn(string content)
        {
            string[] form = content.Split('&');
            //创建一个Hashtable实例
            Hashtable ht = new Hashtable();
            string key = string.Empty, value = string.Empty;
            foreach (var item in form)
            {
                key = item.Split('=')[0];//UserID
                //value = item.Split('=')[1];//123

                value = item.Substring(key.Length + 1);
                //添加key value键值对
                ht.Add(key, value);
            }

            return ht;
        }


        /// <summary>
        /// 资产及收益  5000
        /// </summary>
        public class Receive5001Message
        {
            /// <summary>
            /// 商家ID
            /// </summary>
            public string MerchantID { get; set; }

            /// <summary>
            /// 商户日期
            /// </summary>
            public string Merchantdate { get; set; }

            /// <summary>
            /// 当前请求页码（默认填1）
            /// </summary>
            public string Pageno { get; set; }

            /// <summary>
            /// 返回地址
            /// </summary>
            public string RetURL { get; set; }

            /// <summary>
            /// 定单描述
            /// </summary>
            public string Memo { get; set; }

        }

        /// <summary>
        /// 5002
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceiveProfitSelectMessage AssignmentProfit(DateTime dt)
        {
            ReceiveProfitSelectMessage pEnity = new ReceiveProfitSelectMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Pageno = "1";
            pEnity.RetURL = "http://baidu.com";
            pEnity.Memo = "ceshi";

            return pEnity;
        }

        /// <summary>
        /// 3001
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceivePaySingleQueryMessage AssignmentSingleQuery(DateTime dt)
        {
            ReceivePaySingleQueryMessage pEnity = new ReceivePaySingleQueryMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Orgmerchantdate = "20140608";
            pEnity.OrgorderNO = "20140608161759034157924";
            pEnity.Orgtxcode = "2101";
            pEnity.Commonreturn = "";
            pEnity.RetURL = "http://baidu.com";
            pEnity.Memo = "ceshi";
            pEnity.Assignbuyer = "Vip00003691";

            return pEnity;
        }

        /// <summary>
        /// 5000
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceiveFundProfitMessage Assignment5000(DateTime dt)
        {
            ReceiveFundProfitMessage pEnity = new ReceiveFundProfitMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Pageno = "1";
            pEnity.RetURL = "";
            pEnity.Memo = "ceshi";

            return pEnity;
        }

        /// <summary>
        /// 5001
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ReceiveFundProfitMessage Assignment5001(DateTime dt)
        {
            ReceiveFundProfitMessage pEnity = new ReceiveFundProfitMessage();
            pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
            pEnity.Merchantdate = dt.ToString("yyyyMMdd");
            pEnity.Pageno = "1";
            pEnity.RetURL = "";
            pEnity.Memo = "ceshi";

            return pEnity;
        }

    }
}