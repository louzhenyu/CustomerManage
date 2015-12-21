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
    public class ItemCategoryService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ItemCategoryService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取查询数量
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public int SearchCount(Hashtable _ht)
        {
            string sql = SearchPublicSql(_ht);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取查询结果集
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public DataSet SearchInfoList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = SearchPublicSql(_ht);
            #region
            sql = sql + "select a.Item_Category_Id "
                      + " ,a.Item_Category_Code "
                      + " ,a.Item_Category_Name "
                      + " ,a.Pyzjm "
                      + " ,a.Status "
                      + " ,a.Parent_Id "
                      + " ,a.Create_User_Id "
                      + " ,a.Create_Time "
                      + " ,a.Modify_User_Id "
                      + " ,a.modify_time "
                      + " ,a.DisplayIndex "
                      + " ,case when a.status = '1' then '正常' else '停用' end Status_Desc "
                      + " ,(select USER_NAME From T_User where T_User.user_id = a.create_user_id) Create_User_Name "
                      + " ,(select USER_NAME From T_User where T_User.user_id = a.modify_user_id) Modify_User_Name "
                      + " ,(select item_category_name From T_Item_Category where T_Item_Category.item_category_id = a.parent_id) Parent_Name "
                      + " ,@iCount icount "
                      + " ,b.row_no "
                      + " From t_item_category a "
                      + "inner join @TmpTable b "
                      + "on(a.Item_Category_Id = b.Item_Category_Id) "
                      + "where 1=1 "
                      + "and b.row_no  > '" + _ht["StartRow"].ToString() + "' and  b.row_no <= '" + _ht["EndRow"] + "' "
                      + " ;";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 查询公共部分 //Jermyn20131021 添加
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string SearchPublicSql(Hashtable _ht)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "Declare @TmpTable Table "
                     + " (item_category_id nvarchar(100) "
                     + " ,row_no int "
                     + " ); "
                     + " Declare @iCount int; "
                     + " insert into @TmpTable(item_category_id,row_no) "
                     + " select x.item_category_id ,x.rownum_ From ( select rownum_=row_number() over(order by a.item_category_code),item_category_id"
                     + " from t_item_category a where 1=1 ";
            sql = pService.GetLinkSql(sql, "a.item_category_code", _ht["item_category_code"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.item_category_name", _ht["item_category_name"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.pyzjm", _ht["pyzjm"].ToString(), "%");
            sql = pService.GetLinkSql(sql, "a.status", _ht["status"].ToString(), "=");
            sql = pService.GetLinkSql(sql, "a.customerId", this.CurrentUserInfo.CurrentUser.customer_id.ToString().Trim(), "=");

            if (_ht["item_category_id"] != null && _ht["item_category_id"].ToString().Length > 0)
            {
                sql += " and a.parent_id='" + _ht["item_category_id"].ToString() + "' ";
            }

            sql = sql + " ) x ;";
            sql = sql + " select @iCount = COUNT(*) From @TmpTable; ";
            #endregion
            return sql;
        }
        /// <summary>
        /// 获取所有商品类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemCagegoryList(string status, string bat_id)
        {
            DataSet ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select a.Item_Category_Id 
                          ,a.Item_Category_Code 
                          ,case when a.status <> '1' then '(已停用)' else '' end + a.Item_Category_Name Item_Category_Name
                          ,a.Pyzjm 
                          ,a.Status 
                          ,a.Parent_Id 
                          ,a.Create_User_Id 
                          ,a.Create_Time 
                          ,a.Modify_User_Id 
                          ,a.modify_time 
                          ,a.displayindex as DisplayIndex
                          ,(select top 1 ImageUrl from objectimages where a.item_category_id = objectid and IsDelete=0 order by create_time desc) as ImageUrl
                          ,case when a.status = '1' then '正常' else '停用' end Status_Desc 
                          ,(select USER_NAME From T_User where T_User.user_id = a.create_user_id) Create_User_Name 
                          ,(select USER_NAME From T_User where T_User.user_id = a.modify_user_id) Modify_User_Name 
                          ,(select item_category_name From T_Item_Category where T_Item_Category.item_category_id = a.parent_id) Parent_Name
,isnull((  select COUNT(1) from ItemCategoryMapping  c INNER JOIN T_ITEM i ON c.ItemId=i.item_id  where c.ItemCategoryId=a.item_category_id  and c.IsDelete=0),0) as PromotionItemCount
                          From t_item_category a where a.customerId = '{0}'", this.CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            if (status != "")
                sql.Append(" and a.status='" + status + "'");

            if (bat_id == "2")//代表是促销分组
            {
                sql.Append(" and ISNULL(a.bat_id,'')='2'");
            }
            else
            {
                sql.Append(" and ISNULL(a.bat_id,'')!='2'");
            }

            sql.Append(" order by DisplayIndex ASC");

            ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// 获取单个商品类别
        /// </summary>
        /// <param name="item_category_id"></param>
        /// <returns></returns>
        public DataSet GetItemCategoryById(string item_category_id)
        {
            DataSet ds = new DataSet();
            string sql = " select a.Item_Category_Id "
                          + " ,a.Item_Category_Code "
                          + " ,a.Item_Category_Name "
                          + " ,a.Pyzjm "
                          + " ,a.Status "
                          + " ,a.Parent_Id "
                          + " ,(SELECT item_category_name FROM T_Item_Category WHERE item_category_id = a.parent_id) Parent_Name "
                          + " ,a.Create_User_Id "
                          + " ,a.Create_Time "
                          + " ,a.Modify_User_Id "
                          + " ,a.modify_time "
                          + " ,a.DisplayIndex "
                          + " ,case when a.status = '1' then '正常' else '停用' end Status_Desc "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.create_user_id) Create_User_Name "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.modify_user_id) Modify_User_Name "
                          + " ,(select item_category_name From T_Item_Category where T_Item_Category.item_category_id = a.parent_id) Parent_Name "
                          + " From t_item_category a where a.item_category_id= '" + item_category_id + "' and a.CustomerId='" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetItemCategoryListByParentId(string parent_id)
        {
            DataSet ds = new DataSet();
            string sql = " select a.Item_Category_Id "
                          + " ,a.Item_Category_Code "
                          + " ,a.Item_Category_Name "
                          + " ,a.Pyzjm "
                          + " ,a.Status "
                          + " ,a.Parent_Id "
                          + " ,a.Create_User_Id "
                          + " ,a.Create_Time "
                          + " ,a.Modify_User_Id "
                           + " ,a.DisplayIndex "
                          + " ,a.modify_time "
                          + " ,case when a.status = '1' then '正常' else '停用' end Status_Desc "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.create_user_id) Create_User_Name "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.modify_user_id) Modify_User_Name "
                          + " ,(select item_category_name From T_Item_Category where T_Item_Category.item_category_id = a.parent_id) Parent_Name "
                          + " From t_item_category a where a.parent_id= '" + parent_id + "' and a.CustomerId='" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region GetItemsBytype
        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public DataSet GetItemsBytype(string itemCategoryId)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@" select  item_id,item_name from  t_item where status = '1' and item_category_id='{0}'", itemCategoryId);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }
        #endregion

        #region
        public DataSet GetItemTagListByParentId(string parent_id)
        {
            DataSet ds = new DataSet();
            string sql = " select a.ItemTagID "
                          + " ,a.ItemTagCode "
                          + " ,a.ItemTagName "
                          + " ,a.Status "
                          + " ,a.ParentId "
                          + " ,a.CreateBy "
                          + " ,a.CreateTime "
                          + " ,a.LastUpdateBy "
                          + " ,a.LastUpdateTime "
                          + " ,case when a.status = '1' then '正常' else '停用' end Status_Desc "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.CreateBy) Create_User_Name "
                          + " ,(select USER_NAME From T_User where T_User.user_id = a.LastUpdateBy) Modify_User_Name "
                          + " ,(select ItemTagName From TItemTag where TItemTag.ItemTagID = a.ParentId) Parent_Name "
                          + " From TItemTag a where ";
            sql += parent_id == "" ? " a.ParentId IS NULL " : (" a.ParentId = '" + parent_id + "' ");
            sql += " and a.CustomerId='" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public bool SetUpdateStatus(ItemCategoryInfo itemCategoryInfo)
        {
            string sql = "update t_item_category "
                       + " set status = '" + itemCategoryInfo.Status + "' "
                       + " ,if_flag = '0' "
                       + " ,Modify_Time = '" + itemCategoryInfo.Modify_Time + "' "
                       + " ,Modify_User_Id =  '" + itemCategoryInfo.Modify_User_Id + "' "
                       + " where item_category_id = '" + itemCategoryInfo.Item_Category_Id + "' ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }

        #endregion



        #region 修改顺序
        /// <summary>
        /// 修改顺序
        /// </summary>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public bool SetItemCategoryDisplayIndex(ItemCategoryInfo itemCategoryInfo)
        {
            string sql = "update t_item_category "
                       + " set DisplayIndex = '" + itemCategoryInfo.DisplayIndex + "' "
                   
                       + " ,Modify_Time = '" + itemCategoryInfo.Modify_Time + "' "
                       + " ,Modify_User_Id =  '" + itemCategoryInfo.Modify_User_Id + "' "
                       + " where item_category_id = '" + itemCategoryInfo.Item_Category_Id + "' ;";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }

        #endregion

        #region GetItemCategoryUsedInfo
        public DataSet GetItemCategoryUsedInfo(string itemcategoryid)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
SELECT count(1) as mhadarea FROM dbo.MHAdArea WHERE ObjectId='{0}' AND IsDelete=0
SELECT count(1) as mhcategoryarea FROM dbo.MHCategoryArea WHERE ObjectId='{0}' AND IsDelete=0
", itemcategoryid);

            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        #endregion

        #region insert into  //Jermyn20131021
        public bool SetItemCategoryInfoInsert(ItemCategoryInfo itemCategoryInfo)
        {
            string sql = "INSERT INTO dbo.T_Item_Category "
                        + " ( item_category_id , "
                        + "   item_category_code , "
                        + "   item_category_name , "
                        + "   pyzjm , "
                        + "   status , "
                        + "   parent_id , "
                        + "   create_user_id , "
                        + "   create_time , "
                        + "   modify_user_id , "
                        + "   modify_time , "
                        + "   bat_id , "
                        + "   if_flag , "
                        + "   CustomerID,DisplayIndex "
                        + " ) "
                        + " select '" + itemCategoryInfo.Item_Category_Id + "' item_category_id "
                        + " ,'" + itemCategoryInfo.Item_Category_Code + "' item_category_code "
                        + " ,'" + itemCategoryInfo.Item_Category_Name + "' item_category_name "
                        + " ,'" + itemCategoryInfo.Pyzjm + "' pyzjm "
                        + " ,'" + itemCategoryInfo.Status + "' status "
                        + " ,'" + itemCategoryInfo.Parent_Id + "' parent_id "
                        + " ,'" + itemCategoryInfo.Create_User_Id + "' create_user_id "
                        + " ,'" + itemCategoryInfo.Create_Time + "' create_time "
                        + " ,'" + itemCategoryInfo.Create_User_Id + "' modify_user_id "
                        + " ,'" + itemCategoryInfo.Create_Time + "' modify_time "
                        + " ,'" + itemCategoryInfo.bat_id + "' bat_id " //" ,null bat_id "
                        + " ,'0' if_flag "
                        + " ,'" + this.CurrentUserInfo.CurrentUser.customer_id + "' CustomerID "
                        + " ,'" + itemCategoryInfo.DisplayIndex + "' DisplayIndex ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region update
        public bool SetItemCategoryInfoUpdate(ItemCategoryInfo itemCategoryInfo)
        {
            bool bReturn = false;
            string sql = "update t_item_category set item_category_id = '" + itemCategoryInfo.Item_Category_Id + "'";
            if (itemCategoryInfo != null || itemCategoryInfo.Item_Category_Code != null)
                sql += ",item_category_code='" + itemCategoryInfo.Item_Category_Code + "'";

            if (itemCategoryInfo != null || itemCategoryInfo.Item_Category_Name != null)
                sql += ",item_category_name = '" + itemCategoryInfo.Item_Category_Name + "'";

            if (itemCategoryInfo != null || itemCategoryInfo.Pyzjm != null)
                sql += ",Pyzjm = '" + itemCategoryInfo.Pyzjm + "'";

            if (itemCategoryInfo != null && itemCategoryInfo.DisplayIndex != null)
                sql += ",DisplayIndex = " + itemCategoryInfo.DisplayIndex + "";

            if (itemCategoryInfo != null || itemCategoryInfo.Status != null)
                sql += ",Status = '" + itemCategoryInfo.Status + "'";

            if (itemCategoryInfo != null || itemCategoryInfo.Parent_Id != null)
                sql += ",Parent_Id = '" + itemCategoryInfo.Parent_Id + "'";

            sql += " ,modify_user_id = '" + this.CurrentUserInfo.CurrentLoggingManager.User_Id + "' ";
            sql += " ,modify_time = '" + GetCurrentDateTime() + "' ";
            sql += " ,if_flag = '0' ";
            sql += " where item_category_id = '" + itemCategoryInfo.Item_Category_Id + "' ";

            var result = this.SQLHelper.ExecuteNonQuery(sql);
            bReturn = (result > 0) ? true : false;
            return bReturn;
        }
        #endregion

        public DataSet GetAllLevel()
        {
            StringBuilder sql = new StringBuilder();
            //TODO：Visiting CategoryLevel
            sql.AppendFormat("select top 1 1 as CategoryLevel from T_Item_Category", CurrentUserInfo.ClientID);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取福利商品类别集合
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetItemTypeList(string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT itemTypeId = a.item_category_id ";
            sql += " ,itemTypeName = a.item_category_name ";
            sql += " ,itemTypeCode = a.item_category_code ";
            sql += " ,displayIndex = row_number() over(order by a.DisplayIndex) ";
            sql += " FROM dbo.T_Item_Category a ";
            sql += " WHERE a.status = 1 ";
            sql += " AND CustomerID = '" + customerId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取第一级商品分类
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetLevel1ItemCategory(string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT categoryId = item_category_id, ";
            sql += " categoryName = item_category_name ";
            sql += " FROM dbo.T_Item_Category ";
            sql += " WHERE CustomerID = '" + customerId + "' ";
            sql += " AND status = '1' AND parent_id in ( ";
            sql += " SELECT item_category_id FROM dbo.T_Item_Category WHERE parent_id = '-99' AND status = '1' ";
            sql += " AND CustomerID = '" + customerId + "' ";
            sql += " ) ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #region 获取首页商品分类/商品分组

        /// <summary>
        /// 获取首页商品分组数量
        /// </summary>
        public int GetItemGroupCount(string customerId, string categoryName)
        {
            string sql = GetItemCategorySql(customerId, categoryName, "2");
            sql = sql + " select count(*) as icount From #tmp   ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取首页商品分类数量
        /// </summary>
        public int GetItemCategoryCount(string customerId, string categoryName)
        {
            string sql = GetItemCategorySql(customerId, categoryName,"1");
            sql = sql + " select count(*) as icount From #tmp  ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取首页商品分组列表
        /// </summary>
        public DataSet GetItemGroupList(string customerId, string categoryName, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;

            string sql = GetItemCategorySql(customerId, categoryName, "2");
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by a.displayindex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取首页商品分类列表
        /// </summary>
        public DataSet GetItemCategoryList(string customerId, string categoryName, int pageIndex, int pageSize)
        {
            int beginSize = pageIndex * pageSize + 1;
            int endSize = pageIndex * pageSize + pageSize;

            string sql = GetItemCategorySql(customerId, categoryName,"1");
            sql += " select * From #tmp a where 1=1  and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by a.displayindex ";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 公共查询sql
        /// </summary>
        /// <returns></returns>
        private string GetItemCategorySql(string customerId, string categoryName,string strBatId)
        {
            string sql = string.Empty;
            sql += " SELECT categoryId = item_category_id ";
            sql += " ,categoryName = item_category_name ";
            sql += " ,displayIndex = ROW_NUMBER() OVER(ORDER BY DisplayIndex) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.T_Item_Category ";
            sql += " WHERE CustomerID = '" + customerId + "' ";
            sql += " AND status = '1' and bat_id='" + strBatId + "' ";

            if (!string.IsNullOrEmpty(categoryName))
            {
                sql += " AND item_category_name LIKE '%" + categoryName + "%' ";
            }

            return sql;
        }
        #endregion

        #region 获取同步福利类型

        /// <summary>
        /// 获取同步福利类型
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareTypeList(string latestTime)
        {
            string sql = string.Empty;

            sql += " SELECT * FROM dbo.vw_preferentialType WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND LastUpdateTime >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 俄丽亚首页种类含商品
        public DataSet GetCategoriesAndItems(string pCustomerID, Guid? pParentID, int pagesize, int pageindex)
        {
            StringBuilder temp = new StringBuilder();
            if (pParentID != null)
            {
                temp.AppendFormat("and parent_id='{0}' ", pParentID);
            }
            StringBuilder sb = new StringBuilder();
            //            sb.AppendFormat(@"select * into #temp from 
            //                                     (select row_number()over(order by a.DisplayIndex) _row,a.*,b.ImageURL 
            //                                        from t_item_category a 
            //                                        left join objectimages b on a.item_category_id=b.objectid 
            //                                       where isnull(b.isdelete,0)=0 and a.status=1
            //                                       {3}
            //                                       and a.customerid='{0}' and exists(select 1 from T_Item_Category tt where a.parent_id=tt.item_category_id and not exists
            //                                        (
            //                                        	select 1 from T_Item_Category ttt where ttt.item_category_id=tt.parent_id
            //                                        ))) t ", pCustomerID);
            sb.AppendFormat(@"select * into #temp from 
                                     (select row_number()over(order by a.DisplayIndex) _row,a.*,b.ImageURL 
                                        from t_item_category a 
                                        left join objectimages b on a.item_category_id=b.objectid 
                                       where isnull(b.isdelete,0)=0 and a.status=1
                                       and a.customerid='{0}' and exists(select 1 from T_Item_Category tt where a.parent_id=tt.item_category_id and not exists
                                        (
                                        	select 1 from T_Item_Category ttt where ttt.item_category_id=tt.parent_id
                                        ))) t ", pCustomerID);
            sb.AppendLine("select * from #temp");
            sb.AppendLine(string.Format(@"select a.* from vw_item_detail a
                                        where a.IsDelete=0  and exists(select 1 from #temp where item_category_id=a.item_category_id) and a.customerid='{0}'
                                        and exists(select 1 from ItemCategoryMapping where itemid=a.item_id and itemCategoryId=a.item_category_id and IsFirstVisit=1 and isdelete=0) ", pCustomerID));
            sb.AppendLine("drop table #temp");
            var ds = this.SQLHelper.ExecuteDataset(sb.ToString());
            return ds;
        }
        #endregion

        #region 根据ADAreaId更新MHSearchArea的数据
        public void UpdateMHSearchAreaData(Guid MHSearchAreaID, string customerId)
        {
            //把最后一位的标点符号去掉
            //adsIdList = adsIdList.Substring(0, adsIdList.Length - 1);
            string userId = this.CurrentUserInfo.UserID;

            string sql = string.Format(@"update a set a.isdelete = 1,LastUpdateBy = '{2}',LastUpdateTime = getdate()
                            from MHSeachArea a,MobileHome b where  a.HomeId = b.HomeId and 
                            MHSearchAreaID!='{0}' and b.customerId = '{1}'", MHSearchAreaID, customerId, userId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region 删除MHSearchArea的数据
        public void DeleteMHSearchAreaData(string customerId)
        {
            string userId = this.CurrentUserInfo.UserID;

            string sql = string.Format(@"update a set isdelete = 1,LastUpdateBy = '{1}',LastUpdateTime = getdate() 
                    from MHSeachArea a,MobileHome b where a.HomeId = b.HomeId and b.customerId = '{0}'", customerId, userId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region 根据ADAreaId更新MHAdArea的数据
        public void UpdateMHAdAreaData(string adsIdList, string customerId)
        {
            //把最后一位的标点符号去掉
            adsIdList = adsIdList.Substring(0, adsIdList.Length - 1);
            string userId = this.CurrentUserInfo.UserID;

            string sql = string.Format(@"update a set a.isdelete = 1,LastUpdateBy = '{2}',LastUpdateTime = getdate()
                            from MHAdArea a,MobileHome b where  a.HomeId = b.HomeId and 
                            AdAreaId not in( {0}) and b.customerId = '{1}'", adsIdList, customerId, userId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region 删除MHAdArea的数据
        public void DeleteMHAdAreaData(string customerId)
        {
            string userId = this.CurrentUserInfo.UserID;

            string sql = string.Format(@"update a set isdelete = 1,LastUpdateBy = '{1}',LastUpdateTime = getdate() 
                    from MHAdArea a,MobileHome b where a.HomeId = b.HomeId and b.customerId = '{0}'", customerId, userId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion


        #region 根据ItemAreaId更新MHItemArea的数据
        public void UpdateMHItemAreaData(string itemAreaList, string customerId, string _areaFlag)
        {
            //把最后一位的标点符号去掉
            itemAreaList = itemAreaList.Substring(0, itemAreaList.Length - 1);
            string userId = this.CurrentUserInfo.UserID;
            string sql = string.Format(@"update a set a.isdelete = 1 ,LastUpdateBy = '{2}',LastUpdateTime = getdate() 
                    from MHItemArea a,MobileHome b where  a.HomeId = b.HomeId and 
                    ItemAreaId not in( {0}) and b.customerId = '{1}' and areaFlag='{3}'"
                , itemAreaList, customerId, userId, _areaFlag);
            this.SQLHelper.ExecuteNonQuery(sql);
        }


        public void DeleteMHItemAreaData(string customerId, string _areaFlag)
        {
            //把最后一位的标点符号去掉
            //     itemAreaList = itemAreaList.Substring(0, itemAreaList.Length - 1);
            string userId = this.CurrentUserInfo.UserID;
            string sql = string.Format(@"update a set a.isdelete = 1 ,LastUpdateBy = '{1}',LastUpdateTime = getdate() 
                    from MHItemArea a,MobileHome b where  a.HomeId = b.HomeId  and b.customerId = '{0}' and areaFlag='{2}'"
                , customerId, userId, _areaFlag);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion


        #region 根据groupId删除MHCategoryArea的数据
        public void DeleteItemCategoryAreaData(string groupId)
        {
            string customerId = this.CurrentUserInfo.ClientID;
            string userId = this.CurrentUserInfo.UserID;
            string sql = string.Format(@"update a set isdelete = 1,LastUpdateBy ='{0}' ,LastUpdateTime =getdate()  
                    from MHCategoryArea a,MobileHome b where a.HomeId = b.HomeId 
                    and a.GroupId={1} and b.customerId = '{2}'", userId, groupId, customerId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除模块关联的数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemCategoryAreaData(string groupId, string strHomeId)
        {
            string customerId = this.CurrentUserInfo.ClientID;
            string userId = this.CurrentUserInfo.UserID;
            string sql = string.Format(@"DELETE MHCategoryArea  where GroupId={0} and HomeId='{1}'", groupId,strHomeId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }

        public void DeleteItemCategoryAreaGroupData(string groupId)
        {
            string userId = this.CurrentUserInfo.UserID;
            string customerId = this.CurrentUserInfo.ClientID;
            string sql = string.Format("update MHCategoryAreaGroup set isdelete = 1,LastUpdateBy ='{1}' ,LastUpdateTime =getdate() " +
                                       " where groupvalue = {0} and customerId = '{2}'", groupId, userId, customerId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除模块数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemCategoryAreaGroupData(string groupId, string strHomeId)
        {
            string userId = this.CurrentUserInfo.UserID;
            string customerId = this.CurrentUserInfo.ClientID;
            string sql = string.Format("DELETE MHCategoryAreaGroup  where groupId = {0} and HomeId = '{1}'", groupId, strHomeId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除活动数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemAreaData(string groupId, string strHomeId)
        {
            string userId = this.CurrentUserInfo.UserID;
            string customerId = this.CurrentUserInfo.ClientID;
            string sql = string.Format("DELETE MHItemArea  where groupId = {0} ", groupId);
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        public DataSet GetItemCategoryInfoList(string customerId)
        {
            string sql = string.Empty;
            sql += " select item_category_id ItemCategoryId, ";
            sql += " item_category_name ItemCategoryName, ";
            sql += " parent_id ItemCategoryParentId, ";
            sql += " 0 displayIndex ,isnull(b.imageurl,'') imageurl from T_item_category a left join objectimages b on a.item_category_id = b.objectId where status = 1 and parent_id =  '-99'  and a.customerId = '" + customerId + "'";
            sql += " and b.isdelete = 0";
            sql += " union all";
            sql += " SELECT ItemCategoryId = a.item_category_id ";
            sql += " ,ItemCategoryName = a.item_category_name ";
            sql += " ,ItemCategoryParentId = a.parent_id ";
            sql += " ,displayIndex = row_number() over(order by a.DisplayIndex),isnull(b.imageurl,'') imageurl ";
            sql += " FROM dbo.T_Item_Category a left join objectimages b on a.item_category_id = b.objectId and b.isdelete = 0 ";
            sql += " WHERE a.status = 1 and parent_id in (select item_category_id from T_Item_Category";
            sql += " where parent_id = '-99' and status = 1 and customerId = '" + customerId + "' )";
            sql += " AND a.CustomerID = '" + customerId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetItemCategoryChildList(string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT ItemCategoryId = a.item_category_id ";
            sql += " ,ItemCategoryName = a.item_category_name ";
            sql += " ,ItemCategoryParentId = a.parent_id ";
            sql += " ,displayIndex = row_number() over(order by a.DisplayIndex),isnull(b.imageurl,'') imageurl ";
            sql += " FROM dbo.T_Item_Category a left join objectimages b on a.item_category_id = b.objectId and b.isdelete = 0 ";
            sql += " WHERE a.status = 1 ";
            sql += " AND a.CustomerID = '" + customerId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 根据父分类查询分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataSet GetItemCategoryByParentID(string parentId)
        {
            string sql = string.Empty;
            sql += " SELECT ItemCategoryId = a.item_category_id ";
            sql += " ,ItemCategoryName = a.item_category_name ";
            sql += " ,ItemCategoryParentId = a.parent_id ";
            sql += " ,displayIndex = row_number() over(order by a.DisplayIndex),isnull(b.imageurl,'') imageurl ";
            sql += " FROM dbo.T_Item_Category a left join objectimages b on a.item_category_id = b.objectId";
            sql += " WHERE a.status = 1 ";
            sql += " AND a.parent_id = '" + parentId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetGoodsList(string customerId, string categoryId, int goodsTypeId, int typeDisplayIndex,
            string goodsName, int pageIndex, int pageSize)
        {
            var sqlWhere = new StringBuilder();
            if (categoryId != null && !string.IsNullOrEmpty(categoryId))
            {
                sqlWhere.Append(" and a.item_category_id in (select item_category_id from vw_item_category_level");
                sqlWhere.AppendFormat(" where path_item_category_id like '%{0}%')", categoryId);
            }
            if (goodsName != null && !string.IsNullOrEmpty(goodsName))
            {
                sqlWhere.AppendFormat(" and a.item_name like '%{0}%'", goodsName);
            }
            var sqlDisplay = new StringBuilder();
            switch (goodsTypeId)
            {
                case 1:
                //sqlDisplay.Append("");
                //break;
                case 2:
                    sqlDisplay.Append(" order by a.create_time desc");
                    break;
                case 3:
                    sqlDisplay.Append(typeDisplayIndex == 1 ? " order by a.SalesPrice desc" : " order by a.SalesPrice asc");
                    break;
                case 4:
                    sqlDisplay.Append(" order by a.salesQty desc");
                    break;
                default:
                    sqlDisplay.Append(" order by a.create_time desc");
                    break;
            }

            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.AppendFormat("select row_number()over({0}) as _row,", sqlDisplay);
            sql.Append("a.item_id ItemId,a.skuid SkuId,a.item_category_id CategoryId,a.item_name ItemName,");
            sql.Append("isnull(a.SalesQty,0) SalesQty,isnull(a.Price,0) Price ,isnull(a.SalesPrice,0) SalesPrice,");
            sql.Append("imageUrl ItemUrl,b.item_category_name CategoryName");
            sql.Append(" from vw_item_detail a left join T_Item_Category b on a.item_category_id = b.item_category_id");
            sql.AppendFormat(" where a.status = 1 and a.customerId = '{0}' and isnull(a.Price,0)>0 and isnull(a.SalesPrice,0)>0 ", customerId);   //过滤价格为空数据 update by Henry 2014-10-29
            sql.Append(sqlWhere);
            sql.Append(") t");

            sql.AppendFormat(" where _row>={0} and _row<={1}"
             , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(sql.ToString());

        }
    }
}
