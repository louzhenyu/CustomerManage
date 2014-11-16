using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.CPOS.BS.BLL;
using JIT.Utility.Reflection;
using JIT.Utility.ExtensionMethod;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.Eclub.Module
{
    /// <summary>
    /// MicroIssueHandler 的摘要说明
    /// </summary>
    public class MicroIssueHandler : BaseGateway
    {
        #region 接口请求及响应数据结构
        #region 5.1 获取微刊类型信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueTypeGetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 期刊标识ID
            /// </summary>
            public string NumberId { get; set; }
            /// <summary>
            /// 父ID,取父数据可为null,取子类不能为null
            /// </summary>
            public string ParentID { get; set; }
            /// <summary>
            /// 期刊标识ID
            /// </summary>
            public string TypeLevel { get; set; }
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueTypeGetRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊类型信息集合
            /// </summary>
            public DataTable EclubMicroTypes { get; set; }
        }
        #endregion

        #region 5.2 新增微刊类型信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueTypeAddRP : EclubMicroTypeEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MicroTypeName))
                {
                    throw new ArgumentNullException("微刊类别名称不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueTypeAddRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.3 更新微刊类型信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueTypeUpdateRP : EclubMicroTypeEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (MicroTypeID == null)
                {
                    throw new ArgumentNullException("微刊类别主键ID不能为空！");
                }
                if (string.IsNullOrEmpty(MicroTypeName))
                {
                    throw new ArgumentNullException("微刊类别名称不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueTypeUpdateRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.4 删除微刊类型信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueTypeDeleteRP : EclubMicroTypeEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (MicroTypeID == null)
                {
                    throw new ArgumentNullException("微刊类别主键ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueTypeDeleteRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.5 获取微刊期数信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueNperGetRP : EmptyRequestParameter
        {

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueNperGetRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊期数信息集合
            /// </summary>
            public EclubMicroNumberEntity[] EclubMicroNumbers { get; set; }
        }
        #endregion

        #region 5.6 新增微刊期数信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueNperAddRP : EclubMicroNumberEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MicroNumberName))
                {
                    throw new ArgumentNullException("微刊期数名称不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueNperAddRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.7 更新微刊期数信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueNperUpdateRP : EclubMicroNumberEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroNumberID == null)
                {
                    throw new ArgumentNullException("微刊期数主键ID不能为空！");
                }
                if (string.IsNullOrEmpty(MicroNumberName))
                {
                    throw new ArgumentNullException("微刊期数名称不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueNperUpdateRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.8 删除微刊期数信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueNperDeleteRP : EclubMicroNumberEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroNumberID == null)
                {
                    throw new ArgumentNullException("微刊期数主键ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueNperDeleteRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.9 获取微刊列表信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueListGetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 主键ID 可以为空
            /// </summary>
            public Guid? MicroID { get; set; }
            /// <summary>
            /// 微刊类别ID 可为空
            /// </summary>
            public string MicroTypeID { get; set; }
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public string MicroNumberID { get; set; }
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueListGetRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊列表信息
            /// </summary>
            public EclubMicroEntity[] EclubMicros { get; set; }
        }
        #endregion

        #region 5.10 新增微刊详细信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueDetailAddRP : EclubMicroEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MicroTypeID))
                {
                    throw new ArgumentNullException("微刊类别ID不能为空！");
                }
                if (string.IsNullOrEmpty(MicroNumberID))
                {
                    throw new ArgumentNullException("微刊期数ID不能为空！");
                }
                if (string.IsNullOrEmpty(MicroTitle))
                {
                    throw new ArgumentNullException("微刊标题不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueDetailAddRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.11 更新微刊详细信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueDetailUpdateRP : EclubMicroEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroID == null)
                {
                    throw new ArgumentNullException("微刊信息ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueDetailUpdateRD : EmptyResponseData
        {

        }

        #endregion

        #region 5.12 删除微刊详细信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueDetailDeleteRP : EclubMicroEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroID == null)
                {
                    throw new ArgumentNullException("微刊信息ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueDetailDeleteRD : EmptyResponseData
        {

        }
        #endregion

        #region 5.13 获取微刊分页信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssuePageGetRP : MicroIssueListGetRP
        {
            /// <summary>
            /// 排序字段
            /// </summary>
            public string SortField { get; set; }
            /// <summary>
            /// 0 倒序，1 顺序
            /// </summary>
            public int? SortOrder { get; set; }
            private int _PageIndex;
            /// <summary>
            /// 当前页 默认从 0 开始
            /// </summary>
            public int PageIndex
            {
                get { return _PageIndex; }
                set { _PageIndex = value; }
            }

            private int _PageSize;
            /// <summary>
            /// 页大小 默认大小 15 条
            /// </summary>
            public int PageSize
            {
                get { return _PageSize; }
                set { _PageSize = value; }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssuePageGetRD : IAPIResponseData
        {
            public JIT.Utility.DataAccess.PagedQueryResult<EclubMicroEntity> EclubMicros { get; set; }
        }
        #endregion

        #region 5.14 获取微刊详细信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueDetailGetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 主键ID 可以为空
            /// </summary>
            public Guid? MicroID { get; set; }
            /// <summary>
            /// 微刊类别ID 可为空
            /// </summary>
            public string MicroTypeID { get; set; }
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public string MicroNumberID { get; set; }
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroID == null)
                {
                    throw new ArgumentNullException("微刊ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueDetailGetRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊列表信息
            /// </summary>
            public EclubMicroEntity EclubMicros { get; set; }
            /// <summary>
            /// 该微刊同类别同期数所有兄弟姊妹标识集合
            /// </summary>
            public List<Guid?> MicroIDS { get; set; }
        }
        #endregion

        #region 5.15 微刊阅读、分享、浏览量
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class AddMicroStatsRP : IAPIRequestParameter
        {
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public Guid? MicroID { get; set; }
            /// <summary>
            /// 字段（Goods、Shares）
            /// </summary>
            public string Field { get; set; }
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroID == null)
                {
                    throw new ArgumentNullException("微刊ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class AddMicroStatsRD : EmptyResponseData
        {
            //返回当前数量 add by zyh
            public int Count;
        }
        #endregion

        #region 6.1 获取微新闻分页信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroNewsPageGetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();
            }
            /// <summary>
            /// 微刊类别ID 可为空
            /// </summary>
            public string MicroTypeID { get; set; }
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public string MicroNumberID { get; set; }
            /// <summary>
            /// 排序字段
            /// </summary>
            public string SortField { get; set; }
            /// <summary>
            /// 0 倒序，1 顺序
            /// </summary>
            public int SortOrder { get; set; }
            /// <summary>
            /// 当前页 默认从 0 开始
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 页大小 默认大小 15 条
            /// </summary>
            public int PageSize { get; set; }

        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroNewsPageGetRD : IAPIResponseData
        {
            /// <summary>
            /// 数据集
            /// </summary>
            public DataTable MicroNews { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageCount { get; set; }
            /// <summary>
            /// 总行数
            /// </summary>
            public int RowCount { get; set; }
        }
        #endregion

        #region 6.2 获取微新闻详细信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroNewsDetailGetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 主键ID 可以为空
            /// </summary>
            public string NewsId { get; set; }
            /// <summary>
            /// 微刊类别ID 可为空
            /// </summary>
            public string MicroTypeID { get; set; }
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public string MicroNumberID { get; set; }
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (NewsId == null)
                {
                    throw new ArgumentNullException("微新闻ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroNewsDetailGetRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊详细信息
            /// </summary>
            public DataTable MicroNews { get; set; }
            /// <summary>
            /// 该微刊同类别同期数所有兄弟姊妹标识集合
            /// </summary>
            public DataTable MicroNewsIDs { get; set; }
        }
        #endregion

        #region 6.3 微新闻阅读、分享、浏览量
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroNewsCollSetRP : IAPIRequestParameter
        {
            /// <summary>
            /// 微刊期数ID 可为空
            /// </summary>
            public string NewsId { get; set; }
            /// <summary>
            /// 字段（PraiseCount, CollCount）
            /// </summary>
            public string Field { get; set; }
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (NewsId == null)
                {
                    throw new ArgumentNullException("微新闻ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroNewsCollSetRD : IAPIResponseData
        {
            /// <summary>
            /// 返回当前数量
            /// </summary>
            public int Count { get; set; }
        }
        #endregion
        #endregion

        #region 接口处理逻辑
        #region 5.1 获取微刊类型信息
        protected string DoMicroIssueTypeGet(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeGetRP>>();

            //2、参数校验

            //3、构造响应数据
            var rd = new APIResponse<MicroIssueTypeGetRD>(new MicroIssueTypeGetRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、获取数据信息
                rd.Data.EclubMicroTypes = new EclubMicroTypeBLL(loggingSessionInfo).MicroIssueTypeGet(rp.Parameters.NumberId, rp.Parameters.ParentID, rp.Parameters.TypeLevel);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.2 新增微刊类型信息
        protected string DoMicroIssueTypeAdd(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeAddRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、构造响应数据
            var rd = new APIResponse<MicroIssueTypeAddRD>(new MicroIssueTypeAddRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、新增数据信息
                rp.Parameters.CreateBy = rp.UserID;
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.CustomerId = rp.CustomerID;
                new EclubMicroTypeBLL(loggingSessionInfo).Create(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.3 更新微刊类型信息
        protected string DoMicroIssueTypeUpdate(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeUpdateRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、构造响应数据
            var rd = new APIResponse<MicroIssueTypeUpdateRD>(new MicroIssueTypeUpdateRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、更新数据信息
                rp.Parameters.CustomerId = rp.CustomerID;
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                new EclubMicroTypeBLL(loggingSessionInfo).Update(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.4 删除微刊类型信息
        protected string DoMicroIssueTypeDelete(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeDeleteRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、构造响应数据
            var rd = new APIResponse<MicroIssueTypeDeleteRD>(new MicroIssueTypeDeleteRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、删除数据信息
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                new EclubMicroTypeBLL(loggingSessionInfo).Delete(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.5 获取微刊期数信息
        protected string DoMicroIssueNperGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperGetRP>>();

            //2、验证参数

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueNperGetRD>(new MicroIssueNperGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rd.Data.EclubMicroNumbers = new EclubMicroNumberBLL(loggingSessionInfo).MicroIssueNperGet(new EclubMicroNumberEntity());
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.6 新增微刊期数信息
        protected string DoMicroIssueNperAdd(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperAddRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }
            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueNperAddRD>(new MicroIssueNperAddRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.CreateBy = rp.UserID;
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.CustomerId = rp.CustomerID;
                new EclubMicroNumberBLL(loggingSessionInfo).Create(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.7 更新微刊期数信息
        protected string DoMicroIssueNperUpdate(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperUpdateRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueNperUpdateRD>(new MicroIssueNperUpdateRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                rp.Parameters.CustomerId = rp.CustomerID;
                new EclubMicroNumberBLL(loggingSessionInfo).Update(rp.Parameters);

            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.8 删除微刊期数信息
        protected string DoMicroIssueNperDelete(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperDeleteRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueNperDeleteRD>(new MicroIssueNperDeleteRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                new EclubMicroNumberBLL(loggingSessionInfo).Delete(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.9 获取微刊列表信息
        protected string DoMicroIssueListGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueListGetRP>>();

            //2、验证参数

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueListGetRD>(new MicroIssueListGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rd.Data.EclubMicros = new EclubMicroBLL(loggingSessionInfo).MicroIssueListGet(new EclubMicroEntity());
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.10 新增微刊详细信息
        protected string DoMicroIssueDetailAdd(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailAddRP>>();

            //2、验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueDetailAddRD>(new MicroIssueDetailAddRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.CreateBy = rp.UserID;
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.CustomerId = rp.CustomerID;
                new EclubMicroBLL(loggingSessionInfo).Create(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.11 更新微刊详细信息
        protected string DoMicroIssueDetailUpdate(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailUpdateRP>>();

            //2、验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueDetailUpdateRD>(new MicroIssueDetailUpdateRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                rp.CustomerID = rp.CustomerID;
                new EclubMicroBLL(loggingSessionInfo).Update(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.12 删除微刊详细信息
        protected string DoMicroIssueDetailDelete(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailDeleteRP>>();

            //2、验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueDetailDeleteRD>(new MicroIssueDetailDeleteRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rp.Parameters.LastUpdateBy = rp.UserID;
                rp.Parameters.LastUpdateTime = DateTime.Now;
                new EclubMicroBLL(loggingSessionInfo).Delete(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.13 获取微刊分页信息
        protected string DoMicroIssuePageGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssuePageGetRP>>();

            //2、验证参数

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssuePageGetRD>(new MicroIssuePageGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                rd.Data.EclubMicros = new EclubMicroBLL(loggingSessionInfo).MicroIssuePageGet(new EclubMicroEntity() { MicroTypeID = rp.Parameters.MicroTypeID, MicroNumberID = rp.Parameters.MicroNumberID }, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.SortField, rp.Parameters.SortOrder);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.14 获取微刊详细信息
        protected string DoMicroIssueDetailGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailGetRP>>();

            //2、验证参数
            rp.Parameters.Validate();

            //3、拼装响应数据
            var rd = new APIResponse<MicroIssueDetailGetRD>(new MicroIssueDetailGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                EclubMicroBLL bll = new EclubMicroBLL(loggingSessionInfo);
                bll.AddMicroStats(rp.Parameters.MicroID, "Clicks");//Record browse

                List<Guid?> IdsLst = new List<Guid?>();
                rd.Data.EclubMicros = new EclubMicroBLL(loggingSessionInfo).MicroIssueDetailGet(new EclubMicroEntity() { MicroID = rp.Parameters.MicroID, MicroTypeID = rp.Parameters.MicroTypeID, MicroNumberID = rp.Parameters.MicroNumberID }, ref IdsLst);
                rd.Data.MicroIDS = IdsLst;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.15 微刊阅读、分享、浏览量
        protected string DoAddMicroStats(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddMicroStatsRP>>();

            //2、验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<AddMicroStatsRD>(new AddMicroStatsRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                EclubMicroBLL bll = new EclubMicroBLL(loggingSessionInfo);
                bll.AddMicroStats(rp.Parameters.MicroID, rp.Parameters.Field);

                //返回count
                rd.Data.Count = bll.GetMicroStats(rp.Parameters.MicroID, rp.Parameters.Field);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 6.1 获取微新闻分页信息
        protected string DoMicroNewsPageGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroNewsPageGetRP>>();

            //2、验证参数

            //3、拼装响应数据
            var rd = new APIResponse<MicroNewsPageGetRD>(new MicroNewsPageGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                int pageCount = 0;
                int rowCount = 0;
                rd.Data.MicroNews = new LNewsBLL(loggingSessionInfo).GetMicroNewsPageList(rp.Parameters.MicroNumberID, rp.Parameters.MicroTypeID, rp.Parameters.PageIndex - 1, rp.Parameters.PageSize, rp.Parameters.SortOrder, rp.Parameters.SortField, ref pageCount, ref rowCount);
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

        #region 6.2 获取微新闻详细信息
        protected string DoMicroNewsDetailGet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroNewsDetailGetRP>>();

            //2、验证参数
            rp.Parameters.Validate();

            //3、拼装响应数据
            var rd = new APIResponse<MicroNewsDetailGetRD>(new MicroNewsDetailGetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                LNewsBLL bll = new LNewsBLL(loggingSessionInfo);
                bll.SetMicroNewsColl(rp.Parameters.NewsId, "BrowseCount");//Record browse

                rd.Data.MicroNews = bll.GetMicroNewsDetail(rp.Parameters.NewsId);
                rd.Data.MicroNewsIDs = bll.GetMicroNewsSiblingsId(rp.Parameters.MicroNumberID, rp.Parameters.MicroTypeID);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 6.3 微新闻阅读、分享、浏览量
        protected string DoMicroNewsCollSet(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroNewsCollSetRP>>();

            //2、验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应数据
            var rd = new APIResponse<MicroNewsCollSetRD>(new MicroNewsCollSetRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、Access DB Result
                LNewsBLL bll = new LNewsBLL(loggingSessionInfo);
                bll.SetMicroNewsColl(rp.Parameters.NewsId, rp.Parameters.Field);

                //返回count
                rd.Data.Count = bll.GetMicroNewsStats(rp.Parameters.NewsId, rp.Parameters.Field);
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
            var rst = string.Empty;
            //1.根据type和action找到不同对应的处理程序
            switch (pAction)
            {
                case "MicroIssueTypeGet":
                    {
                        rst = this.DoMicroIssueTypeGet(pRequest);
                    }
                    break;
                case "MicroIssueTypeAdd":
                    {
                        rst = this.DoMicroIssueTypeAdd(pRequest);
                    }
                    break;
                case "MicroIssueTypeUpdate":
                    {
                        rst = this.DoMicroIssueTypeUpdate(pRequest);
                    }
                    break;
                case "MicroIssueTypeDelete":
                    {
                        rst = this.DoMicroIssueTypeDelete(pRequest);
                    }
                    break;
                case "MicroIssueNperGet":
                    {
                        rst = this.DoMicroIssueNperGet(pRequest);
                    }
                    break;
                case "MicroIssueNperAdd":
                    {
                        rst = DoMicroIssueNperAdd(pRequest);
                    }
                    break;
                case "MicroIssueNperUpdate":
                    {
                        rst = DoMicroIssueNperUpdate(pRequest);
                    }
                    break;
                case "MicroIssueNperDelete":
                    {
                        rst = DoMicroIssueNperDelete(pRequest);
                    }
                    break;
                case "MicroIssueListGet":
                    {
                        rst = DoMicroIssueListGet(pRequest);
                    }
                    break;
                case "MicroIssueDetailAdd":
                    {
                        rst = DoMicroIssueDetailAdd(pRequest);
                    }
                    break;
                case "MicroIssueDetailUpdate":
                    {
                        rst = DoMicroIssueDetailUpdate(pRequest);
                    }
                    break;
                case "MicroIssueDetailDelete":
                    {
                        rst = this.DoMicroIssueDetailDelete(pRequest);
                    }
                    break;
                /*case "MicroIssuePageGet":
                    {
                        rst = this.DoMicroIssuePageGet(pRequest);
                    }
                    break;
                case "MicroIssueDetailGet":
                    {
                        rst = this.DoMicroIssueDetailGet(pRequest);
                    }
                    break;
                case "AddMicroStats":
                    {
                        rst = this.DoAddMicroStats(pRequest);
                    }
                    break;
                 */
                case "MicroNewsPageGet":
                    {
                        rst = this.DoMicroNewsPageGet(pRequest);
                    }
                    break;
                case "MicroNewsDetailGet":
                    {
                        rst = this.DoMicroNewsDetailGet(pRequest);
                    }
                    break;
                case "MicroNewsCollSet":
                    {
                        rst = this.DoMicroNewsCollSet(pRequest);
                    }
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction)) { ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER };
            }
            return rst;
        }
    }
}