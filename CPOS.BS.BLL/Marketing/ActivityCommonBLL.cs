using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.Common;
using System.Text.RegularExpressions;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL.Marketing
{
    /// <summary>
    /// 营销活动服务送券业务，生成活动消息业务,发送消息业务
    /// </summary>
    public class ActivityCommonBLL
    {
        private static LoggingSessionInfo c_loggingSessionInfo;
        private volatile static ActivityCommonBLL _instance = null;
        private static readonly object lockHelper = new object();//实例化锁
        private ActivityCommonBLL(LoggingSessionInfo p_loggingSessionInfo)
        {
            c_loggingSessionInfo = p_loggingSessionInfo;
        }
        public static ActivityCommonBLL CreateInstance(LoggingSessionInfo p_loggingSessionInfo)
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ActivityCommonBLL(p_loggingSessionInfo);
                }
            }
            return _instance;
        }

        #region Out Method
        /// <summary>
        /// 营销活动业务
        /// </summary>
        public void MarketingActivityMethod()
        {
            //获取所有商户
            var CustomerIDList = GetALLCustomerInfo_List();
            //轮循商户，更新loggingSessionInfo，执行活动服务送券业务方法
            foreach (var item in CustomerIDList)
            {
                //更新商户
                c_loggingSessionInfo.ClientID = item.Key;
                c_loggingSessionInfo.CurrentLoggingManager.Connection_String = item.Value;
                //执行活动送券业务方法
                ActivitySendCoupon();
            }

        }
        /// <summary>
        /// 发送营销活动消息（短信，微信，邮件（暂无））
        /// </summary>
        public void MarketingActivitySendMessigeMethod()
        {
            //获取所有商户
            var CustomerIDList = GetALLCustomerInfo_List();
            //轮循商户，更新loggingSessionInfo，执行活动服务送券业务方法
            foreach (var item in CustomerIDList)
            {
                //更新商户
                c_loggingSessionInfo.ClientID = item.Key;
                c_loggingSessionInfo.CurrentLoggingManager.Connection_String = item.Value;
                //执行活动发消息业务方法
                ActivitySendMessige();
            }
        }
        #endregion
        #region Inner Method
        /// <summary>
        /// 获取所有商户ID
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetALLCustomerInfo_List()
        {
            var Result = new Dictionary<string, string>();
            string sql = @"select a.customer_id as CustomerID,'user id='+b.db_user+';password='+b.db_pwd+';data source='+b.db_server+';database='+b.db_name+';' as conn 
                        from  cpos_ap..t_customer as a left join cpos_ap..t_customer_connect as b on a.customer_id=b.customer_id 
						where a.customer_status=1";
            var ActivityDAO = new C_ActivityDAO(c_loggingSessionInfo);
            DataSet ds = ActivityDAO.GetALLCustomerInfo(sql);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Result.Add(ds.Tables[0].Rows[i]["CustomerID"].ToString(), ds.Tables[0].Rows[i]["conn"].ToString());
                }
            }
            return Result;
        }
        /// <summary>
        /// 发送消息业务（短信，微信）
        /// </summary>
        private void ActivitySendMessige()
        {
            #region Business Object
            var ActivityBLL = new C_ActivityBLL(c_loggingSessionInfo);
            var MessageSendBLL = new C_MessageSendBLL(c_loggingSessionInfo);
            var ActivityMessageBLL = new C_ActivityMessageBLL(c_loggingSessionInfo);
            var CommonBLL = new CommonBLL();
            #endregion

            #region 获取所有营销活动
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = c_loggingSessionInfo.ClientID });
            //complexCondition.Add(new DirectCondition(" ((IsLongTime=1 and GETDATE()>=StartTime) OR (GETDATE()>=StartTime AND GETDATE()<=EndTime)) AND STATUS=0 and IsDelete=0 "));
            complexCondition.Add(new DirectCondition(" (IsLongTime=1 or (IsLongTime=0 and GETDATE()<=EndTime)) and STATUS=0 and IsDelete=0 "));
            var ActivityList = ActivityBLL.Query(complexCondition.ToArray(), null);//营销活动
            #endregion

            //轮循活动获取到当前活动与发送短信，微信消息集合
            foreach (var item in ActivityList)
            {
                //预发送消息源
                var ActivityMessageList = ActivityMessageBLL.QueryByEntity(new C_ActivityMessageEntity() { ActivityID = item.ActivityID }, null).ToList();
                var ActivitySMSMessageList = new List<C_MessageSendEntity>();//短信消息集合
                var ActivityWeChatMessageList = new List<C_MessageSendEntity>();//微信消息集合
                foreach (var itemes in ActivityMessageList)
                {
                    if (itemes.MessageType.Trim().Equals("SMS"))
                        ActivitySMSMessageList = MessageSendBLL.QueryByEntity(new C_MessageSendEntity() { MessageID = itemes.MessageID, IsSend = 0 }, null).ToList();
                    if (itemes.MessageType.Trim().Equals("WeChat"))
                        ActivityWeChatMessageList = MessageSendBLL.QueryByEntity(new C_MessageSendEntity() { MessageID = itemes.MessageID, IsSend = 0 }, null).ToList();
                }



                //遍历消息集合，调用发送消息方法
                #region 发送短信
                string content = "";
                string mobile = "";
                foreach (var SMSMessage in ActivitySMSMessageList)
                {

                    List<IWhereCondition> MessageCondition = new List<IWhereCondition> { };
                    //获取未发送的信息
                    MessageCondition.Add(new DirectCondition("MessageID='" + SMSMessage.MessageID + "' and IsSend <>2 order by Priority desc "));
                    var MessageSendList = MessageSendBLL.Query(MessageCondition.ToArray(), null).ToList().Take(5000).ToList();
                    StringBuilder PhoneStr = new StringBuilder();
                    //群发短信接口对象
                    api api1 = new api();
                    if (MessageSendList.Count > 100)
                    {
                        #region 大于一百条
                        foreach (var MessageSend in MessageSendList)
                        {
                            if (MessageSend.SendTime <= DateTime.Now)
                            {
                                PhoneStr.Append(MessageSend.Phone + ",");//手机号
                            }
                            if (string.IsNullOrWhiteSpace(content))
                                content = "尊敬的会员," + SMSMessage.Content + "回TD退订【连锁掌柜】";//消息内容
                            mobile = PhoneStr.ToString();//手机号
                        }
                        mobile = mobile.TrimEnd(',');

                        //调用群发发送短信接口
                        SendSmsResult SendSmsResult1 = api1.SendSms("hy_znxx", "znxx", content, mobile, "", "17");
                        //事物对象
                        var pTran = ActivityBLL.GetTran();
                        using (pTran.Connection)
                        {
                            try
                            {
                                if (SendSmsResult1.code == 2)
                                {//发送成功


                                    foreach (var updateItem in MessageSendList)
                                    {
                                        updateItem.ActualSendTime = DateTime.Now;
                                        updateItem.IsSend = 2;
                                        updateItem.SendNumber++;
                                        MessageSendBLL.Update(updateItem, pTran);
                                    }

                                }
                                else
                                {//失败
                                    foreach (var updateItem in MessageSendList)
                                    {
                                        updateItem.SendNumber++;
                                        updateItem.Priority = 1;
                                        MessageSendBLL.Update(updateItem, pTran);
                                    }
                                }
                                //提交
                                pTran.Commit();
                                System.Threading.Thread.Sleep(2000);   // 1秒为单位
                            }
                            catch (Exception ex)
                            {
                                pTran.Rollback();

                                throw ex;
                            }
                        }
                        #endregion


                    }
                    //else
                    //{
                    #region 单发
                    //    foreach (var MessageSend in MessageSendList)
                    //    {
                    //        if (MessageSend.SendTime < DateTime.Now)
                    //        {
                    //            //移动手机号不能单发
                    //            string yidong = @"^1(34[0-8]|(3[5-9]|5[017-9]|8[278])\\d)\\d{7}$";
                    //            Regex YD = new Regex(yidong);
                    //            if (YD.IsMatch(MessageSend.Phone))
                    //                continue;

                    //            string NewContent = "尊敬的会员," + MessageSend.Content + "回TD退订【连锁掌柜】";
                    //            //调用群发发送短信接口
                    //            SendSmsResult SendSmsResult2 = api1.SendSms("hy_znxx", "znxx", NewContent, MessageSend.Phone, "", "17");
                    //            if (SendSmsResult2.code == 2)
                    //            {//发送成功
                    //                MessageSend.ActualSendTime = DateTime.Now;
                    //                MessageSend.IsSend = 2;
                    //                MessageSend.SendNumber++;
                    //                MessageSendBLL.Update(MessageSend);
                    //            }
                    //            else
                    //            {//失败
                    //                MessageSend.SendNumber++;
                    //                MessageSend.Priority = 1;
                    //                MessageSendBLL.Update(MessageSend);
                    //            }
                    //        }
                    //    }
                    #endregion

                    //}

                }

                #endregion
                #region 发送微信
                foreach (var Weitem in ActivityWeChatMessageList)
                {
                    List<IWhereCondition> WeMessageCondition = new List<IWhereCondition> { };
                    //获取预发送的微信
                    WeMessageCondition.Add(new DirectCondition("MessageID='" + Weitem.MessageID + "' and IsSend =0 order by Priority desc "));
                    var WeMessageSendList = MessageSendBLL.Query(WeMessageCondition.ToArray(), null).ToList().Take(10000);
                    List<string> OpenIDList = new List<string>();
                    string WXcontent = "";
                    foreach (var WeMessageSend in WeMessageSendList)
                    {
                        if (WeMessageSend.SendTime <= DateTime.Now)
                            OpenIDList.Add(WeMessageSend.OpenID);
                        if (string.IsNullOrWhiteSpace(WXcontent))
                            WXcontent = WeMessageSend.Content;
                    }
                    if (OpenIDList.Count > 1)
                    {
                        //调用
                        string Json = CommonBLL.BulkSendWXTemplateMessage(OpenIDList.ToArray(), WXcontent, c_loggingSessionInfo);
                        if (!string.IsNullOrWhiteSpace(Json))
                        {

                            var Data = JsonHelper.JsonDeserialize<Result>(Json);
                            //事物对象
                            var pTranWX = ActivityBLL.GetTran();
                            using (pTranWX.Connection)
                            {
                                try
                                {


                                    if (Data.errcode == 0)
                                    {
                                        foreach (var WXUpdateitem in WeMessageSendList)
                                        {
                                            WXUpdateitem.ActualSendTime = DateTime.Now;
                                            WXUpdateitem.IsSend = 2;
                                            WXUpdateitem.SendNumber++;
                                            MessageSendBLL.Update(WXUpdateitem, pTranWX);
                                        }
                                    }
                                    else
                                    {//失败
                                        foreach (var WXUpdateitem in WeMessageSendList)
                                        {
                                            WXUpdateitem.SendNumber++;
                                            WXUpdateitem.Priority = 1;
                                            WXUpdateitem.IsSend = 3;
                                            MessageSendBLL.Update(WXUpdateitem, pTranWX);
                                        }
                                    }
                                    //提交
                                    pTranWX.Commit();
                                    System.Threading.Thread.Sleep(2000);   // 5秒为单位
                                }
                                catch (Exception ex)
                                {
                                    pTranWX.Rollback();
                                    throw ex;
                                }
                            }

                        }
                    }
                }
                #endregion
                #region 发送邮件

                #endregion
            }
        }

        /// <summary>
        /// 活动服务送券业务方法
        /// </summary>
        private void ActivitySendCoupon()
        {

            #region 业务对象
            var activityBLL = new C_ActivityBLL(c_loggingSessionInfo);
            var targetGroupBLL = new C_TargetGroupBLL(c_loggingSessionInfo);
            var prizesBLL = new C_PrizesBLL(c_loggingSessionInfo);
            var prizesDetailBLL = new C_PrizesDetailBLL(c_loggingSessionInfo);
            var CouponTypeBLL = new CouponTypeBLL(c_loggingSessionInfo);
            var ActivityMessageBLL = new C_ActivityMessageBLL(c_loggingSessionInfo);
            #endregion


            #region 获取当前商户活动集合
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = c_loggingSessionInfo.ClientID });
            //complexCondition.Add(new DirectCondition(" ((IsLongTime=1 and GETDATE()>=StartTime) OR (GETDATE()>=StartTime AND GETDATE()<=EndTime)) AND STATUS=0 and IsDelete=0 "));
            complexCondition.Add(new DirectCondition(" (IsLongTime=1 or (IsLongTime=0 and GETDATE()<=EndTime)) and STATUS=0 and IsDelete=0 "));
            var ActivityList = activityBLL.Query(complexCondition.ToArray(), null);//营销活动   

            #endregion
            #region 轮循活动，送券业务处理
            foreach (var activity in ActivityList)
            {
                Loggers.Debug(new DebugLogInfo() { Message = "轮循营销活动" });
                #region 获取卡类型ID（目标卡人群）
                var targetGroupInfo = targetGroupBLL.QueryByEntity(new C_TargetGroupEntity() { ActivityID = activity.ActivityID, GroupType = 1 }, null).FirstOrDefault();
                int VipCardTypeID = targetGroupInfo == null ? -1 : int.Parse(targetGroupInfo.ObjectID);//卡类型ID
                #endregion



                #region 活动是否设置奖品控制
                //获取赠送礼券奖品
                var prizesData = prizesBLL.QueryByEntity(new C_PrizesEntity() { ActivityID = activity.ActivityID, PrizesType = 1 }, null).FirstOrDefault();
                //当前活动消息类型集合
                var ActivityMessageLsit = ActivityMessageBLL.QueryByEntity(new C_ActivityMessageEntity() { ActivityID = activity.ActivityID }, null).ToList();

                List<IWhereCondition> PrizeDetailCondition = new List<IWhereCondition> { };
                if (prizesData != null)
                {
                    PrizeDetailCondition.Add(new DirectCondition(" PrizesID='" + prizesData.PrizesID + "' "));
                }
                var PrizeDetailList = prizesDetailBLL.Query(PrizeDetailCondition.ToArray(), null).ToList();
                #endregion

                #region 当前活动券处理

                #region 获取活动会员人数
                int SumVipCount = 0;//会员数
                if (activity.IsAllVipCardType == 0)
                    SumVipCount = activityBLL.GetholderCardCount(VipCardTypeID.ToString(), null);//当前卡类型会员数
                else
                    SumVipCount = activityBLL.GetholderCardCount(null, null);//所有会员数
                #endregion

                #region 获取当前活动送券集合
                List<IWhereCondition> CouponTypeCondition = new List<IWhereCondition> { };
                StringBuilder strWhere = new StringBuilder();
                string NewStrWhere = "";
                foreach (var itemPrizeDetail in PrizeDetailList)
                {
                    strWhere.Append("couponTypeID='" + itemPrizeDetail.CouponTypeID + "' or ");
                }
                if (PrizeDetailList.Count > 0)
                    NewStrWhere = ((strWhere.ToString().TrimEnd()).TrimEnd('r')).Trim('o');
                if (!string.IsNullOrWhiteSpace(NewStrWhere))
                    CouponTypeCondition.Add(new DirectCondition(NewStrWhere));
                //券集合
                var ActivityCouponTypeList = CouponTypeBLL.Query(CouponTypeCondition.ToArray(), null);
                #endregion

                #region 判断当前券数量是否充足，不充足：计算出缺省券数量，充足获取券数最少的一种券

                //券种数量集合
                List<int> CouponNumList = new List<int>();
                bool CouponFlag = true;
                string Remark = "";
                foreach (var CouponTypeitem in ActivityCouponTypeList)
                {
                    if (CouponTypeitem.IsVoucher >= CouponTypeitem.IssuedQty)
                    {
                        if (SumVipCount > activity.SendCouponQty)
                        {
                            int Math = SumVipCount - (activity.SendCouponQty ?? 0);//目标数-使用数
                            // Loggers.Debug(new DebugLogInfo() { Message = CouponTypeitem.CouponTypeName + "券数量库存不足！" + DateTime.Now });
                            Remark += string.Format("{1}都已发完还少{0}张，", Math, CouponTypeitem.CouponTypeName);
                            CouponFlag = false;
                        }
                    }
                    else
                    {
                        CouponNumList.Add((CouponTypeitem.IssuedQty ?? 0) - (CouponTypeitem.IsVoucher ?? 0));
                    }
                }
                if (CouponFlag == false)
                {
                    Remark += "请追加券数量！";
                    //activity.Remark = Remark;
                    activityBLL.Update(activity);//更新当前活动备注
                    continue;
                }
                int MinCouponNum = CouponNumList.Min();//当前活动奖品券种数量最少的券数
                if (MinCouponNum == 0)
                {
                    continue;
                }
                #endregion


                #endregion

                #region 批量新增券关系,修改券数量
                int ResultConunt = 0;
                string CouponTypeNameStr = "";//所有券名称
                foreach (var CouponTypeitem in ActivityCouponTypeList)
                {
                    CouponTypeNameStr += CouponTypeitem.CouponTypeName + ",";
                    ResultConunt = BatchAddSendCoupon(MinCouponNum, activity.ActivityID.Value.ToString(), activity.ActivityType.Value, CouponTypeitem.CouponTypeID.Value.ToString(), VipCardTypeID);
                    System.Threading.Thread.Sleep(2000);//沉睡2秒

                }
                #endregion

                if (ResultConunt > 0)
                {

                    #region 修改活动领券人数
                    activity.SendCouponQty = activity.SendCouponQty+ResultConunt;
                    activityBLL.Update(activity);

                    #endregion

                    #region 批量新增活动获赠奖品预发送消息
                    //CouponTypeNameStr = CouponTypeNameStr.TrimEnd(',');
                    foreach (var ActMessageitem in ActivityMessageLsit)
                    {
                        string Content = ActMessageitem.Content + "," + CouponTypeNameStr;//消息内容
                        BatchAddMessageSend(ResultConunt, activity.ActivityID.Value.ToString(), VipCardTypeID, ActMessageitem.MessageID.Value.ToString(), ActMessageitem.MessageType, Content, ActMessageitem.SendTime.Value, activity.ActivityType ?? 2);
                        System.Threading.Thread.Sleep(2000);//沉睡2秒
                    }
                    #endregion

                    #region 批量新增获赠信息
                    BatchAddPrizeReceive(ResultConunt, activity.ActivityID.Value.ToString(), VipCardTypeID, activity.ActivityType ?? 2);
                    System.Threading.Thread.Sleep(2000);//沉睡2秒
                    #endregion

                }
            }

            #endregion

        }
        /// <summary>
        /// 批量送券业务
        /// </summary>
        /// <param name="MinCouponNum"></param>
        /// <param name="ActivityID"></param>
        /// <param name="ActivityType"></param>
        /// <param name="CouponTypeID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <returns></returns>
        private int BatchAddSendCoupon(int MinCouponNum, string ActivityID, int ActivityType, string CouponTypeID, int VipCardTypeID)
        {
            int ResultCount = 0;
            var ActivityDAO = new C_ActivityDAO(c_loggingSessionInfo);
            ResultCount = ActivityDAO.BatchAddSendCoupon(MinCouponNum, ActivityID, ActivityType, CouponTypeID, VipCardTypeID);
            return ResultCount;
        }
        /// <summary>
        /// 批量新增预发送消息
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="MessageID"></param>
        /// <param name="MessageType"></param>
        /// <param name="Content"></param>
        /// <param name="SendTime"></param>
        /// <param name="StrCouponName"></param>
        /// <param name="ActivitType"></param>
        private void BatchAddMessageSend(int ResultConunt, string ActivityID, int VipCardTypeID, string MessageID, string MessageType, string Content, DateTime SendTime, int ActivitType)
        {
            var ActivityDAO = new C_ActivityDAO(c_loggingSessionInfo);
            ActivityDAO.BatchAddMessageSend(ResultConunt, ActivityID, VipCardTypeID, MessageID, MessageType, Content, SendTime, ActivitType);
        }
        /// <summary>
        /// 批量新增获赠消息
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="ActivitType"></param>
        private void BatchAddPrizeReceive(int ResultConunt, string ActivityID, int VipCardTypeID, int ActivitType)
        {
            var ActivityDAO = new C_ActivityDAO(c_loggingSessionInfo);
            ActivityDAO.BatchAddPrizeReceive(ResultConunt, ActivityID, VipCardTypeID, ActivitType);
        }

        #endregion
    }


    public class Result
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string msg_id { get; set; }
    }
}
