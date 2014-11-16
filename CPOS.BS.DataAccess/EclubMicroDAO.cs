/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
    /// ��EclubMicro�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubMicroDAO : Base.BaseCPOSDAO, ICRUDable<EclubMicroEntity>, IQueryable<EclubMicroEntity>
    {
        /// <summary>
        /// ��¼΢���Ķ������������
        /// </summary>
        /// <param name="microID">΢����־ID</param>
        /// <param name="field">�ֶΣ�Goods��Shares��</param>
        /// <returns></returns>
        public int AddMicroStats(Guid? microID, string field)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update EclubMicro set {0} = {0} + 1 ", field);
            sbSQL.AppendFormat("where IsDelete=0 ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and MicroID = '{0}';", microID);

            //Access DB 
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        /// <summary>
        /// ȡ΢���Ķ������������
        /// </summary>
        public int GetMicroStats(Guid? microID, string field)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("select top 1 {0} from EclubMicro where IsDelete=0 and MicroID = '{1}'", field, microID);

            //Access DB 
            object o = this.SQLHelper.ExecuteScalar(sbSQL.ToString());
            return o != null ? int.Parse(o.ToString()) : 0;
        }

        #region Get Micro Issue Detial Info By Alan
        /// <summary>
        /// ���ݱ�ʶ����ȡʵ�� Add By Alan 2014-08-18
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public EclubMicroEntity GetByID_V1(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            //��ȡ����ʱͬʱ�����Ķ���
            sql.Append("select MicroId, MicroTypeID, MicroNumberID, MicroTitle, Content, ContentUrl, PublishTime, ImageUrl, ThumbnailImageUrl, Source, SourceUrl, Intro, Sequence, Clicks, Goods, Shares, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete from [EclubMicro] ");
            sql.AppendFormat("where MicroID='{0}' and IsDelete=0 ;", id.ToString());
            //��ȡ����
            EclubMicroEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load_V1(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }
        /// <summary>
        /// װ��ʵ�� Add By Alan 2014-08-18
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load_V1(SqlDataReader pReader, out EclubMicroEntity pInstance)
        {
            this.Load(pReader, out pInstance);
            //����һ���·��ֶΣ����ظ�H5��ʹ�á� add by zyh 2014.8.3
            if (pReader["PublishTime"] != DBNull.Value)
            {
                pInstance.PublishDate = Convert.ToDateTime(pReader["PublishTime"]).ToString("yyyy-MM-dd");
            }
        }
        #endregion
    }
}
