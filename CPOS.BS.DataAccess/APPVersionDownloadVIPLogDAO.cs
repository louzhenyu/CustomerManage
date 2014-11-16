/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 16:29:54
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
    /// ��APPVersionDownloadVIPLog�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class APPVersionDownloadVIPLogDAO : Base.BaseCPOSDAO, ICRUDable<APPVersionDownloadVIPLogEntity>, IQueryable<APPVersionDownloadVIPLogEntity>
    {
        public APPVersionDownloadVIPLogDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
        }
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;
        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }
        /// <summary>
        /// ������ؼ�¼�����ݿ�Ϊ��̬��cpos_ap
        /// </summary>
        /// <param name="entity"></param>
        public void AddVersionDownLoadLog(APPVersionDownloadVIPLogEntity entity)
        {
            this.SQLHelper = StaticSqlHelper;
            this.Create(entity);
        }
    }
}
