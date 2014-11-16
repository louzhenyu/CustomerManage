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

namespace JIT.CPOS.BS.DataAccess
{
    public class SkuService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public SkuService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据商品获取sku信息集合
        /// </summary>
        /// <param name="itemId">商品标识</param>
        /// <returns></returns>
        public DataSet GetSkuListByItemId(string itemId)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("") + " From vw_sku a  where a.item_id = '" + itemId + "' and a.status = '1' order by a.item_code,a.barcode";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据商品获取抢购sku信息集合
        /// </summary>
        /// <param name="itemId">商品标识</param>
        /// <returns></returns>
        public DataSet GetSkuListByItemIdAndEventId(string itemId, Guid eventid)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("") + @" From vw_sku a join PanicbuyingEventSkuMapping b on a.sku_id=b.skuid
                    join PanicbuyingEventItemMapping c on b.EventItemMappingId=c.EventItemMappingId
                    where a.item_id = '" + itemId + @"' and a.status = '1' and c.eventid='" + eventid + @"'
                        order by a.item_code,a.barcode";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取所有sku
        /// </summary>
        /// <returns></returns>
        public DataSet GetSkuList()
        {
            DataSet ds = new DataSet();
            string sql = GetSql("") + " From vw_sku a where a.status = '1' order by a.item_code,a.barcode";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 根据sku标识获取sku信息
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public DataSet GetSkuInfoById(string skuId)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("") + " From vw_sku a where a.status = '1' and sku_id= '" + skuId + "' order by a.item_code,a.barcode";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 模糊查询sku集合
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataSet GetSkuInfoByLike(string condition)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("")
              + " From ( "
              + " select a.* "
              + " ,row_no=row_number() over(order by a.sku_id) "
              + " from vw_sku a "
              + " where a.item_code like '%' + '" + condition + "' + '%' "
              + " or a.item_name like '%' +  '" + condition + "' + '%' "
              + " or a.prop_1_detail_code like '%' +  '" + condition + "' + '%' "
              + " or a.prop_2_detail_code like '%' +  '" + condition + "' + '%' "
              + " or a.prop_3_detail_code like '%' +  '" + condition + "' + '%' "
              + " or a.prop_4_detail_code like '%' +  '" + condition + "' + '%' "
              + " or a.prop_5_detail_code like '%' +  '" + condition + "' + '%' "
              + " ) x where x.row_no > 0 and x.row_no <=10 "
              + " order by x.item_code,x.barcode ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 模糊查询item集合
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataSet GetItemInfoByLike(string condition)
        {
            DataSet ds = new DataSet();
            string sql = "select item_id "
                      + " ,item_code "
                      + " ,item_name "
              + " From ( "
              + " select a.* "
              + " ,row_no=row_number() over(order by a.item_code) "
              + " from t_item a "
              + " where a.item_code like '%' + '" + condition + "' + '%' "
              + " or a.item_name like '%' +  '" + condition + "' + '%' "
              + " ) x where x.row_no > 0 and x.row_no <=10 "
              + " order by x.item_code,x.item_name ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetSkuInfoByOne()
        {
            DataSet ds = new DataSet();
            string sql = GetSql("")
              + " From ( "
              + " select top 1 a.* "
              + " ,row_no=row_number() over(order by a.sku_id) "
              + " from vw_sku a "

              + " ) x where x.row_no > 0 and x.row_no <=10 "
              + " order by x.item_code,x.barcode ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataTable GetSKUAndItemBySKUIDs(string[] pSKUIDs)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.sku_id,a.item_id,b.item_name,b.imageurl,c.prop_1_detail_name,c.prop_2_detail_name,c.prop_3_detail_name,c.prop_4_detail_name,c.prop_5_detail_name from t_sku a left join vw_item_detail b on a.item_id =b.item_id left join vw_sku_detail c on a.sku_id=c.sku_id where 1=1");
            bool isFirst = true;
            sql.Append(" and a.sku_id in (");
            foreach (var id in pSKUIDs)
            {
                if (isFirst)
                {
                    sql.AppendFormat("'{0}'", id);
                    isFirst = false;
                }
                else
                {
                    sql.AppendFormat(",'{0}'", id);
                }
            }
            sql.Append(")");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 新建，修改
        /// <summary>
        /// 判断是否重复
        /// </summary>
        /// <param name="barcode">条形码</param>
        /// <param name="sku_id">sku标识</param>
        /// <returns></returns>
        public int IsExistBarcode(string barcode, string sku_id)
        {
            int count = 0;
            string sql = "select isnull(count(*),0) From t_sku where 1=1 and barcode = '" + barcode + "' ";
            PublicService pService = new PublicService();
            sql = pService.GetLinkSql(sql, "sku_id", sku_id, "!=");

            count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql).ToString());
            return count;
        }
        /// <summary>
        /// 设置sku信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetSkuInfo(ItemInfo itemInfo)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //修改sku下载标志
                    UpdateSkuFlag(itemInfo, tran);

