/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/24 15:51:18
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
    public partial class WLegalizeBLL
    {
        #region 客户认证是否存在
        /// <summary>
        /// 客户认证是否存在
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public VipEntity SetVipLegalize(string OpenId, string CustomerId,string SalesAmount,out string strError)
        {
            string strNo = string.Empty;
            VipEntity vipInfo = new VipEntity();
            try
            {
                VipBLL vipServer = new VipBLL(this.CurrentUserInfo);
                //从总部获取信息
                bool bReturn = vipServer.GetVipInfoFromApByOpenId(OpenId, null);
                if (bReturn)
                {
                    //获取当前最大值
                    int No = _currentDAO.GetMaxNo(CustomerId);
                    #region 插入本次信息
                    WLegalizeEntity info = new WLegalizeEntity();
                    info.LegalizeId = BaseService.NewGuidPub();
                    info.OpenId = OpenId;
                    info.CustomerId = CustomerId;
                    info.CreateBy = OpenId;
                    info.LastUpdateBy = OpenId;
                    info.No = No;
                    info.SalesAmount = Convert.ToDecimal(SalesAmount);
                    Create(info);
                    #endregion
                    strNo = No.ToString();
                    #region
                    VipEntity[] vipObj = { };
                    vipObj = vipServer.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = OpenId

                    }, null);
                    if (vipObj != null && vipObj.Length > 0 && vipObj[0] != null)
                    {
                        vipInfo = vipObj[0];
                        vipInfo.SerialNumber = strNo;
                        strError = "OK";
                    }
                    else {
                        strError = "您还不未关注微信账号</br>请联系门店工作人员!";
                    }
                    #endregion
                }
                else {
                    strError = "获取总部数据出错，请找管理员帮助.";
                }
                
            }
            catch (Exception ex) {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion
    }
}