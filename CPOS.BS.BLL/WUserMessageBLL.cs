/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/25 15:17:08
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class WUserMessageBLL
    {
        #region ��Ϣͬ��
        /// <summary>
        /// ��Ϣͬ��
        /// </summary>
        public IList<WUserMessageEntity> GetClientUserMessageList(WUserMessageEntity entity, int AppendType)
        {
            IList<WUserMessageEntity> eventsList = new List<WUserMessageEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetClientUserMessageList(entity, AppendType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<WUserMessageEntity>(ds.Tables[0]);
            }
            return eventsList;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetClientUserMessageListCount(WUserMessageEntity entity, int AppendType)
        {
            return _currentDAO.GetClientUserMessageListCount(entity, AppendType);
        }
        #endregion
        /// <summary>
        /// �õ�΢��48Сʱ��û�
        /// </summary>
        /// <returns></returns>
        public WUserMessageEntity[] GetActiveUserMessageList()
        {
            IList<WUserMessageEntity> wUserMessageEntities = new List<WUserMessageEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetActiveUserMessageList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                wUserMessageEntities = DataTableToObject.ConvertToList<WUserMessageEntity>(ds.Tables[0]);
            }
            return wUserMessageEntities.ToArray();
        }
    }
}