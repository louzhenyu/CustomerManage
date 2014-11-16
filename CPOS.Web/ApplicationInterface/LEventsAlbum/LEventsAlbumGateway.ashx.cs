using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.LEventsAlbum
{
    /// <summary>
    /// LEventsAlbumGateway 的摘要说明
    /// </summary>
    public class LEventsAlbumGateway : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetEventsAlbumList":   //获取微相册视频 Add by changjian.tian 2014-06-03
                    rst = GetEventsAlbumList(pRequest);
                    break;
                case "GetLEventsAlbumType":
                    rst = GetLEventsAlbumType(pRequest);//获取相册分类 Add by changjian.tian 2014-007-3
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }
        /// <summary>
        /// 获取相册分类
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventsAlbumType(string pRequest)
        {
            LEventsAlbumTypeRD RD = new LEventsAlbumTypeRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<LEventsAlbumTypeRP>>();
                var rsp = new SuccessResponse<IAPIResponseData>();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                if (string.IsNullOrWhiteSpace(rp.CustomerID))
                {
                    rsp.ResultCode = 301;
                    rsp.Message = "客户ID不能为空！";
                }
                else
                {
                    LEventsAlbumTypeBLL bll = new LEventsAlbumTypeBLL(loggingSessionInfo);
                    var list = new List<LEventsAlbumListType> { };
                    var orderBy = new OrderBy[]{
                new OrderBy{ FieldName = "DisplayIndex", Direction=OrderByDirections.Asc }};
                    var entity = bll.QueryByEntity(new LEventsAlbumTypeEntity { CustomerId = rp.CustomerID }, orderBy);
                    if (entity != null && entity.Length > 0)
                    {
                        for (int i = 0; i < entity.Length; i++)
                        {
                            LEventsAlbumListType type = new LEventsAlbumListType();
                            type.ModuleName = entity[i].ModuleName;
                            type.ModuleType = entity[i].ModuleType;
                            list.Add(type);
                        }
                    }
                    RD.EventsAlbumListType = list.ToArray();
                    rsp = new SuccessResponse<IAPIResponseData>(RD);
                }
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        /// <summary>
        /// 获取微相册视频
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetEventsAlbumList(string pRequest)
        {
            EventsAlbumListRD rd = new EventsAlbumListRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<EventsAlbumListRP>>();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                LEventsAlbumBLL bll = new LEventsAlbumBLL(loggingSessionInfo);
                var list = new List<EventsAlbumList> { };
                DataSet ds = bll.GetLEventsAlbumByType(rp.Parameters.ModuleType);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    rd.ModuleName = ds.Tables[0].Rows[0]["ModuleName"].ToString();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        EventsAlbumList Events = new EventsAlbumList();
                        Events.ImageUrl = row["ImageUrl"].ToString();
                        Events.Title = row["Title"].ToString();
                        Events.VideoURL = row["VideoURL"].ToString();
                        Events.Intro = row["Intro"].ToString();

                        Events.BigImageUrl = row["BigImageUrl"].ToString();
                        Events.BigImageTitle = row["BigImageTitle"].ToString();
                        Events.BigImageDescription = row["BigImageDescription"].ToString();
                        list.Add(Events);
                    }
                }
                rd.EventsAlbumList = list.ToArray();
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
    }
    #region 获取视屏分类
    public class EventsAlbumListRD : IAPIResponseData
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        public EventsAlbumList[] EventsAlbumList { get; set; }

    }

    public class EventsAlbumList
    {
        /// <summary>
        /// 首页图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频URL
        /// </summary>
        public string VideoURL { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Intro { get; set; }

        public string BigImageUrl { get; set; } //大图Url

        public string BigImageTitle { get; set; } //大图Title

        public string BigImageDescription { get; set; } //大图描述
    }
    public class LEventsAlbumPhoto
    {
        /// <summary>
        /// 主标识
        /// </summary>
        public string AlbumId { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }
    }

    public class EventsAlbumListRP : IAPIRequestParameter
    {
        const int NULL_MODULETYPE = 301;
        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleType { get; set; }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.ModuleType))
            {
                throw new APIException("模块类型不能为空!") { ErrorCode = NULL_MODULETYPE };
            }
        }
    }
    #endregion

    public class LEventsAlbumTypeRD : IAPIResponseData
    {
        public LEventsAlbumListType[] EventsAlbumListType { get; set; }
    }
    public class LEventsAlbumListType
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleType { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
    }
    public class LEventsAlbumTypeRP : IAPIRequestParameter
    {
        public string CustomerId { get; set; }
        public void Validate()
        {

        }
    }

}