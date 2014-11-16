/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/18 17:51:09
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
using System.Linq;
using System.Reflection;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺 �ͻ��˵��� 
    /// </summary>
    public partial class ClientMenuButtonEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ClientMenuButtonEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// �Զ����
        /// </summary>
        public Guid? ClientMenuID { get; set; }

        /// <summary>
        /// ���ڲ˵�Ȩ�޿���
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// �˵�����(�ͻ��Զ���)
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// �˵�����(�ͻ��Զ���)Ӣ��
        /// </summary>
        public string MenuNameEn { get; set; }

        /// <summary>
        /// �˵�˳��
        /// </summary>
        public int? MenuOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// ��ʽ����
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// �ϼ����
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// �Ƿ�ͳ��
        /// </summary>
        public int? IsCount { get; set; }

        /// <summary>
        /// �ͻ����(����Client��)
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ClientDistributorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }


        public Guid? ClientButtonID { get; set; }
        public string ButtonText { get; set; }
        public string ButtonTextEn { get; set; }
        public string ButtonCode { get; set; }

        #endregion        
    }
}