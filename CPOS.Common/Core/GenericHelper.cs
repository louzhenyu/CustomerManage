using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CPOS.Common.Core
{
    public class GenericHelper
    {
        //
        // LM,2016/04/01
        //

        /// <summary>
        /// 泛型 Helper Instance
        /// </summary>
        public static GenericHelper GH
        {
            get
            {
                return new GenericHelper();
            }
        }

        //public void DbvSetProperty<M>(M model, PropertyInfo prop, object value)
        //{
        //    try
        //    {
        //        var propType = prop.PropertyType.FullName;

        //        if (propType == TypeConst.ShortType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToShort());
        //        }
        //        else if (propType == TypeConst.IntType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToInt());
        //        }
        //        else if (propType == TypeConst.LongType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToLong());
        //        }
        //        else if (propType == TypeConst.DecimalType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToDecimal());
        //        }
        //        else if (propType == TypeConst.DoubleType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToDouble());
        //        }
        //        else if (propType == TypeConst.StringType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToString());
        //        }
        //        else if (propType == TypeConst.DateTimeType)
        //        {
        //            SetPropertyValue<M>(model, prop.Name, value.DbvToDateTime());
        //        }
        //        else
        //        {
        //            throw new Exception("请到DbvSetProperty<M>添加需解析的类型:" + propType);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("方法DbvSetProperty<M>出错:" + ex.Message);
        //    }
        //}

        //public Attribute GetAttribute<A>(Type mType, PropertyInfo prop)
        //{
        //    try
        //    {
        //        return mType.GetMember(prop.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)[0].GetCustomAttribute(typeof(A), false);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("方法GetAttribute<A>出错:" + ex.Message);
        //    }
        //}

        public Type GetPropertyType<M>(M m,string propertyName)
        {
            return m.GetType().GetProperty(propertyName).GetType();
        }

        public RM GetPropertyValue<M, RM>(M m, string properyName)
        {
            try
            {
                return (RM)m.GetType().GetProperty(properyName).GetValue(m, null);
            }
            catch (Exception ex)
            {
                throw new Exception("方法GetPropertyValue<M, RM>出错:" + ex.Message);
            }
        }

        public void SetPropertyValue<M>(M m, string propertyName, object value)
        {
            try
            {
                if (value != null)
                {
                    m.GetType().GetProperty(propertyName).SetValue(m, value, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("方法SetPropertyValue<M>出错:" + ex.Message);
            }
        }

    }
}
