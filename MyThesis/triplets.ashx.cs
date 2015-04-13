using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Common;
namespace MyThesis
{
    /// <summary>
    /// triplets 的摘要说明
    /// </summary>
    public class triplets : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Params["type"] == "list_event_number_per_day")
            {
                context.Response.Write(DataHelper.Obj2Json(this.GetDayAndEventTotalNumbers()));
                context.Response.End();
                
            }
            if (context.Request.Params["type"] == "list_all_events")
            {
                List<String> event_list = this.GetEventList();
                string event_list_JSON = DataHelper.Obj2Json(event_list);
                context.Response.Write(event_list_JSON);

                context.Response.End();
 
            }
            if (context.Request.Params["type"] == "list_single_event")
            {
                String eventName = context.Request.Params["eventname"];
                if (eventName != "show_all_event")
                {
                    String verbStr = eventName.Split(new char[] { ':' })[0];
                    String objectStr = eventName.Split(new char[] { ':' })[1];
                    List<DailyItem> single_event_list = this.GetSingleEventList(verbStr, objectStr);
                    string single_event_list_JSON = DataHelper.Obj2Json(single_event_list);
                    context.Response.Write(single_event_list_JSON);
                    context.Response.End();

                }
                else
                {
                    context.Response.Write(DataHelper.Obj2Json(this.GetDayAndEventTotalNumbers()));
                    context.Response.End();
 
                }


               
 
            }


        }

        private List<DailyItem> GetSingleEventList(String verbStr,String objectStr)
        {
            List<DailyItem> day_and_single_event_number = new List<DailyItem>();
            //按行遍历
            //////////////////////////////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@verbStr", SqlDbType.VarChar) { Value = verbStr };
            parameters[1] = new SqlParameter("@objectStr", SqlDbType.VarChar) { Value = objectStr };
            String t_sql = "select  SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) day,count(*) amount from TripletsSet t where Verb=@verbStr and Object=@objectStr group by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) order by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) asc";
            DataTable dt = DAL.SQLHelper.GetDataTable(t_sql,parameters);
            //DataTable dt = DAL.SQLHelper.ExecuteDt("select  SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) day,count(*) amount from TripletsSet t where Verb='activity_expand ' and Object='marie-architecture ' group by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) order by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) asc");
            foreach (DataRow row in dt.Rows)
            {
                //day_and_event_number.Add(Convert.ToString(row["day"]),Convert.ToInt32(row["amount"]));
               DateTime date = Convert.ToDateTime(row["day"]);
                String JSFormatDate = date.Year.ToString() + "," + (date.Month - 1).ToString() + "," + date.Day.ToString();

               DailyItem item = new DailyItem() { date = JSFormatDate, number = Convert.ToInt32(row["amount"]) };
               day_and_single_event_number.Add(item);


            }
            return day_and_single_event_number;
            
        }

        private List<String> GetEventList()
        {
            List<String> event_list = new List<string>();
            //按行遍历TripletsSet表，list里面没有就加进去
            DataTable dt = DAL.SQLHelper.ExecuteDt("select * from TripletsSet");
            foreach (DataRow row in dt.Rows)
            {
                String eventStr=row["Verb"].ToString().Trim()+":"+row["Object"].ToString().Trim();
               if (!event_list.Contains(eventStr))
                {
                    event_list.Add(eventStr);
                }

            }
            return event_list;
 
        }
        private List<DailyItem> GetDayAndEventTotalNumbers()
        {
            List<DailyItem> day_and_event_number = new List<DailyItem>();
            //按行遍历
            DataTable dt = DAL.SQLHelper.ExecuteDt("select  SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) day,count(*) amount from TripletsSet t group by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) order by SUBSTRING(CONVERT(CHAR(23),t.TimeStamp , 121),1,10) asc");
            foreach (DataRow row in dt.Rows)
            {
                //day_and_event_number.Add(Convert.ToString(row["day"]),Convert.ToInt32(row["amount"]));
                DateTime date = Convert.ToDateTime(row["day"]);
                String JSFormatDate= date.Year.ToString() + "," + (date.Month - 1).ToString() + "," + date.Day.ToString();

                DailyItem item = new DailyItem() { date = JSFormatDate, number = Convert.ToInt32(row["amount"]) };
                day_and_event_number.Add(item);
                

            }
            return day_and_event_number;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class DailyItem
    {
        public String date;
        public int number;
    }
}