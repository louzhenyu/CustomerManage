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
    public class ItemService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ItemService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        public int SearchCount(Hashtable _ht)
        {
            string sql = SearchPublicSql(_ht);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet SearchList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = SearchPublicSql(_ht);
            #region
            sql = sql + "select "
                      + " a.item_id "
                      + " ,a.item_category_id "
                      + ",(select top 1 ImageURL from objectimages b where b.ObjectId=a.item_id  and isdelete=0 and Description != '自动生成的产品二维码' order by displayindex ) as  Image_Url"
                      + " ,a.item_code "
                      + " ,a.item_name "
                      + " ,a.item_name_en "
                      + " ,a.item_name_short "
                      + " ,Case  when ifservice=0 Then '实物商品' when ifservice=1 Then '虚拟商品' end ItemType  "
                      + " ,a.pyzjm "
                      + " ,a.item_remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,a.create_user_id "
                      + " ,a.create_time "
                      + " ,a.modify_user_id "
                      + " ,a.modify_time "
                      + " ,(select item_category_name from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_name "
                      + " ,(select item_category_code from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_code "
                      + " ,(select user_name from t_user x where x.user_id = a.create_user_id) create_user_name "
                      + " ,(select user_name from t_user x where x.user_id = a.modify_user_id) modify_user_name "
                      + " ,case when a.status = '1' then '上架' else '下架' end status_desc "
                      + " ,b.row_no "
                      + " ,@iCount icount "
                      + " ,a.ifgifts "
                      + " ,a.ifoften "
                      + " ,a.ifservice "
                      + " ,a.isGB "
                      + " ,a.data_from "
                      + " ,a.display_index "
                      + ",(  select case when COUNT(1)>0 then 'true' else 'false' end from  T_Item_Category where item_category_id=a.item_category_id and status='-1') isItemCategory"
                      + @",(   select case when prop_value=null or  prop_value='' then 'false' when GETDATE()>prop_value then 'true' else 'false' end  from  t_prop as tp
                           left join T_Item_Property  as tip on tip.prop_id=tp.prop_id and prop_code ='EndTime'
                           where  tp.prop_code ='EndTime' and item_id=a.item_id and tip.status=1) isPause"
                      + @" ,(  select  prop_value  from  t_prop as tp
                           left join T_Item_Property  as tip on tip.prop_id=tp.prop_id 
                           where  tp.prop_code ='Qty' and item_id=a.item_id and tip.status=1 ) stock
                           ,	(  select  prop_value  from  t_prop as tp
                           left join T_Item_Property  as tip on tip.prop_id=tp.prop_id 
                           where  tp.prop_code ='SalesCount' and item_id=a.item_id ) SalesCount
                           ,
                           (select min(sku_price) from T_Sku_Price x inner join t_sku y on x.sku_id=y.sku_id 
                           where y.item_id=a.item_id   and  x.status=1
							and item_price_type_id=(select item_price_type_id from T_Item_Price_Type where item_Price_type_code='零售价')
                           ) as minPrice
                           ,dbo.GetSalesPromotion(a.item_id) as SalesPromotion"
                      + " From T_Item a  with(nolock)"
                      + "inner join @TmpTable b "
                      + "on(a.Item_Id = b.Item_Id) "
                      + "where 1=1 and a.item_category_id<>'-1' "
                      + "and b.row_no  > '" + _ht["StartRow"].ToString() + "' and  b.row_no <= '" + _ht["EndRow"] + "' order by a.modify_time desc";//item_code
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 公共查询
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string SearchPublicSql(Hashtable _ht)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "Declare @TmpTable Table "
                     + " (item_id nvarchar(100) "
                     + " ,row_no int "
                     + " ); "
                     + " Declare @iCount int; "
                     + " insert into @TmpTable(item_id,row_no) "
                     + " select x.item_id ,x.rownum_ From ( select rownum_=row_number() over(order by a.modify_time desc), a.item_id "   //item_code
                     + " from t_item a";

            if (_ht["item_can_redeem"].ToString() != "")
            {
                sql += " left join T_Item_Property tip on a.item_id = tip.item_id";
                sql += " left join T_Prop tp on tip.prop_id = tp.prop_id";
                sql += " left join T_Prop tpv on tip.prop_value = tpv.prop_id";
                sql += " where tp.prop_code='canredeem' and (tpv.prop_code = '" + _ht["item_can_redeem"].ToString() + "' or (" + _ht["item_can_redeem"].ToString() + "= 0 and (tpv.prop_code is null or tpv.prop_code = '')))";
            }
            else
                sql += " where 1=1 and a.item_category_id<>'-1'";
            if (_ht["SalesPromotion_id"]!=null && _ht["SalesPromotion_id"].ToString() != "")
            {
                sql += " and exists (select * from ItemCategoryMapping cate where cate.ItemId=a.item_id and cate.isdelete=0 and cate.ItemCategoryId='" + _ht["SalesPromotion_id"].ToString() + "')";
            }

            sql = pService.GetLinkSql(sql, "a.item_code", _ht["item_code"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.item_name", _ht["item_name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.status", _ht["status"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.customerId", this.CurrentUserInfo.CurrentUser.customer_id.ToString().Trim(), "=");
            if (_ht["item_category_id"] != null && !_ht["item_category_id"].ToString().Equals(""))
            {
                sql = sql + " and ( a.item_category_id = '" + _ht["item_category_id"].ToString() +
                    "' or a.item_category_id in (select item_category_id From vw_item_category_level where path_item_category_id like '%" +
                    _ht["item_category_id"].ToString() + ">%' ) or a.item_id='" + _ht["item_category_id"].ToString() + "')";
            }
            sql = sql + " ) x";

            sql = sql + " select @iCount = COUNT(*) From @TmpTable; ";
            #endregion
            return sql;
        }
        /// <summary>
        /// 获取所有商品信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemAllList()
        {
            DataSet ds = new DataSet();
            string sql = "select "
                      + " a.item_id "
                      + " ,a.item_category_id "
                      + " ,a.item_code "
                      + " ,a.item_name "
                      + " ,a.item_name_en "
                      + " ,a.item_name_short "
                      + " ,a.pyzjm "
                      + " ,a.item_remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,a.create_user_id "
                      + " ,a.create_time "
                      + " ,a.modify_user_id "
                      + " ,a.modify_time "
                      + " ,(select item_category_name from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_name "
                      + " ,(select item_category_code from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_code "
                      + " ,(select user_name from t_user x where x.user_id = a.create_user_id) create_user_name "
                      + " ,(select user_name from t_user x where x.user_id = a.modify_user_id) modify_user_name "
                      + " ,case when a.status = '1' then '正常' else '删除' end status_desc "
                      + " ,b.row_no "
                      + " ,a.ifgifts "
                      + " ,a.ifoften "
                      + " ,a.ifservice "
                      + " ,a.isGB "
                      + " ,a.data_from "
                      + " ,a.display_index "
                      + " From T_Item a where a.status='1' and a.customerId='" + this.CurrentUserInfo.CurrentUser.customer_id.ToString().Trim() + "' order by a.item_code ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetItemById(string item_id)
        {
            DataSet ds = new DataSet();
            string sql = "select "
                      + " a.item_id "
                      + " ,a.item_category_id "
                      + " ,a.item_code "
                      + " ,a.item_name "
                      + " ,a.item_name_en "
                      + " ,a.item_name_short "
                      + " ,a.pyzjm "
                      + " ,a.item_remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,a.create_user_id "
                      + " ,a.create_time "
                      + " ,a.modify_user_id "
                      + " ,a.modify_time "
                      + " ,(select item_category_name from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_name "
                      + " ,(select item_category_code from T_Item_Category x where x.item_category_id = a.item_category_id) item_category_code "
                      + " ,(select user_name from t_user x where x.user_id = a.create_user_id) create_user_name "
                      + " ,(select user_name from t_user x where x.user_id = a.modify_user_id) modify_user_name "
                      + " ,case when a.status = '1' then '正常' else '删除' end status_desc "
                      + " ,a.ifgifts "
                      + " ,a.ifoften "
                      + " ,a.ifservice "
                      + " ,a.isGB "
                      + " ,a.data_from "
                      + " ,a.display_index "
                      + " ,a.imageUrl Image_Url "
                      + " From T_Item a where a.item_id = '" + item_id + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 修改商品状态
        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemStatus(ItemInfo itemInfo)
        {
            string sql = "update t_item "
                      + " set [status] = '" + itemInfo.Status + "'"
                      + " ,Modify_Time = '" + itemInfo.Modify_Time + "' "
                      + " ,Modify_User_Id = '" + itemInfo.Modify_User_Id + "' "
                      + " ,if_flag = '0' "
                      + " where item_id = '" + itemInfo.Item_Id + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 保存

        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetItemInfo(ItemInfo itemInfo, out string strError,bool isOld,string CustomerID)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    DataSet ds = GetItemById(itemInfo.Item_Id);
                    //1.判断号码唯一
                    if (IsExistItemCode(itemInfo.Item_Code, itemInfo.Item_Id, CustomerID) == 1)
                    {
                        strError = "商品类别号码已经存在。";
                        throw (new System.Exception(strError));
                    }
                    else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (!SetItemUpdate(itemInfo, tran))
                        {
                            strError = "修改商品信息失败";
                            throw (new System.Exception(strError));
                        }
                    }
                    //2.1更新商品信息
                    else
                    {
                        //2.2插入商品信息
                        if (!SetItemInsert(itemInfo, tran))
                        {
                            strError = "插入商品信息失败";
                            throw (new System.Exception(strError));
                        }
                    }
                    //3 处理商品属性
                    if (!new ItemPropService(loggingSessionInfo).SetItemPropInfo(itemInfo, tran, out strError))//在事务里
                    {
                        throw (new System.Exception(strError));
                    }
                    //4.处理商品价格
                    if (!new ItemPriceService(loggingSessionInfo).SetItemPriceInfo(itemInfo, tran, out strError))
                    {
                        throw (new System.Exception(strError));
                    }
                    //5.处理sku
                    if ((isOld==false) || ( isOld  &&itemInfo.OperationType=="ADD")) //新版本或者旧版本状态下的添加模式
                    //只有再新增的情况下更新sku，因为修改时及时跟新 modify  by donal 2014-10-13 09:29:00
                    {
                        if (!new SkuService(loggingSessionInfo).SetSkuInfo(itemInfo, tran, out strError))
                        {
                            strError = "处理SKU信息失败";
                            throw (new System.Exception(strError));
                        }
                    }
                    
                    //6.处理商品图片
                    if (!new ItemPriceService(loggingSessionInfo).SetItemImageInfo(itemInfo, tran, out strError))
                    {
                        throw (new System.Exception(strError));
                    }
                    //7.处理商品门店
                    if (!new ItemPriceService(loggingSessionInfo).SetItemUnitInfo(itemInfo, tran, out strError))
                    {
                        throw (new System.Exception(strError));
                    }
                    //8.处理商品与分类关系
                    /**
                    if (!new ItemPriceService(loggingSessionInfo).SetItemCategoryInfo(itemInfo, tran, out strError))
                    {
                        throw (new System.Exception(strError));
                    }
                     * **/
                    tran.Commit();
                    strError = "成功";
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
        /// 单个修改sku
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="strError"></param>
        /// <returns>skuid</returns>
        /// add by donal 2014-10-13 09:39:42
        public string SetSkuInfo(ItemInfo itemInfo, out string strError)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    SkuInfo skuInfo = itemInfo.SkuList.FirstOrDefault();

                    SkuService skuService = new SkuService(loggingSessionInfo);
                    SkuPriceService skuPriceService = new SkuPriceService(loggingSessionInfo);

                    //删除sku
                    skuService.DeleteSku(skuInfo, tran);
                    skuPriceService.DeleteSkuPriceInfo(skuInfo,tran);

                    if (itemInfo.OperationType =="EDIT")
                    {
                        //新增sku
                        skuInfo.sku_id = Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
                        skuService.InsertSku(skuInfo, tran);
                        foreach (SkuPriceInfo skuPriceInfo in skuInfo.sku_price_list)
                        {
                            if (skuPriceInfo.sku_price != -1)
                            {
                                skuPriceInfo.sku_id = skuInfo.sku_id;
                                if (skuPriceInfo.modify_time == null || skuPriceInfo.modify_time.Equals("")) skuPriceInfo.modify_time = GetCurrentDateTime();
                                if (skuPriceInfo.modify_user_id == null || skuPriceInfo.modify_user_id.Equals("")) skuPriceInfo.modify_user_id = skuInfo.create_user_id;
                                if (skuPriceInfo.create_time == null || skuPriceInfo.create_time.Equals("")) skuPriceInfo.create_time = GetCurrentDateTime();
                                if (skuPriceInfo.create_user_id == null || skuPriceInfo.create_user_id.Equals("")) skuPriceInfo.create_user_id = skuInfo.create_user_id;
                                new SkuPriceService(loggingSessionInfo).InsertSkuPrice(skuPriceInfo, tran);//新增
                            }
                        }
                    }                   
                    
                    tran.Commit();
                    strError = "成功";
                    return skuInfo.sku_id;
                }
                catch (Exception ex)
                {
                    strError = "失败";
                    tran.Rollback();
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="item_code"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public int IsExistItemCode(string item_code, string item_id,string CustomerID)
        {
            string sql = "select count(*) From t_item where 1=1 and CustomerId= '" + CustomerID + "' and item_code = '" + item_code + "'";

            if (!item_id.Equals(""))
            {
                sql = sql + " and item_id <> '" + item_id + "' ";
            }

            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count;
        }


        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="itemInfo"></param>
        private bool SetItemUpdate(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region
            string sql = "update t_item set item_category_id = '" + itemInfo.Item_Category_Id + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "Item_Code", itemInfo.Item_Code);
            sql = pService.GetIsNotNullUpdateSql(sql, "Item_Name", itemInfo.Item_Name);
            sql = pService.GetIsNotNullUpdateSql(sql, "Item_Name_En", itemInfo.Item_Name_En);
            sql = pService.GetIsNotNullUpdateSql(sql, "Item_Name_Short", itemInfo.Item_Name_Short);
            sql = pService.GetIsNotNullUpdateSql(sql, "Pyzjm", itemInfo.Pyzjm);
            sql = pService.GetIsNotNullUpdateSql(sql, "Item_Remark", itemInfo.Item_Remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "Modify_User_Id", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "Modify_Time", itemInfo.Modify_Time);
            sql = pService.GetIsNotNullUpdateSql(sql, "ifgifts", itemInfo.ifgifts.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "ifoften", itemInfo.ifoften.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "ifservice", itemInfo.ifservice.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", "0");
            sql = pService.GetIsNotNullUpdateSql(sql, "isGB", itemInfo.isGB.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "data_from", "3");
            sql = pService.GetIsNotNullUpdateSql(sql, "display_index", itemInfo.display_index.ToString());
            //edit by Willie @2014/04/01 该字段用于首页图片
            //sql = pService.GetIsNotNullUpdateSql(sql, "imageUrl", (itemInfo.Image_Url ?? string.Empty).ToString());
            sql = sql + " where item_id = '" + itemInfo.Item_Id + "' ;";
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
        /// 插入商品信息
        /// </summary>
        /// <param name="itemInfo"></param>
        private bool SetItemInsert(ItemInfo itemInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into T_Item ( "
                      + " item_id "
                      + " ,item_category_id "
                      + " ,item_code "
                      + " ,item_name "
                      + " ,item_name_en "
                      + " ,item_name_short "
                      + " ,pyzjm "
                      + " ,item_remark "
                      + " ,status "
                      + " ,status_desc "
                      + " ,create_user_id "
                      + " ,create_time "
                      + " ,modify_user_id "
                      + " ,modify_time "
                      + " ,ifgifts "
                      + " ,ifoften "
                      + " ,ifservice "
                      + " ,isGB "
                      + " ,data_from "
                      + " ,display_index "
                      + " ,if_flag "
                //edit by Willie @2014/04/01 该字段用于首页图片
                //+ " ,imageUrl "
                      + " ,customerId "
                      + " )"
                      + " select a.* From ( "
                      + " select '" + itemInfo.Item_Id + "' item_id "
                      + " ,'" + itemInfo.Item_Category_Id + "' item_category_id "
                      + " ,'" + itemInfo.Item_Code + "' item_code "
                      + " ,'" + itemInfo.Item_Name + "' item_name "
                      + " ,'" + itemInfo.Item_Name_En + "' item_name_en "
                      + " ,'" + itemInfo.Item_Name_Short + "' item_name_short "
                      + " ,'" + itemInfo.Pyzjm + "' pyzjm "
                      + " ,'" + itemInfo.Item_Remark + "' item_remark "
                      + " ,'" + itemInfo.Status + "' status "
                      + " ,'" + itemInfo.Status_Desc + "' status_desc "
                      + " ,'" + itemInfo.Create_User_Id + "' create_user_id "
                      + " ,'" + itemInfo.Create_Time + "' create_time "
                      + " ,'" + itemInfo.Modify_User_Id + "' modify_user_id "
                      + " ,'" + itemInfo.Modify_Time + "' modify_time "
                      + " ,'" + itemInfo.ifgifts + "' ifgifts "
                      + " ,'" + itemInfo.ifoften + "' ifoften "
                      + " ,'" + itemInfo.ifservice + "' ifservice "
                      + " ,'" + itemInfo.isGB + "' isGB "
                      + " ,'3' data_from "
                      + " ,'" + itemInfo.display_index + "' display_index "
                      + " ,'0' if_flag "
                //edit by Willie @2014/04/01 该字段用于首页图片
                //+ " ,'" + itemInfo.Image_Url + "' imageUrl "
                      + " ,'" + this.CurrentUserInfo.CurrentUser.customer_id.Trim() + "' customerId "
                      + " ) a "
                      + " left join T_Item b "
                      + " on(a.item_id = b.item_id) "
                      + " where b.item_id is null ; ";
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

        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="item_name"></param>
        /// <returns></returns>
        public DataSet GetItemListLikeItemName(string item_name)
        {
            DataSet ds = new DataSet();
            PublicService pService = new PublicService();
            string sql = " select top 10 item_id,item_name From t_item a where status = '1' and 1=1 and (";

            sql = pService.GetLinkSql(sql, "a.item_name", item_name, "%");
            sql = pService.GetLinkSql(sql, "a.item_code", item_name, "%");
            sql = pService.GetLinkSql(sql, "a.customerId", this.CurrentUserInfo.CurrentLoggingManager.Customer_Id, "=");
            sql = sql + " ) order by a.item_code ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取校友福利商品列表
        /// <summary>
        /// 获取校友福利商品列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="isKeep">true 已收藏列表， false 所有列表</param>
        /// <returns></returns>
        public DataSet GetWelfareItemList(string userId, string itemName, string itemTypeId, int page, int pageSize, bool isKeep, string isExchange, string storeId)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetWelfareItemListSql(userId, itemName, itemTypeId, isKeep, isExchange, storeId);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int GetWelfareItemListCount(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId)
        {
            string sql = GetWelfareItemListSql(userId, itemName, itemTypeId, isKeep, isExchange, storeId);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string GetWelfareItemListSql(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId)
        {
            string sql = " SELECT itemId = a.item_id ";
            sql += " ,itemName = a.item_name ";
            sql += " ,imageUrl = a.imageUrl ";
            sql += " ,price = a.Price ";
            sql += " ,salesPrice = a.SalesPrice ";
            sql += " ,discountRate = a.DiscountRate ";
            sql += " ,displayIndex = row_number() over(order by a.ItemDisplayIndex asc, a.BeginTime desc) ";
            sql += " ,pTypeId = a.PTypeId ";
            sql += " ,pTypeCode = a.PTypeCode ";
            sql += " ,CouponURL = a.CouponURL ";
            sql += " ,salesPersonCount = a.SalesPersonCount ";
            sql += " ,itemCategoryName = a.ItemCategoryName ";
            sql += " ,skuId = a.SkuId ";
            sql += " ,isShoppingCart = case when c.vipid is null then 0 else 1 end ";
            sql += ",CONVERT(NVARCHAR(10),a.CreateTime,120) createDate ";
            sql += " ,itemTypeDesc = a.itemTypeDesc ";
            sql += " ,itemSortDesc = a.itemSortDesc ";
            sql += " ,salesQty = a.salesQty ";
            sql += " ,remark = a.item_remark ";
            sql += ",IsExchange=a.IsExchange";
            sql += ",IntegralExchange=a.IntegralExchange";
            sql += " into #tmp ";
            sql += " FROM dbo.vw_item_detail a ";
            if (!string.IsNullOrEmpty(itemTypeId))
            {
                sql += " left join ItemCategoryMapping e on (e.IsDelete='0' and a.item_id=e.ItemId) ";
            }
            if (isKeep)
            {
                sql += " INNER JOIN dbo.ItemKeep b ON b.CreateBy = '" + userId + "' ";
            }
            sql += " left join (select * From ShoppingCart where vipid = '" + userId + "' and qty > 0 and isdelete= '0' ) c on(a.skuId = c.skuId) ";

            //Jermyn20131008 餐饮门店关系
            if (storeId != null && !storeId.Equals(""))
            {
                sql += " INNER JOIN (SELECT * FROM ItemStoreMapping WHERE UnitId='" + storeId + "') d ON(a.item_id = d.ItemId) ";
            }

            sql += " WHERE 1 = 1 and a.customerId = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ";
            sql += " AND (a.BeginTime <= GETDATE() AND a.EndTime >= GETDATE()) ";

            if (!string.IsNullOrEmpty(itemName))
            {
                sql += " AND a.item_name LIKE '%" + itemName + "%' ";
            }
            if (!string.IsNullOrEmpty(itemTypeId))
            {
                sql += " AND (a.item_category_id = '" + itemTypeId + "' or e.ItemCategoryId = '" + itemTypeId + "') ";
            }
            //if (!string.IsNullOrEmpty(isExchange))
            //{
            //    sql += " AND a.isExchange = '" + isExchange + "' ";
            //}

            return sql;
        }
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
            string sql = string.Empty;
            sql += " SELECT itemId = a.item_id ";
            sql += " ,itemName = a.item_name ";
            sql += " ,pTypeId = a.PTypeId ";
            sql += " ,pTypeCode = a.PTypeCode ";
            sql += " ,buyType = a.BuyType ";
            sql += " ,buyTypeDesc = CASE a.BuyType WHEN '1' THEN '马上预订' WHEN '2' THEN '立即抢购' WHEN '3' THEN '卖完啦' END ";
            sql += " ,salesPersonCount = a.SalesPersonCount ";
            sql += " ,downloadPersonCount = a.DownloadPersonCount ";
            sql += " ,overCount = a.OverCount ";
            sql += " ,useInfo = a.UseInfo ";
            sql += " ,tel = a.Tel ";
            sql += " ,endTime = a.EndTime ";
            sql += " ,couponURL = a.CouponURL ";
            sql += " ,offersTips = a.OffersTips ";
            sql += " ,isKeep = (SELECT COUNT(*) FROM dbo.ItemKeep b WHERE b.ItemId = a.item_id AND b.VipId = '" + userId + "') ";
            sql += " ,isShoppingCart = (SELECT isnull(count(*),0) FROM dbo.ShoppingCart b WHERE b.isdelete='0' and b.qty>0 and b.skuId = a.skuId AND b.vipid = '" + userId + "') ";
            sql += " ,prop1Name=a.Prop1Name";
            sql += " ,prop2Name=a.Prop2Name";
            sql += " ,itemCategoryId=a.item_category_id";
            sql += " ,itemCategoryName=a.ItemCategoryName";
            sql += " ,isProp2= case when a.Prop2Name is null or a.Prop2Name = '' then '0' else '1' end ";
            sql += " ,a.ItemTypeDesc";
            sql += " ,a.ItemSortDesc";
            sql += " ,a.salesQty";
            sql += " ,a.item_remark remark ";
            sql += " ,itemIntroduce,itemParaIntroduce ";
            sql += " FROM dbo.vw_item_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取商品图片集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemImageList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT imageId = a.ImageId ";
            sql += " ,imageURL = a.ImageURL ";
            sql += " FROM dbo.ObjectImages a ";
            sql += " WHERE a.ObjectId = '" + itemId + "' AND a.IsDelete = 0 order by a.DisplayIndex asc,a.createtime asc ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取商品sku集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSkuList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT skuId = a.sku_id ";
            sql += " ,skuProp1 = a.prop_1_detail_name ";
            sql += " ,price = a.Price ";
            sql += " ,salesPrice = a.SalesPrice ";
            sql += " ,discountRate = a.DiscountRate ";
            sql += " ,integral = a.Integral ";
            sql += " FROM dbo.vw_sku_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' and a.status = '1' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }



        //public DataSet GetItemSkuList(string itemId, string userId, string customerId)
        //{
        //    string sql = string.Empty;
        //    sql += " SELECT skuId = a.sku_id ";
        //    sql += " ,skuProp1 = a.prop_1_detail_name ";
        //    sql += " ,price = a.Price ";
        //    sql += " ,VipLevelName =  case when  dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId + "') <> 1 then dbo.Fn_GetVipLevelName('" + userId + "','" + customerId + "') else  '' end ";
           
        //    sql += " ,discountRate = a.DiscountRate ";
        //    sql += " ,integral = a.Integral ";
        //    sql += " FROM dbo.vw_sku_detail a ";
        //    sql += " WHERE a.item_id = '" + itemId + "' ";

        //    return this.SQLHelper.ExecuteDataset(sql);
        //}

        public DataSet GetItemSkuList(string itemId, string userId, string customerId, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;
            sql += "SELECT * from ( SELECT skuId = a.sku_id ";
            sql += " ,skuProp1 = a.prop_1_detail_name ";
            //价格
            sql += " ,price = a.Price ";       
            
            sql += " ,VipLevelName =  case when  dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId + "') <> 1 then dbo.Fn_GetVipLevelName('" + userId + "','" + customerId + "') else  '' end ";
            //会员价
            //sql += " ,salesPrice = case when dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId + "') <> 1 then a.Price * dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId + "') else a.salesPrice end ";
            sql += " ,a.salesPrice ";
            sql += " ,discountRate = a.DiscountRate ";
            sql += " ,integral = a.Integral ";
            sql += ",EveryoneSalesPrice = a.everyonesalesprice";
            sql += ",Stock = a.Stock";
            sql += ",SalesCount = a.SalesCount";
            sql += " FROM dbo.vw_sku_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' and a.status = '1') a order by a.salesPrice asc";          
            return this.SQLHelper.ExecuteDataset(sql);
        }


        
     
        /// <summary>
        /// 花间堂房价的数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetHotelItemSkuList(string itemId, string userId, string customerId, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;
            sql += " SELECT skuId = a.sku_id ";
            sql += " ,skuProp1 = a.prop_1_detail_name ";
            //价格
            //sql += " ,price = a.Price ";
            sql += @"  ,convert(decimal(18,0), ISNULL( (select avg(c.SourcePrice)	 from StoreItemDailyStatus c  where c.SkuID=a.sku_id	 and StatusDate>=cast('{0}' as datetime) AND StatusDate<cast('{1}' as datetime)  ),price) )   as price";

            sql += " ,VipLevelName =  case when  dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId + "') <> 1 then dbo.Fn_GetVipLevelName('" + userId + "','" + customerId + "') else  '' end ";
            //会员价
            sql += @" ,salesPrice =  convert(decimal(18,0), isnull( ( case when dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId +
     "') <> 1 then isnull((select avg(c.LowestPrice)	 from StoreItemDailyStatus c where c.SkuID=a.sku_id	and StatusDate>=cast('{0}' as datetime) and StatusDate<cast('{1}' as datetime)   ),a.salesPrice) * dbo.Fn_GetVipDiscountRate('" + userId + "','" + customerId
                + "') else isnull((select avg(c.LowestPrice)	 from StoreItemDailyStatus c where c.SkuID=a.sku_id	and StatusDate>=cast('{0}' as datetime) AND StatusDate<cast('{1}' as datetime)   ),a.salesPrice) end   ),0)  )   ";


            sql += " ,discountRate = a.DiscountRate ";
            sql += " ,integral = a.Integral ";
            sql += " FROM dbo.vw_sku_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' and a.status = '1'";
            sql = string.Format(sql, beginDate, endDate);
            return this.SQLHelper.ExecuteDataset(sql);
        }



        /// <summary>
        /// 购买用户集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSalesUserList(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT DISTINCT a.vip_no AS userId, imageURL = '' ";
            sql += " FROM dbo.T_Inout a ";
            sql += " INNER JOIN dbo.T_Inout_Detail b ON a.order_id = b.order_id ";
            sql += " INNER JOIN dbo.T_Sku c ON b.sku_id = c.sku_id ";
            sql += " WHERE (a.vip_no IS NOT NULL AND a.vip_no <> '') ";
            sql += " AND c.item_id = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreInfo(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT TOP 1 storeId = a.UnitId ";
            sql += " ,storeName = b.unit_name ";
            sql += " ,address = b.unit_address ";
            sql += " ,imageURL = b.imageURL ";
            sql += " ,storeCount = (SELECT COUNT(*) FROM dbo.ItemStoreMapping WHERE ItemId = '" + itemId + "' ) ";
            sql += " FROM dbo.ItemStoreMapping a ";
            sql += " INNER JOIN dbo.t_unit b ON a.UnitId = b.unit_id ";
            sql += " WHERE a.ItemId = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemBrandInfo(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT brandId = a.BrandId ";
            sql += " ,brandLogoURL = b.BrandLogoURL ";
            sql += " ,brandName = b.BrandName ";
            sql += " ,brandEngName = b.BrandEngName ";
            sql += " FROM dbo.vw_item_detail a ";
            sql += " INNER JOIN dbo.BrandDetail b ON a.BrandId = b.BrandId ";
            sql += " WHERE a.item_id = '" + itemId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取同步福利商品

        /// <summary>
        /// 获取同步福利商品
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemTypeList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT itemid = a.item_id ";
            sql += " , itemcode = a.item_code ";
            sql += " , itemname = a.item_name ";
            sql += " , itemengname = a.item_name_en ";
            sql += " , itemshortname = a.item_name_short ";
            sql += " , itemdesc = a.item_remark ";
            sql += " , imageurl = a.imageUrl ";
            sql += " , displayindex = a.display_index ";
            sql += " , [address] = a.[Address] ";
            sql += " , itemunit = a.ItemUnit ";
            sql += " , tel = a.Tel ";
            sql += " , qty = a.Qty ";
            sql += " , begintime = a.BeginTime ";
            sql += " , endtime = a.EndTime ";
            sql += " , useinfo = a.UseInfo ";
            sql += " , [status] = a.[status] ";
            sql += " , buytype = a.BuyType ";
            sql += " , offerstips = a.OffersTips ";
            sql += " , couponurl = a.CouponURL ";
            sql += " , isdelete =  CASE a.[status] WHEN '1' THEN '0' ELSE '1' END ";
            sql += " , brandid = a.BrandId ";
            sql += " , ptypeid = a.PTypeId ";
            sql += " FROM dbo.vw_item_detail a WHERE 1 = 1 ";
            sql += " AND a.IsIAlumniItem = '1' and a.customerId = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND a.modify_time >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 根据门店获取汽车型号 2014-10-16
        
        /// <summary>
        /// 根据门店获取汽车型号
        /// </summary>
        /// <param name="unitid">门店ID</param>
        /// <returns></returns>
        public DataSet GetItemInfoList(string unitid)
        {
            string sql = string.Empty;
            sql += " select i.item_name as CarModel,i.item_id as ModelID from  ItemStoreMapping ism   ";
            sql += " left join t_unit  u on ism.UnitId=u.unit_id ";
            sql += " left join T_Item i on ism.ItemId=i.item_id ";
            sql += "  where unitid='" + unitid + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取门店与商品Mapping集合

        /// <summary>
        /// 获取门店与商品Mapping集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreMapping(string itemId)
        {
            string sql = string.Empty;
            sql += " SELECT mappingid = a.MappingId ";
            sql += " , storeid = a.UnitId ";
            sql += " , isdelete = a.IsDelete ";
            sql += " FROM dbo.ItemStoreMapping a ";
            sql += " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(itemId))
            {
                sql += " AND a.ItemId = '" + itemId + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取商品信息
        /// <summary>
        /// 获取商品信息
        /// </summary>
        public DataSet GetVwItemDetailById(string itemId, string vipId)
        {
            string sql = string.Empty;
            sql += " SELECT a.* ";
            sql += " ,buyTypeDesc = CASE a.BuyType WHEN '1' THEN '马上预订' WHEN '2' THEN '立即抢购' WHEN '3' THEN '卖完啦' END ";
            sql += " ,isKeep = (SELECT COUNT(*) FROM dbo.ItemKeep b WHERE b.ItemId = a.item_id AND b.vipId = '" + vipId + "') ";
            sql += " FROM dbo.vw_item_detail a ";
            sql += " WHERE a.item_id = '" + itemId + "' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetVwItemDetailListCount(VwItemDetailEntity entity)
        {
            string sql = GetVwItemDetailListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetVwItemDetailList(VwItemDetailEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetVwItemDetailListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetVwItemDetailListSql(VwItemDetailEntity entity)
        {
            string sql = string.Empty;
            //sql = "select a.* ";
            //sql += " ,DisplayIndexLast = row_number() over(order by a.create_time desc) ";
            //sql += " into #tmp ";
            //sql += " from [vw_item_detail] a ";
            //sql += " where a.status='1' and a.customerId = '"+this.CurrentUserInfo.CurrentLoggingManager.Customer_Id+"' ";

            sql = "select a.*  "
            + " ,DisplayIndexLast = row_number() over(order by c.create_time desc)  "
            + " into #tmp "
            + " from [vw_item_detail] a  "
            + " INNER JOIN dbo.T_Inout_detail b ON(a.SkuId = b.sku_id) "
            + " INNER JOIN dbo.T_Inout c ON(b.order_id = c.order_id) "
            + " where a.status='1'  "
            + " AND c.vip_no = '" + entity.create_user_id + "' "
            + " and a.customerId = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "'";
            //if (entity.CourseTypeId != null)
            //{
            //    sql += " and a.CourseTypeId = '" + entity.CourseTypeId + "' ";
            //}
            return sql;
        }
        #endregion

        #region Jermyn20131121获取 商品属性集合
        public DataSet GetItemProp1List(string itemId)
        {
            string sql = "SELECT x.prop1DetailId,x.prop1DetailName,MAX(skuId) skuId FROM ( SELECT DISTINCT a.sku_id skuId,a.sku_prop_id1 prop1DetailId,ISNULL(b.prop_name,a.sku_prop_id1) prop1DetailName FROM dbo.T_Sku a "
                        + " LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id1 = b.prop_id "
                        + " AND b.status = '1') "
                        + " WHERE a.status = '1' "
                        + " AND a.item_id = '" + itemId + "' ) x GROUP BY x.prop1DetailId,x.prop1DetailName ";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetItemProp2List(string itemId, string propDetailId)
        {
            string sql = "SELECT DISTINCT a.sku_id skuId,a.sku_prop_id2 prop2DetailId,ISNULL(b.prop_name,a.sku_prop_id2) prop2DetailName,c.Stock,c.SalesCount"
                        + " FROM dbo.T_Sku a "
                        + " LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id2 = b.prop_id "
                        + " AND b.status = '1') "
                        + " LEFT JOIN vw_sku_detail c ON a.sku_Id=c.sku_Id "
                        + " WHERE a.status = '1' "
                        + " AND a.sku_prop_id1 = '" + propDetailId + "' "
                        + " AND a.item_id = '" + itemId + "'";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 查询[vw_item_detail]
        public DataSet GetVWItemDetailByItemCode(string itemCode, string customerId)
        {
            string sql = "select a.*, b.prop_1_detail_name as Specification, b.prop_2_detail_name as SKUDegree from [vw_item_detail] a left join (select * From vw_sku where  status = '1') b on a.item_id = b.item_id where a.item_code='" + itemCode + "' and a.[CustomerId] = '" + customerId + "' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 查询[vwAllItemDetail]
        public DataSet GetvwAllItemDetailByItemCode(string itemCode, string customerId)
        {
            string sql = "select a.*, b.prop_1_detail_name as Specification, b.prop_2_detail_name as SKUDegree from [vwAllItemDetail] a left join (select * From vw_sku where  status = '1')  b on a.item_id = b.item_id where a.item_code='" + itemCode + "' and a.[CustomerId] = '" + customerId + "'";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取首页商品

        /// <summary>
        /// 获取首页商品数量
        /// </summary>
        public int GetItemListCount(string customerId, string categoryId, string itemName)
        {
            string sql = GetItemListSql(customerId, categoryId, itemName);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取首页商品列表
        /// </summary>
        public DataSet GetItemList(string customerId, string categoryId, string itemName, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;

            string sql = GetItemListSql(customerId, categoryId, itemName);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by a.displayindex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 公共查询sql
        /// </summary>
        /// <returns></returns>
        private string GetItemListSql(string customerId, string categoryId, string itemName)
        {
            string sql = string.Empty;
            sql += " SELECT categoryName = b.item_category_name ";
            sql += " , itemId = a.item_id ";
            sql += " , itemName = a.item_name ";
            sql += " , displayIndex = ROW_NUMBER() OVER(ORDER BY a.item_name) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.T_Item a ";
            sql += " INNER JOIN dbo.T_Item_Category b ON a.item_category_id = b.item_category_id and b.status='1'";            
            sql += " WHERE a.CustomerId = '" + customerId + "' ";
            sql += " AND a.status = '1' ";

            if (!string.IsNullOrEmpty(categoryId))
            {
                sql += " AND a.item_category_id";
                sql += " in (select item_category_id from vw_item_category_level where path_item_category_id  ";
                sql += " like '" + categoryId + "%' or item_category_id = '" + categoryId + "') ";
             
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                sql += " AND a.item_name LIKE '%" + itemName + "%' ";
            }

            return sql;
        }
        #endregion

        public SqlDataReader GetGreatestItemCode(string customerCode, string customerId)
        {
            string sql = "select top 1 * from t_item where CustomerId = '" + customerId + "' and item_code like '" + customerCode + "[0-9][0-9][0-9][0-9][0-9][0-9][0-9]' order by item_code desc";
            return this.SQLHelper.ExecuteReader(sql);
        }

        #region 获取活动商品列表
        /// <summary>
        /// 获取活动商品列表
        /// </summary>
        public DataSet GetItemAreaList(string customerId, string eventTypeId, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;


            var sql = new StringBuilder(500);

            #region 原sql
            //sql.Append("select ItemID,ItemName,ImageUrl,EventId,SalesPrice,displayindex =ROW_NUMBER() OVER(ORDER BY a.ItemName)  ");
            //sql.Append("INTO #tmp ");
            //sql.Append("from vwItemPEventDetail a,");
            //sql.Append("MobileHome b where a.customerId = b.customerId and b.isdelete = 0 ");
            //sql.AppendFormat("and a.customerid = '{0}' ", customerId);
            //sql.AppendFormat("and a.eventTypeid = '{0}'", eventTypeId);
            //sql.AppendFormat("select * from #tmp a where 1 = 1 and a.displayindex between {0} and {1} order by a.displayindex", beginSize, endSize);
            #endregion

            sql.Append(" SELECT  L.EventId,L.ItemID,L.ItemName,L.ImageUrl,L.SalesPrice,displayindex =ROW_NUMBER()  OVER(ORDER BY ItemName) ");
            sql.Append(" INTO #tmp ");
            sql.Append(" FROM ( SELECT  a.ItemID , ");
            sql.Append(" a.ItemName , ");
            sql.Append(" a.ImageUrl , ");
            sql.Append(" a.EventId , ");
            sql.Append(" a.SalesPrice ");
            sql.Append(" FROM    vwPEventItemDetail a ");
            sql.Append(" INNER JOIN MobileHome b ON a.CustomerID = b.CustomerId ");
            sql.Append(" WHERE   ( ( BeginTime < GETDATE() AND EndTime > GETDATE()) OR BeginTime > GETDATE()) ");
            sql.Append(" AND( EventStatus IS NULL OR EventStatus = 20) AND b.IsDelete = 0 ");
            sql.AppendFormat(" And a.customerid='{0}'", customerId);
            sql.AppendFormat(" And a.eventTypeid='{0}'", eventTypeId);
            sql.Append(") L ");
            sql.Append(" INNER JOIN ( SELECT ItemId ,EventId = MIN(CONVERT(nvarchar(100) , EventId)) ");
            sql.Append(" FROM   vwPEventItemDetail ");
            sql.Append(" where  ( ( BeginTime < GETDATE() AND EndTime > GETDATE()) OR BeginTime > GETDATE()) ");
            sql.Append(" AND( EventStatus IS NULL OR EventStatus = 20) ");
            sql.AppendFormat(" And customerid='{0}'", customerId);
            sql.AppendFormat(" And eventTypeid='{0}'", eventTypeId);
            sql.Append(" GROUP BY ItemId ) R ON L.EventId = R.EventId AND L.ItemID = R.ItemId ");

            sql.AppendFormat("select * from #tmp a where 1 = 1 and a.displayindex between {0} and {1} order by a.displayindex", beginSize, endSize);

            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 根据活动id EventId获取参加活动的商品
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetItemListByEventId(string customerId, string eventId, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;
            var sql = new StringBuilder(500);
            sql.Append(" SELECT  L.EventId,L.ItemID,L.ItemName,L.ImageUrl,L.SalesPrice,L.Price,displayindex =ROW_NUMBER()  OVER(ORDER BY ItemName) ");
            sql.Append(" INTO #tmp ");
            sql.Append(" FROM ( SELECT  a.ItemId,a.EventId,");
            sql.Append(" b.item_name ItemName , ");
            sql.Append(" CASE WHEN b.imageUrl IS NULL OR b.imageUrl = '' THEN ( SELECT TOP 1 ImageURL	FROM    ObjectImages x WHERE   x.ObjectId = a.ItemId AND x.IsDelete = '0'ORDER BY DisplayIndex)   ELSE b.imageUrl END ImageUrl,");
            sql.Append(" a.SalesPrice,a.Price");
            sql.Append(" FROM    PanicbuyingEventItemMapping a ");
            sql.Append(" INNER JOIN T_Item b ON a.ItemId = b.item_id and b.status=1 AND a.IsDelete=0 ");
            sql.Append(" where a.EventId='" + eventId + "'  ");
            sql.Append(") L ");
            sql.AppendFormat("select * from #tmp a where 1 = 1 and a.displayindex between {0} and {1} order by a.displayindex", beginSize, endSize);
            sql.Append(" select count(1) TotalCount from #tmp ");
            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion
        #region 获取活动商品数量
        /// <summary>
        /// 获取活动商品数量
        /// </summary>
        public int GetItemAreaListCount(string customerId, string eventTypeId)
        {
            StringBuilder sql = new StringBuilder(500);
            //sql.AppendFormat("select count(1) as totalCount from vwItemPEventDetail where eventTypeId = '{0}' and customerid = '{1}'",
            //    eventTypeId, customerId);

            sql.Append(" select COUNT(*) from ( ");
            sql.Append(" SELECT ItemID ");
            sql.Append(" FROM   vwPEventItemDetail ");
            sql.Append(" where  ( ( BeginTime < GETDATE() AND EndTime > GETDATE()) OR BeginTime > GETDATE()) ");
            sql.Append(" AND( EventStatus IS NULL OR EventStatus = 20) ");
            sql.AppendFormat(" And customerid='{0}' ", customerId);
            sql.AppendFormat(" And eventTypeid='{0}' ", eventTypeId);
            sql.Append(" GROUP BY ItemId ) a ");
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));
        }
        #endregion
        #region 根据groupIdFrom、groupIdTo更新MHCategoryArea表的GroupId
        /// <summary>
        ///根据groupIdFrom、groupIdTo更新MHCategoryArea表的GroupId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="groupIdFrom"></param>
        /// <param name="groupIdTo"></param>
        public void UpdateMHCategoryAreaByGroupId(string customerId, int groupIdFrom, int groupIdTo)
        {
            int maxGroupId = GetMHCategoryAreaMaxGroupId() + 100;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" update a set groupid = {0} from MHCategoryArea a,MobileHome b where a.homeid = b.homeid and groupid = {1} and  b.customerid = '{2}' and a.isdelete = 0 and b.isdelete = 0;",
                   maxGroupId, groupIdFrom, customerId);
            sql.AppendFormat(
                " update MHCategoryAreaGroup set groupvalue = {0} where groupValue = {1} and customerId = '{2}' and  isdelete = 0;",
                maxGroupId, groupIdFrom, customerId);

            if (groupIdFrom < groupIdTo)
            {
                sql.AppendFormat(
                    " update a set groupid = groupid - 1 from MHCategoryArea a,MobileHome b where a.homeid = b.homeid and groupid > {0} and groupid<={1} and b.customerid = '{2}' and a.isdelete = 0 and b.isdelete = 0;",
                    groupIdFrom, groupIdTo, customerId);
                sql.AppendFormat(
                    " update a set groupid = {0} from MHCategoryArea a, MobileHome b where a.homeid = b.homeid and b.isdelete = 0 and  groupid = {1} and b.customerid = '{2}';"
                    , groupIdTo, maxGroupId, customerId);
                sql.AppendFormat(
                    " update MHCategoryAreaGroup set groupValue = groupValue -1 where groupValue>{0} and groupValue <={1} and customerId = '{2}' and isdelete = 0;",
                    groupIdFrom, groupIdTo, customerId);
                sql.AppendFormat(
                    " update MHCategoryAreaGroup set groupValue = {0} where customerId = '{1}'and groupValue = {2} and isdelete = 0 ;",
                    groupIdTo, customerId, maxGroupId);
            }
            else
            {
                sql.AppendFormat(" update a set groupid = groupid + 1 from MHCategoryArea a,MobileHome b where a.homeid = b.homeid and  groupid >= {0} and groupid<{1} and b.customerId = '{2}' and a.isdelete = 0 and b.isdelete = 0;"
                    , groupIdTo, groupIdFrom, customerId);
                sql.AppendFormat(" update a set groupid = {0} from MHCategoryArea a, MobileHome b where a.homeid = b.homeid and b.isdelete = 0 and  groupid = {1} and b.customerid = '{2}';"
                    , groupIdTo, maxGroupId, customerId);

                sql.AppendFormat(
                    " update MHCategoryAreaGroup set groupValue = groupValue +1 where customerId = '{0}' and groupValue<{1} and groupValue>={2} and isdelete = 0;",
                    customerId, groupIdFrom, groupIdTo);
                sql.AppendFormat(
                    " update MHCategoryAreaGroup set groupValue = {0} where isdelete = 0 and groupValue = {1} and customerId = '{2}';",
                    groupIdTo, maxGroupId, customerId);
            }
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        public int GetMHCategoryAreaMaxGroupId()
        {
            string sql = "select max(groupId) as groupid from mhcategoryArea";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion
        #region 获取咨询类型列表
        /// <summary>
        /// 获取咨询类型列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetLNewsTypeList(string customerId)
        {
            StringBuilder sql = new StringBuilder(500);
            sql.AppendFormat("select newstypeId,newstypeName from LnewsType where isdelete = 0 and customerid = '{0}'", customerId);

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion
        #region 获取资讯类别列表
        /// <summary>
        /// 获取资讯类别列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newsTypeId"></param>
        /// <param name="publishTimeFrom"></param>
        /// <param name="publishTimeTo"></param>
        /// <param name="newsTitle"></param>
        /// <returns></returns>
        public DataSet GetLNewsList(string customerId, string newsTypeId, string publishTimeFrom, string publishTimeTo, string newsTitle)
        {
            StringBuilder sql = new StringBuilder(500);
            publishTimeTo = publishTimeTo + " 23:59:59";
            sql.Append("select a.newsTitle,a.publishTime,b.user_name from LNews a,T_user b where a.createBy = b.user_id and a.isdelete = 0 and b.isdelete =0 ");
            sql.AppendFormat("and a.customerid = '{0}' and a.newsType = '{1}' and a.newsTitle like '%{2}%' ", customerId, newsTypeId, newsTitle);
            sql.AppendFormat("and publishTime between '{0}' and '{1}' ", publishTimeFrom, publishTimeTo);

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion
        #region 获取活动列表
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="eventTypeId"></param>
        /// <param name="eventName"></param>
        /// <param name="eventBeginTime"></param>
        /// <param name="eventEndTime"></param>
        /// <returns></returns>
        public DataSet GetLEventsList(string customerId, string eventTypeId, string title, string eventBeginTime, string eventEndTime)
        {
            StringBuilder sql = new StringBuilder(500);
            eventEndTime = eventEndTime + " 23:59:59";

            sql.Append("select a.eventId,a.title,a.EventTypeId,a.BeginTime,a.EndTime,a.CityID,cityName = b.city1_name + '-' + b.city2_name + '-' + b.city3_name ,a.displayIndex from LEvents a ,t_city b where a.cityId = b.city_id and  isdelete =0 ");
            sql.AppendFormat("and a.customerid = '{0}' and a.eventTypeId = {1} and a.title like '%{2}%' ", customerId, eventTypeId, title);
            sql.AppendFormat("and (a.beginTime >= '{0}' or a.endTime <='{1}') order by a.displayIndex", eventBeginTime, eventEndTime);

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        public DataSet GetLEventsList(string customerId, string eventTypeId)
        {
            StringBuilder sql = new StringBuilder(500);

            sql.Append("select a.eventId,a.title,a.EventTypeId,a.BeginTime,a.EndTime,a.CityID,cityName = b.city1_name + '-' + b.city2_name + '-' + b.city3_name ,a.displayIndex from LEvents a ,t_city b where a.cityId = b.city_id and  isdelete =0 ");
            sql.AppendFormat("and a.customerid = '{0}' and a.eventTypeId = {1} ", customerId, eventTypeId);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion

        #region 获取商品评论信息 add by changjian.tian 2014/5/30
        public DataSet GetItemCommentList(string pItemId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select  V.HeadImgUrl,V.VipName,I.CommentContent, * from ItemComment I left join Vip V on I.VipId=V.VIPID where 1=1 and ItemId='"+pItemId+"'");
            var ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        #endregion

        #region 商品详情接口，获取最近一家门店信息 add by changjian.tian 2014/5/30
        public DataSet GetNearbyOneStore(string pLng, string pDim)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT top 1 x.*,DisplayIndex = row_number() over(order BY x.Distance asc)  into #tmp FROM (SELECT a.unit_id StoreId,a.unit_name StoreName,a.imageURL ,a.unit_address [Address],a.unit_tel Tel ,isnull(a.longitude,0) Longitude ,isnull(a.dimension,0) Latitude ,case when '" + pDim + "' = '0' and '" + pLng + "' = '0' then '0' else ABS(dbo.DISTANCE_TWO_POINTS(" + Convert.ToDouble(pDim) + "," + Convert.ToDouble(pLng) + ",a.dimension,a.longitude)) end Distance   FROM (select * From dbo.t_unit where type_id ='EB58F1B053694283B2B7610C9AAD2742'and status='1' and LTRIM(dimension)<>'' and LTRIM(longitude)<>'' ) a )x;");
            sbSQL.Append("select * From #tmp a where 1=1  order by DisplayIndex; ");

            //如果按经纬°没有找到数据。默认取当前客户下默认top1的门店
            sbSQL.Append(string.Format("select unit_id as StoreId ,unit_name as StoreName,imageURL,unit_address as [Address],unit_tel as Tel From dbo.t_unit  where customer_id='{0}' and type_id='EB58F1B053694283B2B7610C9AAD2742' ", CurrentUserInfo.ClientID));
            var ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        #endregion

        #region 获取店铺首页
        public DataSet GetItemHomePageList(string pCustomerId)
        {
            StringBuilder sbSQL = new StringBuilder("select COUNT(*) as ConuntItem from   dbo.vw_item_detail");
            sbSQL.Append(" where 1=1 and CustomerId='" + pCustomerId + "' AND ISNULL(Price,0)>0 AND ISNULL(SalesPrice,0)>0 ;"); //查询全部商品
            //sbSQL.Append("select COUNT(*) as NewCount  from dbo.vw_item_detail where CustomerId='"+pCustomerId+"'and  DATEADD(dd,-30,getdate())<create_time; "); //查询新上
            sbSQL.Append("SELECT COUNT(*) as NewCount FROM dbo.t_unit WHERE customer_id='" + pCustomerId + "' AND Status=1; "); //获取门店个数 update by Henry 2014-10-31
            sbSQL.Append("select top 4 item_name as ItemDescription,isnull(Price,0) CostPrice ,isnull(SalesPrice,0) SalesPrice,isnull(imageUrl,'') ItemUrl,('aldlinks://product/detail/customerid='+CustomerId+'/itemid='+item_id) as ItemDetailUrl ");
            sbSQL.Append(" from dbo.vw_item_detail where CustomerId='" + pCustomerId + "' AND ISNULL(Price,0)>0 AND ISNULL(SalesPrice,0)>0 ;"); //过滤价格为0数据 update by Henry 2014-10-29
            sbSQL.Append("select * from CustomerBasicSetting where CustomerID='" + pCustomerId + "' and SettingCode in('AppLogo','AppTopBackground') and IsDelete=0 ");
            var ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        #endregion

        public DataSet GetItemCommentByItemId(string customerId, string ItemId,  int pageIndex, int pageSize)
        {
            var sqlWhere = new StringBuilder();          
            //if (ItemId != null && !string.IsNullOrEmpty(ItemId))
            //{
            //    sqlWhere.AppendFormat(" and a.ItemId={0}", ItemId);
            //}
            var sqlDisplay = new StringBuilder();
            sqlDisplay.Append(" order by a.CreateTime desc");//添加排序        

            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.AppendFormat("select row_number() over ({0}) as _row,", sqlDisplay);
            sql.Append(" ItemCommentId,ItemId,a.VipId,b.HeadImgUrl,b.VipName,b.VipLevel,a.CommentContent,a.CreateTime ");          
            sql.Append(" from ItemComment   a inner join	 Vip b on a.VipId=b.VIPID");
            sql.AppendFormat(" where a.IsDelete=0 and a.customerId = '{0}' and a.ItemId='{1}'", customerId,ItemId);
          //  sql.Append(sqlWhere);
            sql.Append(") t");

            sql.AppendFormat(" where _row>={0} and _row<={1}"
             , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(sql.ToString());

        }
        public DataSet GetInoutOrderByItemId(string customerId, string ItemId, int pageIndex, int pageSize)
        {
            var sqlWhere = new StringBuilder();            
            var sqlDisplay = new StringBuilder();
            sqlDisplay.Append(" order by a.PayTime desc");//添加排序        

            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.AppendFormat("select row_number()over({0}) as _row,", sqlDisplay);
            sql.Append(" OrderId,VipId,HeadImgUrl,VipName,VipLevel,Price,Qty,ItemDesc,PayTime ");
            sql.Append(" from VwInoutOrderItems a");
            sql.AppendFormat(" where isnull(IsPay,'0')='1' and a.customerId = '{0}' and a.ItemId='{1}'", customerId, ItemId);//IsPay=1 ,代表支付成功
            //  sql.Append(sqlWhere);
            sql.Append(") t");

            sql.AppendFormat(" where _row>={0} and _row<={1}"
             , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(sql.ToString());

        }



        //新版本保存商品信息
        /// <summary>
        /// 根据分类类型(分类，分组)获取商品信息
        /// </summary>
        /// <param name="strBat_id">2:分组1:分类</param>
        /// <returns></returns>
        public DataSet GetItemTreeByCategoryType(string strBat_id)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT  B.ItemCategoryId ParentId,a.item_id id,a.item_name text,'close' state");
            strSql.Append(" FROM    dbo.T_Item A ");
            strSql.Append(" INNER JOIN dbo.ItemCategoryMapping B ON A.item_id = B.ItemId ");
            strSql.AppendFormat(" INNER JOIN dbo.T_Item_Category c ON b.ItemCategoryId=c.item_category_id AND c.status='1' AND c.bat_id='{0}' ", strBat_id);
            strSql.AppendFormat(" WHERE B.IsDelete=0 AND a.status='1' AND a.CustomerId='{0}'",loggingSessionInfo.ClientID);
            strSql.Append(" ORDER BY C.create_time DESC");
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        /// <summary>
        /// 根据分组Id查询是否有关联商品
        /// </summary>
        /// <param name="strCategoryId"></param>
        /// <param name="strBatId">1:分类，2：分组</param>
        /// <returns></returns>
        public int GetItemCountByCategory(string strCategoryId,string strBatId)
        {

            string strSql = "";
            if (strBatId == "2")//分组
            {
                strSql = string.Format("SELECT COUNT(1) FROM ItemCategoryMapping WHERE IsDelete=0 AND ItemCategoryId='{0}'", strCategoryId);
            }
            else//分类
            {
                strSql = string.Format("SELECT COUNT(1) FROM T_Item WHERE status=1 AND item_category_id='{0}'", strCategoryId);

            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));
        }
    }
}
