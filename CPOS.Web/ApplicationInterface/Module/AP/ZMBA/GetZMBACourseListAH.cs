using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.AP.ZMBA.Response;
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
    /// 获取正念商学院课程列表
    /// </summary>
    public class GetZMBACourseListAH : BaseActionHandler<EmptyRequestParameter, GetZMBACourseRD>
    {
        protected override GetZMBACourseRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            //基础数据初始化
          GetZMBACourseRD ZMBACourseRD = new GetZMBACourseRD();
            T_ZMBA_CourseBLL bll = new T_ZMBA_CourseBLL(this.CurrentUserInfo);
            List<T_ZMBA_CourseEntity> ZMBACourseList = new List<T_ZMBA_CourseEntity>();

            //查询条件初始化
            T_ZMBA_CourseEntity pT_ZMBA_CourseEntity = new T_ZMBA_CourseEntity();
            OrderBy[] pOrderBy = new OrderBy[1];
            pOrderBy[0] = new OrderBy(){ FieldName = "DisplayIndex",Direction = OrderByDirections.Asc};

            //执行查询
            ZMBACourseList = bll.QueryByEntity(pT_ZMBA_CourseEntity, pOrderBy).ToList();

            ZMBACourseRD.ZMBACourseList = ZMBACourseList;

            return ZMBACourseRD;

        }
 

    }
}