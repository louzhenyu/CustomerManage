/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 15:34:04
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
    /// 业务处理：  
    /// </summary>
    public partial class VipShowLogBLL
    {
        #region 获取近期关注的会员
        /// <summary>
        /// 获取近期关注的会员
        /// </summary>
        /// <param name="Weixin"></param>
        /// <param name="TimeLength"></param>
        /// <returns></returns>
        public IList<VipShowLogEntity> GetRecentfollowers(string Weixin, string TimeLength)
        {
            IList<VipShowLogEntity> vipShowLogInfoList = new List<VipShowLogEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetRecentfollowers(Weixin, TimeLength);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipShowLogInfoList = DataTableToObject.ConvertToList<VipShowLogEntity>(ds.Tables[0]);
                if (vipShowLogInfoList != null && vipShowLogInfoList.Count > 0)
                {
                    VipBLL vipService = new VipBLL(this.CurrentUserInfo);
                    foreach(var info in vipShowLogInfoList)
                    {
                        if (info.VipCode == null || info.VipCode.Equals(""))
                        {
                            VipEntity vipInfo = new VipEntity();
                            vipInfo.VipCode = vipService.GetVipCode();
                            info.VipCode = vipInfo.VipCode;
                            vipInfo.VIPID = info.VIPID;
                            vipService.Update(vipInfo,false);
                        }
                    }
                }
            }
            return vipShowLogInfoList;
        }
        #endregion

        #region 根据流水号获取客户信息
        /// <summary>
        /// 根据流水号获取客户信息
        /// </summary>
        /// <param name="Weixin"></param>
        /// <param name="TimeLength"></param>
        /// <returns></returns>
        public IList<VipShowLogEntity> GetVipInfoBySerialNumber()
        {
            IList<VipShowLogEntity> vipShowLogInfoList = new List<VipShowLogEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipInfoBySerialNumber();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipShowLogInfoList = DataTableToObject.ConvertToList<VipShowLogEntity>(ds.Tables[0]);
            }
            return vipShowLogInfoList;
        }
        #endregion
    }
}