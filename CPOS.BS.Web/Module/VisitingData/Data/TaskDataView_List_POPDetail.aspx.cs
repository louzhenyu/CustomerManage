using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Extension;
using System.Data;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Module.VisitingData.Data
{
    public partial class TaskDataView_List_POPDetail : JIT.CPOS.BS.Web.PageBase.JITChildPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["btncode"]))
            {
                string res = "";
                switch (Request.QueryString["btncode"])
                {
                    case "search":
                        res = GetPOPInfo(Request.Params);
                        break;
                }
                Response.Write(res);
                Response.End();
            } 
        }

        #region GetPOPInfo
        protected string GetPOPInfo(NameValueCollection rParams)
        {
            DataSet ds = new DataSet();
            if(rParams["POPType"].ToInt()==1)
            {
                ds = new VisitingTaskDataBLL(CurrentUserInfo).GetPOPInfo_Store(Guid.Parse(rParams["POPID"]));
            }
            else
            {
                ds = new VisitingTaskDataBLL(CurrentUserInfo).GetPOPInfo_Distributor(rParams["POPID"].ToInt());
            }
            return ds.Tables[0].ToJSON();
        }
        #endregion
    }
}