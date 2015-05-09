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
              + " from vw_sku a  where a.status='1'"

              + " ) x where x.row_no > 0 and x.row_no <=10 "
              + " order by x.item_code,x.barcode ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataTable GetSKUAndItemBySKUIDs(string[] pSKUIDs)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select a.sku_id,a.item_id,b.item_name,b.imageurl
,prop_1_name,c.prop_1_detail_name,prop_2_name,c.prop_2_detail_name
,prop_3_name,c.prop_3_detail_name,prop_4_name,c.prop_4_detail_name,prop_5_name,c.prop_5_detail_name 
from t_sku a 
left join vw_item_detail b on a.item_id =b.item_id 
left join vw_sku_detail c on a.sku_id=c.sku_id 
where 1=1   ");
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
        public bool SetSkuInfo(ItemInfo itemInfo, IDbTransaction pTran, out string strError)
        {
           var skupriceService=  new SkuPriceService(loggingSessionInfo);
            //using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            //{
                try
                {




                    //修改sku下载标志
                    UpdateSkuFlag(itemInfo, pTran);

                    if (itemInfo.SkuList != null)
                    {


                        // T_Item_Price_Type
                        var priceTypeService = new ItemPriceTypeService(loggingSessionInfo);
                        var priceTypeList = priceTypeService.GetItemPriceTypeList();
                        //
                        var skustockTypeId = "";//库存id
                        var skusalesCountTypeId = "";//销量id
                        if (priceTypeList != null && priceTypeList.Tables.Count != 0)
                        {
                            var priceTypeTable = priceTypeList.Tables[0];
                            foreach (DataRow row in priceTypeTable.Rows)
                            {
                                if (row["item_price_type_code"] != null && row["item_price_type_code"].ToString() == "库存")
                                {
                                    skustockTypeId = row["item_price_type_id"].ToString();
                                }
                                if (row["item_price_type_code"] != null && row["item_price_type_code"].ToString() == "销量")
                                {
                                    skusalesCountTypeId = row["item_price_type_id"].ToString();
                                }
                            }
                        }
                        //还要遍历看看商品的库存和销量
                        var ItemStockPropId = "";//库存id
                        var ItemsalesCountPropId = "";//销量id
                        var propService = new PropService(loggingSessionInfo);
                        //
                        PropInfo pro1 = new PropInfo();
                        pro1.Prop_Code = "Qty";
                        pro1.Prop_Domain = "ITEM";
                        pro1.Prop_Type = "2";
                        var pro1DataSet = propService.GetWebProp(pro1, 0, 10);
                        if (pro1DataSet != null && pro1DataSet.Tables.Count != 0)
                        {
                            var pro1Table = pro1DataSet.Tables[0];
                            if (pro1Table.Rows.Count > 0)
                            {
                                ItemStockPropId = pro1Table.Rows[0]["prop_id"].ToString();
                            }
                        }

                        //
                        PropInfo pro2 = new PropInfo();
                        pro2.Prop_Code = "SalesCount";
                        pro2.Prop_Domain = "ITEM";
                        pro2.Prop_Type = "2";
                        var pro2DataSet = propService.GetWebProp(pro2, 0, 10);
                        if (pro2DataSet != null && pro2DataSet.Tables.Count != 0)
                        {
                            var pro2Table = pro2DataSet.Tables[0];
                            if (pro2Table.Rows.Count > 0)
                            {
                                ItemsalesCountPropId = pro2Table.Rows[0]["prop_id"].ToString();
                            }
                        }

                        //商品的属性****
                        ItemPropService itemPropService = new ItemPropService(loggingSessionInfo);
                        var ds = itemPropService.GetItemPropListByItemId(itemInfo.Item_Id,pTran);//绑到事务里
                        var stockItemProp = new ItemPropInfo();
                        var SalesCountProp = new ItemPropInfo();
                        if (ds != null && ds.Tables.Count != 0)
                        {
                            var dt = ds.Tables[0];
                            var drs = dt.Select(" PropertyCodeId='" + ItemStockPropId + "'");//原来prop_id
                            if (drs != null && drs.Length != 0)
                            {
                                var row=drs[0];
                                stockItemProp.Item_Property_Id = row["item_property_id"].ToString();
                                try
                                {
                                    stockItemProp.PropertyCodeValue = row["PropertyCodeValue"].ToString();//库存属性之
                                    int temp = Convert.ToInt32(row["PropertyCodeValue"].ToString());
                                }
                                catch {
                                    stockItemProp.PropertyCodeValue ="0";
                                }
                            }
                            //
                            var drs2 = dt.Select(" PropertyCodeId='" + ItemsalesCountPropId + "'");
                            if (drs2 != null && drs2.Length != 0)
                            {
                                var row = drs2[0];
                                SalesCountProp.Item_Property_Id = row["item_property_id"].ToString();
                                try
                                {
                                    SalesCountProp.PropertyCodeValue = row["PropertyCodeValue"].ToString();
                                    int temp = Convert.ToInt32(row["PropertyCodeValue"].ToString());//出异常时，就为0
                                }
                                catch
                                {
                                    SalesCountProp.PropertyCodeValue = "0";//销量属性
                                }
                            }

                        }



                        decimal stockCountTemp = 0;
                        decimal salesCountTemp = 0;


                        foreach (SkuInfo skuInfo in itemInfo.SkuList)
                        {
                            if (skuInfo.item_id == null) skuInfo.item_id = itemInfo.Item_Id;
                            if (skuInfo.sku_id == null || skuInfo.sku_id.Equals("")) skuInfo.sku_id = NewGuid();
                            if (skuInfo.modify_time == null || skuInfo.modify_time.Equals("")) skuInfo.modify_time = GetCurrentDateTime();
                            if (skuInfo.modify_user_id == null || skuInfo.modify_user_id.Equals("")) skuInfo.modify_user_id = itemInfo.Create_User_Id;
                            if (skuInfo.create_time == null || skuInfo.create_time.Equals("")) skuInfo.create_time = GetCurrentDateTime();
                            if (skuInfo.create_user_id == null || skuInfo.create_user_id.Equals("")) skuInfo.create_user_id = itemInfo.Create_User_Id;
                            UpdateSku(skuInfo, pTran);//修改
                            InsertSku(skuInfo, pTran);//删除




                            //处理sku相关价格信息(jifeng.cao 20140224)
                            foreach (SkuPriceInfo skuPriceInfo in skuInfo.sku_price_list)
                            {
                                if (skuPriceInfo.sku_price != -1)
                                {
                                    decimal oldPrice = 0;
                                    if (!string.IsNullOrEmpty(skuPriceInfo.sku_price_id))
                                    {
                                        var skuPriceInfoTemp = skupriceService.GetSkuPriceByID(skuPriceInfo.sku_price_id, pTran);
                                        if (skuPriceInfoTemp != null && skuPriceInfoTemp.Tables.Count != 0 && skuPriceInfoTemp.Tables[0].Rows.Count!=0)
                                        {
                                            oldPrice=Convert.ToDecimal( skuPriceInfoTemp.Tables[0].Rows[0]["sku_price"]);
                                        }
                                    }

                                    if (skuPriceInfo.sku_id == null) skuPriceInfo.sku_id = skuInfo.sku_id;
                                    if (skuPriceInfo.sku_price_id == null || skuPriceInfo.sku_price_id.Equals("")) skuPriceInfo.sku_price_id = NewGuid();//如果是新建
                                    if (skuPriceInfo.modify_time == null || skuPriceInfo.modify_time.Equals("")) skuPriceInfo.modify_time = GetCurrentDateTime();
                                    if (skuPriceInfo.modify_user_id == null || skuPriceInfo.modify_user_id.Equals("")) skuPriceInfo.modify_user_id = skuInfo.create_user_id;
                                    if (skuPriceInfo.create_time == null || skuPriceInfo.create_time.Equals("")) skuPriceInfo.create_time = GetCurrentDateTime();
                                    if (skuPriceInfo.create_user_id == null || skuPriceInfo.create_user_id.Equals("")) skuPriceInfo.create_user_id = skuInfo.create_user_id;
                                    new SkuPriceService(loggingSessionInfo).UpdateSkuPrice(skuPriceInfo, pTran);//修改
                                    new SkuPriceService(loggingSessionInfo).InsertSkuPrice(skuPriceInfo, pTran);//删除
                                    // skuPriceInfo.item_price_type_name==
                                    //skuPriceInfo
                                    //判断如果是库存的,并且当前sku的price_type_type_id=获取到的库存类型的id，并且商品里有库存
                                    if (skustockTypeId != "" && skuPriceInfo.item_price_type_id == skustockTypeId && ItemStockPropId != "")
                                    {
                                        //找出该商品对应的库存的属性
                                        stockCountTemp += skuPriceInfo.sku_price - oldPrice;
                                    }


                                    //如果是销量的
                                    if (skusalesCountTypeId != "" && skuPriceInfo.item_price_type_id == skusalesCountTypeId && ItemsalesCountPropId != "")
                                    {
                                        //找出该商品对应的库存的属性
                                        salesCountTemp += skuPriceInfo.sku_price - oldPrice;
                                    }

                                }
                            }

                        }
                        stockItemProp.PropertyCodeValue = (Convert.ToDecimal(stockItemProp.PropertyCodeValue) + stockCountTemp)+"";
                        SalesCountProp.PropertyCodeValue = (Convert.ToDecimal(SalesCountProp.PropertyCodeValue) + salesCountTemp) + "";
                        //更新数据
                        itemPropService.updateValue(stockItemProp.Item_Property_Id, stockItemProp.PropertyCodeValue, pTran);
                        itemPropService.updateValue(SalesCountProp.Item_Property_Id, SalesCountProp.PropertyCodeValue, pTran);
                    }
                    //图片

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

                //    tran.Commit();
                    strError = "";
                  return true;
                }
                catch (Exception ex)
                {
                    strError = "";
                    return false;
                    //tran.Rollback();
                    //throw (ex);
                }
           // }
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
        /// 删除单个sku
        /// </summary>
        /// add by donal 2014-10-11 17:37:37
        public void DeleteSku(SkuInfo skuInfo, IDbTransaction pTran)
        {
            string sql = "update t_sku "
                      + " set status= '-1' "
                      + " ,modify_user_id = '" + skuInfo.create_user_id + "' "
                      + " ,modify_time = '" + skuInfo.create_time + "'"
                      + " ,if_flag = '0' "
                      + " where  t_sku.sku_id = '" + skuInfo.sku_id + "';";
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
            //StringBuilder sql = new StringBuilder();
            //sql.AppendLine(" SELECT skuId = a.SkuID ");
            //sql.AppendLine(" ,skuProp1 = a.Prop1Name ");
            //sql.AppendLine(" ,skuProp2 = a.Prop2Name ");
            //sql.AppendLine(" ,price = a.Price ");
            //sql.AppendLine(" ,salesPrice = a.SalesPrice ");
            //sql.AppendLine(" ,discountRate = convert(decimal(18,1),a.DiscountRate)  ");
            //sql.AppendLine(" ,integral = a.IntegralExchange ");
            //sql.AppendLine(" ,qty=a.qty");
            //sql.AppendLine(" ,overQty=a.RemainingQty");
            //sql.AppendLine(@" FROM vwPEventItemDetail a");
            //sql.AppendLine(string.Format(" WHERE a.itemid = '{0}' and a.eventid='{1}'", itemId, eventId));

            /*
            string sql = @"select b.sku_id as skuId,b.prop_1_name as skuProp1,b.prop_2_name as skuProp2,
	                        a.Price as price,a.SalesPrice as salesPrice,convert(decimal(18,1),b.DiscountRate) as discountRate,
	                        b.Integral as integral
                            From vwPEventItemDetail a
                            inner join vw_sku_detail b
                            on(a.itemid = b.item_id)
                            where a.itemid='{0}' and a.eventid='{1}' and b.status='1'";
             */

            // modify by donal 2014-11-13 11:22:46
            //以前查的结果不对，现在重新整理sql
            string sql = @"
                SELECT  b.SkuId AS skuId,
                c.prop_1_name AS skuProp1 ,
                c.prop_2_name AS skuProp2,
                b.Price AS price,
                b.SalesPrice AS salesPrice,
                CONVERT (DECIMAL(18, 1), c.DiscountRate) AS discountRate,
                c.Integral AS integral
                FROM    PanicbuyingEventItemMapping a
                        LEFT JOIN dbo.PanicbuyingEventSkuMapping b ON a.EventItemMappingId = b.EventItemMappingId
                        INNER JOIN dbo.vw_sku_detail c ON b.SkuId = c.sku_id
                WHERE   a.IsDelete = 0
                        AND b.IsDelete = 0
                        AND a.ItemId = '{0}'
                        AND a.EventId = '{1}'
                        AND c.status ='1'";
            return this.SQLHelper.ExecuteDataset(string.Format(sql, itemId, eventId));
        }

        #endregion
    }
}
