/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    /// ���ݷ��ʣ�  
    /// ��EclubClientCitySequence�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubClientCitySequenceDAO : Base.BaseCPOSDAO, ICRUDable<EclubClientCitySequenceEntity>, IQueryable<EclubClientCitySequenceEntity>
    {

        #region ��ȡʡ��������
        /// <summary>
        /// ��ȡʡ��������
        /// </summary>
        /// <param name="cityType">1.Ϊʡ��2.Ϊ�У�3.Ϊ��</param>
        /// <param name="cityCode">���</param>
        /// <returns></returns>
        public DataSet GetCityList(int? cityType, string cityCode)
        {
            string query = "";

            if (cityType == 1)
            {
                query = " select substring(city_code,1,2) as CityCode,city1_name as CityName from T_City ";
            }
            else if (cityType == 2)
            {
                query = string.Format(@"    
select substring(city_code,1,4) as CityCode,city2_name as CityName from T_City 
				where substring(city_code,1,2)='{0}' ", cityCode);
            }
            else if (cityType == 3)
            {
                query = string.Format(@"    
select city_code as CityCode,city3_name as CityName from T_City 
				where substring(city_code,1,4)='{0}' ", cityCode);
            }

            string sql = string.Format(@"    
select b.*,e.Sequence from (select * from ({0}) a  
			  group by CityCode,CityName)b 
			  left join EclubClientCitySequence e 
			  on b.CityCode=e.CityCode and  e.IsDelete=0 and e.CustomerId='{1}' and e.CityType={2} 
			  order by isnull(e.Sequence,10000),b.CityName ", query, CurrentUserInfo.CurrentLoggingManager.Customer_Id, cityType);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// ��ȡʡ�������ݣ��޸�
        /// </summary>
        /// <param name="cityType">27.Ϊʡ��28.Ϊ�У�29.Ϊ��</param>
        /// <param name="cityCode">���</param>
        /// <returns></returns>
        public DataSet GetCityList_V1(int? cityType, string cityCode)
        {
            string query = "";

            if (cityType == 27)
            {
                query = " select substring(city_code,1,2) as ID,city1_name as Text from T_City ";
            }
            else if (cityType == 28)
            {
                if (string.IsNullOrEmpty(cityCode))
                {
                    query = string.Format(@"    
select substring(city_code,1,4) as ID,city2_name as Text from T_City ");
                }
                else
                {
                    query = string.Format(@"    
select substring(city_code,1,4) as ID,city2_name as Text from T_City 
				where substring(city_code,1,2)='{0}' ", cityCode);
                }
            }
            else if (cityType == 29)
            {
                query = string.Format(@"    
select city_code as ID,city3_name as Text from T_City 
				where substring(city_code,1,4)='{0}' ", cityCode);
            }

            string sql = string.Format(@"    
select b.* from (select * from ({0}) a  
			  group by ID,Text)b 
			  left join EclubClientCitySequence e 
			  on b.ID=e.CityCode and  e.IsDelete=0 and e.CustomerId='{1}' 
			  order by isnull(e.Sequence,10000),b.Text ", query, CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

    }
}
