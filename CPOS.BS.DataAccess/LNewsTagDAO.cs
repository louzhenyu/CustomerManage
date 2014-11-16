/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/25 14:38:12
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
    /// ��LNewsTag�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LNewsTagDAO : Base.BaseCPOSDAO, ICRUDable<LNewsTagEntity>, IQueryable<LNewsTagEntity>
    {
        #region �������Ż�ȡ���ż���
        public DataSet GetNewsTagsList(string NewsId)
        {
            string sql = "SELECT *,(SELECT ISNULL(COUNT(*),0) FROM LNewsTagMapping x WHERE x.newsid='"+NewsId+"' AND x.tagId = a.tagId) IsCheck "
                    + ",DisplayIndex=row_number() over(order by a.tagName) "
                    + "FROM  LNewsTag a "
                    + "WHERE a.isdelete = '0' ";
            DataSet ds = new DataSet();

            ds = this.SQLHelper.ExecuteDataset(sql);

            return ds;
        }
        #endregion
    }
}
