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
    /// InnerGoupNews 的摘要说明
    /// </summary>
    public class InnerGoupNews : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "GetUnitByUser":
                    rst = GetUnitByUser(pRequest);
                    break;
                case "GetDeptList":  //获取部门 
                    rst = GetDeptList(pRequest);
                    break;
                case "SaveInnerGroupNews":  //更改促销分组
                    rst = SaveInnerGroupNews(pRequest);
                    break;
                case "GetInnerGroupNewsList":  //更改促销分组
                    rst = GetInnerGroupNewsList(pRequest);
                    break;
                case "GetUserList":  //获取员工
                    rst = GetUserList(pRequest);
                    break;
                case "DeleteInnerGroupNews":  //更改促销分组
                    rst = DeleteInnerGroupNews(pRequest);
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
        public string DeleteInnerGroupNews(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<DeleteInnerGroupNewsRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new DeleteInnerGroupNewsRD();//返回值

            var TypeId = rp.Parameters.GroupNewsId;
            if (string.IsNullOrEmpty(TypeId))
            {
                throw new APIException("缺少参数【GroupNewsId】或参数值为空") { ErrorCode = 135 };
            }
            InnerGroupNewsBLL _InnerGroupNewsBLL = new InnerGroupNewsBLL(loggingSessionInfo);
       //     TagsTypeEntity TagsTypeEn = TagsTypeBLL.GetByID(TypeId);
          
            //虚拟删除标签类型和下面的标签         
            _InnerGroupNewsBLL.DeleteInnerGroupNews(rp.Parameters.GroupNewsId);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        /// <summary>
        /// 获取当前用户下的门店
        /// </summary>
        public string GetUnitByUser(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitByUserRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;
            //  LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            UnitService unitService = new UnitService(loggingSessionInfo);

            var rd = new GetUnitByUserRD();
            IList<UnitInfo> units;

            //现在需要根据用户的userID和customerID来取他权限下面的角色
            units = unitService.GetUnitByUser(loggingSessionInfo.ClientID, loggingSessionInfo.UserID);//获取当前登录人的门店
            //门店list

            List<Unit_Info> ls = new List<Unit_Info>();
            if (units != null && units.Count() > 0)
            {
                foreach (UnitInfo en in units)
                {
                    Unit_Info _Unit_Info = new Unit_Info();
                    _Unit_Info.UnitID = en.Id;
                    _Unit_Info.UnitName = en.Name;
                    //  _Unit_Info.TreeLevel = e;
                    _Unit_Info.ParentUnitID = en.Parent_Unit_Id;
                    ls.Add(_Unit_Info);
                }
            }


            rd.UnitList = ls;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取当前用户下的门店
        /// </summary>
        public string GetDeptList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitByUserRP>>();//不需要参数
            string userId = rp.UserID;
            string customerId = rp.CustomerID;
            //  LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            T_DeptBLL T_DeptBLL = new T_DeptBLL(loggingSessionInfo);

            var rd = new GetDeptListRD();
            T_DeptEntity T_DeptEntity = new T_DeptEntity();
            T_DeptEntity.CustomerID = loggingSessionInfo.ClientID;
            T_DeptEntity.IsDelete = 0;
            //T_DeptEntity.ShowInApp=1;后台全部显示出来？
            IList<T_DeptEntity> T_DeptList = T_DeptBLL.QueryByEntity(T_DeptEntity, null);
            rd.DeptList = T_DeptList.ToList();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #region  保存内部消息SaveInnerGroupNews
        public string SaveInnerGroupNews(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveInnerGroupNewsRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new SaveInnerGroupNewsRD();//返回值          
            var _InnerGroupNewsInfo = rp.Parameters.InnerGroupNewsInfo;
            if (string.IsNullOrEmpty(_InnerGroupNewsInfo.Title))
            {
                throw new APIException("缺少参数【Title】或参数值为空") { ErrorCode = 135 };
            }
            if (string.IsNullOrEmpty(_InnerGroupNewsInfo.Text))
            {
                throw new APIException("缺少参数【Text】或参数值为空") { ErrorCode = 135 };
            }
            if (rp.Parameters.NewsUserList == null || rp.Parameters.NewsUserList.Count == null)
            {
                throw new APIException("请选择要发送给信息的员工") { ErrorCode = 135 };
            }
            InnerGroupNewsBLL _InnerGroupNewsBLL = new InnerGroupNewsBLL(loggingSessionInfo);
            // _InnerGroupNewsInfo.DeptID = _TagsTypeInfo.TypeName;
            //_InnerGroupNewsInfo.Title = _TagsTypeInfo.TypeName;
            //_InnerGroupNewsInfo.Text = _TagsTypeInfo.TypeName;
            _InnerGroupNewsInfo.CustomerID = loggingSessionInfo.ClientID;
            _InnerGroupNewsInfo.LastUpdateBy = loggingSessionInfo.UserID;
            _InnerGroupNewsInfo.LastUpdateTime = DateTime.Now;
            _InnerGroupNewsInfo.IsDelete = 0;
            if (string.IsNullOrEmpty(_InnerGroupNewsInfo.GroupNewsId))
            {
                _InnerGroupNewsInfo.GroupNewsId = Guid.NewGuid().ToString();
                _InnerGroupNewsInfo.CreateBy = loggingSessionInfo.UserID;
                _InnerGroupNewsInfo.CreateTime = DateTime.Now;
                _InnerGroupNewsBLL.Create(_InnerGroupNewsInfo);
            }
            else
            {
                _InnerGroupNewsBLL.Update(_InnerGroupNewsInfo, null, false);//是否更新空值
            }
            //关联会员
            NewsUserMappingBLL _NewsUserMappingBLL = new NewsUserMappingBLL(loggingSessionInfo);
            foreach (var itemInfo in rp.Parameters.NewsUserList)//数组，更新数据
            {
                //TagsEntity TagsEn = new TagsEntity();
                // itemInfo.UserID  //已有
                itemInfo.MappingID = Guid.NewGuid().ToString();
                itemInfo.GroupNewsID = _InnerGroupNewsInfo.GroupNewsId;
                itemInfo.LastUpdateBy = rp.UserID;
                itemInfo.LastUpdateTime = DateTime.Now;
                itemInfo.IsDelete = 0;
                itemInfo.CustomerId = loggingSessionInfo.ClientID;
                itemInfo.HasRead = 0;
                itemInfo.CreateBy = rp.UserID;
                itemInfo.CreateTime = DateTime.Now;
                _NewsUserMappingBLL.Create(itemInfo);

            }



            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion


        /// <summary>
        /// 内部消息
        /// </summary>
        public string GetInnerGroupNewsList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetInnerGroupNewsListRP>>();//不需要参数
            //string userId = rp.UserID;
            //string customerId = rp.CustomerID;
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            //  LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);

            var rd = new GetInnerGroupNewsListRD();
            var ds = bll.GetInnerGroupNewsList(pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType, "", loggingSessionInfo.ClientID,"");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.InnerGroupNewsList = DataTableToObject.ConvertToList<InnerGroupNewsInfo>(ds.Tables[1]);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        /// <summary>
        /// 获取员工信息
        /// </summary>
        public string GetUserList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUserListRP>>();//不需要参数
            //string userId = rp.UserID;
            //string customerId = rp.CustomerID;
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            InnerGroupNewsBLL bll = new InnerGroupNewsBLL(loggingSessionInfo);
            var userService = new cUserService(loggingSessionInfo);

            string PhoneList = "";
            if(!string.IsNullOrEmpty(rp.Parameters.PhoneList))
            {
                string[] phones=rp.Parameters.PhoneList.Split(new char[]{','});
                string propIds = "";
                foreach (var itemInfo in phones)//数组，更新数据
                {
                    if (!string.IsNullOrEmpty(itemInfo))
                    {
                        if (propIds != "")
                        {
                            propIds += ",";
                        }
                        propIds += "'" + itemInfo + "'";
                    }
                }
                PhoneList = propIds;
            }

           var rd = new GetUserListRD();
            var ds = userService.GetUserList(pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType, loggingSessionInfo.UserID, loggingSessionInfo.ClientID,PhoneList
                ,rp.Parameters.UnitID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.UserList = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[1]);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

    }

    public class GetUnitByUserRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetUnitByUserRD : IAPIResponseData
    {
        public List<Unit_Info> UnitList { get; set; }

    }

    public class Unit_Info
    {

        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string ParentUnitID { get; set; }
        public string TreeLevel { get; set; }
    }


    public class GetDeptListRD : IAPIResponseData
    {
        public List<T_DeptEntity> DeptList { get; set; }
    }
    public class SaveInnerGroupNewsRP : IAPIRequestParameter
    {
        //标签
        public InnerGroupNewsEntity InnerGroupNewsInfo { get; set; }
        public List<NewsUserMappingEntity> NewsUserList { get; set; }


        public void Validate()
        {
        }
    }
    public class SaveInnerGroupNewsRD : IAPIResponseData
    {

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
        public string CreateByName { get; set; }
        

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



    public class GetUserListRP : IAPIRequestParameter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }


        public string PhoneList { get; set; }
        public string UnitID { get; set; }
        public void Validate()
        {
        }
    }
    public class GetUserListRD : IAPIResponseData
    {
        public List<UserInfo> UserList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }

    public class UserInfo
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string Phone { get; set; }
    }


    public class DeleteInnerGroupNewsRP : IAPIRequestParameter
    {

        public string GroupNewsId { get; set; }

        public void Validate()
        {
        }
    }
    public class DeleteInnerGroupNewsRD : IAPIResponseData
    {


    }

}