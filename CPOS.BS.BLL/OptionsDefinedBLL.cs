/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/19 19:26:38
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

using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理： Options定义 
    /// </summary>
    public partial class OptionsDefinedBLL
    {
        #region GetByOptionName
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public OptionsDefinedEntity GetByOptionName(object pOptionName,string ClientID)
        {
            //参数检查
            return _currentDAO.GetByOptionName(pOptionName, ClientID);
        }
        #endregion
    }
}