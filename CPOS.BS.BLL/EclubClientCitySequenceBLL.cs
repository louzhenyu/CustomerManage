/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    public partial class EclubClientCitySequenceBLL
    {

        #region ��ȡʡ��������
        /// <summary>
        /// ��ȡʡ��������
        /// </summary>
        /// <param name="cityType">1.Ϊʡ��2.Ϊ�У�3.Ϊ��</param>
        /// <param name="privinceName">ʡ����</param>
        /// <param name="cityName">������</param>
        /// <returns></returns>
        public DataSet GetCityList(int? cityType, string cityCode)
        {
            return _currentDAO.GetCityList(cityType, cityCode);
        }

         /// <summary>
        /// ��ȡʡ�������ݣ��޸�
        /// </summary>
        /// <param name="cityType">1.Ϊʡ��2.Ϊ�У�3.Ϊ��</param>
        /// <param name="cityCode">���</param>
        /// <returns></returns>
        public DataSet GetCityList_V1(int? cityType, string cityCode)
        {
            return _currentDAO.GetCityList_V1(cityType, cityCode);
        }
        #endregion

    }
}