/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:26
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
    /// 数据访问：  
    /// 表T_Inout的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_InoutDAO : Base.BaseCPOSDAO, ICRUDable<T_InoutEntity>, IQueryable<T_InoutEntity>
    {
        #region 获取指定订单佣金信息

        /// <summary>
        /// 获取指定订单佣金信息
        /// </summary>
        /// <param name="pOrderId">订单id</param>
        /// <returns></returns>
        public DataTable GetCommissionList(string pOrderId)
        {
            string sql = @"SELECT ti.item_id,ti.item_category_id,tic.CommissionRate
                ,o.discount_rate,td.enter_price,o.total_amount,o.actual_amount ,
                o.create_time,o.vip_no,o.order_no 
                FROM 
                dbo.T_Inout o WITH(NOLOCK) JOIN dbo.T_Inout_Detail td WITH(NOLOCK) ON o.order_id=td.order_id
                JOIN dbo.T_Sku tsku WITH(NOLOCK) ON tsku.sku_id = td.sku_id
                JOIN dbo.T_Item ti WITH(NOLOCK) ON ti.item_id = tsku.item_id
                JOIN dbo.T_Item_Category tic WITH(NOLOCK) ON tic.item_category_id = ti.item_category_id
                WHERE o.order_id='" + pOrderId + @"'";
            //string sql = @"SELECT ti.item_id,ti.item_category_id,5 AS CommissionRate
            //    ,o.discount_rate,td.enter_price,o.total_amount,o.actual_amount 
            //    FROM 
            //    dbo.T_Inout o WITH(NOLOCK) JOIN dbo.T_Inout_Detail td WITH(NOLOCK) ON o.order_id=td.order_id
            //    JOIN dbo.T_Sku tsku WITH(NOLOCK) ON tsku.sku_id = td.sku_id
            //    JOIN dbo.T_Item ti WITH(NOLOCK) ON ti.item_id = tsku.item_id
            //    WHERE o.order_id='" + pOrderId + @"'";

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        #endregion

        #region 批量更新订单
        public int BatchChangeOrderStatus(List<InoutInfo> OrderList, Dictionary<string,string> SapOrderList = null)
        {
            int Result = 0;
            StringBuilder Sql = new StringBuilder();
            //创建零时表
            Sql.Append(@"create table #HelpOrede
                        (
                        order_id nvarchar(50),
                        Field2 nvarchar(50),
                        carrier_id nvarchar(50),
                        [status] nvarchar(50),
                        status_desc nvarchar(50),
                        Field7 nvarchar(50),
                        Field10 nvarchar(50),
                        modify_time nvarchar(50),
                        modify_user_id nvarchar(50)
                        ) ");

            int index = 0;
            foreach (var item in OrderList)
            {
                Sql.AppendFormat(@" insert into #HelpOrede values('{0}','{1}','{2}','{3}','{4}','{3}','{4}',CONVERT(varchar(100), GETDATE(), 25),'{5}') ", item.order_id, item.Field2, item.carrier_id, item.status, item.status_desc, CurrentUserInfo.CurrentUser.User_Id);
                // 判断商户是否是多利,否则
                if (SapOrderList.ContainsKey(item.order_id))
                {
                    // 索引0是订单号     1是事务类型A/C/U    2是具体内容     取消的时候内容可能为空
                    string[] orders = SapOrderList[item.order_id].Split('|');
                    Sql.AppendFormat(@" INSERT  INTO dbo.DataQueue( IsComplete ,objectType ,CustomerId ,TransType ,FieldNames ,FieldValues ,FieldsInKey ,ConsumptionCount ,PrevTime ,
                LastTime ,ErrorMsg ,NextTime ,Value ,Flied1 ,Flied2 ,Flied3 ,Flied4 ,Flied5        )
                VALUES  ( 0 ,          'UDSORDR' ,
                          '7e144bf108b94505a890ec3a7820db8d' ,
                          '{3}' ,'order_no' ,'{0}' ,1 ,
                          0 ,'' ,'' ,'' ,
                          GETDATE() , -- NextTime - datetime
                          '{1}' ,'{2}' ,
                          '' ,'' ,'' ,''  
                        ); ", orders[0], orders[2], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), orders[1]);
                }
                index++;
            }
            //批量更新
            Sql.Append(@" update t_inout set Field2=#HelpOrede.Field2,carrier_id=#HelpOrede.carrier_id,
                        [status]=#HelpOrede.[status],status_desc=#HelpOrede.status_desc,Field7=#HelpOrede.Field7,
                        Field10=#HelpOrede.Field10,modify_time=#HelpOrede.modify_time,modify_user_id=#HelpOrede.modify_user_id 
                       from #HelpOrede inner join t_inout on #HelpOrede.order_id=t_inout.order_id ");
            //删除零时表
            Sql.Append(" drop table #HelpOrede ");
            SqlTransaction tran = SQLHelper.CreateTransaction();
            try
            {
                if (OrderList.Count > 0)
                {
                    Result = this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, Sql.ToString());
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return Result;
        }
        #endregion
    }
}
