using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Request;
using JIT.CPOS.DTO.Module.Event.ContactEvent.Response;

using System.Data;
using RedisOpenAPIClient.Models.CC;
using JIT.CPOS.Common;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.ContactEvent
{
    public class SetContactEventAH : BaseActionHandler<SetContactEventRP, SetContactEventRD>
    {
        protected override SetContactEventRD ProcessRequest(DTO.Base.APIRequest<SetContactEventRP> pRequest)
        {
            var rd = new SetContactEventRD();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bllContactEvent = new ContactEventBLL(loggingSessionInfo);
            var bllEvent = new LEventsBLL(loggingSessionInfo);
            var bllPrizes = new LPrizesBLL(loggingSessionInfo);
            string strErrMsg = string.Empty;
            try
            {


                string[] CouponTypeIdList = para.CouponTypeID;

                if (para.ContactEventId != null && para.ContactEventId != "")
                {
                    var contactEvent = bllContactEvent.GetByID(para.ContactEventId);
                    //if (contactEvent.Status == 2 || contactEvent.Status==3)//状态为运行时只可追加数量
                    if (para.Method == "Append")
                    {
                        contactEvent.PrizeCount = (contactEvent.PrizeCount == null ? 0 : contactEvent.PrizeCount) + para.PrizeCount;
                        LPrizesBLL bllPrize = new LPrizesBLL(loggingSessionInfo);
                        var entityPrize = bllPrize.QueryByEntity(new LPrizesEntity() { EventId = para.ContactEventId, IsDelete = 0 }, null).FirstOrDefault();
                        var CouponTypeTemp = bllContactEvent.QueryByEntity(new ContactEventEntity() { ContactEventId =new Guid(para.ContactEventId), IsDelete = 0 },null).SingleOrDefault().CouponTypeID;
                        if (CouponTypeTemp != null)
                        {
                            CouponTypeIdList = CouponTypeTemp.Split(',');
                            if (CouponTypeIdList != null && CouponTypeIdList.Count() > 0)
                            {

                                var bllCoupon = new CouponBLL(loggingSessionInfo);

                                foreach (var cou in CouponTypeIdList)
                                {
                                    //优惠券未被使用了的数量
                                    int intHaveCout = (int)entityPrize.CountTotal;
                                    DataSet ds = bllCoupon.GetCouponCountByCouponTypeID(cou);
                                    if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    {
                                        int intUnUsedCouponCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RemainCount"].ToString());
                                        if ((para.PrizeCount + intHaveCout) > intUnUsedCouponCount)
                                        {
                                            strErrMsg += ds.Tables[0].Rows[0]["CouponTypeName"].ToString() + "奖品总数量超过未使用优惠券数量,未使用量：" + intUnUsedCouponCount.ToString() + "<br/>";

                                        }
                                    }

                                }
                                if (!string.IsNullOrEmpty(strErrMsg) && strErrMsg.Length > 0)
                                {
                                    throw new APIException(strErrMsg) { ErrorCode = 342 };
                                }


                            }
                        }

                        entityPrize.CountTotal = para.PrizeCount;
                        entityPrize.LastUpdateBy = loggingSessionInfo.UserID;
                        bllPrize.AppendPrize(entityPrize);

                        //入奖品池队列
                        LPrizePoolsBLL bllPools = new LPrizePoolsBLL(loggingSessionInfo);
                        DataSet dsPools = bllPools.GetPrizePoolsByEvent(loggingSessionInfo.ClientID, para.ContactEventId);
                        if (dsPools != null && dsPools.Tables.Count > 0 && dsPools.Tables[0].Rows.Count > 0)
                        {
                            var poolsList = DataTableToObject.ConvertToList<CC_PrizePool>(dsPools.Tables[0]);
                            if (poolsList != null && poolsList.Count > 0)
                            {

                                var redisPrizePoolsBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools.RedisPrizePoolsBLL();
                                CC_PrizePool prizePool = new CC_PrizePool();
                                prizePool.CustomerId = loggingSessionInfo.ClientID;
                                prizePool.EventId = para.ContactEventId;

                                redisPrizePoolsBLL.DeletePrizePoolsList(prizePool);
                                redisPrizePoolsBLL.SetPrizePoolsToRedis(poolsList);
                            }
                        }
                    }
                    else
                    {
                        if (para.PrizeType == "Point")
                            contactEvent.Integral = para.Integral;
                        if (para.PrizeType == "Coupon")
                        {
                            contactEvent.CouponTypeID = string.Join(",", para.CouponTypeID);
                            var bllCoupon = new CouponBLL(loggingSessionInfo);
                            if (CouponTypeIdList != null && CouponTypeIdList.Count() > 0)
                            {
                                foreach (var cou in CouponTypeIdList)
                                {
                                    //优惠券未被使用了的数量
                                    DataSet ds = bllCoupon.GetCouponCountByCouponTypeID(cou);
                                    if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    {
                                        int intUnUsedCouponCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RemainCount"].ToString());
                                        if (para.PrizeCount > intUnUsedCouponCount)
                                        {
                                            strErrMsg += ds.Tables[0].Rows[0]["CouponTypeName"].ToString() + "奖品总数量超过未使用优惠券数量,未使用量：" + intUnUsedCouponCount.ToString() + "<br/>";

                                        }
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(strErrMsg) && strErrMsg.Length > 0)
                            {
                                throw new APIException(strErrMsg) { ErrorCode = 342 };

                            }
                        }
                        if (para.PrizeType == "Chance")
                        {
                            contactEvent.EventId = para.EventId;
                            contactEvent.ChanceCount = para.ChanceCount;
                        }

                        contactEvent.PrizeCount = para.PrizeCount;
                        contactEvent.ContactTypeCode = para.ContactTypeCode;
                        contactEvent.ContactEventName = para.ContactEventName;
                        contactEvent.BeginDate = para.BeginDate;
                        contactEvent.EndDate = para.EndDate;
                        contactEvent.PrizeType = para.PrizeType;
                        contactEvent.CustomerID = CurrentUserInfo.ClientID;
                        contactEvent.RewardNumber = para.RewardNumber;
                        contactEvent.ShareEventId = para.ShareEventId;
                        contactEvent.UnLimited = para.UnLimited;
                        contactEvent.IsCTW = 0;

                        if (para.ContactTypeCode=="Share" && para.ShareEventId != null && para.ShareEventId != "")
                            bllEvent.UpdateEventIsShare(para.ShareEventId);
                        //开始日期是当天的 状态直接变为运行中
                        if (DateTime.Compare(Convert.ToDateTime(para.BeginDate), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) <= 0 && DateTime.Compare(Convert.ToDateTime(para.EndDate), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) >= 0)
                        {
                            contactEvent.Status = 2;
                        }
                    }


                    bllContactEvent.Update(contactEvent);
                    rd.ContactEventId = para.ContactEventId.ToString();
                    rd.ErrMsg = "操作成功";
                    rd.Success = true;
                     
                }
                else
                {
                    ContactEventEntity entityContactEvent = new ContactEventEntity();


                    //RewardType:Point,Coupon,Chance
                    if (para.PrizeType == "Point")
                        entityContactEvent.Integral = para.Integral;
                    if (para.PrizeType == "Coupon")
                    {
                        entityContactEvent.CouponTypeID = string.Join(",", para.CouponTypeID); ;

                        var bllCoupon = new CouponBLL(loggingSessionInfo);
                        if (CouponTypeIdList != null && CouponTypeIdList.Count() > 0)
                        {
                            foreach (var cou in CouponTypeIdList)
                            {
                                //优惠券未被使用了的数量

                                DataSet ds = bllCoupon.GetCouponCountByCouponTypeID(cou);
                                if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    int intUnUsedCouponCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RemainCount"].ToString());
                                    if (para.PrizeCount > intUnUsedCouponCount)
                                    {
                                        strErrMsg += ds.Tables[0].Rows[0]["CouponTypeName"].ToString() + "奖品总数量超过未使用优惠券数量,未使用量：" + intUnUsedCouponCount.ToString() + "<br/>";

                                    }
                                }
                       
                            }
                        }
                        if (!string.IsNullOrEmpty(strErrMsg) && strErrMsg.Length > 0)
                        {
                            throw new APIException(strErrMsg) { ErrorCode = 342 };

                        }
                    }
                    if (para.PrizeType == "Chance")
                    {
                        entityContactEvent.EventId = para.EventId;
                        entityContactEvent.ChanceCount = para.ChanceCount;
                    }

                    if (bllContactEvent.ExistsContact(para.ContactTypeCode, string.IsNullOrEmpty(para.ShareEventId) == true ? "" : para.ShareEventId) > 0)
                    {
                        if (para.ContactTypeCode == "Share" && para.ShareEventId != null && para.ShareEventId.Length > 0)
                        {

                            rd.ErrMsg = "该分享活动已存在";
                        }
                        else
                        {
                            rd.ErrMsg = "该触点活动类型已存在";
                        }
                        rd.Success = false;
                        return rd;
                    }

                    if (para.ShareEventId != null && para.ShareEventId.Length > 0)
                    {
                        //判断触点中的分享设置的开始时间和结束时间是否在被分享的活动时间范围内
                        var entityEvent = bllEvent.GetByID(para.ShareEventId);
                        if (DateTime.Compare(Convert.ToDateTime(para.BeginDate), Convert.ToDateTime(entityEvent.BeginTime)) < 0 || DateTime.Compare(Convert.ToDateTime(para.EndDate), Convert.ToDateTime(entityEvent.EndTime)) > 0)
                        {
                            rd.Success = false;

                            rd.ErrMsg = "活动的时间不在被分享的活动时间范围内";
                            return rd;
                        }
                        entityContactEvent.ShareEventId = para.ShareEventId;
                        entityEvent.IsShare = 1;
                        bllEvent.Update(entityEvent, false);
                    }
                   

                    entityContactEvent.PrizeCount = para.PrizeCount;
                    entityContactEvent.ContactTypeCode = para.ContactTypeCode;
                    entityContactEvent.ContactEventName = para.ContactEventName;
                    entityContactEvent.BeginDate = para.BeginDate;
                    entityContactEvent.EndDate = para.EndDate;
                    entityContactEvent.PrizeType = para.PrizeType;
                    entityContactEvent.CustomerID = CurrentUserInfo.ClientID;
                    entityContactEvent.RewardNumber = para.RewardNumber;
                    entityContactEvent.UnLimited = para.UnLimited;
                    entityContactEvent.IsCTW = 0;
                    //开始日期是当天的 状态直接变为运行中
                    if (DateTime.Compare(Convert.ToDateTime(para.BeginDate), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) <= 0 && DateTime.Compare(Convert.ToDateTime(para.EndDate), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) >= 0)
                    {
                        entityContactEvent.Status = 2;
                    }
                    else
                    {
                        entityContactEvent.Status = 1;
                    }
                    bllContactEvent.Create(entityContactEvent);
             
                        ///保存奖品 生成奖品池
                        var entityPrize = new LPrizesEntity();
                        entityPrize.EventId = entityContactEvent.ContactEventId.ToString();
                        entityPrize.PrizeName = para.ContactEventName;
                        entityPrize.PrizeTypeId = para.PrizeType;
                        entityPrize.Point = para.Integral;
                        entityPrize.CouponTypeID = string.Join(",", para.CouponTypeID); ;
                        entityPrize.CountTotal = para.PrizeCount;
                        entityPrize.CreateBy = loggingSessionInfo.UserID;

                        bllContactEvent.DeleteContactPrize(entityContactEvent.ContactEventId.ToString());
                        bllContactEvent.AddContactEventPrize(entityPrize);

                        //入奖品池队列
                        LPrizePoolsBLL bllPools = new LPrizePoolsBLL(loggingSessionInfo);
                        DataSet dsPools = bllPools.GetPrizePoolsByEvent(loggingSessionInfo.ClientID, entityContactEvent.ContactEventId.ToString());
                        if (dsPools != null && dsPools.Tables.Count > 0 && dsPools.Tables[0].Rows.Count > 0)
                        {
                            var poolsList = DataTableToObject.ConvertToList<CC_PrizePool>(dsPools.Tables[0]);
                            if (poolsList != null && poolsList.Count > 0)
                            {

                                var redisPrizePoolsBLL = new JIT.CPOS.BS.BLL.RedisOperationBLL.PrizePools.RedisPrizePoolsBLL();
                                CC_PrizePool prizePool = new CC_PrizePool();
                                prizePool.CustomerId = loggingSessionInfo.ClientID;
                                prizePool.EventId = entityContactEvent.ContactEventId.ToString();

                                redisPrizePoolsBLL.DeletePrizePoolsList(prizePool);
                                redisPrizePoolsBLL.SetPrizePoolsToRedis(poolsList);
                            }
                        }
                  
                    rd.ContactEventId = entityContactEvent.ContactEventId.ToString();
                    rd.ErrMsg="操作成功";
                    rd.Success = true;
                }

            }
            catch (APIException apiEx)
            {
                rd.Success = false;
                rd.ErrMsg = apiEx.Message;
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
                

            }

            return rd;
        }
    }
}