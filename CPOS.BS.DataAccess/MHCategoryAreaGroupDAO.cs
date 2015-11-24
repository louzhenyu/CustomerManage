/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/5 18:19:19
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
    /// ��MHCategoryAreaGroup�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MHCategoryAreaGroupDAO : BaseCPOSDAO, ICRUDable<MHCategoryAreaGroupEntity>, IQueryable<MHCategoryAreaGroupEntity>
    {
        public int GetMaxGroupId()
        {
            string sql = "select isnull(max(groupId),0) from MHCategoryAreaGroup";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql))+1;
        }
        /// <summary>
        /// ��ȡ��ҳÿ��ģ��������
        /// </summary>
        /// <returns></returns>

        public DataSet GetLayoutList(string pCustomerId)
        {
            
            //C��ģ��2û�ж�Ӧ��ObjectCSSDefine,ͨ���ó��򲻷���ʵ�ʵ�����Ӧ�������ݿ������������¼
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select distinct a.GroupValue,a.ModelName,b.Height,b.TopDistance,b.ModelType,b.DisplayIndex from dbo.MHCategoryAreaGroup a inner join ObjectCSSDefine b on b.ObjectId=a.ModelName inner join MHCategoryArea c on c.GroupId=a.GroupValue where a.IsDelete=0 and b.IsDelete=0 and c.IsDelete=0  and a.CustomerID='" + pCustomerId + "' and c.HomeId =(select top 1 HomeId from MobileHome where CustomerID='" + pCustomerId + "') order by DisplayIndex ");
            return this.SQLHelper.ExecuteDataset(CommandType.Text,sbSQL.ToString());
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="strGroupId"></param>
        /// <returns></returns>
        public int UpdateCategoryAreaGroupDisplayIndex(int strIndex, string strGroupId)
        {
            string sql = "update MHCategoryAreaGroup set DisplayIndex='" + strIndex + "' where GroupId='" + strGroupId + "'";
            return this.SQLHelper.ExecuteNonQuery(sql);
        }
    }
}
