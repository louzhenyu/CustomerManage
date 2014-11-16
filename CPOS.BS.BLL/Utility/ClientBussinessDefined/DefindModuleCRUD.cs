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
using JIT.Utility.Reflection;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    public abstract class DefindModuleCRUD
    {
        protected LoggingSessionInfo _pUserInfo;
        protected ClientBussinessDefinedDAO _currentDAO;
        protected string _pTableName;//虚拟的tablename，即clientbusinessdefined里的tablename
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo"></param>
        /// <param name="pTableName"></param>
        public DefindModuleCRUD(LoggingSessionInfo pUserInfo, string pTableName)
        {
            this._currentDAO = new ClientBussinessDefinedDAO(pUserInfo);
            _pUserInfo = pUserInfo;
            _pTableName = pTableName;
        }
        #endregion
        #region 获取定义
        /// <summary>
        /// 获得查询条件区域的控件
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetQueryConditionControls()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select * 
                                from ClientBussinessDefined 
                                where isnull(ConditionOrder,0)!=0  and isdelete=0 and ClientID='{0}' and TableName='{1}'
                                order by  ConditionOrder asc
                              ", _pUserInfo.ClientID, _pTableName);
            DataTable dt = _currentDAO.GetDefind(sql.ToString()).Tables[0];
              return GetDefindControls(dt);
           

        }
        /// <summary>
        /// 获取编辑页面的控件 
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditControls()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select * 
                                from ClientBussinessDefined 
                                where isnull(EditOrder,0)!=0  and isdelete=0 and ClientID='{0}' and TableName='{1}'
                                order by  EditOrder asc
                              ", _pUserInfo.ClientID, _pTableName);
            DataTable dt = _currentDAO.GetDefind(sql.ToString()).Tables[0];
            return GetDefindControls(dt);
        }
        /// <summary>
        /// 获取表格数据模型
        /// </summary>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModels()
        {
            List<GridColumnEntity> lc = GetGridColumns();
            List<GridColumnModelEntity> lm = new List<GridColumnModelEntity>();
            for (int i = 0; i < lc.Count; i++)
            {
                GridColumnModelEntity m = new GridColumnModelEntity();
                m.DataIndex = lc[i].DataIndex;
                switch (lc[i].ColumnControlType)
                { 
                    case 2: //整型
                        m.DataType = 2;
                     break;
                    case 3: //数字
                        m.DataType = 3;
                        break;
                    case 4://日期
                        m.DataType = 4;
                        break;
                    default:
                        m.DataType = 1;
                        break;
                
                }
                lm.Add(m);
            
            }
            return lm;
        }
       
        /// <summary>
        /// 获取表格的列定义
        /// </summary>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumns()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select * 
                                from ClientBussinessDefined 
                                where isnull(ListOrder,0)!=0  and isdelete=0 and ClientID='{0}' and TableName='{1}'
                                order by  ListOrder asc
                              ", _pUserInfo.ClientID, _pTableName);
            DataTable dt = _currentDAO.GetDefind(sql.ToString()).Tables[0];
            List<GridColumnEntity> l = new List<GridColumnEntity>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GridColumnEntity m = new GridColumnEntity();
                m.ColumnText = Convert.ToString(dt.Rows[i]["ColumnDesc"]);
                m.ColumnWdith =string.IsNullOrEmpty( dt.Rows[i]["GridWidth"].ToString() )?0:Convert.ToInt32(dt.Rows[i]["GridWidth"]);
                m.DataIndex = Convert.ToString(dt.Rows[i]["ColumnName"]);
                m.ColumnControlType = Convert.ToInt32(dt.Rows[i]["ControlType"]);
                if (dt.Rows[i]["CorrelationValue"] != DBNull.Value)
                    m.CorrelationValue = Convert.ToString(dt.Rows[i]["CorrelationValue"]);
                if (!string.IsNullOrEmpty(dt.Rows[i]["IsMustDo"].ToString()))
                {
                    m.IsMustDo = Convert.ToInt32(dt.Rows[i]["IsMustDo"]);
                }
                l.Add(m);

            }
            return l;
        }
        #endregion
        #region SQL拼接
        /// <summary>
        /// 返回字段SQL
        /// </summary>
        /// <param name="pColumnDefind"></param>
        /// <returns></returns>
        public StringBuilder GetPubGridFildSQL()
        {
            List<GridColumnEntity> pColumnDefind = GetGridColumns();
            StringBuilder fieldSQL = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 301:
                    case 1: //字符串文本
                        fieldSQL.AppendLine(string.Format("main.{0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 2: //整型文本
                        fieldSQL.AppendLine(string.Format("main.{0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 3: //数字文本
                        fieldSQL.AppendLine(string.Format("main.{0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 4: //日期
                        fieldSQL.AppendLine(string.Format("main.{0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 6: //自定义下拉框
                        fieldSQL.AppendLine(string.Format("T_{0}.OptionText {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 7: //省市县
                        fieldSQL.AppendLine(string.Format("T_{0}.city1_name+T_{0}.city2_name+T_{0}.city3_name {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 30: //地图
                        fieldSQL.AppendLine(string.Format("main.Longitude+','+main.Latitude {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                }
            }
            return fieldSQL;
        }
        /// <summary>
        /// 返回关联字段定义表SQL
        /// </summary>
        /// <param name="pTableName"></param>
        /// <param name="pColumnDefind"></param>
        /// <returns></returns>
        public StringBuilder GetPubLeftGridJoinSQL()
        {
            StringBuilder leftJoinSql = new StringBuilder();
            List<GridColumnEntity> pColumnDefind = GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                string CorrelationValue = null;
                switch (cType)
                {

                    case 6: //自定义下拉框
                        CorrelationValue = Convert.ToString(pColumnDefind[i].CorrelationValue);
                        leftJoinSql.AppendLine(string.Format("left join options T_{0} on  T_{0}.OptionName='{1}' and isnull( T_{0}.ClientID,'{2}')='{2}' and T_{0}.OptionValue=main.{0}", ColumnName, CorrelationValue, _pUserInfo.ClientID));
                        break;
                    case 7: //城市
                        leftJoinSql.AppendLine(string.Format("left join T_city T_{0} on   T_{0}.city_code =main.{0}", ColumnName));
                        break;
                }

            }
            return leftJoinSql;
        }
        /// <summary>
        /// 返回一公用的SQL查询条件
        /// </summary>
        /// <param name="pSearch">查询条件数据列表实体</param>
        /// <returns></returns>
        public StringBuilder GetPubGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = new StringBuilder();
            for (int i = 0; i < pSearch.Count; i++)
            {
                string ControlName = pSearch[i].ControlName;
                string ControlValue = pSearch[i].ControlValue;
                switch (pSearch[i].ControlType)
                {
                    case 1: //字符串文本
                        searchSQL.AppendLine(string.Format(" and main.{0} like '%{1}%'", ControlName, ControlValue));
                        break;
                    case 2: //整型文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 3: //数字文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 4: //日期
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                      break;
                    //case 6: //自定义下拉框
                    //    searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                    //    break;
                    case 7: //城市
                        if (ControlValue.Length == 2)
                        {
                            searchSQL.AppendLine(string.Format(" and left(main.{0},2) = '{1}' ", ControlName, ControlValue));
                        }
                        else if (ControlValue.Length == 4)
                        {
                            searchSQL.AppendLine(string.Format(" and left(main.{0},4) = '{1}' ", ControlName, ControlValue));
                        }
                        else if (ControlValue.Length == 6)
                        {
                            searchSQL.AppendLine(string.Format(" and left(main.{0},6) = '{1}' ", ControlName, ControlValue));
                        }
                      break;
                    case 301:
                      if (ControlValue == "1")
                      {
                          searchSQL.AppendLine(string.Format(" and isnull(main.{0},'') <> '' ", ControlName));
                      }
                      else if (ControlValue == "2")
                      {
                          searchSQL.AppendLine(string.Format(" and isnull(main.{0},'') = '' ", ControlName));
                      }
                      break;
                }

            }

            return searchSQL;

        }
        
        #region 重复条件语句查询
        public StringBuilder GetPubRepeatSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = new StringBuilder();
            for (int i = 0; i < pSearch.Count; i++)
            {
                string ControlName = pSearch[i].ControlName;
                string ControlValue = pSearch[i].ControlValue.Trim();
                switch (pSearch[i].ControlType)
                {
                    case 301:
                    case 1: //字符串文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 2: //整型文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 3: //数字文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 4: //日期
                        searchSQL.AppendLine(string.Format(" and main.{0} >= '{1} '  and main.{0}<='{1} 23:59:59 '", ControlName, ControlValue));
                        break;
                    case 27: //省
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 28: //市
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 29: //县
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 33://自动编号
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 34://照片控件
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                }
            }
            return searchSQL;

        }
        #endregion

        /// <summary>
        /// 返回层系JionSQL 用于条件
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetPubSearchJoinSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder HierarchyJoinSQL = new StringBuilder();

            for (int i = 0; i < pSearch.Count; i++)
            {
                string ControlName = pSearch[i].ControlName;
                string ControlValue = pSearch[i].ControlValue;
                switch (pSearch[i].ControlType)
                {
                    //case 7: //城市
                    //    HierarchyJoinSQL.AppendLine(string.Format(" inner join t_city T_{1}  on  T_{1}.city_code = main.{1} ", ControlValue, ControlName));
                    //    break;
                    case 6: //自定义下拉框
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join (select value from dbo.fnSplitStr('{0}',',') ) fn_{1} on ','+CONVERT(nvarchar(100),main.{1})+',' like  '%,'+fn_{1}.value+',%'", ControlValue, ControlName));
                       
                        break;

                }

            }

            return HierarchyJoinSQL;


        }
        /// <summary>
        /// 返回分页SQL
        /// </summary>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public StringBuilder GetPubPageSQL(int? pPageSize, int? pPageIndex)
        {
            StringBuilder pageSql = new StringBuilder();
            pageSql.AppendLine(string.Format(@"
                                                declare @PageIndex int ={0}
                                                declare @PageSize int={1}
                                                declare @PageCount int
                                                declare @RowsCount int
                                                declare @PageStart int
                                                declare @PageEnd int
                                                SELECT @RowsCount=COUNT(1) FROM #OutTemp
                                                if(@RowsCount%@PageSize=0)
                                                begin
                                                set @PageCount=@RowsCount/@PageSize
                                                end
                                                else
                                                begin
                                                set @PageCount=@RowsCount/@PageSize+1
                                                end
                                                if(@PageIndex<0)
                                                begin
                                                set @PageIndex=0
                                                end
                                                else if(@PageIndex>=@PageCount)
                                                begin
                                                set @PageIndex=@PageCount
                                                end
                                                set @PageStart=@PageIndex*@PageSize
                                                set @PageEnd=@PageStart+@PageSize
                                                set @PageEnd=@PageStart+@PageSize
                                                SELECT * FROM #OutTemp WHERE ROW_NUMBER between  @PageStart+1 and @PageEnd
                                                SELECT @RowsCount RowsCount,@PageCount PageCount
                                                DROP TABLE #outTemp
                    ", pPageIndex, pPageSize));

            return pageSql;
        }
        /// <summary>
        /// 排序字段SQL
        /// </summary>
        /// <param name="pLsort"></param>
        /// <returns></returns>
        public StringBuilder GetPubOrderBySql(List<SortEnity> pLsort)
        {
            StringBuilder sql = new StringBuilder();
            if (pLsort!=null)
            for (int i = 0; i < pLsort.Count; i++)
            { 
                if (pLsort[i].SortType==0)
                    sql.AppendLine(string.Format("'main.{0} asc",pLsort[i].SortName));
                else
                    sql.AppendLine(string.Format("'main.{0} desc", pLsort[i].SortName));
                sql.Append(",");
               
            }
            return sql;
        
        
        }
         #endregion
        #region 工具类
        /// <summary>
        /// 返回控件定义
        /// </summary>
        /// <param name="dt">查询定义数据的datatbale</param>
        /// <returns></returns>
        private List<DefindControlEntity> GetDefindControls(DataTable dt)
        {

            List<DefindControlEntity> l = new List<DefindControlEntity>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DefindControlEntity m = new DefindControlEntity();
                ///控件关联值，用于生成层系控件的值
                if (dt.Rows[i]["CorrelationValue"] != DBNull.Value)
                {
                    m.CorrelationValue = Convert.ToString(dt.Rows[i]["CorrelationValue"]);
                }

                if (dt.Rows[i]["ControlType"] != DBNull.Value)
                {//控件类型
                    m.ControlType = Convert.ToInt32(dt.Rows[i]["ControlType"]); ;
                }

                /// <summary>
                /// 数据值类型
                /// </summary>
                if (dt.Rows[i]["ColumnType"] != DBNull.Value)
                    m.ControlValueDataType = Convert.ToInt32(dt.Rows[i]["ColumnType"]); ;
                /// <summary>
                /// 查询控件的Label
                /// </summary>
                //1中文 0英文
                if (dt.Rows[i]["ColumnDesc"] != DBNull.Value)
                    m.fieldLabel = Convert.ToString(dt.Rows[i]["ColumnDesc"]);

                /// <summary>
                /// 作为生成控件命名而用
                /// </summary>
                if (dt.Rows[i]["ColumnName"] != DBNull.Value)
                {
                    m.ControlName = Convert.ToString(dt.Rows[i]["ColumnName"]);
                }
                //是否必填 
                if (dt.Rows[i]["IsMustDo"] != DBNull.Value)
                {
                    m.IsMustDo = Convert.ToInt32(dt.Rows[i]["IsMustDo"]);
                }
                if (dt.Rows[i]["IsRepeat"] != DBNull.Value)
                {
                    m.IsRepeat = Convert.ToInt32(dt.Rows[i]["IsRepeat"]);
                }
                if (dt.Rows[i]["IsRead"] != DBNull.Value)
                {
                    m.IsRead = Convert.ToInt32(dt.Rows[i]["IsRead"]);
                }
                
                l.Add(m);

            }
            return l;
        }
        #endregion
        #region 获取表数据
        public DataSet GetDataByDataSet(string sql)
        {
            return _currentDAO.GetGridData(sql);
        
        }
        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetData(string sql)
        {
            return _currentDAO.GetGridData(sql).Tables[0];
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public PageResultEntity GetPageData(string sql)
        {
            DataSet dt = _currentDAO.GetGridData(sql);
            PageResultEntity r = new PageResultEntity();
            r.GridData = dt.Tables[0];
            r.RowsCount = Convert.ToInt32(dt.Tables[1].Rows[0][0]);
            r.PageCount = Convert.ToInt32(dt.Tables[1].Rows[0][1]);
            return r;
        }

        #endregion
        #region 生成增删改SQL
        /// <summary>
        /// 获取得到添加语句的SQL
        /// </summary>
        /// <param name="pLrm">非特殊的业务数据</param>
        /// <returns>SQL</returns>
        public StringBuilder GetCreateSql(List<DefindControlEntity> pLrm)
        { 
            //return pLrm.ToArray().Where(i=>i.ControlType!=1&&i.ControlType!=2).ToArray();

            StringBuilder sqlf = new StringBuilder();
            StringBuilder sqlvalue=new StringBuilder();
            for (int i = 0; i < pLrm.Count; i++)
            {
                sqlf.Append(pLrm[i].ControlName);
                sqlf.Append(",");
                sqlvalue.Append("'"+pLrm[i].ControlValue+"'");
                sqlvalue.Append(",");
            }
            sqlf.Append("ClientID,CreateBy");
            sqlvalue.Append(_pUserInfo.ClientID.ToString());
            sqlvalue.Append(",");
            sqlvalue.Append(_pUserInfo.UserID.ToString());
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("insert into {0}", _pTableName));
            sql.Append("(");
            sql.Append(sqlf);
            sql.Append(")");
            sql.Append("Values(");
            sql.Append(sqlvalue);
            sql.Append(")");
            return sql;
           
        }
        /// <summary>
        /// GURID
        /// </summary>
        /// <param name="pLrm"></param>
        /// <param name="pKeyName"></param>
        /// <returns></returns>
        public StringBuilder GetCreateSql(List<DefindControlEntity> pLrm, string pKeyName)
        {
            StringBuilder sqlf = new StringBuilder();
            StringBuilder sqlvalue = new StringBuilder();
            for (int i = 0; i < pLrm.Count; i++)
            {
                sqlf.Append(pLrm[i].ControlName);
                sqlf.Append(",");
                sqlvalue.Append("'" + pLrm[i].ControlValue + "'");
                sqlvalue.Append(",");
            }
            sqlf.Append(string.Format("ClientID,{0},LastUpdateBy", pKeyName));
            sqlvalue.Append(_pUserInfo.ClientID.ToString());
            sqlvalue.Append(string.Format(",@GUID,{0}",_pUserInfo.UserID));
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("declare @GUID uniqueidentifier  ");
            sql.AppendLine("set @GUID=NEWID()");
            sql.Append(string.Format("insert into {0}", _pTableName));
            sql.Append("(");
            sql.Append(sqlf);
            sql.Append(")");
            sql.Append("Values(");
            sql.Append(sqlvalue);
            sql.Append(")");
            return sql;
           
        }

        public StringBuilder GetCreateSql(List<DefindControlEntity> pLrm, string pKeyName, string pFactTableName)
        {
            StringBuilder sqlf = new StringBuilder();
            StringBuilder sqlvalue = new StringBuilder();
            for (int i = 0; i < pLrm.Count; i++)
            {
                if (pLrm[i].ControlType == 30)//地图处理成两个字段
                {
                    if (pLrm[i].ControlValue.Split(',').Length == 2)
                    {
                        sqlf.Append("Longitude,Latitude,");
                        sqlvalue.Append("'" + pLrm[i].ControlValue.Split(',')[0] + "','"
                            + pLrm[i].ControlValue.Split(',')[1] + "'");
                        sqlvalue.Append(",");
                    }
                }
                else
                {
                    sqlf.Append(pLrm[i].ControlName);
                    sqlf.Append(",");
                    sqlvalue.Append("'" + pLrm[i].ControlValue + "'");
                    sqlvalue.Append(",");
                }
            }
            sqlf.Append(string.Format("ClientID,{0},LastUpdateBy", pKeyName));
            sqlvalue.Append("'" + _pUserInfo.ClientID.ToString() + "'");
            sqlvalue.Append(string.Format(",@GUID,'{0}'", _pUserInfo.UserID));
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("declare @GUID uniqueidentifier  ");
            sql.AppendLine("set @GUID=NEWID()");
            sql.Append(string.Format("insert into {0}", pFactTableName));
            sql.Append("(");
            sql.Append(sqlf);
            sql.Append(")");
            sql.Append("Values(");
            sql.Append(sqlvalue);
            sql.Append(")");
            return sql;

        }


        /// <summary>
        /// 获取得到修改的SQL
        /// </summary>
        /// <param name="pLrm">非特殊的业务数据</param>
        /// <returns>SQL</returns>
        public StringBuilder GetUpdateSql(List<DefindControlEntity> pLrmNull,List<DefindControlEntity> pLrm, string pKeyName, string pKeyValue)
        {
                StringBuilder sql = new StringBuilder();
                var p = pLrmNull;
                //选把其他值清空
                 sql.AppendLine(string.Format("update {0} ", _pTableName));
                 sql.AppendLine("set ");
                 for (var i = 0; i < p.Count; i++)
                 {
                     sql.AppendLine(string.Format("{0}=null", p[i].ControlName));
                     sql.Append(",");
                 }
                 sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", _pUserInfo.UserID));
                 sql.AppendLine(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
                //再修改有值的数据
                sql.AppendLine(string.Format("update {0} ", _pTableName));
                sql.AppendLine("set ");
                for (var i = 0; i < pLrm.Count; i++)
                {
                    sql.AppendLine(string.Format("{0}='{1}'",pLrm[i].ControlName,pLrm[i].ControlValue));
                    sql.Append(",");
                }
                sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", _pUserInfo.UserID));
                sql.Append(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
                return sql;
        
        }

        #region GetUpdateSql
        /// <summary>
        /// 获取更新语句
        /// </summary>
        /// <param name="pLrmNull"></param>
        /// <param name="pLrm"></param>
        /// <param name="pKeyName"></param>
        /// <param name="pKeyValue"></param>
        /// <param name="pFactTableName">操作的真实表名</param>
        /// <returns></returns>
        public StringBuilder GetUpdateSql(List<DefindControlEntity> pLrmNull, List<DefindControlEntity> pLrm, string pKeyName, string pKeyValue, string pFactTableName)
        {
            StringBuilder sql = new StringBuilder();
            var p = pLrmNull;
            //选把其他值清空
            sql.AppendLine(string.Format("update {0} ", pFactTableName));
            sql.AppendLine("set ");
            for (var i = 0; i < p.Count; i++)
            {
                if (p[i].ControlType == 30)//地图处理成两个字段
                {
                    sql.AppendFormat(" Longitude=null,Latitude=null,");
                }
                else
                {
                    sql.AppendLine(string.Format("{0}=null", p[i].ControlName));
                    sql.Append(",");
                }
            }
            sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", _pUserInfo.UserID));
            sql.AppendLine(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
            //再修改有值的数据
            sql.AppendLine(string.Format("update {0} ", pFactTableName));
            sql.AppendLine("set ");
            for (var i = 0; i < pLrm.Count; i++)
            {
                if (pLrm[i].ControlType == 30)//地图处理成两个字段
                {
                    if (pLrm[i].ControlValue.Split(',').Length == 2)
                    {
                        sql.AppendFormat(" Longitude={0},Latitude={1},", pLrm[i].ControlValue.Split(',')[0], pLrm[i].ControlValue.Split(',')[1]);
                    }
                }
                else
                {
                    sql.AppendLine(string.Format("{0}='{1}',", pLrm[i].ControlName, pLrm[i].ControlValue));
                }
            }
            sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", _pUserInfo.UserID));
            sql.Append(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
            return sql;

        }
        #endregion
        

        /// <summary>
        /// 得到删除的SQL    
        /// </summary>
        /// <param name="pKeyName"></param>
        /// <param name="pKeyValue"></param>
        /// <returns></returns>
        public string GetDeleteSql(string pKeyName,string pKeyValue)
        {
            string sql = string.Format(@"update {0} 
                                        set IsDelete=1 ,LastUpdateTime=getdate(),LastUpdateBy='{3}'
                                        where CONVERT(nvarchar(100),{1}) in 
                                        (select value from dbo.fnSplitStr('{2}',','))", _pTableName, pKeyName, pKeyValue,_pUserInfo.UserID);
            return sql;
        
        }

        public string GetDeleteSql(string pKeyName,string pKeyValue,string pFactTableName)
                {
                    string sql = string.Format(@"update {0} 
                                                set IsDelete=1 ,LastUpdateTime=getdate(),LastUpdateBy='{3}'
                                                where CONVERT(nvarchar(100),{1}) in 
                                                (select value from dbo.fnSplitStr('{2}',','))", pFactTableName, pKeyName, pKeyValue,_pUserInfo.UserID);
                    return sql;
        
                }

        #endregion
        #region 增删改操作
        public bool ICRUDable(string pSql)
        {
            bool res = false;
            TransactionHelper tranHelper = new TransactionHelper(this._pUserInfo);
            IDbTransaction tran = tranHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    _currentDAO.ICRUDable(pSql, tran);

                    tran.Commit();
                 
                    res = true;
                }
                catch 
                {
                    tran.Rollback();
                    throw;
                   
                }
            }
            return res;
        
        }
     
        #endregion


    }
}
