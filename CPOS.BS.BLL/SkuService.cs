using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// sku服务
    /// </summary>
    public class SkuService : BaseService
    {
        JIT.CPOS.BS.DataAccess.SkuService skuService = null;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public SkuService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            skuService = new DataAccess.SkuService(loggingSessionInfo);
        }
        #endregion

        #region 获取商品组合信息
        /// <summary>
        /// 拼商品详细信息（barcode/item_name/prop1/prop2/prop3/prop4/prop5）
        /// </summary>
        /// <param name="skuInfo"></param>
        /// <returns></returns>
        public static string GetItemAllName(SkuInfo skuInfo)
        {
            string display_name = string.Empty;
            display_name = string.Format("{0}/{1}",
                skuInfo.barcode, skuInfo.item_name);
            if (skuInfo.prop_1_id != null)
            {
                display_name += "/" + skuInfo.prop_1_detail_name;
            }
            if (skuInfo.prop_2_id != null)
            {
                display_name += "/" + skuInfo.prop_2_detail_name;
            }
            if (skuInfo.prop_3_id != null)
            {
                display_name += "/" + skuInfo.prop_3_detail_name;
            }
            if (skuInfo.prop_4_id != null)
            {
                display_name += "/" + skuInfo.prop_4_detail_name;
            }
            if (skuInfo.prop_5_id != null)
            {
                display_name += "/" + skuInfo.prop_5_detail_name;
            }

            return display_name;
        }

        public static string GetItemAllName(InoutDetailInfo inoutDetailInfo)
        {
            string display_name = string.Empty;
            display_name = string.Format("{0}/{1}",
                inoutDetailInfo.item_code, inoutDetailInfo.item_name);
            if (inoutDetailInfo.prop_1_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_1_detail_name;
            }
            if (inoutDetailInfo.prop_2_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_2_detail_name;
            }
            if (inoutDetailInfo.prop_3_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_3_detail_name;
            }
            if (inoutDetailInfo.prop_4_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_4_detail_name;
            }
            if (inoutDetailInfo.prop_5_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_5_detail_name;
            }

            return display_name;
        }
        public static string GetItemPropName(InoutDetailInfo inoutDetailInfo)
        {
            string display_name = string.Empty;
            //display_name = string.Format("{0}/{1}",
            //    inoutDetailInfo.item_code, inoutDetailInfo.item_name);
            if (inoutDetailInfo.prop_1_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_1_detail_name;
            }
            if (inoutDetailInfo.prop_2_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_2_detail_name;
            }
            if (inoutDetailInfo.prop_3_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_3_detail_name;
            }
            if (inoutDetailInfo.prop_4_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_4_detail_name;
            }
            if (inoutDetailInfo.prop_5_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_5_detail_name;
            }
            if (display_name.StartsWith("/")) display_name = display_name.Substring(1);
            return display_name;
        }

        public static string GetItemAllName(OrderDetailInfo inoutDetailInfo)
        {
            string display_name = string.Empty;
            display_name = string.Format("{0}/{1}",
                inoutDetailInfo.item_code, inoutDetailInfo.item_name);
            if (inoutDetailInfo.prop_1_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_1_detail_name;
            }
            if (inoutDetailInfo.prop_2_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_2_detail_name;
            }
            if (inoutDetailInfo.prop_3_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_3_detail_name;
            }
            if (inoutDetailInfo.prop_4_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_4_detail_name;
            }
            if (inoutDetailInfo.prop_5_detail_name != null)
            {
                display_name += "/" + inoutDetailInfo.prop_5_detail_name;
            }

            return display_name;
        }

        public static string GetItemAllName(CCDetailInfo detailInfo)
        {
            string display_name = string.Empty;
            display_name = string.Format("{0}/{1}",
                detailInfo.item_code, detailInfo.item_name);
            if (detailInfo.prop_1_detail_name != null)
            {
                display_name += "/" + detailInfo.prop_1_detail_name;
            }
            if (detailInfo.prop_2_detail_name != null)
            {
                display_name += "/" + detailInfo.prop_2_detail_name;
            }
            if (detailInfo.prop_3_detail_name != null)
            {
                display_name += "/" + detailInfo.prop_3_detail_name;
            }
            if (detailInfo.prop_4_detail_name != null)
            {
                display_name += "/" + detailInfo.prop_4_detail_name;
            }
            if (detailInfo.prop_5_detail_name != null)
            {
                display_name += "/" + detailInfo.prop_5_detail_name;
            }

            return display_name;
        }

        #endregion

        #region sku基本操作
        /// <summary>
        /// 根据商品获取Sku
        /// </summary>
        /// <param name="itemId">商品标识</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuListByItemId(string itemId)
        {
            IList<SkuInfo> skuInfoList = new List<SkuInfo>();
            DataSet ds = new DataSet();
            ds = skuService.GetSkuListByItemId(itemId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                skuInfoList = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
            }
            return skuInfoList;
        }

        /// <summary>
        /// 根据商品获取Sku
        /// </summary>
        /// <param name="itemId">商品标识</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuListByItemIdAndEventId(string itemId, Guid eventId)
        {
            IList<SkuInfo> skuInfoList = new List<SkuInfo>();
            DataSet ds = new DataSet();
            ds = skuService.GetSkuListByItemIdAndEventId(itemId, eventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                skuInfoList = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
            }
            return skuInfoList;
        }

        /// <summary>
        /// 获取所有sku
        /// </summary>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuList()
        {
            IList<SkuInfo> skuInfoList = new List<SkuInfo>();
            DataSet ds = new DataSet();
            ds = skuService.GetSkuList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                skuInfoList = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
            }
            return skuInfoList;
        }



        /// <summary>
        /// 根据sku标识获取sku明细
        /// </summary>
        /// <param name="skuId">sku标识</param>
        /// <returns></returns>
        public SkuInfo GetSkuInfoById(string skuId)
        {
            SkuInfo skuInfo = new SkuInfo();
            DataSet ds = new DataSet();
            ds = skuService.GetSkuInfoById(skuId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                skuInfo = DataTableToObject.ConvertToObject<SkuInfo>(ds.Tables[0].Rows[0]);
            }
            return skuInfo;
        }

        /// <summary>
        /// 设置商品与商品类型与价格关系
        /// </summary>
        /// <param name="loggingSessionInfo">login model</param>
        /// <param name="itemInfo">item info</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        public bool SetSkuInfo(ItemInfo itemInfo, out string strError)
        {
            try
            {
                if (itemInfo.SkuList != null)
                {
                    foreach (SkuInfo skuInfo in itemInfo.SkuList)
                    {
                        if (skuInfo.sku_id == null || skuInfo.sku_id.Equals(""))
                        {
                            skuInfo.sku_id = NewGuid();
                        }
                        if (!IsExistBarcode(skuInfo.barcode, skuInfo.sku_id))
                        {
                            strError = "Barcode号码已经存在。";
                            return false;
                        }
                    }
                    string error="";
                    skuService.SetSkuInfo(itemInfo, null, out error);
                }

                strError = "处理sku信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 判断barcode是否重复
        /// </summary>
        /// <param name="loggingSessionInfo">login model</param>
        /// <param name="barcode">条形码</param>
        /// <param name="sku_id">sku标识</param>
        /// <returns></returns>
        public bool IsExistBarcode(string barcode, string sku_id)
        {
            try
            {
                int n = skuService.IsExistBarcode(barcode, sku_id);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 查询sku
        /// <summary>
        /// 模糊查询sku集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuInfoByLike(string condition)
        {
            try
            {
                IList<SkuInfo> skuInfoList = new List<SkuInfo>();
                DataSet ds = new DataSet();
                ds = skuService.GetSkuInfoByLike(condition);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    skuInfoList = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
                }
                return skuInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 模糊查询item集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemInfoByLike(string condition)
        {
            try
            {
                IList<ItemInfo> skuInfoList = new List<ItemInfo>();
                DataSet ds = new DataSet();
                ds = skuService.GetItemInfoByLike(condition);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    skuInfoList = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
                }
                return skuInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取一条SKU记录
        /// </summary>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuInfoByOne(string orderId)
        {
            try
            {
                IList<SkuInfo> skuInfoList = new List<SkuInfo>();
                DataSet ds = new DataSet();
                ds = skuService.GetSkuInfoByOne(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    skuInfoList = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
                }
                return skuInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 根据SKU ID获取SKU及Item信息
        /// </summary>
        /// <param name="pSKUIDs"></param>
        /// <returns></returns>
        public DataTable GetSKUAndItemBySKUIDs(string[] pSKUIDs)
        {
            return skuService.GetSKUAndItemBySKUIDs(pSKUIDs);
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        /// <summary>
        /// 获取未打包的SKU数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public int GetSkuNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        {
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Sku.SelectUnDownloadCount", "");
            return 0;
        }
        /// <summary>
        /// 需要打包的Sku信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<SkuInfo> GetSkuListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<SkuInfo>("Sku.SelectUnDownload", _ht);
            IList<SkuInfo> skuInfoList = new List<SkuInfo>();
            return skuInfoList;
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="SkuInfoList">商品集合</param>
        /// <returns></returns>
        public bool SetSkuBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<SkuInfo> SkuInfoList)
        {
            SkuInfo skuInfo = new SkuInfo();
            skuInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            skuInfo.modify_time = GetCurrentDateTime();
            skuInfo.bat_id = bat_id;
            skuInfo.SkuInfoList = SkuInfoList;
            //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Sku.UpdateUnDownloadBatId", skuInfo);
            return true;
        }
        /// <summary>
        /// 更新Sku表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <returns></returns>
        public bool SetSkuIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id)
        {
            SkuInfo skuInfo = new SkuInfo();
            skuInfo.bat_id = bat_id;
            skuInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            skuInfo.modify_time = GetCurrentDateTime();
            //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Sku.UpdateUnDownloadIfFlag", skuInfo);
            return true;
        }
        #endregion
        #endregion

        #region 获取同步福利SKU

        /// <summary>
        /// 获取同步福利SKU
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareSkuList(string latestTime)
        {
            return this.skuService.GetSynWelfareSkuList(latestTime);
        }

        #endregion

        #region 抢购SKU
        public DataSet GetItemSkuListByEventId(string itemId, Guid eventId)
        {
            return this.skuService.GetItemSkuListByEventId(itemId, eventId);
        }
        #endregion
    }
}
