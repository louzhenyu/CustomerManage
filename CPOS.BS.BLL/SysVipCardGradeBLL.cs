/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class SysVipCardGradeBLL
    {
        #region
        /// <summary>
        /// ��ȡ�ȼ�����
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public int GetVipLevelCount(string levelId)
        {
            return _currentDAO.GetVipLevelCount(levelId);
        }
        #endregion
        /// <summary>
        /// ��ȡ��Ա�ۿ�
        /// </summary>
        /// <returns></returns>
        public decimal GetVipDiscount()
        {
            decimal vipDiscount = 10;    //��Ա�ۿ�
            //�ж��Ƿ����û�Ա�ۿ�
            var basicSettingBLL = new CustomerBasicSettingBLL(CurrentUserInfo);
            var vipBLL = new VipBLL(CurrentUserInfo);
            //var sysVipCardGradeBLL = new SysVipCardGradeBLL(CurrentUserInfo);
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(CurrentUserInfo);
            //string vipDiscountSetting = basicSettingBLL.GetSettingValueByCode("VipCardGradeSalesPreferentia");
            //if (!string.IsNullOrEmpty(vipDiscountSetting))
            //{
            //    if (int.Parse(vipDiscountSetting) > 0)//����
            //    {
            //        var vipInfo = vipBLL.GetByID(CurrentUserInfo.UserID);
            //        if (vipInfo != null)
            //        {
            //            if (vipInfo.VipLevel>0)
            //            {
            //                //��ȡ��Ա�ȼ��ۿ�
            //                var sysVipCardGradeInfo = sysVipCardGradeBLL.GetByID(vipInfo.VipLevel);
            //                if (sysVipCardGradeInfo != null)
            //                    vipDiscount = sysVipCardGradeInfo.SalesPreferentiaAmount.Value;
            //            }
            //        }
            //    }
            //}
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = CurrentUserInfo.UserID }, null).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    var vipCardRuleInfo = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                    if (vipCardRuleInfo != null && vipCardRuleInfo.CardDiscount > 0)
                    {
                        vipDiscount = vipCardRuleInfo.CardDiscount == null ? 10 : (vipCardRuleInfo.CardDiscount.Value / 10);
                    }
                }
            }
            return vipDiscount;
        }
    }
}