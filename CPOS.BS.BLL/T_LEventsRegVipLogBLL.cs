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
    /// ҵ����  
    /// </summary>
    public partial class T_LEventsRegVipLogBLL
    {
        /// <summary>
        /// ��ע����ע����־
        /// </summary>
        /// <param name="strCTWEventId">������ʶ</param>
        /// <param name="strVipId">ע�������vipid</param>
        /// <param name="strFocusVipId">��ע������vipid</param>
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
            BaseService.WriteLogWeixin(" ����ֿ���־��" + strCTWEventId + "+" + strVipId + "+" + strType);
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
                    //���㽱��
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
                                #region ���û���ͳһ�ӿ�
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
                                //�䶯ǰ����
                                string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                                //�䶯����
                                string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                                var vipIntegralDetailId = bllVipIntegral.AddIntegral(ref vipInfo, null, IntegralDetail, null, loggingSession);
                                //����΢�Ż��ֱ䶯֪ͨģ����Ϣ
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

                BaseService.WriteLogWeixin(" ����ֿ���־��" + ex.ToString() + "+");
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
            BaseService.WriteLogWeixin(" ����ֿ���־��" + strCTWEventId + "+" + strVipId + "+" + strType);
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

                BaseService.WriteLogWeixin(" �Ż�ȯע����־��" + ex.ToString() + "+");
            }
        }
    }
}