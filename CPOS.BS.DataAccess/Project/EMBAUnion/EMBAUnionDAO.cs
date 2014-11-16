using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 中欧数据处理
    /// </summary>
    public partial class EMBAUnionDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO
    {
        protected string _pTableName = "vip";//虚拟的tablename，即MobileBussinessDefined里的TableName

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EMBAUnionDAO(LoggingSessionInfo pUserInfo,string pTableName)
            : base(pUserInfo)
        {
            this._pTableName = pTableName;
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select ");
            strSql.AppendFormat(GetFiledStr());
            strSql.AppendFormat(",ROW_NUMBER() OVER( order by Vip.CreateTime desc) ROW_NUMBER,");
            strSql.AppendFormat("Vip.VIPID into #outTemp from VIP as Vip");
           // strSql.AppendFormat("");
            strSql.AppendFormat(GetLeftOption() + " ");
            strSql.AppendFormat("where Vip.ClientID='{0}' ", CurrentUserInfo.ClientID);
            strSql.AppendFormat(GetPubGridVipSerch(pSearch));
            strSql.AppendFormat(GetPubPageSQL(pPageSize, pPageIndex).ToString());
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetModuleColumn
        /// <summary>
        /// 获取显示列表头
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleColumn()
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@"SELECT 
            ColumnDesc,ColumnName,ControlType,CorrelationValue
            from  MobileBussinessDefined 
            WHERE CustomerID='{0}' AND isDelete='0' 
             ORDER BY ViewOrder
             ", CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetVipDetail
        /// <summary>
        ///通过ID获取VIP明细
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetVipDetail(string ID)
        {
            StringBuilder strbSql = new StringBuilder();
            string filedStr = GetFiledStr();
            if (filedStr == null || filedStr == "")
            {
                return null;
            }
            strbSql.AppendFormat(@" SELECT " + filedStr +
            @"from Vip as Vip");
            strbSql.AppendFormat(GetLeftOption().ToString());

            strbSql.AppendFormat("WHERE Vip.ClientID='{0}' AND Vip.isDelete='0'", CurrentUserInfo.ClientID);
            strbSql.AppendFormat("WHERE Vip.VIPID='" + ID + "'");

            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetFiledStr
        /// <summary>
        /// 获取所有文件的表头
        /// </summary>
        /// <returns></returns>
        public string GetFiledStr()
        {
            DataSet ds = GetModuleColumn();
            StringBuilder fileSql = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        int cType = Convert.ToInt32(item["ControlType"].ToString());
                        string ColumnName = item["ColumnName"].ToString();
                        string ColumnDesc=item["ColumnDesc"].ToString();
                        switch (cType)
                        {
                            case 6:
                                fileSql.AppendLine(string.Format("Op_{0}.OptionText as {1}", ColumnName, ColumnDesc));
                                fileSql.Append(",");
                                break;
                            default:
                                fileSql.AppendLine(string.Format("Vip.{0} as {1} ",ColumnName, ColumnDesc));
                                fileSql.Append(",");
                                break;
                        }

                    }
                }
            }
            return fileSql.ToString().Trim(',');
        }
        #endregion

        #region GetLeftOption
        /// <summary>
        /// 获取左连接字段
        /// </summary>
        /// <returns></returns>
        public string GetLeftOption()
        {
            StringBuilder strbSql = new StringBuilder();
            DataSet ds = GetModuleColumn();
            StringBuilder fileSql = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        int cType = Convert.ToInt32(item["ControlType"].ToString());
                        string ColumnName = item["ColumnName"].ToString();
                        string ColumnDesc = item["ColumnDesc"].ToString();
                        string CorrelationValue = item["CorrelationValue"].ToString();
                        switch (cType)
                        {
                            case 6:
                                strbSql.AppendFormat(@" left join Options as Op_{0} on Op_{0}.OptionValue={0} AND  Op_{0}.OptionName='{1}' and Vip.ClientID=Op_{0}.ClientID and Op_{0}.IsDelete='0'  ", ColumnName, CorrelationValue);
                                break;
                        }
                    }
                }
            }
