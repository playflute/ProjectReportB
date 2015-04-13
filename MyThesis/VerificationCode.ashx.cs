using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;

namespace MyThesis
{
    /// <summary>
    /// VerificationCode 的摘要说明
    /// </summary>
    public class VerificationCode : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            // Whether to generate verification code or not. 
            bool isCreate = true;


            // Session["CreateTime"]: The createTime of verification code 
            if (context.Session["CreateTime"] == null)
            {
                context.Session["CreateTime"] = DateTime.Now;
            }
            else
            {
                DateTime startTime = Convert.ToDateTime(context.Session["CreateTime"]);
                DateTime endTime = Convert.ToDateTime(DateTime.Now);
                TimeSpan ts = endTime - startTime;


                // The time interval to generate a verification code. 
                if (ts.Minutes > 15)
                {
                    isCreate = true;
                    context.Session["CreateTime"] = DateTime.Now;
                }
                else
                {
                    //isCreate = false;
                    isCreate = true;
                }
            }




            context.Response.ContentType = "image/gif";
            //Create Bitmap object and to draw 
            Bitmap basemap = new Bitmap(200, 60);
            Graphics graph = Graphics.FromImage(basemap);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            Random r = new Random();
            string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz0123456789";
            string letter;
            StringBuilder s = new StringBuilder();


            if (isCreate)
            {
                // Add a random five-letter 
                for (int x = 0; x < 5; x++)
                {
                    letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
                    s.Append(letter);


                    // Draw the String 
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), x * 38, r.Next(0, 15));
                }
            }
            else
            {
                // Using the previously generated verification code. 
                string currentCode = context.Session["ValidateCode"].ToString();
                s.Append(currentCode);


                foreach (char item in currentCode)
                {
                    letter = item.ToString();
                    // Draw the String 
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), currentCode.IndexOf(item) * 38, r.Next(0, 15));
                }
            }


            // Confusion background 
            Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
            for (int x = 0; x < 10; x++)
            {
                graph.DrawLine(linePen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
            }


            // Save the picture to the output stream      
            basemap.Save(context.Response.OutputStream, ImageFormat.Gif);
            // If you do not realize the IRequiresSessionState,it will be wrong here,and it can not generate a picture also. 
            context.Session["ValidateCode"] = s.ToString();
            context.Response.End();
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