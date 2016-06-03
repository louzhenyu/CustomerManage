using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Data.SqlClient;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Connection;
using System.Configuration;
using System.Threading;
using JIT.CPOS.BS.BLL.WX;
//using ServiceStack.Redis;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon
{
    public class RedisVipMappingCouponBLL
    {

        /// <summary>
        /// vip绑定coupon入队列
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="strObjectId"></param>
        /// <param name="strVipId"></param>
        /// <param name="strSource"></param>
        public void SetVipMappingCoupon(CC_Coupon coupon, string strObjectId, string strVipId, string strSource)
        {
            RedisCouponBLL redisCouponBLL = new RedisCouponBLL();
            var count = RedisOpenAPI.Instance.CCCoupon().GetCouponListLength(coupon);
            if (count.Code == ResponseCode.Success)
            {
                if (count.Result == 0)
                {
                    LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                    LoggingManager CurrentLoggingManager = new LoggingManager();
                    string strCon=string.Empty;
                    var connection = new RedisConnectionBLL().GetConnection(coupon.CustomerId);
                    if (connection.CustomerID==null)
                    {
                        connection.ConnectionStr = GetCustomerConn(coupon.CustomerId);
                    }
                    _loggingSessionInfo.ClientID = coupon.CustomerId;
                    CurrentLoggingManager.Connection_String = connection.ConnectionStr;
                    _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                    var bllCouponType = new CouponTypeBLL(_loggingSessionInfo);
                    
                    var couponType = bllCouponType.QueryByEntity(new CouponTypeEntity() { CustomerId = coupon.CustomerId,CouponTypeID=new Guid(coupon.CouponTypeId),IsDelete = 0 }, null).SingleOrDefault();
                    if (couponType != null)
                    {

                        int intCouponLenth = Convert.ToInt32(couponType.IssuedQty)- Convert.ToInt32(couponType.IsVoucher);
                        if (intCouponLenth <= 0)
                        {
                            intCouponLenth = 1000;

                            bllCouponType.UpdateCouponTypeIssuedQty(coupon.CouponTypeId, intCouponLenth);
                        }
                        
                        RedisOpenAPI.Instance.CCCoupon().SetCouponList(new CC_Coupon()
                                    {
                                        CustomerId = couponType.CustomerId,
                                        CouponTypeId = couponType.CouponTypeID.ToString(),
                                        CouponTypeDesc = couponType.CouponTypeDesc,
                                        CouponTypeName = couponType.CouponTypeName,
                                        BeginTime = couponType.BeginTime.ToString(),
                                        EndTime = couponType.EndTime.ToString(),
                                        ServiceLife = couponType.ServiceLife ?? 0,
                                        CouponLenth = intCouponLenth
                                    });
                        
                    }

                }
                var response = redisCouponBLL.RedisGetCoupon(coupon);
                if (response.Code == ResponseCode.Success)
                {
                    String uperStr = StringUtil.GetRandomUperStr(4);
                    String strInt = StringUtil.GetRandomStrInt(8);
                    string strCouponCode = uperStr + "-" + strInt;
                    var _coupon = new CC_Coupon()
                    {
                        CustomerId = response.Result.CustomerId,
                        CouponTypeId = response.Result.CouponTypeId,
                        CouponTypeDesc = response.Result.CouponTypeDesc,
                        CouponTypeName = response.Result.CouponTypeName,
                        CouponCode = strCouponCode,
                        BeginTime = response.Result.BeginTime,
                        EndTime = response.Result.EndTime,
                        ServiceLife = response.Result.ServiceLife,
                        CouponId = Guid.NewGuid().ToString()
                    };
                    BaseService.WriteLog("---------------------------入vip绑定优惠券队列---------------------------");
                    RedisOpenAPI.Instance.CCVipMappingCoupon().SetVipMappingCoupon(new CC_VipMappingCoupon()
                    {
                        CustomerId = coupon.CustomerId,
                        ObjectId = strObjectId,
                        VipId = strVipId,
                        Source = strSource,
                        Coupon = _coupon
                    });
                }
            }
        }
        /// <summary>
        /// 批量
        /// </summary>
        public void InsertDataBase()
        {

            BaseService.WriteLog("---------------------------vip绑定优惠券开始---------------------------");
            try
            {
                var numCount = 50;
                var customerIDs = CustomerBLL.Instance.GetCustomerList();
                foreach (var customer in customerIDs)
                {
                    LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                    LoggingManager CurrentLoggingManager = new LoggingManager();
                    loggingSessionInfo.ClientID = customer.Key;
                    CurrentLoggingManager.Connection_String = customer.Value;
                    loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                    loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
                    loggingSessionInfo.CurrentUser.customer_id = customer.Key;

                    DataTable dtCoupon = new DataTable();
                    dtCoupon.Columns.Add("CouponID", typeof(string));
                    dtCoupon.Columns.Add("CouponCode", typeof(string));
                    dtCoupon.Columns.Add("CouponDesc", typeof(string));
                    dtCoupon.Columns.Add("BeginDate", typeof(DateTime));
                    dtCoupon.Columns.Add("EndDate", typeof(DateTime));
                    dtCoupon.Columns.Add("CouponUrl", typeof(string));
                    dtCoupon.Columns.Add("ImageUrl", typeof(string));
                    dtCoupon.Columns.Add("Status", typeof(Int32));
                    dtCoupon.Columns.Add("CreateTime", typeof(DateTime));
                    dtCoupon.Columns.Add("CreateBy", typeof(string));
                    dtCoupon.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dtCoupon.Columns.Add("LastUpdateBy", typeof(string));
                    dtCoupon.Columns.Add("IsDelete", typeof(Int32));
                    dtCoupon.Columns.Add("CouponTypeID", typeof(string));
                    dtCoupon.Columns.Add("CoupnName", typeof(string));
                    dtCoupon.Columns.Add("DoorID", typeof(string));
                    dtCoupon.Columns.Add("CouponPwd", typeof(string));
                    dtCoupon.Columns.Add("CollarCardMode", typeof(string));
                    dtCoupon.Columns.Add("CustomerID", typeof(string));
                    DataTable dtVipCoupon = new DataTable();
                    dtVipCoupon.Columns.Add("VipCouponMapping", typeof(string));
                    dtVipCoupon.Columns.Add("VIPID", typeof(string));
                    dtVipCoupon.Columns.Add("CouponID", typeof(string));
                    dtVipCoupon.Columns.Add("UrlInfo", typeof(string));
                    dtVipCoupon.Columns.Add("IsDelete", typeof(Int32));
                    dtVipCoupon.Columns.Add("LastUpdateBy", typeof(string));
                    dtVipCoupon.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dtVipCoupon.Columns.Add("CreateBy", typeof(string));
                    dtVipCoupon.Columns.Add("CreateTime", typeof(DateTime));
                    dtVipCoupon.Columns.Add("FromVipId", typeof(string));
                    dtVipCoupon.Columns.Add("ObjectId", typeof(string));
                    dtVipCoupon.Columns.Add("CouponSourceId", typeof(string));

                    var count = RedisOpenAPI.Instance.CCVipMappingCoupon().GetVipMappingCouponLength(new CC_VipMappingCoupon
                    {
                        CustomerId = customer.Key
                    });
                    if (count.Code != ResponseCode.Success)
                    {
                        BaseService.WriteLog("从redis获取待绑定优惠券数量失败");
                        continue;
                    }
                    if (count.Result <= 0)
                    {
                        continue;
                    }
                    BaseService.WriteLog("优惠券redis取数据：" + customer.Key );
                    if (count.Result < numCount)
                    {
                        numCount = Convert.ToInt32(count.Result);
                    }

                    for (var i = 0; i < numCount; i++)
                    {
                        BaseService.WriteLog("---------------------------vip绑定优惠券长度:" + count.Result.ToString());
                        var response = RedisOpenAPI.Instance.CCVipMappingCoupon().GetVipMappingCoupon(new CC_VipMappingCoupon
                        {
                            CustomerId = customer.Key
                        });
                        if (response.Code == ResponseCode.Success)
                        {
                            DataRow dr_Coupon = dtCoupon.NewRow();
                            dr_Coupon["CouponID"] = response.Result.Coupon.CouponId;
                            dr_Coupon["CouponCode"] = response.Result.Coupon.CouponCode;
                            dr_Coupon["CouponDesc"] = response.Result.Coupon.CouponTypeDesc;
                            if (response.Result.Coupon.ServiceLife > 0)
                            {
                                dr_Coupon["BeginDate"] = DateTime.Now;
                                dr_Coupon["EndDate"] = DateTime.Now.Date.AddDays(response.Result.Coupon.ServiceLife - 1).ToShortDateString() + " 23:59:59.998";

                            }
                            else
                            {
                                dr_Coupon["BeginDate"] = response.Result.Coupon.BeginTime;
                                dr_Coupon["EndDate"] = response.Result.Coupon.EndTime;
                            }
                            dr_Coupon["CouponUrl"] = "";
                            dr_Coupon["ImageUrl"] = "";
                            dr_Coupon["Status"] = 2;
                            dr_Coupon["CreateTime"] = DateTime.Now;
                            dr_Coupon["CreateBy"] = "Redis";
                            dr_Coupon["LastUpdateTime"] = DateTime.Now;
                            dr_Coupon["LastUpdateBy"] = "Redis";
                            dr_Coupon["IsDelete"] = 0;
                            dr_Coupon["CouponTypeID"] = response.Result.Coupon.CouponTypeId;
                            dr_Coupon["CoupnName"] = response.Result.Coupon.CouponTypeName;
                            dr_Coupon["DoorID"] = "";
                            dr_Coupon["CouponPwd"] = "";
                            dr_Coupon["CollarCardMode"] = "";
                            dr_Coupon["CustomerID"] = customer.Key;
                            dtCoupon.Rows.Add(dr_Coupon);

                            DataRow dr_VipCoupon = dtVipCoupon.NewRow();
                            dr_VipCoupon["VipCouponMapping"] = Guid.NewGuid().ToString().Replace("-", "");
                            dr_VipCoupon["VIPID"] = response.Result.VipId;
                            dr_VipCoupon["CouponID"] = response.Result.Coupon.CouponId;
                            dr_VipCoupon["UrlInfo"] = "";
                            dr_VipCoupon["IsDelete"] = 0;
                            dr_VipCoupon["LastUpdateBy"] = "Redis";
                            dr_VipCoupon["LastUpdateTime"] = DateTime.Now;
                            dr_VipCoupon["CreateBy"] = "Redis";
                            dr_VipCoupon["CreateTime"] = DateTime.Now;
                            dr_VipCoupon["FromVipId"] = "";
                            dr_VipCoupon["ObjectId"] = response.Result.ObjectId;
                            dr_VipCoupon["CouponSourceId"] = GetSourceId(response.Result.Source);


                            dtVipCoupon.Rows.Add(dr_VipCoupon);
                            try
                            {
                                ///优惠券到账通知
                                var CommonBLL = new CommonBLL();
                                var bllVip = new VipBLL(loggingSessionInfo);
                                var vip = bllVip.GetByID(response.Result.VipId);

                                string strValidityData = Convert.ToDateTime(dr_Coupon["BeginDate"].ToString()).ToShortDateString() + "-" + Convert.ToDateTime(dr_Coupon["EndDate"].ToString()).ToShortDateString();
                                CommonBLL.CouponsArrivalMessage(response.Result.Coupon.CouponCode, response.Result.Coupon.CouponTypeName, strValidityData, response.Result.Coupon.CouponCategory == null ? "" : response.Result.Coupon.CouponCategory, vip.WeiXinUserId, loggingSessionInfo);
                            }
                            catch (Exception ex)
                            {
                                BaseService.WriteLog("优惠券到账通知异常：" + ex.Message);
                                continue;
                            }
                        }
                    }
                    if (dtCoupon != null && dtCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(customer.Value, dtCoupon, "Coupon");

                        var bllCouponType = new CouponTypeBLL(loggingSessionInfo);
                        bllCouponType.UpdateCouponTypeIsVoucher(customer.Key);


                        BaseService.WriteLog("批量插入Coupon:");

                    }
                    if (dtVipCoupon != null && dtVipCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(customer.Value, dtVipCoupon, "VipCouponMapping");
                        BaseService.WriteLog("批量插入VipCouponMapping:");

                    }
                    BaseService.WriteLog("延迟时间开始");
                    Thread.Sleep(1000);
                    BaseService.WriteLog("延迟时间结束");

                }
            }
            catch (Exception ex)
            {
                BaseService.WriteLog("vip绑定优惠券异常" + ex.Message);

                throw;
            }
            BaseService.WriteLog("---------------------------vip绑定优惠券结束---------------------------");

        }
        /// <summary>
        /// 批量插入数据库
        /// </summary>
        /// <param name="strCon"></param>
        /// <param name="dt"></param>
        /// <param name="strTableName"></param>
        public void SqlBulkCopy(string strCon,DataTable dt,string strTableName)
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans))
                        {
                            sqlbulkcopy.DestinationTableName = strTableName;
                            sqlbulkcopy.BatchSize = dt.Rows.Count;
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                            }
                            sqlbulkcopy.WriteToServer(dt);
                            trans.Commit();

                            //sqlbulkcopy.BatchSize = 1000000;
                            //sqlbulkcopy.DestinationTableName = "db_User";
                            //sqlbulkcopy.WriteToServer(dt);
                            //trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }
        public string GetSourceId(string strType)
        {
            switch (strType)
            {
                case "Reg":
                    return "FD977FBA-03F9-4AC0-805F-75A56BD6A429";
                case "Comment":
                    return "80EF450A-5CEF-4DB6-B28D-420BDDA59894";
                case "Focus":
                    return "945C3C2D88DF4AFAA260A1CED81C6870";
                case "Share":
                    return "5F671057-E6B5-4B5E-B2D4-AC3A64F6710F";
                case "Game":
                    return "7D87E7E1-66AC-403B-9BB4-80AE4278F6A4";
                case "CTW":
                    return "07231C5F-8B9B-4A67-BCF5-B7D1C95CEA8E";
                default:
                    return "";
            }
        }

        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }


        public void InsertDataBaseNew()
        {

            BaseService.WriteLog("---------------------------vip绑定优惠券开始---------------------------");
            try
            {
                var numCount = 1000;
                var customerIDs = CustomerBLL.Instance.GetCustomerList();
                CC_Connection connection = new CC_Connection();
                foreach (var customer in customerIDs)
                {

                    DataTable dtCoupon = new DataTable();
                    dtCoupon.Columns.Add("CouponID", typeof(string));
                    dtCoupon.Columns.Add("CouponCode", typeof(string));
                    dtCoupon.Columns.Add("CouponDesc", typeof(string));
                    dtCoupon.Columns.Add("BeginDate", typeof(DateTime));
                    dtCoupon.Columns.Add("EndDate", typeof(DateTime));
                    dtCoupon.Columns.Add("CouponUrl", typeof(string));
                    dtCoupon.Columns.Add("ImageUrl", typeof(string));
                    dtCoupon.Columns.Add("Status", typeof(Int32));
                    dtCoupon.Columns.Add("CreateTime", typeof(DateTime));
                    dtCoupon.Columns.Add("CreateBy", typeof(string));
                    dtCoupon.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dtCoupon.Columns.Add("LastUpdateBy", typeof(string));
                    dtCoupon.Columns.Add("IsDelete", typeof(Int32));
                    dtCoupon.Columns.Add("CouponTypeID", typeof(string));
                    dtCoupon.Columns.Add("CoupnName", typeof(string));
                    dtCoupon.Columns.Add("DoorID", typeof(string));
                    dtCoupon.Columns.Add("CouponPwd", typeof(string));
                    dtCoupon.Columns.Add("CollarCardMode", typeof(string));
                    dtCoupon.Columns.Add("CustomerID", typeof(string));
                    DataTable dtVipCoupon = new DataTable();
                    dtVipCoupon.Columns.Add("VipCouponMapping", typeof(string));
                    dtVipCoupon.Columns.Add("VIPID", typeof(string));
                    dtVipCoupon.Columns.Add("CouponID", typeof(string));
                    dtVipCoupon.Columns.Add("UrlInfo", typeof(string));
                    dtVipCoupon.Columns.Add("IsDelete", typeof(Int32));
                    dtVipCoupon.Columns.Add("LastUpdateBy", typeof(string));
                    dtVipCoupon.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dtVipCoupon.Columns.Add("CreateBy", typeof(string));
                    dtVipCoupon.Columns.Add("CreateTime", typeof(DateTime));
                    dtVipCoupon.Columns.Add("FromVipId", typeof(string));
                    dtVipCoupon.Columns.Add("ObjectId", typeof(string));
                    dtVipCoupon.Columns.Add("CouponSourceId", typeof(string));

                    var count = RedisOpenAPI.Instance.CCVipMappingCoupon().GetVipMappingCouponLength(new CC_VipMappingCoupon
                    {
                        CustomerId = customer.Key
                    });
                    if (count.Code != ResponseCode.Success)
                    {
                        BaseService.WriteLog("从redis获取待绑定优惠券数量失败");
                        continue;
                    }
                    if (count.Result <= 0)
                    {
                        continue;
                    }
                    connection = new RedisConnectionBLL().GetConnection(customer.Key);
                    BaseService.WriteLog("优惠券redis取数据：" + customer.Key + " Code" + connection.Customer_Code + " Name" + connection.Customer_Name);

                    if (connection.ConnectionStr == null)
                    {
                        BaseService.WriteLog("优惠券conn从数据库取数据" + connection.ConnectionStr);
                        connection.ConnectionStr = GetCustomerConn(customer.Key);

                    }
                    if (connection.ConnectionStr == null)
                    {
                        BaseService.WriteLog("优惠券conn从数据库取数据失败");
                        continue;

                    }
                    if (count.Result < numCount)
                    {
                        numCount = Convert.ToInt32(count.Result);
                    }
                     BaseService.WriteLog("---------------------------vip绑定优惠券长度:" + count.Result.ToString());
                    for (var i = 0; i < numCount; i++)
                    {
                       
                        var response = RedisOpenAPI.Instance.CCVipMappingCoupon().GetVipMappingCoupon(new CC_VipMappingCoupon
                        {
                            CustomerId = customer.Key
                        });
                        if (response.Code == ResponseCode.Success)
                        {
                            DataRow dr_Coupon = dtCoupon.NewRow();
                            dr_Coupon["CouponID"] = response.Result.Coupon.CouponId;
                            dr_Coupon["CouponCode"] = response.Result.Coupon.CouponCode;
                            dr_Coupon["CouponDesc"] = response.Result.Coupon.CouponTypeDesc;
                            if (response.Result.Coupon.ServiceLife > 0)
                            {
                                dr_Coupon["BeginDate"] = DateTime.Now;
                                dr_Coupon["EndDate"] = DateTime.Now.Date.AddDays(response.Result.Coupon.ServiceLife - 1).ToShortDateString() + " 23:59:59.998";

                            }
                            else
                            {
                                dr_Coupon["BeginDate"] = response.Result.Coupon.BeginTime;
                                dr_Coupon["EndDate"] = response.Result.Coupon.EndTime;
                            }
                            dr_Coupon["CouponUrl"] = "";
                            dr_Coupon["ImageUrl"] = "";
                            dr_Coupon["Status"] = 2;
                            dr_Coupon["CreateTime"] = DateTime.Now;
                            dr_Coupon["CreateBy"] = "Redis";
                            dr_Coupon["LastUpdateTime"] = DateTime.Now;
                            dr_Coupon["LastUpdateBy"] = "Redis";
                            dr_Coupon["IsDelete"] = 0;
                            dr_Coupon["CouponTypeID"] = response.Result.Coupon.CouponTypeId;
                            dr_Coupon["CoupnName"] = response.Result.Coupon.CouponTypeName;
                            dr_Coupon["DoorID"] = "";
                            dr_Coupon["CouponPwd"] = "";
                            dr_Coupon["CollarCardMode"] = "";
                            dr_Coupon["CustomerID"] = customer.Key;
                            dtCoupon.Rows.Add(dr_Coupon);

                            DataRow dr_VipCoupon = dtVipCoupon.NewRow();
                            dr_VipCoupon["VipCouponMapping"] = Guid.NewGuid().ToString().Replace("-", "");
                            dr_VipCoupon["VIPID"] = response.Result.VipId;
                            dr_VipCoupon["CouponID"] = response.Result.Coupon.CouponId;
                            dr_VipCoupon["UrlInfo"] = "";
                            dr_VipCoupon["IsDelete"] = 0;
                            dr_VipCoupon["LastUpdateBy"] = "Redis";
                            dr_VipCoupon["LastUpdateTime"] = DateTime.Now;
                            dr_VipCoupon["CreateBy"] = "Redis";
                            dr_VipCoupon["CreateTime"] = DateTime.Now;
                            dr_VipCoupon["FromVipId"] = "";
                            dr_VipCoupon["ObjectId"] = response.Result.ObjectId;
                            dr_VipCoupon["CouponSourceId"] = GetSourceId(response.Result.Source);


                            dtVipCoupon.Rows.Add(dr_VipCoupon);
                        }
                    }
                    if (dtCoupon != null && dtCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(connection.ConnectionStr, dtCoupon, "Coupon");

                        LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                        LoggingManager CurrentLoggingManager = new LoggingManager();

                        _loggingSessionInfo.ClientID = customer.Key;
                        CurrentLoggingManager.Connection_String = connection.ConnectionStr;
                        _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                        var bllCouponType = new CouponTypeBLL(_loggingSessionInfo);
                        bllCouponType.UpdateCouponTypeIsVoucher(customer.Key);


                        BaseService.WriteLog("批量插入Coupon:");

                    }
                    if (dtVipCoupon != null && dtVipCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(connection.ConnectionStr, dtVipCoupon, "VipCouponMapping");
                        BaseService.WriteLog("批量插入VipCouponMapping:");

                    }

                }
            }
            catch (Exception ex)
            {
                BaseService.WriteLog("vip绑定优惠券异常" + ex.Message);

                throw;
            }
            BaseService.WriteLog("---------------------------vip绑定优惠券结束---------------------------");

        }
    }

}

