using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.Lj.Interface
{
    public partial class PrizesData : System.Web.UI.Page
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);
                switch (dataType)
                {
                    case "getEventPrizes":  //获取活动奖项信息
                        content = getEventPrizes();
                        break;
                    case "setEventPrizes":  //刮奖
                        content = setEventPrizes();
                        break;
                    case "bindRecommender":  //绑定推荐人
                        content = BindRecommender();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region getEventPrizes 获取活动奖项信息
        /// <summary>
        /// 获取活动奖项信息
        /// </summary>
        public string getEventPrizes()
        {
            string content = string.Empty;

            var respData = new getEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventPrizesReqData>();

                string openId = reqObj.common.openId;
                string weixinId = reqObj.common.weiXinId ?? reqObj.common.openId;
                string eventId = reqObj.special.eventId;    //活动ID
                string vipId = reqObj.common.userId;
                string vipName = string.Empty;
                string longitude = reqObj.special.longitude;   //经度
                string latitude = reqObj.special.latitude;     //纬度

                if (string.IsNullOrEmpty(eventId))
                {
                    eventId = "E5A304D716D14CD2B96560EBD2B6A29C";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventPrizes: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new getEventPrizesRespContentData();
                respData.content.prizeList = new List<PrizesEntity>();

                var eventList = new LEventsBLL(loggingSessionInfo).QueryByEntity(new LEventsEntity { EventID = eventId }, null);

                if (eventList != null && eventList.Length > 0)
                {
                    var eventEntity = eventList.FirstOrDefault();

                    if (Convert.ToDateTime(eventEntity.EndTime).AddDays(1) > DateTime.Now)  //当天还是有效的   updated by Willie Yan on 2014-04-28
                    {
                        #region 获取VIPID

                        VipBLL vipService = new VipBLL(loggingSessionInfo);
                        var vipList = vipService.QueryByEntity(new VipEntity() { WeiXinUserId = openId }, null);

                        if (vipList == null || vipList.Length == 0)
                        {
                            respData.code = "103";
                            respData.description = "未查找到匹配的VIP信息";
                            return respData.ToJSON();
                        }
                        else
                        {
                            vipId = vipList.FirstOrDefault().VIPID;
                            vipName = vipList.FirstOrDefault().VipName;
                        }

                        #endregion

                        //查询抽奖日志
                        LLotteryLogBLL lotteryService = new LLotteryLogBLL(loggingSessionInfo);
                        var lotteryList = lotteryService.QueryByEntity(new LLotteryLogEntity() { EventId = eventId, VipId = vipId }, null);

                        #region 奖品信息

                        LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);

                        var prizesList = prizesService.QueryByEntity(new LPrizesEntity() { EventId = eventId },
                            new OrderBy[] { new OrderBy { FieldName = " DisplayIndex ", Direction = OrderByDirections.Asc } });

                        if (prizesList != null && prizesList.Length > 0)
                        {
                            foreach (var item in prizesList)
                            {
                                var entity = new PrizesEntity()
                                {
                                    prizesID = item.PrizesID,
                                    prizeName = item.PrizeName,
                                    prizeDesc = item.PrizeDesc,
                                    displayIndex = item.DisplayIndex.ToString(),
                                    countTotal = item.CountTotal.ToString(),
                                    imageUrl = item.ImageUrl
                                };

                                respData.content.prizeList.Add(entity);
                            }
                        }

                        #endregion

                        #region 抽奖信息

                        //respData.content.lotteryCount = eventEntity.PrizesCount.ToString();
                        respData.content.lotteryNumber = "0";
                        respData.content.validTime = ConfigurationManager.AppSettings["ValidTime"];
                        int totalLotteryCount = 0;

                        //获取剩余抽奖次数
                        VwVipCenterInfoBLL vwVipCenterInfoBLL = new VwVipCenterInfoBLL(loggingSessionInfo);
                        var vwVipCenterInfo = vwVipCenterInfoBLL.GetByID(vipId);
                        totalLotteryCount = vwVipCenterInfo.LotteryCount ?? 0;

                        //获取已抽奖次数
                        if (lotteryList != null && lotteryList.Length > 0)
                        {
                            respData.content.lotteryNumber = lotteryList.FirstOrDefault().LotteryCount.ToString();
                        }
                        else
                            totalLotteryCount++; //如果未抽奖，活动默认有一次抽奖机会

                        //判断抽奖次数是否有效
                        if (Convert.ToInt32(respData.content.lotteryNumber) >= totalLotteryCount)
                        {
                            respData.content.isLottery = "0";
                            respData.content.lotteryDesc = "您已经没有抽奖机会了，想得到更多抽奖机会，请在对话栏内发送中文“分享”给我们，获得图文消息后多多转发。详情关注“推荐有礼”菜单。";
                        }
                        else
                        {
                            //判断之前是否已经中奖
                            LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(loggingSessionInfo);
                            var prize = winnerService.GetWinnerInfo(vipId, eventId);

                            if (!prize.Read())
                            {
                                //抽奖
                                LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
                                var returnDataObj = poolsServer.SetShakeOffLottery(vipName, vipId, eventId, ToFloat(longitude), ToFloat(latitude));

                                if (returnDataObj.Params.result_code.Equals("1"))   //中奖
                                {
                                    //获取奖品信息
                                    prize = winnerService.GetWinnerInfo(vipId, eventId);
                                    if (prize.Read())
                                    {
                                        var prizeValue = GetPrizeValue(prize["PrizeShortDesc"].ToString());

                                        respData.content.isLottery = "1";
                                        respData.content.lotteryDesc = returnDataObj.Params.result_message;// "恭喜您中奖了";
                                        respData.content.isWinning = "1";
                                        respData.content.winningValue = prizeValue;
                                    }
                                    //added by zhangwei 中奖后绑定推荐关系，设置奖项
                                    BindRecommender();
                                    setEventPrizes();
                                }
                                else    //没有中奖，一直能抽奖
                                {
                                    respData.content.isLottery = "1";
                                    respData.content.isWinning = "0";
                                    respData.content.winningValue = "0";
                                    respData.content.lotteryDesc = "恭喜您中奖了";//张伟，为泸州老窖新人有礼前台判断使用
                                }
                            }
                            else
                            {
                                var prizeValue = GetPrizeValue(prize["PrizeShortDesc"].ToString());

                                respData.content.isLottery = "1";
                                respData.content.lotteryDesc = "恭喜您中奖了";
                                respData.content.isWinning = "1";
                                respData.content.winningValue = prizeValue;
                            }
                            prize.Close();
                            respData.content.lotteryCount = totalLotteryCount.ToString();

                        }

                        #endregion
                    }
                    else
                    {
                        respData.content.isLottery = "0";
                        respData.content.lotteryDesc = "活动已经结束";
                    }
                }
                else
                {
                    respData.content.isLottery = "0";
                    respData.content.lotteryDesc = "指定的活动不存在";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        private string GetPrizeValue(string prizeName)
        {
            var prizeValue = "0";

            if (prizeName == "一等奖")
            {
                prizeValue = "1";
            }
            else if (prizeName == "二等奖")
            {
                prizeValue = "2";
            }
            else if (prizeName == "三等奖")
            {
                prizeValue = "3";
            }
            else if (prizeName == "四等奖")
            {
                prizeValue = "4";
            }
            return prizeValue;
        }

        public class getEventPrizesRespData : Default.LowerRespData
        {
            public getEventPrizesRespContentData content;
        }
        public class getEventPrizesRespContentData
        {
            public string isLottery;        //是否抽奖  1= 是，0=否
            public string isWinning;        //是否中奖  1= 是，0=否
            public string validTime;        //有效时间（单位： 秒）
            public string winningValue;     //中奖奖品值 0,1,2,3 (0表示未中奖)
            public string lotteryDesc;      //抽奖描述（您已经没有抽奖机会了）
            public string lotteryCount;     //可以抽奖次数
            public string lotteryNumber;    //第几轮（初始为0）
            public IList<PrizesEntity> prizeList;   //奖项集合
            public IList<WinnerEntity> winnerList;  //中奖名单
        }
        public class PrizesEntity
        {
            public string prizesID;     //奖品标识
            public string prizeName;    //奖品名称
            public string prizeDesc;    //奖品描述
            public string displayIndex; //排序
            public string countTotal;   //奖品数量
            public string imageUrl;     //图片
        }
        public class WinnerEntity
        {
            public string userName;     //中奖人姓名
            public string phone;        //手机号
        }
        public class getEventPrizesReqData : Default.ReqData
        {
            public getEventPrizesReqSpecialData special;
        }
        public class getEventPrizesReqSpecialData
        {
            public string eventId;      //活动标识
            public string longitude;    //经度
            public string latitude;     //纬度
        }

        #endregion

        #region setEventPrizes 刮奖
        /// <summary>
        /// 刮奖
        /// </summary>
        public string setEventPrizes()
        {
            string content = string.Empty;
            var respData = new setEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setEventPrizesReqData>();

                string vipId = reqObj.common.userId;
                string openId = reqObj.common.openId;
                string weixinId = reqObj.common.weiXinId;
                string eventId = reqObj.special.eventId;    //活动标识

                if (string.IsNullOrEmpty(eventId))
                {
                    eventId = "E5A304D716D14CD2B96560EBD2B6A29C";
                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventPrizes: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                vipId = GetVipIDByOpenID(loggingSessionInfo, openId);

                if (vipId == "")
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }

                CouponBLL couponService = new CouponBLL(loggingSessionInfo);
                couponService.SetEventPrizes(vipId, eventId);

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventPrizesRespData : Default.LowerRespData
        {
            //public setEventPrizesRespContentData content;
        }
        public class setEventPrizesRespContentData
        {

        }
        public class setEventPrizesReqData : Default.ReqData
        {
            public setEventPrizesReqSpecialData special;
        }
        public class setEventPrizesReqSpecialData
        {
            public string eventId;      //活动标识
        }

        #endregion

        #region 关联领奖人和推荐人
        /// <summary>
        /// 关联领奖人和推荐人
        /// </summary>
        /// <returns></returns>
        public string BindRecommender()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();

            string reqContent = Request["ReqContent"];

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("getItemList: {0}", reqContent)
            });

            try
            {
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<BindRecommenderReqData>();
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var vipBLL = new VipBLL(loggingSessionInfo);
                var vip = vipBLL.GetVipDetailByVipID(reqObj.common.userId);

                //更新推荐人openId
                if (reqObj.special.Recommender != null && reqObj.special.Recommender != "")
                {
                    string vipID = GetVipIDByOpenID(loggingSessionInfo, reqObj.special.Recommender);
                    if ((vip.HigherVipID == null || vip.HigherVipID == "") && reqObj.common.userId != vipID)
                    {
                        vip.HigherVipID = vipID;
                        vipBLL.Update(vip);
                        //查看推荐人成功推荐人数，满足条件给奖励
                        CouponBLL couponService = new CouponBLL(loggingSessionInfo);
                        //TODO:added by zhangwei20141009，保存上下线记录
                        couponService.UpdateVipRecommandTrace(reqObj.common.userId, vipID);
                        couponService.RecommenderPrize(vipID,null);
                    }


                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.Message;
            }

            content = respData.ToJSON();
            return content;
        }

        public class BindRecommenderReqData : Default.ReqData
        {
            public BindRecommenderReqSpecialData special;
        }
        public class BindRecommenderReqSpecialData
        {
            public string Recommender { get; set; }	//推荐人openId
        }

        #endregion

        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }

        private string GetVipIDByOpenID(LoggingSessionInfo loggingSessionInfo, string openId)
        {
            #region 获取VIPID

            string VipID = "";

            VipBLL vipService = new VipBLL(loggingSessionInfo);
            var vipList = vipService.QueryByEntity(new VipEntity() { WeiXinUserId = openId }, null);

            if (vipList != null && vipList.Length > 0)
            {
                VipID = vipList.FirstOrDefault().VIPID;
            }

            return VipID;
            #endregion
        }
    }
}