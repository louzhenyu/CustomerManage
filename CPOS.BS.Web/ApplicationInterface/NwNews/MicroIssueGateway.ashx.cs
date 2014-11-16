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
    /// MicroIssueGateway 的摘要说明
    /// </summary>
    public class MicroIssueGateway : BaseGateway
    {

        #region Private Obj
        private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo { get { return new SessionManager().CurrentUserLoginInfo; } }
        //private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo = new Entity.LoggingSessionInfo()
        //{
        //    CurrentUser = new Entity.User.UserInfo() { User_Id = "39E7EFBB-B28A-4B02-A396-B4BEB0EC2F82", customer_id = "ef10dfb65a004b88a6d3f547366c56b7" },
        //    CurrentLoggingManager = new Entity.LoggingManager()
        //    {
        //        Connection_String = "user id=dev;password=jit!2014;data source=112.124.68.147;database=cpos_bs_jit;",
        //        Customer_Id = "ef10dfb65a004b88a6d3f547366c56b7",
        //        Customer_Code = "Lzlj",
        //        Customer_Name = "",
        //        User_Name = "管理员"
        //    },
        //    ClientID = "ef10dfb65a004b88a6d3f547366c56b7"
        //};
        private EclubMicroNumberBLL PrivateEclubMicroNumberBLL { get { return new EclubMicroNumberBLL(PrivateLoggingSessionInfo); } }
        private EclubMicroTypeBLL PrivateEclubMicroTypeBLL { get { return new EclubMicroTypeBLL(PrivateLoggingSessionInfo); } }
        private EclubMicroBLL PrivateEclubMicroBLL { get { return new EclubMicroBLL(PrivateLoggingSessionInfo); } }
        private LNumberTypeMappingBLL PrivateLNumberTypeMappingBLL { get { return new LNumberTypeMappingBLL(PrivateLoggingSessionInfo); } }
        #endregion

        #region 接口请求及响应数据结构
        #region 5.1 获取微刊类型信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class MicroIssueTypeDetailRP : EclubMicroTypeEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 校验参数
            /// </summary>
            public void Validate()
            {
                if (MicroTypeID == null)
                {
                    throw new ArgumentNullException("标志ID不能为空！");
                }
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueTypeDetailRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊类型信息集合
            /// </summary>
            public DataTable EclubMicroType { get; set; }
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
                if (string.IsNullOrEmpty(ParentID))
                {
                    ParentID = null;
                }
            }
            /// <summary>
            /// 父类的层级
            /// </summary>
            public int ParentLevel { get; set; }
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
            /// <summary>
            /// 父类的层级
            /// </summary>
            public int ParentLevel { get; set; }
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
                if (string.IsNullOrEmpty(TypeIds))
                {
                    throw new ArgumentNullException("版块标识IDs不能为空！");
                }
            }
            /// <summary>
            /// 分类标识ID : 批量删除
            /// </summary>
            public string TypeIds { get; set; }
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
        class MicroNumberDetailRP : EclubMicroNumberEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (MicroNumberID == null)
                {
                    throw new ArgumentNullException("期数ID不能为空！");
                }
            }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroNumberDetailRD : IAPIResponseData
        {
            /// <summary>
            /// 微刊期数信息集合
            /// </summary>
            public EclubMicroNumberEntity EclubMicroNumber { get; set; }
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
            /// <summary>
            /// 期刊主键ID
            /// </summary>
            public string NumberId { get; set; }
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
                if (string.IsNullOrEmpty(NumberIds))
                {
                    throw new ArgumentNullException("期刊标识IDs不能为空！");
                }
            }
            /// <summary>
            /// 期刊标识ID : 批量删除 “1,2,3,4,5,6”
            /// </summary>
            public string NumberIds { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class MicroIssueNperDeleteRD : EmptyResponseData
        {

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

        #region 5.13 获取期刊列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNumberListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();                
            }
            /// <summary>
            /// 刊号
            /// </summary>
            public string Number { get; set; }
            /// <summary>
            /// 搜索关键字
            /// </summary>
            public string Keyword { get; set; }
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
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNumberListRD : IAPIResponseData
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

        #region 5.14 获取期刊列表
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetNumberTypeSumRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(NumberId))
                {
                    throw new ArgumentNullException("期刊标识字段Id不能为空！");
                }
            }
            /// <summary>
            /// 期刊标识字段
            /// </summary>
            public string NumberId { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetNumberTypeSumRD : IAPIResponseData
        {
            /// <summary>
            /// 数据集
            /// </summary>
            public DataTable Summarys { get; set; }
        }
        #endregion

        #endregion

        #region 接口处理逻辑
        #region 5.1 获取微刊类型信息
        protected string DoMicroIssueTypeDetail(string pRequest)
        {
            // 请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeDetailRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 构造响应数据
            var rd = new APIResponse<MicroIssueTypeDetailRD>(new MicroIssueTypeDetailRD());
            try
            {
                // 获取数据信息
                rd.Data.EclubMicroType = PrivateEclubMicroTypeBLL.GetMicroTypeDtail(rp.Parameters);
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
            // 请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeAddRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 构造响应数据
            var rd = new APIResponse<MicroIssueTypeAddRD>(new MicroIssueTypeAddRD());
            try
            {
                // 新增数据信息
                rp.Parameters.CustomerId = PrivateLoggingSessionInfo.ClientID;
                rp.Parameters.TypeLevel = rp.Parameters.ParentLevel + 1;
                rp.Parameters.Style = rp.Parameters.Style == null ? 0 : rp.Parameters.Style;
                PrivateEclubMicroTypeBLL.Create(rp.Parameters);
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
            // 请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeUpdateRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 构造响应数据
            var rd = new APIResponse<MicroIssueTypeUpdateRD>(new MicroIssueTypeUpdateRD());
            try
            {
                // 更新数据信息
                rp.Parameters.CustomerId = PrivateLoggingSessionInfo.ClientID;
                rp.Parameters.TypeLevel = rp.Parameters.ParentLevel + 1;
                PrivateEclubMicroTypeBLL.Update(rp.Parameters, false);
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
            // 请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueTypeDeleteRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 构造响应数据
            var rd = new APIResponse<MicroIssueTypeDeleteRD>(new MicroIssueTypeDeleteRD());
            try
            {
                // 删除数据信息
                PrivateEclubMicroTypeBLL.Delete(rp.Parameters.TypeIds.Split(','));
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
        protected string DoMicroNumberDetail(string pRequest)
        {
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroNumberDetailRP>>();

            // 验证参数

            // 拼装响应数据
            var rd = new APIResponse<MicroNumberDetailRD>(new MicroNumberDetailRD());

            try
            {
                // Access DB Result
                rd.Data.EclubMicroNumber = PrivateEclubMicroNumberBLL.GetByID(rp.Parameters.MicroNumberID);
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
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperAddRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }
            // 拼装响应数据
            var rd = new APIResponse<MicroIssueNperAddRD>(new MicroIssueNperAddRD());
            try
            {
                // Access DB Result
                rp.Parameters.CustomerId = PrivateLoggingSessionInfo.ClientID;
                rp.Parameters.MicroNumberID = Guid.NewGuid();
                rp.Parameters.MicroNumberNo = "Vol.0" + rp.Parameters.MicroNumber;//自动生成微刊编号
                PrivateEclubMicroNumberBLL.Create(rp.Parameters);
                //将主键ID返回给客户端
                rd.Data.NumberId = rp.Parameters.MicroNumberID.ToString();
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
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperUpdateRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<MicroIssueNperUpdateRD>(new MicroIssueNperUpdateRD());

            try
            {
                // Access DB Result
                rp.Parameters.CustomerId = PrivateLoggingSessionInfo.ClientID;
                rp.Parameters.MicroNumberNo = "Vol.0" + rp.Parameters.MicroNumber;//自动生成微刊编号
                PrivateEclubMicroNumberBLL.Update(rp.Parameters, false);

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
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueNperDeleteRP>>();

            // 参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<MicroIssueNperDeleteRD>(new MicroIssueNperDeleteRD());

            try
            {
                // Access DB Result
                PrivateEclubMicroNumberBLL.Delete(rp.Parameters.NumberIds.Split(','));
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
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailAddRP>>();

            // 验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<MicroIssueDetailAddRD>(new MicroIssueDetailAddRD());

            try
            {
                // Access DB Result
                rp.Parameters.CustomerId = PrivateLoggingSessionInfo.ClientID;
                PrivateEclubMicroBLL.Create(rp.Parameters);
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
            // 反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailUpdateRP>>();

            // 验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<MicroIssueDetailUpdateRD>(new MicroIssueDetailUpdateRD());

            try
            {
                // Access DB Result
                rp.CustomerID = PrivateLoggingSessionInfo.ClientID;
                PrivateEclubMicroBLL.Update(rp.Parameters);
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
            //反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<MicroIssueDetailDeleteRP>>();

            // 验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<MicroIssueDetailDeleteRD>(new MicroIssueDetailDeleteRD());

            try
            {
                // Access DB Result
                PrivateEclubMicroBLL.Delete(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 5.13 获取期刊列表
        protected string DoGetNumberList(string pRequest)
        {
            //反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNumberListRP>>();

            // 验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<GetNumberListRD>(new GetNumberListRD());

            try
            {
                // Access DB Result
                int pageCount = 0;
                int rowCount = 0;
                rd.Data.Page = PrivateEclubMicroNumberBLL.GetNumberList(rp.Parameters.Number, rp.Parameters.Keyword, rp.Parameters.SortField, rp.Parameters.SortOrder, rp.Parameters.PageIndex, rp.Parameters.PageSize, ref pageCount, ref rowCount);
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

        #region 5.14 获取期刊列表
        protected string DoGetNumberTypeSum(string pRequest)
        {
            //反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetNumberTypeSumRP>>();

            // 验证参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            // 拼装响应数据
            var rd = new APIResponse<GetNumberTypeSumRD>(new GetNumberTypeSumRD());

            try
            {
                // Access DB Result
                rd.Data.Summarys = PrivateLNumberTypeMappingBLL.GetNumberTypeSum(rp.Parameters.NumberId);
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
                case "MicroIssueTypeAdd":
                    rst = this.DoMicroIssueTypeAdd(pRequest);
                    break;
                case "MicroIssueTypeUpdate":
                    rst = this.DoMicroIssueTypeUpdate(pRequest);
                    break;
                case "MicroIssueTypeDelete":
                    rst = this.DoMicroIssueTypeDelete(pRequest);
                    break;
                case "MicroIssueNperAdd":
                    rst = this.DoMicroIssueNperAdd(pRequest);
                    break;
                case "MicroIssueNperUpdate":
                    rst = this.DoMicroIssueNperUpdate(pRequest);
                    break;
                case "MicroIssueNperDelete":
                    rst = this.DoMicroIssueNperDelete(pRequest);
                    break;
                case "MicroIssueDetailAdd":
                    rst = this.DoMicroIssueDetailAdd(pRequest);
                    break;
                case "MicroIssueDetailUpdate":
                    rst = this.DoMicroIssueDetailUpdate(pRequest);
                    break;
                case "MicroIssueDetailDelete":
                    rst = this.DoMicroIssueDetailDelete(pRequest);
                    break;
                case "GetNumberList":
                    rst = this.DoGetNumberList(pRequest);
                    break;
                case "MicroIssueTypeDetail":
                    rst = this.DoMicroIssueTypeDetail(pRequest);
                    break;
                case "MicroNumberDetail":
                    rst = this.DoMicroNumberDetail(pRequest);
                    break;
                case "GetNumberTypeSum":
                    rst = this.DoGetNumberTypeSum(pRequest);
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