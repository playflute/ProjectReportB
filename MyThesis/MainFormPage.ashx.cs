using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyThesis
{
    /// <summary>
    /// Summary description for MainFormPage
    /// </summary>
    public class MainFormPage : IHttpHandler
    {
        private XmlDocument xDoc;
        public void ProcessRequest(HttpContext context)
        {
            xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Zhigang Zhang\Documents\Visual Studio 2010\Projects\ExtractDataFromExcel\MyThesis\duration.xml");
            //find the physical path of the List Template
            string strPhysicalPath = context.Server.MapPath("MainFormPage.htm");
            string strFile = PageHelper.ReadFile(strPhysicalPath);
            System.Text.StringBuilder sbTrs = new System.Text.StringBuilder(200);
            //read the XML file and append each item of XML to the table row
             foreach (XmlNode x in xDoc.SelectNodes("NewDataSet/Table"))
             {
                 sbTrs.Append("<tr>");
                 sbTrs.Append("<td>" + x["Timestamp"].InnerText+"</td>");
                 sbTrs.Append("<td>" + x["Activity_id"].InnerText + "</td>");
                 string sTemporary="";
                 if(!(x["Activity_duration"]==null))
                 {
                     sTemporary=x["Activity_duration"].InnerText;
                 }
                 sbTrs.Append("<td>" + sTemporary + "</td>");
                 sbTrs.Append("<td><a href=\"javascript:void(0)\" onclick=\"doDel('" + x["Timestamp"].InnerText + "')\">Delete</a></td>");
                 sbTrs.Append("<td><a href=\"Modify.ashx?timestamp=" + x["Timestamp"].InnerText + "\">Change</a></td>");
                 //sbTrs.Append("<td>" + "???" + "</td>");
                 sbTrs.AppendLine("</tr>");
                 
             }
             strFile = strFile.Replace("{@new_row}", sbTrs.ToString());
             context.Response.Write(strFile);



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}