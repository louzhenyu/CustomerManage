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
    /// ҵ���� ���������ͻ���ϵ�� 
    /// </summary>
    public partial class WXHouseAssignbuyerBLL
    {
        /// <summary>
        /// ��ȡ�ͻ�Э���
        /// </summary>
        /// <param name="customerID">�ͻ�ID</param>
        /// <param name="userID">�û�ID</param>
        /// <returns></returns>
        public WXHouseAssignbuyerEntity GetWXHouseAssignbuyer(string customerID, string userID)
        {
            return _currentDAO.GetWXHouseAssignbuyer(customerID, userID);
        }

        /// <summary>
        /// ��ȡ���Ŀͻ�Э���
        /// </summary>
        /// <param name="customerID">�ͻ�ID</param>
        /// <returns></returns>
        public string GetWXHouseMaxAssignbuyer(string customerID)
        {
            return _currentDAO.GetWXHouseMaxAssignbuyer(customerID);
        }
    }
}