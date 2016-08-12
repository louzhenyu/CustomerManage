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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Vip
{
    /// <summary>
    /// VipGateway 的摘要说明
    /// </summary>
    public class VipGateway : BaseGateway
    {
        #region 获取O2O业务系统会员统计

        public string GetVipTotal(string pRequest)
        {
            //var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new GetVipTotalRD();

            var vipBll = new VipBLL(loggingSessionInfo);

            var ds = vipBll.VipLandingPage(loggingSessionInfo.ClientID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new ThatDayAndMonthVipInfo
                {
                    TodayVipCount = Convert.ToInt32(t["NewMemberCountToday"]),//今天新增vip
                    AddRatioByDay = t["NewMemberPercentByDay"].ToString(),
                    MonthVipCount = Convert.ToInt32(t["NewMemberCountThisMonth"]),
                    AddRatioByMonth = t["NewMemberPercentByMonth"].ToString(),
                    CashOnDeliveryCount = Convert.ToInt32(t["CashOnDeliveryCount"]),
                    OffShelfCount = Convert.ToInt32(t["OffShelfCount"]),
                    OnlineUnitCount = Convert.ToInt32(t["OnlineUnitCount"]),
                    PaidNotSentCount = Convert.ToInt32(t["PaidNotSentCount"]),
                    SentCount = Convert.ToInt32(t["SentCount"]),
                    UpShelfCount = Convert.ToInt32(t["UpShelfCount"])
                }).FirstOrDefault();
                rd.VipInfo = temp;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                var tmp = ds.Tables[1].AsEnumerable().Select(t => new AttentionVipInfo
                {
                    Date = Convert.ToDateTime(t["PDate"]).ToShortDateString(),// t["PDate"].ToString(),
                    CumulativeNo = Convert.ToInt32(t["VipNumber"])

                });

                rd.AttentionVipList = tmp.ToArray();
            }
            //if (ds.Tables[2].Rows.Count > 0)
            //{
            //    var eventList = ds.Tables[2].AsEnumerable().Select(t => new EventAnalysisInfo
            //    {
            //        //EventID,Title,EventPeriod,QRCodeVipAmount,SaleAmount,TransferAmount,
            //        //RecommendedAmount,RegisteredAmount,PurchasedAmount

            //        EventId = t["EventID"].ToString(),
            //        Title = t["Title"].ToString(),
            //        EventTime = t["EventPeriod"].ToString(),
            //        DecodeNo = Convert.ToInt32(t["QRCodeVipAmount"]),
            //        VipNo = Convert.ToInt32(t["RegisteredAmount"]),
            //        ForwardingNo = Convert.ToInt32(t["TransferAmount"]),
            //        ForwardingSignNo = Convert.ToInt32(t["RecommendedAmount"]),
            //        SalesVipNo = Convert.ToInt32(t["PurchasedAmount"]),
            //        SalesNo = Convert.ToInt32(t["SaleAmount"]),
            //    });

            //    rd.EventAnalysisList = eventList.ToArray();
            //}
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        /// <summary>
        /// 获取新增会员动态配置属性
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCreateVipPropList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipBLL(loggingSessionInfo);
            var result = bll.GetCreateVipPropList(loggingSessionInfo.ClientID, loggingSessionInfo.UserID);
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            xml.LoadXml(result);
            result = ProcessXml.XmlToJSON2(xml);
            //result = new JavaScriptSerializer().Serialize(xml);
            if (result == null)
            {
                return "{\"ResultCode\":122,\"Message\":\"没有客户化配置数据.\",\"IsSuccess\":false,\"Data\":null}";
            }
            else
            {
                return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":" + result + "}";
            }
        }
        public string GetVipLogs(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;
            var bll = new VipLogBLL(loggingSessionInfo);
            var ds = bll.GetVipLogs(pageIndex ?? 1, pageSize ?? 15, rp.Parameters.VipId);
            var viplogs = new List<VipLogInfo>();
            var rd = new VipLogInfoRD();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.TotalCount = (int)ds.Tables[0].Rows[0]["totalCount"];
                rd.TotalPages = rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(rd.TotalCount * 1.00 / (pageSize ?? 15) * 1.00)));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var log = new VipLogInfo();
                    log.logid = ds.Tables[0].Rows[i]["logid"].ToString();
                    log.action = ds.Tables[0].Rows[i]["action"].ToString();
                    log.cu_name = ds.Tables[0].Rows[i]["cu_name"].ToString();
                    log.createtime = ((DateTime)ds.Tables[0].Rows[i]["createtime"]).ToString();
                    viplogs.Add(log);
                }
            }
            rd.VipLogs = viplogs.ToArray();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string UpdateVipInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipEntityRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            var bll = new VipBLL(loggingSessionInfo);
            try
            {
                bll.UpdateVipInfo(rp.Parameters.VipId, rp.Parameters.Columns);
                var rsp = new SuccessResponse<EmptyResponseData>();
                return rsp.ToJSON();
            }
            catch (APIException ex)
            {
                throw new APIException("更新出错.");
            }
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteVip(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<DeleteVipRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipBLL(loggingSessionInfo);
            try
            {
                bll.DeleteVip(rp.Parameters.VipIds);
                var rsp = new SuccessResponse<EmptyResponseData>();
                return rsp.ToJSON();
            }
            catch (APIException ex)
            {
                throw new APIException("删除出错.") { ErrorCode = 121 };
            }
        }
        #region 获取会员查询动态配置列表
        public string GetVipSearchPropList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var bll = new VipBLL(loggingSessionInfo);

            var result = bll.GetVipSearchPropList(loggingSessionInfo.ClientID, "Vip", loggingSessionInfo.CurrentUserRole.UnitId);

            //var result = bll.GetVipSearchPropList(loggingSessionInfo.ClientID, "Vip");


            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            xml.LoadXml(result);
            result = ProcessXml.XmlToJSON2(xml);
            if (result == null)
            {
                return "{\"ResultCode\":122,\"Message\":\"没有客户化配置数据.\",\"IsSuccess\":false,\"Data\":null}";
            }
            else
            {
                return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":" + result + "}";
            }
        }
        #endregion

        /// <summary>
        /// 获取会员标签类型列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetVipTagTypeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipTagTypeList();
            var rd = new GetVipTagTypeListRD();
            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new VipTagTypeInfo()
                {
                    TagTypeId = t["TypeId"].ToString(),
                    TagTypeName = t["TypeName"].ToString()
                });
                rd.VipTagTypeList = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        //导出会员交易记录
        public void ExportVipOrderList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipOrderList(rp.Parameters.VipId, loggingSessionInfo.ClientID, 1, Int32.MaxValue, rp.Parameters.OrderType);
            var columns = new Dictionary<string, string>();
            columns.Add("order_no", "订单编号");
            columns.Add("create_time", "交易时间");
            columns.Add("vipsourcename", "下单方式");
            columns.Add("UnitName", "交易门店");
            columns.Add("actual_amount", "交易金额");
            columns.Add("PayStatus", "支付状态");
            columns.Add("payTypeName", "支付方式");
            columns.Add("status_desc", "订单状态");
            ExportVipInfo(columns, ds.Tables[0], "会员交易记录");
        }
        //导出会员上线与下线
        public void ExportVipOnlineOffline(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipOnlineOfflineRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.VipId))
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            var si = new SessionManager().CurrentUserLoginInfo;
            //var si = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            if (null == si)
                si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new VipBLL(si);
            var ds = bll.GetVipOnlineOffline(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
            var columns = new Dictionary<string, string>();
            columns.Add("vipcode", "会员编号");
            columns.Add("vipname", "微信昵称");
            columns.Add("viprealname", "姓名");
            columns.Add("vipcardgradename", "等级");
            columns.Add("endintegral", "积分");
            columns.Add("highercount", "下线数");
            columns.Add("createtime", "入会时间");
            ExportVipInfo(columns, ds.Tables[1], "会员上线与下线");
        }
        //导出会员操作日志
        public void ExportVipLogs(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipLogBLL(loggingSessionInfo);
            var ds = bll.GetVipLogs(0, 15, rp.Parameters.VipId);
            var columns = new Dictionary<string, string>();
            columns.Add("createtime", "时间");
            columns.Add("cu_name", "操作人");
            columns.Add("action", "操作事项");
            ExportVipInfo(columns, ds.Tables[0], "会员操作记录");
        }
        //导出会员消费卡列表
        public void ExportVipConsumerCard(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipBLL(loggingSessionInfo);
            var rd = new VipConsumeCardInfoRD();
            var ds = bll.GetVipConsumeCardList(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
            var columns = new Dictionary<string, string>();
            columns.Add("CouponTypeName", "卡类型");
            columns.Add("CoupnName", "卡名称");
            columns.Add("OptionText", "领卡方式");
            columns.Add("CouponDesc", "备注");
            columns.Add("CouponStatus", "状态");
            ExportVipInfo(columns, ds.Tables[1], "会员消费卡列表");
        }
        //导出会员余额列表
        public void ExportVipAmount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession("e703dbedadd943abacf864531decdac1", "7c292994c45143028cbf0b60c9555aec");
            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipAmountList(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
            var columns = new Dictionary<string, string>();
            columns.Add("createtime", "时间");
            columns.Add("Amount", "余额");
            columns.Add("optiontext", "变更类型");
            columns.Add("Remark", "备注");
            ExportVipInfo(columns, ds.Tables[0], "会员余额列表");
        }
        //导出积分列表
        public void ExportVipIntegral(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipBLL(loggingSessionInfo);
            var rd = new GetVipIntegralInfoRD();
            var ds = bll.GetVipIntegralList(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
            var columns = new Dictionary<string, string>();
            columns.Add("CreateTime", "时间");
            columns.Add("Integral", "积分变更");
            columns.Add("IntegralSource", "变更类型");
            columns.Add("Remark", "备注");
            ExportVipInfo(columns, ds.Tables[0], "会员积分列表");
        }
        /// <summary>
        /// 导出会员列表
        /// </summary>
        /// <param name="pRequest"></param>
        public void ExportVipList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipSearchListRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.SortType))
                rp.Parameters.SortType = "DESC";
            if (string.IsNullOrEmpty(rp.Parameters.OrderBy))
                rp.Parameters.OrderBy = "CREATETIME";
            rp.Parameters.PageIndex = 0;
            rp.Parameters.PageSize = 10;
            var si = new SessionManager().CurrentUserLoginInfo;
            //var si = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            if (null == si)
                si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new VipBLL(si);
            var ds = bll.SearchVipList(si.ClientID, si.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize,
                                        rp.Parameters.OrderBy, rp.Parameters.SortType, rp.Parameters.SearchColumns,
                                        rp.Parameters.VipSearchTags);
            var columns = GetColumns(ds.Tables[0]);
            ExportVipInfo(columns, ds.Tables[1], "会员列表");
        }

        //#region 导出卡号
        ///// <summary>
        ///// 导出卡号
        ///// </summary>
        ///// <param name="pRequest"></param>
        //public void ExportVipCardCode(string pRequest)
        //{
        //    var rp = pRequest.DeserializeJSONTo<APIRequest<ExportVipCardCodeRP>>();
        //    if (!string.IsNullOrWhiteSpace(rp.Parameters.BatchNo))
        //    {
        //        var si = new SessionManager().CurrentUserLoginInfo;
        //        if (null == si)
        //            si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
        //        var VipCardBLL = new VipCardBLL(si);
        //        var ds = VipCardBLL.ExportVipCardCode(rp.Parameters.BatchNo);
        //        var columns = new Dictionary<string, string>();
        //        columns.Add("VipCardCode", "卡号");
        //        ExportVipInfo(columns, ds.Tables[0], "导出卡号");
        //    }
        //}
        //#endregion
        /// <summary>
        ///  从dataTable数据转成列的定义到数据字典
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="dt"></param>
        private Dictionary<string, string> GetColumns(DataTable dt)
        {
            var columns = new Dictionary<string, string>();
            if (null != dt && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    columns.Add(dt.Rows[i]["ColumnName"].ToString(), dt.Rows[i]["ColumnDesc"].ToString());
            }
            return columns;
        }

        #region 获取会员标签集合
        public string GetVipTagList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var bll = new VipBLL(loggingSessionInfo);
            var ds = bll.GetVipTagList(loggingSessionInfo.ClientID);
            var typeDs = bll.GetVipTagTypeList();
            var rd = new GetVipTagListRD();
            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new VipTagInfo()
                {
                    TagTypeId = t["TypeId"].ToString(),
                    TagId = t["TagsId"].ToString(),
                    TagName = t["TagsName"].ToString(),
                    TagDesc = t["TagsDesc"].ToString()
                });
                rd.VipTagList = temp.ToArray();
            }
            if (typeDs.Tables[0].Rows.Count > 0)
            {
                var typeTemp = typeDs.Tables[0].AsEnumerable().Select(t => new VipTagTypeInfo()
                {
                    TagTypeId = t["TypeId"].ToString(),
                    TagTypeName = t["TypeName"].ToString()
                });
                rd.VipTagTypeList = typeTemp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 更新会员信息
        public string GetUpdateVipInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUpdateVipInfoRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var bll = new VipBLL(loggingSessionInfo);
            var vipEntity = bll.GetByID(rp.Parameters.VipId);

            //if (vipEntity != null)
            //{
            //    vipEntity.Phone = string.IsNullOrEmpty(rp.Parameters.Phone) ? vipEntity.Phone : rp.Parameters.Phone;
            //    vipEntity.VipName = string.IsNullOrEmpty(rp.Parameters.VipName) ? vipEntity.VipName : rp.Parameters.VipName;
            //    vipEntity.VipRealName = string.IsNullOrEmpty(rp.Parameters.VipRealName) ? vipEntity.VipRealName : rp.Parameters.VipRealName;

            //}
            var rd = new GetUpdateVipInfoRD();
            rd.VipInfo = vipEntity;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 导出会员信息
        private void ExportVipInfo(Dictionary<string, string> columns, DataTable dt, string fileName)
        {
            #region /// comment
            //Dictionary<string, string> exportFields = new Dictionary<string, string>();
            //exportFields.Add("VipCode", "会员编号");
            //exportFields.Add("VipName", "微信昵称");
            //exportFields.Add("VipRealName", "会员姓名");
            //exportFields.Add("UnitName", "会籍店");
            //exportFields.Add("Status", "状态");
            //exportFields.Add("VipLevel", "等级");
            //exportFields.Add("Integral", "积分");
            //exportFields.Add("Phone", "手机号");

            #region 替换标题信息
            DataColumn dc = new DataColumn();
            dt = dt.DefaultView.ToTable(false, columns.Keys.ToArray());

            foreach (DataColumn c in dt.Columns)
            {
                string title;
                columns.TryGetValue(c.ColumnName, out title);
                c.ColumnName = title;
            }
            #endregion
            ///数据获取
            #endregion
            Workbook wb = DataTableExporter.WriteXLS(dt, 0);
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/Vip");
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }
            savePath = savePath + "\\" + fileName + DateTime.Now.ToFileTime() + ".xls";
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
            HttpContext.Current.Response.End();
        }

        #endregion
        #region 获取会员信息
        public string GetVipDetailInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                //throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };

                //刷卡查询业务处理 Update by Henry 2015-8-27
                if (rp.Parameters.VipCardISN == null || string.IsNullOrEmpty(rp.Parameters.VipCardISN))
                    throw new APIException("缺少请求参数") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                var vipCardBLL = new VipCardBLL(loggingSessionInfo);//会员卡业务对象示例化
                var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);//会员卡和会员映射业务对象实例化
                //获取会员卡信息
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardISN = rp.Parameters.VipCardISN }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    var mappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VipCardID = vipCardInfo.VipCardID }, null).FirstOrDefault();
                    if (mappingInfo != null)
                        rp.Parameters.VipId = mappingInfo.VIPID;//重新赋值VipID
                }
            }

            var bll = new VipBLL(loggingSessionInfo);

            var ds = bll.GetVipDetailInfo(rp.Parameters.VipId, loggingSessionInfo.ClientID);

            var rd = new GetVipDetailInfoRD();
            rd.VipDetailInfo = new VipInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.VipDetailInfo.VipId = ds.Tables[0].Rows[0]["VipId"].ToString();
                rd.VipDetailInfo.VipNo = ds.Tables[0].Rows[0]["VipCode"].ToString();
                rd.VipDetailInfo.VipRealName = ds.Tables[0].Rows[0]["VipRealName"].ToString();
                rd.VipDetailInfo.VipName = ds.Tables[0].Rows[0]["VipName"].ToString();
                rd.VipDetailInfo.VipLevel = ds.Tables[0].Rows[0]["vipcardgradename"].ToString();
                rd.VipDetailInfo.UnitId = ds.Tables[0].Rows[0]["CouponInfo"].ToString();
                rd.VipDetailInfo.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();
                rd.VipDetailInfo.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                var integral = ds.Tables[0].Rows[0]["Integration"].ToString();
                if (string.IsNullOrEmpty(integral))
                    integral = "0";
                rd.VipDetailInfo.VipIntegral = Convert.ToDecimal(integral);

                //获取累计积分
                var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                var IntegralEntity = vipIntegralBLL.GetByID(rp.Parameters.VipId);
                if (IntegralEntity != null)
                    rd.VipDetailInfo.CumulativeIntegral = IntegralEntity.CumulativeIntegral != null ? IntegralEntity.CumulativeIntegral.Value : 0;

                var vipAmountBll = new VipAmountBLL(loggingSessionInfo);
                var vipAmountEntity = vipAmountBll.GetByID(rp.Parameters.VipId);
                if (vipAmountEntity != null)
                {
                    rd.VipDetailInfo.VipEndAmount = vipAmountEntity.EndAmount ?? 0;
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                var tags = new List<VipTag>();
                var tagTbl = ds.Tables[1];
                for (int i = tagTbl.Rows.Count - 1; i >= 0; i--)
                {
                    var tag = new VipTag();
                    tag.TagDesc = tagTbl.Rows[i]["TagsDesc"].ToString();
                    tag.TagId = tagTbl.Rows[i]["TagsId"].ToString();
                    tag.TagName = tagTbl.Rows[i]["TagsName"].ToString();
                    tags.Add(tag);
                }
                rd.VipTags = tags.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        /// <summary>
        /// 根据客户动态配置的属性，获取会员信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetExistVipInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                //throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
                //刷卡查询业务处理 Update by Henry 2015-8-27
                if (rp.Parameters.VipCardISN == null || string.IsNullOrEmpty(rp.Parameters.VipCardISN))
                    throw new APIException("缺少请求参数") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                var vipCardBLL = new VipCardBLL(loggingSessionInfo);//会员卡业务对象示例化
                var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);//会员卡和会员映射业务对象实例化
                //获取会员卡信息
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardISN = rp.Parameters.VipCardISN }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    var mappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VipCardID = vipCardInfo.VipCardID }, null).FirstOrDefault();
                    if (mappingInfo != null)
                        rp.Parameters.VipId = mappingInfo.VIPID;//重新赋值VipID
                }
            }
            if (null == loggingSessionInfo)
                loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);// "1");
            var bll = new VipBLL(loggingSessionInfo);
            var rd = new GetExistVipInfoRD();
            var ds = bll.GetExistVipInfo(loggingSessionInfo.ClientID, loggingSessionInfo.UserID, rp.Parameters.VipId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.LoadXml(ds.Tables[0].Rows[0][0].ToString());
                rd.JsonColumns = ProcessXml.XmlToJSON2(xml);
            }
            rd.VipInfo = ds.Tables[1];
            if (rd.VipInfo.Columns.Contains("Phone"))
            {
                for (int i = 0; i < rd.VipInfo.Rows.Count; i++)
                {
                    var phone = rd.VipInfo.Rows[i]["Phone"].ToString();
                    if (!string.IsNullOrEmpty(phone))
                    {
                        rd.VipInfo.Rows[i]["Phone"] = phone.Substring(0, 3) + "****" + phone.Substring(7);
                    }
                }
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #region 获取会员账户金额
        public string GetVipAmountList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            var bll = new VipBLL(loggingSessionInfo);

            var rd = new GetVipAmountInfoRD();

            var ds = bll.GetVipAmountList(rp.Parameters.VipId, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new VipAmountInfo()
                    {
                        VipAmountId = t["VipAmountDetailId"].ToString(),
                        VipAmountSource = t["optiontext"].ToString(),
                        Date = t["createtime"].ToString(),
                        Amount = Convert.ToDecimal(t["Amount"].ToString()),
                        Remark = t["Remark"].ToString()
                    });
                rd.VipAmountList = temp.ToArray();
                ds = bll.GetVipAmountList(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));

            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        #region 获取会员积分信息
        public string GetVipIntegralList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var bll = new VipBLL(loggingSessionInfo);

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            var rd = new GetVipIntegralInfoRD();

            var ds = bll.GetVipIntegralList(rp.Parameters.VipId, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new VipIntegralInfo()
                {
                    VipIntegralId = t["VIpIntegralDetailId"].ToString(),
                    VipIntegralSource = t["IntegralSource"].ToString(),
                    Date = t["CreateTime"].ToString(),
                    Integral = Convert.ToDecimal(t["Integral"].ToString()),
                    Remark = t["Remark"].ToString(),
                    UnitName=t["UnitName"].ToString(),
                    Reason=t["Reason"].ToString(),
                    IntegralSourceId = t["IntegralSourceId"].ToString(),
                    CreateBy=t["CreateBy"].ToString()
                });
                rd.VipIntegralList = temp.ToArray();

                //人工调整时，显示操作人
                var userBLL = new T_UserBLL(loggingSessionInfo);
                T_UserEntity userInfo = null;
                foreach (var item in rd.VipIntegralList)
                {
                    if (item.IntegralSourceId == "27")//人工调整
                    {
                        userInfo = userBLL.GetByID(item.CreateBy);
                        item.CreateByName = userInfo != null ? userInfo.user_name : "";
                    }
                }

                ds = bll.GetVipIntegralList(rp.Parameters.VipId, 1, Int32.MaxValue, rp.Parameters.OrderType);
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 获取会员交易记录
        public string GetVipOrderList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            var bll = new VipBLL(loggingSessionInfo);

            var rd = new VipOrderInfoRD();

            var ds = bll.GetVipOrderList(rp.Parameters.VipId, loggingSessionInfo.ClientID, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new VipOrderInfo()
                {
                    OrderId = t["order_id"].ToString(),
                    OrderNo = t["order_no"].ToString(),
                    OrderStatus = t["status_desc"].ToString(),
                    PayAmount = t["actual_amount"] as decimal?,
                    PayStatus = t["PayStatus"].ToString(),
                    PayType = t["payTypeName"].ToString(),
                    PayUnitId = t["vipsourcename"].ToString(),
                    PayUnitName = t["UnitName"].ToString(),
                    CreateTime = t["create_time"].ToString(),
                    VipCardCode = t["VipCardCode"].ToString(),
                    TotalAmount =Convert.ToDecimal(t["total_amount"].ToString())
                });
                rd.VipOrderList = temp.ToArray();
                ds = bll.GetVipOrderList(rp.Parameters.VipId, loggingSessionInfo.ClientID, 1, Int32.MaxValue, rp.Parameters.OrderType);
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));

            }


            #region test short url
            //string[] urls = new[] { "http://www.o2omarketing.cn/", "http://www.cnblogs.com/top5/archive/2010/03/13/1685292.html", "http://www.cnblogs.com/shadowtale/p/3372735.html" };
            //var urlBll = new UrlBLL(loggingSessionInfo);
            //var entities = urlBll.GetEntitysByLongUrl(urls, loggingSessionInfo.ClientID);
            //string debugStr = "test string";
            #endregion

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region 获取会员消费卡记录
        public string GetVipConsumeCardList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipDetailInfoRP>>();

            if (rp.Parameters.VipId == null || string.IsNullOrEmpty(rp.Parameters.VipId))
            {
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            }
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            var bll = new VipBLL(loggingSessionInfo);

            var rd = new VipConsumeCardInfoRD();
            var ds = bll.GetVipConsumeCardList(rp.Parameters.VipId, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderType);
            rd.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(rd.TotalCount * 1.00 / (pageSize ?? 15) * 1.00)));
            if (ds.Tables[1].Rows.Count > 0)
            {
                var temp = ds.Tables[1].AsEnumerable().Select(t => new VipConsumeCardInfo()
                {
                    CouponId = t["CouponID"].ToString(),
                    CouponCode=t["CouponCode"].ToString(),
                    CouponName = t["CoupnName"].ToString(),
                    CouponType = t["CouponTypeName"].ToString(),
                    CouponStatus = t["CouponStatus"].ToString(),
                    CollarCardMode = t["OptionText"].ToString(),
                    Remark = t["CouponDesc"].ToString(),
                    EndDate=t["EndDate"].ToString()
                });
                rd.VipConsumeCardList = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        private string AddVip(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipEntityRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            var bll = new VipBLL(loggingSessionInfo);
            try
            {
                bll.InsertVipEntity(rp.Parameters.Columns, loggingSessionInfo.ClientID);
                var rsp = new SuccessResponse<EmptyResponseData>();
                return rsp.ToJSON();
            }
            catch (APIException ex)
            {
                throw new APIException("添加失败");
            }
        }
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetVipLogs":
                    rst = this.GetVipLogs(pRequest);
                    break;
                case "GetExistVipInfo":
                    rst = this.GetExistVipInfo(pRequest);
                    break;
                case "UpdateVipInfo":
                    rst = this.UpdateVipInfo(pRequest);
                    break;
                case "AddVip":
                    rst = this.AddVip(pRequest);
                    break;
                case "DeleteVip":
                    rst = this.DeleteVip(pRequest);
                    break;
                case "GetVipOnlineOffline":
                    rst = this.GetVipOnlineOffline(pRequest);
                    break;
                case "ExportVipLogs":
                    this.ExportVipLogs(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipOnlineOffline":
                    this.ExportVipOnlineOffline(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipConsumerCard":
                    this.ExportVipConsumerCard(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipAmount":
                    this.ExportVipAmount(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipIntegral":
                    this.ExportVipIntegral(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipOrderList":
                    this.ExportVipOrderList(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "ExportVipList":
                    this.ExportVipList(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "GetVipInfoList":
                    rst = this.GetVipInfoList(pRequest);
                    break;
                case "GetVipTotal":
                    rst = this.GetVipTotal(pRequest);
                    break;
                case "GetCreateVipPropList":
                    rst = this.GetCreateVipPropList(pRequest);
                    break;
                case "GetVipSearchPropList":
                    rst = this.GetVipSearchPropList(pRequest);
                    break;
                case "GetVipTagTypeList":
                    rst = this.GetVipTagTypeList(pRequest);
                    break;
                case "GetVipTagList":
                    rst = this.GetVipTagList(pRequest);
                    break;
                case "GetVipDetail":
                    rst = this.GetVipDetailInfo(pRequest);
                    break;
                case "GetUpdateVipInfo":
                    rst = this.GetUpdateVipInfo(pRequest);
                    break;
                case "GetVipAmountList":
                    rst = this.GetVipAmountList(pRequest);
                    break;
                case "GetVipIntegralList":
                    rst = this.GetVipIntegralList(pRequest);
                    break;
                case "GetVipOrderList":
                    rst = this.GetVipOrderList(pRequest);
                    break;
                case "GetVipConsumeCardList":
                    rst = this.GetVipConsumeCardList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }

        private string GetVipOnlineOffline(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipOnlineOfflineRP>>();
            if (rp.Parameters.PageIndex <= 0) rp.Parameters.PageIndex = 1;
            if (rp.Parameters.PageSize <= 0) rp.Parameters.PageSize = 15;
            if (string.IsNullOrEmpty(rp.Parameters.VipId))
                throw new APIException("请求参数中缺少VipId或值为空.") { ErrorCode = 121 };
            var si = new SessionManager().CurrentUserLoginInfo;
            //var si = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            if (null == si)
                si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new VipBLL(si);
            var ds = bll.GetVipOnlineOffline(rp.Parameters.VipId, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.OrderType);
            var rd = new GetVipOnlineOfflineRD();
            rd.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(rd.TotalCount * 1.00 / (rp.Parameters.PageSize) * 1.00)));
            if (ds.Tables[1].Rows.Count > 0)
            {
                var temp = ds.Tables[1].AsEnumerable().Select(t => new VipOnlineOffline()
                {
                    // v.vipid,v.vipcode,v.vipname,v.viprealname,g.vipcardgradename,
                    //p.endintegral,x.highercount offlineCount,v.createtime
                    VipId = t["vipid"].ToString(),
                    VipCode = t["vipcode"].ToString(),
                    VipName = t["vipname"].ToString(),
                    VipRealName = t["viprealname"].ToString(),
                    VipCardGradeName = t["vipcardgradename"].ToString(),
                    EndIntegral = t["endintegral"] as decimal?,
                    OfflineCount = t["highercount"] as int?,
                    CreateTime = t["createtime"].ToString(),
                });
                rd.VipOnlineOfflines = temp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 查询会员列表信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetVipInfoList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipSearchListRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.SortType))
                rp.Parameters.SortType = "DESC";
            if (string.IsNullOrEmpty(rp.Parameters.OrderBy))
                rp.Parameters.OrderBy = "CREATETIME";
            if (rp.Parameters.PageIndex <= 0)
                rp.Parameters.PageIndex = 1;
            if (rp.Parameters.PageSize <= 0)
                rp.Parameters.PageSize = 15;
            var si = new SessionManager().CurrentUserLoginInfo;
            //var si = Default.GetBSLoggingSession(rp.CustomerID, "d601bdeaed614c35bf792a2aeda640fa");// "1");
            //si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bll = new VipBLL(si);
            var ds = bll.SearchVipList(si.ClientID, si.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize,
                                        rp.Parameters.OrderBy, rp.Parameters.SortType, rp.Parameters.SearchColumns,
                                        rp.Parameters.VipSearchTags);
            if (ds == null || ds.Tables.Count < 2)
            {
                throw new APIException("客户没有配置查询列信息.") { ErrorCode = 121 };
            }
            var rd = new VipSearchListRD();
            rd.VipTable = ds.Tables[1];
            rd.Columns = GetColumns(ds.Tables[0]);

            if (rd.VipTable.Rows.Count > 0)
            {
                for (int i = rd.VipTable.Rows.Count - 1; i >= 0; i--)
                {

                    if (rd.VipTable.Columns.Contains("CreateTime"))
                    {
                        // dt.Columns["ChkYn"].DataType=Type.GetType("System.bool");
                        // rd.VipTable.Columns["CreateTime"].DataType = Type.GetType("System.string");
                        var CreateTime = rd.VipTable.Rows[i]["CreateTime"] == null || rd.VipTable.Rows[i]["CreateTime"] is DBNull ? "" : rd.VipTable.Rows[i]["CreateTime"].ToString();
                        if (!string.IsNullOrEmpty(CreateTime))
                        {
                            var tempTime = Convert.ToDateTime(rd.VipTable.Rows[i]["CreateTime"]).ToString("yyyy-MM-dd"); ;//隐藏中间的几列***
                            rd.VipTable.Rows[i]["CreateTime"] = tempTime;
                        }
                        rd.VipTable.AcceptChanges();
                    }
                    if (rd.VipTable.Columns.Contains("Birthday"))
                    {

                        var Birthday = rd.VipTable.Rows[i]["Birthday"] == null || rd.VipTable.Rows[i]["Birthday"] is DBNull ? "" : rd.VipTable.Rows[i]["Birthday"].ToString();
                        if (!string.IsNullOrEmpty(Birthday))
                        {
                            rd.VipTable.Rows[i]["Birthday"] = Convert.ToDateTime(rd.VipTable.Rows[i]["Birthday"]).ToString("yyyy-MM-dd");//隐藏中间的几列***
                        }

                    }
                    if (rd.VipTable.Columns.Contains("Phone"))
                    {
                        var phone = rd.VipTable.Rows[i]["Phone"].ToString();
                        if (!string.IsNullOrEmpty(phone) && phone.Length > 8)
                        {
                            rd.VipTable.Rows[i]["Phone"] = phone.Substring(0, 3) + "****" + phone.Substring(7);

                        }
                    }
                }
                rd.TotalPageCount = (int)rd.VipTable.Rows[0]["PageCount"];
                rd.PageIndex = Convert.ToInt32(rd.VipTable.Rows[0]["PID"]);
                rd.TotalCount = Convert.ToInt32(rd.VipTable.Rows[0]["Rnt"]);
            }



            else
            {
                rd.PageIndex = rd.TotalPageCount = rd.TotalCount = 0;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
    }

    #region VipTotal

    public class GetVipTotalRD : IAPIResponseData
    {
        public ThatDayAndMonthVipInfo VipInfo { get; set; }
        public AttentionVipInfo[] AttentionVipList { get; set; }
        public EventAnalysisInfo[] EventAnalysisList { get; set; }
    }

    public class ThatDayAndMonthVipInfo
    {
        /// <summary>
        /// 当天新增会员数,为空则为0
        /// </summary>
        public int TodayVipCount { get; set; }
        /// <summary>
        /// 新增比率
        /// </summary>
        public string AddRatioByDay { get; set; }

        /// <summary>
        /// 当月新增会员数,为空则为0
        /// </summary>
        public int MonthVipCount { get; set; }
        /// <summary>
        /// 新增比率
        /// </summary>
        public string AddRatioByMonth { get; set; }
        /// <summary>
        /// 当日已发货订单数
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// 已支付待发货订单数
        /// </summary>
        public int PaidNotSentCount { get; set; }
        /// <summary>
        /// 货到付款待发货订单数
        /// </summary>
        public int CashOnDeliveryCount { get; set; }
        /// <summary>
        /// 上架商品数
        /// </summary>
        public int UpShelfCount { get; set; }
        /// <summary>
        /// 下架商品数
        /// </summary>
        public int OffShelfCount { get; set; }
        /// <summary>
        /// 上线门店数
        /// </summary>
        public int OnlineUnitCount { get; set; }
    }


    public class AttentionVipInfo
    {
        public int DisplayIndex { get; set; }
        public string Date { get; set; }
        //每天数量
        public int CumulativeNo { get; set; }
    }

    public class EventAnalysisInfo
    {
        public string EventId { get; set; }
        public int DisplayIndex { get; set; }
        public string Title { get; set; }
        //活动时间
        public string EventTime { get; set; }
        //扫描参与
        public int DecodeNo { get; set; }
        //转发数
        public int ForwardingNo { get; set; }
        //转发带来参与者
        public int ForwardingSignNo { get; set; }
        //会员数
        public int VipNo { get; set; }
        //有购买会员数
        public int SalesVipNo { get; set; }
        //销售数
        public int SalesNo { get; set; }

    }

    #endregion

    public class GetVipTagTypeListRD : IAPIResponseData
    {
        public VipTagTypeInfo[] VipTagTypeList { get; set; }
    }
    public class VipTagTypeInfo
    {
        public string TagTypeId { get; set; }
        public string TagTypeName { get; set; }
    }

    public class GetExistVipInfoRD : IAPIResponseData
    {
        public DataTable VipInfo { get; set; }
        public string JsonColumns { get; set; }
    }
    public class GetVipTagListRD : IAPIResponseData
    {
        public VipTagInfo[] VipTagList { get; set; }
        public VipTagTypeInfo[] VipTagTypeList { get; set; }
    }
    public class VipTagInfo
    {
        public string TagTypeId { get; set; }
        public string TagId { get; set; }
        public string TagName { get; set; }
        public string TagDesc { get; set; }
    }

    public class GetVipDetailInfoRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        /// <summary>
        /// 会员卡内码
        /// </summary>
        public string VipCardISN { get; set; }

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 适用范围：1=购物券2=服务券
        /// </summary>
        public int UsableRange { get; set; }
        /// <summary>
        /// 使用此券的门店
        /// </summary>
        public string ObjectID { get; set; }

        public void Validate()
        {
        }
    }
    public class DeleteVipRP : IAPIRequestParameter
    {
        public string[] VipIds { get; set; }
        public void Validate()
        {
        }
    }
    public class GetVipOnlineOfflineRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public void Validate()
        {
        }
    }

    public class GetVipOnlineOfflineRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipOnlineOffline[] VipOnlineOfflines { get; set; }

    }
    public class GetVipDetailInfoRD : IAPIResponseData
    {
        public VipInfo VipDetailInfo { get; set; }
        public VipTag[] VipTags { get; set; }

    }
    public class GetVipAmountInfoRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipAmountInfo[] VipAmountList { get; set; }
    }

    public class GetVipIntegralInfoRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipIntegralInfo[] VipIntegralList { get; set; }
    }
    public class VipOrderInfoRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipOrderInfo[] VipOrderList { get; set; }
    }

    public class VipConsumeCardInfoRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipConsumeCardInfo[] VipConsumeCardList { get; set; }
    }
    public class VipLogInfoRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public VipLogInfo[] VipLogs { get; set; }
    }
    public class VipLogInfo
    {
        public string logid { get; set; }
        public string cu_name { get; set; }
        public string createtime { get; set; }
        public string action { get; set; }
    }
    public class VipInfo
    {
        public string VipId { get; set; }
        public string VipNo { get; set; }
        public string VipRealName { get; set; }
        public string VipName { get; set; }
        public string VipLevel { get; set; }
        public string UnitName { get; set; }
        public string UnitId { get; set; }
        public decimal VipIntegral { get; set; }
        /// <summary>
        /// 累计积分
        /// </summary>
        public decimal CumulativeIntegral { get; set; }
        public decimal VipEndAmount { get; set; }
        public string Phone { get; set; }

    }
    public class VipTag
    {
        public string TagId { get; set; }
        public string TagName { get; set; }
        public string TagDesc { get; set; }
    }
    public class VipOnlineOffline
    {
        public string VipId { get; set; }
        public string VipCode { get; set; }
        public string VipName { get; set; }
        public string VipRealName { get; set; }
        public string VipCardGradeName { get; set; }
        public decimal? EndIntegral { get; set; }
        public int? OfflineCount { get; set; }
        public string CreateTime { get; set; }
    }
    public class VipIntegralInfo
    {
        public string VipIntegralId { get; set; }
        public string VipIntegralSource { get; set; }
        public string Date { get; set; }
        public decimal Integral { get; set; }
        public string Remark { get; set; }
        public string UnitName { get; set; }
        public string Reason { get; set; }
        public string IntegralSourceId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string CreateByName { get; set; }
    }
    public class VipAmountInfo
    {
        public string VipAmountId { get; set; }
        public string VipAmountSource { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
    }
    public class VipOrderInfo
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string CreateTime { get; set; }
        public string PayUnitId { get; set; }
        public string PayUnitName { get; set; }
        public decimal? PayAmount { get; set; }
        public string PayStatus { get; set; }
        public string PayType { get; set; }
        public string OrderStatus { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal TotalAmount { get; set; }
    }

    public class GetUpdateVipInfoRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        //public string VipRealName { get; set; }
        //public string Phone { get; set; }
        ////  public string UnitId { get; set; }
        //public string VipName { get; set; }
        public void Validate()
        {

        }
    }
    public class GetUpdateVipInfoRD : IAPIResponseData
    {
        public VipEntity VipInfo { get; set; }
    }
    public class VipConsumeCardInfo : IAPIResponseData
    {
        public string CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public string CouponType { get; set; }
        //public decimal ConditionValue { get; set; }
        //public decimal ParValue { get; set; }
        public string CouponStatus { get; set; }
        //public string CouponDate { get; set; }
        public string Remark { get; set; }
        public string CollarCardMode { get; set; }
        public string EndDate { get; set; }

    }
    public class VipEntityRP : IAPIRequestParameter
    {
        public string VipId { get; set; }
        public SearchColumn[] Columns { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 查询会员时的请求参数
    /// </summary>
    public class VipSearchListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 查询会员时的列条件
        /// </summary>
        public SearchColumn[] SearchColumns { get; set; }
        /// <summary>
        /// 查询会员时的标签条件
        /// </summary>
        public VipSearchTag[] VipSearchTags { get; set; }
        /// <summary>
        /// 查询第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页多少项
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 指定排序的列名，可为空
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 是否是降序排列
        /// </summary>
        public string SortType { get; set; }

        public void Validate()
        {

        }
    }
    /// <summary>
    /// 查询会员的返回结果
    /// </summary>
    public class VipSearchListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 查询的总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 当前页对应的表数据
        /// </summary>
        public DataTable VipTable { get; set; }
        /// <summary>
        /// 列定义
        /// </summary>
        public Dictionary<string, string> Columns { get; set; }
    }
}