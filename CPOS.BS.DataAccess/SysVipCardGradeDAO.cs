/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
    /// ��SysVipCardGrade�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysVipCardGradeDAO : Base.BaseCPOSDAO, ICRUDable<SysVipCardGradeEntity>, IQueryable<SysVipCardGradeEntity>
    {
        #region ��ȡ�ȼ�����
        /// <summary>
        /// ��ȡ�ȼ�����
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public int GetVipLevelCount(string levelId)
        {
            string sql = "select COUNT(*) From Vip where IsDelete = '0' and VipLevel='"+levelId+"' and ClientID = '"+this.CurrentUserInfo.CurrentUser.customer_id+"' ";
            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            
            return count;
        }
        #endregion
    }
}
