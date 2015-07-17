using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.VipList.Request;
using JIT.CPOS.DTO.Module.VIP.VipList.Response;
using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.NewAppContent
{
    /// <summary>
    /// NewApp 的摘要说明
    /// </summary>
    public class NewApp : BaseGateway
    {
        #region 错误码
        private const int ERROR_USERID_NOTNULL = 801;        //USerID不能为空
        private const int ERROR_WDAMOUNT_TOOBIG = 802;     //日累计提现金额等能大于设置金额
        private const int ERROR_WDAMOUNT_NOTWDTIME = 803;  //超出提现次数限制
        #endregion
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetTagTypeAndTags":
                    rst = GetTagTypeAndTags(pRequest);
                    break;
                case "SetVipTags":  //保存会员标签
                    rst = SetVipTags(pRequest);
                    break;
                case "GetTUnit":
                    rst = GetTUnit(pRequest);
                    break;
                case "GetDeptList":     //获取部门   
                    rst = GetDeptList(pRequest);
                    break;
                case "GetInnerGroupNewsList":  //更改促销分组
                    rst = GetInnerGroupNewsList(pRequest);
                    break;
                case "GetGroupNewsByID":  //更改促销分组
                    rst = GetGroupNewsByID(pRequest);
                    break;
                
                    
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

        /// <summary>
        /// 获取当前用户下的门店
        /// </summary>
        public string GetDeptList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitByUserRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
         //   var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_DeptBLL T_DeptBLL = new T_DeptBLL(loggingSessionInfo);

            var rd = new GetDeptListRD();
            T_DeptEntity T_DeptEntity = new T_DeptEntity();
            T_DeptEntity.CustomerID = loggingSessionInfo.ClientID;
            T_DeptEntity.IsDelete = 0;
            //T_DeptEntity.ShowInApp=1;后台全部显示出来？
            IList<T_DeptEntity> T_DeptList = T_DeptBLL.QueryByEntity(T_DeptEntity, null);
            rd.DeptList = T_DeptList.Where(p=>p.ShowInApp==1).ToList();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取快递公司
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetTUnit(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetTUnitRP>>();//不需要参数
          //  var type = HttpContext.Current.Request.Params["Type"];
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var rd = new GetTUnitRD();
            var result = new UnitService(loggingSessionInfo).GetUnitInfoListByTypeCode(rp.Parameters.Type);
            List<Unit_Info> ls=new  List<Unit_Info>();
            foreach (UnitInfo item in result)
            {
                Unit_Info en = new Unit_Info();
                en.unit_id = item.Id;
                en.unit_name = item.Name;
                ls.Add(en);
            }

            rd.UnitList = ls;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }



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

             LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
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
                    var ds2 = _TagsBLL.GetTagsList(en.TypeId, rp.CustomerID);
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
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
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

        /// 内部消息
        /// </summary>
        public string GetInnerGroupNewsList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetInnerGroupNewsListRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

          LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
           // var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);

            var rd = new GetInnerGroupNewsListRD();
            var ds = bll.GetInnerGroupNewsList(pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType, rp.UserID, loggingSessionInfo.ClientID,rp.Parameters.DeptID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.InnerGroupNewsList = DataTableToObject.ConvertToList<InnerGroupNewsInfo>(ds.Tables[1]);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[1].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// 内部消息
        /// </summary>
        public string GetGroupNewsByID(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetGroupNewsByIDRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;
            if (string.IsNullOrEmpty(rp.Parameters.GroupNewsID))
            {
                throw new APIException("缺少参数【GroupNewsID】或参数值为空") { ErrorCode = 135 };
            }

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            // var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);

            var rd = new GetGroupNewsByIDRD();
            var ds = bll.GetByID(rp.Parameters.GroupNewsID);

            rd.InnerGroupNewsInfo = ds;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        #region  保存产品试用或渠道代理信息
        public string SaveAgentCustomer(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveAgentCustomerRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var rd = new SetVipTagsRD();//返回值

            if (rp.Parameters.AgentCustomerInfo==null)
            {
                throw new APIException("缺少参数【AgentCustomerInfo】或参数值为空") { ErrorCode = 135 };
            }

            if (string.IsNullOrEmpty(rp.Parameters.AgentCustomerInfo.AgentName))
            {
                throw new APIException("缺少参数【AgentName】或参数值为空") { ErrorCode = 135 };
            }


            AgentCustomerBLL _AgentCustomerBLL = new AgentCustomerBLL(loggingSessionInfo);


            var AgentCustomerInfo = rp.Parameters.AgentCustomerInfo;
                //如果该标签的id为空//创建一条记录
            if (string.IsNullOrEmpty(AgentCustomerInfo.AgentID))
            {
                //TagsEntity en = new TagsEntity();
                AgentCustomerInfo.AgentID = Guid.NewGuid().ToString();
                AgentCustomerInfo.CreateTime = DateTime.Now;
                AgentCustomerInfo.CreateBy = rp.UserID;
                AgentCustomerInfo.LastUpdateTime = DateTime.Now;
                AgentCustomerInfo.LastUpdateBy = rp.UserID;
                AgentCustomerInfo.IsDelete = 0;
                AgentCustomerInfo.CustomerID = rp.CustomerID;
                _AgentCustomerBLL.Create(AgentCustomerInfo);
            }
            else {
             
                AgentCustomerInfo.LastUpdateTime = DateTime.Now;
                AgentCustomerInfo.LastUpdateBy = rp.UserID;
                AgentCustomerInfo.IsDelete = 0;
                AgentCustomerInfo.CustomerID = rp.CustomerID;
                _AgentCustomerBLL.Update(AgentCustomerInfo, null, false);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion



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


  



    public class GetTUnitRP : IAPIRequestParameter
    {

        //标签
        public string Type { get; set; }
        public int page { get; set; }
        public int start { get; set; }
        public int      limit { get; set; }


        public void Validate()
        {
        }
        




    }
    public class GetTUnitRD : IAPIResponseData
    {
        public IList<Unit_Info> UnitList { get; set; }
    }
    public class Unit_Info : IAPIResponseData
    {
        public string unit_id { get; set; }
        public string unit_name { get; set; }
    }

    public class GetDeptListRD : IAPIResponseData
    {
        public List<T_DeptEntity> DeptList { get; set; }
    }
    public class GetUnitByUserRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }


    public class InnerGroupNewsInfo
    {

        public string GroupNewsId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateTimeStr { get; set; }
        public string CreateBy { get; set; }
        public int spanNow { get; set; }
        public string spanNowStr { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public int NewsUserCount { get; set; }
        public int ReadUserCount { get; set; }

    }




    public class GetInnerGroupNewsListRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }

        public string DeptID { get; set; }
        public void Validate()
        {
        }
    }
    public class GetInnerGroupNewsListRD : IAPIResponseData
    {
        public List<InnerGroupNewsInfo> InnerGroupNewsList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }



    public class GetGroupNewsByIDRP : IAPIRequestParameter
    {

        public string GroupNewsID { get; set; }
        public void Validate()
        {
        }
    }
    public class GetGroupNewsByIDRD : IAPIResponseData
    {
        public InnerGroupNewsEntity  InnerGroupNewsInfo { get; set; }
      
    }

}