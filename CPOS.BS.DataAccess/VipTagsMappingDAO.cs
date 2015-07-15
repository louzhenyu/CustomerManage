/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:09:10
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
    /// ��VipTagsMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipTagsMappingDAO : Base.BaseCPOSDAO, ICRUDable<VipTagsMappingEntity>, IQueryable<VipTagsMappingEntity>
    {
        public DataSet GetList(VipTagsMappingEntity qEntity)
        {
            IList<TagsEntity> list = new List<TagsEntity>();
            string sql = string.Format("select t.* from tags t inner join viptagsmapping vtm on t.TagsID=vtm.TagsID where vtm.VipID='{0}'", qEntity.VipId);

            DataSet ds = SQLHelper.ExecuteDataset(sql);
            //���ؽ��
            return ds;
        }

        public bool DeleteByVipID(string VipID)
        {
            try
            {
                string sql = "update viptagsmapping "
                    + " set isdelete=1 "



                    + " where VipID = '" + VipID + "';";



                this.SQLHelper.ExecuteNonQuery(sql);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
