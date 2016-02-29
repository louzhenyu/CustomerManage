using JIT.Utility.DataAccess.Query;
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
    /// 插入正念商学院课程访问记录
    /// </summary>
    public class SetZMBACourseDetailReadLogAH : BaseActionHandler<SetZMBACourseDetailReadLogRP,EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetZMBACourseDetailReadLogRP> pRequest)
        {
            

            //基础数据初始化
            var para = pRequest.Parameters;
            T_ZMBA_CourseDetailReadLogBLL bll = new T_ZMBA_CourseDetailReadLogBLL(this.CurrentUserInfo);
            EmptyResponseData data = new EmptyResponseData();

            //插入数据初始化
            T_ZMBA_CourseDetailReadLogEntity ZMBACourseDetailReadLogEntity = new T_ZMBA_CourseDetailReadLogEntity() 
            {
                CourseId = new Guid(para.CourseId),
                CourseDetailId = new Guid(para.CourseDetailId),
                Name = para.Name,
                CustomerId = pRequest.CustomerID

            };

            bll.Create(ZMBACourseDetailReadLogEntity);

            return data;

        }
    }
}