/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/24 15:51:18
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
    public partial class WLegalizeBLL
    {
        #region �ͻ���֤�Ƿ����
        /// <summary>
        /// �ͻ���֤�Ƿ����
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public VipEntity SetVipLegalize(string OpenId, string CustomerId,string SalesAmount,out string strError)
        {
            string strNo = string.Empty;
            VipEntity vipInfo = new VipEntity();
            try
            {
                VipBLL vipServer = new VipBLL(this.CurrentUserInfo);
                //���ܲ���ȡ��Ϣ
                bool bReturn = vipServer.GetVipInfoFromApByOpenId(OpenId, null);
                if (bReturn)
                {
                    //��ȡ��ǰ���ֵ
                    int No = _currentDAO.GetMaxNo(CustomerId);
                    #region ���뱾����Ϣ
                    WLegalizeEntity info = new WLegalizeEntity();
                    info.LegalizeId = BaseService.NewGuidPub();
                    info.OpenId = OpenId;
                    info.CustomerId = CustomerId;
                    info.CreateBy = OpenId;
                    info.LastUpdateBy = OpenId;
                    info.No = No;
                    info.SalesAmount = Convert.ToDecimal(SalesAmount);
                    Create(info);
                    #endregion
                    strNo = No.ToString();
                    #region
                    VipEntity[] vipObj = { };
                    vipObj = vipServer.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = OpenId

                    }, null);
                    if (vipObj != null && vipObj.Length > 0 && vipObj[0] != null)
                    {
                        vipInfo = vipObj[0];
                        vipInfo.SerialNumber = strNo;
                        strError = "OK";
                    }
                    else {
                        strError = "������δ��ע΢���˺�</br>����ϵ�ŵ깤����Ա!";
                    }
                    #endregion
                }
                else {
                    strError = "��ȡ�ܲ����ݳ������ҹ���Ա����.";
                }
                
            }
            catch (Exception ex) {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion
    }
}