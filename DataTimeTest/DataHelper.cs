using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Common
{
    public class DataHelper
    {
        static JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// 将 对象 转成 json格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Obj2Json(object obj)
        {
            //把集合 转成 json 数组格式字符串
            return jss.Serialize(obj);
        }
        public static string MD5(String clear_text ) { 
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(clear_text,"MD5");

            
        }
    }
}
