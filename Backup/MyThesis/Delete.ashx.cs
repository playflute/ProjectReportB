using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyThesis
{
    /// <summary>
    /// Summary description for Delete
    /// </summary>

    public class Delete : IHttpHandler
    {
        private XmlDocument xDoc;

        public void ProcessRequest(HttpContext context)
        {
            //		"2013-08-02T20:19:15 10:00"	"2013-08-02T20:19:15 10:00"	string

            xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Zhigang Zhang\Documents\Visual Studio 2010\Projects\ExtractDataFromExcel\MyThesis\duration.xml");
            string strId = context.Request.QueryString["timestamp"];
            foreach (XmlNode x in xDoc.SelectNodes("NewDataSet/Table"))
            {
                if (x["Timestamp"].InnerText.Substring(0, 19) == strId.Substring(0, 19))
                {

                    x.ParentNode.RemoveChild(x);
                    xDoc.Save(@"C:\Users\Zhigang Zhang\Documents\Visual Studio 2010\Projects\ExtractDataFromExcel\MyThesis\duration.xml");
                    string strJsCode = PageHelper.WriteJsMsg("Good,tou have sucessfully deleted the specified activity of timespot:" + strId, "MainFormPage.ashx");
                    context.Response.Write(strJsCode);
 
                }
            }



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