/*
 * Author		:陆荣平
 * EMail		:lurp@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/11 16:43:53
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

using System.Collections.Generic;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// SKUContorlSelectBLL 
    /// </summary>
    public class SKUContorlSelectBLL : SKUModuleCRUDBLL
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SKUContorlSelectBLL(LoggingSessionInfo pUserInfo, string pTableName) :
            base(pUserInfo, pTableName)
        {

        }
        #endregion
        #region 点选控件设置值的值，从后端取回的Text的值
        /// <summary>
        /// 点选控件的SetValue返回的值
        /// </summary>
        /// <param name="pKeyValue"></param>
        /// <returns></returns>
        public DataTable GetSelectData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(base.GetSKUEditFildSQL()); //获取字SQL
            sql.AppendLine("main.SKUID ");
            sql.AppendLine("from SKU main");
            sql.Append(base.GetSKULeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID={0} and CONVERT(varchar(100),main.SKUID) in (select value from dbo.fnSplitStr('{1}',','))", base._pUserInfo.ClientID, pKeyValue));
            return base.GetData(sql.ToString());

        }
        #endregion
        #region 用于点选控件，来设置的数据值 己选择排序靠前
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetSelectPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetSKUGridFildSQL()); //获取字SQL
            if (string.IsNullOrEmpty(pKeyValue))
            {
                sql.AppendLine("ROW_NUMBER() OVER( order by main.LastUpdateTime desc) ROW_NUMBER,0 isSelectCheck,");

            }
            else
            {
                sql.AppendLine("ROW_NUMBER() OVER( order by case when T_Check_Temp.value is null then 0 else 1 end desc, main.LastUpdateTime desc) ROW_NUMBER, case when T_Check_Temp.value is null then 0 else 1 end isSelectCheck,");
            }
            sql.AppendLine("main.SKUID into #outTemp");
            sql.AppendLine("from SKU main");
            sql.Append(GetSKULeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetSelectLeftCridJoinSql(pKeyValue));
            sql.AppendLine("");
            sql.Append(GetSKUSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0}", base._pUserInfo.ClientID));
            sql.Append(GetSKUGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region 用于点选产品时的左连接语句
        /// <summary>
        /// 用于点选控件设置值用
        /// </summary>
        /// <param name="pKeyValue">传进来的Key值</param>
        /// <returns></returns>
        public StringBuilder GetSelectLeftCridJoinSql(string pKeyValue)
        {
            StringBuilder leftJoinSql = new StringBuilder();
            if (!string.IsNullOrEmpty(pKeyValue))
            {
                leftJoinSql.AppendLine(string.Format("left join dbo.fnSplitStr('{0}',',') T_Check_Temp on T_Check_Temp.value=CONVERT(varchar(100),main.SKUID)", pKeyValue));

            }
            return leftJoinSql;

        }
        #endregion
       
    }
}
