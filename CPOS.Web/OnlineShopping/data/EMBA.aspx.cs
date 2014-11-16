using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    public partial class EMBA : System.Web.UI.Page
    {
        public string customerId;
        public VipBLL vipBLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].ToString().Trim();
                switch (dataType)
                {
                    case "updateVip":      //1.获取商品列表
                        content = UpdateVip();
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

        public string UpdateVip()
        {
            string content = string.Empty;
            var respData = new UpdateVipRespData();
            respData.content = new UpdateVipRespContentData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("UpdateVip: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<UpdateVipReqData>();
                reqObj = reqObj == null ? new UpdateVipReqData() : reqObj;

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.vipRealName == null || reqObj.special.vipRealName.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "姓名不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "电话不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.school == null || reqObj.special.school.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "学校不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.className == null || reqObj.special.className.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "班级不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.company == null || reqObj.special.company.Equals(""))
                {
                    respData.code = "2205";
                    respData.description = "公司不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.position == null || reqObj.special.position.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "职位不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = vipBLL.Query(
                    new IWhereCondition[] {
                        new EqualsCondition() { FieldName = "Phone", Value = reqObj.special.phone }
                        , new EqualsCondition() { FieldName = "ClientId", Value = customerId }
                    }
                    , null).FirstOrDefault();

                bool newFlag = false;

                if (vipInfo == null)
                {
                    newFlag = true;
                    vipInfo = new VipEntity();
                    vipInfo.VIPID = Utils.NewGuid();
                    vipInfo.Phone = reqObj.special.phone;
                    vipInfo.VipLevel = 1;
                    vipInfo.Status = 1;
                    vipInfo.ClientID = customerId;
                    vipInfo.RegistrationTime = DateTime.Now;
                    vipInfo.IsDelete = 0;
                    vipInfo.CreateTime = DateTime.Now;
                    vipInfo.CreateBy = vipInfo.VIPID;
                }
                else
                {
                    vipInfo.LastUpdateTime = DateTime.Now;
                    vipInfo.LastUpdateBy = reqObj.common.userId;
                }

                vipInfo.VipRealName = reqObj.special.vipRealName;
                vipInfo.Col41 = reqObj.special.school;
                vipInfo.Col42 = reqObj.special.className;
                vipInfo.Col43 = reqObj.special.company;
                vipInfo.Col44 = reqObj.special.position;
                vipInfo.Email = reqObj.special.email;
                vipInfo.Col46 = reqObj.special.hobby;
                vipInfo.Col47 = reqObj.special.myValue;
                vipInfo.Col48 = reqObj.special.needValue;
                vipInfo.SinaMBlog = reqObj.special.sinaMBlog;
                vipInfo.TencentMBlog = reqObj.special.weixin;

                // CreateQrcode
                var qrImageUrl = "";
                var qrFlag = cUserService.CreateQrcode(vipInfo.VipRealName, vipInfo.Email, "", vipInfo.Phone, "",
                    vipInfo.Col43, vipInfo.Col44, "", "", ref qrImageUrl);
                if (qrFlag)
                {
                    vipInfo.QRVipCode = qrImageUrl;
                }
                else
                {
                    respData.code = "2207";
                    respData.description = "用户二维码数据保存失败";
                    return respData.ToJSON().ToString();
                }

                #endregion

                string strError = string.Empty;
                if (newFlag)
                    vipBLL.Create(vipInfo);
                else
                    vipBLL.Update(vipInfo, false);

                respData.content.VipId = vipInfo.VIPID;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo() { ErrorMessage = ex.Message });
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }

            content = respData.ToJSON();
            return content;
        }

        #region 参数对象
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class UpdateVipReqData : ReqData
        {
            public UpdateVipReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class UpdateVipReqSpecialData
        {
            public string vipRealName { get; set; }
            public string phone { get; set; }
            public string school { get; set; }
            public string className { get; set; }
            public string company { get; set; }
            public string position { get; set; }
            public string email { get; set; }
            public string hobby { get; set; }
            public string myValue { get; set; }
            public string needValue { get; set; }
            public string sinaMBlog { get; set; }
            public string weixin { get; set; }
        }

        public class UpdateVipRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public UpdateVipRespContentData content;
        }
        public class UpdateVipRespContentData
        {
            public string VipId;
        }

        #endregion
    }
}