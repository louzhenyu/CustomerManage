using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// datatable 转成 object
    /// </summary>
    public class DataTableToObject
    {


        public DataTableToObject() { }

        /// <summary>
        /// Convert an DataRow to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(DataRow row) where T : new()
        {
            System.Object obj = new T();
            if (row != null)
            {
                DataTable dataTable = row.Table;
                GetObject(dataTable.Columns, row, obj);
            }
            if (obj != null && obj is T)
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }

        private static void GetObject(DataColumnCollection cols, DataRow dr, System.Object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            foreach (PropertyInfo pro in pros)
            {
                if (cols.Contains(pro.Name))
                {
                    //added by Willie Yan on 2014-10-09 to avoid exception of non-string type to string type
                    if ((pro.PropertyType).Name.ToLower() == "string")
                        pro.SetValue(obj, dr[pro.Name] == DBNull.Value ? "" : dr[pro.Name].ToString(), null);
                    else
                        pro.SetValue(obj, dr[pro.Name] == DBNull.Value ? null : dr[pro.Name], null);
                }
            }
        }

        /// <summary>
        /// Convert a data table to an objct list  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T obj = ConvertToObject<T>(row);
                list.Add(obj);
            }
            return list;
        }



        /// <summary>
        /// Convert object collection to an data table  
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTableFromList(System.Object list)
        {
            DataTable dt = null;
            System.Type listType = list.GetType();

            if (listType.IsGenericType)
            {
                System.Type type = listType.GetGenericArguments()[0];
                dt = new DataTable(type.Name + "List");
                MemberInfo[] mems = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

                #region 表结构构建
                foreach (MemberInfo mem in mems)
                {
                    //switch(mem.MemberType)
                    //{
                    //    case MemberTypes.Property:
                    //        {
                    //            dt.Columns.Add(((PropertyInfo)mem).Name,typeof(System.String));
                    //            break;
                    //        }
                    //    case MemberTypes.Field:
                    //        {
                    //            dt.Columns.Add(((FieldInfo)mem).Name,typeof(System.String));
                    //            break;
                    //        }
                    //}
                    dt.Columns.Add(mem.Name, mem.ReflectedType);
                }
                #endregion

                #region 表数据填充
                IList iList = list as IList;
                foreach (System.Object record in iList)
                {
                    System.Int32 i = 0;
                    System.Object[] fieldValues = new System.Object[dt.Columns.Count];
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        MemberInfo mem = listType.GetMember(dataColumn.ColumnName)[0];
                        switch (mem.MemberType)
                        {
                            case MemberTypes.Field:
                                {
                                    fieldValues[i] = ((FieldInfo)mem).GetValue(record);
                                    break;
                                }
                            case MemberTypes.Property:
                                {
                                    fieldValues[i] = ((PropertyInfo)mem).GetValue(record, null);
                                    break;
                                }
                        }
                        i++;
                    }
                    dt.Rows.Add(fieldValues);
                }
                #endregion

            }

            return dt;
        }

        public static string NoHTML(string Htmlstring) //去除HTML标记   
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            Htmlstring.Replace("\n", "");
            Htmlstring.Replace("\r", "");
            Htmlstring.Replace("\t", "");
            Htmlstring.Replace("<span>", "");
            Htmlstring.Replace("</span>", "");
            //Htmlstring = System.Web. HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();   

            return Htmlstring;
        }  
    }
}
