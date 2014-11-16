using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// XieHuiBaoDAO
    /// </summary>
    public partial class XieHuiBaoDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO
    {
        protected string _pTableName = "vip";

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public XieHuiBaoDAO(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo)
        {
            this._pTableName = pTableName;
        }
        #endregion

        #region EMBA
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
                        string ColumnDesc = item["ColumnDesc"].ToString();
                        switch (cType)
                        {
                            case 6:
                                fileSql.AppendLine(string.Format("Op_{0}.OptionText as {1}", ColumnName, ColumnDesc));
                                fileSql.Append(",");
                                break;
                            default:
                                fileSql.AppendLine(string.Format("Vip.{0} as {1} ", ColumnName, ColumnDesc));
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


        //jifeng.cao(20140604)
        public string GetPubGridVipSerch_Cjf(List<DefindControlEntity> pSerch)
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
                            if (ControlName == "VipName")
                            {
                                if (!string.IsNullOrEmpty(ControlValue))
                                {
                                    strbSerch.Append(string.Format(" and {0} like '%{1}%'", ControlName, ControlValue));
                                }
                            }
                            else
                            {
                                strbSerch.Append(string.Format(" and Op.{0} like '%{1}%'", ControlName, ControlValue));
                            }
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
                            if (ControlName == "Status")
                            {
                                if (!string.IsNullOrEmpty(ControlValue))
                                {
                                    strbSerch.Append(string.Format("and {0}='{1}'", ControlName, ControlValue));
                                }
                            }
                            else
                            {
                                strbSerch.Append(string.Format("and OP.{0}='{1}'", ControlName, ControlValue));
                            }
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
            strbSql.AppendFormat(@" Update Vip set Status='" + Status + "' where VIPID='" + VipId + "'");
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
        public int NoVipApprove(string VipId, string Remark, string Status)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@" Update Vip set Col50='" + Remark + "',Status='" + Status + "' where VIPID='" + VipId + "'");
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
            select COUNT(*) from Vip main 
inner join options t_status on t_status.OptionName='VipStatus' and isnull( t_status.ClientID,'{0}')='{0}' and t_status.OptionValue=main.status and t_status.IsDelete=0 
             Where main.IsDelete=0 and main.ClientID='{0}'", CurrentUserInfo.ClientID));
            if (!string.IsNullOrEmpty(GetPubGridVipSerch_Cjf(pSerch)))
            {
                strbSql.AppendFormat(GetPubGridVipSerch_Cjf(pSerch));
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
        /// <param name="pType">类型 1：注册 2：活动</param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumns(int? pType)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" 
            select
            *
            from MobileBussinessDefined
            where ISNULL(ViewOrder,0)!=0 and IsDelete=0 and CustomerID='{0}' and TableName='{1}' and TypeId={2}
            order by ViewOrder asc", CurrentUserInfo.ClientID, _pTableName, pType);
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
        public List<GridColumnModelEntity> GetGridDataModels(int? pType)
        {
            List<GridColumnEntity> lc = GetGridColumns(pType);
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

        #region 获取表的定义,2014-04-21 修改者:tiansheng.zhu
        /// <summary>
        /// 获取表格的列定义
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumnsByEventID(string pEventID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" 
             select  mbd.* 
                     from MobileModuleObjectMapping as mmom
                     left join MobilePageBlock as mpb
                     on mpb.MobileModuleID=mmom.MobileModuleID
                     and mpb.CustomerID=mmom.CustomerID
                     and mpb.IsDelete=mmom.IsDelete
                     left join MobileBussinessDefined as mbd
                     on mbd.MobilePageBlockID=mpb.MobilePageBlockID 
                     and mbd.IsDelete=mpb.IsDelete
                     and mbd.CustomerID=mpb.CustomerID 
                     where mmom.ObjectID='{1}' 
                     and mmom.CustomerID='{0}'
                     and mmom.IsDelete=0
                     and mbd.EditOrder>0
                     order by mpb.Sort,mbd.EditOrder", CurrentUserInfo.ClientID, pEventID);
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
        /// 获取表格数据模型
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModelsByEventID(string pEventID)
        {
            List<GridColumnEntity> lc = GetGridColumnsByEventID(pEventID);
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
        /// 根据活动返回字段SQL
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public StringBuilder GetPubGridFildSQLByEventID(string pEventID)
        {
            List<GridColumnEntity> pColumnDefind = GetGridColumnsByEventID(pEventID);
            StringBuilder fieldSQL = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 1: //字符串文本
                        if (ColumnName.ToString().ToLower() == "vipname")
                        {
                            ColumnName = "UserName as vipname";
                        }
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
        public StringBuilder GetPubLeftGridJoinSQLByEventID(string pEventID)
        {
            StringBuilder leftJoinSql = new StringBuilder();
            List<GridColumnEntity> pColumnDefind = GetGridColumnsByEventID(pEventID);
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
        #endregion

        #region SQL拼接
        /// <summary>
        /// 返回字段SQL
        /// </summary>
        /// <param name="pColumnDefind"></param>
        /// <returns></returns>
        public StringBuilder GetPubGridFildSQL(int? pType)
        {
            List<GridColumnEntity> pColumnDefind = GetGridColumns(pType);
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
                    default:
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
        public StringBuilder GetPubLeftGridJoinSQL(int? pType)
        {
            StringBuilder leftJoinSql = new StringBuilder();
            List<GridColumnEntity> pColumnDefind = GetGridColumns(pType);
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
            if (pSearch != null)
            {
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
            if (pSearch != null)
            {
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
        public StringBuilder GetVIPGridFildSQL(int? pType)
        {
            return this.GetPubGridFildSQL(pType);
        }
        #endregion

        #region 左连接语句 left join
        #region 会员管理及列表公共左联接的语句
        /// <summary>
        /// 返回会员需要联接的SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPLeftGridJoinSQL(int? pType)
        {
            return this.GetPubLeftGridJoinSQL(pType);
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
        public List<DefindControlEntity> GetEditData(string pKeyValue, int? pType)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetEditFildSQL(pType)); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from VIP main");
            sql.Append(GetLeftEditViewJoinSQL(pType)); //获取联接SQL
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

        #region GetEditFildSQL
        /// <summary>
        /// 编辑页面获取数据SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetEditFildSQL(int? pType)
        {
            var pColumnDefind = this.GetGridColumns(pType);
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
        #endregion

        #region GetLeftEditViewJoinSQL
        /// <summary>
        /// 编辑页的连接Join
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetLeftEditViewJoinSQL(int? pType)
        {
            StringBuilder leftJoinSql = new StringBuilder();
            //leftJoinSql.AppendLine("left join StructureByTran SBT  on  SBT.ClientStructureID=main.ClientStructureID");
            var pColumnDefind = this.GetGridColumns(pType);
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
        #endregion

        #region GetGridData
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
        #endregion

        #region GetData
        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetData(string sql)
        {
            return this.GetGridData(sql).Tables[0];
        }
        #endregion

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
                ,SignUpID,CreateTime
	            into #tmp
            from LEventSignUp
            where IsDelete=0
            order by EventID

            select
	            b.*
                ,a.SignUpID
	            ,ROW_NUMBER() over(order by a.createtime) ID
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
	            EventID,VipID,CreateTime
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
                ,convert(date,a.CreateTime) '报名时间'               
            from #tmp a
            inner join Vip b on a.VIPID=b.VIPID and b.IsDelete=0
            left join Options opt on opt.OptionName='VipSchool' and b.Col1=opt.OptionValue and opt.ClientID=b.ClientID and opt.IsDelete=0
            left join Options opt1 on opt1.OptionName='VipClass' and b.Col3=opt1.OptionValue and opt1.ClientID=b.ClientID and opt1.IsDelete=0
            where b.ClientID='{0}' {1}
            order by a.CreateTime
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
                set IsDelete=1,LastUpdateTime=GETDATE(),lastUpdateBy='{1}'
            where SignUpID='{0}'", pID, this.CurrentUserInfo.UserID);
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
        #endregion

        #region CEIBS
        #region EventStatsPageData
        /// <summary>
        /// 最受关注获取信息
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="ObjectID"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public DataSet EventStatsPageData(string Type, string ObjectID, int? pPageSize, int? pPageIndex)
        {

            #region 拼接SQL
            StringBuilder strb = new StringBuilder();
            strb.Append(string.Format(@"
                 select EventStatsID,ObjectID,ObjectType,Op.OptionText as Type_Name,BrowseNum,PraiseNum,BookMarkNum
                  ,Ev.Sequence,ShareNum,ROW_NUMBER() Over (Order By Ev.Sequence ASC)ROW_NUMBER,Oi.Title
                  into #eventStatsTemp from Eventstats as Ev
                 ")
                );
            strb.AppendLine(string.Format(@" inner join Options as Op on Op.ClientID=Ev.CustomerID AND Op.OptionName='EventsStat' AND Op.OptionValue=Ev.ObjectType "));
            strb.AppendLine(string.Format(@" left join 
                                  ( select NewsID,Title from 
                                  (   
                                   select 
                                 AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                 
                from LEventsAlbum
                where IsDelete=0 
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                   
                from LEvents
                where IsDelete=0  
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    
                from LNews
                where IsDelete=0 ) Oi) as Oi on Oi.NewsID=Ev.ObjectID
             "));
            strb.AppendFormat("where Ev.CustomerID='{0}' AND Ev.IsDelete='0' ", CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(Type))
            {
                strb.AppendLine(string.Format(@" AND Ev.ObjectType='{0}'", Type));
            }
            if (!string.IsNullOrEmpty(ObjectID))
            {
                strb.AppendLine(string.Format(@" AND Ev.ObjectID='{0}'", ObjectID));
            }
            strb.AppendLine(GetEvPageSQL(pPageSize, pPageIndex).ToString());
            #endregion

            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region GetEvPageSQL
        /// <summary>
        /// 返回分页SQL
        /// </summary>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public StringBuilder GetEvPageSQL(int? pPageSize, int? pPageIndex)
        {
            StringBuilder pageSql = new StringBuilder();
            pageSql.AppendLine(string.Format(@"
            declare @PageIndex int ={0}
            declare @PageSize int={1}
            declare @PageCount int
            declare @RowsCount int
            declare @PageStart int
            declare @PageEnd int
            SELECT @RowsCount=COUNT(1) FROM #eventStatsTemp
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
            SELECT * FROM #eventStatsTemp WHERE ROW_NUMBER between  @PageStart+1 and @PageEnd
            SELECT @RowsCount RowsCount,@PageCount PageCount
            DROP TABLE #eventStatsTemp", pPageIndex, pPageSize));
            return pageSql;
        }
        #endregion

        #region DelEventStats
        /// <summary>
        /// 删除最受关注信息
        /// </summary>
        /// <param name="EventStatsID"></param>
        /// <returns></returns>
        public int DelEventStats(string eventStatsID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(
                string.Format(@"
               declare @ObjectID nvarchar
               select @ObjectID=ObjectID from EventStats where EventStatsID='{0}'
               update Eventstats set IsDelete='1' where EventStatsID='{0}'
               update EventStatsDetail set IsDelete='1' where ObjectID=@ObjectID and CustomerID='{1}'
            ", eventStatsID, CurrentUserInfo.ClientID));
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());
        }
        #endregion

        #region SaveEventStats
        /// <summary>
        /// 保存最受关注信息
        /// </summary>
        /// <param name="eventStatsID">主键id</param>
        /// <param name="objectType">类型</param>
        /// <param name="objectID">类型id</param>
        /// <param name="browseNum">浏览数</param>
        /// <param name="praiseNum">点赞书</param>
        /// <param name="bookMarkNum">收藏数</param>
        /// <param name="sequence">排序</param>
        /// <returns>影响行数</returns>
        public int SaveEventStats(string eventStatsID, string objectType, string objectID, string browseNum, string praiseNum, string bookMarkNum, string sequence)
        {
            StringBuilder strbSql = new StringBuilder();
            if (!string.IsNullOrEmpty(eventStatsID))
            {
                strbSql.AppendFormat(@" declare @rowcount int 
                                        select @rowcount=count(1) from Eventstats where ObjectID='{0}' and CustomerID='{1}' and IsDelete='0' and EventStatsID!='{2}'
                                        ", objectID, CurrentUserInfo.ClientID, eventStatsID);
                strbSql.AppendFormat(@" if @rowcount<=0
                                          begin ");

                strbSql.Append(string.Format(
                    @"
                     update Eventstats set ObjectID='{1}',ObjectType='{2}',Sequence='{3}' where EventStatsID='{0}' 
                    "
                    , eventStatsID, objectID, objectType, sequence));
                strbSql.AppendFormat(" end ");

            }
            else
            {
                if (!string.IsNullOrEmpty(sequence))
                {
                    sequence = "1000";
                }
                int ShareNum = 0;
                strbSql.AppendLine(string.Format(@"declare @rowcount int"));
                strbSql.Append(string.Format(@"select @rowcount=count(1) from Eventstats where CustomerID='{0}' AND ObjectID='{1}' and IsDelete='0'
                                     if @rowcount<=0 ", CurrentUserInfo.ClientID, objectID));
                strbSql.AppendFormat(" begin ");
                strbSql.AppendLine(string.Format(@" declare @GUID uniqueidentifier"));
                strbSql.AppendLine(string.Format(@" set @GUID=NEWID()"));
                strbSql.AppendLine(string.Format(@" insert into Eventstats(EventStatsID,ObjectID,ObjectType,BrowseNum,PraiseNum,BookmarkNum,Sequence,CustomerID,ShareNum)
                                   Values(@GUID,'{1}','{2}',{3},'{4}','{5}','{6}','{7}','0')",
                                   eventStatsID, objectID, objectType, browseNum, praiseNum, bookMarkNum, sequence, CurrentUserInfo.ClientID, ShareNum));
                strbSql.AppendFormat("end");
            }
            return this.SQLHelper.ExecuteNonQuery(strbSql.ToString());
        }

        #endregion

        #region GetEventStatsType
        /// <summary>
        /// 获取option中最受关注的单据类型
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStatsType()
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat(@"
            select OptionValue,OptionText 
              from Options
              where
              OptionName='{0}'
              AND ClientID='{1}'
            ", "EventsStat", CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetOptionID
        /// <summary>
        /// 根据Option单据类型获取单据信息
        /// </summary>
        /// <param name="ObjectType">option类型</param>
        /// <returns></returns>
        public DataSet GetOptionID(string objectType)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"
 select * from 
   (   
      select 
             AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                 
                from LEventsAlbum
                where IsDelete=0 And CustomerId='{0}'
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                   
                from LEvents
                where IsDelete=0  And CustomerId='{0}'
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    
                from LNews
                where IsDelete=0  And CustomerId='{0}' )Oi
               where NewsType='{1}'

         ", CurrentUserInfo.ClientID, objectType));
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetEventStatsDetail
        /// <summary>
        /// 根据Id获取最受关注明细信息
        /// </summary>
        /// <param name="eventStatsID"></param>
        /// <returns></returns>
        public DataSet GetEventStatsDetail(string eventStatsID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.AppendFormat("select ObjectType,ObjectID,Sequence from Eventstats where EventStatsID='{0}'", eventStatsID);
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetAlbumList(string pVipID, int pageSize, int pageIndex)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            --获取视频列表
            declare @pVipID nvarchar(200)='{0}'
            declare @pClientID nvarchar(200)='{1}'
            select
            *,
            BrowseCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=1 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            PraiseCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=2 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            KeepCount=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=3 and news.NewsID=AlbumId and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            IsPraise=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=2 and news.NewsID=AlbumId and news.VipID=@pVipID and news.CustomerID=@pClientID and news.IsDelete=0
            ),
            IsKeep=(
                select
                COUNT(*)
                from NewsCount news
                where news.CountType=3 and news.NewsID=AlbumId and news.VipID=@pVipID and news.CustomerID=@pClientID and news.IsDelete=0
            )
            ,ROW_NUMBER() OVER(ORDER BY SortOrder) AS rownumber
            into #tmp
            from LEventsAlbum
            where [Type]=2 and IsDelete=0 and customerid='{1}'
            
            select * from #tmp where rownumber>{2} and rownumber<={3}
            select count(1) from #tmp

            drop table #tmp", pVipID, CurrentUserInfo.ClientID, (pageIndex - 1) * pageSize, pageIndex * pageSize);
            #endregion
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region AddEventStats
        /// <summary>
        /// 浏览 收藏数据的操作
        /// </summary>
        /// <param name="pNewsID">数据ID</param>
        /// <param name="pVipID">用户ID</param>
        /// <param name="pCountType">操作类型 <1.BrowseCount(浏览数量) 2.PraiseCount(赞的数量) 3.KeepCount(收藏数量)> </param>
        /// <param name="pNewsType">数据类型 <1.咨询 2.视频 3.活动></param>
        /// <returns></returns>
        public int AddEventStats(string pNewsID, string pVipID, string pCountType, string pObjectType)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(pVipID))
            {
                strSql.AppendFormat(@"
                declare @pCountType int = {4}
                declare @pNewsType int = {3} --1.资讯 2.视频 3.活动
                declare @pVipID nvarchar(200) = '{2}'
                declare @pNewsID nvarchar(200) = '{1}'
                declare @pClientID nvarchar(200) = '{0}'
                declare @rowcount int
                declare @objectId nvarchar(50) 
                select @rowcount=count(1) from EventStatsDetail where VipID='{2}' AND IsDelete='0'  AND ObjectID='{1}' and CountType='{4}'
             
                 declare @Number int 
                 select @Number=count(1) from EventStats where ObjectID='{1}' 

                if @pCountType=1
                       begin
                           update EventStats set BrowseNum=BrowseNum+1 where ObjectID='{1}' 
                       end
                else if @pCountType=2 
                    begin
                        if @rowcount<=0
                            begin 
                                update EventStats set PraiseNum=PraiseNum+1 where ObjectID='{1}'
                            end
                        else 
                            begin
                                update EventStats set PraiseNum=PraiseNum-1 where ObjectID='{1}'  
                            end 
                       end
                 
                else if @pCountType=3
                    begin
                        update EventStats set BookmarkNum=BookmarkNum+1 where ObjectID='{1}' 
                       end
                else if  @pCountType=4
                     begin
                        update EventStats set ShareNum=isnull(ShareNum,0)+1 where ObjectID='{1}' 
                       end
                if @rowcount<=0
                    begin
                        insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,CreateTime,IsDelete)
                        values('" + Guid.NewGuid().ToString() + @"','{1}','{3}','{4}','{2}','{0}','{2}','{5}','0')
                    end
                else
                    begin
                        update EventStatsDetail set IsDelete='1' where VipID='{2}' AND CountType='{4}'  AND ObjectID='{1}'
                    end", CurrentUserInfo.ClientID, pNewsID, pVipID, pObjectType, pCountType, DateTime.Now.ToString());
            }
            else
            {
                strSql.AppendFormat(@"
                declare @pCountType int = {4}
                declare @pNewsType int = {3} -- 1.资讯 2.视频 3.活动
                declare @pVipID nvarchar(200) = '{2}'
                declare @pNewsID nvarchar(200) = '{1}'
                declare @pClientID nvarchar(200) = '{0}'
                declare @rowcount int
                declare @objectId nvarchar(50) 
                select @objectId=ObjectID from EventStats where ObjectID='{1}' 
                declare @Number int 
                select @Number=count(1) from EventStats where ObjectID='{1}'     
                 
                if @pCountType=1
                    begin
                        update EventStats set BrowseNum=BrowseNum+1 where  ObjectID='{1}' 
                    end
                else if @pCountType=2 
                    begin
                        update EventStats set PraiseNum=PraiseNum+1 where ObjectID='{1}' 
                     end
                else if @pCountType=3
                    begin
                        update EventStats set BookmarkNum=BookmarkNum+1 where ObjectID='{1}' 
                    end 
                  else if  @pCountType=4
                     begin
                        update EventStats set ShareNum=isnull(ShareNum,0)+1 where ObjectID='{1}' 
                    end
                insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,CreateTime,IsDelete)
                values('" + Guid.NewGuid().ToString() + @"','{1}','{3}','{4}','{2}','{0}','{2}','{5}','0')", CurrentUserInfo.ClientID, pNewsID, pVipID, pObjectType, pCountType, DateTime.Now.ToString());
            }
            return this.SQLHelper.ExecuteNonQuery(strSql.ToString());
        }
        #endregion

        #region GetEventStats
        /// <summary>
        /// 获取最受关注列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStats(int? pPageSize, int? pPageIndex)
        {
            #region 拼接SQL
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(
            @"select 
                a.NewsID as NewsID
                ,es.EventStatsID as eventStatsID
                ,a.NewsType
                ,a.NewsTypeText
                ,es.BrowseNum as BrowseCount
                ,es.PraiseNum as PraiseCount
                ,es.BookmarkNum as KeepCount
                ,(es.BrowseNum+es.PraiseNum+es.BookmarkNum) as AllCount 
                ,a.Title
                ,ISNULL(a.ImageUrl,'') ImageUrl
                ,ISNULL(a.VideoUrl,'') VideoUrl
                ,ISNULL(a.Intro,'') Intro
                ,ISNULL(a.[Description],'') [Description]
                ,a.CreateTime
                ,datediff(HOUR,a.CreateTime,GETDATE()) AgoTime
                ,isnull(ShareNum,'0') ShareNum
                ,ROW_NUMBER() Over (Order By es.Sequence ASC)ROW_NUMBER"));
            if (CurrentUserInfo.UserID == "1")
            {

                strbSql.AppendFormat(",'false' isPraise");
            }
            else
            {
                strbSql.AppendFormat(",(select case when count(1)>0 then 'true' else 'false' end  from  EventStatsDetail where es.ObjectID=ObjectID and CountType='2' and VipID='{0}') isPraise", CurrentUserInfo.UserID);
            }
            strbSql.Append(string.Format(@" into #eventStatsTemp  from EventStats as es 
            left join  Options as op on op.OptionValue=es.ObjectType  and op.OptionName='EventsStat' and op.ClientID='{0}'
            left join(
                select 
                    AlbumId NewsID
                    ,1 NewsType
                    ,'视频' NewsTypeText
                    ,Title
                    ,ImageUrl
                    ,[Description] VideoUrl
                    ,Intro
                    ,Intro [Description]
                    ,CreateTime
                from LEventsAlbum 
                where IsDelete=0 And CustomerId='{0}'
                union all
                select 
                    EventID NewsID 
                    ,2 NewsType
                    ,'活动' NewsTypeText
                    ,Title
                    ,ImageUrl
                    ,'' VideoUrl
                    , Intro
                    ,[Description]
                    ,CreateTime
                from LEvents
                where IsDelete=0 And CustomerId='{0}' 
                union all
                select 
                    NewsID
                    ,3 NewsType
                    ,'资讯' NewsTypeText
                    ,NewsTitle Title
                    ,ImageUrl
                    ,'' VideoUrl
                    ,Intro
                    ,Content [Description]
                    ,CreateTime
                from LNews
                where IsDelete=0 And CustomerId='{0}'
                union all
                select 
                    item_id NewsID
                    ,4 NewsType
                    ,'商品' NewsTypeText
                    ,item_name Title
                    ,ImageUrl
                    ,'' VideoUrl
                    ,'' Intro
                    ,item_remark [Description]
                    ,Create_Time CreateTime
                from [T_Item]
                where CustomerId='{0}')a 
            on a.NewsID=convert(nvarchar(max),es.ObjectID) where a.NewsType is NOT null  and es.IsDelete='0'", CurrentUserInfo.ClientID));
            strbSql.AppendFormat(GetEvPageSQL(pPageSize, pPageIndex).ToString());
            #endregion
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetEventAlbumList(string pVipID, int pageSize, int pageIndex)
        {
            #region sql
            StringBuilder strb = new StringBuilder();
            strb.Append(string.Format(@"
            select 
                Le.*,Ev.BookmarkNum as KeepCount
                ,Ev.BrowseNum as BrowseCount
                ,Ev.PraiseNum as PraiseCount,
                IsPraise=(
                    select 
                        COUNT(1) 
                    from EventStatsDetail as Ed 
                    where Ed.ObjectID=Le.AlbumId and IsDelete='0' and Ed.CustomerID='{0}' and Ed.VipID='{1}' and  Ed.CountType='2' 
                ),
                IsKeep=(
                select 
                    COUNT(1) 
                from EventStatsDetail as Ed 
                where Ed.ObjectID=Le.AlbumId and Ed.CustomerID='{0}' and Ed.CountType='3' and IsDelete='0' and Ed.VipID='{1}' 
                )
                ,ROW_NUMBER() OVER(ORDER BY SortOrder) AS rownumber
                into #tmp
            from LEventsAlbum as Le
            left join EventStats as Ev on Le.AlbumId=Ev.ObjectID and Ev.IsDelete='0' and Ev.CustomerID='{0}'
            where Le.Type=2 and Le.IsDelete=0 and Le.CustomerID='{0}'", CurrentUserInfo.ClientID, CurrentUserInfo.UserID));
            strb.Append(string.Format(@"select * from #tmp where rownumber>{1} and rownumber<={2}
            select count(1) from #tmp
            drop table #tmp", CurrentUserInfo.ClientID, (pageIndex - 1) * pageSize, pageIndex * pageSize));
            #endregion
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion
       

        #region GetMostDetail
        /// <summary>
        /// 获取点赞数量
        /// </summary>
        /// <param name="pEventsID"></param>
        /// <returns></returns>
        public int GetMostDetail(string pEventsID)
        {
            string str = string.Format("select isnull(PraiseNum,0) PraiseNum from EventStats where ObjectID='{0}'", pEventsID);
            DataSet ds = this.SQLHelper.ExecuteDataset(str);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return (int)ds.Tables[0].Rows[0][0];
                }
            }
            return 0;
        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细 
        /// </summary>
        /// <param name="pCourseType">课程类型<1=MBA 2=EMBA 3=FMBA 4=高级经理课程></param>
        /// <returns></returns>
        public DataSet GetCourseInfo(string pCourseType)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            top 1 CourseId
	            ,CouseDesc --课程介绍
	            ,CourseName --课程名字
	            ,CourseSummary --课程简介
	            ,CourseFee --课程费用
	            ,CourseStartTime
	            ,CouseCapital
	            ,CouseContact
            from ZCourse
            where CourseTypeId={1} and IsDelete=0 and ClientID='{0}'", CurrentUserInfo.ClientID, pCourseType);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(string pVipID)
        {
            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            VIPID
                ,VipName
                ,'' Hobby
                ,HeadImgUrl
                ,Col50 notApproveReson
                ,vip.Status
                ,opt.OptionText
                ,opt.OptionTextEn
                ,PageIndex=case when ISNULL(mpb.Sort,0)=0 then 2 else mpb.Sort end 
            from vip
            left join Options opt on vip.Status=opt.OptionValue and opt.OptionName='VipStatus' and opt.ClientID=vip.ClientID and opt.IsDelete=vip.IsDelete
            left join MobilePageBlock mpb on mpb.CustomerID=vip.ClientID and mpb.IsDelete=vip.IsDelete and mpb.Remark=CAST(vip.[Status] as nvarchar(200))
            where vip.VIPID='{1}' and vip.ClientID='{0}' and vip.IsDelete=0", CurrentUserInfo.ClientID, pVipID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="pVipName"></param>
        /// <param name="pClass"></param>
        /// <param name="pCompany"></param>
        /// <param name="pCity"></param>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public DataSet GetUserList(string pVipName, string pClass, string pCompany, string pCity, string pVipID)
        {
            #region 接收条件
            string strWhere = "";
            if (!string.IsNullOrEmpty(pVipName))
            {
                strWhere += string.Format(" and vip.VipName like '%{0}%'", pVipName);
            }
            if (!string.IsNullOrEmpty(pClass))
            {
                strWhere += string.Format(" and vip.Col3 like '%{0}%'", pClass);
            }
            if (!string.IsNullOrEmpty(pCompany))
            {
                strWhere += string.Format(" and vip.Col5 like '%{0}%'", pCompany);
            }
            if (!string.IsNullOrEmpty(pCompany))
            {
                strWhere += string.Format(" and vip.City like '%{0}%'", pCity);
            }
            #endregion

            #region 组织SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            update Vip 
	            set Col49=ISNULL(Vip.Col49,0)+1
            where VIPID='{2}'

            select
	            VIPID
                ,VipName
                ,Col6 PositionName
                ,HeadImgUrl
                ,'' Hobby
                ,opt.OptionText ClassName
                ,SearchCount=(
		            select
			            ISNULL(vip.Col49,0) SearchCount
		            from Vip
		            where VIPID='{2}'
                )
            from Vip vip
            left join Options opt on vip.Col3=opt.OptionValue and opt.OptionName='VipClass' and opt.ClientID=vip.ClientID and vip.IsDelete=opt.IsDelete
            where vip.ClientID='{0}' and vip.IsDelete=0 {1}", CurrentUserInfo.ClientID, strWhere, pVipID);
            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region getEventstatsDetailByNewsID
        /// <summary>
        /// 根据资讯ID更新浏览量
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet getEventstatsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            StringBuilder strb = new StringBuilder();

            strb.AppendFormat(@"
            update EventStats 
                set  BrowseNum=isnull(BrowseNum,0)+1  
            where ObjectID='{1}' 
            select 
                L.NewsId
                ,L.NewsTitle,L.NewsSubTitle
                ,L.Content,
                convert(nvarchar(10),L.PublishTime,120)as PublishTime
                ,L.ContentUrl,
                L.ImageUrl
                ,L.ThumbnailImageUrl
                ,L.Author
                ,nc.BrowseNum as BrowseCount
                ,nc.PraiseNum as PraiseCount
                ,L.CollCount
                ,nc.ObjectID
            from LNews  as L
            left join EventStats as nc on  nc.ObjectID=l.NewsID
            and nc.IsDelete=l.IsDelete and nc.IsDelete='0'
            and nc.ObjectType='3'
            where l.NewsId='{1}'", CurrentUserInfo.ClientID, pEntity.special.NewsID);

            strb.AppendLine(string.Format(@" 
            insert into EventStatsDetail(DetailID,ObjectID,ObjectType,CountType,VipID,CustomerID,CreateBy,IsDelete)
			values('" + Guid.NewGuid().ToString() + @"','{1}','3','1','{2}','{0}','{2}','0')", CurrentUserInfo.ClientID, pEntity.special.NewsID, pEntity.common.userId));

            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region GetVipPayMent
        /// <summary>
        ///获取用户商品价格
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public DataSet GetVipPayMent(string pVipID)
        {
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(string.Format(@"
             select 
            Vip.VipName as vipName,It.item_id as itemId,It.item_code as itemCode,It.item_name as itemName
            ,item_price as itemPrice,isnull(BeginTime,'2014-01-01')beginTime,isnull(EndTime,'2014-12-31')endTime
            from Vip as Vip 
            inner join VIPRoleMapping as Vpm on Vip.VIPID=Vpm.VipId 
            inner join ItemRoleMapping as Irm on Irm.RoleId=Vpm.RoleId
            inner join T_ITEM_PRICE as Ip on Ip.item_id=Irm.ItemId
            left join T_Item as It on It.item_id=Ip.item_id 
            left join vw_item_detail as vm on vm.item_id=It.item_id 
            where Vip.VIPID='{0}' and Vip.ClientID='{1}'", pVipID, CurrentUserInfo.ClientID));
            return this.SQLHelper.ExecuteDataset(strbSql.ToString());
        }
        #endregion

        #region SubmitVipPayMent
        /// <summary>
        /// 缴会费提交订单
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="ItemId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public int SubmitVipPayMent(string itemId, string vipId, string orderId, decimal price)
        {

            var tran = this.SQLHelper.CreateTransaction();
            int res = 0;

            try
            {
                StringBuilder strb = new StringBuilder();

                strb.AppendLine(@"Insert into VipPayMent(");
                strb.AppendLine(string.Format(@"VipPayMentID,VipID,EventID,CreateBy,IsDelete,Fee)"));
                strb.AppendLine(string.Format(@"Values('{0}','{1}','{2}','{3}','0','{4}')", Guid.NewGuid(), vipId, orderId, CurrentUserInfo.UserID, price));

                res = this.SQLHelper.ExecuteNonQuery(strb.ToString());


            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }
        #endregion

        #region GetPriceByItemId
        /// <summary>
        /// 根据商品ID获取商品价格
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public decimal GetPriceByItemId(string itemId)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"select item_price from  T_ITEM_PRICE where item_id='{0}'", itemId);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables.Count > 0)
                {
                    return decimal.Parse(ds.Tables[0].Rows[0]["item_price"].ToString());
                }
            }
            return 0;
        }
        #endregion

        #region GetSkuIdByItemId
        /// <summary>
        /// 根据itemId获取skuId
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetSkuIdByItemId(string itemId)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@" select sku_id from T_Sku  where item_id='{0}'", itemId);
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["sku_id"].ToString();
                }
            }
            return "";
        }
        #endregion

        #endregion
    }
}
