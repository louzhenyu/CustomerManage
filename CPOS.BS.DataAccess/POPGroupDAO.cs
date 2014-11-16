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
    /// ���ݷ��ʣ� �ն�/�����̷��鶨�� 
    /// ��POPGroup�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class POPGroupDAO
    {
        #region GetPOPGroupByTaskID
        public POPGroupEntity GetPOPGroupByTaskID(Guid taskid)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from POPGroup where POPGroupID=(select POPGroupID from VisitingTask where VisitingTaskID='{0}' ) and isdelete=0", taskid);
            //��ȡ����
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