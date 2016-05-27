
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
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
using ServiceStack.Redis.Support;
using ServiceStack.Redis;
using Newtonsoft.Json;
using System.Configuration;
namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Coupon
{
    public class RedisCouponBLL
    {
        

        public void RedisSetSingleCoupon(CC_Coupon coupon)
        {
            RedisOpenAPI.Instance.CCCoupon().SetCouponList(coupon);
        }
        /// <summary>
        /// 种植所有商户优惠券
        /// </summary>
        public void RedisSetAllCoupon()
        {
            //CC_Connection connection = new CC_Connection();

            LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
            LoggingManager CurrentLoggingManager = new LoggingManager();

            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {

                _loggingSessionInfo.ClientID = customer.Key;
                CurrentLoggingManager.Connection_String = customer.Value;
                _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                var bllCouponType = new CouponTypeBLL(_loggingSessionInfo);
                var bllCoupon = new CouponBLL(_loggingSessionInfo);
                var couponTypeList=bllCouponType.QueryByEntity(new CouponTypeEntity() { CustomerId = customer.Key, IsDelete = 0 },null).ToList();
                foreach(var couponType in couponTypeList)
                {
                    RedisOpenAPI.Instance.CCCoupon().DeleteCouponList(new CC_Coupon() { CustomerId = customer.Key, CouponTypeId = couponType.CouponTypeID.ToString() });

                    int intUsed = bllCoupon.QueryByEntity(new CouponEntity() { CouponTypeID = couponType.CouponTypeID.ToString(), IsDelete = 0 }, null).ToList().Count();
                    RedisSetSingleCoupon(new CC_Coupon()
                        {
                            CustomerId = couponType.CustomerId,
                            CouponTypeId = couponType.CouponTypeID.ToString(),
                            CouponTypeDesc = couponType.CouponTypeDesc,
                            CouponTypeName = couponType.CouponTypeName,
                            BeginTime = couponType.BeginTime.ToString(),
                            EndTime = couponType.EndTime.ToString(),
                            ServiceLife = couponType.ServiceLife ?? 0,
                            CouponLenth = Convert.ToInt32(couponType.IssuedQty) - intUsed
                        });
                    //xuRedis
                    //List<string> couponList = new List<string>();
                    //for (int i = 0; i < (Convert.ToInt32(couponType.IssuedQty) - intUsed); i++)
                    //{
                    //    Test_Coupon coupon = new Test_Coupon()
                    //        {
                    //            CustomerId = couponType.CustomerId,
                    //            CouponTypeId = couponType.CouponTypeID.ToString(),
                    //            CouponTypeDesc = couponType.CouponTypeDesc,
                    //            CouponTypeName = couponType.CouponTypeName,
                    //            BeginTime = couponType.BeginTime.ToString(),
                    //            EndTime = couponType.EndTime.ToString(),
                    //            ServiceLife = couponType.ServiceLife ?? 0,
                    //            CouponLenth = Convert.ToInt32(couponType.IssuedQty) - intUsed
                    //        };
                    //    string ss = JsonConvert.SerializeObject(coupon);
                    //    couponList.Add(ss);
                    //}
                    //SetRedisCouponNew(couponList, customer.Key, couponType.CouponTypeID.ToString());
                    
                }
            }
        }
        public ResponseModel<CC_Coupon> RedisGetCoupon(CC_Coupon coupon)
        {
           return RedisOpenAPI.Instance.CCCoupon().GetCoupon(coupon);

        }
        public long GetCouponListLength(CC_Coupon coupon)
        {
            var count=RedisOpenAPI.Instance.CCCoupon().GetCouponListLength(coupon);
            return count.Result;
        }
        /// <summary>
        /// 下载二维码
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public DataTable DownloadCoupon(CC_Coupon coupon, string strCustomerid, int downLoadNum)
        {
            var count = RedisOpenAPI.Instance.CCCoupon().GetCouponListLength(coupon);
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

            DataTable dtCouponExport = new DataTable();
            dtCouponExport.Columns.Add("CouponCode", typeof(string));
            CC_Connection connection = new CC_Connection();
            if (count.Code == ResponseCode.Success)
            {
                if (count.Result > 0)
                {
                    string strCon = string.Empty;
                    connection = new RedisConnectionBLL().GetConnection(strCustomerid);
                    if (connection.ConnectionStr == null)
                    {
                        strCon= GetCustomerConn(strCustomerid);

                    }
                    strCon = connection.ConnectionStr;

                    long num = 0;
                    if (downLoadNum > count.Result)
                        num = count.Result;
                    else
                        num = downLoadNum;

                    for (int i = 0; i < num; i++)
                        {
                            var response = RedisGetCoupon(coupon);
                            if (response.Code == ResponseCode.Success)
                            {
                                String uperStr = StringUtil.GetRandomUperStr(4);
                                String strInt = StringUtil.GetRandomStrInt(8);
                                string strCouponCode = uperStr + "-" + strInt;

                                DataRow dr_Coupon = dtCoupon.NewRow();
                                dr_Coupon["CouponID"] = Guid.NewGuid().ToString();
                                dr_Coupon["CouponCode"] = strCouponCode;
                                dr_Coupon["CouponDesc"] = response.Result.CouponTypeDesc;
                                if (response.Result.ServiceLife > 0)
                                {
                                    dr_Coupon["BeginDate"] = DateTime.Now;
                                    dr_Coupon["EndDate"] = DateTime.Now.Date.AddDays(response.Result.ServiceLife - 1).ToShortDateString() + " 23:59:59.998";

                                }
                                else
                                {
                                    dr_Coupon["BeginDate"] = response.Result.BeginTime;
                                    dr_Coupon["EndDate"] = response.Result.EndTime;
                                }
                                dr_Coupon["CouponUrl"] = "";
                                dr_Coupon["ImageUrl"] = "";
                                dr_Coupon["Status"] = 0;
                                dr_Coupon["CreateTime"] = DateTime.Now;
                                dr_Coupon["CreateBy"] = "Redis";
                                dr_Coupon["LastUpdateTime"] = DateTime.Now;
                                dr_Coupon["LastUpdateBy"] = "Redis";
                                dr_Coupon["IsDelete"] = 0;
                                dr_Coupon["CouponTypeID"] = response.Result.CouponTypeId;
                                dr_Coupon["CoupnName"] = response.Result.CouponTypeName;
                                dr_Coupon["DoorID"] = "";
                                dr_Coupon["CouponPwd"] = "";
                                dr_Coupon["CollarCardMode"] = "";
                                dr_Coupon["CustomerID"] = coupon.CustomerId;
                                dtCoupon.Rows.Add(dr_Coupon);

                                DataRow dr_CouponExport = dtCouponExport.NewRow();
                                dr_CouponExport["CouponCode"] = strCouponCode;
                                dtCouponExport.Rows.Add(dr_CouponExport);

                            }
                        }
                    if (dtCoupon != null && dtCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(strCon, dtCoupon, "Coupon");

                        LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                        LoggingManager CurrentLoggingManager = new LoggingManager();

                        _loggingSessionInfo.ClientID = strCustomerid;
                        CurrentLoggingManager.Connection_String = strCon;
                        _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                        var bllCouponType = new CouponTypeBLL(_loggingSessionInfo);
                        bllCouponType.UpdateCouponTypeIsVoucher(strCustomerid);
                    }
                }

            }
            return dtCouponExport;
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
        /// <summary>
        /// 批量插入数据库
        /// </summary>
        /// <param name="strCon"></param>
        /// <param name="dt"></param>
        /// <param name="strTableName"></param>
        public void SqlBulkCopy(string strCon, DataTable dt, string strTableName)
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
        #region Redis_Xu
        public void SetRedisCouponNew(List<string> coupon,string strCustomerId,string strCouponTypeId)
        {
            PooledRedisClientManager pooleManager = new PooledRedisClientManager(10, 5, "182.254.151.114:6379");
            using (var redisClient = pooleManager.GetClient())
            {
                redisClient.Db = 2;
                redisClient.Password = "Zmind@123";
                var ser = new ObjectSerializer();    //位于namespace ServiceStack.Redis.Support;
                if (coupon.Count > 0)
                {
                    string strKey = "TestCoupon" + strCustomerId + strCouponTypeId;
                    redisClient.AddRangeToList(strKey, coupon);


                }

            }
        }
        public Test_Coupon GetRedisCouponNew(string strKey)
        {
            PooledRedisClientManager pooleManager = new PooledRedisClientManager(10, 5, "182.254.151.114:6379");
            Test_Coupon coupon = new Test_Coupon();
            using (var redisClient = pooleManager.GetClient())
            {
                redisClient.Db = 2;
                redisClient.Password = "Zmind@123";
                if (strKey.Length >0)
                {
                    string strCoupon=redisClient.PopItemFromList(strKey);
                    if (strCoupon.Length > 0)
                        coupon= JsonConvert.DeserializeObject<Test_Coupon>(strCoupon);
                }

            }
            return coupon;
            
        }
        public DataTable DownloadCouponNew(string strCustomerId,string strCouponTypeId, int downLoadNum)
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

            DataTable dtCouponExport = new DataTable();
            dtCouponExport.Columns.Add("CouponCode", typeof(string));

            PooledRedisClientManager pooleManager = new PooledRedisClientManager(10, 5, "182.254.151.114:6379");
            var redisClient = pooleManager.GetClient();
            redisClient.Db = 2;
            redisClient.Password = "Zmind@123";
            string strKey = "Coupon" + strCustomerId + strCouponTypeId;
            long ListCount=redisClient.GetListCount(strKey);
            if (ListCount>0)
            {


                string strCon = new RedisConnectionBLL().GetConnection(strCustomerId).ConnectionStr;
                    long num = 0;
                    if (downLoadNum > ListCount)
                        num = ListCount;
                    else
                        num = downLoadNum;

                    for (int i = 0; i < num; i++)
                    {
                        var coupon = GetRedisCouponNew(strKey);
                        if (coupon.CouponTypeId != null && coupon.CouponTypeId.Length>0)
                        {
                            String uperStr = StringUtil.GetRandomUperStr(4);
                            String strInt = StringUtil.GetRandomStrInt(8);
                            string strCouponCode = uperStr + "-" + strInt;

                            DataRow dr_Coupon = dtCoupon.NewRow();
                            dr_Coupon["CouponID"] = Guid.NewGuid().ToString();
                            dr_Coupon["CouponCode"] = strCouponCode;
                            dr_Coupon["CouponDesc"] = coupon.CouponTypeDesc;
                            if (coupon.ServiceLife > 0)
                            {
                                dr_Coupon["BeginDate"] = DateTime.Now;
                                dr_Coupon["EndDate"] = DateTime.Now.Date.AddDays(coupon.ServiceLife - 1).ToShortDateString() + " 23:59:59.998";

                            }
                            else
                            {
                                dr_Coupon["BeginDate"] = coupon.BeginTime;
                                dr_Coupon["EndDate"] = coupon.EndTime;
                            }
                            dr_Coupon["CouponUrl"] = "";
                            dr_Coupon["ImageUrl"] = "";
                            dr_Coupon["Status"] = 0;
                            dr_Coupon["CreateTime"] = DateTime.Now;
                            dr_Coupon["CreateBy"] = "Redis";
                            dr_Coupon["LastUpdateTime"] = DateTime.Now;
                            dr_Coupon["LastUpdateBy"] = "Redis";
                            dr_Coupon["IsDelete"] = 0;
                            dr_Coupon["CouponTypeID"] = coupon.CouponTypeId;
                            dr_Coupon["CoupnName"] = coupon.CouponTypeName;
                            dr_Coupon["DoorID"] = "";
                            dr_Coupon["CouponPwd"] = "";
                            dr_Coupon["CollarCardMode"] = "";
                            dr_Coupon["CustomerID"] = coupon.CustomerId;
                            dtCoupon.Rows.Add(dr_Coupon);

                            DataRow dr_CouponExport = dtCouponExport.NewRow();
                            dr_CouponExport["CouponCode"] = strCouponCode;
                            dtCouponExport.Rows.Add(dr_CouponExport);

                        }
                    }
                    if (dtCoupon != null && dtCoupon.Rows.Count > 0)
                    {
                        SqlBulkCopy(strCon, dtCoupon, "Coupon");

                        LoggingSessionInfo _loggingSessionInfo = new LoggingSessionInfo();
                        LoggingManager CurrentLoggingManager = new LoggingManager();

                        _loggingSessionInfo.ClientID = strCustomerId;
                        CurrentLoggingManager.Connection_String = strCon;
                        _loggingSessionInfo.CurrentLoggingManager = CurrentLoggingManager;
                        var bllCouponType = new CouponTypeBLL(_loggingSessionInfo);
                        bllCouponType.UpdateCouponTypeIsVoucher(strCustomerId);
                    }
            }
            return dtCouponExport;
        }
        [Serializable]
        public class Test_Coupon
        {
            public string CustomerId { get; set; }
            public string CouponTypeId { get; set; }
            /// <summary>
            /// 优惠券描述
            /// </summary>
            public string CouponTypeDesc { get; set; }
            /// <summary>
            /// 优惠券名称
            /// </summary>
            public string CouponTypeName { get; set; }
            /// <summary>
            /// 有效期开始时间
            /// </summary>
            public string BeginTime { get; set; }
            /// <summary>
            /// 有效期结束时间
            /// </summary>
            public string EndTime { get; set; }
            /// <summary>
            /// 相对有效期
            /// </summary>
            public int ServiceLife { get; set; }
            /// <summary>
            /// 优惠券码
            /// </summary>
            public string CouponCode { get; set; }
            /// <summary>
            /// 优惠券标识
            /// </summary>
            public string CouponId { get; set; }

            /// <summary>
            /// 优惠券个数
            /// </summary>
            public int CouponLenth { get; set; }
        }
        #endregion
    }
}
