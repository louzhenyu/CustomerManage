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
    /// ҵ���� Options���� 
    /// </summary>
    public partial class OptionsDefinedBLL
    {
        #region GetByOptionName
        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public OptionsDefinedEntity GetByOptionName(object pOptionName,string ClientID)
        {
            //�������
            return _currentDAO.GetByOptionName(pOptionName, ClientID);
        }
        #endregion
    }
}