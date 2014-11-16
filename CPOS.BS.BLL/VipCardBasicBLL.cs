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
    /// 业务处理：  
    /// </summary>
    public partial class VipCardBasicBLL
    {
        #region 获取会员卡号5位码
        /// <summary>
        /// 获取卡号5位码
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

        #region 获取会员卡号
        /// <summary>
        /// 获取会员卡号
        /// 
        /// 卡类型号（1位）：8
        /// 门店号（6位）：021001（门店号的组成应该由“区号+编号“）
        /// 会员设定号（5位）： 00001
        /// 保留号（1位）：0
        /// 
        /// 会员卡号：卡类型号+门店号+保留号+会员设定号
        /// 总计：13位
        /// </summary>
        /// <returns></returns>
        public string GetVipCardCode(string unitID)
        {
            var unitServer = new UnitService(this.CurrentUserInfo);
            var unitEntity = unitServer.GetUnitById(unitID);
            
            var vipCardCode = string.Empty;
            vipCardCode += "8";             //卡类型号（1位）：8
            vipCardCode += unitEntity.Code; //门店号（6位）：021001（门店号的组成应该由“区号+编号“）
            vipCardCode += "0";             //保留号
            vipCardCode += this.GetVipCardSeqNo();  //会员设定号（5位）： 00001

            return vipCardCode;
        }
        #endregion
    }
}