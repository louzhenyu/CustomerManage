using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Data;
using JIT.CPOS.Web.ApplicationInterface.Util.SMS;
using JIT.Utility;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.ApplicationInterface.NwNews
{
    /// <summary>
    /// NwNewsGateway 的摘要说明
    /// </summary>
    public class NwNewsGateway : BaseGateway
    {
        #region Private Obj
        private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo { get { return new SessionManager().CurrentUserLoginInfo; } }
        //private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo = new Entity.LoggingSessionInfo()
        //{
        //    CurrentUser = new Entity.User.UserInfo() { User_Id = "39E7EFBB-B28A-4B02-A396-B4BEB0EC2F82", customer_id = "ef10dfb65a004b88a6d3f547366c56b7" },
        //    CurrentLoggingManager = new Entity.LoggingManager() { Connection_String = "user id=dev;password=jit!2014;data source=112.124.68.147;database=cpos_bs_jit;", Customer_Id = "ef10dfb65a004b88a6d3f547366c56b7", Customer_Code = "Lzlj", Customer_Name = "泸州老窖", User_Id = "null},'CurrentLoggingManager':{'Customer_Id':'ef10dfb65a004b88a6d3f547366c56b7','Customer_Code':'Lzlj','Customer_Name':'泸州老窖','User_Id':'7c292994c45143028cbf0b60c9555aec", User_Name = "管理员" },
        //    ClientID = "ef10dfb65a004b88a6d3f547366c56b7"
        //};
        private LNewsBLL PrivateLNewsBLL { get { return new LNewsBLL(PrivateLoggingSessionInfo); } }
        private LNewsTypeBLL PrivateLNewsTypeBLL { get { return new LNewsTypeBLL(PrivateLoggingSessionInfo); } }
        private EclubMicroNumberBLL PrivateEclubMicroNumberBLL { get { return new EclubMicroNumberBLL(PrivateLoggingSessionInfo); } }
        private EclubMicroTypeBLL PrivateEclubMicroTypeBLL { get { return new EclubMicroTypeBLL(PrivateLoggingSessionInfo); } }
        private LNumberTypeMappingBLL PrivateLNumberTypeMappingBLL { get { return new LNumberTypeMappingBLL(PrivateLoggingSessionInfo); } }
        private LNewsMicroMappingBLL PrivateLNewsMicroMappingBLL { get { return new LNewsMicroMappingBLL(PrivateLoggingSessionInfo); } }

        #endregion

        #region 接口请求及响应数据结构
        #region 1.1 获取分类列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNewsTypeListRP : EmptyRequestParameter
        {

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNewsTypeListRD : IAPIResponseData
        {
            /// <summary>
            /// 返回的类型列表
            /// </summary>
            public DataTable TypeList { get; set; }
        }
        #endregion

        #region 1.2 获取资讯列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNewsListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();                
            }
            /// <summary>
            /// 分类标识ID
            /// </summary>
            public string NewsTypeId { get; set; }
            /// <summary>
            /// 搜索关键字
            /// </summary>
            public string Keyword { get; set; }
            /// <summary>
            /// 起始日期
            /// </summary>
            public string StartDate { get; set; }
            /// <summary>
            /// 截止日期
            /// </summary>
            public string EndDate { get; set; }
            /// <summary>
            /// 排序字段：Ex: PublishTime【日期】 , NewsTypeName【标题】, BrowseCount【浏览数】, PraiseCount【赞数】, CollCount【分享数】
            /// </summary>
            public string SortField { get; set; }
            /// <summary>
            /// 排序方式：0 升序 1 降序
            /// </summary>
            public int SortOrder { get; set; }
            /// <summary>
            /// 当前页：默认从 0 
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 页大小：默认大小 15
            /// </summary>
            public int PageSize { get; set; }
            /// <summary>
            /// 是否取未关联的资讯（资讯微刊关联模块使用）
            /// </summary>
            public bool IsMappingNews { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNewsListRD : IAPIResponseData
        {
            /// <summary>
            /// 分页信息
            /// </summary>
            public DataTable Page { get; set; }
            /// <summary>
            /// 页大小
            /// </summary>
            public int PageCount { get; set; }
            /// <summary>
            /// 记录条数
            /// </summary>
            public int RowCount { get; set; }
        }
        #endregion

        #region 1.3 新增资讯信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class AddNewsInfoRP : LNewsEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NewsTypeId))
                {
                    throw new ArgumentNullException("分类标志不能为空！");
                }
                if (string.IsNullOrEmpty(NewsTitle))
                {
                    throw new ArgumentNullException("标题不能为空！");
                }
                if (string.IsNullOrEmpty(Content))
                {
                    throw new ArgumentNullException("内容不能为空！");
                }
                if (IsRelMicro)
                {
                    if (string.IsNullOrEmpty(MicroTypeID))
                    {
                        throw new ArgumentNullException("刊号标志ID不能为空！");
                    }
                    if (string.IsNullOrEmpty(MicroNumberID))
                    {
                        throw new ArgumentNullException("板块标志ID不能为空！");
                    }
                }
            }
            /// <summary>
            /// 分类标志ID
            /// </summary>
            public string NewsTypeId { get; set; }
            /// <summary>
            /// 标签
            /// </summary>
            public string LabelID { get; set; }
            /// <summary>
            /// 是否关联微刊  True：关联 False：不关联
            /// </summary>
            public bool IsRelMicro { get; set; }
            /// <summary>
            /// 板块标志ID
            /// </summary>
            public string MicroTypeID { get; set; }
            /// <summary>
            /// 刊号标志ID
            /// </summary>
            public string MicroNumberID { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class AddNewsInfoRD : IAPIResponseData
        { }
        #endregion

        #region 1.4 更新资讯信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class UpdateNewsInfoRP : AddNewsInfoRP
        {
            /// <summary>
            /// 资讯微刊关联标识ID
            /// </summary>
            public string MappingId { get; set; }
            /// <summary>
            /// 旧的期刊标识ID
            /// </summary>
            public string OldNumberId { get; set; }
            /// <summary>
            /// 旧的类型标识ID
            /// </summary>
            public string OldTypeId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class UpdateNewsInfoRD : IAPIResponseData
        { }
        #endregion

        #region 1.5 删除资讯信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class DelNewsInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NewsIds))
                {
                    throw new ArgumentNullException("资讯IDs不能为空");
                }
            }
            public string NewsIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class DelNewsInfoRD : IAPIResponseData
        { }
        #endregion

        #region 1.6 上传缩略图

        #endregion

        #region 1.7 获取刊号列表
        class GetMicroNumListRP : EmptyRequestParameter
        {

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetMicroNumListRD : IAPIResponseData
        {
            /// <summary>
            /// 返回的类型列表
            /// </summary>
            public DataTable NumberList { get; set; }
        }
        #endregion

        #region 1.8 获取版块列表
        class GetMicroTypesRP : EmptyRequestParameter
        {

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetMicroTypesRD : IAPIResponseData
        {
            /// <summary>
            /// 返回的类型列表
            /// </summary>
            public DataTable MicroTypeList { get; set; }
        }
        #endregion

        #region 1.8.1 获取版块列表：分页
        class GetMicroTypeListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();
            }
            /// <summary>
            /// 排序方式：0 升序 1 降序
            /// </summary>
            public int SortOrder { get; set; }
            /// <summary>
            /// 排序字段
            /// </summary>
            public string SortField { get; set; }
            /// <summary>
            /// 当前页
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 页大小
            /// </summary>
            public int PageSize { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetMicroTypeListRD : IAPIResponseData
        {
            /// <summary>
            /// 返回的类型列表
            /// </summary>
            public DataTable MicroTypeList { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageCount { get; set; }
            /// <summary>
            /// 总记录数
            /// </summary>
            public int RowCount { get; set; }
        }
        #endregion

        #region 1.9 获取版块列表
        class GetNewsDetailRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NewsId))
                {
                    throw new ArgumentNullException("新闻资讯NewsId不能为空！");
                }
            }
            /// <summary>
            /// 新闻资讯ID
            /// </summary>
            public string NewsId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNewsDetailRD : IAPIResponseData
        {
            /// <summary>
            /// 返回的类型列表
            /// </summary>
            public DataTable NewsDetail { get; set; }
            /// <summary>
            /// 是否关联微刊
            /// </summary>
            public bool IsRelMicro { get; set; }
        }
        #endregion

        #region 2.1 添加分类
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class AddNewsTypesRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public virtual void Validate()
            {
                if (string.IsNullOrEmpty(TypeName))
                {
                    throw new ArgumentNullException("类型名称不能为空！");
                }
            }
            /// <summary>
            /// 父类
            /// </summary>
            public string ParentId { get; set; }
            /// <summary>
            /// 父类的层级
            /// </summary>
            public int ParentLevel { get; set; }
            /// <summary>
            /// 类型名称
            /// </summary>
            public string TypeName { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class AddNewsTypesRD : EmptyResponseData
        {

        }
        #endregion

        #region 2.2 修改分类
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class UpdateNewsTypesRP : AddNewsTypesRP
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public override void Validate()
            {
                if (string.IsNullOrEmpty(TypeName))
                {
                    throw new ArgumentNullException("类型名称不能为空！");
                }
                if (string.IsNullOrEmpty(TypeId))
                {
                    throw new ArgumentNullException("类型ID不能为空！");
                }
            }
            /// <summary>
            /// 类型标志
            /// </summary>
            public string TypeId { get; set; }

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class UpdateNewsTypesRD : EmptyResponseData
        {

        }
        #endregion

        #region 2.3 修改分类
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class DelNewsTypeRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(TypeIds))
                {
                    throw new ArgumentNullException("分类标识IDs不能为空！");
                }
            }
            /// <summary>
            /// 分类标识ID : 批量删除 “1,2,3,4,5,6”
            /// </summary>
            public string TypeIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class DelNewsTypeRD : EmptyResponseData
        {

        }
        #endregion

        #region 2.4 获取分类详细
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNewsTypesDetailRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(TypeId))
                {
                    throw new ArgumentNullException("分类标识ID不能为空！");
                }
            }
            /// <summary>
            /// 分类标识ID
            /// </summary>
            public string TypeId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNewsTypesDetailRD : LNewsTypeEntity, IAPIResponseData
        {
        }
        #endregion

        #region 2.5 获取分类列表(分页)
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetTypeListPageRP : GetNewsListRP
        {
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetTypeListPageRD : GetNewsListRD
        {

        }
        #endregion

        #region 2.6 获取期数版块关联列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNumberTypeMappingRP : LNumberTypeMappingEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NumberId))
                {
                    throw new ArgumentNullException("刊号ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNumberTypeMappingRD : IAPIResponseData
        {
            /// <summary>
            /// 返回类型ID数组，以逗号分割
            /// </summary>
            public string TypeIds { get; set; }
        }
        #endregion

        #region 2.7 设置期数版块关联列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class SetNumberTypeMappingRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NumberId))
                {
                    throw new ArgumentNullException("刊号ID不能为空！");
                }
                if (string.IsNullOrEmpty(TypeIds))
                {
                    throw new ArgumentNullException("分类ID列表不能为空！");
                }
            }
            /// <summary>
            /// 刊号ID
            /// </summary>
            public string NumberId { get; set; }
            /// <summary>
            /// 分类ID列表，逗号分割的字符串
            /// </summary>
            public string TypeIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class SetNumberTypeMappingRD : EmptyResponseData
        {
        }
        #endregion

        #region 2.8 获取微刊资讯关联列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNewsMappListRP : LNewsMicroMappingEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MicroNumberId))
                {
                    throw new ArgumentNullException("微刊期数标志ID不能为空！");
                }
                if (string.IsNullOrEmpty(MicroTypeId))
                {
                    throw new ArgumentNullException("微刊分类标志ID不能为空！");
                }
            }
            /// <summary>
            /// 排序字段
            /// </summary>
            public string SortField { get; set; }
            /// <summary>
            /// 排序方式 0 升序 1 降序
            /// </summary>
            public int SortOrder { get; set; }
            /// <summary>
            /// 当前页
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 页大小
            /// </summary>
            public int PageSize { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNewsMappListRD : GetNewsListRD
        {
        }
        #endregion

        #region 2.9 删除资讯微刊关联
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class DelNewsMicroMapRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MappingIds))
                {
                    throw new ArgumentNullException("标识IDs不能为空");
                }
            }
            public string MappingIds { get; set; }
            public string MicroNumberId { get; set; }
            public string MicroTypeId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class DelNewsMicroMapRD : EmptyResponseData
        { }
        #endregion

        #region 3.0 设置关联表排序
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class SetNewsMapOrderRP : LNewsMicroMappingEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (MappingId == null)
                {
                    throw new ArgumentNullException("标识ID不能为空");
                }
                if (Sequence == null)
                {
                    throw new ArgumentNullException("排序值不能为空");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class SetNewsMapOrderRD : EmptyResponseData
        { }
        #endregion

        #region 3.1 设置资讯微刊关联列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class SetNewsMapRP : LNewsMicroMappingEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MicroNumberId))
                {
                    throw new ArgumentNullException("微刊期数Id不能为空！");
                }
                if (string.IsNullOrEmpty(MicroTypeId))
                {
                    throw new ArgumentNullException("关联的微刊版块Id不能为空！");
                }
                if (string.IsNullOrEmpty(NewIds))
                {
                    throw new ArgumentNullException("关联的资讯Ids不能为空！");
                }
            }
            /// <summary>
            /// 关联的资讯Id列表，逗号分割的字符串
            /// </summary>
            public string NewIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class SetNewsMapRD : EmptyResponseData
        { }
        #endregion
        #endregion

        #region 接口处理逻辑
        #region 1.1 获取分类列表
        protected string DoGetNewsTypes(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNewsTypeListRP>>();

            //参数校验

            //构造响应数据
            var rd = new APIResponse<GetNewsTypeListRD>(new GetNewsTypeListRD());
            try
            {
                //获取数据集
                /*DataTable dt = new DataTable();
                    dt.Columns.Add("TypeId");
                    dt.Columns.Add("TypeName");
                    dt.Rows.Add(new string[]{"34E07242-B38F-4262-9AA9-21A18C73DEF5","资讯信息"});
                    dt.Rows.Add(new string[] { "A13F9C0D-BA0E-4F08-8075-1F4DA2E099B1", "项目推荐" });
                */
                rd.Data.TypeList = PrivateLNewsTypeBLL.GetNewsTypes("");
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.2 获取资讯列表
        protected string DoGetNewsList(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNewsListRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetNewsListRD>(new GetNewsListRD());
            try
            {
                //获取列表信息
                int pageCount = 0;
                int rowCount = 0;
                //增加是否取未关联的资讯 add by yehua
                rd.Data.Page = PrivateLNewsBLL.GetNewsList(new LNewsEntity() { NewsType = rp.Parameters.NewsTypeId }, rp.Parameters.StartDate, rp.Parameters.EndDate, rp.Parameters.Keyword, rp.Parameters.SortField, rp.Parameters.SortOrder, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.IsMappingNews, ref pageCount, ref rowCount);
                rd.Data.PageCount = pageCount;
                rd.Data.RowCount = rowCount;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.3 新增资讯信息
        protected string DoAddNewsInfo(string pRequest)
        {
            //请求参数反序列化
            var rq = pRequest.DeserializeJSONTo<APIRequest<AddNewsInfoRP>>();

            //参数校验
            rq.Parameters.Validate();

            //构造响应数据
            var rp = new APIResponse<AddNewsInfoRD>(new AddNewsInfoRD());
            try
            {
                //新增记录
                LNewsEntity lNewsEn = new LNewsEntity();
                lNewsEn.NewsType = rq.Parameters.NewsTypeId;
                lNewsEn.Author = rq.Parameters.Author;
                lNewsEn.NewsTitle = rq.Parameters.NewsTitle;
                lNewsEn.Intro = rq.Parameters.Intro;
                lNewsEn.Content = rq.Parameters.Content;
                lNewsEn.ImageUrl = rq.Parameters.ImageUrl;
                lNewsEn.PublishTime = rq.Parameters.PublishTime;

                PrivateLNewsBLL.AddNewsInfo(lNewsEn, rq.Parameters.MicroNumberID, rq.Parameters.MicroTypeID, rq.Parameters.LabelID ?? string.Empty);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rp.ToJSON();
        }
        #endregion

        #region 1.4 更新资讯信息
        protected string DoUpdateNewsInfo(string pRequest)
        {
            //请求参数反序列化
            var rq = pRequest.DeserializeJSONTo<APIRequest<UpdateNewsInfoRP>>();

            //参数校验
            rq.Parameters.Validate();

            //构造响应数据
            var rp = new APIResponse<UpdateNewsInfoRD>(new UpdateNewsInfoRD());
            try
            {
                //更新数据库
                LNewsEntity lNewsEn = new LNewsEntity();
                lNewsEn.NewsId = rq.Parameters.NewsId;
                lNewsEn.NewsType = rq.Parameters.NewsTypeId;
                lNewsEn.Author = rq.Parameters.Author;
                lNewsEn.NewsTitle = rq.Parameters.NewsTitle;
                lNewsEn.Intro = rq.Parameters.Intro;
                lNewsEn.Content = rq.Parameters.Content;
                lNewsEn.ImageUrl = rq.Parameters.ImageUrl;
                lNewsEn.PublishTime = rq.Parameters.PublishTime;

                PrivateLNewsBLL.UpdateNewsInfo(lNewsEn, rq.Parameters.MappingId, rq.Parameters.OldNumberId, rq.Parameters.OldTypeId, rq.Parameters.MicroNumberID, rq.Parameters.MicroTypeID, rq.Parameters.LabelID, rq.Parameters.IsRelMicro);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rp.ToJSON();
        }
        #endregion

        #region 1.5 删除资讯信息
        protected string DoDelNewsInfo(string pRequest)
        {
            //请求参数反序列化
            var rq = pRequest.DeserializeJSONTo<APIRequest<DelNewsInfoRP>>();

            //参数校验
            rq.Parameters.Validate();

            //构造响应数据
            var rp = new APIResponse<DelNewsInfoRD>(new DelNewsInfoRD());
            try
            {
                //删除
                PrivateLNewsBLL.DelNewsInfo(rq.Parameters.NewsIds);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rp.ToJSON();
        }
        #endregion

        #region 1.6 上传缩略图

        #endregion

        #region 1.7 获取刊号列表
        protected string DoGetMicroNums(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMicroNumListRP>>();

            //参数校验

            //构造响应数据
            var rd = new APIResponse<GetMicroNumListRD>(new GetMicroNumListRD());
            try
            {
                /*
                DataTable dt = new DataTable();
                dt.Columns.Add("NumberID");
                dt.Columns.Add("NumberNo ");
                dt.Rows.Add(new string[] { "2438FFBF-437B-480D-B43C-307C84FF1595", "1" });
                dt.Rows.Add(new string[] { "9DED11A2-E1E4-4A23-8A13-AADE92B4C18E", "2" });*/
                rd.Data.NumberList = PrivateEclubMicroNumberBLL.GetMicroNums(new EclubMicroNumberEntity());
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.8 获取版块列表
        protected string DoGetMicroTypes(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMicroTypesRP>>();

            //参数校验

            //构造响应数据
            var rd = new APIResponse<GetMicroTypesRD>(new GetMicroTypesRD());
            try
            {
                /*
                DataTable dt = new DataTable();
                dt.Columns.Add("TypeId");
                dt.Columns.Add("TypeName ");
                dt.Rows.Add(new string[] { "E8C34143-6419-4ABA-BF71-EA5B860BC934", "热点" });
                dt.Rows.Add(new string[] { "47131C9E-7B8B-4D17-8409-E5CAFCF7B1DA", "活动" });
                */
                rd.Data.MicroTypeList = PrivateEclubMicroTypeBLL.GetMicroTypes(new EclubMicroTypeEntity());

            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.8.1 获取版块列表:分页
        protected string DoGetMicroTypeList(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMicroTypeListRP>>();

            //参数校验

            //构造响应数据
            var rd = new APIResponse<GetMicroTypeListRD>(new GetMicroTypeListRD());
            try
            {
                int pageCount = 0;
                int rowCount = 0;
                rd.Data.MicroTypeList = PrivateEclubMicroTypeBLL.GetMicroTypeList(rp.Parameters.SortOrder, rp.Parameters.SortField, rp.Parameters.PageIndex, rp.Parameters.PageSize, ref pageCount, ref rowCount);
                rd.Data.RowCount = rowCount;
                rd.Data.PageCount = pageCount;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.9 获取咨询详细
        protected string DoGetNewsDetail(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNewsDetailRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetNewsDetailRD>(new GetNewsDetailRD());
            try
            {
                //Get News Detail Info
                bool isRel = false;
                rd.Data.NewsDetail = PrivateLNewsBLL.GetNewsDetail(rp.Parameters.NewsId, ref isRel);
                rd.Data.IsRelMicro = isRel;

            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.1 添加分类
        protected string DoAddNewsTypes(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddNewsTypesRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<AddNewsTypesRD>(new AddNewsTypesRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsTypeBLL.Create(new LNewsTypeEntity() { NewsTypeId = Guid.NewGuid().ToString(), ParentTypeId = rp.Parameters.ParentId, NewsTypeName = rp.Parameters.TypeName, TypeLevel = rp.Parameters.ParentLevel + 1, CustomerId = PrivateLoggingSessionInfo.ClientID });
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.2 修改分类
        protected string DoUpdateNewsTypes(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateNewsTypesRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<UpdateNewsTypesRD>(new UpdateNewsTypesRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsTypeBLL.Update(new LNewsTypeEntity() { NewsTypeId = rp.Parameters.TypeId, ParentTypeId = rp.Parameters.ParentId, NewsTypeName = rp.Parameters.TypeName, TypeLevel = rp.Parameters.ParentLevel + 1 }, false);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.3 删除分类
        protected string DoDelNewsType(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<DelNewsTypeRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<DelNewsTypeRD>(new DelNewsTypeRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsTypeBLL.Delete(rp.Parameters.TypeIds.Split(','));
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.4 获取分类详细
        protected string DoGetNewsTypesDetail(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNewsTypesDetailRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetNewsTypesDetailRD>(new GetNewsTypesDetailRD());
            try
            {
                //Get News Detail Info
                LNewsTypeEntity tyEn = PrivateLNewsTypeBLL.GetByID(rp.Parameters.TypeId);
                if (tyEn != null)
                {
                    rd.Data.NewsTypeId = tyEn.NewsTypeId;
                    rd.Data.NewsTypeName = tyEn.NewsTypeName;
                    rd.Data.TypeLevel = tyEn.TypeLevel - 1;
                    rd.Data.ParentTypeId = tyEn.ParentTypeId;
                    LNewsTypeEntity preEn = PrivateLNewsTypeBLL.GetByID(tyEn.ParentTypeId);
                    rd.Data.ParentTypeName = preEn != null ? preEn.NewsTypeName : string.Empty;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.5 获取分类列表(分页)
        protected string DoGetTypeListPage(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetTypeListPageRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetTypeListPageRD>(new GetTypeListPageRD());
            try
            {
                //获取列表信息
                int pageCount = 0;
                int rowCount = 0;
                rd.Data.Page = PrivateLNewsTypeBLL.GetTypeListPage(rp.Parameters.SortField, rp.Parameters.SortOrder, rp.Parameters.PageIndex, rp.Parameters.PageSize, ref pageCount, ref rowCount);
                rd.Data.PageCount = pageCount;
                rd.Data.RowCount = rowCount;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.6 获取期数版块关联列表
        protected string DoGetNumberTypeMapping(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNumberTypeMappingRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetNumberTypeMappingRD>(new GetNumberTypeMappingRD());
            try
            {
                //Get News Detail Info
                rd.Data.TypeIds = PrivateLNumberTypeMappingBLL.GetNumberTypeMapping(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.7 设置期数版块关联列表
        protected string DoSetNumberTypeMapping(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetNumberTypeMappingRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<SetNumberTypeMappingRD>(new SetNumberTypeMappingRD());
            try
            {
                //Get News Detail Info
                PrivateLNumberTypeMappingBLL.SetNumberTypeMapping(rp.Parameters.NumberId, rp.Parameters.TypeIds);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.8 获取微刊资讯关联列表
        protected string DoGetNewsMappList(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNewsMappListRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<GetNewsMappListRD>(new GetNewsMappListRD());
            try
            {
                //Get News Detail Info
                int rowCount = 0;
                int pageCount = 0;
                rd.Data.Page = PrivateLNewsMicroMappingBLL.GetNewsMappList(rp.Parameters.MicroNumberId, rp.Parameters.MicroTypeId, rp.Parameters.SortField, rp.Parameters.SortOrder, rp.Parameters.PageIndex, rp.Parameters.PageSize, ref rowCount, ref pageCount);
                rd.Data.PageCount = pageCount;
                rd.Data.RowCount = rowCount;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 2.9 删除资讯微刊关联
        protected string DoDelNewsMicroMap(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<DelNewsMicroMapRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<DelNewsMicroMapRD>(new DelNewsMicroMapRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsMicroMappingBLL.Delete(rp.Parameters.MappingIds.Split(','));
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 3.0 设置关联表排序
        protected string DoSetNewsMapOrder(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetNewsMapOrderRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<SetNewsMapOrderRD>(new SetNewsMapOrderRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsMicroMappingBLL.Update(new LNewsMicroMappingEntity() { MappingId = rp.Parameters.MappingId, Sequence = rp.Parameters.Sequence }, false);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 3.1 设置资讯微刊关联列表
        protected string DoSetNewsMap(string pRequest)
        {
            //请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetNewsMapRP>>();

            //参数校验
            rp.Parameters.Validate();

            //构造响应数据
            var rd = new APIResponse<SetNewsMapRD>(new SetNewsMapRD());
            try
            {
                //Get News Detail Info
                PrivateLNewsMicroMappingBLL.SetNewsMap(rp.Parameters.MicroNumberId, rp.Parameters.MicroTypeId, rp.Parameters.NewIds);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion
        #endregion

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst = string.Empty;
            switch (pAction)
            {
                case "GetNewsTypes"://获取分类列表
                    rst = this.DoGetNewsTypes(pRequest);
                    break;
                case "GetNewsList": //获取资讯列表
                    rst = this.DoGetNewsList(pRequest);
                    break;
                case "AddNewsInfo": //新增资讯列表
                    rst = this.DoAddNewsInfo(pRequest);
                    break;
                case "UpdateNewsInfo": //更新资讯列表
                    rst = this.DoUpdateNewsInfo(pRequest);
                    break;
                case "DelNewsInfo": //删除资讯列表
                    rst = this.DoDelNewsInfo(pRequest);
                    break;
                case "ImgUpload": //上传缩略图
                    rst = string.Empty;
                    break;
                case "GetMicroNums": //获取刊号列表
                    rst = this.DoGetMicroNums(pRequest);
                    break;
                case "GetMicroTypes": //获取版块列表
                    rst = this.DoGetMicroTypes(pRequest);
                    break;
                case "GetMicroTypeListPage"://获取版块列表：分页
                    rst = this.DoGetMicroTypeList(pRequest);
                    break;
                case "GetNewsDetail"://获取咨询详细
                    rst = this.DoGetNewsDetail(pRequest);
                    break;
                case "AddNewsTypes"://添加分类
                    rst = this.DoAddNewsTypes(pRequest);
                    break;
                case "UpdateNewsTypes"://修改分类
                    rst = this.DoUpdateNewsTypes(pRequest);
                    break;
                case "GetNewsTypesDetail"://获取分类详细信息
                    rst = this.DoGetNewsTypesDetail(pRequest);
                    break;
                case "DelNewsType"://删除分类
                    rst = this.DoDelNewsType(pRequest);
                    break;
                case "GetTypeListPage"://获取分类列表(分页)
                    rst = this.DoGetTypeListPage(pRequest);
                    break;
                case "GetNumberTypeMapping"://获取期数版块关联列表
                    rst = this.DoGetNumberTypeMapping(pRequest);
                    break;
                case "SetNumberTypeMapping"://设置期数版块关联列表
                    rst = this.DoSetNumberTypeMapping(pRequest);
                    break;
                case "GetNewsMappList"://获取微刊资讯关联列表
                    rst = this.DoGetNewsMappList(pRequest);
                    break;
                case "DelNewsMicroMap"://删除资讯微刊关联
                    rst = this.DoDelNewsMicroMap(pRequest);
                    break;
                case "SetNewsMapOrder"://设置关联表排序
                    rst = this.DoSetNewsMapOrder(pRequest);
                    break;
                case "SetNewsMap"://设置资讯微刊关联列表
                    rst = this.DoSetNewsMap(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }
}