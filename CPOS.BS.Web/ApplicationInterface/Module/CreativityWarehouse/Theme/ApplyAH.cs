using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.Theme
{
    public class ApplyAH : BaseActionHandler<ApplyRP, ApplyRD>
    {

        protected override ApplyRD ProcessRequest(APIRequest<ApplyRP> pRequest)
        {
            var rd = new ApplyRD();

            FromSetting fs = new FromSetting();
            fs.SMTPServer = "smtp.exmail.qq.com";
            fs.SendFrom = "jianxian.wu@zmind.cn";
            fs.UserName = "jianxian.wu@zmind.cn";
            fs.Password = "830104wujx";
            string mailTo = "business@zmind.cn";
            string[] s = new string[1];

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_CTW_LEventThemeBLL bllTheme = new T_CTW_LEventThemeBLL(loggingSessionInfo);

            T_CTW_LEventThemeEntity entityTheme = bllTheme.GetByID(para.ThemeId);

            DataSet dsCustomer=bllTheme.GetCustomerInfo();

            if(dsCustomer.Tables[0]==null)
            {
                 rd.errCode = 1;
                rd.errMsg = "商户信息不存在！";
                return rd;
            }
            t_customerEntity entityCustomer = DataTableToObject.ConvertToObject<t_customerEntity>(dsCustomer.Tables[0].Rows[0]);

            if(entityTheme!=null)
            {
                string strSubject = "活动申请";
                string strBody = "商户名称：" + entityCustomer.customer_name + "<br/>联系人：" + entityCustomer.customer_contacter + "<br/>联系方式：" + entityCustomer.customer_tel + "<br/>活动名称：" + entityTheme.ThemeName + "<br/>申请时间：" + DateTime.Now;
                Mail.SendMail(fs, mailTo, strSubject, strBody, s);
                rd.errCode = 0;
            }

            return rd;
        }
    }
}