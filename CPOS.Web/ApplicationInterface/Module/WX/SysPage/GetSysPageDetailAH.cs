/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/09 11:22
 * Description	:获取模板页明细
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
    public class GetSysPageDetailAH : BaseActionHandler<GetSysPageDetailRP, GetSysPageDetailRD>
    {
        protected override GetSysPageDetailRD ProcessRequest(APIRequest<GetSysPageDetailRP> pRequest)
        {
            GetSysPageDetailRD rd = new GetSysPageDetailRD();
            var userInfo = this.CurrentUserInfo;
            userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["APConn"];
            SysPageBLL PageBLL = new SysPageBLL(userInfo);

            SysModulePageMappingBLL ModulePageMappingBLL = new SysModulePageMappingBLL(userInfo);

            var para = pRequest.Parameters;
            #region 1.根据PageID查找是否存在
            var entity = PageBLL.GetByID(para.PageId);
            #endregion
            #region 2.定义错误码
            const int ERROE_NO_EXISTS = 301;
            #endregion
            if (entity == null)
            {
                throw new APIException(string.Format("不存在PageId为{0}的对象", para.PageId)) { ErrorCode = ERROE_NO_EXISTS };
            }
            else   //获取模板页明细
            {
                var pageInfolist = new List<JIT.CPOS.DTO.Module.WeiXin.SysPage.Response.GetSysPageDetailRD.PageInfoList> { };
                JIT.CPOS.DTO.Module.WeiXin.SysPage.Response.GetSysPageDetailRD.PageInfoList list = new JIT.CPOS.DTO.Module.WeiXin.SysPage.Response.GetSysPageDetailRD.PageInfoList();
                list.PageId = entity.PageID.ToString();
                list.Author = entity.Author;
                list.Version = entity.Version;
                list.PageJson = entity.JsonValue;
                list.ModuleName = entity.ModuleName;
                pageInfolist.Add(list);

                var PageMappinglist = new List<GetSysPageDetailRD.ModulePageMappingList> { };

                SysModulePageMappingEntity en = new SysModulePageMappingEntity();
                en.PageID = Guid.Parse(para.PageId);
                var entityModulePageList = ModulePageMappingBLL.QueryByEntity(en, null);
                if (entityModulePageList != null)
                {
                    var temp = entityModulePageList.Select(t => new GetSysPageDetailRD.ModulePageMappingList
                    {
                        VocaVerMappingID = t.VocaVerMappingID.ToString()
                    }).ToArray();
                    rd.ModulePageMappingInfo = temp;
                }
                rd.PageInfo = pageInfolist.ToArray();
            }
            return rd;
        }
    }
}
