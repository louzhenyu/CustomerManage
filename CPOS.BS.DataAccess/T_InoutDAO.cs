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
    /// ���ݷ��ʣ�  
    /// ��T_Inout�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_InoutDAO : Base.BaseCPOSDAO, ICRUDable<T_InoutEntity>, IQueryable<T_InoutEntity>
    {
        #region �������¶���
        public int BatchChangeOrderStatus(List<InoutInfo> OrderList)
        {
            int Result = 0;
            StringBuilder Sql = new StringBuilder();
            //������ʱ��
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
            foreach (var item in OrderList)
            {
                Sql.AppendFormat(@" insert into #HelpOrede values('{0}','{1}','{2}','{3}','{4}','{3}','{4}',CONVERT(varchar(100), GETDATE(), 25),'{5}') ", item.order_id, item.Field2, item.carrier_id, item.status, item.status_desc, CurrentUserInfo.CurrentUser.User_Id);
            }
            //��������
            Sql.Append(@" update t_inout set Field2=#HelpOrede.Field2,carrier_id=#HelpOrede.carrier_id,
                        [status]=#HelpOrede.[status],status_desc=#HelpOrede.status_desc,Field7=#HelpOrede.Field7,
                        Field10=#HelpOrede.Field10,modify_time=#HelpOrede.modify_time,modify_user_id=#HelpOrede.modify_user_id 
                       from #HelpOrede inner join t_inout on #HelpOrede.order_id=t_inout.order_id ");
            //ɾ����ʱ��
            Sql.Append(" drop table #HelpOrede ");
            try
            {
                if (OrderList.Count > 0)
                    Result = this.SQLHelper.ExecuteNonQuery(Sql.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Result;
        }
        #endregion
    }
}
