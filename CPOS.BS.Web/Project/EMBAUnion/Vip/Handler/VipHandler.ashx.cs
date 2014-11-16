﻿using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;

using JIT.Utility.Web;
using JIT.Utility.Reflection;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using System.Configuration;
using System.Xml;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Base.XML;



namespace JIT.CPOS.BS.Web.Project.EMBAUnion.Vip.Handler
{
    /// <summary>
    /// VipHandler 的摘要说明
    /// </summary>
    public class VipHandler : JITCPOSAjaxHandler
    {
        public class GridInitEntity
        {
            /// <summary>
            /// 表格数据定义
            /// </summary>
            public List<GridColumnModelEntity> GridDataDefinds { get; set; }
            /// <summary>
            /// 表格表头定义
            /// </summary>
            public List<GridColumnEntity> GridColumnDefinds { get; set; }
            /// <summary>
            /// 表格数据
            /// </summary>
            public PageResultEntity GridDatas { get; set; }

        }

        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "PageGridData": //查询数据分页查询
                    res = GetPageData(pContext).ToJSON();
                    break;
                case "InitGridData": //初使化终端编辑列表的数据
                    res = GetInitGridData(pContext).ToJSON();
                    break;
                case "GetList":
                    res = GetList(pContext).ToJSON();
                    break;
                case "GetModuleColumn":
                    res = GetModuleColumn().ToJSON();
                    break;
                case "GetVipDetail":
                    res = GetVipDetail(pContext.Request.Form).ToJSON();
                    break;
                case "GetVipStatus":
                    res = GetVipStatus().ToJSON();
                    break;
                case "VipApprove":
                    res = VipApprove(pContext.Request.Form);
                    break;
                case "NoVipApprove":
                    res = NoVipApprove(pContext.Request.Form);
                    break;
                case "GetVipStatusNum"://得到状态的数量
                    res = GetVipStatusNum(pContext);
                    break;
                case "EditView": //得到修改控件
                    res = GetEditControls(pContext).ToJSON();
                    break;
                case "EditViewData": //得到表单数据
                    res = GetEditData(pContext).ToJSON();
                    break;
                case "GetVipStatusInfo":
                    res = GetVipStatusInfo(pContext.Request.Form);
                    break;
                case "update": //更新逻辑
                    res = Update(pContext);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetPageData
        private PageResultEntity GetPageData(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }

            int pageSize = pContext.Request["limit"].ToInt();
            int pageIndex = pContext.Request["page"].ToInt();

            return b.GetPageData(l, pageSize, pageIndex - 1);
        }
        #endregion

        #region GetInitGridData
        private GridInitEntity GetInitGridData(HttpContext pContext)
        {
            GridInitEntity g = new GridInitEntity();
            g.GridDataDefinds = GetGridDataModels(pContext);
            g.GridColumnDefinds = GetGridColumns(pContext);

            return g;
        }
        #endregion

        #region GetGridDataModels
        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModels(HttpContext pContext)
        {
            EMBAUnionBLL bll = new EMBAUnionBLL(CurrentUserInfo, "vip");
            return bll.GetGridDataModels();
        }
        #endregion

        #region GetGridColumns
        /// <summary>
        /// 获取终端列表表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumns(HttpContext pContext)
        {
            EMBAUnionBLL bll = new EMBAUnionBLL(CurrentUserInfo, "vip");
            return bll.GetGridColumns();
        }
        #endregion

        #region GetList
        private DataSet GetList(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }

            int pageSize = pContext.Request["limit"].ToInt();
            int pageIndex = pContext.Request["page"].ToInt();

