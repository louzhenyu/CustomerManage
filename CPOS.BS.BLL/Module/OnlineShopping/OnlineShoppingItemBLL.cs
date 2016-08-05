using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Notification;
using System.Configuration;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.BLL
{
    public class OnlineShoppingItemBLL : BaseService
    {
        JIT.CPOS.BS.DataAccess.OnlineShoppingItemService itemService = null;
        #region 构造函数
        public OnlineShoppingItemBLL(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemService = new DataAccess.OnlineShoppingItemService(loggingSessionInfo);
        }
        #endregion

        #region GetWelfareItemList
        /// <summary>
        /// 获取校友福利商品列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="isKeep"></param>
        /// <param name="isStore"></param>
        /// <param name="socialSalesType">类型(0=按订单；1=按商品)</param>
        /// <returns></returns>
        public DataSet GetWelfareItemList(string userId, string itemName, string itemTypeId, int page, int pageSize, bool isKeep, string isExchange, string storeId, string isGroupBy, string ChannelId, int isStore, int socialSalesType, string strSortName, string strSort, int intVirtual, double Price)
        {
            return itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, isKeep, isExchange, storeId, isGroupBy, ChannelId, isStore, socialSalesType, strSortName, strSort, intVirtual, Price);
        }

        //public int GetWelfareItemListCount(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId)
        //{
        //    return itemService.GetWelfareItemListCount(userId, itemName, itemTypeId, isKeep, isExchange, storeId);
        //}
        #endregion

        #region 获取福利商品明细信息

        /// <summary>
        /// 获取福利商品明细信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataSet GetItemDetailByItemId(string itemId, string userId)
        {
            return itemService.GetItemDetailByItemId(itemId, userId);
        }

        /// <summary>
        /// 获取商品图片集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemImageList(string itemId)
        {
            return itemService.GetItemImageList(itemId);
        }

        /// <summary>
        /// 获取商品sku集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSkuList(string itemId)
        {
            return itemService.GetItemSkuList(itemId);
        }

        /// <summary>
        /// 购买用户集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSalesUserList(string itemId)
        {
            return itemService.GetItemSalesUserList(itemId);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreInfo(string itemId)
        {
            return itemService.GetItemStoreInfo(itemId);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemBrandInfo(string itemId)
        {
            return itemService.GetItemBrandInfo(itemId);
        }

        #endregion

        #region Jermyn20131121获取 商品属性集合
        /// <summary>
        /// 获取商品属性1集合
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="Prop1DetailId"></param>
        /// <returns></returns>
        public DataSet GetItemProp1List(string itemId)
        {
            return itemService.GetItemProp1List(itemId);
        }
        /// <summary>
        /// 获取商品属性2集合
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="propDetailId"></param>
        /// <returns></returns>
        public DataSet GetItemProp2List(string itemId, string propDetailId)
        {
            return itemService.GetItemProp2List(itemId, propDetailId);
        }
        #endregion

        #region 获取电商订单信息
        /// <summary>
        /// 获取电商订单详细信息
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public JIT.CPOS.BS.Entity.Interface.SetOrderEntity GetOrderOnline(string OrderId)
        {
            JIT.CPOS.BS.Entity.Interface.SetOrderEntity orderInfo = new JIT.CPOS.BS.Entity.Interface.SetOrderEntity();
            DataSet ds = new DataSet();
            OnlineShoppingItemService onlineShoppingItemService = new OnlineShoppingItemService(loggingSessionInfo);
            ds = onlineShoppingItemService.GetOrderOnline(OrderId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                orderInfo = DataTableToObject.ConvertToObject<JIT.CPOS.BS.Entity.Interface.SetOrderEntity>(ds.Tables[0].Rows[0]);
            }
            return orderInfo;
        }
        #endregion

        #region SetOrderAddress
        public bool SetOrderAddress(JIT.CPOS.BS.Entity.Interface.SetOrderEntity pEntity)
        {
            itemService.SetOrderAddress(pEntity);
            return true;
        }
        #endregion

        #region GetVipValidIntegral
        /// <summary>
        /// 获取Vip会员积分
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public string GetVipValidIntegral(string pVipID)
        {
            return itemService.GetVipValidIntegral(pVipID);
        }
        #endregion

        #region SetOrderIntegralInfo
        public bool SetOrderIntegralInfo(OrderIntegralEntity pEntity, string customerId, out string res)
        {
            decimal integral = this.itemService.GetItemIntegral(pEntity.ItemID);
            decimal integralAmmount = pEntity.Quantity.Value * integral;//使用积分
            decimal userIntegral = ToDecimal(this.itemService.GetVipValidIntegral(pEntity.VIPID));//用户剩余积分

            if (userIntegral - integralAmmount < 0)
            {
                res = "用户剩余积分不足";
                return false;
            }

            IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    /*插入订单积分、修改订单积分数据*/
                    //获取商品积分信息

                    pEntity.Integral = integral;
                    pEntity.IntegralAmmount = integralAmmount;

                    new OrderIntegralBLL(loggingSessionInfo).Create(pEntity, tran);

                    VipIntegralBLL vipIntegralServer = new VipIntegralBLL(loggingSessionInfo);
                    vipIntegralServer.ProcessPoint(18, customerId, pEntity.VIPID, "", null, "", -integralAmmount);

                    //VipIntegralDetailEntity integralDetailEntity = new VipIntegralDetailEntity();
                    //integralDetailEntity.VipIntegralDetailID = Guid.NewGuid().ToString().Replace("-", "");
                    //integralDetailEntity.VIPID = pEntity.VIPID;
                    //integralDetailEntity.Integral = integralAmmount;
                    //integralDetailEntity.IntegralSourceID = "18";//兑礼减积分
                    //integralDetailEntity.CreateBy = "system";
                    //integralDetailEntity.EffectiveDate = DateTime.Now;
                    //integralDetailEntity.ObjectId = pEntity.OrderIntegralID;

                    //new VipIntegralDetailBLL(loggingSessionInfo).Create(integralDetailEntity, tran);

                    //this.itemService.UpdateVIPIntegral(userIntegral - integralAmmount, pEntity.VIPID, (SqlTransaction)tran);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            res = "操作成功";
            return true;
        }
        #endregion

        #region GetOrderAmmount
        public decimal GetOrderAmmount(string pOrderID)
        {
            return itemService.GetOrderAmmount(pOrderID);
        }
        #endregion


        #region GetOrderPayCenterCode
        public string GetOrderPayCenterCode(string pOrderID)
        {
            return itemService.GetOrderPayCenterCode(pOrderID);
        }
        #endregion
        #region SetOrderPayCenterCode
        public void SetOrderPayCenterCode(string pOrderID, string paymentOrderID)
        {
            itemService.SetOrderPayCenterCode(pOrderID, paymentOrderID);
        }
        #endregion

        public DataSet GetStoreItemDailyStatuses(string beginDate, string endDate, string storeId, string itemId)
        {
            return itemService.GetStoreItemDailyStatuses(beginDate, endDate, storeId, itemId);
        }

        public DataSet GetStoreItemDailyStatuses(string beginDate, string endDate, string storeId, string itemId, string userId, string customerId)
        {
            return itemService.GetStoreItemDailyStatuses(beginDate, endDate, storeId, itemId, userId, customerId);
        }

        #region GetOrderIntegral
        /// <summary>
        /// 我的兑换记录
        /// </summary>
        /// <param name="pVipId"></param>
        /// <returns></returns>
        public DataSet GetOrderIntegral(string pVipId)
        {
            return itemService.GetOrderIntegral(pVipId);
        }
        #endregion

        #region GetProvince
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvince()
        {
            return itemService.GetProvince();
        }
        #endregion

        #region GetCityByProvince
        /// <summary>
        /// 根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public DataSet GetCityByProvince(string pProvince)
        {
            return itemService.GetCityByProvince(pProvince);
        }
        #endregion

        #region 花间堂_获取门店区域属性信息
        /// <summary>
        /// 花间堂_获取门店区域属性信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStoreArea()
        {
            return itemService.GetStoreArea();
        }
        #endregion

        #region GetStoreListByCityName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public List<StoreListEntity> GetStoreListByCityName(Dictionary<string, string> pParams)
        {
            List<StoreListEntity> storeList = new List<StoreListEntity>();
            List<StoreItemDailyStatusEntity> storeStatusList = new List<StoreItemDailyStatusEntity>();
            DataSet ds = new DataSet();
            ds = itemService.GetStoreListByCityName(pParams);
            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0
                && ds.Tables[1].Rows.Count > 0)
            {
                storeList = DataTableToObject.ConvertToList<StoreListEntity>(ds.Tables[0]);
                storeStatusList = DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(ds.Tables[1]);
            }
            else
            {
                return null;
            }

            DataTable dt = ConstuctIsFullDataTable(DateTime.Parse(pParams["pBeginDate"]), DateTime.Parse(pParams["pEndDate"]), ds.Tables[1]);

            for (int i = 0; i < storeList.Count; i++)
            {
                var info = dt.Select("StoreID='" + storeList[i].StoreID + "' and IsFull=0").ToArray();
                if (dt.Select("StoreID='" + storeList[i].StoreID + "' and IsFull=0").ToArray().Length > 0)
                {
                    storeList[i].IsFull = 0;
                }
            }

            return storeList;
        }
        #endregion

        #region GetStoreDetailByStoreID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public List<StoreDetailViewEntity> GetStoreDetailByStoreID(Dictionary<string, string> pParams)
        {
            List<StoreDetailViewEntity> storeList = new List<StoreDetailViewEntity>();
            List<StoreItemDailyStatusEntity> storeStatusList = new List<StoreItemDailyStatusEntity>();
            DataSet ds = new DataSet();
            ds = itemService.GetStoreDetailByStoreID(pParams);
            if (ds != null
                && ds.Tables.Count > 0
                && ds.Tables[0].Rows.Count > 0       //两张表
                && ds.Tables[1].Rows.Count > 0)
            {
                storeList = DataTableToObject.ConvertToList<StoreDetailViewEntity>(ds.Tables[0]);    //
                storeStatusList = DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(ds.Tables[1]);
            }
            else
            {
                return null;
            }
            //构造门店房间房态价格
            DataTable dt = ConstuctIsFullDataTable(DateTime.Parse(pParams["pBeginDate"]), DateTime.Parse(pParams["pEndDate"]), ds.Tables[1]);

            for (int i = 0; i < storeList.Count; i++)
            {
                var info = dt.Select("skuid='" + storeList[i].SkuID + "' and IsFull=0").ToArray();
                if (dt.Select("skuid='" + storeList[i].SkuID + "' and IsFull=0").ToArray().Length > 0)
                {
                    storeList[i].IsFull = 0;
                }
            }

            return storeList;
        }
        #endregion

        #region ConstuctIsFullDataTable
        /// <summary>
        /// 构造门店房间房态表格
        /// </summary>
        /// <param name="pBeginDate"></param>
        /// <param name="pEndDate"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private DataTable ConstuctIsFullDataTable(DateTime pBeginDate, DateTime pEndDate, DataTable dtStoreSKUStatus)
        {
            DateTime beginDate = pBeginDate;
            #region 构建dt
            DataTable dt = new DataTable();
            dt.Columns.Add("StoreID", typeof(string));
            dt.Columns.Add("SkuID", typeof(string));
            dt.Columns.Add("IsFull", typeof(int));
            for (; pBeginDate < pEndDate; pBeginDate += new TimeSpan(24, 0, 0))
            {
                string date = pBeginDate.ToString("yyyy-MM-dd");
                dt.Columns.Add(date, typeof(int));
            }

            for (int i = 0; i < dtStoreSKUStatus.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dtStoreSKUStatus.Columns.Count; j++)
                {
                    DataColumn dc = dtStoreSKUStatus.Columns[j];
                    if (j < 2)
                    {
                        dr[dc.ColumnName] = dtStoreSKUStatus.Rows[i][dc.ColumnName].ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dtStoreSKUStatus.Rows[i][dc.ColumnName].ToString()))
                        {
                            dr[dc.ColumnName] = int.Parse(dtStoreSKUStatus.Rows[i][dc.ColumnName].ToString());
                        }
                        else
                        {
                            dr[dc.ColumnName] = 0;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            #endregion

            #region 给IsFull赋值
            //原则，其中任何一天满房，则满房，如果整个时间段都不满放，则不满房
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                pBeginDate = beginDate;
                for (; pBeginDate < pEndDate; pBeginDate += new TimeSpan(24, 0, 0))
                {
                    string date = pBeginDate.ToString("yyyy-MM-dd");
                    if (int.Parse(dr[date].ToString()) == 0)
                    {
                        dr["IsFull"] = 1;
                        break;
                    }
                    else
                    {
                        dr["IsFull"] = 0;
                    }
                }
            }
            #endregion

            return dt;
        }
        #endregion

        #region GetProvinceOfHS
        /// <summary>
        /// 华硕校园获取省份信息_根据校园大使获取
        /// </summary>
        /// <returns></returns>
        public DataSet GetProvinceOfHS()
        {
            return itemService.GetProvinceOfHS();
        }
        #endregion

        #region GetCityByProvinceOfHS
        /// <summary>
        /// 根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public DataSet GetCityByProvinceOfHS(string pClientID, string pProvince)
        {
            return itemService.GetCityByProvinceOfHS(pClientID, pProvince);
        }
        #endregion

        #region GetSchoolListByCityNameOfHS
        /// <summary>
        /// 根据城市名称获取学校信息
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet GetSchoolListByCityNameOfHS(Dictionary<string, string> pParams)
        {
            return itemService.GetSchoolListByCityNameOfHS(pParams);
        }
        #endregion

        #region GetRoleID
        /// <summary>
        /// 根据城市名称获取学校信息
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public string GetRoleID(string pClientID)
        {
            return itemService.GetRoleID(pClientID);
        }
        #endregion

        #region getWEventByPhone
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPhone"></param>
        /// <returns></returns>
        public DataSet getWEventByPhone(string pUserID)
        {
            return itemService.getWEventByPhone(pUserID);
        }
        #endregion

        #region UpdateWEventByPhone
        public void UpdateWEventByPhone(WEventUserMappingEntity pEntity)
        {
            this.itemService.UpdateWEventByPhone(pEntity);
        }
        #endregion

        #region HS_GetVipDetail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPhone"></param>
        /// <returns></returns>
        public DataSet HS_GetVipDetail(string pVipID)
        {
            return itemService.HS_GetVipDetail(pVipID);
        }
        #endregion

        #region SendMail
        /// <summary>
        /// 下单发邮件
        /// </summary>
        /// <param name="pMailTo"></param>
        /// <param name="pTitle"></param>
        /// <param name="pBody"></param>
        public void SendMail(string pMailTo, string pTitle, string pBody)
        {
            FromSetting fs = new FromSetting();
            fs.SMTPServer = "smtp.exmail.qq.com";
            fs.SendFrom = "mailcenter@acmlife.org";
            fs.UserName = "mailcenter@acmlife.org";
            fs.Password = "asus+123456";
            Mail.SendMail(fs, pMailTo, pTitle, pBody, null);


        }
        #endregion

        #region GetItemDetailByItemIdAndStoreID
        /// <summary>
        /// 获取商品明细
        /// </summary>
        /// <param name="pStoreID"></param>
        /// <param name="pItemID"></param>
        /// <returns></returns>
        public DataSet GetItemDetailByItemIdAndStoreID(string pStoreID, string pItemID)
        {
            return itemService.GetItemDetailByItemIdAndStoreID(pStoreID, pItemID);
        }
        #endregion

        #region GetStoreListByArea
        /// <summary>
        /// 根据区域名称获取门店列表
        /// </summary>
        /// <param name="pAreaName"></param>
        /// <returns></returns>
        public DataSet GetStoreListByArea(string pAreaName)
        {
            return itemService.GetStoreListByArea(pAreaName);
        }
        #endregion

        #region Asus

        #region AmbassadorLoginIn
        /// <summary>
        /// 华硕校园大使登录
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public List<VipViewEntity> AmbassadorLoginIn(Dictionary<string, string> pParams)
        {
            List<VipViewEntity> vipEntityList = null;
            DataSet ds = new DataSet();
            ds = itemService.AmbassadorLoginIn(pParams);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                vipEntityList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            else
            {
                return null;
            }
            return vipEntityList;
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 根据用户名或手机号 查找会员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public List<VipViewEntity> GetUserList(Dictionary<string, string> pParams, out int rowCount)
        {
            List<VipViewEntity> vipEntityList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetUserList(pParams);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                vipEntityList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return vipEntityList;
        }
        #endregion

        #region GetOrderList
        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public List<TInoutViewEntity> GetOrderList(string pUserID, int pageSize, int pageIndex, out int rowCount)
        {
            List<TInoutViewEntity> orderEntityList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetOrderList(pUserID, pageSize, pageIndex);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                orderEntityList = DataTableToObject.ConvertToList<TInoutViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return orderEntityList;
        }
        #endregion

        #region GetBuyWay
        /// <summary>
        /// 获取购买方式信息
        /// </summary>
        /// <returns></returns>
        public List<OptionsEntity> GetBuyWay()
        {
            List<OptionsEntity> optionList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetBuyWay();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                optionList = DataTableToObject.ConvertToList<OptionsEntity>(ds.Tables[0]);
            }
            else
            {
                return null;
            }
            return optionList;
        }
        #endregion

        #region GetWay
        /// <summary>
        /// 获取客户获取方式信息
        /// </summary>
        /// <returns></returns>
        public List<OptionsEntity> GetWay()
        {
            List<OptionsEntity> optionList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetWay();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                optionList = DataTableToObject.ConvertToList<OptionsEntity>(ds.Tables[0]);
            }
            else
            {
                return null;
            }
            return optionList;
        }
        #endregion

        #region ForgetPassword
        /// <summary>
        /// 根据邮箱获取密码
        /// </summary>
        /// <param name="pEmail"></param>
        /// <returns></returns>
        public List<VipViewEntity> ForgetPassword(string pEmail)
        {
            List<VipViewEntity> vipEntityList = null;
            DataSet ds = new DataSet();
            ds = itemService.ForgetPassWord(pEmail);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipEntityList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            return vipEntityList;
        }
        #endregion

        #endregion

        #region CEIBS

        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="rowCount">视频个数</param>
        /// <returns></returns>
        public List<LEventsAlbumViewEntity> GetAlbumList(string pVipID, int pageSize, int pageIndex, out int rowCount)
        {
            List<LEventsAlbumViewEntity> videoList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetAlbumList(pVipID, pageSize, pageIndex);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                videoList = DataTableToObject.ConvertToList<LEventsAlbumViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return videoList;
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="rowCount">视频个数</param>
        /// <returns></returns>
        public List<LEventsAlbumViewEntity> GetEventAlbumList(string pVipID, int pageSize, int pageIndex, out int rowCount)
        {
            List<LEventsAlbumViewEntity> videoList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetEventAlbumList(pVipID, pageSize, pageIndex);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                videoList = DataTableToObject.ConvertToList<LEventsAlbumViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return videoList;
        }
        #endregion

        #region AddNewsCount
        /// <summary>
        /// 浏览 收藏数据的操作
        /// </summary>
        /// <param name="pNewsID">数据ID</param>
        /// <param name="pVipID">用户ID</param>
        /// <param name="pCountType">操作类型 <1.BrowseCount(浏览数量) 2.PraiseCount(赞的数量) 3.KeepCount(收藏数量)> </param>
        /// <param name="pNewsType">数据类型 <1.咨询 2.视频 3.活动></param>
        /// <returns></returns>
        public int AddEventStats(string pNewsID, string pVipID, string pCountType, string pNewsType)
        {
            return itemService.AddEventStats(pNewsID, pVipID, pCountType, pNewsType);
        }
        #endregion

        #region GetMostConcern
        /// <summary>
        /// 最受关注列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStats()
        {
            return itemService.GetEventStats();
        }
        #endregion

        #region GetMostDetail
        /// <summary>
        ///获取点赞数量
        /// </summary>
        /// <param name="pEventsID"></param>
        /// <returns></returns>
        public int GetMostDetail(string pEventsID)
        {
            return itemService.GetMostDetail(pEventsID);
        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细 
        /// </summary>
        /// <param name="pCourseType">课程类型<1=MBA 2=EMBA 3=FMBA 4=高级经理课程></param>
        /// <returns></returns>
        public List<ZCourseEntity> GetCourseInfo(string pCourseType)
        {
            List<ZCourseEntity> courseList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetCourseInfo(pCourseType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                courseList = DataTableToObject.ConvertToList<ZCourseEntity>(ds.Tables[0]);
            }
            return courseList;
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public List<VipViewEntity> GetUserInfo(string pVipID)
        {
            List<VipViewEntity> vipList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetUserInfo(pVipID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            return vipList;
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public List<VipViewEntity> GetUserList(string pVipName, string pClass, string pCompany, string pCity, string pVipID)
        {
            List<VipViewEntity> vipList = null;
            DataSet ds = new DataSet();
            ds = itemService.GetUserList(pVipName, pClass, pCompany, pCity, pVipID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            return vipList;
        }
        #endregion

        #region getNewsDetailByNewsID
        public reqNewsEntity getEventstatsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            reqNewsEntity pNewsEntity = new reqNewsEntity();
            DataSet ds = this.itemService.getEventstatsDetailByNewsID(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                NewsDetailEntity[] entity = DataLoader.LoadFrom<NewsDetailEntity>(ds.Tables[0]);
                pNewsEntity.News = entity[0];
            }
            return pNewsEntity;
        }
        #endregion

        #region GetVipPayMent
        /// <summary>
        /// 根据用户获取会费价格
        /// </summary>
        /// <param name="pVipID">用户ID</param>
        /// <returns></returns>
        public DataSet GetVipPayMent(string pVipID)
        {
            DataSet ds = new DataSet();
            ds = itemService.GetVipPayMent(pVipID);
            return ds;
        }
        #endregion

        #region SaveIoutData
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="inoutEntity"></param>
        /// <param name="ItemId">物资ID</param>
        /// <param name="VipId">用户ID</param>
        /// <returns></returns>
        public int SubmitVipPayMent(T_InoutEntity inoutEntity, string ItemId, string VipId)
        {

            int i = itemService.SubmitVipPayMent(inoutEntity, ItemId, VipId);
            return i;
        }

        #endregion
        #endregion


    }
}
