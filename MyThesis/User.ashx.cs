using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
namespace MyThesis
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class User : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private HttpContext context = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Session["access_token"] = null;
            this.context = context;
            string request_type = context.Request.Params["t"];
            switch (request_type)
            {
                case "login":
                    login();
                    break;
                case "register":
                    regUser();
                    break;
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        #region 注册
        void regUser()
        {
            //1.验证数据
            if (context.Request.Params["verification_code"].ToString() != context.Session["ValidateCode"].ToString())
            {
                AjaxMsgHelper.AjaxMsg("err", "输入验证码不正确");
                context.Response.End();
            }

            //2.使用实体，封装数据
            MODEL.Users userModel = new MODEL.Users()
            {
                UName = context.Request.Params["user_name"],
                ULoginName = context.Request.Params["login_name_n"],
                UPwd = DataHelper.MD5(context.Request.Params["password_first"]),
                UAddtime = DateTime.Now,
                UIsDel = false
            };
            //3.调用业务层对象，封装数据
            BLL.Users users = new BLL.Users();

            //把新注册的用户的ID存入session中
            try
            {
                int CurrentNewUserID = users.Add(userModel);
                MODEL.Users new_user = new BLL.Users().GetModel(CurrentNewUserID);
                context.Session["User_Info"] = new_user;
                //context.Response.Redirect("/View/MsgList.aspx");这么做是没有意义的，浏览器不会跳转，因为这只不过是个异步对象
                AjaxMsgHelper.AjaxMsg("302", "注册成功", null, "ActivityNumberSelectable.aspx");
                context.Response.End();
            }
            catch (Exception e)
            {
                throw;
            }


        }
        #endregion

        public void login()
        {
            //验证验证码,登陆页面可以不需要验证码
            //if (context.Request.Params["UCode"].ToString() != context.Session["ValidateCode"].ToString())
            //{
            //    AjaxMsgHelper.AjaxMsg("err", "输入验证码不正确");
            //    context.Response.End();
            //}
            //如果是使用facebook登陆的话，把用户token保存在session内
            if(context.Request.Params["access_token"]!=null)
            {
                context.Session["access_token"] = context.Request.Params["access_token"];
                //context.Server.Transfer("ActivityNumberSelectable.aspx");
                //context.Response.Redirect("http://www.baidu.com");
                context.Response.Write("ActivityNumberSelectable.aspx");
                //context.Response.End();
                //context.Response.Write("who am I");
               
              
            }
            else
            {
                //使用实体封装数据
                MODEL.Users u = new MODEL.Users()
                {
                    ULoginName = context.Request.Params["ULoginName"].ToString(),
                    UPwd = context.Request.Params["UPwd"].ToString()
                };

                //1.查找登录名
                u = new BLL.Users().FindUser(u);


                if (u == null)
                {//1.1查无此登录名，返回json
                    //Console.Write("查无此用户");在这里是没有用的
                    System.Diagnostics.Debug.Write("查无此用户");
                    AjaxMsgHelper.AjaxMsg("no_user_found", "User name not found!");

                }
                else
                { //1.2有这个登录名，验证密码，密码正确的话跳转MsgList.aspx
                    //Console.Clear();
                    //Console.WriteLine("找得到了用户"+u.UName);
                    if (DataHelper.MD5(context.Request.Form["UPwd"]) == u.UPwd)
                        System.Diagnostics.Debug.Write("找得到了用户" + u.ULoginName);
                    context.Session["User_Info"] = u;

                    if (DataHelper.MD5(context.Request.Form["UPwd"]) == u.UPwd)
                    {
                        AjaxMsgHelper.AjaxMsg("find_suceess", "找到了这个用户:" + u.ULoginName, null, "ActivityNumberSelectable.aspx");
                        if (context.Request.Form["chkAlways"] == "on")//使用cookie保存用户登陆信息一小时
                        {
                            HttpCookie cookie = new HttpCookie("User_Info", u.UId.ToString());//注意只保存id，这样安全
                            cookie.Expires = DateTime.Now.AddMinutes(60);
                            cookie.Path = "";
                            context.Response.Cookies.Add(cookie);

                        }
                    }
                    else
                    {
                        AjaxMsgHelper.AjaxMsg("no_user_found", "Password incorrect");
                    }

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