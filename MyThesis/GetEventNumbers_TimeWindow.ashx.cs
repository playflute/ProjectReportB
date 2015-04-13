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
    /// Summary description for GetEventNumbers_TimeWindow
    /// </summary>
    public class GetEventNumbers_TimeWindow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["type"] == "list_event")
            {
                List <String> event_list=this.GetEventList();
                string event_list_JSON= DataHelper.Obj2Json(event_list);
                context.Response.Write(event_list_JSON);
                
                context.Response.End();
            }
            if (context.Request.Params["type"] == "query")
            {
                if (context.Request.Params["eventName"] == null)
                {
                    //this is the request when initialize the page
                    List<String> event_list = this.GetEventList();
                    String eventName=event_list[0];////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    DataTable dt_top = SQLHelper.ExecuteDt("select top 1  * from EmbeddedSet order by TimeStamp Asc");
                    DateTime initial_start_date = Convert.ToDateTime(dt_top.Rows[0]["TimeStamp"]);
                        DateTime start=initial_start_date ;

                        DataTable dt_bottom = SQLHelper.ExecuteDt("select top 1  * from EmbeddedSet order by TimeStamp DESC");
                        DateTime initial_end_date = Convert.ToDateTime(dt_bottom.Rows[0]["TimeStamp"]);
                        DateTime end = initial_end_date;
                    Dictionary<String, int> EventNumberOfEventAndTimeWindow = GetEventNumberOfEventAndTimeWindow(eventName, start, end);
                    context.Response.Write(Common.DataHelper.Obj2Json(EventNumberOfEventAndTimeWindow));

                }

                else
                {
                    String eventName = context.Request.Params["eventName"];
                    String startTime = context.Request.Params["startTime"];
                    String endTime = context.Request.Params["endTime"];
                    //有问题，还未将字符串转化为下面的DateTime对象
                    DateTime start = DateTime.Parse(startTime);
                    DateTime end = Convert.ToDateTime(endTime);
                    Dictionary<String, int> EventNumberOfEventAndTimeWindow = GetEventNumberOfEventAndTimeWindow(eventName, start, end);
                    context.Response.Write(Common.DataHelper.Obj2Json(EventNumberOfEventAndTimeWindow)); 
                }
 
            }

            
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
        private Dictionary<String,int> GetEventNumberOfEventAndTimeWindow(String eventName,DateTime start,DateTime end)
        {
            Dictionary<String, int> dictionary = new Dictionary<string, int>();
            //格式例子 select COUNT(*) from
            //(Select * from EmbeddedSet where Item1='marie_question_1' and Item2=0 and TimeStamp between '2013-08-25 17:50:53.000' and '2013-08-25 17:56:53.000' ) as target_view
            //查询0的数量
            SqlParameter[] parameters=new SqlParameter[3];
             parameters[0]=new SqlParameter("@eventName", SqlDbType.VarChar){Value=eventName};
            parameters[1]=new SqlParameter("@startTime", SqlDbType.DateTime){Value=start};
            parameters[2]=new SqlParameter("@endTime", SqlDbType.DateTime){Value=end};
           // SQLHelper.ExecuteSql("Select * from EmbeddedSet where Item1='marie_question_1' and Item2=0 and TimeStamp between '2013-08-25 17:50:53.000' and '2013-08-25 17:56:53.000'")
            String sql0 = "select COUNT(*) from (Select * from EmbeddedSet where Item1=@eventName and Item2=0 and TimeStamp between @startTime and @endTime ) as target_view";
            DataTable dt0 = SQLHelper.GetDataTable(sql0, parameters);

            if (int.Parse(dt0.Rows[0][0].ToString()) > 0)
            {
                dictionary.Add("0", Convert.ToInt32(dt0.Rows[0][0].ToString()));
            }
            else
            {
                dictionary.Add("0", 0);
 
            }
            //查询1的数量
            String sql1 = "select COUNT(*) from (Select * from EmbeddedSet where Item1=@eventName and Item2=1 and TimeStamp between @startTime and @endTime ) as target_view";
            DataTable dt1 = SQLHelper.GetDataTable(sql1, parameters);
            if (int.Parse(dt1.Rows[0][0].ToString()) > 0)
            {
                dictionary.Add("1", Convert.ToInt32(dt1.Rows[0][0].ToString()));
            }
            else
            {
                dictionary.Add("1", 0);

            }
            //查询-1的数量

            String sqlminus1 = "select COUNT(*) from (Select * from EmbeddedSet where Item1=@eventName and Item2=-1 and TimeStamp between @startTime and @endTime ) as target_view";
            DataTable dtminus1 = SQLHelper.GetDataTable(sqlminus1, parameters);
            if (int.Parse(dtminus1.Rows[0][0].ToString()) > 0)
            {
                dictionary.Add("-1", Convert.ToInt32(dtminus1.Rows[0][0].ToString()));
            }
            else
            {
                dictionary.Add("-1", 0);

            }
            return dictionary;
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