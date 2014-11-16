/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/13 13:44:18
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��TInOutStatusNode�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TInOutStatusNodeDAO : Base.BaseCPOSDAO, ICRUDable<TInOutStatusNodeEntity>, IQueryable<TInOutStatusNodeEntity>
    {
        public void NewLoad(IDataReader rd, out TInOutStatusNodeEntity m)
        {
            this.Load(rd, out m);
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        public void Load(IDataReader pReader, out TInOutStatusNodeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new TInOutStatusNodeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["NodeID"] != DBNull.Value)
            {
                pInstance.NodeID = (Guid)pReader["NodeID"];
            }
            if (pReader["NodeCode"] != DBNull.Value)
            {
                pInstance.NodeCode = Convert.ToString(pReader["NodeCode"]);
            }
            if (pReader["NodeValue"] != DBNull.Value)
            {
                pInstance.NodeValue = Convert.ToString(pReader["NodeValue"]);
            }
            if (pReader["PreviousValue"] != DBNull.Value)
            {
                pInstance.PreviousValue = Convert.ToString(pReader["PreviousValue"]);
            }
            if (pReader["NextValue"] != DBNull.Value)
            {
                pInstance.NextValue = Convert.ToString(pReader["NextValue"]);
            }
            if (pReader["PayMethod"] != DBNull.Value)
            {
                pInstance.PayMethod = Convert.ToString(pReader["PayMethod"]);
            }
            if (pReader["Sequence"] != DBNull.Value)
            {
                pInstance.Sequence = Convert.ToInt32(pReader["Sequence"]);
            }
            if (pReader["CustomerID"] != DBNull.Value)
            {
                pInstance.CustomerID = Convert.ToString(pReader["CustomerID"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["DeliveryMethod"] != DBNull.Value)
            {
                pInstance.DeliveryMethod = Convert.ToString(pReader["DeliveryMethod"]);
            }
            if (pReader["ActionDesc"] != DBNull.Value)
            {
                pInstance.ActionDesc = Convert.ToString(pReader["ActionDesc"]);
            }
            if (pReader["ActionDescEn"] != DBNull.Value)
            {
                pInstance.ActionDescEn = Convert.ToString(pReader["ActionDescEn"]);
            }
        }
        #region ��ȡ������һ��״̬����
        /// <summary>
        /// ��ȡ������һ��״̬����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        public DataSet GetOrderStatusList(string orderId, string paymentTypeId, string deliveryMethodId)
        {
            string sql = "select * From TInOutStatusNode a "
                        + " where a.IsDelete = '0' "
                        + " and a.CustomerID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' "
                        + " and a.NodeValue = (select top 1 status From T_Inout where order_id='" + orderId + "') ";
            if (paymentTypeId != null && paymentTypeId.Length > 0)
            {
                sql = sql + " and a.PayMethod = '" + paymentTypeId + "' ";
            }

            //jifeng.cao 20140416
            if (deliveryMethodId != null && deliveryMethodId.Length > 0)
            {
                sql = sql + " and a.DeliveryMethod = '" + deliveryMethodId + "' ";
            }

            sql = sql + " order by a.Sequence;";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ����֧���ɹ��޸�״̬ Jermyn20140313
        /// <summary>
        /// ����֧���ɹ��޸�״̬
        /// </summary>
        /// <param name="orderId">������ʶ</param>
        /// <param name="strError">������ʾ</param>
        /// <returns></returns>
        public bool SetOrderPayment(string orderId, out string strError, string ChannelId)
        {
            try
            {
                //string sql = "update T_Inout "
                //        //+ " set status = b.NextValue "
                //        //+ " ,Field7 = b.NextValue "
                //        + " ,Field1 = '1' "
                //        //+ " ,Field1 = '1' "
                //        //+ " ,status_desc = case b.NextValue "
                //        //+ " 		   when '300' then 'δ����' "
                //        //+ " 		   when '400' then '�Ѹ���' "
                //        //+ " 		   when '500' then 'δ����' "
                //        //+ " 		   else a.Field10 end "
                //        //+ " ,Field10 = case b.NextValue  "
                //        //+ " 		   when '300' then 'δ����' "
                //        //+ " 		   when '400' then '�Ѹ���' "
                //        //+ " 		   when '500' then 'δ����' "
                //        //+ " 		   else a.Field10 end "
                //        + " ,modify_time = CONVERT(nvarchar(30), GETDATE(),120) "
                //        + " from T_Inout a "
                //        + " inner join ( "
                //        + " select top 1 a.NextValue,a.NodeValue From TInOutStatusNode a "
                //        + " where a.IsDelete = '0' "
                //        + " and a.CustomerID = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' "
                //        + " and a.NodeValue = (select status From T_Inout where order_id='"+orderId+"') "
                //        + " and a.NextValue < '600' and a.NextValue > '200') b "
                //        + " on(a.status = b.NodeValue ) "
                //        + " where a.order_id = '" + orderId + "'";

                string sql = "update T_Inout set Field1 = '1' ";
                if (!string.IsNullOrEmpty( ChannelId))
                {
                    sql += ",pay_id = (select top 1 PaymentTypeID from TPaymentTypeCustomerMapping where IsDelete = '0' and ChannelId = '" + ChannelId + "' and CustomerId='" + this.CurrentUserInfo.CurrentUser.customer_id.ToString() + "') ";
                }
                sql += ",modify_time = CONVERT(nvarchar(30), GETDATE(),120) where order_id = '" + orderId + "' ";
                this.SQLHelper.ExecuteNonQuery(sql);
                strError = "�ɹ�.";
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }

        public void OrderPayCallBack(string orderId,string SerialPay, string customerId, int channelId)
        {
            SqlParameter[] Parm = new SqlParameter[4];
            Parm[0] = new SqlParameter("@pOrderId", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = orderId;
            Parm[1] = new SqlParameter("@pChannelId", System.Data.SqlDbType.Int);
            Parm[1].Value = channelId;        
            Parm[2] = new SqlParameter("@pCustomerId", System.Data.SqlDbType.NVarChar, 100);
            Parm[2].Value = customerId;
            Parm[3] = new SqlParameter("@pSerialPay", System.Data.SqlDbType.NVarChar, 100);
            Parm[3].Value = SerialPay;

            Loggers.Debug(new DebugLogInfo()
            {
                Message = Parm.ToJSON()
            });

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "OrderPayCallBack", Parm);



        }
        #endregion

        #region ��ȡ��Ӧ�ͻ���ȫ������״̬ jifeng.cao 20140319
        /// <summary>
        /// ��ȡ��Ӧ�ͻ���ȫ������״̬
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        public DataSet GetAllOrderStatus(string paymentTypeId)
        {
            string sql = "select * From TInOutStatusNode a "
                        + " where a.IsDelete = '0' "
                        + " and a.CustomerID = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (paymentTypeId != null && paymentTypeId.Length > 0)
            {
                sql = sql + " and a.PayMethod = '" + paymentTypeId + "' ";
            }
            sql = sql + " order by a.Sequence;";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ��ȡ������ִ�в���  changjian.tian 20140421

        /// <summary>
        /// ��ȡ�������ͷ�ʽ
        /// </summary>
        /// <returns></returns>
        public string GetOrderDeliveryMethod(string pOrderId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("select Field8 from T_Inout where order_id='{0}'", pOrderId); //��ȡ�������ͷ�ʽ
            object obj = this.SQLHelper.ExecuteScalar(sbSQL.ToString());
            return obj.ToString();
        }
        /// <summary>
        /// ���ݶ�����źͶ������ͷ�ʽ��ȡ������ִ�в���
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <returns></returns>
        public DataSet GetOrderActions(string pOrderId, string pDeliveryMethod)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat(" and DeliveryMethod='{0}'", string.IsNullOrWhiteSpace(pDeliveryMethod) ? "1" : pDeliveryMethod);
            //�������ݿ��п��ܻ��ж���״̬ ���Ժ���� CustomerID����and DeliveryMethod=1 ����
            sbSQL.AppendFormat("select * From TInOutStatusNode a where a.IsDelete = '0' "+sbWhere+"  and a.CustomerID='{0}' and a.NodeValue = (select  top 1 status from T_Inout where order_id='{1}') order by  Sequence,DeliveryMethod  desc", this.CurrentUserInfo.ClientID, pOrderId);
            DataSet ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        #endregion

        #region ��ȡ��ӡ��Ϣ
        /// <summary>
        //��ȡ��ӡ��Ϣ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet GetPrintPickingInfo(string orderID)
        {
            //����һ����Ʒ���sku��ItemType
            //�����˶�������ı�ע��orderRemark
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
            select ti.order_no as orderno,titem.item_code as itemcode,titem.item_name as itemname,isnull(tid.enter_price,0) as price,order_qty as orderqty,isnull(tid.enter_amount,0) money,tid.remark,ti.remark as orderRemark
            ,ti.Field3 as username,ti.Field6 as tel,ti.field9 as deliveryTime
  ,(CASE WHEN prop_1_id IS NOT NULL THEN prop_1_name + N':' + prop_1_detail_code + N'.' ELSE '' END 
            + CASE WHEN prop_2_id IS NOT NULL  THEN prop_2_name + N':' + prop_2_detail_code + N'.' ELSE '' END 
            + CASE  WHEN prop_3_id IS NOT NULL THEN prop_3_name + N':' + prop_3_detail_code + N'.' ELSE '' END 
            + CASE WHEN prop_4_id IS NOT NULL THEN prop_4_name + N':' + prop_4_detail_code + N'.' ELSE '' END 
            + CASE WHEN prop_5_id IS NOT NULL THEN prop_5_name + N':' + prop_5_detail_code + N'.' ELSE '' END) AS ItemType

            from T_Inout as ti 
            inner join T_Inout_Detail as tid on ti.order_id=tid.order_id and ti.order_id='{0}'
            inner join vw_sku as vm_sku  on vm_sku.sku_id = tid.sku_id
            left join T_Item as titem on titem.item_id=vm_sku.item_id", orderID);

            strb.AppendLine(string.Format(@" select tit.ItemTagID ,tit.ItemTagName from T_Inout as ti 
            inner join T_Inout_Detail as tid on ti.order_id=tid.order_id and ti.order_id='{0}'
            left join T_Sku as tsku on tsku.sku_id=tid.sku_id
            left join ItemCategoryMapping as ic on ic.ItemId=tsku.item_id and ic.IsDelete='0'
            left join TItemTag as tit on convert(nvarchar(50),tit.ItemTagID)=ic.ItemCategoryId and CustomerID='{1}'    
            where tit.ItemTagID is not null
            group by tit.ItemTagID ,tit.ItemTagName ", orderID, CurrentUserInfo.ClientID)); //������CustomerID
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }
        #endregion

        #region ��ȡ��ͬ���͵Ķ�����
        /// <summary>
        /// ��ȡ��ӡ��ͬ���͵Ķ�����
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="itemTagID"></param>
        /// <returns></returns>
        public DataSet GetPrintPickingTypeInfo(string orderID, string itemTagID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
            select ti.order_no as orderno,titem.item_code as itemcode,titem.item_name as itemname,isnull(tsp.sku_price,0) as price,order_qty as orderqty,(isnull(tsp.sku_price,0)*order_qty) money,tid.remark,tit.ItemTagID ,tit.ItemTagName from T_Inout as ti 
            inner join T_Inout_Detail as tid on ti.order_id=tid.order_id and ti.order_id='{0}'
            inner join vw_sku as vm_sku  on vm_sku.sku_id = tid.sku_id
            left join T_Sku as tsku on tsku.sku_id=tid.sku_id
            left join ItemCategoryMapping as ic on ic.ItemId=tsku.item_id
            left join TItemTag as tit on  convert(nvarchar(50),tit.ItemTagID)=ic.ItemCategoryId
            left join T_Item as titem on titem.item_id=tsku.item_id
            left join T_Sku_Price as tsp on tsp.sku_id=tsku.sku_id", orderID);
            if (!string.IsNullOrEmpty(itemTagID))
            {
                strb.AppendFormat(" where ic.ItemCategoryId='{0}'", itemTagID);
            }
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }
        #endregion

    }
}
