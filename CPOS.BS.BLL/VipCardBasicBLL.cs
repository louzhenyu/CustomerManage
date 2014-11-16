/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/25 17:20:55
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
    public partial class VipCardBasicBLL
    {
        #region ��ȡ��Ա����5λ��
        /// <summary>
        /// ��ȡ����5λ��
        /// </summary>
        /// <returns></returns>
        public string GetVipCardSeqNo()
        {
            string iSeq = string.Empty;
            int seqNo = _currentDAO.GetVipCardSeqNo();
            switch (seqNo.ToString().Length)
            {
                case 1:
                    iSeq = "0000" + seqNo.ToString();
                    break;
                case 2:
                    iSeq = "000" + seqNo.ToString();
                    break;
                case 3:
                    iSeq = "00" + seqNo.ToString();
                    break;
                case 4:
                    iSeq = "0" + seqNo.ToString();
                    break;
                case 5:
                    iSeq = seqNo.ToString();
                    break;
                default:
                    iSeq = "00000";
                    break;
            }
            return iSeq;
        }
        #endregion

        #region ��ȡ��Ա����
        /// <summary>
        /// ��ȡ��Ա����
        /// 
        /// �����ͺţ�1λ����8
        /// �ŵ�ţ�6λ����021001���ŵ�ŵ����Ӧ���ɡ�����+��š���
        /// ��Ա�趨�ţ�5λ���� 00001
        /// �����ţ�1λ����0
        /// 
        /// ��Ա���ţ������ͺ�+�ŵ��+������+��Ա�趨��
        /// �ܼƣ�13λ
        /// </summary>
        /// <returns></returns>
        public string GetVipCardCode(string unitID)
        {
            var unitServer = new UnitService(this.CurrentUserInfo);
            var unitEntity = unitServer.GetUnitById(unitID);
            
            var vipCardCode = string.Empty;
            vipCardCode += "8";             //�����ͺţ�1λ����8
            vipCardCode += unitEntity.Code; //�ŵ�ţ�6λ����021001���ŵ�ŵ����Ӧ���ɡ�����+��š���
            vipCardCode += "0";             //������
            vipCardCode += this.GetVipCardSeqNo();  //��Ա�趨�ţ�5λ���� 00001

            return vipCardCode;
        }
        #endregion
    }
}