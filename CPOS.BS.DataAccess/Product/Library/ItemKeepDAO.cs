/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:34
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
    /// ��ItemKeep�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ItemKeepDAO : Base.BaseCPOSDAO, ICRUDable<ItemKeepEntity>, IQueryable<ItemKeepEntity>
    {
        #region ��ȡ�û��ղ�
        /// <summary>
        /// ��ȡ�û��ղ�
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVipId"></param>
        /// <returns></returns>
        public ItemKeepEntity GetItemKeepByUser(string pItemId, string pVipId)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ItemKeep] where ItemId='{0}' and VipId='{1}' and IsDelete=0 ", pItemId, pVipId);
            //��ȡ����
            ItemKeepEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }
        #endregion
    }
}
