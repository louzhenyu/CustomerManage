/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/27 10:35:51
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
    /// ��GOrder�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class GOrderDAO : Base.BaseCPOSDAO, ICRUDable<GOrderEntity>, IQueryable<GOrderEntity>
    {
        #region ��ȡ�ŵ���Ϣ
        public DataSet GetMapNodeInfo()
        {
            DataSet ds = new DataSet();
            string sql = "select * from [vwGMapNode] ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ��ȡ��ͬ״̬�Ķ�������
        public DataSet GetOrderStatusCount()
        {
            string sql = "SELECT Status,COUNT(*) icount,sum(CONVERT(INT,qty)) qty FROM dbo.GOrder a "
                    + " WHERE a.IsDelete = '0' "
                    + " GROUP BY a.Status order by a.status ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ���������Һ��ʵĽӵ�Ա
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Distance"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetFieldwork(float Distance, string orderId)
        {
            string sql = "SELECT DISTINCT b.* FROM dbo.GOrder a "
                        + " ,(SELECT a.* FROM dbo.Vip a "
                        + "INNER JOIN dbo.T_User_Role b ON(a.VIPID = b.user_id) "
                        + "INNER JOIN dbo.T_Role c ON(b.role_id = c.role_id) WHERE c.role_code = '5171') b "
                        + "WHERE a.Status in ('1','2') "
                        + "and a.OrderId = '" + orderId + "' "
                        + "AND dbo.DISTANCE_TWO_POINTS(a.lat,a.Lng,b.Latitude,b.Longitude) BETWEEN '" + (Distance - 5) + "'  and '" + Distance + "';";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ״̬����
        public bool SetGorderStatus(string orderId, int nextStatus,string nextStatusDesc)
        {
            string sql = "UPDATE dbo.GOrder "
                        + " SET [Status] = '" + nextStatus + "' "
                        + " ,StatusDesc = '" + nextStatusDesc + "' "
                        + " ,FirstPushTime = CASE WHEN '" + nextStatus + "' = '2' THEN GETDATE() ELSE FirstPushTime END "
                        + " ,SecondPushTime = CASE WHEN '" + nextStatus + "' = '3' THEN GETDATE() ELSE SecondPushTime END "
                        + " ,LastUpdateTime = GETDATE() "
                        + " WHERE OrderId = '" + orderId + "';";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region ȷ���յ�
        public DataSet GetReceiptConfirm(string orderId, string userId)
        {
            DataSet ds = new DataSet();
            string sql = " " //--2.�޸�vip����Ϣ
	                    + " UPDATE dbo.Vip "
	                    + " SET Phone = (SELECT Phone FROM dbo.GOrder WHERE OrderId = '"+orderId+"') "
	                    + " ,DeliveryAddress = (SELECT Address FROM dbo.GOrder WHERE OrderId = '"+orderId+"') "
                        + " WHERE VIPID = (select vipid from Gorder where OrderId = '" + orderId + "' ); "
                        + "  " //--3.�޸Ķ�����Ϣ
	                    + " UPDATE dbo.GOrder "
	                    + " SET [Status] = '4' "
	                    + " ,StatusDesc = 'ȷ���յ�' "
	                    + " ,ReceiptVipId = '"+userId+"' "
	                    + " ,ReceiptOpenId = (SELECT WeiXinUserId FROM dbo.Vip WHERE VIPID = '"+userId+"') "
	                    + " ,ReceiptOrderTime = GETDATE() "
	                    + " ,LastUpdateTime = GETDATE() "
	                    + " ,LastUpdateBy = '"+userId+"' "
	                    + " ,unitid = (SELECT TOP 1 a.unit_id FROM dbo.T_User_Role a "
				                    + " INNER JOIN dbo.t_unit b ON(a.unit_id = b.unit_id) "
				                    + " INNER JOIN dbo.T_Type c ON(b.type_id = c.type_id) "
				                    + " WHERE user_id = '"+userId+"' "
				                    + " AND c.type_code = '��ϴ�ŵ�' "
                                    + " ) "
	                    + " WHERE OrderId = '"+orderId+"'; "


                        + " SELECT * FROM dbo.Vip WHERE VIPID = (select vipid from Gorder where OrderId = '" + orderId + "' );";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ��ȡƥ�����Ա����
        public DataSet GetReceiptByOrderId(string OrderId)
        {
            string sql = "SELECT x.OrderId,x.Distance,x.UserName,x.UserId  "
                        + " ,CASE  x.DisplayIndex WHEN '1' THEN '80' "
                        + " 					  WHEN '2' THEN '75' "
                        + " 					  WHEN '3' THEN '60' "
                        + " 					  WHEN '4' THEN '20' "
                        + " 					  ELSE '45' END GoodnessFit "
                        + " FROM ( "
                        + " SELECT DISTINCT a.* "
                        + " ,dbo.DISTANCE_TWO_POINTS(a.lat,a.Lng,b.Latitude,b.Longitude) Distance  "
                        + " ,b.VipName UserName "
                        + " ,b.VIPID UserId "
                        + " ,DisplayIndex = row_number() over(order by b.vipid) "
                        + " FROM dbo.GOrder a "
                        + " ,(SELECT a.* FROM dbo.Vip a "
                        + " INNER JOIN dbo.T_User_Role b ON(a.VIPID = b.user_id) "
                        + " INNER JOIN dbo.T_Role c ON(b.role_id = c.role_id) WHERE c.role_code = '5171') b "
                        + " WHERE a.Status in ('1','2','3') "
                        + " and a.OrderId = '" + OrderId + "' "
                        + " ) x ORDER BY x.Distance ; ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region ��ȡ��������
        public int GetGOrderCode()
        {
            string sql = "select isnull(count(*),0)+1 From GOrder ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region ���������Һ��ʵĽӵ�Ա
        public DataSet GetFieldwork2(float Distance, string orderId)
        {
            string sql = "SELECT DISTINCT b.VipId FROM dbo.GOrder a "
                        + " INNER JOIN dbo.MarketSendLog b ON(a.OrderId = b.MarketEventId) "
                        + " WHERE a.Status in ('2','3') "
                        + " and a.OrderId = '" + orderId + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public DataSet GetFieldwork4(float Distance, string orderId)
        {
            string sql = "SELECT DISTINCT a.ReceiptVipId VipId FROM dbo.GOrder a "
                        //+ " INNER JOIN dbo.MarketSendLog b ON(a.OrderId = b.MarketEventId) "
                        + " WHERE a.Status in ('4') "
                        + " and a.OrderId = '" + orderId + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

    }
}
