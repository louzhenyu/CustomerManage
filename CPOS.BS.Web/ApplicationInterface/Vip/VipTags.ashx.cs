using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// VipTags 的摘要说明
    /// </summary>
    public class VipTags : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "GetTagTypeAndTags":
                    rst = GetTagTypeAndTags(pRequest);
                    break;
                case "SetVipTags":  //保存会员标签
                    rst = SetVipTags(pRequest);
                    break;

                case "SaveTagsType":  //更改促销分组
                    rst = SaveTagsType(pRequest);
                    break;
                case "DeleteTagsType":  //更改促销分组
                    rst = DeleteTagsType(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public string DeleteTagsType(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<DeleteTagsTypeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new DeleteTagsTypeRD();//返回值

            var TypeId = rp.Parameters.TypeId;
            if (string.IsNullOrEmpty(TypeId))
            {
                throw new APIException("缺少参数【TypeId】或参数值为空") { ErrorCode = 135 };
            }
            TagsTypeBLL TagsTypeBLL = new TagsTypeBLL(loggingSessionInfo);
            TagsTypeEntity TagsTypeEn = TagsTypeBLL.GetByID(TypeId);
            if (TagsTypeEn == null)
            {
                throw new APIException("没有找到对应的标签类型") { ErrorCode = 135 };
            }
           var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //查看标签是否已经被使用
          DataSet ds= TagsTypeBLL.HasUse(TypeId);
          if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                   rsp.ResultCode = 310;
                        rsp.Message = "该标签类型下面的标签已经被使用";
                        return rsp.ToJSON();
            }

            //虚拟删除标签类型和下面的标签
          TagsTypeBLL.DeleteTagsType(TypeId);
          

            return rsp.ToJSON();
        }


        #region  保存标签类型SaveTagsType
        public string SaveTagsType(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveTagsTypeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SaveTagsTypeRD();//返回值

            var _TagsTypeInfo = rp.Parameters.TagsTypeInfo;
            if (string.IsNullOrEmpty(_TagsTypeInfo.TypeName))
            {
                throw new APIException("缺少参数【TypeName】或参数值为空") { ErrorCode = 135 };
            }
            TagsTypeEntity TagsTypeEn = new TagsTypeEntity();
            bool isNew = false;
            if (string.IsNullOrEmpty(_TagsTypeInfo.TypeId))
            {
                _TagsTypeInfo.TypeId = Guid.NewGuid().ToString();
                isNew = true;
            }
            TagsTypeEn.TypeId = _TagsTypeInfo.TypeId;
            TagsTypeEn.TypeName = _TagsTypeInfo.TypeName;

            TagsTypeEn.LastUpdateBy = loggingSessionInfo.UserID;
            TagsTypeEn.LastUpdateTime = DateTime.Now;
            TagsTypeEn.IsDelete = 0;
            string error = "";
            //service.SaveProp(propInfo, ref error);
            TagsTypeBLL TagsTypeBLL = new TagsTypeBLL(loggingSessionInfo);
            if (isNew)
            {
                TagsTypeEn.CreateBy = loggingSessionInfo.UserID;
                TagsTypeEn.CreateTime = DateTime.Now;
                TagsTypeBLL.Create(TagsTypeEn);
            }
            else
            {
                TagsTypeBLL.Update(TagsTypeEn, false);
            }
            TagsBLL TagsBLL = new TagsBLL(loggingSessionInfo);
            if (!isNew)//不是新的
            {
                string propIds = "";
                foreach (var itemInfo in _TagsTypeInfo.TagsList)//数组，更新数据
                {
                    if (!string.IsNullOrEmpty(itemInfo.TagsId))
                    {
                        if (propIds != "")
                        {
                            propIds += ",";
                        }
                        propIds += "'" + itemInfo.TagsId + "'";
                    }
                }
                //删除不在这个里面的
                if (!string.IsNullOrEmpty(propIds))
                {
                    TagsBLL.DeleteByIds(propIds, TagsTypeEn);
                }
            }


            foreach (var itemInfo in _TagsTypeInfo.TagsList)//数组，更新数据
            {
                TagsEntity TagsEn = new TagsEntity();

                TagsEn.TagsName = itemInfo.TagsName;
                TagsEn.TagsDesc = itemInfo.TagsName;
                TagsEn.LastUpdateBy = rp.UserID;
                TagsEn.LastUpdateTime = DateTime.Now;
                TagsEn.IsDelete = 0;
                TagsEn.CustomerId = loggingSessionInfo.ClientID;
                TagsEn.TypeId = TagsTypeEn.TypeId;

                if (string.IsNullOrEmpty(itemInfo.TagsId))
                {
                    TagsEn.TagsId = Guid.NewGuid().ToString();
                    TagsEn.CreateBy = rp.UserID;
                    TagsEn.CreateTime = DateTime.Now;
                    TagsBLL.Create(TagsEn);
                }
                else
                {
                    TagsEn.TagsId = itemInfo.TagsId;
                    TagsBLL.Update(TagsEn, false);
                }
            }



            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        /// <summary>
        /// 销售（服务）订单
        /// </summary>
        public string GetTagTypeAndTags(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetTagTypeAndTagsRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            //  LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            TagsTypeBLL bll = new TagsTypeBLL(loggingSessionInfo);

            var rd = new GetTagTypeAndTagsRD();
            var ds = bll.GetAll2(pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);
            List<TagsTypeEntity> tagTypeList = new List<TagsTypeEntity>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagTypeList = DataTableToObject.ConvertToList<TagsTypeEntity>(ds.Tables[1]);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
            }
            //标签
            TagsBLL _TagsBLL = new TagsBLL(loggingSessionInfo);
            List<TagsTypeInfo> ls = new List<TagsTypeInfo>();
            if (tagTypeList != null && tagTypeList.Count() > 0)
            {
                foreach (TagsTypeEntity en in tagTypeList)
                {
                    TagsTypeInfo _TagsTypeInfo = new TagsTypeInfo();
                    _TagsTypeInfo.TypeId = en.TypeId;
                    _TagsTypeInfo.TypeName = en.TypeName;
                    var ds2 = _TagsBLL.GetTagsList(en.TypeId, loggingSessionInfo.ClientID);  //要根据本地的customerid
                    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        _TagsTypeInfo.TagsList = DataTableToObject.ConvertToList<TagsInfo>(ds2.Tables[0]);//直接根据所需要的字段反序列化
                    }
                    ls.Add(_TagsTypeInfo);
                }
            }


            rd.TagTypesAndTags = ls;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        #region  保存会员标签
        public string SetVipTags(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetVipTagsRP>>();
            //  LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SetVipTagsRD();//返回值

            if (string.IsNullOrEmpty(rp.Parameters.VIPID))
            {
                throw new APIException("缺少参数【VIPID】或参数值为空") { ErrorCode = 135 };
            }

            VipTagsMappingBLL _VipTagsMappingBLL = new VipTagsMappingBLL(loggingSessionInfo);
            TagsBLL _TagsBLL = new TagsBLL(loggingSessionInfo);

            //删除之前该会员的标签
            //ItemCategoryMappingBLL itemCategoryMappingBLL=new itemCategoryMappingBLL()
            //   itemCategoryMappingBLL.DeleteByItemID(rp.Parameters.VIPID);
            _VipTagsMappingBLL.DeleteByVipID(rp.Parameters.VIPID);
            //这里不应该删除之前的促销分组，而应该根据商品的id和促销分组的id找一找，如果有isdelete=0的，就不要加，没有就加

            foreach (var tagsInfo in rp.Parameters.IdentityTagsList)
            {
                //如果该标签的id为空//创建一条记录
                if (string.IsNullOrEmpty(tagsInfo.TagsId))
                {
                    TagsEntity en = new TagsEntity();
                    en.TagsId = Guid.NewGuid().ToString();
                    en.TagsName = tagsInfo.TagsName;
                    en.TagsDesc = tagsInfo.TagsName;
                    en.CreateTime = DateTime.Now;
                    en.CreateBy = rp.UserID;
                    en.LastUpdateTime = DateTime.Now;
                    en.LastUpdateBy = rp.UserID;
                    en.IsDelete = 0;
                    en.CustomerId = rp.CustomerID;
                    //  en.TypeId
                    _TagsBLL.Create(en);
                    tagsInfo.TagsId = en.TagsId;//
                }

                //创建vip的年龄标签
                VipTagsMappingEntity TagsEn = new VipTagsMappingEntity();
                TagsEn.MappingId = Guid.NewGuid().ToString();
                TagsEn.VipId = rp.Parameters.VIPID;
                TagsEn.TagsId = tagsInfo.TagsId;//标签的id
                TagsEn.CreateTime = DateTime.Now;
                TagsEn.CreateBy = rp.UserID;
                TagsEn.LastUpdateTime = DateTime.Now;
                TagsEn.LastUpdateBy = rp.UserID;
                TagsEn.IsDelete = 0;
                _VipTagsMappingBLL.Create(TagsEn);

            }


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

    }





    public class TagsTypeInfo
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public List<TagsInfo> TagsList { get; set; }
    }

    public class TagsInfo
    {
        public string TagsId { get; set; }
        public string TagsName { get; set; }
        public string TagsDesc { get; set; }
        public string TagsFormula { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }

    }

    public class SetVipTagsRP : IAPIRequestParameter
    {

        //标签
        public IList<TagsInfo> IdentityTagsList { get; set; }
        public string VIPID { get; set; }


        public void Validate()
        {
        }
    }
    public class SetVipTagsRD : IAPIResponseData
    {

    }


    public class SaveTagsTypeRP : IAPIRequestParameter
    {

        //标签
        public TagsTypeInfo TagsTypeInfo { get; set; }
        // public string VIPID { get; set; }


        public void Validate()
        {
        }
    }
    public class SaveTagsTypeRD : IAPIResponseData
    {

    }

    public class GetTagTypeAndTagsRP : IAPIRequestParameter
    {


        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }

        public void Validate()
        {
        }
    }
    public class GetTagTypeAndTagsRD : IAPIResponseData
    {
        public List<TagsTypeInfo> TagTypesAndTags { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }



    public class DeleteTagsTypeRP : IAPIRequestParameter
    {

        public string TypeId { get; set; }

        public void Validate()
        {
        }
    }
    public class DeleteTagsTypeRD : IAPIResponseData
    {


    }

}