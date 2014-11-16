/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 15:25:07
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
    /// 数据访问：  
    /// 表EclubIndustry的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubIndustryDAO : Base.BaseCPOSDAO, ICRUDable<EclubIndustryEntity>, IQueryable<EclubIndustryEntity>
    {
        public DataSet GetIndustryListByIndustryType(string parentID, int? industryType)
        {
            //Create SQL Text
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select IndustryID,IndustryName from EclubIndustry where CustomerID='{0}' and IsDelete=0 ", this.CurrentUserInfo.ClientID);

            if (!string.IsNullOrEmpty(parentID))
            {
                sb.AppendFormat("and ParentID ='{0}'", parentID.Trim());
            }
            sb.AppendFormat(" Order by Sequence");
            return this.SQLHelper.ExecuteDataset(sb.ToString());
        }

        public DataSet GetIndustryListByIndustryType(string parentID)
        {
            //Create SQL Text
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select CONVERT(nvarchar(50),IndustryID) as ID,IndustryName as Text from EclubIndustry where CustomerID='{0}' and IsDelete=0 ", this.CurrentUserInfo.ClientID);

            if (!string.IsNullOrEmpty(parentID))
            {
                string[] parents = parentID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sbStr = new StringBuilder();
                foreach (var str in parents)
                {
                    if (str.Equals(parents[parents.Length - 1]))
                    {
                        sbStr.AppendFormat("'{0}'", str);
                        break;
                    }
                    sbStr.AppendFormat("'{0}',", str);
                }
                sb.AppendFormat("and ParentID in({0}) ", sbStr.ToString());
            }
            else
            {
                sb.Append("and ParentID is null and IndustryType=1 ");
            }
            sb.AppendFormat(" Order by Sequence");
            return this.SQLHelper.ExecuteDataset(sb.ToString());
        }
    }
}