            return b.GetList(l, pageSize, pageIndex);
        }
        #endregion

        #region GetModuleColumn
        public string GetModuleColumn()
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            return b.GetModuleColumn().ToJSON();
        }
        #endregion

        #region GetVipDetail
        public string GetVipDetail(NameValueCollection rParams)
        {
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
                string vipID = rParams["id"].ToString();
                DataSet ds = new DataSet();
                ds = b.GetVipDetail(vipID);
                return string.Format("{{\"vipDetail\":{0}}}", ds.Tables[0].ToJSON());
            }
            else
            {
                return "{{\"vipDetail\":\"\"}}";
            }
        }

        #endregion

        #region VipApprove
        /// <summary>
        /// VIP审核
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string VipApprove(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            if ((!string.IsNullOrEmpty(rParams["id"])) && (!string.IsNullOrEmpty(rParams["status"])))
            {
                EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
                string vipID = rParams["id"].ToString();
                string vipStatus = rParams["status"].ToString();
                b.VipApprove(vipID, vipStatus).ToString();
                res = "{success:true,msg:'保存成功'}";
                return res;
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region GetVipStatus
        public DataSet GetVipStatus()
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip2");
            return b.GetVipStatus();
        }
        #endregion

        #region GetVipStatusNum
        private string GetVipStatusNum(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }
            DataSet ds = b.GetVipStatusNum(l);
            int allCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());

            return string.Format("{{\"unapproveCount\":{0},\"approveCount\":{1},\"approveSuccessCount\":{2},\"allCount\":{3}}}"
              , ds.Tables[0].Rows[0][0].ToString()
              , ds.Tables[0].Rows[1][0].ToString()
              , ds.Tables[0].Rows[2][0].ToString()
              , allCount.ToString()
              );
        }
        #endregion

        #region NoVipApprove
        /// <summary>
        /// 不通过/保存成功
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string NoVipApprove(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            if ((!string.IsNullOrEmpty(rParams["id"])) && (!string.IsNullOrEmpty(rParams["remarke"])))
            {
                EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
                string vipID = rParams["id"].ToString();
                string vipRemarke = rParams["remarke"].ToString();
                string vipStatus = rParams["status"].ToString();
                b.NoVipApprove(vipID, vipRemarke, vipStatus).ToString();
                res = "{success:true,msg:'保存成功'}";
                #region 邮件发送
                try
                {
                    XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);
                    FromSetting fs = new FromSetting();
                    fs.SMTPServer = xml.SelectNodeText("//Root/AssociationMail//SMTPServer", 0);
                    fs.SendFrom = xml.SelectNodeText("//Root/AssociationMail//MailSendFrom", 0);
                    fs.UserName = xml.SelectNodeText("//Root/AssociationMail//MailUserName", 0);
                    fs.Password = xml.SelectNodeText("//Root/AssociationMail//MailUserPassword", 0);
                    string content = xml.SelectNodeText("//Root/AssociationMail//MAILCOENT", 0);

                    VipEntity vip = new VipBLL(CurrentUserInfo).GetByID(vipID);
                    List<VipViewEntity> bll = new OnlineShoppingItemBLL(CurrentUserInfo).GetUserInfo(vipID);
                    content = content + "&customerId=" + CurrentUserInfo.ClientID + "&pageIndex=" + bll[0].PageIndex;
                    Mail.SendMail(fs, vip.Email + "," + xml.SelectNodeText("//Root/AssociationMail//MailTo", 0), xml.SelectNodeText("//Root/AssociationMail//MailTitle", 0), vip.UserName + "你好:你的URL：" + content, null);
                    content = content + "&customerId=" + CurrentUserInfo.ClientID + "&pageIndex=" + bll[0].PageIndex;
                    Mail.SendMail(fs, vip.Email + "," + xml.SelectNodeText("//Root/AssociationMail//MailTo", 0), xml.SelectNodeText("//Root/AssociationMail//MailTitle", 0), vip.UserName + "你好,你所上传的认证信息审核不通过,请点击链接重新上传：" + content, null);
                }
                catch
                {

                }
                #endregion

                return res;
            }
            else
            {
                return "";
            }

        }

        #endregion

        #region GetEditControls
        /// <summary>
        /// 得到编辑页面控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetEditControls(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            return b.GetEditControls();
        }
        #endregion

        #region GetEditData
        /// <summary>
        /// 返回编辑页面的值
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetEditData(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            string pKeyValue = pContext.Request["id"];
            return b.GetEditData(pKeyValue);
        }
        #endregion

        #region GetVipStatusInfo
        /// <summary>
        /// 根据会员ID获取会员状态
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string GetVipStatusInfo(NameValueCollection rParams)
        {
            if ((!string.IsNullOrEmpty(rParams["id"])))
            {
                VipBLL bll = new VipBLL(CurrentUserInfo);
                var info = bll.GetByID(rParams["id"]);
                return string.Format("{{\"status\":{0}}}", info.Status);
            }
            return "";
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private string Update(HttpContext pContext)
        {
            EMBAUnionBLL b = new EMBAUnionBLL(CurrentUserInfo, "vip");
            string pKeyValue = pContext.Request["pKeyValue"];
            string pEditValue = pContext.Request["pEditValue"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pEditValue))
            {
                l = pEditValue.DeserializeJSONTo<List<DefindControlEntity>>();
            }
            bool res = b.Update(l, pKeyValue);
            if (res == true)
            {
                return "{success:true,msg:''}";
            }
            else
            {
                return "{success:false}";
            }
        }
        #endregion

    }
}