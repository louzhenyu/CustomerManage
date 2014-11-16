/*
 * Author		:陆荣平
 * EMail		:lurp@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/11 14:34:01
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
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
  
    /// <summary>
    /// 产品管理部份CURD 
    /// </summary>
    public class SKUModuleCRUDBLL : DefindModuleCRUD
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public SKUModuleCRUDBLL(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {

        }
        #endregion
        #region 获取产品数据部份
        #region 产品管理不分页数据
        /// <summary>
        /// 不分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <returns>产品数据</returns>
        public DataTable GetSKUData(List<DefindControlEntity> pSearch)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetSKUGridFildSQL()); //获取字SQL
            sql.AppendLine("main.SKUID ");
            sql.AppendLine("from SKU main");
            sql.Append(GetSKULeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetSKUSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0} ", base._pUserInfo.ClientID));
            sql.Append(GetSKUGridSearchSQL(pSearch)); //获取条件
            sql.Append(" order by main.CreateTime desc  ");
            return base.GetData(sql.ToString());
        }
        #endregion
        #region 产品管理分页数据
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetSKUPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetSKUGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.SKUID into #outTemp");
            sql.AppendLine("from SKU main");
            sql.Append(GetSKULeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetSKUSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0}", base._pUserInfo.ClientID));
            sql.Append(GetSKUGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region 产品管理编辑页面获取的数据
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetSKUEditData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetSKUEditFildSQL()); //获取字SQL
            sql.AppendLine("main.SKUID ");
            sql.AppendLine("from SKU main");
            sql.Append(GetSKULeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID={0} and CONVERT(varchar(100),main.SKUID) in (select value from dbo.fnSplitStr('{1}',','))", base._pUserInfo.ClientID, pKeyValue));

            DataTable db = base.GetData(sql.ToString());
            List<DefindControlEntity> l = base.GetEditControls();
            for (var i = 0; i < l.Count; i++)
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][l[i].ControlName] != DBNull.Value)
                        l[i].ControlValue = db.Rows[0][l[i].ControlName].ToString();

                }

            }

            return l;

        }
        #endregion
       
        #endregion
        #region 拼SQL
        #region 查询需要的字段
        #region 产品列表公共查询的字段
        /// <summary>
        /// 返回产品需要特殊处理的字段SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSKUGridFildSQL()
        {
            var pColumnDefind = base.GetGridColumns();
            StringBuilder fieldSQL = base.GetPubGridFildSQL();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 19: //品类
                        fieldSQL.AppendLine(string.Format("T_{0}.CategoryName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 18: //品牌
                        fieldSQL.AppendLine(string.Format("T_{0}.BrandName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 31: //单位分组
                        fieldSQL.AppendLine(string.Format("T_{0}.UnitGroupName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;


                }

            }

            return fieldSQL;
        }
        #endregion
        #region 产品管理编辑页获取数据字段SQL
        /// <summary>
        /// 编辑页面获取数据SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSKUEditFildSQL()
        {

            var pColumnDefind = base.GetGridColumns();
            StringBuilder fieldSQL = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    //case 23: //职位点选用户
                    //    fieldSQL.AppendLine(string.Format("T_{0}.{0} {0}", ColumnName));
                    //    fieldSQL.Append(",");
                    //    break;
                    default:
                        fieldSQL.AppendLine(string.Format("main.{0} {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;

                }

            }
            return fieldSQL;
        }
        #endregion
        #endregion
        #region 左连接语句 left join
        #region 产品管理及列表公共左联接的语句
        /// <summary>
        /// 返回产品需要联接的SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSKULeftGridJoinSQL()
        {

            StringBuilder leftJoinSql = base.GetPubLeftGridJoinSQL();
            var pColumnDefind = base.GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 18: //品牌
                        leftJoinSql.AppendLine(string.Format("left join Brand T_{0} on  T_{0}.BrandID=main.BrandID", ColumnName));
                        break;
                    case 19: //品类
                        leftJoinSql.AppendLine(string.Format("left join Category T_{0} on  T_{0}.CategoryID=main.CategoryID", ColumnName));
                        break;
                    case 31:
                        leftJoinSql.AppendLine(string.Format("left join Unit T_{0} on  T_{0}.UnitID=main.UnitID", ColumnName));
                        break;
                }

            }
            if (!string.IsNullOrEmpty(_pUserInfo.ClientDistributorID) && _pUserInfo.ClientDistributorID != "0")
            {
                leftJoinSql.AppendLine(string.Format("inner join DistributorSKUMapping TDSMappingMain on  TDSMappingMain.SKUID=main.SKUID and TDSMappingMain.IsDelete=0 and TDSMappingMain.DistributorID ='{0}' and TDSMappingMain.ClientID={1}", _pUserInfo.ClientDistributorID, _pUserInfo.ClientID));

            }
            return leftJoinSql;

        }
        #endregion
        #region 用于产品管理编辑页面取数据左联接句
        /// <summary>
        /// 编辑页的连接Join
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSKULeftEditViewJoinSQL()
        {

            StringBuilder leftJoinSql = new StringBuilder();
            var pColumnDefind = base.GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {

                    case 23: //职位点选用户
                        //fieldSQL.AppendLine(string.Format("T_{0}.{0} {0}", ColumnName));
                        break;


                }

            }
            return leftJoinSql;

        }
        #endregion
        #endregion
        #region 条件语句 包括inner join 和 where 后条件语句
        #region 用于产品查询一些公共的条件语句
        /// <summary>
        /// 返回产品要处理的条件语句
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSKUGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = base.GetPubGridSearchSQL(pSearch);
        
            return searchSQL;
        }
        #endregion
        #region 用于产品查询时，递归查询的条件语句，这些数据一般生成表于主表inner join
        /// <summary>
        /// 返回产品特殊查询连接SQL
        /// </summary>
        /// <param name="pSearch"></param>
        /// <returns></returns>
        public StringBuilder GetSKUSearchJoinSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder HierarchyJoinSQL = base.GetPubSearchJoinSQL(pSearch);
            for (int i = 0; i < pSearch.Count; i++)
            {
                string ControlName = pSearch[i].ControlName;
                string ControlValue = pSearch[i].ControlValue;
                switch (pSearch[i].ControlType)
                {
                    case 18: //品牌
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnGetBrand('{0}',1) fn_{1} on  CONVERT(nvarchar(100),main.{1}) = fn_{1}.BrandID", ControlValue, ControlName));
                        break;
                    case 19: //品类
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnGetCategory('{0}',1) fn_{1} on  CONVERT(nvarchar(100),main.{1}) = fn_{1}.CategoryID", ControlValue, ControlName));
                        break;
                }

            }
            return HierarchyJoinSQL;

        }
        #endregion
        #endregion
        #region 需要生成的临时表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<StringBuilder> GetSKUTempSql()
        {

            return null;
        }
        #endregion
        #endregion
        #region 增删改操作
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Create(List<DefindControlEntity> pEditValue)
        {
            StringBuilder sql = base.GetCreateSql(pEditValue);

            //if (!string.IsNullOrEmpty(_pUserInfo.ClientDistributorID) && _pUserInfo.ClientDistributorID != "0")
            //{
            //    sql.AppendLine("declare @GUID int ");
            //    sql.AppendLine("set @GUID=IDENT_CURRENT('SKU')"); //获取自增ID
            //    sql.AppendLine(string.Format("update SKU set ClientDistributorID='{0}' where SKUID=@GUID", _pUserInfo.ClientDistributorID));

            //} 
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            var p = base.GetEditControls();
            StringBuilder sql = base.GetUpdateSql(p,pEditValue, "SKUID", pKeyValue);
         
            //if (!string.IsNullOrEmpty(_pUserInfo.ClientDistributorID) && _pUserInfo.ClientDistributorID != "0")
            //    sql.AppendLine(string.Format("update SKU set ClientDistributorID='{0}' where SKUID={1}", _pUserInfo.ClientDistributorID, pKeyValue));
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="pKeyValue">主健值以,分割</param>
        /// <returns>成功/失败</returns>
        public bool Delete(string pKeyValue, out string resStr)
        {
            resStr = "";

            string sql = base.GetDeleteSql("SKUID", pKeyValue);
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        #endregion
    }
}
