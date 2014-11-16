/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/09 12:27
 * Description	:设置模块页
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
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class SetSysPageAH : BaseActionHandler<SetSysPageRP, SetSysPageRD>
    {

        protected override SetSysPageRD ProcessRequest(APIRequest<SetSysPageRP> pRequest)
        {
            SetSysPageRD rd = new SetSysPageRD();
            var para = pRequest.Parameters;
            var userInfo = this.CurrentUserInfo;
            userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["APConn"];
            SysPageBLL sysPageBLL = new SysPageBLL(userInfo);
            SysPageEntity sysPageEntity = new SysPageEntity();
            JsonValveDeserializeDoc(para.PageJson, sysPageEntity);

            #region 错误码
            const int ExistsPageKey = 301;
            #endregion



            if (string.IsNullOrWhiteSpace(para.PageId.ToString()))  //如果为NULL。则新增
            {

                if (!string.IsNullOrWhiteSpace(sysPageEntity.PageKey))
                {
                    #region 判断数据库中是否存在该PageKey.如果存在抛出错误信息。
                    bool boolPageKey = sysPageBLL.GetExistsPageKey(sysPageEntity.PageKey);
                    if (boolPageKey)
                    {
                        throw new APIException(string.Format("已经存在PageKey'{0}'", sysPageEntity.PageKey)) { ErrorCode = ExistsPageKey };
                    }
                    else
                    {
                        sysPageEntity.PageID = Guid.NewGuid();
                        sysPageEntity.Version = para.Version;
                        sysPageEntity.Author = para.Author;
                        sysPageEntity.IsRebuild = 0; //新增默认标识为0
                       // sysPageEntity.CustomerID = CurrentUserInfo.ClientID;
                        sysPageBLL.Create(sysPageEntity);
                        rd.PageId = sysPageEntity.PageID;
                    }
                }
                    #endregion
            }
            else //如果不为NULL，则修改
            {
                bool ExistPageKey = sysPageBLL.GetExistsPageKey(sysPageEntity.PageKey, Guid.Parse(para.PageId.ToString()).ToString());
                if (ExistPageKey)
                {
                    throw new APIException(string.Format("不能修改PageKey='{0}',数据库中已经存在该PageKey", sysPageEntity.PageKey));
                }
                else
                {
                    sysPageEntity.PageID = para.PageId;
                    sysPageEntity.Version = para.Version;
                    sysPageEntity.Author = para.Author;
                    //sysPageEntity.CustomerID = CurrentUserInfo.ClientID;
                    sysPageEntity.IsRebuild = 1;  //修改过。就标识为1
                    sysPageBLL.Update(sysPageEntity);
                    rd.PageId = para.PageId;
                }
            }
            return rd;
        }
        /// <summary>
        /// 对Json体反序列化成对象
        /// </summary>
        /// <param name="PageId"></param>
        public void JsonValveDeserializeDoc(string PageJson, SysPageEntity sysPageEntity)
        {
            string jsonValue = PageJson;
            var tempdic = jsonValue.DeserializeJSONTo<Dictionary<string, object>>(); //将Json体反序列化成Dictionary对象
            sysPageEntity.PageKey = tempdic["pageKey"].ToString();
            sysPageEntity.Title = tempdic["title"].ToString();
            sysPageEntity.ModuleName = tempdic["pageDes"].ToString();//显示的是ModuleName
            sysPageEntity.IsEntrance = Convert.ToInt32(tempdic["isEntry"]);
            sysPageEntity.URLTemplate = tempdic["urlTemplete"].ToString();
            sysPageEntity.JsonValue = PageJson;
            sysPageEntity.PageCode = tempdic["pageCode"].ToString();
            sysPageEntity.IsAuth = Convert.ToInt32(tempdic["NeedAuth"]);
            sysPageEntity.DefaultHtml = tempdic["defaultHtml"].ToString();
            sysPageEntity.CustomerID = tempdic["customerId"].ToString();
        }
    }
}