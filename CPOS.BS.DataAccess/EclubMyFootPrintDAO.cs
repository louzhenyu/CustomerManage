/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/5 17:14:24
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
    /// ��EclubMyFootPrint�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubMyFootPrintDAO : Base.BaseCPOSDAO, ICRUDable<EclubMyFootPrintEntity>, IQueryable<EclubMyFootPrintEntity>
    {
        /// <summary>
        /// ��¼�㼣
        /// </summary>
        /// <param name="pageCode">ҳ���</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="objectID">�������ID</param>
        /// <param name="footType">0��������1.��Ѷ,  2.��Ƶ ,3.�, 4.�γ�, 5.У��</param>
        /// <param name="operationType">1.��ѯ��2.�޸ģ�3.����,4.ɾ��,5��½��6�ղ�</param>
        public void RecordSpoorInfo(string pageCode, string userID, string objectID, int footType, int operationType)
        {
            //Temp Variant
            string customerId = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
            string userName = CurrentUserInfo.CurrentLoggingManager.User_Name;

            //Build Sql Script Object
            StringBuilder sbPageInfo = new StringBuilder();
            StringBuilder sbPageCount = new StringBuilder();
            StringBuilder sbFootPrint = new StringBuilder();

            //Get PageInfoID By PageCode
            sbPageInfo.Append("select PageInfoID from EclubPageInfo ");
            sbPageInfo.AppendFormat("where PageCode='{0}' and CustomerId='{1}' and IsDelete =0 ;", pageCode, customerId);
            var pageInfoID = this.SQLHelper.ExecuteScalar(sbPageInfo.ToString());
            if (pageInfoID == null)
            {
                return;
            }

            //Create Transaction
            using (var trans = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //Record Page View Count
                    sbPageCount.Append("update dbo.EclubPageInfo set BrowseCount=BrowseCount + 1 ");
                    sbPageCount.AppendFormat("where PageCode='{0}' and IsDelete =0 and CustomerId='{1}' ;", pageCode, customerId);
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbPageCount.ToString());

                    //Record Foot Print
                    sbFootPrint.Append("insert into dbo.EclubMyFootPrint(PageInfoID, VipID, PageDate, ObjectID, CustomerId, CreateBy,LastUpdateBy,FootType,OperationType) ");
                    sbFootPrint.AppendFormat("values('{0}', '{1}', GETDATE(), '{2}', '{3}', '{4}','{4}',{5},{6}) ", pageInfoID, userID, objectID, customerId, userName, footType, operationType);
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbFootPrint.ToString());

                    //Commit Transaction
                    trans.Commit();
                }
                catch
                {
                    //Rollback Transaction
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
