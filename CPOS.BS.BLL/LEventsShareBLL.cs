/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/2 15:39:12
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class LEventsShareBLL
    {  
        
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="strShareId"></param>
        /// <param name="intState"></param>
        public void UpdateEventShareStatus(string strShareId, int intState)
        {
            this._currentDAO.UpdateEventShareStatus(strShareId, intState);
        }
        /// <summary>
        /// ��ӷ������ý�Ʒ
        /// </summary>
        /// <param name="pEntity"></param>
        public void SaveSharePrize(LPrizesEntity pEntity)
        {
            this._currentDAO.SaveSharePrize(pEntity);
        }
        /// <summary>
        /// ׷�ӷ������ý�Ʒ
        /// </summary>
        /// <param name="pEntity"></param>
        public void AppendSharePrize(LPrizesEntity pEntity)
        {
            this._currentDAO.AppendSharePrize(pEntity);
        }

        public DataSet GetShareList(int pageIndex, int pageSize)
        {
            return _currentDAO.GetShareList(pageIndex,pageSize);
        }
        /// <summary>
        /// ��Ƿ��������˷���
        /// </summary>
        /// <param name="strEventId"></param>
        /// <returns></returns>
        public int HasShare(string strEventId)
        {
            return this._currentDAO.HasShare(strEventId);
        }
    }
}