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
    public class ItemPropService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ItemPropService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region
        /// <summary>
        /// 根据商品获取商品属性集合
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public DataSet GetItemPropListByItemId(string item_id, IDbTransaction pTran)
        {
            string sql = string.Format(@" 
                           select z.* 
                           From ( 
                           select a.item_property_id 
                           ,a.item_id 
                           ,(select prop_id From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupId 
                           ,(select prop_name From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupName 
                           ,(select display_index From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupDisplay 
                           ,a.prop_id PropertyCodeId 
                           ,b.prop_name PropertyCodeName 
                           ,(select prop_id From t_prop where t_prop.prop_id = a.prop_value) PropertyDetailId 
                           ,case when (select prop_name From t_prop where t_prop.prop_id = a.prop_value) is null 
                           then a.prop_value 
                           else (select prop_name From t_prop where t_prop.prop_id = a.prop_value) 
                           end PropertyCodeValue 
                           ,a.Status 
                           ,b.display_index PropertyCodeDisplay 
                           From T_Item_Property a 
                           inner join T_Prop b 
                           on(a.prop_id = b.prop_id) 
                           where 1=1 
                           and a.[status] = '1' 
                           and b.[status] = '1' 
                           and a.item_id = '{0}'
                           ) z
                           where 1=1  order by z.PropertyCodeGroupDisplay,z.PropertyCodeDisplay", item_id);


            //isnull('{0}',a.item_id)
            DataSet ds = new DataSet();
            //ds = this.SQLHelper.ExecuteDataset(sql);
            if (pTran != null)
            {
                //this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
                ds = this.SQLHelper.ExecuteDataset((SqlTransaction)pTran, CommandType.Text, sql);
            }
            else
            {
             //   this.SQLHelper.ExecuteNonQuery(sql);
               ds = this.SQLHelper.ExecuteDataset(sql);

            }
            return ds;
        }
        #endregion

        #region 获取商品所有属性集合
        /// <summary>
        /// 获取商品所有属性集合
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemPropInfoList()
        {
            string sql = " select a.prop_id "
                      + " ,a.prop_code "
                      + " ,a.prop_name "
                      + " ,a.display_index "
                      + " From T_Prop a "
                      + " inner join T_Prop b "
                      + " on(a.parent_prop_id = b.prop_id) "
                      + " where a.prop_domain='ITEM' "
                      + " and a.prop_level = '2' "
                      + " and a.status = '1' "
                      + " order by b.display_index,a.display_index;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 设置商品与商品属性关系
        /// <summary>
        /// 设置商品与商品属性关系
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemPropInfo(ItemInfo itemInfo, IDbTransaction pTran, out string strError)
        {
            try
            {
                if (itemInfo.ItemPropList != null)
                {
                    foreach (ItemPropInfo itemPropInfo in itemInfo.ItemPropList)
                    {
                        if (itemPropInfo.Item_Property_Id == null || itemPropInfo.Item_Property_Id.Equals(""))
                        {
                            itemPropInfo.Item_Property_Id = NewGuid();
                        }
                    }
                    if (!SetItemPropStatus(itemInfo, pTran))
                    {
                        strError = "删除商品属性失败";
                        return false;
                    }

                    if (!SetItemPropUpdate(itemInfo, pTran))
                    {
                        strError = "修改商品属性失败";
                        return false;
                    }

                    if (!SetItemPropInsert(itemInfo, pTran))
                    {
                        strError = "插入商品属性失败";
                        return false;
                    }
                }
                strError = "设置商品与商品属性关系成功.";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPropStatus(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region

            string sql = "update t_item_property set status = '-1' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", itemInfo.Modify_Time);

            sql = sql + " where item_id = '" + itemInfo.Item_Id + "' ";

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
        /// 修改商品属性
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPropUpdate(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region
            string sql = "update t_item_property set status = '1' ,prop_value = a.prop_value";
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", itemInfo.Modify_User_Id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", itemInfo.Modify_Time);
            sql = sql + " From ( ";
            int i = 0;
            foreach (ItemPropInfo itemPropInfo in itemInfo.ItemPropList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' item_id "
                          + ",'" + itemPropInfo.PropertyCodeId + "' prop_id "
                          + ",'" + itemPropInfo.Item_Property_Id + "' item_property_id "
                          + ",'" + itemPropInfo.PropertyCodeValue + "' prop_value ";
                i++;

            }
            sql = sql + " ) a  where ( t_item_property.item_id = a.item_id and t_item_property.prop_id = a.prop_id ) or (t_item_property.item_property_id = a.item_property_id)";

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
        /// 插入商品属性
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool SetItemPropInsert(ItemInfo itemInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region

            string sql = " insert into t_item_property "
                        + " (item_property_id "
                        + " ,item_id "
                        + " ,prop_id "
                        + " ,prop_value "
                        + " ,status "
                        + " ,create_user_id "
                        + " ,create_time "
                        + " ,modify_user_id "
                        + " ,modify_time "
                        + " ) "
                        + " SELECT P.item_property_id "
                        + " ,P.item_id "
                        + " ,P.prop_id "
                        + " ,P.prop_value "
                        + " ,P.status "
                        + " ,P.create_user_id "
                        + " ,P.create_time "
                        + " ,P.modify_user_id "
                        + " ,P.modify_time "

                        + " FROM ( ";
            int i = 0;
            foreach (ItemPropInfo itemPropInfo in itemInfo.ItemPropList)
            {
                if (i != 0) { sql = sql + " union all "; }
                sql = sql + "select '" + itemInfo.Item_Id + "' item_id "
                          + ",'" + itemInfo.Create_User_Id + "' modify_user_id "
                          + ",'" + itemInfo.Create_Time + "' modify_time "
                          + ",'" + itemInfo.Create_User_Id + "' create_user_id "
                          + ",'" + itemInfo.Create_Time + "' create_time "
                          + ",'1' status "
                          + ",'" + itemPropInfo.PropertyCodeId + "' prop_id "
                          + ",'" + itemPropInfo.Item_Property_Id + "' item_property_id "
                          + ",'" + itemPropInfo.PropertyCodeValue + "' prop_value "
                          ;
                i++;

            }

            sql = sql + " ) P "
                    + " left join t_item_property  b "
                    + " on(P.item_property_id = b.item_property_id) "
                    + " left join t_item_property c "
                    + " on(P.item_id = c.item_id "
                    + " and P.prop_id = c.prop_id) "
                    + " where b.item_property_id is null "
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


        public bool updateValue(string item_property_id, string prop_value, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();

            #region
            string sql = "update t_item_property set prop_value = '"+prop_value+"' where item_property_id='"+item_property_id+"'" ;        

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
    }
}
