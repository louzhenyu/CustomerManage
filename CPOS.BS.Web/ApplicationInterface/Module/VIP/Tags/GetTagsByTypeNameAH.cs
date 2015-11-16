using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.Tags.Request;
using JIT.CPOS.DTO.Module.VIP.Tags.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.Tags
{
    public class GetTagsByTypeNameAH : BaseActionHandler<GetTagsByTypeNameRP, GetTagsByTypeNameRD>
    {
        protected override GetTagsByTypeNameRD ProcessRequest(DTO.Base.APIRequest<GetTagsByTypeNameRP> pRequest)
        {
            var rd = new GetTagsByTypeNameRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var tagsTypeBLL = new TagsTypeBLL(loggingSessionInfo);    //标签分类业务对象实例化
            var tagsBLL = new TagsBLL(loggingSessionInfo);            //标签业务对象实例化

            //获取标签类型对象
            var tagsTypeEntity = tagsTypeBLL.QueryByEntity(new TagsTypeEntity() { TypeName = para.TypeName }, null).FirstOrDefault();

            if (tagsTypeEntity != null)
            {
                //排序字段
                var orderBy = new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Asc } };
                //获取标签列表
                var tagsList = tagsBLL.QueryByEntity(new TagsEntity() { TypeId = tagsTypeEntity.TypeId }, orderBy);
                //返回数据转换
                rd.TagsList = tagsList.Select(t => new TagsInfo() { TagsID = t.TagsId, TagsName = t.TagsName }).ToList();
            }
            return rd;
        }
    }
}