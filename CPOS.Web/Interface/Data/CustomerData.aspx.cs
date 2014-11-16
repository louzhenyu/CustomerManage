using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Data;

namespace JIT.CPOS.Web.Interface.Data
{
    public partial class CustomerData : System.Web.UI.Page
    {
        string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);

                switch (dataType)
                {
                    case "setSignIn":     //客户登录  qianzhi 2014-03-13
                        content = SetSignIn();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region SetSignIn 客户登录  qianzhi 2014-03-13

        /// <summary>
        /// 客户登录
        /// </summary>
        public string SetSignIn()
        {
            string content = string.Empty;

            var respData = new SetSignInRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SetSignInReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetSignIn: {0}", reqContent)
                });

                #region 判断参数是否传递
                if (string.IsNullOrEmpty(reqObj.special.customerCode))
                {
                    respData.code = "103";
                    respData.description = "customerCode不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.special.phone))
                {
                    respData.code = "103";
                    respData.description = "phone不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.special.password))
                {
                    respData.code = "103";
                    respData.description = "password不能为空";
                    return respData.ToJSON();
                }
                #endregion

                WMenuBLL menuServer = new WMenuBLL(Default.GetAPLoggingSession(""));
                customerId = menuServer.GetCustomerIDByCustomerCode(reqObj.special.customerCode);

                if (string.IsNullOrEmpty(customerId))
                {
                    respData.code = "103";
                    respData.description = "customerCode对应的客户不存在";
                    return respData.ToJSON();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var ds = new VipBLL(loggingSessionInfo).SetSignIn(reqObj.special.phone, reqObj.special.password, customerId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content = new SetSignInRespContentData();
                    respData.content.customerId = customerId;
                    respData.content.userId = ds.Tables[0].Rows[0]["user_id"].ToString();
                }
                else
                {
                    respData.code = "103";
                    respData.description = "用户名或密码错误";
                    return respData.ToJSON();
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class SetSignInRespData : Default.LowerRespData
        {
            public SetSignInRespContentData content;
        }
        public class SetSignInRespContentData
        {
            public string userId { get; set; }      //用户标识
            public string customerId { get; set; }  //客户标识
        }
        public class SetSignInReqData : Default.ReqData
        {
            public SetSignInReqSpecialData special;
        }
        public class SetSignInReqSpecialData
        {
            public string customerCode { get; set; }    //客户号码
            public string phone { get; set; }           //手机号码
            public string password { get; set; }        //密码
        }

        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion
    }
}