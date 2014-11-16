using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Data;

namespace JIT.CPOS.Web.Interface.Data
{
    public partial class BaseData : System.Web.UI.Page
    {
        string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);

                switch (dataType)
                {
                    case "getCityList":     //获取某个客户的所有门店所在的城市列表，地级市  qianzhi 2014-03-07
                        content = GetCityList();
                        break;
                    case "getStoreList":    //获取某个客户的某个城市的门店列表  qianzhi 2014-03-07
                        content = GetStoreList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region GetCityList 获取某个客户的所有门店所在的城市列表，地级市  qianzhi 2014-03-07

        /// <summary>
        /// 获取某个客户的所有门店所在的城市列表，地级市
        /// </summary>
        public string GetCityList()
        {
            string content = string.Empty;

            var respData = new GetCityListRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetCityListReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetCityList: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                else
                {
                    respData.code = "103";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetCityListRespContentData();
                respData.content.cityList = new List<CityEntity>();

                DataSet ds = new TUnitSortBLL(loggingSessionInfo).GetCityListByCustomerId(customerId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.cityList = DataTableToObject.ConvertToList<CityEntity>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetCityListRespData : Default.LowerRespData
        {
            public GetCityListRespContentData content;
        }
        public class GetCityListRespContentData
        {
            public IList<CityEntity> cityList { get; set; }     //城市集合
        }
        public class CityEntity
        {
            public string cityName { get; set; }    //城市名称
            public int cityCount { get; set; }      //城市数量
        }
        public class GetCityListReqData : Default.ReqData
        {
            public GetCityListReqSpecialData special;
        }
        public class GetCityListReqSpecialData
        {

        }

        #endregion

        #region GetStoreList 获取某个客户的某个城市的门店列表  qianzhi 2014-03-07

        public string GetStoreList()
        {
            string content = string.Empty;
            var respData = new getStoreListByItemRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetStoreList: {0}", reqContent)
                });

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreListByItemReqData>();
                reqObj = reqObj == null ? new getStoreListByItemReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreListByItemReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 20;
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                else
                {
                    respData.code = "103";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                respData.content = new getStoreListByItemRespContentData();
                respData.content.storeList = new List<getStoreListByItemRespContentItemTypeData>();
                #region
                string strError = string.Empty;

                TUnitSortBLL server = new TUnitSortBLL(loggingSessionInfo);
                var storeInfo = server.GetStoreListByItem(reqObj.special.cityName, reqObj.special.page, reqObj.special.pageSize, reqObj.special.longitude, reqObj.special.latitude, out strError);
                if (strError.Equals("ok"))
                {
                    IList<getStoreListByItemRespContentItemTypeData> list = new List<getStoreListByItemRespContentItemTypeData>();
                    foreach (var store in storeInfo.StoreBrandList)
                    {
                        getStoreListByItemRespContentItemTypeData info = new getStoreListByItemRespContentItemTypeData();
                        info.storeId = ToStr(store.StoreId);
                        info.storeName = ToStr(store.StoreName);
                        info.imageURL = ToStr(store.ImageUrl);
                        info.address = ToStr(store.Address);
                        info.tel = ToStr(store.Tel);
                        info.displayIndex = ToStr(store.DisplayIndex);
                        info.lng = ToStr(store.Longitude);
                        info.lat = ToStr(store.Latitude);
                        if (store.Distance.ToString().Equals("0"))
                        {
                            info.distance = "";
                        }
                        else
                        {
                            info.distance = ToStr(store.Distance) + "km";
                        }
                        list.Add(info);
                    }
                    respData.content.storeList = list;
                    respData.content.totalCount = ToInt(storeInfo.TotalCount);
                }
                else
                {
                    respData.code = "111";
                    respData.description = "数据库操作错误";
                    respData.exception = strError;
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getStoreListByItemRespData : Default.LowerRespData
        {
            public getStoreListByItemRespContentData content { get; set; }
        }
        public class getStoreListByItemRespContentData
        {
            public int totalCount { get; set; }
            public IList<getStoreListByItemRespContentItemTypeData> storeList { get; set; }     //商品类别集合
        }
        public class getStoreListByItemRespContentItemTypeData
        {
            public string storeId { get; set; }
            public string storeName { get; set; }
            public string imageURL { get; set; }
            public string displayIndex { get; set; }
            public string address { get; set; }
            public string tel { get; set; }
            public string distance { get; set; }
            public string lng { get; set; }
            public string lat { get; set; }
        }
        public class getStoreListByItemReqData : Default.ReqData
        {
            public getStoreListByItemReqSpecialData special;
        }
        public class getStoreListByItemReqSpecialData
        {
            public string cityName { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
        }

        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion
    }
}