                    if (itemInfo.SkuList != null)
                    {
                        foreach (SkuInfo skuInfo in itemInfo.SkuList)
                        {
                            if (skuInfo.item_id == null) skuInfo.item_id = itemInfo.Item_Id;
                            if (skuInfo.sku_id == null || skuInfo.sku_id.Equals("")) skuInfo.sku_id = NewGuid();
                            if (skuInfo.modify_time == null || skuInfo.modify_time.Equals("")) skuInfo.modify_time = GetCurrentDateTime();
                            if (skuInfo.modify_user_id == null || skuInfo.modify_user_id.Equals("")) skuInfo.modify_user_id = itemInfo.Create_User_Id;
                            if (skuInfo.create_time == null || skuInfo.create_time.Equals("")) skuInfo.create_time = GetCurrentDateTime();
                            if (skuInfo.create_user_id == null || skuInfo.create_user_id.Equals("")) skuInfo.create_user_id = itemInfo.Create_User_Id;
                            UpdateSku(skuInfo, tran);//修改
                            InsertSku(skuInfo, tran);//删除

                            //处理sku相关价格信息(jifeng.cao 20140224)
                            foreach (SkuPriceInfo skuPriceInfo in skuInfo.sku_price_list)
                            {
                                if (skuPriceInfo.sku_price != -1)
                                {
                                    if (skuPriceInfo.sku_id == null) skuPriceInfo.sku_id = skuInfo.sku_id;
                                    if (skuPriceInfo.sku_price_id == null || skuPriceInfo.sku_price_id.Equals("")) skuPriceInfo.sku_price_id = NewGuid();
                                    if (skuPriceInfo.modify_time == null || skuPriceInfo.modify_time.Equals("")) skuPriceInfo.modify_time = GetCurrentDateTime();
                                    if (skuPriceInfo.modify_user_id == null || skuPriceInfo.modify_user_id.Equals("")) skuPriceInfo.modify_user_id = skuInfo.create_user_id;
                                    if (skuPriceInfo.create_time == null || skuPriceInfo.create_time.Equals("")) skuPriceInfo.create_time = GetCurrentDateTime();
                                    if (skuPriceInfo.create_user_id == null || skuPriceInfo.create_user_id.Equals("")) skuPriceInfo.create_user_id = skuInfo.create_user_id;
                                    new SkuPriceService(loggingSessionInfo).UpdateSkuPrice(skuPriceInfo, tran);//修改
                                    new SkuPriceService(loggingSessionInfo).InsertSkuPrice(skuPriceInfo, tran);//删除
                                }
                            }

                        }
                    }

