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
        public void CTWRegOrFocusLog(string strCTWEventId, string strRegVipId, string strFocusVipId, LoggingSessionInfo loggingSession)
        {
            T_LEventsRegVipLogEntity entityRegVipLog = new T_LEventsRegVipLogEntity();
            
            entityRegVipLog.BusTypeCode = "CTW";
            entityRegVipLog.ObjectId = strCTWEventId;
            entityRegVipLog.RegVipId = strRegVipId;
            entityRegVipLog.FocusVipId = strFocusVipId;
            this._currentDAO.Create(entityRegVipLog);
            //���㽱��
            ContactEventBLL bllContactEvent = new ContactEventBLL(loggingSession);
            var entityContact = bllContactEvent.QueryByEntity(new ContactEventEntity() { EventId = strCTWEventId, IsDelete = 0, IsCTW = 1 }, null).SingleOrDefault();
            if(entityContact!=null)
            {
                LPrizesBLL bllPrize = new LPrizesBLL(loggingSession);
                var prize = bllPrize.QueryByEntity(new LPrizesEntity() { EventId = entityContact.ContactEventId.ToString(), IsDelete = 0 }, null).SingleOrDefault();
                if(prize!=null)
                {
                    CouponBLL bllCoupon = new CouponBLL(loggingSession);
                    bllCoupon.CouponBindVip(strRegVipId, prize.CouponTypeID, entityContact.ContactEventId.ToString(), "CTW");
                }

            }
        }
    }
}