/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:01
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using System.Diagnostics;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问： 批量制卡 
    /// 表VipCardBatch的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardBatchDAO : Base.BaseCPOSDAO, ICRUDable<VipCardBatchEntity>, IQueryable<VipCardBatchEntity>
    {
        /// <summary>
        /// 制卡
        /// </summary>
        /// <param name="p_Data"></param>
        public void BatchMakeVipCard(VipCardBatchEntity p_Data)
        {
            if (p_Data.Qty > 0)
            {
                SqlConnection sqlCon = new SqlConnection(CurrentUserInfo.CurrentLoggingManager.Connection_String);
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction(); // 开始事务
                SqlBulkCopy sqlBC = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran);
                sqlBC.DestinationTableName = "VipCard";
                sqlBC.BatchSize = p_Data.Qty.Value;

                try
                {
                    #region 特殊值处理
                    //卡类型ID
                    int? CardTypeID = null;
                    var Result = new SysVipCardTypeDAO(CurrentUserInfo).QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeCode = p_Data.VipCardTypeCode }, null);
                    if (Result.Length > 0)
                    {
                        CardTypeID = Result[0].VipCardTypeID;
                    }
                    if (CardTypeID == null)
                        throw new Exception("卡类型ID为null！");
                    #endregion

                    #region 获取卡号
                    int m_VipCardCode = int.Parse(p_Data.CardPrefix.Substring(p_Data.CardPrefix.Length-1)+GetVipCardCode(CurrentUserInfo.ClientID));
                    #endregion

                    DataTable dataTable = new DataTable();
                    #region 列名
                    dataTable.Columns.Add("VipCardID");
                    dataTable.Columns.Add("VipCardTypeID", typeof(Int32));
                    dataTable.Columns.Add("VipCardGradeID", typeof(Int32));
                    dataTable.Columns.Add("VipCardCode");
                    dataTable.Columns.Add("VipCardISN");
                    dataTable.Columns.Add("VipCardName");
                    dataTable.Columns.Add("BatchNo");
                    dataTable.Columns.Add("VipCardStatusId", typeof(Int32));
                    dataTable.Columns.Add("MembershipTime", typeof(DateTime));
                    dataTable.Columns.Add("MembershipUnit");
                    dataTable.Columns.Add("BeginDate");
                    dataTable.Columns.Add("EndDate");
                    dataTable.Columns.Add("TotalAmount", typeof(decimal));
                    dataTable.Columns.Add("BalanceAmount", typeof(decimal));
                    dataTable.Columns.Add("BalancePoints", typeof(decimal));
                    dataTable.Columns.Add("BalanceBonus", typeof(decimal));
                    dataTable.Columns.Add("CumulativeBonus", typeof(decimal));
                    dataTable.Columns.Add("PurchaseTotalAmount", typeof(decimal));
                    dataTable.Columns.Add("PurchaseTotalCount", typeof(Int32));
                    dataTable.Columns.Add("CheckCode");
                    dataTable.Columns.Add("SingleTransLimit", typeof(decimal));
                    dataTable.Columns.Add("IsOverrunValid", typeof(Int32));
                    dataTable.Columns.Add("RechargeTotalAmount", typeof(decimal));
                    dataTable.Columns.Add("LastSalesTime", typeof(DateTime));
                    dataTable.Columns.Add("IsGift", typeof(Int32));
                    dataTable.Columns.Add("SalesAmount");
                    dataTable.Columns.Add("SalesUserId");
                    dataTable.Columns.Add("CreateTime", typeof(DateTime));
                    dataTable.Columns.Add("CreateBy");
                    dataTable.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dataTable.Columns.Add("LastUpdateBy");
                    dataTable.Columns.Add("IsDelete", typeof(Int32));
                    dataTable.Columns.Add("CustomerID");
                    dataTable.Columns.Add("SalesUserName");
                    #endregion
                    for (int i = 0; i < p_Data.Qty; i++)
                    {
                        #region 属性赋值
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["VipCardID"] = System.Guid.NewGuid().ToString();
                        dataRow["VipCardTypeID"] = CardTypeID.Value;
                        dataRow["VipCardGradeID"] = DBNull.Value;
                        dataRow["VipCardCode"] = p_Data.CardPrefix + m_VipCardCode.ToString();
                        dataRow["VipCardISN"] = "";
                        dataRow["VipCardName"] = "";
                        dataRow["BatchNo"] = p_Data.BatchNo.Value.ToString();
                        dataRow["VipCardStatusId"] = 0;
                        dataRow["MembershipTime"] = DBNull.Value;
                        dataRow["MembershipUnit"] = "";
                        dataRow["BeginDate"] = "";
                        dataRow["EndDate"] = "";
                        dataRow["TotalAmount"] = 0;
                        dataRow["BalanceAmount"] = 0;
                        dataRow["BalancePoints"] = 0;
                        dataRow["BalanceBonus"] = 0;
                        dataRow["CumulativeBonus"] = 0;
                        dataRow["PurchaseTotalAmount"] = 0;
                        dataRow["PurchaseTotalCount"] = 0;
                        dataRow["CheckCode"] = "";
                        dataRow["SingleTransLimit"] = 0;
                        dataRow["IsOverrunValid"] = 0;
                        dataRow["RechargeTotalAmount"] = 0;
                        dataRow["LastSalesTime"] = DBNull.Value;
                        dataRow["IsGift"] = DBNull.Value;
                        dataRow["SalesAmount"] = "";
                        dataRow["SalesUserId"] = "";
                        dataRow["CreateTime"] = DateTime.Now;
                        dataRow["CreateBy"] = CurrentUserInfo.UserID;
                        dataRow["LastUpdateTime"] = DateTime.Now;
                        dataRow["LastUpdateBy"] = CurrentUserInfo.UserID;
                        dataRow["IsDelete"] = 0;
                        dataRow["CustomerID"] = CurrentUserInfo.ClientID;
                        dataRow["SalesUserName"] = "";
                        #endregion
                        //
                        dataTable.Rows.Add(dataRow);
                        m_VipCardCode++;
                    }

                    //执行
                    sqlBC.WriteToServer(dataTable); //此处报错
                    sqlTran.Commit();

                    #region 修改卡号配置数量
                    var m_CustomerBasicSettingDAO = new CustomerBasicSettingDAO(CurrentUserInfo);
                    var UpResult = m_CustomerBasicSettingDAO.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode="VipCardCode",CustomerID = CurrentUserInfo.ClientID }, null);
                    if (UpResult.Length > 0)
                    {
                        CustomerBasicSettingEntity UpDate = UpResult[0];
                        string m_NewVipCardCode = (m_VipCardCode-1).ToString();
                        UpDate.SettingValue = m_NewVipCardCode.Substring(1, m_NewVipCardCode.Length - 1);
                        m_CustomerBasicSettingDAO.Update(UpDate);
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    sqlBC.Close();
                    sqlCon.Close();
                }
            }
        }
        /// <summary>
        /// 生成会员卡号
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        private string GetVipCardCode(string pCustomerID)
        {
            string sql = string.Format(@"declare @ReturnValue nvarchar(50)
exec spGetNextCode '{0}','VipCardCode',6,'',@ReturnValue output
select @ReturnValue", pCustomerID);
            return this.SQLHelper.ExecuteScalar(sql).ToString();
        }
        /// <summary>
        /// 导入卡内吗
        /// </summary>
        /// <param name="VipCardInfoList"></param>
        public void ImportVipCardISN(Dictionary<string, string> p_VipCardInfoCollection, string p_BatchNo)
        {

            if (p_VipCardInfoCollection.Count > 0)
            {
                #region Excel数据导至导卡内辅助表
                SqlConnection sqlCon = new SqlConnection(CurrentUserInfo.CurrentLoggingManager.Connection_String);
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction(); // 开始事务
                SqlBulkCopy sqlBC = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran);
                sqlBC.DestinationTableName = "VipCardISNImportHelp";
                sqlBC.BatchSize = p_VipCardInfoCollection.Count;
                try
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("VipCardCode");
                    dataTable.Columns.Add("VipCardISN");
                    dataTable.Columns.Add("BatchNo");
                    dataTable.Columns.Add("IsAbnormal", typeof(Int32));
                    dataTable.Columns.Add("CreateTime", typeof(DateTime));
                    dataTable.Columns.Add("CreateBy");
                    dataTable.Columns.Add("LastUpdateTime", typeof(DateTime));
                    dataTable.Columns.Add("LastUpdateBy");
                    dataTable.Columns.Add("IsDelete", typeof(Int32));
                    dataTable.Columns.Add("CustomerID");
                    foreach (var item in p_VipCardInfoCollection)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["VipCardCode"] = item.Key;
                        dataRow["VipCardISN"] = item.Value;
                        dataRow["BatchNo"] = p_BatchNo;
                        dataRow["IsAbnormal"] = 0;
                        dataRow["CreateTime"] = DateTime.Now;
                        dataRow["CreateBy"] = CurrentUserInfo.UserID;
                        dataRow["LastUpdateTime"] = DateTime.Now;
                        dataRow["LastUpdateBy"] = CurrentUserInfo.UserID;
                        dataRow["IsDelete"] = 0;
                        dataRow["CustomerID"] = CurrentUserInfo.ClientID;
                        //
                        dataTable.Rows.Add(dataRow);
                    }

                    //执行
                    sqlBC.WriteToServer(dataTable); //此处报错
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {

                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    sqlBC.Close();
                    sqlCon.Close();
                }
                #endregion

                #region 执行存储过程
                var parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@BatchNo", System.Data.SqlDbType.NVarChar) { Value = p_BatchNo };
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = parm.ToJSON()
                });
                this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Import_VipCardISN", parm);
                #endregion
            }

        }
    }
}
