/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 10:08:00
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
    /// 业务处理： 世联华安客户关系表 
    /// </summary>
    public partial class WXHouseAssignbuyerBLL
    {
        /// <summary>
        /// 获取客户协议号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public WXHouseAssignbuyerEntity GetWXHouseAssignbuyer(string customerID, string userID)
        {
            return _currentDAO.GetWXHouseAssignbuyer(customerID, userID);
        }

        /// <summary>
        /// 获取最大的客户协议号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public string GetWXHouseMaxAssignbuyer(string customerID)
        {
            return _currentDAO.GetWXHouseMaxAssignbuyer(customerID);
        }
    }
}