//            strbSql.Append(@" left join Options as Op on Op.OptionValue=Vip.Col1
//                              AND Op.OptionName='VipSchool'");
            return strbSql.ToString();
        }
        #endregion

        #region GetPubGridVipSerch
        public string GetPubGridVipSerch(List<DefindControlEntity> pSerch)
        {
            StringBuilder strbSerch = new StringBuilder();

            if (pSerch != null)
            {
                foreach (DefindControlEntity item in pSerch)
                {
                    string ControlName = item.ControlName;
                    string ControlValue = item.ControlValue;
                    switch (item.ControlType)
                    {
                        case 1://字符串
                            strbSerch.Append(string.Format(" and Op.{0} like '%{1}%'", ControlName, ControlValue));
                            break;
                        case 7:
                            if (ControlValue.Length == 2)
                            {
                                strbSerch.AppendLine(string.Format(" and left(Op.{0},2) = '{1}' ", ControlName, ControlValue));
                            }
                            else if (ControlValue.Length == 4)
                            {
                                strbSerch.AppendLine(string.Format(" and left(Op.{0},4) = '{1}' ", ControlName, ControlValue));
                            }
                            else if (ControlValue.Length == 6)
                            {
                                strbSerch.AppendLine(string.Format(" and left(Op.{0},6) = '{1}' ", ControlName, ControlValue));
                            }
                            break;
                        default://2整型//3数字//4日期//
                            strbSerch.Append(string.Format("and OP.{0}='{1}'", ControlName, ControlValue));
                            break;

                    }

                }
            }
            return strbSerch.ToString();
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 获取VIP所有状态
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipStatus()
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(" SELECT OptionValue,OptionText FROM Options WHERE ClientID='{0}' AND OptionName='VipStatus'", CurrentUserInfo.ClientID));
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
	    #endregion

        #region VipApprove
        /// <summary>
        ///审核
        /// </summary>
        /// <param name="VipId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int VipApprove(string VipId, string Status)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@" Update Vip set Status='"+Status+"' where VIPID='"+VipId+"'");
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());

        }


        #endregion

        #region NoVipApprove
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="VipId"></param>
        /// <param name="Remarke"></param>
        /// <returns></returns>
        public int NoVipApprove(string VipId, string Remark,string Status)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@" Update Vip set Col50='" + Remark + "',Status='"+Status+"' where VIPID='" + VipId + "'");
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());
        }

        #endregion

        #region GetVipStatusNum
        /// <summary>
        /// 获取所有状态值
        /// </summary>
        /// <param name="pSerch"></param>
        /// <returns></returns>
        public DataSet GetVipStatusNum(List<DefindControlEntity> pSerch)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"select isnull(result.Number,0) Number,Op.OptionValue  from Options as Op left join 
            (select count(1) Number,Status,ClientID  from  Vip where ClientID='{0}' and IsDelete=0 group by Status,ClientID) as result on result.Status=Op.OptionValue and result.ClientID=Op.ClientID", CurrentUserInfo.ClientID));
            strbSql.AppendFormat(" where Op.OptionName='VipStatus' AND ( Op.OptionValue in ('11','12','13'))");
            strbSql.AppendLine(string.Format(@"
            select COUNT(*) from Vip
            where ClientID='{0}' and IsDelete=0",CurrentUserInfo.ClientID));
            if (!string.IsNullOrEmpty(GetPubGridVipSerch(pSerch)))
            {
                strbSql.AppendFormat(GetPubGridVipSerch(pSerch));
            }  
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }

        #endregion

        #region GetEmalilFile
        public DataSet GetEmalilFile(string VipId, string ClientID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"select
                 VIPID
                ,VipName
                ,Col14 Hobby
                ,HeadImgUrl
                ,Col50 notApproveReson
                ,vip.Status
                ,opt.OptionTextEn
                ,PageIndex=case when ISNULL(mpb.Sort,0)=0 then '' else mpb.Sort + 1 end
            from vip
            left join Options opt on vip.Status=opt.OptionValue and opt.OptionName='VipStatus' and opt.ClientID=vip.ClientID and opt.IsDelete=vip.IsDelete
            left join MobilePageBlock mpb on mpb.CustomerID=vip.ClientID and mpb.IsDelete=vip.IsDelete and mpb.Remark=CAST(vip.[Status] as nvarchar(200))
            where vip.VIPID='{1}' and vip.ClientID='{0}' and vip.IsDelete=0", VipId, ClientID));
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());

        }
        #endregion

        #region 获取定义
        /// <summary>
        /// 获取表格的列定义
        /// </summary>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumns()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" 
            select
            *
            from MobileBussinessDefined
            where ISNULL(ViewOrder,0)!=0 and IsDelete=0 and CustomerID='{0}' and TableName='{1}' and TypeId=1
            order by ViewOrder asc", CurrentUserInfo.ClientID, _pTableName);
            DataTable dt = this.GetDefind(sql.ToString()).Tables[0];
            List<GridColumnEntity> l = new List<GridColumnEntity>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GridColumnEntity m = new GridColumnEntity();
                m.ColumnText = Convert.ToString(dt.Rows[i]["ColumnDesc"]);
                m.DataIndex = Convert.ToString(dt.Rows[i]["ColumnName"]);
                m.ColumnControlType = Convert.ToInt32(dt.Rows[i]["ControlType"]);
                if (dt.Rows[i]["CorrelationValue"] != DBNull.Value)
                {
                    m.CorrelationValue = Convert.ToString(dt.Rows[i]["CorrelationValue"]);
                }
                l.Add(m);
            }
            return l;
        }

        /// <summary>
        /// 得到配置生成Grid的数据源数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet GetDefind(string pSql)
        {
            return this.SQLHelper.ExecuteDataset(pSql);
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
                    case 10: //密码
                        fieldSQL.AppendLine(string.Format("main.{0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 9: //富文本
                        fieldSQL.AppendLine(string.Format("main.{0} ", ColumnName));
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
                        leftJoinSql.AppendLine(string.Format("left join options T_{0} on  T_{0}.OptionName='{1}' and isnull( T_{0}.ClientID,'{2}')='{2}' and T_{0}.OptionValue=main.{0}", ColumnName, CorrelationValue, CurrentUserInfo.ClientID));
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
                        if (!string.IsNullOrEmpty(ControlValue))
                        {
                            searchSQL.AppendLine(string.Format(" and main.{0} like '%{1}%'", ControlName, ControlValue));
                        }
                        break;
                    case 2: //整型文本
                        if (!string.IsNullOrEmpty(ControlValue))
                        {
                            searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));   
                        }
                        break;
                    case 3: //数字文本
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                    case 4: //日期
                        searchSQL.AppendLine(string.Format(" and main.{0} = '{1}'", ControlName, ControlValue));
                        break;
                }
            }
            return searchSQL;
        }

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
            DROP TABLE #outTemp", pPageIndex, pPageSize));
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
            if (pLsort != null)
                for (int i = 0; i < pLsort.Count; i++)
                {
                    if (pLsort[i].SortType == 0)
                        sql.AppendLine(string.Format("'main.{0} asc", pLsort[i].SortName));
                    else
                        sql.AppendLine(string.Format("'main.{0} desc", pLsort[i].SortName));
                    sql.Append(",");

                }
            return sql;
        }
        #endregion

        #region 会员列表公共查询的字段
        /// <summary>
        /// 返回会员需要特殊处理的字段SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPGridFildSQL()
        {
            return this.GetPubGridFildSQL();
        }
        #endregion

        #region 左连接语句 left join
        #region 会员管理及列表公共左联接的语句
        /// <summary>
        /// 返回会员需要联接的SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPLeftGridJoinSQL()
        {
            return this.GetPubLeftGridJoinSQL();
        }
        #endregion
        
        #endregion

        #region 条件语句 包括inner join 和 where 后条件语句

        #region 用于会员查询一些公共的条件语句
        /// <summary>
        /// 返回会员要处理的条件语句
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStroeGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = this.GetPubGridSearchSQL(pSearch);
            return searchSQL;
        }
        #endregion

        #region 用于会员查询时，递归查询的条件语句，这些数据一般生成表于主表inner join
        /// <summary>
        /// 返回会员特殊查询连接SQL
        /// </summary>
        /// <param name="pSearch"></param>
        /// <returns></returns>
        public StringBuilder GetVIPSearchJoinSQL(List<DefindControlEntity> pSearch)
        {
            return this.GetPubSearchJoinSQL(pSearch);
        }
        #endregion

        #endregion

        #region GetEditControls
        /// <summary>
        /// 获取编辑页面的控件 
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditControls()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select * 
                                from MobileBussinessDefined 
                                where isnull(EditOrder,0)!=0  and isdelete=0 and CustomerID='{0}' and TableName='{1}' and TypeID=1
                                order by  EditOrder asc", CurrentUserInfo.ClientID, _pTableName);
            DataTable dt = this.GetDefind(sql.ToString()).Tables[0];
            return GetDefindControls(dt);
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
                {
                    //控件类型
                    m.ControlType = Convert.ToInt32(dt.Rows[i]["ControlType"]); ;
                }
                
                /// <summary>
                /// 查询控件的Label
                /// </summary>
                //1中文 0英文
                if (dt.Rows[i]["ColumnDesc"] != DBNull.Value)
                {
                    m.fieldLabel = Convert.ToString(dt.Rows[i]["ColumnDesc"]);
                }
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
                l.Add(m);
            }
            return l;
        }
        #endregion

        #region GetEditData
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetEditFildSQL()); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from VIP main");
            sql.Append(GetLeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID='{0}' and CONVERT(varchar(100),main.VIPID) in (select value from dbo.fnSplitStr('{1}',','))", CurrentUserInfo.ClientID, pKeyValue));
            DataTable db = this.GetData(sql.ToString());
            List<DefindControlEntity> l = this.GetEditControls();
            for (var i = 0; i < l.Count; i++)
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][l[i].ControlName] != DBNull.Value)
                    {
                        l[i].ControlValue = db.Rows[0][l[i].ControlName].ToString();
                    }
                }
            }
            return l;
        }
        #endregion

        /// <summary>
        /// 编辑页面获取数据SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetEditFildSQL()
        {
            var pColumnDefind = this.GetGridColumns();
            StringBuilder fieldSQL = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {

                    case 23: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.Value {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 25: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.DistributorID {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 1014: break; //创建开始时间
                    case 1015: break; //创建结束时间
                    case 1017:
                        fieldSQL.AppendLine(string.Format("SBT.{0} {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    default:
                        fieldSQL.AppendLine(string.Format("main.{0} {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                }
            }
            return fieldSQL;
        }

        /// <summary>
        /// 编辑页的连接Join
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetLeftEditViewJoinSQL()
        {
            StringBuilder leftJoinSql = new StringBuilder();
            //leftJoinSql.AppendLine("left join StructureByTran SBT  on  SBT.ClientStructureID=main.ClientStructureID");
            var pColumnDefind = this.GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 23: //职位点选用户
                        leftJoinSql.AppendLine(string.Format("left join #{0} T_{0} on  T_{0}.StoreID=main.StoreID", ColumnName));
                        break;
                    case 25: //点选经销商
                        leftJoinSql.AppendLine(string.Format("left join ClientStoreDistributorMapping T_{0} on  T_{0}.StoreID=main.StoreID and T_{0}.IsDelete=0 ", ColumnName));
                        break;
                    case 1014: break; //创建开始时间
                    case 1015: break; //创建结束时间
                }
            }
            return leftJoinSql;
        }

        /// <summary>
        /// 得到配置信息的数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet GetGridData(string pSql)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var dataes = this.SQLHelper.ExecuteDataset(pSql);
            stopwatch.Stop();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("执行会员耗时:{0} ClientID={1} ClientUserID={2}", stopwatch.ElapsedMilliseconds, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.UserID) });
            return dataes;

        }

        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetData(string sql)
        {
            return this.GetGridData(sql).Tables[0];
        }

        #region GetUserList
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VipViewEntity> GetUserList(Dictionary<string, string> pParems, int pageIndex, int pageSize)
        {
            #region 接收参数
            string strWhere = "";
            //门店
            if (pParems.ContainsKey("pVipName"))
            {
                strWhere += string.Format(" and b.VipName like '%{0}%'", pParems["pVipName"]);
            }
            //活动ID
            if (pParems.ContainsKey("pEventID"))
            {
                strWhere += string.Format(" and a.EventID = '{0}'", pParems["pEventID"]);
            }
            #endregion

            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
                EventID
                ,VipID
                ,SignUpID
	            into #tmp
            from LEventSignUp
            where IsDelete=0
            order by EventID

            select
	            b.*
                ,a.SignUpID
	            ,ROW_NUMBER() over(order by b.createtime) ID
	            ,opt.OptionText VipSchool
	            ,opt1.OptionText VipClass
	            into #result
            from #tmp a
            inner join Vip b on a.VIPID=b.VIPID and b.IsDelete=0
            left join Options opt on opt.OptionName='VipSchool' and b.Col1=opt.OptionValue and opt.ClientID=b.ClientID and opt.IsDelete=0
            left join Options opt1 on opt1.OptionName='VipClass' and b.Col3=opt1.OptionValue and opt1.ClientID=b.ClientID and opt1.IsDelete=0
            where b.ClientID='{0}' {1}

            select
                * 
            from  #result
            where ID between {2} and {3}
            select count(*) from #result
            drop table #tmp
            drop table #result", CurrentUserInfo.ClientID, strWhere, (pageSize * (pageIndex - 1)) + 1, pageSize * pageIndex);
            #endregion

            DataSet dsSource = this.SQLHelper.ExecuteDataset(strSql.ToString());
            PagedQueryResult<VipViewEntity> pageQuery = new PagedQueryResult<VipViewEntity>();
            pageQuery.Entities = ConvertHelper<VipViewEntity>.ConvertToList(dsSource.Tables[0]).ToArray();
            int pageCount = 0;
            int.TryParse(dsSource.Tables[1].Rows[0][0] + "", out pageCount);
            pageQuery.PageCount = pageCount;
            return pageQuery;
        }
        #endregion

        #region ExportUserList
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet ExportUserList(Dictionary<string, string> pParems)
        {
            #region 接收参数
            string strWhere = "";
            //门店
            if (pParems.ContainsKey("pVipName"))
            {
                strWhere += string.Format(" and b.VipName like '%{0}%'", pParems["pVipName"]);
            }
            //活动ID
            if (pParems.ContainsKey("pEventID"))
            {
                strWhere += string.Format(" and a.EventID = '{0}'", pParems["pEventID"]);
            }
            #endregion

            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            EventID,VipID
	            into #tmp
            from LEventSignUp
            where IsDelete=0
            order by EventID

            select
	            b.VipName '姓名'
	            ,b.Phone '手机'
	            ,b.Email '邮箱'
	            ,b.Col5 '公司'
	            ,b.Col6 '职位'
	            ,opt.OptionText '学校'
	            ,b.Col2 '期/班级'
	            ,opt1.OptionText '班级'
            from #tmp a
            inner join Vip b on a.VIPID=b.VIPID and b.IsDelete=0
            left join Options opt on opt.OptionName='VipSchool' and b.Col1=opt.OptionValue and opt.ClientID=b.ClientID and opt.IsDelete=0
            left join Options opt1 on opt1.OptionName='VipClass' and b.Col3=opt1.OptionValue and opt1.ClientID=b.ClientID and opt1.IsDelete=0
            where b.ClientID='{0}' {1}

            drop table #tmp", CurrentUserInfo.ClientID, strWhere);
            #endregion

            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region DeleteEventVipMapping
        /// <summary>
        /// 删除重复报名人员数据
        /// </summary>
        /// <param name="pID"></param>
        public void DeleteEventVipMapping(string pID)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            update LEventSignUp
                set IsDelete=1,LastUpdateTime=GETDATE()
            where SignUpID='{0}'",pID);
            this.SQLHelper.ExecuteNonQuery(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetUpdateSql
        /// <summary>
        /// 获取得到修改的SQL
        /// </summary>
        /// <param name="pLrm">非特殊的业务数据</param>
        /// <returns>SQL</returns>
        public StringBuilder GetUpdateSql(List<DefindControlEntity> pLrmNull, List<DefindControlEntity> pLrm, string pKeyName, string pKeyValue)
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
            sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", CurrentUserInfo.UserID));
            sql.AppendLine(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
            //再修改有值的数据
            sql.AppendLine(string.Format("update {0} ", _pTableName));
            sql.AppendLine("set ");
            for (var i = 0; i < pLrm.Count; i++)
            {
                sql.AppendLine(string.Format("{0}='{1}'", pLrm[i].ControlName, pLrm[i].ControlValue));
                sql.Append(",");
            }
            sql.AppendLine(string.Format("LastUpdateTime=getdate(),LastUpdateBy='{0}'", CurrentUserInfo.UserID));
            sql.Append(string.Format("where {0}='{1}'", pKeyName, pKeyValue));
            return sql;
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 23 && i.ControlType != 25).ToList();
            var pall = this.GetEditControls();
            var p = pall.ToArray().Where(i => i.ControlType != 23 && i.ControlType != 25).ToList();
            StringBuilder sql = this.GetUpdateSql(p, plm, "VIPID", pKeyValue);
            bool res = this.ICRUDable(sql.ToString());
            return res;
        }
        #endregion

        #region 增删改操作
        public bool ICRUDable(string pSql)
        {
            bool res = false;
            TransactionHelper tranHelper = new TransactionHelper(CurrentUserInfo);
            IDbTransaction tran = tranHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this.ICRUDable(pSql, tran);

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

        public void ICRUDable(string pSql, IDbTransaction pTran)
        {
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, pSql);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, pSql);
            return;

        }
        #endregion
    }
}
