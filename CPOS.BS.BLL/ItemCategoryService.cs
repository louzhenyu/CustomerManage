using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 商品类别类
    /// </summary>
    public class ItemCategoryService : BaseService
    {
        JIT.CPOS.BS.DataAccess.ItemCategoryService itemCategoryService = null;
        #region 构造函数
        public ItemCategoryService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemCategoryService = new DataAccess.ItemCategoryService(loggingSessionInfo);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="item_category_code">号码</param>
        /// <param name="item_category_name">名称</param>
        /// <param name="pyzjm">拼音助记码</param>
        /// <param name="status">状态</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public ItemCategoryInfo SearchItemCategoryList(string item_category_code
                                                            , string item_category_name
                                                            , string pyzjm
                                                            , string status
                                                            , int maxRowCount
                                                            , int startRowIndex
                                                            , string item_category_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("item_category_code", item_category_code);
            _ht.Add("item_category_name", item_category_name);
            _ht.Add("pyzjm", pyzjm);
            _ht.Add("status", status);
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("item_category_id", item_category_id);

            ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
            int iCount = itemCategoryService.SearchCount(_ht);

            IList<ItemCategoryInfo> itemCategoryInfoList = new List<ItemCategoryInfo>();
            DataSet ds = itemCategoryService.SearchInfoList(_ht);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                itemCategoryInfoList = DataTableToObject.ConvertToList<ItemCategoryInfo>(ds.Tables[0]);
            }

            itemCategoryInfo.ICount = iCount;
            itemCategoryInfo.ItemCategoryInfoList = itemCategoryInfoList;
            return itemCategoryInfo;
        }

        #endregion

        #region 修改状态
        /// <summary>
        /// 设置商品类别状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_category_id">商品类别标识</param>
        /// <param name="status">修改值</param>
        /// <returns></returns>
        public void SetItemCategoryStatus(LoggingSessionInfo loggingSessionInfo, string item_category_id, string status,out string res)
        {
            string strResult = string.Empty;
            try
            {
                #region 停用限制判断
                if (this.GetItemCategoryUsedInfo(item_category_id, out res) != 0)
                {
                    return;
                }
                #endregion

                //设置要改变的类别信息
                ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
                itemCategoryInfo.Status = status;
                itemCategoryInfo.Item_Category_Id = item_category_id;
                itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itemCategoryInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交

                itemCategoryService.SetUpdateStatus(itemCategoryInfo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion


        #region 修改商品分类顺序 
        /// <summary>
        /// 设置商品类别状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_category_id">商品类别标识</param>
        /// <param name="status">修改值</param>
        /// <returns></returns>
        public void SetItemCategoryDisplayIndex(LoggingSessionInfo loggingSessionInfo, string item_category_id, int displayindx )
        {
            string strResult = string.Empty;
            try
            {
                #region 停用限制判断
                //if (this.GetItemCategoryUsedInfo(item_category_id, out res) != 0)
                //{
                //    return;
                //}
                #endregion

                //设置要改变的类别信息
                ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
                itemCategoryInfo.DisplayIndex = displayindx;
                itemCategoryInfo.Item_Category_Id = item_category_id;
                itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itemCategoryInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交

                itemCategoryService.SetItemCategoryDisplayIndex(itemCategoryInfo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion


        #region GetItemCategoryUsedInfo
        /// <summary>
        /// 获取商品分类使用情况
        /// </summary>
        /// <returns>状态码，0可以删除，100以上是有数据</returns>
        public int GetItemCategoryUsedInfo(string categoryid,out string resMsg)
        {
            int resCode = 0;

            DataSet ds = itemCategoryService.GetItemCategoryUsedInfo(categoryid);
            StringBuilder sbRes = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count == 1)
                    {
                        if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                        {
                            resCode = 101;
                            switch (dt.Columns[0].ColumnName.ToString())
                            {
                                case "mhadarea":
                                    sbRes.AppendFormat("移动终端首页广告区域使用了该分类信息</br>", dt.Rows[0][0].ToString());
                                    break;
                                case "mhcategoryarea":
                                    sbRes.AppendFormat("移动终端首页商品分类区域使用了该分类信息</br>", dt.Rows[0][0].ToString());
                                    break;
                            }
                        }
                    }
                }
            }
            if (sbRes.ToString().Length > 0)
            {
                sbRes.Append("操作失败。");
            }
            resMsg = sbRes.ToString();
            return resCode;
        }
        #endregion

        #region 获取商品类别信息
        /// <summary>
        /// 获取所有的商品类别
        /// </summary>
        /// <returns></returns>
        public IList<ItemCategoryInfo> GetItemCagegoryList(string status, string bat_id)
        {
            try
            {
                DataSet ds = itemCategoryService.GetItemCagegoryList(status, bat_id);

                IList<ItemCategoryInfo> itemCategoryInfoList = new List<ItemCategoryInfo>();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    itemCategoryInfoList = DataTableToObject.ConvertToList<ItemCategoryInfo>(ds.Tables[0]);
                }
                return itemCategoryInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取单个商品类别信息
        /// </summary>
        /// <param name="Item_Category_Id"></param>
        /// <returns></returns>
        public ItemCategoryInfo GetItemCategoryById(string Item_Category_Id)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = itemCategoryService.GetItemCategoryById(Item_Category_Id);
                ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    itemCategoryInfo = DataTableToObject.ConvertToObject<ItemCategoryInfo>(ds.Tables[0].Rows[0]);

                    var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);


                    var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = Item_Category_Id }, null);
                    if (tmpImageList != null && tmpImageList.Length > 0)
                    {
                        itemCategoryInfo.ImageUrl = tmpImageList[0].ImageURL;
                    }
                }

                return itemCategoryInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 根据父节点获取子节点所有信息
        /// </summary>
        /// <param name="parent_id"></param>
        /// <returns></returns>
        public IList<ItemCategoryInfo> GetItemCategoryListByParentId(string parent_id)
        {
            IList<ItemCategoryInfo> itemCategoryInfoList = new List<ItemCategoryInfo>();
            DataSet ds = new DataSet();
            ds = itemCategoryService.GetItemCategoryListByParentId(parent_id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                itemCategoryInfoList = DataTableToObject.ConvertToList<ItemCategoryInfo>(ds.Tables[0]);
            }
            return itemCategoryInfoList;
        }
        #endregion

        #region
         //<summary>
         //ItemTag--根据父节点获取子节点所有信息
         //</summary>
         //<param name="parent_id"></param>
         //<returns></returns>
        public IList<TItemTagEntity> GetItemTagListByParentId(string parent_id)
        {
            IList<TItemTagEntity> ItemTagInfoList = new List<TItemTagEntity>();
            DataSet ds = new DataSet();
            ds = itemCategoryService.GetItemTagListByParentId(parent_id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ItemTagInfoList = DataTableToObject.ConvertToList<TItemTagEntity>(ds.Tables[0]);
            }
            return ItemTagInfoList;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存商品类别（新建或者修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public string SetItemCategoryInfo(LoggingSessionInfo loggingSessionInfo, ItemCategoryInfo itemCategoryInfo)
        {
            string strResult = string.Empty;

            //事物信息
            //cSqlMapper.Instance().BeginTransaction();

            try
            {
                //处理是新建还是修改
                if (itemCategoryInfo.Item_Category_Id == null || itemCategoryInfo.Item_Category_Id.Equals(""))
                {
                    //如果是新建，并且没有传值给DisplayIndex，就取他所在子类里最大的displayindex+1
                    if (itemCategoryInfo.DisplayIndex == null || itemCategoryInfo.DisplayIndex == 0)
                    {
                        int displayindex = 0;
                        //获取他的父类下的子分类的
                        IList<ItemCategoryInfo> list = GetItemCategoryListByParentId(itemCategoryInfo.Parent_Id).OrderByDescending(p => p.DisplayIndex).ToList();
                        if(list!=null && list.Count()!=0)
                        {
                           int oldDisplayIndex= list[0].DisplayIndex==null?0: (int)list[0].DisplayIndex;
                            displayindex=oldDisplayIndex+1;
                        }
                        itemCategoryInfo.DisplayIndex=displayindex;
                    }

                    itemCategoryInfo.Item_Category_Id = NewGuid();
                    //2.提交用户信息
                    if (!SetItemCategoryTableInfo(loggingSessionInfo, itemCategoryInfo))
                    {
                        strResult = "提交用户表失败";
                        return strResult;
                    }
                }
                else
                {
                    CPOS.BS.DataAccess.ItemCategoryService server = new DataAccess.ItemCategoryService(loggingSessionInfo);
                    strResult = server.SetItemCategoryInfoUpdate(itemCategoryInfo).ToString();
                }
                var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
                var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = itemCategoryInfo.Item_Category_Id }, null);
                if (tmpImageList != null && tmpImageList.Length > 0)
                {
                    foreach (var tmpImageItem in tmpImageList)
                    {
                        objectImagesBLL.Delete(tmpImageItem);
                    }
                }
                if (itemCategoryInfo.ImageUrl != null && itemCategoryInfo.ImageUrl.Length > 0)
                {
                    objectImagesBLL.Create(new ObjectImagesEntity()
                    {
                        ImageId = NewGuid(),
                        ObjectId = itemCategoryInfo.Item_Category_Id,
                        ImageURL = itemCategoryInfo.ImageUrl
                    });
                }

                strResult = "保存成功!";
                return strResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //return "";
        }


        /// <summary>
        /// 判断号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_category_code"></param>
        /// <param name="item_category_id"></param>
        /// <returns></returns>

        public bool IsExistItemCategoryCode(LoggingSessionInfo loggingSessionInfo, string item_category_code, string item_category_id)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Item_Category_Code", item_category_code);
                _ht.Add("Item_Category_Id", item_category_id);
                //int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ItemCategory.IsExsitItemCategoryCode", _ht);
                int n = 0;
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 保存商品类别信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemCategoryInfo"></param>
        /// <returns></returns>
        public bool SetItemCategoryTableInfo(LoggingSessionInfo loggingSessionInfo, ItemCategoryInfo itemCategoryInfo)
        {
            try
            {
                bool bReturn = false;
                if (itemCategoryInfo != null)
                {
                    itemCategoryInfo.Status = "1";
                    if (itemCategoryInfo.Create_User_Id == null || itemCategoryInfo.Create_User_Id.Equals(""))
                    {
                        itemCategoryInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemCategoryInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (itemCategoryInfo.Modify_User_Id == null || itemCategoryInfo.Modify_User_Id.Equals(""))
                    {
                        itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemCategoryInfo.Modify_Time = GetCurrentDateTime();
                    }
                    CPOS.BS.DataAccess.ItemCategoryService server = new DataAccess.ItemCategoryService(loggingSessionInfo);
                    bReturn = server.SetItemCategoryInfoInsert(itemCategoryInfo);
                }

                return bReturn;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        ///// <summary>
        ///// 获取未打包的商品类别数量
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <returns></returns>
        //public int GetItemCategoryNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        //{
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ItemCategory.SelectUnDownloadCount", "");
        //}
        ///// <summary>
        ///// 需要打包的商品类别信息
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <returns></returns>
        //public IList<ItemCategoryInfo> GetItemCategoryListPackaged(LoggingSessionInfo loggingSessionInfo)
        //{
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemCategoryInfo>("ItemCategory.SelectUnDownload", "");
        //}

        ///// <summary>
        ///// 设置打包批次号
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次号</param>
        ///// <param name="ItemCategoryInfoList">商品集合</param>
        ///// <returns></returns>
        //public bool SetItemCategoryBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ItemCategoryInfo> ItemCategoryInfoList)
        //{
        //    ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
        //    itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //    itemCategoryInfo.Modify_Time = GetCurrentDateTime();
        //    itemCategoryInfo.bat_id = bat_id;
        //    itemCategoryInfo.ItemCategoryInfoList = ItemCategoryInfoList;
        //    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.UpdateUnDownloadBatId", itemCategoryInfo);
        //    return true;

        //}
        ///// <summary>
        ///// 更新Item Category表打包标识方法
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次标识</param>
        ///// <returns></returns>
        //public bool SetItemCategoryIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id)
        //{
        //    ItemCategoryInfo itemCategoryInfo = new ItemCategoryInfo();
        //    itemCategoryInfo.bat_id = bat_id;
        //    itemCategoryInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //    itemCategoryInfo.Modify_Time = GetCurrentDateTime();
        //    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemCategory.UpdateUnDownloadIfFlag", itemCategoryInfo);

        //    return true;

        //}
        #endregion
        #endregion

        #region GetAllLevel
        /// <summary>
        /// 获取品类等级
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllLevel()
        {
            return itemCategoryService.GetAllLevel();
        }
        #endregion

        #region 获取福利商品类别集合

        /// <summary>
        /// 获取福利商品类别集合
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetItemTypeList(string customerId)
        {
            return itemCategoryService.GetItemTypeList(customerId);
        }

        #endregion

        #region 商品分组
        /// <summary>
        /// 首页商品分组列表获取
        /// </summary>
        public DataSet GetItemGroupList(string customerId, string categoryName, int pageIndex, int pageSize)
        {
            return itemCategoryService.GetItemGroupList(customerId, categoryName, pageIndex, pageSize);
        }
        /// <summary>
        /// 首页商品分组数量获取
        /// </summary>
        public int GetItemGroupCount(string customerId, string categoryName)
        {
            return itemCategoryService.GetItemGroupCount(customerId, categoryName);
        }
        #endregion

        #region 获取第一级商品分类

        /// <summary>
        /// 获取第一级商品分类
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetLevel1ItemCategory(string customerId)
        {
            return itemCategoryService.GetLevel1ItemCategory(customerId);
        }

        #endregion

        #region 获取首页商品分类
        /// <summary>
        /// 首页商品分类列表获取
        /// </summary>
        public DataSet GetItemCategoryList(string customerId, string categoryName, int pageIndex, int pageSize)
        {
            return itemCategoryService.GetItemCategoryList(customerId, categoryName, pageIndex, pageSize);
        }
        /// <summary>
        /// 首页商品分类数量获取
        /// </summary>
        public int GetItemCategoryCount(string customerId, string categoryName)
        {
            return itemCategoryService.GetItemCategoryCount(customerId, categoryName);
        }
        #endregion

        #region 获取福利商品类别集合

        /// <summary>
        /// 获取同步福利类型
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareTypeList(string latestTime)
        {
            return itemCategoryService.GetSynWelfareTypeList(latestTime);
        }

        #endregion

        #region 俄丽亚首页
        public CPOS.BS.Entity.Eliya.CategoryInfo[] GetCategoriesAndItems(string pCustomerID, Guid? pParentID, int? pagesize, int? pageindex)
        {
            List<CPOS.BS.Entity.Eliya.CategoryInfo> list = new List<Entity.Eliya.CategoryInfo> { };
            var ds = itemCategoryService.GetCategoriesAndItems(pCustomerID, pParentID ?? null, pagesize ?? 5, pageindex ?? 0);
            var categoryrowlist = ds.Tables[0].Rows;
            var itemrowlist = ds.Tables[1].Rows;
            foreach (DataRow item in categoryrowlist)
            {
                var category = new CPOS.BS.Entity.Eliya.CategoryInfo()
                {
                    CagegoryID = item["item_category_id"].ToString(),
                    ImageUrl = item["ImageURL"].ToString(),
                    TargetUrl = "aldlinks://store/category/customerid="+pCustomerID+"",
                    Name = item["item_category_name"].ToString()
                };
                var itemstemmp = (from DataRow a in itemrowlist
                                  where a["item_category_id"].ToString() == item["item_category_id"].ToString()
                                  select a).Take(2);
                category.ShowItems = itemstemmp.Aggregate(new List<CPOS.BS.Entity.Eliya.ItemInfo> { }, (i, j) =>
                                       {
                                           var temp = new CPOS.BS.Entity.Eliya.ItemInfo()
                                           {
                                               ItemID = j["item_id"].ToString(),
                                               ImageUrl = j["imageUrlFirst"].ToString(),
                                               Name = j["item_name"].ToString(),
                                               Description = j["item_remark"].ToString(),
                                               Ad = j["EdProp"].ToString()      //广告语
                                           };
                                           i.Add(temp);
                                           return i;
                                       }).ToArray();
                list.Add(category);
            }
            return list.ToArray();
        }
        #endregion

        public DataSet GetItemsBytype(string itemCategoryId)
        {
            return itemCategoryService.GetItemsBytype(itemCategoryId);
        }

        public void UpdateMHAdAreaData(string adsIdList,string customerId)
        {
            itemCategoryService.UpdateMHAdAreaData(adsIdList, customerId);
        }

        public void DeleteMHAdAreaData(string customerId)
        {
            itemCategoryService.DeleteMHAdAreaData(customerId);
        }
        public void UpdateMHItemAreaData(string itemAreaList,string customerId,string _areaFlag)
        {
            itemCategoryService.UpdateMHItemAreaData(itemAreaList, customerId, _areaFlag); 
        }
        public void DeleteMHItemAreaData( string customerId, string _areaFlag)
        {
            itemCategoryService.DeleteMHItemAreaData( customerId, _areaFlag);
        }

        public void DeleteItemCategoryAreaData(string groupID)
        {
            itemCategoryService.DeleteItemCategoryAreaData(groupID);
        }
        /// <summary>
        /// 删除模块关联的数据
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemCategoryAreaData(string groupID,string strHomeId)
        {
            itemCategoryService.DeleteItemCategoryAreaData(groupID, strHomeId);
        }
        public void DeleteItemCategoryAreaGroupData(string groupId)
        {
            itemCategoryService.DeleteItemCategoryAreaGroupData(groupId);
        }
        /// <summary>
        /// 删除模块数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemCategoryAreaGroupData(string groupId, string strHomeId)
        {
            itemCategoryService.DeleteItemCategoryAreaGroupData(groupId, strHomeId);
        }
        /// <summary>
        /// 删除活动数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="strHomeId"></param>
        public void DeleteItemAreaData(string groupId, string strHomeId)
        {
            itemCategoryService.DeleteItemAreaData(groupId, strHomeId);
        }
        public void UpdateMHSearchAreaData(Guid MHSearchAreaID, string customerId)
        {
            itemCategoryService.UpdateMHSearchAreaData(MHSearchAreaID, customerId);
        }

        public void DeleteMHSearchAreaData(string customerId)
        {
            itemCategoryService.DeleteMHSearchAreaData(customerId);
        }

       


        public DataSet GetItemCategoryInfoList(string customerId)
        {
            return itemCategoryService.GetItemCategoryInfoList(customerId);
        }

        public DataSet GetItemCategoryChildList(string customerId)
        {
            return itemCategoryService.GetItemCategoryChildList(customerId);
        }
         /// <summary>
        /// 根据父分类查询分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataSet GetItemCategoryByParentID(string parentId)
        {
            return itemCategoryService.GetItemCategoryByParentID(parentId);
        }

        public DataSet GetGoodsList(string customerId, string categoryId, int goodsTypeId, int typeDisplayIndex,
            string goodsName, int pageIndex, int pageSize)
        {
            return itemCategoryService.GetGoodsList(customerId, categoryId, goodsTypeId, typeDisplayIndex, goodsName,
                pageIndex, pageSize);
        }

    }
}

