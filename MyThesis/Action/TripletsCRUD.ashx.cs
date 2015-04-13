using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MyThesis.Action
{
    /// <summary>
    /// TrippletCRUD 的摘要说明
    /// </summary>
    public class TripletsCRUD : IHttpHandler
    {
        HttpContext context = null;
        BLL.TripletsSet ts = new BLL.TripletsSet();
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string strType = context.Request.Params["type"];
            switch (strType)
            {
                case "get_list"://加载班级列表
                    GetTripletsList(Convert.ToInt32(context.Request.Params["page"]),Convert.ToInt32(context.Request.Params["rows"]));
                    break;
                case "delete"://软删除班级
                    DoDel();
                    break;
            }
        }



        private void GetTripletsList(int PageNo, int PageSize)
        {
            ////1.2获取TripletSet表  数据
            //IList<MODEL.TripletsSet> list = ts.GetList();
            ////1.3将 集合数据 生成 json格式 字符串 发回浏览器
            ////1.3.1创建js序列化器对象
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //jss.MaxJsonLength = Int32.MaxValue;
            ////1.3.2把集合 转成 json 数组格式字符串
            //string strJson = jss.Serialize(list);
            ////1.4发回到浏览器
            //// AjaxMsgHelper.AjaxMsg("ok", "加载成功~", strJson);
            //context.Response.Write(strJson);
            MODEL.PageInfo page_info = ts.getPageList(PageNo, PageSize);//获取第三页，每张页面5条记录
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //string strJson = jss.Serialize(page_info);
            //////////
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("total", page_info.total_row_number);
            d.Add("rows", page_info.list);
            string strJson = jss.Serialize(d);
            /////////
            context.Response.Write(strJson);
        }

        private void DoDel()
        {

            string strCid = context.Request.Form["triplet_id"];


            try
            {
                ts.Del(strCid);
                context.Response.Write("Delete_Success");

            }
            catch (Exception)
            {
                context.Response.Write("Delete_Fail");

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