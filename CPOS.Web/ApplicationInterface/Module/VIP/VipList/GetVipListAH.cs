using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.VipList.Request;
using JIT.CPOS.DTO.Module.VIP.VipList.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Reflection;
using System.Configuration;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipList
{
    /// <summary>
    /// 获取表单列表
    /// </summary>
    public class GetVipListAH : BaseActionHandler<GetVipListRP, GetVipListRD>
    {

        #region 错误码
        const int ERROR_LNEWS_FAILURE = 330;
        #endregion

        protected override GetVipListRD ProcessRequest(DTO.Base.APIRequest<GetVipListRP> pRequest)
        {
            GetVipListRD rd = new GetVipListRD();

            pRequest.CustomerID = ConfigurationManager.AppSettings["WiFiCustomerID"].Trim();
            pRequest.Parameters.Validate();

            if (string.IsNullOrEmpty(pRequest.Parameters.UnitID))
            {
                //门店ID，前期默认，可以不传(上海越洋广场店)
                pRequest.Parameters.UnitID = "9678d0e66d8d411baf8a6027c3e623f9";
            }

            #region 获取表单列表
            try
            {
                var ds = new WiFiUserVisitBLL(base.CurrentUserInfo).GetVipListByPage(pRequest.Parameters.UnitID, pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize);

                if (ds.Tables.Count > 0)
                {
                    rd.TotalRow = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        IList<VipInfo> list = DataLoader.LoadFrom<VipInfo>(ds.Tables[1]);

                        foreach (VipInfo item in list)
                        {

                            int minutes = int.Parse(item.VipTime);

                            //格式化处理登录时间
                            int h = minutes / 60;
                            int m = minutes % 60;

                            if (h > 0)
                            {
                                item.VipTime = string.Format("{0}小时{1}分钟前", h, m);
                            }
                            else
                            {
                                item.VipTime = string.Format("{0}分钟前", m);
                            }

                        }

                        rd.VipList = list.ToArray();
                    }
                    
                }
            }
            catch (Exception)
            {
                throw new APIException("查询数据错误") { ErrorCode = ERROR_LNEWS_FAILURE };
            }
            #endregion

            return rd;
        }

    }
}