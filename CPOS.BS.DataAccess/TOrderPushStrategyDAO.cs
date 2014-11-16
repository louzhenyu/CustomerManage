/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 18:49:15
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
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��TOrderPushStrategy�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TOrderPushStrategyDAO : Base.BaseCPOSDAO, ICRUDable<TOrderPushStrategyEntity>, IQueryable<TOrderPushStrategyEntity>
    {

        #region ��ȡ�ͻ������б�
        /// <summary>
        /// ��ȡ�ͻ������б�
        /// </summary>
        /// <param name="pCustomer_id">�ͻ�ID ���� f6a7da3d28f74f2abedfc3ea0cf65c01</param>
        /// <param name="pOrderStatus">����״̬</param>
        /// <returns>��ǰ�ͻ���Ӧ�����Ĳ����б�</returns>
        public IList<TOrderPushStrategyEntity> GetTOrderPushStrategyEntityList(string pCustomer_id, string pOrderStatus)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT [Id],[CustomerId],[OrderStatus],[IsIOSPush] ,[IsAndroidPush],[IsWXPush],[IsSMSPush],[IsEmailPush],[PushInfo],[PushObject],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete]");
            sql.AppendLine("FROM [TOrderPushStrategy]");
            sql.AppendLine("WHERE CustomerId = @CustomerId and OrderStatus=@OrderStatus and IsDelete=0");

            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@CustomerId", Value = pCustomer_id},
                new SqlParameter() {ParameterName = "@OrderStatus", Value = pOrderStatus}
            };


            var ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

            #region û���ҵ���Ӧ�Ĳ���,��ȡ��������

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                sql.Clear();
                sql.Append("SELECT [Id],[CustomerId],[OrderStatus],[IsIOSPush] ,[IsAndroidPush],[IsWXPush],[IsSMSPush],[IsEmailPush],[PushInfo],[PushObject],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete]");
                sql.AppendLine("FROM [TOrderPushStrategy]");
                sql.AppendLine("WHERE CustomerId is null and OrderStatus=@OrderStatus and IsDelete=0");

                var parasPublic = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@OrderStatus", Value = pOrderStatus}
            };
                ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), parasPublic.ToArray());
            }

            #endregion

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataLoader.LoadFrom<TOrderPushStrategyEntity>(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }
        #endregion


    }
}
