using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.AP.ZMBA.Response;
using JIT.CPOS.DTO.Module.AP.ZMBA.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.AP.ZMBA
{
    /// <summary>
    /// 获取正念商学院内容列表
    /// </summary>
    public class GetZMBACourseDetailListAH : BaseActionHandler<GetZMBACourseDetailListRP, GetZMBACourseDetailListRD>
    {
        protected override GetZMBACourseDetailListRD ProcessRequest(DTO.Base.APIRequest<GetZMBACourseDetailListRP> pRequest)
        {
            //基础数据初始化
            GetZMBACourseDetailListRD ZMBACourseDetailListRD = new GetZMBACourseDetailListRD();
            T_ZMBA_CourseDetailBLL bll = new T_ZMBA_CourseDetailBLL(this.CurrentUserInfo);

            //查询条件初始化
            T_ZMBA_CourseDetailEntity pT_ZMBA_CourseDetailEntity = new T_ZMBA_CourseDetailEntity()
            {
                CourseId = new Guid(pRequest.Parameters.CourseId)
            };
            OrderBy[] pOrderBy = new OrderBy[1];
            pOrderBy[0] = new OrderBy() { FieldName = "DisplayIndex", Direction = OrderByDirections.Asc };

            //执行查询
            var ZMBACourseDetailResult = bll.PagedQueryByEntity(pT_ZMBA_CourseDetailEntity, pOrderBy, pRequest.Parameters.PageSize, pRequest.Parameters.PageIndex);

            ZMBACourseDetailListRD.TotalCount = ZMBACourseDetailResult.RowCount;
            ZMBACourseDetailListRD.TotalPageCount = ZMBACourseDetailResult.PageCount;
            ZMBACourseDetailListRD.ZMBACourseDetailList = ZMBACourseDetailResult.Entities.ToList();

            return ZMBACourseDetailListRD;
        } 
   }
}