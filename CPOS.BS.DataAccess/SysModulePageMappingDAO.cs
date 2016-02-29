/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/21 16:06:37
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
    /// ��SysModulePageMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysModulePageMappingDAO : BaseCPOSDAO, ICRUDable<SysModulePageMappingEntity>, IQueryable<SysModulePageMappingEntity>
    {
         /// <summary>
        /// ��ѯ���ݿ��SysModulePageMapping ���Ƿ���ڸ�MappingId��PageId������ Add By changjian.tian 2014-05-26
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public DataSet GetExistsVocaVerMappingIDandPageId(string MappingId,string PageId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select * from SysModulePageMapping where VocaVerMappingID='{0}' and PageId='{1}'", MappingId,PageId));
            DataSet ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }

        /// <summary>
        /// ��ѯ���ݿ��SysModulePageMapping ���ش���
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public object GetModulePageMappingBySequence(string MappingId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine(string.Format("select Max(Sequence) as Sequence from SysModulePageMapping where VocaVerMappingID='{0}'", MappingId));
            object Sequence= this.SQLHelper.ExecuteScalar(sbSQL.ToString());
            return Sequence;
        }

        public bool DeleteMappingByIds(string propIds, string PageID)
        {
            string sql = "delete from  SysModulePageMapping  ";
           // sql += " isdelete=1 ";
        //    sql += " ,Last='" +  + "' ";
           // sql += " ,LastUpdateTime='" + propInfo.Modify_Time + "' ";
            sql += " where PageID='" + PageID + "'";
            sql += " and VocaVerMappingID not in (" + propIds + ") ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
    }
}
