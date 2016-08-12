/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/14 16:14:12
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
using JIT.CPOS.BS.Entity;
using CPOS.Common;
using JIT.CPOS.BS.BLL.WX;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_LEventsRegVipLogBLL
    {
        /// <summary>
        /// 关注或者注册日志
        /// </summary>
        /// <param name="strCTWEventId">主题活动标识</param>
        /// <param name="strVipId">注册操作的vipid</param>
        /// <param name="strFocusVipId">关注操作的vipid</param>
        public void CTWRegOrFocusLog(string strCTWEventId, string strRegVipId, string strFocusVipId, LoggingSessionInfo loggingSession,string strType)
        {
            string strVipId = string.Empty;
            if (strType == "Reg")
            {
                strVipId = strRegVipId;
            }
            if (strType == "Focus")
            {
                strVipId = strFocusVipId;
            }
            BaseService.WriteLogWeixin(" 创意仓库日志：" + strCTWEventId + "+" + strVipId + "+" + strType);
            try
            {


                int intResult = this._currentDAO.IsExistsLog(strCTWEventId, strVipId, strType, loggingSession.ClientID);
                if (intResult == 0)
                {

                    T_LEventsRegVipLogEntity entityRegVipLog = new T_LEventsRegVipLogEntity();

                    entityRegVipLog.BusTypeCode = "CTW";
                    entityRegVipLog.ObjectId = strCTWEventId;
                    entityRegVipLog.RegVipId = strRegVipId;
                    entityRegVipLog.FocusVipId = strFocusVipId;
                    entityRegVipLog.CustomerId = loggingSession.ClientID;
                    this._currentDAO.Create(entityRegVipLog);
                    //触点奖励
                    ContactEventBLL bllContactEvent = new ContactEventBLL(loggingSession);
                    var entityContact = bllContactEvent.QueryByEntity(new ContactEventEntity() { EventId = strCTWEventId, IsDelete = 0, IsCTW = 1, ContactTypeCode = strType }, null).SingleOrDefault();
                    if (entityContact != null)
                    {


                        LPrizesBLL bllPrize = new LPrizesBLL(loggingSession);
                        var prize = DataTableToObject.ConvertToList<LPrizesEntity>(bllPrize.GetCouponTypeIDByEventId(entityContact.ContactEventId.ToString()).Tables[0]).FirstOrDefault();

                        if (prize != null)
                        {
                            CouponBLL bllCoupon = new CouponBLL(loggingSession);
                            if (prize.PrizeTypeId == "Coupon")
                            {
                                bllCoupon.CouponBindVip(strVipId, prize.CouponTypeID, entityContact.ContactEventId.ToString(), strType);
                            }
                            if (prize.PrizeTypeId == "Point")
                            {
                                #region 调用积分统一接口
                                var salesReturnBLL = new T_SalesReturnBLL(loggingSession);
                                VipIntegralBLL bllVipIntegral = new VipIntegralBLL(loggingSession);
                                var vipBLL = new VipBLL(loggingSession);

                                var vipInfo = vipBLL.GetByID(strVipId);
								string strIntegralSourceID = string.Empty;
								switch (entityContact.ContactTypeCode.ToString().TrimEnd()) {
									case "Reg":
										strIntegralSourceID = "2";
										break;
									case "Focus":
										strIntegralSourceID = "3";
										break;
									case "Share":
										strIntegralSourceID = "28";
										break;
									default:
										strIntegralSourceID = "22";
										break;
								}
                                var IntegralDetail = new VipIntegralDetailEntity()
                                {
                                    Integral = prize.Point,
									IntegralSourceID = strIntegralSourceID,
                                    ObjectId = entityContact.ContactEventId.ToString()
                                };
                                //变动前积分
                                string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                                //变动积分
                                string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                                var vipIntegralDetailId = bllVipIntegral.AddIntegral(ref vipInfo, null, IntegralDetail, null, loggingSession);
                                //发送微信积分变动通知模板消息
                                if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                                {
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, loggingSession);
                                }

                                #endregion
                            }
                            //LPrizeWinnerEntity entityPrizeWinner = new LPrizeWinnerEntity()
                            //{
                            //    PrizeWinnerID = Guid.NewGuid().ToString(),
                            //    VipID = strVipId,
                            //    PrizeID = prize.PrizesID,
                            //    PrizeName = prize.PrizeName,
                            //    PrizePoolID = entityPrizePool == null ? "" : entityPrizePool.PrizePoolsID,
                            //    CreateBy = this.CurrentUserInfo.UserID,
                            //    CreateTime = DateTime.Now,
                            //    IsDelete = 0
                            //};

                            //bllPrizeWinner.Create(entityPrizeWinner);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                BaseService.WriteLogWeixin(" 创意仓库日志：" + ex.ToString() + "+");
            }
        }


        public void CouponRegOrFocusLog(string strCTWEventId, string strRegVipId, string strFocusVipId, LoggingSessionInfo loggingSession, string strType)
        {
            string strVipId = string.Empty;
            if (strType == "Reg")
            {
                strVipId = strRegVipId;
            }
            if (strType == "Focus")
            {
                strVipId = strFocusVipId;
            }
            BaseService.WriteLogWeixin(" 创意仓库日志：" + strCTWEventId + "+" + strVipId + "+" + strType);
            try
            {


                int intResult = this._currentDAO.IsExistsLog(strCTWEventId, strVipId, strType, loggingSession.ClientID);
                if (intResult == 0)
                {

                    T_LEventsRegVipLogEntity entityRegVipLog = new T_LEventsRegVipLogEntity();

                    entityRegVipLog.BusTypeCode = "Coupon";
                    entityRegVipLog.ObjectId = strCTWEventId;
                    entityRegVipLog.RegVipId = strRegVipId;
                    entityRegVipLog.FocusVipId = strFocusVipId;
                    entityRegVipLog.CustomerId = loggingSession.ClientID;
                    this._currentDAO.Create(entityRegVipLog);                   
                }
            }
            catch (Exception ex)
            {

                BaseService.WriteLogWeixin(" 优惠券注册日志：" + ex.ToString() + "+");
            }
        }
    }
}