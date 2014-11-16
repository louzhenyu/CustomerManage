/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using Aspose.Cells;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class LEventSignUpBLL
    {
        #region 2.1.33 �������Ա�б� Jermyn20130428
        /// <summary>
        /// �������Ա�б�(2.1.33) Jermyn20130428
        /// </summary>
        /// <param name="EventID">�ID</param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetResponseParams<LEventSignUpEntity> GetEventApplies(string EventID, int Page, int PageSize)
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<LEventSignUpEntity>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "�ID����Ϊ��",
                };
            }
            if (PageSize == 0) PageSize = 15;
            #endregion

            GetResponseParams<LEventSignUpEntity> response = new GetResponseParams<LEventSignUpEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
                #region ҵ����
                LEventSignUpEntity usersInfo = new LEventSignUpEntity();

                usersInfo.ICount = _currentDAO.GetEventAppliesCount(EventID);

                IList<LEventSignUpEntity> usersInfoList = new List<LEventSignUpEntity>();
                if (usersInfo.ICount > 0)
                {
                    DataSet ds = new DataSet();
                    ds = _currentDAO.GetEventAppliesList(EventID, Page, PageSize);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        usersInfoList = DataTableToObject.ConvertToList<LEventSignUpEntity>(ds.Tables[0]);
                    }
                }

                usersInfo.EntityList = usersInfoList;
                #endregion
                response.Params = usersInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "����:" + ex.ToString();
                return response;
            }
        }
        #endregion

        #region �����ύ�����۱�����

        /// <summary>
        /// �����ύ�����۱�����
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="userName">�û�����</param>
        /// <param name="phone">�ֻ���</param>
        /// <returns></returns>
        public string SetEventSignUp(string userId, string userName, string phone, string eventId)
        {
            string signUpId = string.Empty;

            if (string.IsNullOrEmpty(userId))
            {
                //����
                signUpId = Utils.NewGuid();

                this._currentDAO.Create(new LEventSignUpEntity()
                {
                    SignUpID = signUpId,
                    EventID = eventId,
                    UserName = userName,
                    Phone = phone
                });
            }
            else
            {
                //����
                signUpId = userId;

                this._currentDAO.Update(new LEventSignUpEntity()
                {
                    SignUpID = signUpId,
                    EventID = eventId,
                    UserName = userName,
                    Phone = phone
                }, false);
            }

            return signUpId;
        }

        #endregion

        #region GetEventSignUp
        public IList<LEventSignUpEntity> GetEventSignUp(LEventSignUpEntity entity, int Page, int PageSize)
        {

            IList<LEventSignUpEntity> usersInfoList = new List<LEventSignUpEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetEventSignUpList(entity, Page, PageSize);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LEventSignUpEntity>(ds.Tables[0]);
            }
            return usersInfoList;

        }
        #endregion

        #region �滻��̬�ؼ���
        public string ReplaceTemplate(string message, LEventSignUpEntity eventVip, VipEntity vip, LEventsEntity eventEntity)
        {
            message = message.Replace("$VipName$", eventVip.UserName);
            message = message.Replace("$Region$", null);
            message = message.Replace("$Seat$", null);
            message = message.Replace("$Ver$", new Random().Next().ToString());
            message = message.Replace("$CustomerId$", eventVip.CustomerId);
            message = message.Replace("$UserId$", vip.VIPID);
            message = message.Replace("$OpenId$", vip.WeiXinUserId);
            message = message.Replace("$EventName$", eventEntity.Title);

            return message;
        }
        #endregion

        #region ����ģ��
        public DataTable DownEnrollTpl(string eventID, int status)
        {
            DataTable dataTable = new DataTable();

            DataSet formDetail = DynamicFormLoadByEventID(eventID);

            if (Utils.IsDataSetValid(formDetail))
            {
                dataTable.TableName = formDetail.Tables[0].Rows[0][0].ToString();

                //build a new table which column names are ColumnDesc
                foreach (DataRow field in formDetail.Tables[1].Rows)
                {
                    if (field["IsUsed"].ToString() == "1")
                        dataTable.Columns.Add(field["ColumnDesc"].ToString());
                }
            }

            //1����δȷ�ϱ����ģ�10������ȷ�ϱ�����
            //����ȷ�ϱ������棬����ģ�����Ҫ����EventId��̬��ȡ���ֶ��⣬���⻹��һ���̶��ֶη���������Ӧ���ݿ��ֶ�GroupName��
            switch (status)
            {
                case 10:
                    dataTable.Columns.Add("�Ƿ�֧��");
                    dataTable.Columns.Add("����");
                    break;
                default:
                    break;
            }


            return dataTable;
        }
        #endregion ����ģ��

        public DataSet DynamicFormLoadByEventID(string eventID)
        {
            DataSet dataSet = new DataSet();

            MobileModuleObjectMappingBLL mobileModuleObjectMappingBLL = new MobileModuleObjectMappingBLL(CurrentUserInfo);
            var mobileModuleObjectMapping = mobileModuleObjectMappingBLL.Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "ObjectID", Value = eventID}
                , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
            }, null);

            if (mobileModuleObjectMapping.Length > 0)
            {
                string formID = mobileModuleObjectMapping[0].MobileModuleID.ToString();

                MobileModuleBLL mobileModuleBLL = new MobileModuleBLL(CurrentUserInfo);
                dataSet = mobileModuleBLL.DynamicFormLoad(formID, "LEventSignUp");
            }

            return dataSet;
        }

        #region ���뱨������
        public string ImportEnrollData(string fileName, DownEnrollTplRP downEnrollTplRP, out int total, out int add, out int update)
        {
            string result = "";
            //ǰ�˷���ֵ
            total = 0; add = 0; update = 0;

            if (!string.IsNullOrEmpty(fileName))
            {
                Aspose.Cells.License lic = new License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook wb = new Workbook(fileName);

                //�ж��Ƿ���worksheet
                if (wb.Worksheets.Count > 0)
                {
                    Worksheet ws = wb.Worksheets[0];

                    //�ж��Ƿ������Ч����
                    if (ws.Cells[0, 0].Value != null && !string.IsNullOrWhiteSpace(ws.Cells[0, 0].Value.ToString()))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = ws.Cells.ExportDataTableAsString(0, 0, ws.Cells.MaxRow + 1, ws.Cells.MaxColumn + 1, true);
                        //�������е�����
                        total = dataTable.Rows.Count;

                        DataSet formDetail = DynamicFormLoadByEventID(downEnrollTplRP.EventID);

                        if (Utils.IsDataSetValid(formDetail))
                        {
                            StringBuilder sbField = new StringBuilder();
                            StringBuilder sbValue = new StringBuilder();
                            string sql = "";
                            List<System.Data.SqlClient.SqlParameter> parameter = new List<System.Data.SqlClient.SqlParameter>();
                            Random random = new Random();

                            //1����δȷ�ϱ����ģ�10������ȷ�ϱ�����
                            //��δȷ�ϱ���ҳ�棬����excel���ݣ�ֻ��Ҫ����EventId������̬�ֶε�ֵ�������ݿ⡣
                            //����ȷ�ϱ���ҳ�棬����excel����ʱ��
                            //1����Ҫ����������ֵ���뵽�ֶ�GroupName��
                            //2����Ҫ����һ��6λ����ǩ���룬��д�뵽�ֶ�RegistrationCode�С�
                            switch (downEnrollTplRP.Status)
                            {
                                case 1:
                                    //Ĭ�ϸ�������һ�����ַ�����Ϊ��json���治����NULL��
                                    DataColumn groupCol = new DataColumn();
                                    groupCol.ColumnName = "Groupname";
                                    dataTable.Columns.Add(groupCol);
                                    sbField.Append("Groupname, ");
                                    sbValue.Append("@Groupname, ");
                                    parameter.Add(new System.Data.SqlClient.SqlParameter("@Groupname", SqlDbType.NVarChar, 4000, "Groupname"));
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        dr["Groupname"] = string.Empty;
                                    }
                                    break;
                                case 10:
                                    if (dataTable.Columns.Contains("����"))
                                    {
                                        //֧��״̬
                                        dataTable.Columns["�Ƿ�֧��"].ColumnName = "IsPay";
                                        sbField.Append("IsPay, ");
                                        sbValue.Append("@IsPay, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@IsPay", SqlDbType.NVarChar, 4000, "IsPay"));

                                        //����
                                        dataTable.Columns["����"].ColumnName = "GroupName";
                                        sbField.Append("GroupName, ");
                                        sbValue.Append("@GroupName, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@GroupName", SqlDbType.NVarChar, 4000, "GroupName"));

                                        DataColumn dataColumn = new DataColumn();
                                        dataColumn.ColumnName = "RegistrationCode";
                                        dataTable.Columns.Add(dataColumn);
                                        sbField.Append("RegistrationCode, ");
                                        sbValue.Append("@RegistrationCode, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@RegistrationCode", SqlDbType.NVarChar, 4000, "RegistrationCode"));

                                        DataColumn statusColumn = new DataColumn();
                                        statusColumn.ColumnName = "Status";
                                        dataTable.Columns.Add(statusColumn);
                                        sbField.Append("Status, ");
                                        sbValue.Append("@Status, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@Status", SqlDbType.NVarChar, 4000, "Status"));

                                        foreach (DataRow dr in dataTable.Rows)
                                        {
                                            dr["RegistrationCode"] = random.Next(100000, 999999);
                                            dr["Status"] = 10;
                                            dr["IsPay"] = dr["IsPay"].ToString().Length == 0 ? "0" : (dr["IsPay"].ToString() == "��" ? "1" : "0");
                                        }
                                    }
                                    else
                                        result = "��ȷ�ϱ��������ݱ�����������顱�У�";
                                    break;
                                default:
                                    break;
                            }

                            //Ĭ�����CustomerId
                            DataColumn cusCol = new DataColumn();
                            cusCol.ColumnName = "CustomerId";
                            dataTable.Columns.Add(cusCol);
                            sbField.Append("CustomerId, ");
                            sbValue.Append("@CustomerId, ");
                            parameter.Add(new System.Data.SqlClient.SqlParameter("@CustomerId", SqlDbType.NVarChar, 4000, "CustomerId"));
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                dr["CustomerId"] = CurrentUserInfo.ClientID;
                            }

                            foreach (DataRow field in formDetail.Tables[1].Rows)
                            {
                                if (field["IsUsed"].ToString() == "1")
                                {
                                    dataTable.Columns[field["ColumnDesc"].ToString()].ColumnName = field["ColumnName"].ToString();
                                    sbField.Append(field["ColumnName"] + ",");
                                    sbValue.Append("@" + field["ColumnName"] + ",");

                                    parameter.Add(new System.Data.SqlClient.SqlParameter("@" + field["ColumnName"].ToString(), SqlDbType.NVarChar, 4000, field["ColumnName"].ToString()));
                                }
                            }
                            sbField.Remove(sbField.Length - 1, 1);
                            sbValue.Remove(sbValue.Length - 1, 1);

                            DataColumn eventID = new DataColumn("EventID", typeof(string));
                            eventID.DefaultValue = downEnrollTplRP.EventID;
                            dataTable.Columns.Add(eventID);

                            sbField.Append(", EventID");
                            sbValue.Append(", @EventID");
                            parameter.Add(new System.Data.SqlClient.SqlParameter("@EventID", SqlDbType.NVarChar, 4000, "EventID"));

                            if (string.IsNullOrEmpty(result))
                            {
                                //����ֻ����Ƿ����ظ� add by zyh
                                List<string> phones = _currentDAO.GetPhoneList(downEnrollTplRP.EventID);
                                int repeat = 0;
                                //�����������µ�sql���
                                StringBuilder sbUpdate = new StringBuilder();

                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    DataRow dr = dataTable.Rows[i];
                                    string phone = dr["Phone"].ToString();
                                    //����Excelʱ������������е����⡣���ǻ��һ�п����ݡ�
                                    //������ʱ�ж��ֻ����Ƿ�գ��վ���Ϊ����Ч���ݡ�
                                    if (phone.ToString().Length == 0)
                                    {
                                        dataTable.Rows.RemoveAt(i);
                                        i--;
                                    }
                                    else
                                    {
                                        //����ֻ������ظ���ֱ�Ӵ�table���Ƴ�
                                        if (phones.Contains(phone))
                                        {
                                            repeat++;
                                            //��������ֶ�
                                            string _fields = string.Empty;
                                            foreach (DataRow field in formDetail.Tables[1].Rows)
                                            {
                                                if (field["IsUsed"].ToString() == "1")
                                                {
                                                    string column = field["ColumnName"].ToString();
                                                    _fields += column + "='" + dr[column].ToString() + "',";
                                                }
                                            }
                                            if (_fields.Length > 0)
                                            {
                                                if (downEnrollTplRP.Status == 1)
                                                    _fields = _fields.Substring(0, _fields.Length - 1);
                                                else
                                                    _fields += "IsPay=" + dr["IsPay"].ToString() + ",GroupName='" + dr["GroupName"].ToString() + "'";
                                                sbUpdate.Append(string.Format("UPDATE LEventSignUp SET {0} WHERE EventId = '{1}' AND Phone = '{2}'", _fields, downEnrollTplRP.EventID, phone));
                                            }

                                            //�Ƴ�����
                                            dataTable.Rows.RemoveAt(i);
                                            i--;
                                        }
                                    }
                                }
                                //���������͸��µ�����
                                update = repeat;
                                add = total - repeat;

                                sql = string.Format("insert into [LEventSignUp] ({0}) values ({1})", sbField.ToString(), sbValue.ToString());
                                _currentDAO.AdapterUpdate(CurrentUserInfo.CurrentLoggingManager.Connection_String, dataTable, sql, parameter);
                                //�����ֻ����ظ�����Ϣ
                                if (sbUpdate.Length > 0)
                                    _currentDAO.UpdateSignUpInfo(sbUpdate.ToString());

                            }
                        }
                    }
                    else
                        result = "�ļ������ݣ�";
                }
                else
                    result = "�ļ������ݣ�";
            }

            return result;
        }
        #endregion ����ģ��

        #region ������б�
        public LEventsTypeEntity[] EventTypeList()
        {
            LEventsTypeBLL lEventsTypeBLL = new LEventsTypeBLL(CurrentUserInfo);
            return lEventsTypeBLL.Query(
                new IWhereCondition[] { 
                    new IsNullOrEqaulCondition(CurrentUserInfo, "ClientID")
            }, new OrderBy[] { 
                new OrderBy() { FieldName = "Title", Direction = OrderByDirections.Asc }
            });
        }
        #endregion

        #region ����췽�б�
        public OptionsEntity[] EventSponsorList()
        {
            OptionsBLL optionsBLL = new OptionsBLL(CurrentUserInfo);
            return optionsBLL.Query(
                new IWhereCondition[] { 
                    new IsNullOrEqaulCondition(CurrentUserInfo, "ClientID")
                    , new EqualsCondition() { FieldName = "OptionName", Value = "EventSponsor"}
            }, new OrderBy[] { 
                new OrderBy() { FieldName = "Sequence", Direction = OrderByDirections.Asc }
            });
        }
        #endregion

        #region RP
        public class DownEnrollTplRP : IAPIRequestParameter
        {
            public string EventID { get; set; }
            public int Status { get; set; }
            public void Validate()
            {
                if (string.IsNullOrEmpty(EventID))
                    throw new APIException(201, "�ID����Ϊ�գ�");
            }
        }

        #endregion RP

        #region RD
        public class DownEnrollTplRD : IAPIResponseData
        {
            public int Total { get; set; }
            public int Add { get; set; }
            public int Update { get; set; }
        }

        public class EventTypeListRD : IAPIResponseData
        {
            public LEventsTypeEntity[] EventTypeList { get; set; }
        }

        public class EventSponsorListRD : IAPIResponseData
        {
            public OptionsEntity[] EventSponsorList { get; set; }
        }
        #endregion RD

        /*******************************************************************************************************************
         * �°� ����� Add 2014-07-30 By Alan
         */
        #region ��ȡ��б�
        /// <summary>
        /// ��ȡ�������Ա����
        /// </summary>
        /// <param name="eventID">�Id</param>
        /// <param name="status">״ֵ̬��δȷ��(1)����ȷ��(10)</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="pageCount">ҳ����</param>
        /// <param name="TotalCountUn">δȷ���ܼ�¼����</param>
        /// <param name="TotalCountYet">��ȷ���ܼ�¼����</param>
        /// <param name="dicCloumnNames">��������</param>
        /// <returns></returns>
        public DataTable GetEventSignUpList(string eventID, int status, int pageIndex, int pageSize, out int pageCount, out int TotalCountUn, out int TotalCountYet, out Dictionary<string, string> dicCloumnNames)
        {
            DataSet ds = _currentDAO.GetEventSignUpList(eventID, status, pageIndex, pageSize, false, out dicCloumnNames);

            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out TotalCountUn);
            int.TryParse(ds.Tables[2].Rows[0][0].ToString(), out TotalCountYet);

            if (status == 1)
            {
                pageCount = TotalCountUn / pageSize;

                if (TotalCountUn % pageSize > 0)
                {
                    pageCount++;
                }
            }
            else if (status == 10)
            {
                pageCount = TotalCountYet / pageSize;

                if (TotalCountYet % pageSize > 0)
                {
                    pageCount++;
                }
            }
            else
            {
                pageCount = -1;
            }
            return ds.Tables[0];
        }
        #endregion

        #region ȷ���μӱ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">״ֵ̬��δȷ��(1)����ȷ��(10)</param>
        /// <param name="signUpID">�������ԱID</param>
        /// <param name="operID">������ԱID</param>
        /// <param name="signInCode">ǩ����</param>
        /// <returns>ִ�н��</returns>
        public int SetEventSignUpOper(int status, string signUpID, string operID, string signInCode)
        {
            return _currentDAO.SetEventSignUpOper(status, signUpID, operID, signInCode);
        }
        #endregion

        #region ����֪ͨ
        /// <summary>
        /// ��ȡ֪ͨ��Ա��Ϣ
        /// </summary>
        /// <param name="Ids">֪ͨ��ԱIds����</param>
        /// <returns>���ݼ�</returns>
        public DataTable GetNotifyEventSignUpList(string IdsStr)
        {
            if (string.IsNullOrEmpty(IdsStr))
            {
                return null;
            }
            string[] Ids = IdsStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return _currentDAO.GetNotifyEventSignUpList(Ids).Tables[0];
        }
        /// <summary>
        /// �޸ķ���״̬
        /// </summary>
        /// <param name="signUpId">�������ԱID</param>
        /// <param name="sendStatus">����״̬</param>
        /// <param name="sendType">���ͷ�ʽ��1���š�2�ʼ���3΢��</param>
        /// <param name="operID">������ID</param>
        /// <returns>ִ�н��</returns>
        public int SetEventSignUpStatus(string signUpId, int sendStatus, int sendType, string operID)
        {
            return _currentDAO.SetEventSignUpStatus(signUpId, sendStatus, sendType, operID);
        }
        #endregion

        #region ���÷���
        /// <summary>
        /// ���÷���ӿ�
        /// </summary>
        /// <param name="signUpId">�������ԱID</param>
        /// <param name="groupName">����</param>
        /// <param name="operId">������ԱId</param>
        /// <returns>�������</returns>
        public int SetSignUpGroup(string signUpId, string groupName, string operId)
        {
            return _currentDAO.SetSignUpGroup(signUpId, groupName, operId);
        }
        #endregion

        #region ��ȡǩ���б�
        /// <summary>
        /// ��ȡǩ���б�
        /// </summary>
        /// <param name="eventId">�Id</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="TotalCount">�ܼ�¼����</param>
        /// <param name="TotalPage">��ҳ��</param>
        /// <returns>���ݼ�</returns>
        public DataTable GetEventUserMappingList(string eventId, int pageIndex, int pageSize, out int TotalCount, out int TotalPage)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                TotalCount = 0;
                TotalPage = 0;
                return null;
            }
            //Get List
            DataSet ds = _currentDAO.GetEventUserMappingList(eventId, pageIndex, pageSize);
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out TotalCount);

            TotalPage = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
            {
                TotalPage++;
            }
            return ds.Tables[0];
        }
        #endregion

        #region ǩ��
        /// <summary>
        /// ��ȡǩ����Ա��Ϣ
        /// </summary>
        /// <param name="signUpId">��û�ID</param>
        /// <param name="regCode">ǩ����</param>
        /// <param name="phone">�ֻ�����</param>
        /// <param name="dicInfo">����������Ϣ</param>
        /// <returns>������Ϣ</returns>        
        public bool EventCheckInInfo(string signUpId, string regCode, string phone, out Dictionary<string, string> dicInfo)
        {
            bool res = false;

            dicInfo = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(regCode))
            {

                res = false;
            }
            DataSet ds = _currentDAO.EventCheckInInfo(signUpId, phone, regCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var v = ds.Tables[0].Rows[0];
                if (regCode.Equals(v["RegistrationCode"].ToString()))
                {
                    res = true;
                }
                else if (phone.Equals(v["Phone"].ToString()))
                {
                    res = true;
                }
                //У��ɹ�
                if (res)
                {
                    _currentDAO.EventCheckInRecord(v["SignUpID"].ToString(), v["EventID"].ToString(), v["UserName"].ToString(), v["Phone"].ToString(), v["Email"].ToString(), v["UserName"].ToString());

                    dicInfo.Add("Name", v["UserName"].ToString());
                    dicInfo.Add("Company", v["Col7"].ToString());
                    dicInfo.Add("Job", v["Col8"].ToString());
                    dicInfo.Add("Mobile", v["Phone"].ToString());
                    dicInfo.Add("GroupName", v["GroupName"].ToString());
                    dicInfo.Add("IsPay", v["IsPay"].ToString());
                    dicInfo.Add("EventID", v["EventID"].ToString());
                }
            }

            return res;
        }
        #endregion

        #region ǩ���ɹ�����ʾ����Ϣ
        /// <summary>
        /// ��ȡ���Ա��Ϣ
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public DataTable GetCheckInGroupsList(string eventID, string groupName)//, int pageIndex, int pageSize, out Dictionary<string, string> dicCloumnNames
        {
            DataSet ds = _currentDAO.GetCheckInGroupsList(eventID, groupName);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion
    }
}