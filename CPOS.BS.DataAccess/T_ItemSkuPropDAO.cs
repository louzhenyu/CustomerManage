/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/4/15 11:33:15
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��T_ItemSkuProp�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_ItemSkuPropDAO : Base.BaseCPOSDAO, ICRUDable<T_ItemSkuPropEntity>, IQueryable<T_ItemSkuPropEntity>
    {
        public bool DeleteByItemID(string itemID)
        {
            try
            {
                string sql = "update T_ItemSkuProp "
                    + " set status= '-1',isdelete=1 "

                    + " ,modify_time = '" + DateTime.Now.ToString() + "'"

                    + " where Item_ID = '" + itemID + "';";


             
                    this.SQLHelper.ExecuteNonQuery(sql);
            
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        
         /// <summary>
        /// ������ƷID ��ȡ��Ӧ��sku��Ϣ
        /// </summary>
        /// <param name="itemId">��Ʒ��ʶ</param>
        /// <returns></returns>
        public DataSet GetItemSkuPropByItemId(string itemId)
        {
            DataSet ds = new DataSet();
            string sql = @"select ItemSkuPropID,Item_id,itemsku_prop_1_id as prop_1_id,itemsku_prop_2_id as prop_2_id,itemsku_prop_3_id as prop_3_id
,itemsku_prop_4_id as prop_4_id,itemsku_prop_5_id as prop_5_id
                    ,(select prop_name from T_Prop b where b.prop_id=a.itemsku_prop_1_id) as prop_1_name
                     ,(select prop_code from T_Prop b where b.prop_id=a.itemsku_prop_1_id) as prop_1_code
                      ,(select prop_name from T_Prop b where b.prop_id=a.itemsku_prop_2_id) as prop_2_name
                     ,(select prop_code from T_Prop b where b.prop_id=a.itemsku_prop_2_id) as prop_2_code
                    ,(select prop_name from T_Prop b where b.prop_id=a.itemsku_prop_3_id) as prop_3_name
                     ,(select prop_code from T_Prop b where b.prop_id=a.itemsku_prop_3_id) as prop_3_code
                   ,(select prop_name from T_Prop b where b.prop_id=a.itemsku_prop_4_id) as prop_4_name
                     ,(select prop_code from T_Prop b where b.prop_id=a.itemsku_prop_4_id) as prop_4_code
                   ,(select prop_name from T_Prop b where b.prop_id=a.itemsku_prop_5_id) as prop_5_name
                     ,(select prop_code from T_Prop b where b.prop_id=a.itemsku_prop_5_id) as prop_5_code
                                
                        from T_ItemSkuProp" + " a  where a.item_id = '" + itemId + "' and a.status = '1' and isdelete=0  ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
    }
}
