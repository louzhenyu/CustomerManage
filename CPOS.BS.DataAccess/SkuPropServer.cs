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
    public class SkuPropServer : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public SkuPropServer(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        public DataSet GetSkuPropList()
        {
            DataSet ds = new DataSet();
            string sql = "select a.sku_prop_id "
                          + " ,a.prop_id "
                          + " ,a.display_index "
                          + " ,b.prop_code "
                          + " ,b.prop_name "
                          + " ,b.prop_input_flag "
                          + " From T_SKU_PROPerty a "
                          + " inner join T_Prop b "
                          + " on(a.prop_id = b.prop_id and a.status = '1') "
                          + " where b.status = '1' "
                          + " and a.CustomerId = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' "
                          + " order by a.display_index";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #region AddSkuProp
        /// <summary>
        /// AddSkuProp
        /// </summary>
        /// <returns></returns>
        public bool AddSkuProp(SkuPropInfo skuPropInfo)
        {
            string sql = "insert into T_Sku_Property  ";
            sql += " (sku_prop_id, prop_id, display_index, status, create_time, create_user_id, modify_time, modify_user_id, CustomerId) ";
            sql += " values ('" + skuPropInfo.sku_prop_id + "' ";
            sql += " ,'" + skuPropInfo.prop_id + "' ";
            sql += " ," + skuPropInfo.display_index;
            sql += " ,'" + skuPropInfo.status + "' ";
            sql += " ,'" + skuPropInfo.create_time + "' ";
            sql += " ,'" + skuPropInfo.create_user_id + "' ";
            sql += " ,'" + skuPropInfo.modify_time + "' ";
            sql += " ,'" + skuPropInfo.modify_user_id + "' ";
            sql += " ,'" + skuPropInfo.CustomerId + "' ";
            sql += " ) ";

            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region CheckSkuProp
        /// <summary>
        /// CheckSkuProp
        /// </summary>
        /// <returns></returns>
        public bool CheckSkuProp(string propId)
        {
            string sql = "select count(*) from T_Sku_Property ";
            sql += " where status='1' and prop_id='" + propId + "'";
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }


        public bool CheckSkuPropByDisplayindex(string customerId, int display_index)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM T_Sku_Property			
			WHERE CustomerID='{0}'
			AND display_index={1} ", customerId, display_index);
            var obj = this.SQLHelper.ExecuteScalar(sql);
            if (obj == DBNull.Value) return false;
            var count = Convert.ToInt32(obj);
            return count > 0 ? true : false;
        }
        #endregion

        #region DeleteSkuProp
        /// <summary>
        /// DeleteSkuProp
        /// </summary>
        /// <returns></returns>
        public bool DeleteSkuProp(SkuPropInfo skuPropInfo)
        {
            //string sql = "update T_Sku_Property set ";
            //sql += " status='-1' ";
            //sql += " ,modify_user_id='" + skuPropInfo.modify_user_id + "' ";
            //sql += " ,modify_time='" + skuPropInfo.modify_time + "' ";
            //sql += " where prop_id='" + skuPropInfo.prop_id + "'";

            string sql = string.Format("delete from T_Sku_Property where prop_id='{0}'", skuPropInfo.prop_id);

            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// 判断商品属性里是否引用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ISCheckSkuProp(string Id)
        {
            string str = "select count(1) from T_ITEM_PROPERTY where prop_id='" + Id + "' ";//除了要判断
            int i = (int)this.SQLHelper.ExecuteScalar(str);
        


            if (i > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断sku是否引用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ISCheckSkuProp2(string Id)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            string str = @"       select count(1) from T_SKU where 
       sku_prop_id1 in (select prop_id from T_Prop where parent_prop_id=@prop_id )
         or   sku_prop_id2 in (select prop_id from T_Prop where parent_prop_id=@prop_id )
            or   sku_prop_id3 in (select prop_id from T_Prop where parent_prop_id=@prop_id )
                or   sku_prop_id4 in (select prop_id from T_Prop where parent_prop_id=@prop_id )
                         or   sku_prop_id5 in (select prop_id from T_Prop where parent_prop_id=@prop_id ) ";//除了要判断
            ls.Add(new SqlParameter("@prop_id", Id));
            int i = (int)this.SQLHelper.ExecuteScalar(CommandType.Text, str,ls.ToArray());



            if (i > 0)
            {
                return true;
            }
            return false;
        }
        #endregion


    }
}
