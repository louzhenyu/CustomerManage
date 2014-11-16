/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
using System.Data;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 终端/经销商分组定义 
    /// 表POPGroup的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class POPGroupDAO
    {
        #region GetPOPGroupByTaskID
        public POPGroupEntity GetPOPGroupByTaskID(Guid taskid)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from POPGroup where POPGroupID=(select POPGroupID from VisitingTask where VisitingTaskID='{0}' ) and isdelete=0", taskid);
            //读取数据
            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            POPGroupEntity[] entity = DataLoader.LoadFrom<POPGroupEntity>(ds.Tables[0]);
            if (entity.Length == 1)
            {
                return entity[0];
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}