                    if (itemInfo.SkuImageList != null && itemInfo.SkuImageList.Count > 0)
                    {
                        ObjectImagesDAO objectImagesDAO = new ObjectImagesDAO(CurrentUserInfo);
                        foreach (var imageItem in itemInfo.SkuImageList)
                        {
                            var exsitObj = objectImagesDAO.QueryByEntity(new ObjectImagesEntity()
                            {
                                ObjectId = imageItem.ObjectId,
                                ImageURL = imageItem.ImageURL
                            }, null);
                            if (exsitObj == null || exsitObj.Length == 0)
                            {
                                imageItem.ImageId = NewGuid();
                                imageItem.CreateTime = DateTime.Now;
                                imageItem.CreateBy = itemInfo.Create_User_Id;
                                imageItem.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                                objectImagesDAO.Create(imageItem);
                            }
                        }
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw (ex);
                }
            }
        }
        /// <summary>
        /// 更改下载标志
        /// </summary>
        /// <param name="itemInfo">商品对象</param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public void UpdateSkuFlag(ItemInfo itemInfo, IDbTransaction pTran)
        {
            string sql = "update t_sku "
                      + " set status= '-1' "
                      + " ,modify_user_id = '" + itemInfo.Create_User_Id + "' "
                      + " ,modify_time = '" + itemInfo.Create_Time + "'"
                      + " ,if_flag = '0' "
                      + " where  t_sku.item_id = '" + itemInfo.Item_Id + "';";
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 修改sku信息
        /// </summary>
        /// <param name="skuInfo"></param>
        /// <param name="pTran"></param>
        public void UpdateSku(SkuInfo skuInfo, IDbTransaction pTran)
        {
            string sql = "update t_sku "
                    + " set sku_prop_id1 = '" + skuInfo.prop_1_detail_id + "' "
                    + " ,sku_prop_id2 = '" + skuInfo.prop_2_detail_id + "' "
                    + " ,sku_prop_id3 = '" + skuInfo.prop_3_detail_id + "' "
                    + " ,sku_prop_id4 = '" + skuInfo.prop_4_detail_id + "' "
                    + " ,sku_prop_id5 = '" + skuInfo.prop_5_detail_id + "'  "
                    + " ,barcode = '" + skuInfo.barcode + "'  "
                    + " ,modify_user_id = '" + skuInfo.create_user_id + "'  "
                    + " ,modify_time = '" + skuInfo.create_time + "'  "
                    + " ,status = '1' "
                    + " ,if_flag = '0' where sku_id = '" + skuInfo.sku_id + "'";
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
        }
        /// <summary>
        /// 插入sku信息
        /// </summary>
        /// <param name="skuInfo"></param>
        /// <param name="pTran"></param>
        public void InsertSku(SkuInfo skuInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_sku "
                    + " (sku_id "
                    + " ,item_id "
                    + " ,sku_prop_id1 "
                    + " ,sku_prop_id2 "
                    + " ,sku_prop_id3 "
                    + " ,sku_prop_id4 "
                    + " ,sku_prop_id5 "
                    + " ,barcode "
                    + " ,status "
                    + " ,create_user_id "
                    + " ,create_time "
                    + " ,modify_user_id "
                    + " ,modify_time "
                    + " ) "
                    + " select a.* From ( select '" + skuInfo.sku_id + "' sku_id"
                    + ", '" + skuInfo.item_id + "' item_id"
                    + ", '" + skuInfo.prop_1_detail_id + "' sku_prop_id1"
                    + ", '" + skuInfo.prop_2_detail_id + "' sku_prop_id2"
                    + ", '" + skuInfo.prop_3_detail_id + "' sku_prop_id3"
                    + ", '" + skuInfo.prop_4_detail_id + "' sku_prop_id4"
                    + ", '" + skuInfo.prop_5_detail_id + "' sku_prop_id5"
                    + ", '" + skuInfo.barcode + "' barcode"
                    + ", '1' status"
                    + ", '" + skuInfo.create_user_id + "' create_user_id"
                    + ", '" + skuInfo.create_time + "' create_time"
                    + ", '" + skuInfo.create_user_id + "' modify_user_id"
                    + ", '" + skuInfo.create_time + "' modify_time"
                    + " ) a "
                    + " left join t_sku b on(a.sku_id = b.sku_id) where b.sku_id is null ;";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
        }
        #endregion

        #region 公共sql
        private string GetSql(string sql)
        {
            sql = sql + "select sku_id "
                      + " ,item_id "
                      + " ,item_code "
                      + " ,prop_1_detail_id "
                      + " ,prop_1_detail_name "
                      + " ,prop_2_detail_id "
                      + " ,prop_2_detail_name "
                      + " ,prop_3_detail_id "
                      + " ,prop_3_detail_name "
                      + " ,prop_4_detail_id "
                      + " ,prop_4_detail_name "
                      + " ,prop_5_detail_id "
                      + " ,prop_5_detail_name "
                      + " ,prop_1_detail_code "
                      + " ,prop_2_detail_code "
                      + " ,prop_3_detail_code "
                      + " ,prop_4_detail_code "
                      + " ,prop_5_detail_code "
                      + " ,status "
                      + " ,create_user_id "
                      + " ,create_time "
                      + " ,modify_user_id "
                      + " ,modify_time "
                      + " ,item_name "
                      + " ,item_code "
                      + " ,prop_1_id "
                      + " ,prop_1_name "
                      + " ,prop_2_id "
                      + " ,prop_2_name "
                      + " ,prop_3_id "
                      + " ,prop_3_name "
                      + " ,prop_4_id "
                      + " ,prop_4_name "
                      + " ,prop_5_id "
                      + " ,prop_5_name "
                      + " ,barcode "
                      + " ,prop_1_code "
                      + " ,prop_2_code "
                      + " ,prop_3_code "
                      + " ,prop_4_code "
                      + " ,prop_5_code ";

            return sql;
        }
        #endregion

        #region 获取同步福利SKU

        /// <summary>
        /// 获取同步福利SKU
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareSkuList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT skuId = a.sku_id ";
            sql += " , itemId = a.item_id ";
            sql += " , skuprop1 = a.prop_1_detail_id ";
            sql += " , skuprop2 = a.prop_2_detail_id ";
            sql += " , skuprop3 = a.prop_3_detail_id ";
            sql += " , skuprop4 = a.prop_4_detail_id ";
            sql += " , salesprice = a.SalesPrice ";
            sql += " , price = a.Price ";
            sql += " , discountrate = a.DiscountRate ";
            sql += " , integral = a.Integral ";
            sql += " , displayindex = row_number() over(order by a.modify_time DESC) ";
            sql += " , isdelete =  CASE a.[status] WHEN '1' THEN '0' ELSE '1' END ";
            sql += " FROM dbo.vw_sku_detail a ";
            sql += " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND a.modify_time >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 抢购SKUList
        /// <summary>
        /// 获取商品sku集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSkuListByEventId(string itemId, Guid eventId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT skuId = a.SkuID ");
            sql.AppendLine(" ,skuProp1 = a.Prop1Name ");
            sql.AppendLine(" ,skuProp2 = a.Prop2Name ");
            sql.AppendLine(" ,price = a.Price ");
            sql.AppendLine(" ,salesPrice = a.SalesPrice ");
            sql.AppendLine(" ,discountRate = convert(decimal(18,1),a.DiscountRate)  ");
            sql.AppendLine(" ,integral = a.IntegralExchange ");
            sql.AppendLine(" ,qty=a.qty");
            sql.AppendLine(" ,overQty=a.RemainingQty");
            sql.AppendLine(@" FROM vwPEventItemDetail a");
            sql.AppendLine(string.Format(" WHERE a.itemid = '{0}' and a.eventid='{1}'", itemId, eventId));
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        #endregion
    }
}
