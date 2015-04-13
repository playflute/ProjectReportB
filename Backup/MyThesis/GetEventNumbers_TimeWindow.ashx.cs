using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace MyThesis
{
    /// <summary>
    /// Summary description for GetEventNumbers_TimeWindow
    /// </summary>
    public class GetEventNumbers_TimeWindow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["type"] == "list_event")
            {
                List <String> event_list=this.GetEventList();
                //string event_list_JSON= DataHelper.Obj2Json(event_list);
                //context.Response.Write(event_list_JSON);
                
                context.Response.End();
            }
            String eventName = "";
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            GetEventNumberOfEventAndTimeWindow(eventName,start,end);
            
        }
        private List<String> GetEventList()
        {
            List<String> event_list=new List<string>();
            //按行遍历EmbeddedSet表，list里面没有就加进去
            DataTable dt= DAL.SQLHelper.ExecuteDt("select * from EmbeddedSet");
            foreach (DataRow row in dt.Rows)
            {
                if (!event_list.Contains(row["Item1"].ToString()))
                {
                    event_list.Add(row["Item1"].ToString());
                }
 
            }
            return event_list;
        }
        private int GetEventNumberOfEventAndTimeWindow(String eventName,DateTime start,DateTime end)
        {
            return 0;
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