using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class ItemPriceService : Base.BaseCPOSDAO
    {
          #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ItemPriceService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 根据商品获取商品价格集合
        /// <summary>
        /// 根据商品获取商品价格集合
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public DataSet GetItemPriceListByItemId(string item_id)
        {
            DataSet ds = new DataSet();
            string sql = "select a.item_price_id "
                      + " ,a.item_id "
                      + " ,tt.item_price_type_id "
                      + " ,a.item_price "
                      + " ,a.status "
                      + " ,a.create_user_id "
                      + " ,a.create_time "
                      + " ,a.modify_user_id "
                      + " ,a.modify_time "
                      + " ,tt.item_price_type_name "
                      + " ,a.customer_id "
                      + " From T_Item_Price a right join t_item_price_type tt on a.item_price_type_id = tt.item_price_type_id and a.item_id= '" + item_id + "' "
                      + " and a.status = '1' "
                      + " and a.customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString() + "'"
            + " ORDER BY tt.create_user_id ";
                

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
           
        }
        #endregion

        #region 设置商品与商品价格关系
        /// <summary>
        /// 设置商品与商品类型与价格关系
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemPriceInfo(ItemInfo itemInfo, IDbTransaction pTran,out string strError)
        {
            try
            {
                itemInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (itemInfo.ItemPriceList != null)
                {
                    foreach (ItemPriceInfo itemPriceInfo in itemInfo.ItemPriceList)
                    {
                        if (itemPriceInfo.item_price_id == null || itemPriceInfo.item_price_id.Equals(""))
                        {
                            itemPriceInfo.item_price_id = NewGuid();
                        }
                        itemPriceInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    }
                    if (!SetItemPriceStatus(itemInfo, pTran))
                    {
                        strError = "插入商品信息失败";
                        return false;
                    }

                    if (itemInfo.ItemPriceList.Count > 0)
                    {
                        if (!SetItemPriceUpdate(itemInfo, pTran))
                        {
                            strError = "修改商品价格失败";
                            return false;
                        }

                        if (!SetItemPriceInsert(itemInfo, pTran))
                        {
                            strError = "插入商品价格失败";
                            return false;
                        }
                    }
                }
                strError = "处理商品信息成功.";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 删除商品价格
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPriceStatus(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            
            string sql = "update t_item_price set status = '-1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", itemInfo.Modify_Time);
            sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = sql + " where item_id = '" + itemInfo.Item_Id + "' and customer_id = '"+this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString() +"' ";
            
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 修改商品价格
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPriceUpdate(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "update t_item_price set status = '1' ,item_price = a.item_price";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", itemInfo.Modify_Time);
            sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = sql + " From ( ";
            int i = 0;
            foreach (ItemPriceInfo itemPriceInfo in itemInfo.ItemPriceList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' item_id "
                          + ",'" + itemPriceInfo.item_price + "' item_price "
                          + ",'" + itemPriceInfo.item_price_type_id + "' item_price_type_id "
                          + ",'" + itemPriceInfo.item_price_id + "' item_price_id "
                          + ",'" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' customer_id ";
                i++;
                          
            }
            sql = sql + " ) a, t_item_price x where ( x.item_id = a.item_id and x.item_price_type_id = a.item_price_type_id and x.customer_id = a.customer_id) or (x.item_price_id = a.item_price_id)";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入商品价格
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPriceInsert(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "insert into t_item_price "
                        + " (item_price_id "
                        + " ,item_id "
                        + " ,item_price_type_id "
                        + " ,item_price "
                        + " ,status "
                        + " ,create_user_id "
                        + " ,create_time "
                        + " ,modify_user_id "
                        + " ,modify_time "
                        + " ,customer_id "
                        + " ) "
                        + " SELECT P.item_price_id "
                        + " ,P.item_id "
                        + " ,P.item_price_type_id "
                        + " ,P.item_price "
                        + " ,P.status "
                        + " ,P.create_user_id "
                        + " ,P.create_time "
                        + " ,P.modify_user_id "
                        + " ,P.modify_time "
                        + " ,P.customer_id "
                        + " FROM ( ";
            int i = 0;
            foreach (ItemPriceInfo itemPriceInfo in itemInfo.ItemPriceList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' item_id "
                          + ",'" + itemInfo.Create_User_Id + "' modify_user_id "
                          + ",'" + itemInfo.Create_Time + "' modify_time "
                          + ",'" + itemInfo.Create_User_Id + "' create_user_id "
                          + ",'" + itemInfo.Create_Time + "' create_time "
                          + ",'1' status "
                          + ",'" + itemPriceInfo.item_price + "' item_price "
                          + ",'" + itemPriceInfo.item_price_type_id + "' item_price_type_id "
                          + ",'" + itemPriceInfo.item_price_id + "' item_price_id "
                          + ",'" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' customer_id ";
                i++;

            }
            

            sql = sql + " ) P "
                    + " left join t_item_price  b "
                    + " on(P.item_price_id = b.item_price_id) "
                    + " left join t_item_price c "
                    + " on(P.item_id = c.item_id "
                    + " and P.item_price_type_id = c.item_price_type_id "
                    + " and P.customer_id = c.customer_id) "
                    + " where b.item_price_id is null "
                    + " and c.item_id is null;";
           
            
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion

        #region 设置商品与商品图片关系
        /// <summary>
        /// 设置商品与商品图片关系
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemImageInfo(ItemInfo itemInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                itemInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (itemInfo.ItemImageList != null)
                {
                    foreach (var itemImageInfo in itemInfo.ItemImageList)
                    {
                        if (itemImageInfo.ImageId == null || itemImageInfo.ImageId.Equals(""))
                        {
                            itemImageInfo.ImageId = NewGuid();
                        }
                    }
                    if (!SetItemImageStatus(itemInfo, pTran))
                    {
                        strError = "删除图片信息失败";
                        return false;
                    }

                    if (itemInfo.ItemImageList.Count > 0)
                    {
                        if (!SetItemImageUpdate(itemInfo, pTran))
                        {
                            strError = "修改图片信息失败";
                            return false;
                        }

                        if (!SetItemImageInsert(itemInfo, pTran))
                        {
                            strError = "插入图片信息失败";
                            return false;
                        }
                    }
                }
                strError = "处理图片信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 删除图片信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemImageStatus(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update ObjectImages set IsDelete = '1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", itemInfo.Modify_Time);
            sql = sql + " where ObjectId = '" + itemInfo.Item_Id + "' ";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 修改图片信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemImageUpdate(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "update ObjectImages set IsDelete = '0' ,ImageURL = a.ImageURL, DisplayIndex = a.DisplayIndex, Title = a.Title, Description = a.Description ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", itemInfo.Modify_Time);
            sql = sql + " From ( ";
            int i = 0;
            foreach (var itemImageInfo in itemInfo.ItemImageList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' ObjectId "
                          + ",'" + itemImageInfo.ImageURL + "' ImageURL "
                          + ",'" + (itemImageInfo.DisplayIndex + 1) + "' DisplayIndex "//让DisplayIndex+1了
                          + ",'" + itemImageInfo.Title + "' Title "
                          + ",'" + itemImageInfo.Description + "' Description "
                          + ",'" + itemImageInfo.ImageId + "' ImageId "
                          ;
                i++;
            }
            sql = sql + " ) a, ObjectImages x where ( x.ObjectId = a.ObjectId and x.ImageURL = a.ImageURL and x.DisplayIndex = a.DisplayIndex) or (x.ImageId = a.ImageId)";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入图片信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemImageInsert(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "insert into ObjectImages "
                        + " (ImageId "
                        + " ,ObjectId "
                        + " ,ImageURL "
                        + " ,DisplayIndex "
                        + " ,CreateBy "
                        + " ,CreateTime "
                        + " ,LastUpdateBy "
                        + " ,LastUpdateTime "
                        + " ,IsDelete "
                        + " ,Title "
                        + " ,Description "
                        + " ,CustomerId "
                        + " ) "
                        + " SELECT P.ImageId "
                        + " ,P.ObjectId "
                        + " ,P.ImageURL "
                        + " ,P.DisplayIndex "
                        + " ,P.CreateBy "
                        + " ,P.CreateTime "
                        + " ,P.LastUpdateBy "
                        + " ,P.LastUpdateTime "
                        + " ,P.IsDelete "
                        + " ,P.Title "
                        + " ,P.Description "
                        + " ,P.CustomerId "
                        + " FROM ( ";
            int i = 0;
            foreach (var itemImageInfo in itemInfo.ItemImageList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' ObjectId "
                          + ",'" + itemInfo.Create_User_Id + "' LastUpdateBy "
                          + ",'" + itemInfo.Create_Time + "' LastUpdateTime "
                          + ",'" + itemInfo.Create_User_Id + "' CreateBy "
                          + ",'" + itemInfo.Create_Time + "' CreateTime "
                          + ",'" + itemImageInfo.ImageURL + "' ImageURL "
                          + ",'" + (itemImageInfo.DisplayIndex ?? 0+1) + "' DisplayIndex "
                          + ",'" + itemImageInfo.ImageId + "' ImageId "
                          + ",'0' IsDelete "
                          + ",'" + itemImageInfo.Title + "' Title "
                          + ",'" + itemImageInfo.Description + "' Description "
                          + ",'" + CurrentUserInfo.CurrentUser.customer_id + "' CustomerId "
                          ;
                i++;
            }

            sql = sql + " ) P "
                    + " left join ObjectImages b "
                    + " on(P.ImageId = b.ImageId) "
                    + " left join ObjectImages c "
                    + " on(P.ObjectId = c.ObjectId "
                    + " and P.ImageURL = c.ImageURL "
                    + " and P.DisplayIndex = c.DisplayIndex) "
                    + " where b.ImageId is null "
                    + " and c.ObjectId is null;";


            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion

        #region 设置门店与门店图片关系
        /// <summary>
        /// 设置门店与门店图片关系
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public bool SetUnitImageInfo(UnitInfo unitInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                unitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (unitInfo.ItemImageList != null)
                {
                    foreach (var itemImageInfo in unitInfo.ItemImageList)
                    {
                        if (itemImageInfo.ImageId == null || itemImageInfo.ImageId.Equals(""))
                        {
                            itemImageInfo.ImageId = NewGuid();
                        }
                    }
                    if (!SetUnitImageStatus(unitInfo, pTran))//先把门店相关的图片都删除 IsDelete=1
                    {
                        strError = "删除图片信息失败";
                        return false;
                    }

                    if (unitInfo.ItemImageList.Count > 0)
                    {
                        if (!SetUnitImageUpdate(unitInfo, pTran))//把这次带的图片的IsDelete=0
                        {
                            strError = "修改图片信息失败";
                            return false;
                        }

                        if (!SetUnitImageInsert(unitInfo, pTran))//插入图片信息
                        {
                            strError = "插入图片信息失败";
                            return false;
                        }
                    }
                }
                strError = "处理图片信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 删除图片信息
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitImageStatus(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update ObjectImages set IsDelete = '1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", unitInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", unitInfo.Modify_Time);
            sql = sql + " where ObjectId = '" + unitInfo.Id + "' ";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 修改图片信息
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitImageUpdate(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "update ObjectImages set IsDelete = '0' ,ImageURL = a.ImageURL, DisplayIndex = a.DisplayIndex ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", unitInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", unitInfo.Modify_Time);
            sql = sql + " From ( ";
            int i = 0;
            foreach (var itemImageInfo in unitInfo.ItemImageList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + unitInfo.Id + "' ObjectId "
                          + ",'" + itemImageInfo.ImageURL + "' ImageURL "
                          + ",'" + itemImageInfo.DisplayIndex + "' DisplayIndex "
                          + ",'" + itemImageInfo.ImageId + "' ImageId ";
                i++;
            }
            sql = sql + " ) a, ObjectImages x where ( x.ObjectId = a.ObjectId and x.ImageURL = a.ImageURL and x.DisplayIndex = a.DisplayIndex) or (x.ImageId = a.ImageId)";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 插入商品价格
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitImageInsert(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "insert into ObjectImages "
                        + " (ImageId "
                        + " ,ObjectId "
                        + " ,ImageURL "
                        + " ,DisplayIndex "
                        + " ,CreateBy "
                        + " ,CreateTime "
                        + " ,LastUpdateBy "
                        + " ,LastUpdateTime "
                        + " ,IsDelete,CustomerId "
                        + " ) "
                        + " SELECT P.ImageId "
                        + " ,P.ObjectId "
                        + " ,P.ImageURL "
                        + " ,P.DisplayIndex "
                        + " ,P.CreateBy "
                        + " ,P.CreateTime "
                        + " ,P.LastUpdateBy "
                        + " ,P.LastUpdateTime "
                        + " ,P.IsDelete,P.CustomerId "
                        + " FROM ( ";
            int i = 0;
            foreach (var itemImageInfo in unitInfo.ItemImageList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + unitInfo.Id + "' ObjectId "
                          + ",'" + unitInfo.Create_User_Id + "' LastUpdateBy "
                          + ",'" + unitInfo.Create_Time + "' LastUpdateTime "
                          + ",'" + unitInfo.Create_User_Id + "' CreateBy "
                          + ",'" + unitInfo.Create_Time + "' CreateTime "
                          + ",'" + itemImageInfo.ImageURL + "' ImageURL "
                          + ",'" + itemImageInfo.DisplayIndex + "' DisplayIndex "
                          + ",'" + itemImageInfo.ImageId + "' ImageId "
                          + ",'0' IsDelete "
                          + ",'" + this.loggingSessionInfo.CurrentUser.customer_id +"' CustomerId ";
                i++;
            }

            sql = sql + " ) P "
                    + " left join ObjectImages b "
                    + " on(P.ImageId = b.ImageId) "
                    + " left join ObjectImages c "
                    + " on(P.ObjectId = c.ObjectId "
                    + " and P.ImageURL = c.ImageURL "
                    + " and P.DisplayIndex = c.DisplayIndex) "
                    + " where b.ImageId is null "
                    + " and c.ObjectId is null;";


            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion

        #region 设置商品与门店关系
        /// <summary>
        /// 设置商品与门店关系
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemUnitInfo(ItemInfo itemInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                itemInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (itemInfo.ItemUnitList != null)
                {
                    foreach (ItemStoreMappingEntity itemUnitInfo in itemInfo.ItemUnitList)
                    {
                        if (itemUnitInfo.MappingId == null || itemUnitInfo.MappingId.Equals(""))
                        {
                            itemUnitInfo.MappingId = NewGuid();
                        }
                        //itemUnitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    }
                    if (!SetItemUnitStatus(itemInfo, pTran))
                    {
                        strError = "插入商品信息失败";
                        return false;
                    }

                    if (itemInfo.ItemUnitList.Count > 0)
                    {
                        if (!SetItemUnitUpdate(itemInfo, pTran))
                        {
                            strError = "修改商品门店失败";
                            return false;
                        }

                        if (!SetItemUnitInsert(itemInfo, pTran))
                        {
                            strError = "插入商品门店失败";
                            return false;
                        }
                    }
                }
                strError = "处理商品信息成功.";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 删除商品门店
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemUnitStatus(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update ItemStoreMapping set IsDelete = '1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", itemInfo.Modify_Time);
            //sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = sql + " where itemId = '" + itemInfo.Item_Id + "' ";
            //" and customer_id = '" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id.ToString() + "' ";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 修改商品门店
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemUnitUpdate(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "update ItemStoreMapping set isDelete = '0' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", itemInfo.Modify_Time);
            //sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = sql + " From ( ";
            int i = 0;
            foreach (ItemStoreMappingEntity itemUnitInfo in itemInfo.ItemUnitList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' itemId "
                          + ",'" + itemUnitInfo.UnitId + "' UnitId "
                          + ",'" + itemUnitInfo.MappingId + "' MappingId ";
                          //+ ",'" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' customer_id ";
                i++;

            }
            sql = sql + " ) a, ItemStoreMapping x where x.itemId = a.itemId and x.UnitId = a.UnitId";    // or (x.UnitId = a.UnitId)

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入商品门店
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemUnitInsert(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "insert into ItemStoreMapping "
                        + " (MappingId "
                        + " ,itemId "
                        + " ,UnitId "
                        + " ,IsDelete "
                        + " ,CreateBy "
                        + " ,CreateTime "
                        + " ,LastUpdateBy "
                        + " ,LastUpdateTime "
                        + " ) "
                        + " SELECT P.MappingId "
                        + " ,P.itemId "
                        + " ,P.UnitId "
                        + " ,P.IsDelete "
                        + " ,P.CreateBy "
                        + " ,P.CreateTime "
                        + " ,P.LastUpdateBy "
                        + " ,P.LastUpdateTime "
                        + " FROM ( ";
            int i = 0;
            foreach (ItemStoreMappingEntity itemUnitInfo in itemInfo.ItemUnitList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' itemId "
                          + ",'" + itemInfo.Create_User_Id + "' LastUpdateBy "
                          + ",'" + itemInfo.Create_Time + "' LastUpdateTime "
                          + ",'" + itemInfo.Create_User_Id + "' CreateBy "
                          + ",'" + itemInfo.Create_Time + "' CreateTime "
                          + ",'0' IsDelete "
                          + ",'" + itemUnitInfo.UnitId + "' UnitId "
                          + ",'" + itemUnitInfo.MappingId + "' MappingId ";
                          //+ ",'" + this.loggingSessionInfo.CurrentLoggingManager.Customer_Id + "' customer_id ";
                i++;

            }


            sql = sql + " ) P "
                    + " left join ItemStoreMapping  b "
                    + " on(P.MappingId = b.MappingId) "
                    + " left join ItemStoreMapping c "
                    + " on(P.itemId = c.itemId "
                    + " and P.UnitId = c.UnitId) "
                    //+ " and P.customer_id = c.customer_id) "
                    + " where b.UnitId is null "
                    + " and c.itemId is null;";


            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion

        #region 设置门店与品牌关系
        /// <summary>
        /// 设置门店与品牌关系
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public bool SetUnitBrandInfo(UnitInfo unitInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                unitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (unitInfo.ItemBrandList != null)
                {
                    foreach (var itemBrandInfo in unitInfo.ItemBrandList)
                    {
                        if (itemBrandInfo.MappingId == null || itemBrandInfo.MappingId.Equals(""))
                        {
                            itemBrandInfo.MappingId = NewGuid();
                        }
                    }
                    if (!SetUnitBrandStatus(unitInfo, pTran))
                    {
                        strError = "删除品牌信息失败";
                        return false;
                    }

                    if (unitInfo.ItemBrandList.Count > 0)
                    {
                        if (!SetUnitBrandUpdate(unitInfo, pTran))
                        {
                            strError = "修改品牌信息失败";
                            return false;
                        }

                        if (!SetUnitBrandInsert(unitInfo, pTran))
                        {
                            strError = "插入品牌信息失败";
                            return false;
                        }
                    }
                }
                strError = "处理品牌信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 删除品牌信息
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitBrandStatus(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update StoreBrandMapping set IsDelete = '1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", unitInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", unitInfo.Modify_Time);
            sql = sql + " where StoreId = '" + unitInfo.Id + "' ";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 修改品牌信息
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitBrandUpdate(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "update StoreBrandMapping set IsDelete = '0' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateBy", unitInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "LastUpdateTime", unitInfo.Modify_Time);
            sql = sql + " From ( ";
            int i = 0;
            foreach (var itemBrandInfo in unitInfo.ItemBrandList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + unitInfo.Id + "' StoreId "
                          + ",'" + itemBrandInfo.MappingId + "' MappingId "
                          + ",'" + itemBrandInfo.BrandId + "' BrandId ";
                i++;
            }
            sql = sql + " ) a, StoreBrandMapping x where ( x.BrandId = a.BrandId and x.StoreId = a.StoreId) or (x.BrandId = a.BrandId)";

            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        /// <summary>
        /// 插入品牌
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetUnitBrandInsert(UnitInfo unitInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = "insert into StoreBrandMapping "
                        + " (MappingId "
                        + " ,StoreId "
                        + " ,BrandId "
                        + " ,CreateBy "
                        + " ,CreateTime "
                        + " ,LastUpdateBy "
                        + " ,LastUpdateTime "
                        + " ,IsDelete "
                        + " ) "
                        + " SELECT P.MappingId "
                        + " ,P.StoreId "
                        + " ,P.BrandId "
                        + " ,P.CreateBy "
                        + " ,P.CreateTime "
                        + " ,P.LastUpdateBy "
                        + " ,P.LastUpdateTime "
                        + " ,P.IsDelete "
                        + " FROM ( ";
            int i = 0;
            foreach (var itemBrandInfo in unitInfo.ItemBrandList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + unitInfo.Id + "' StoreId "
                          + ",'" + unitInfo.Create_User_Id + "' LastUpdateBy "
                          + ",'" + unitInfo.Create_Time + "' LastUpdateTime "
                          + ",'" + unitInfo.Create_User_Id + "' CreateBy "
                          + ",'" + unitInfo.Create_Time + "' CreateTime "
                          + ",'" + itemBrandInfo.BrandId + "' BrandId "
                          + ",'" + itemBrandInfo.MappingId + "' MappingId "
                          + ",'0' IsDelete ";
                i++;
            }

            sql = sql + " ) P "
                    + " left join StoreBrandMapping b "
                    + " on(P.MappingId = b.MappingId) "
                    + " left join StoreBrandMapping c "
                    + " on(P.StoreId = c.StoreId "
                    + " and P.BrandId = c.BrandId) "
                    + " where b.BrandId is null "
                    + " and c.StoreId is null;";


            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }

        #endregion

        #region 设置门店与分类关系
        /// <summary>
        /// 设置门店与分类关系
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public bool SetUnitSortMappingInfo(UnitInfo unitInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                unitInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (unitInfo.UnitSortMappingList != null)
                {
                    TUnitUnitSortMappingDAO tUnitUnitSortMappingDAO = new TUnitUnitSortMappingDAO(CurrentUserInfo);
                    var mappingList = tUnitUnitSortMappingDAO.QueryByEntity(
                        new TUnitUnitSortMappingEntity() { 
                            UnitId = unitInfo.Id
                        }, null);
                    if (mappingList != null && mappingList.Length > 0)
                    {
                        foreach (var mappingItem in mappingList)
                        {
                            tUnitUnitSortMappingDAO.Delete(mappingItem);
                        }
                    }

                    if (unitInfo.UnitSortMappingList != null && unitInfo.UnitSortMappingList.Count > 0)
                    {
                        foreach (var mappingItem in unitInfo.UnitSortMappingList)
                        {
                            tUnitUnitSortMappingDAO.Create(mappingItem);
                        }
                    }
                }
                strError = "处理分类信息成功";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 设置商品与分类关系
        /// <summary>
        /// 设置商品与分类关系
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemCategoryInfo(ItemInfo itemInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                itemInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                ItemCategoryMappingDAO itemCategoryMappingDAO = new ItemCategoryMappingDAO(loggingSessionInfo);
                var tmpItemCategoryList = itemCategoryMappingDAO.QueryByEntity(new ItemCategoryMappingEntity()
                {
                    ItemId = itemInfo.Item_Id
                }, null);
                if (tmpItemCategoryList != null && tmpItemCategoryList.Length > 0)
                {
                    foreach (var tmpItemCategoryItem in tmpItemCategoryList)
                    {
                        itemCategoryMappingDAO.Delete(tmpItemCategoryItem);
                    } 
                }

                if (itemInfo.ItemCategoryList != null)
                {
                    foreach (TItemTagEntity itemTag in itemInfo.ItemCategoryList)
                    {

                        itemCategoryMappingDAO.Create(new ItemCategoryMappingEntity()
                        {
                            MappingId = Guid.Parse(NewGuid()),
                            ItemId = itemInfo.Item_Id,
                            ItemCategoryId = itemTag.ItemTagID.Value.ToString(),
                            IsFirstVisit = itemTag.IsFirstVisit
                        });
                    }

                }


                strError = "处理商品信息成功.";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
    }
}
