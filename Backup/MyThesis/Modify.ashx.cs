using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyThesis
{
    /// <summary>
    /// Summary description for Modify
    /// </summary>
    public class Modify : IHttpHandler
    {
        private XmlDocument xDoc;
        public void ProcessRequest(HttpContext context)
        {
            string strFile = PageHelper.ReadFile(context.Server.MapPath("Modify.htm"));

            //if the the HTML1.1 request method is get,it means the user just open this page,
            //the input text box should be able to show the original information ot the user
            xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Zhigang Zhang\Documents\Visual Studio 2010\Projects\ExtractDataFromExcel\MyThesis\duration.xml");
            if (context.Request.HttpMethod.ToLower() == "get")
            {
                string strId = context.Request.QueryString["timestamp"];
                //1.1 get the timestamp which need to be deleted
               
                

                foreach (XmlNode x in xDoc.SelectNodes("NewDataSet/Table"))
                {
                    if (x["Timestamp"].InnerText.Substring(0, 19) == strId.Substring(0, 19))
                    {
                        strFile = strFile.Replace("{@txt_timestamp}", x["Timestamp"].InnerText).Replace("{@txt_activity}", x["Activity_id"].InnerText).Replace("{@txt_duration}", x["Activity_duration"].InnerText);

                    }
                }


            }
                //if the HTTP1.1 request method is "POST",so I need to submit the new data,and then change the XML
            else
            {
                //use string var to temporarlly store the new data
                string timestamp = context.Request.Form["timestamp"];
                string activity_name = context.Request.Form["activity_name"];
                string duration = context.Request.Form["duration"];
                //then update these new value to the XML file
                foreach (XmlNode x in xDoc.SelectNodes("NewDataSet/Table"))
                {
                    if (x["Timestamp"].InnerText.Substring(0, 19) == timestamp.Substring(0, 19))
                    {
                        x["Activity_id"].InnerText = activity_name;
                        x["Activity_duration"].InnerText = duration;
                        xDoc.Save(@"C:\Users\Zhigang Zhang\Documents\Visual Studio 2010\Projects\ExtractDataFromExcel\MyThesis\duration.xml");
                        string strJsCode = PageHelper.WriteJsMsg("Update sucessfull!","MainFormPage.ashx");
                        context.Response.Write(strJsCode);
                    }
                    
                }
 
            }
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