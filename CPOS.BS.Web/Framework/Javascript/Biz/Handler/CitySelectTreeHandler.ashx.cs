using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Web.SessionState;
using JIT.Utility.Web.ComponentModel;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CitySelectTreeHandler 的摘要说明
    /// </summary>
    public class CitySelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
        IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <param name="pParentNodeID">父节点ID</param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();

            var cityService = new CityService(new SessionManager().CurrentUserLoginInfo);
            IList<CityInfo> data = new List<CityInfo>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("node") != null && Request("node") != string.Empty)
            {
                key = Request("node").ToString().Trim();
            }
            if (key == "root")
            {
                key = "";
            }

            if (key.Length == 2)//更具省份获取城市
            {
                data = cityService.GetCityListByProvince(key);
            }
            else if (key.Length == 4)//更具城市获取区县
            {
                data = cityService.GetAreaListByCity(key);
            }
            else if (key.Length == 0)//
            {
                data = cityService.GetProvinceList();
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            var parentCode = string.Empty;
            foreach (var item in data)
            {
                nodes.Add(item.City_Code, item.City_Name, 
                    GetParentCode(item.City_Code), 
                    item.City_Code.Length == 6 ? true : false);
            }
            return nodes;
        }

        private string GetParentCode(string cityCode)
        {
            if (cityCode.Length == 2)//省份的上级返回
                return "root";
            if (cityCode.Length == 4)//城市的上级是省，是两位数
                return cityCode.Substring(0, 2);
            if (cityCode.Length == 6)//区的上级，取中间的两位
                return cityCode.Substring(2, 2);
            return string.Empty;
        }

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}