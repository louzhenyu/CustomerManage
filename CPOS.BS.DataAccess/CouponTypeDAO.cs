/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��CouponType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CouponTypeDAO : Base.BaseCPOSDAO, ICRUDable<CouponTypeEntity>, IQueryable<CouponTypeEntity>
    {
        #region ��ȡ�Ż݄����
        /// <summary>
        /// ��ȡ�Ż݄��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponType()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select convert(nvarchar(50),CouponTypeID),CoupontypeName from CouponType where IsDelete='0' and (CustomerID is null or CustomerID = '");
            strb.Append(CurrentUserInfo.ClientID);
            strb.Append("')");
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region ��ȡ�Ż�ȯ���
        public DataSet GetCouponTypeList()
        {
            //updated by Willie: CustomerId is null Ϊͨ������ on 2014-09-17
            string sql = @"select 
                         CouponTypeID
                       ,('��'+convert(nvarchar,ISNULL(ConditionValue,0)) +'��' +convert(nvarchar,isnull(ParValue,0))+CouponTypeName) as CouponTypeName
                        , CouponTypeName as OriginalCouponTypeName
                         from  CouponType  where  IsDelete='0' and  (CustomerId is null or CustomerId='" + this.CurrentUserInfo.ClientID + "')";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion
    }
}
