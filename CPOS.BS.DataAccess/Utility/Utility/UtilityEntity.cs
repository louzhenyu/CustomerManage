/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2012/12/5 18:27:23
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
using JIT.Utility;
using JIT.Utility.Entity;
using System.Data;

namespace JIT.CPOS.BS.DataAccess.Utility
{
    /// <summary>
    /// �û���¼��Ϣʵ�� 
    /// </summary>
    public partial class UtilityEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public UtilityEntity()
        {
        }
        #endregion

        #region ���Լ�
        #region ��������
        /// <summary>
        /// �û�������[�����ֶ�]
        /// </summary>
        public int? UsersID { get; set; }
        /// <summary>
        /// ���ݿ����[�����ֶ�]
        /// </summary>
        public string TableName { get; set; }       
        /// <summary>
        /// �������ͣ�1�޸�ɾ����2��գ�3Ϊ��ѯ,4Ϊ�Զ���Sql��ѯ[�����ֶ�]
        /// </summary>
        public int? OperationType { get; set; }
        #endregion

        #region ��ѯ�ֶ�
        /// <summary>
        /// ��ǰҳ,��һҳΪ0 [��ѯ�ֶ�]
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// ��ʾ���� [��ѯ�ֶ�]
        /// </summary>
        public int  PageSize { get; set; }
        /// <summary>
        /// �Ƿ��ҳ,0��1�� [��ѯ�ֶ�]
        /// </summary>
        public int? PageIs { get; set; }
        /// <summary>
        /// �������� ID desc,NID asc [��ѯ�ֶ�]
        /// </summary>
        public string PageSort { get; set; }
        /// <summary>
        /// Where����������ʱ����ҪWhere isdelete=0 ��ֱ�Ӵ�����ʾ����and ID=1 and ID!=2 [�����ֶ�]
        /// </summary>
        public string PageWhere { get; set; }
        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int PageTotal { get; set; }
        public int PageCount { get; set; }
        public DataSet PageDataSet { get; set; }
        #endregion

        #region �޸��ֶ�
        /// <summary>
        /// �ֶ�����[�޸��ֶ�]
        /// </summary>
        public string UptField { get; set; }
        /// <summary>
        /// ��ֵ����[��ӣ��޸��ֶ�]
        /// </summary>
        public string UptValue { get; set; }

        /// <summary>
        /// �����ֶ�-- Where �����ֶ� in ���� [�޸��ֶ�]
        /// </summary>
        public string UptWhereField { get; set; }
        /// <summary>
        ///  ����1,2,3-- Where �����ֶ� in ���� [�޸��ֶ�]
        /// </summary>
        public string UptWhereValue { get; set; }
        #endregion

        #region ɾ������
        /// <summary>
        /// ������ݵ��ֶ�
        /// </summary>
        public string DeleteField { get; set; }
        /// <summary>
        /// ������ݵ�����
        /// </summary>
        public string DeleteValue { get; set; }
        #endregion

        #region �Զ���sql��ѯ
        /// <summary>
        /// �Զ����ѯ
        /// </summary>
        public string CustomSql { get; set; }
        #endregion

        #region ����ֶ�
        /// <summary>
        /// ������Ӱ��������������ǵ�һ�е�һ�е�ֵ ��1*1λ�ã�[����ֶ�]
        /// </summary>
        public int ResultNum { set; get; }
        /// <summary>
        /// ���ز�ѯ�Ľ��[����ֶ�]
        /// </summary>
        public string ResultError { set; get; }
        public int? OpResultID { set; get; }
        #endregion
        #endregion
    }
}