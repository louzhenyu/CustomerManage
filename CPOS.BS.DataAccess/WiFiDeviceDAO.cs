/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    /// ��WiFiDevice�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WiFiDeviceDAO : Base.BaseCPOSDAO, ICRUDable<WiFiDeviceEntity>, IQueryable<WiFiDeviceEntity>
    {

        #region  ���ݽڵ��Ż�ȡʵ��
        /// <summary>
        /// ���ݽڵ��Ż�ȡʵ��
        /// </summary>
        /// <param name="NodeSn">�ڵ���</param>
        /// <returns></returns>
        public WiFiDeviceEntity GetByNodeSn(string NodeSn)
        {
            //�������
            if (string.IsNullOrEmpty(NodeSn))
                return null;
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WiFiDevice] where NodeSn='{0}' and IsDelete=0 ", NodeSn);
            //��ȡ����
            WiFiDeviceEntity m = null;
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
