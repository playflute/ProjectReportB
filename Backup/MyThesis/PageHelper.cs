using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyThesis
{
    public class PageHelper
    {
        /// <summary>
        /// Read the HTML String
        /// </summary>
        /// <param name="strPath">The physical path of the html path</param>
        /// <returns></returns>
        public static string ReadFile(string strPath)
        {
            return System.IO.File.ReadAllText(strPath);
        }
        /// <summary>
        /// get the prompt and and the string of skipping Javascript
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <returns></returns>
        public static string WriteJsMsg(string strMsg, string strBackUrl)
        {
            return "<script>alert('"+strMsg+"');window.location='"+strBackUrl+"';</script>";
        }
    }
}