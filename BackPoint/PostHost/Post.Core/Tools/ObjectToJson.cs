using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.Tools
{
    public static class ObjectToJson
    {
        /// <summary>
        /// 将一个对象转换成JSON格式的字符串
        /// </summary>
        /// <param name="p_Instance">需要转换的对象</param>
        /// <returns>转换后的字符串，如果转换失败，返回""</returns>
        public static string ObjectToJsonString(object p_Instance)
        {
            try
            {
                if (p_Instance == null)
                    return "{}";
                Newtonsoft.Json.Converters.IsoDateTimeConverter timeFormat = new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                {
                    //指定转换的日期格式
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                return JsonConvert.SerializeObject(p_Instance, timeFormat);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将JSON字符串转换成指定的对象类型
        /// </summary>
        /// <typeparam name="T">需要转换的类型</typeparam>
        /// <param name="p_String">JSON描述的待转换的字符串</param>
        /// <returns>转换结果，失败为null</returns>
        public static T JsonStringToObject<T>(string p_String)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(p_String);
            }
            catch
            {
                //如果出错，返回指定类型的默认值
                return default(T);
            }
        }
    }
}
