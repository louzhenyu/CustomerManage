/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/29 11:42:21
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
    /// ��LEventsType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LEventsTypeDAO : Base.BaseCPOSDAO, ICRUDable<LEventsTypeEntity>, IQueryable<LEventsTypeEntity>
    {
        #region ��ȡ������б�
        public DataSet GetEventsTypePage(LEventsTypeEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize;
            int endSize = Page * PageSize + PageSize;
            string sql = GetSql(entity);
            sql += "  select *  from #tmp where DisIndex between '" + beginSize + "' and '" + endSize + "'";
            sql += " select count(1) from #tmp";
            return this.SQLHelper.ExecuteDataset(sql);

        }
        public string GetSql(LEventsTypeEntity entityint)
        {
            string sql = @" select EventTypeID,Title,Remark,ClientID,LastUpdateBy,LastUpdateTime,isnull(GroupNo,1) GroupNo,tu.user_name,
                            row_number()over( order by lt.Createtime desc ) DisIndex 
                            into  #tmp
                            from LEventsType lt
                            left join T_User tu on lt.LastUpdateBy=tu.user_id  
                            where lt.IsDelete='0'";
            if (!string.IsNullOrEmpty(entityint.Title))
            {
                sql += " and lt.Title like '%" + entityint.Title + "%'";
            }
            return sql;
        }
        #endregion


    }
}
